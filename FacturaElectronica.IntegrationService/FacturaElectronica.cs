using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using FacturaElectronica.Common;
using FacturaElectronica.Utils;
using FacturaElectronica.Service.Engine;
using System.Security.Permissions;

namespace FacturaElectronica.IntegrationService
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class FacturaElectronica : ServiceBase
    {
        private FileSystemWatcher FolderWatcher = null;
        private IntegrationEngine integrationEngine = new IntegrationEngine();
        private DBIntegration dbIntegration = new DBIntegration();
        private Thread tProcess = null;
        private bool stopThreads = false;

        public FacturaElectronica()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
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
                eColl.Clear();               
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", "OnStart Error: " + ex.Message, EventLogEntryType.Error);

                this.Stop();
            }
        }

        protected override void OnStop()
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

        private void StartMSSQLProcess(Settings oSettings)
        {
            try
            {
                tProcess = new Thread(new ParameterizedThreadStart(GetMSSQLDataToProcess));
                tProcess.Start(oSettings);
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine("<ERROR><Accion>Service(StartMSSQLProcess)</Accion><Resultado>Error: " + ex.Message + "</Resultado></ERROR>", oSettings.PathDebug + "\\000-P6.ERROR-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");
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

            Settings oSettings = (Settings)ParamSettings;

            // si ocurren mas de 3 excepciones seguidas detengo el servicio
            // provablemente por errores de conexion con la base de datos
            int iExceptinonsCount = 0;

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

                            // espero 1 segundo antes de leer la linea por si aun no se copio, lo repito 2 veces
                            int iSelectsCount = 0;
                            do
                            {
                                Thread.Sleep(1000);

                                dtLineas = dbIntegration.GetData(oSettings.EntradaExtra.Split('\\')[0], oSettings.EntradaExtra.Split('\\')[1], oSettings.EntradaExtra.Split('\\')[2], oSettings.Entrada.Split('\\')[3] + "=" + dr[oSettings.Entrada.Split('\\')[3]].ToString(), "*");

                                iSelectsCount++;
                            }
                            while (dtLineas.Rows.Count == 0 && iSelectsCount < 3);

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

                            //Guardo respuesta en la base intermedia
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
                    iExceptinonsCount++;

                    if (Convert.ToBoolean(oSettings.ActivarDebug))
                        Utils.Utils.DebugLine("<ERROR><Accion>MSSQL(GetOldMSSQLDataToProcess)</Accion><Resultado>ExeptionCount: " + iExceptinonsCount.ToString() + ". Error: " + ex.Message + "</Resultado></ERROR>", oSettings.PathDebug + "\\000-P6.ERROR-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                    System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", ex.Message, EventLogEntryType.Error);
                }
            }

            if (iExceptinonsCount >= 3)
                this.Stop();
        }

        private void StartFSProcess(Settings oSettings)
        {
            FolderWatcher = new FileSystemWatcher();
            FolderWatcher.Path = oSettings.Entrada;
            FolderWatcher.Filter = "*.txt";
            FolderWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;

            //FileWatcher.Created += (s,e) => DoSomething(s, e.FullPath, mSettings);
            FolderWatcher.Created += new FileSystemEventHandler(OnCreatedFile);
            FolderWatcher.Error += new ErrorEventHandler(OnWatcherError);
            FolderWatcher.IncludeSubdirectories = false;
            FolderWatcher.EnableRaisingEvents = true;

            GetOldFilesOnInputFolder(oSettings);
        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", "Watcher Error: " + e.GetException().Source + ", " + e.GetException().Message, EventLogEntryType.Error);
        }

        private void OnCreatedFile(object sender, FileSystemEventArgs e)
        {
            try
            {
                string EmpresaID = "1";
                Settings oSettings = new Settings(EmpresaID);

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
                System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", "Disabling events: " + ex.Message, EventLogEntryType.Error);
                FolderWatcher.EnableRaisingEvents = false;

                this.Stop();
            }
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
                System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", ex.Source + ", " + ex.InnerException + ", " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                FolderWatcher.EnableRaisingEvents = false;
            }
        }
    }
}
