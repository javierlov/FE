using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using FacturaElectronica.Service.Engine.FEWS;
using FacturaElectronica.Utils;
using FacturaElectronica.Service.Engine;
using System.Security.Permissions;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Threading;
using RequestBatch = FacturaElectronica.Common.RequestBatch;
using ResponseBatch = FacturaElectronica.Common.ResponseBatch;

namespace FacturaElectronica.Service
{
    public partial class Form1 : Form
    {
        private FileSystemWatcher FolderWatcher = null;
        private IntegrationEngine integrationEngine = new IntegrationEngine();
        private DBIntegration dbIntegration = new DBIntegration();
        private Thread tProcess = null;
        private bool stopThreads = false;
 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void button1_Click(object sender, EventArgs e)
        {
            Settings oSettings = null;
            EmpresaCollection eColl = null;

            try
            {
                eColl = Settings.GetEmpresas();

                foreach (Empresa oEmp in eColl)
                {
                    oSettings = new Settings(oEmp.EmpresaID);

                    switch (oSettings.TipoEntrada)
                    {
                        case Settings.TipoDato.FS:

                            StartFSProcess(oSettings);
                            break;

                        case Settings.TipoDato.MSSQL:

                            StartMSSQLProcess(oSettings);

                            break;

                        case Settings.TipoDato.MSMQ:
                            break;

                        case Settings.TipoDato.WS:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", "OnStart Error: " + ex.Message, EventLogEntryType.Error);

                this.Close();
            }
        }

        private void StartMSSQLProcess(Settings oSettings)
        {
            try
            {
                //while (!stopThreads)
                //{
                    tProcess = new Thread(new ParameterizedThreadStart(GetMSSQLDataToProcess));
                    tProcess.Start(oSettings);
                //}
                //using (BackgroundWorker bw = new BackgroundWorker())
                //{
                //    bw.WorkerReportsProgress = false;
                //    bw.WorkerSupportsCancellation = true;

                //    bw.DoWork += RunProcessBegin;

                //    bw.RunWorkerAsync(oSettings);
                //}
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine("<ERROR><Accion>Service(StartMSSQLProcess)</Accion><Resultado>Error: " + ex.Message + "</Resultado></ERROR>", oSettings.PathDebug + "\\000-P6.ERROR-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");
            }
        }

        //private void RunProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //}

        //private void RunProcessBegin(object sender, DoWorkEventArgs e)
        //{
        //    DataTable dtCabecera = new DataTable();

        //    Settings oSettings = (Settings)e.Argument;

        //    do
        //    {
        //        dtCabecera = dbIntegration.GetData(oSettings.Entrada.Split('\\')[0], oSettings.Entrada.Split('\\')[1], oSettings.Entrada.Split('\\')[2], "Estado is null or Estado like '' and RESULTADO_PROCEDURE = 't'", "*", 1);

        //        GetMSSQLDataToProcess(dtCabecera, oSettings);

        //        Thread.Sleep(5000);
        //    }
        //    while (true);
        //}

        private void StartFSProcess(Settings oSettings)
        {
            FolderWatcher = new FileSystemWatcher();
            FolderWatcher.Path = oSettings.Entrada;
            FolderWatcher.Filter = "*.txt";
            FolderWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;

            FolderWatcher.Created += (s, e) => OnCreatedFile(s, e, oSettings);
            FolderWatcher.Error += new ErrorEventHandler(OnWatcherError);
            FolderWatcher.IncludeSubdirectories = false;
            FolderWatcher.EnableRaisingEvents = true;
            
            GetOldFilesOnInputFolder(oSettings);
        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            System.Diagnostics.EventLog.WriteEntry("Servicio FE Integración", "Watcher Error: " + e.GetException().Source + ", " + e.GetException().Message);
        }

        private void OnCreatedFile(object sender, FileSystemEventArgs e, Settings oSettings)
        {
            try
            {               
                FolderWatcher.EnableRaisingEvents = false;

                DirectoryInfo dirInput = new DirectoryInfo(oSettings.Entrada);
                FileInfo[] rgFiles = dirInput.GetFiles("*.*");
                while (rgFiles.Length > 0)
                {
                    GetOldFilesOnInputFolder(oSettings);
                    rgFiles = dirInput.GetFiles("*.*");
                }

                FolderWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Servicio FE Integración", "Disabling events: " + ex.Message);
                FolderWatcher.EnableRaisingEvents = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings oSettings = null;
            EmpresaCollection eColl = null;

            try
            {
                eColl = Settings.GetEmpresas();
                
                foreach (Empresa oEmp in eColl)
                {
                    oSettings = new Settings(oEmp.EmpresaID);

                    switch (oSettings.TipoEntrada)
                    {
                        case Settings.TipoDato.FS:

                            FolderWatcher.EnableRaisingEvents = false;
                            break;

                        case Settings.TipoDato.MSSQL:

                            stopThreads = true;
                            //SqlDependency.Stop(Settings.GetConnectionString(oSettings.Entrada.Split('\\')[0], oSettings.Entrada.Split('\\')[1]));
                            break;

                        case Settings.TipoDato.MSMQ:
                            break;

                        case Settings.TipoDato.WS:
                            break;
                    }
                }
                eColl.Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", "OnStop Error: " + ex.Message, EventLogEntryType.Error);
            }
        }

        private void GetMSSQLDataToProcess(object ParamSettings)
        {
            RequestBatch requestDB = null;
            ResponseBatch responseDB = null;

            DataTable dtCabecera = new DataTable();
            DataTable dtReprocessed = null;
            DataTable dtLineas = null;
            DataTable dtImpuestos = null;
            Settings oSettings = null;

            int iExceptinonsCount = 0;

            try
            {
                oSettings = (Settings)ParamSettings;
            }
            catch (Exception ex)
            {
                iExceptinonsCount = 3;
                MessageBox.Show(ex.Message, "Error");
            }

            // si ocurren mas de 3 excepciones seguidas detengo el servicio
            // provablemente por errores de conexion con la base de datos            

            while (!stopThreads && iExceptinonsCount < 3)
            {
                try
                {
                    //thread safe
                    lock (this)
                    {
                        dtCabecera = dbIntegration.GetData(oSettings.Entrada.Split('\\')[0], oSettings.Entrada.Split('\\')[1], oSettings.Entrada.Split('\\')[2], "Estado is null or Estado like '' and RESULTADO_PROCEDURE = 't'", "*", 1);

                        foreach (DataRow dr in dtCabecera.Rows)
                        {
                            dtReprocessed = new DataTable();
                            dtLineas = new DataTable();

                            dbIntegration.ProcessInitialized("", dr[oSettings.Entrada.Split('\\')[3]].ToString(), oSettings);

                            //verifico si este comprobante no fue procesado antes y es un reproceso
                            dtReprocessed = dbIntegration.GetData("cbtecabecera", "NroComprobanteDesde = '" + dr["NroComprobante"].ToString() + "' and PuntoVenta = '" + dr["PuntoVenta"].ToString() + "' and TipoComprobante = '" + dr["TipoComprobante"].ToString() + "' and EstadoTransaccion = 'Aceptado'", "BatchUniqueId");

                            // espero 1 segundo antes de leer la linea por si aun no se copio, lo repito 3 veces
                            int iSelectsCount = 0;
                            do
                            {
                                Thread.Sleep(1000);

                                dtLineas = dbIntegration.GetData(oSettings.EntradaExtra.Split('\\')[0], oSettings.EntradaExtra.Split('\\')[1], oSettings.EntradaExtra.Split('\\')[2], oSettings.Entrada.Split('\\')[3] + "=" + dr[oSettings.Entrada.Split('\\')[3]].ToString(), "*");

                                iSelectsCount++;
                            }
                            while (dtLineas.Rows.Count == 0 && iSelectsCount < 4);

                            dr.Table.TableName = oSettings.Entrada.Split('\\')[2];
                            dtLineas.TableName = oSettings.EntradaExtra.Split('\\')[2];

                            requestDB = dbIntegration.ProcessData(dr, dtLineas, dtImpuestos, oSettings);

                            if (dtReprocessed.Rows.Count == 0)
                            {
                                responseDB = integrationEngine.ProcessRequest(requestDB, oSettings, false);
                            }
                            else
                            {
                                responseDB = integrationEngine.ProcessRequest(requestDB, oSettings, true);
                                responseDB.Reproceso = "s";
                            }

                            dbIntegration.ProcessResponse(responseDB, oSettings);

                            dtReprocessed.Clear();
                            dtReprocessed.Dispose();

                            dtLineas.Clear();
                            dtLineas.Dispose();
                        }

                        dtCabecera.Clear();
                        dtCabecera.Dispose();
                    }

                    //espero 3 segundos para volver a preguntar por un nuevo registro
                    Thread.Sleep(3000);

                    iExceptinonsCount = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");

                    if (Convert.ToBoolean(oSettings.ActivarDebug))
                        Utils.Utils.DebugLine("<ERROR><Accion>MSSQL(GetOldMSSQLDataToProcess)</Accion><Resultado>Error: " + ex.Message + "</Resultado></ERROR>", oSettings.PathDebug + "\\000-P6.ERROR-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                    System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", ex.Message, EventLogEntryType.Error);
                }
            }
            if (iExceptinonsCount >= 3)
                this.Close();
        }

        private void GetOldFilesOnInputFolder(Settings oSettings)
        {
            RequestBatch rbFileResult = null;
            ResponseBatch rbResult = null;

            try
            {
                DirectoryInfo dirInput = new DirectoryInfo(oSettings.Entrada);
                FileIntegration fileIntegration = new FileIntegration();
                IntegrationEngine integrationEngine = new IntegrationEngine();

                FileInfo[] rgFiles = dirInput.GetFiles("*.*");

                foreach (FileInfo fi in rgFiles)
                {
                    rbFileResult = fileIntegration.ProcessData(fi, oSettings);

                    rbResult = integrationEngine.ProcessRequest(rbFileResult, oSettings, false);

                    fileIntegration.ProcessResponse(rbResult, oSettings);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("F. Elect. Integracion", ex.Source +", "+  ex.InnerException + ", " +  ex.Message, System.Diagnostics.EventLogEntryType.Error);
                FolderWatcher.EnableRaisingEvents = false;
            }
        }

        static private string GetRegistryKey(string RegPath, string regKey)
        {
            return FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();

            /*
            string strResult = string.Empty;
            RegistryKey key1 = null;
            key1 = Registry.LocalMachine.OpenSubKey(RegPath);
            if (key1 == null)
                throw (new Exception("Could not load registry key"));
            strResult = Convert.ToString(key1.GetValue(regKey));
            return strResult;
             * */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmpresaCollection eColl = Settings.GetEmpresas();

            foreach (Empresa oEmp in eColl)
            {
                Settings oSettings = new Settings(oEmp.EmpresaID);

                switch (oSettings.TipoEntrada)
                {
                    case Settings.TipoDato.FS:

                        FolderWatcher.EnableRaisingEvents = false;
                        break;

                    case Settings.TipoDato.MSSQL:

                        SqlDependency.Stop(Settings.GetConnectionString(oSettings.Entrada.Split('\\')[0], oSettings.Entrada.Split('\\')[1]));
                        break;

                    case Settings.TipoDato.MSMQ:
                        break;

                    case Settings.TipoDato.WS:
                        break;
                }
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string xmlstr = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting(); 
            label1.Text = xmlstr;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();
            System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", "CLICK button: " + label1.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) comboBox1.SelectedIndex = 0;

            string idEmpresa = comboBox1.Text;
            string xmlstr = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting(); 
            label1.Text = xmlstr;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\convertido.xml");

            FEServices feservices = new FEServices();
            string response = feservices.ProcesarLoteFacturasBienesServicios("3", xmlDoc.OuterXml.ToString());

            MessageBox.Show(response);
        }

    }
}
