using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Test.FacturaElectronica.WebServices.FEServ;

namespace Test.FacturaElectronica.WebServices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FEWServ.FEServices feService = new Test.FacturaElectronica.WebServices.FEWServ.FEServices();            
            ////feService.Timeout = 10000;
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"C:\CONVERTIDO.xml");
            //string response = feService.ProcesarLoteFacturas(xmlDoc.OuterXml.ToString());

            //FEWS.FEServices feService = new Test.FacturaElectronica.WebServices.FEWS.FEServices();
            //feService.Timeout = 10000;
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"C:\FE\RequestBatch.xml");
            //FEWServ.RequestBatch requestBatch = new FEWServ.RequestBatch();
            //FEWServ.ResponseBatch responseBatch = new FEWServ.ResponseBatch();

            //responseBatch = feService.ProcesarLoteFacturasBienesServiciosObj("5", requestBatch);
            //string response = feService.ProcesarLoteFacturasBienesServicios("5", xmlDoc.OuterXml);


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\convertido.xml");
            FEServ.FEServices feservices = new FEServices();
            string response = feservices.ProcesarLoteFacturasBienesServicios("3", xmlDoc.OuterXml.ToString());

            richTextBox1.Text = response;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\convertido.xml");

            //richTextBox1.Lines.Intersect("inicio");
            UtilXML ux = new UtilXML();
            string response = ux.TransformXML(xmlDoc.OuterXml.ToString());
            richTextBox1.Text = response;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\convertido.xml");

            UtilXML ux = new UtilXML();
            ux.LoadXMLString(xmlDoc.OuterXml.ToString());


            //FEServ.FEServices feservices = new FEServices();
            string EmpresaID = "3";
            //RequestBatch documentBatch = new RequestBatch();
           // ResponseBatch response = feservices.ProcesarLoteFacturasBienesServiciosObj(EmpresaID, documentBatch);

        }

        private RequestBatch GetResponseBatch()
        {
            RequestBatch documentBatch = new RequestBatch();
            documentBatch.BatchUniqueId = "";

            return documentBatch;
        }
    }
}
