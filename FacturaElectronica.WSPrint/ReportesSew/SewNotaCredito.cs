using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FacturaElectronica.PrintEngine
{
    public partial class SewNotaCredito : DevExpress.XtraReports.UI.XtraReport
    {
        public SewNotaCredito()
        {
            InitializeComponent();
        }

        private void xrTableCellTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("ImporteSubtotalMonedaFacturacion") != null)
            {
                xrTableCellTotal.Text = GetFormatedNumber(GetCurrentColumnValue("ImporteSubtotalMonedaFacturacion").ToString(), "{0:#,##0.00}");
            }
        }

        private string GetFormatedNumber(string StringNumber, string StringFormat)
        {
            string strReturn = string.Empty;

            //put .00 for import columns
            if (StringNumber != null && StringNumber.Length > 0)
            {
                string ImporteAux = StringNumber;

                if (ImporteAux.Split('.').Length > 1)
                {
                    if (ImporteAux.Split('.')[1].Length == 1)
                    {
                        ImporteAux += "0";
                    }
                }
                else
                {
                    ImporteAux += ".00";
                }

                ImporteAux = String.Format(StringFormat, Convert.ToDecimal(ImporteAux));

                strReturn = ImporteAux.Split('.')[0].Replace(",", ".") + "," + ImporteAux.Split('.')[1];
            }
            else
            {
                strReturn = "0.00";
            }

            return strReturn;
        }

        private void xrTableCellCantidad_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("Cantidad") != null)
            {
                xrTableCellCantidad.Text = GetFormatedNumber(GetCurrentColumnValue("Cantidad").ToString(), "{0:#,##0.00}");
            }
        }

    }
}
