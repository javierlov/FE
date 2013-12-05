using System;
using System.Data;

namespace FacturaElectronica.PrintEngine
{
    public partial class ReporteBase : DevExpress.XtraReports.UI.XtraReport
    {
        const string FORMATO_NRO = "{0:#,##0.00}";
        
        public ReporteBase()
        {
            InitializeComponent();
        }

        private void xrTableCellTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                //Si la linea corresponde a una 'bonificacion'...
                DataRow dr = ((System.Data.DataTable)this.DataSource).Rows[this.CurrentRowIndex];
                if (Convert.ToInt32(dr["ImporteBonificacion"]) > 0)
                    xrTableCellTotal.Text = "-" + xrTableCellTotal.Text;
            }
            catch (Exception)
            {
            }
        }

        private void xrTableCellDetalle_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                //Si la linea corresponde a una 'bonificacion'...
                DataRow dr = ((System.Data.DataTable)this.DataSource).Rows[this.CurrentRowIndex];
                if (Convert.ToInt32(dr["ImporteBonificacion"]) > 0)
                    xrTableCellDetalle.Text = "Bonificación";
            }
            catch (Exception)
            {
            }
        }

        //private void xrTableCellTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    if (txtLetraCbte.Text != "B")
        //        xrTableCellTotal.Text = String.Format(FORMATO_NRO, Convert.ToDecimal(GetCurrentColumnValue("ImporteSubtotalMonedaFacturacion").ToString()));
        //    else
        //        xrTableCellTotal.Text = String.Format(FORMATO_NRO, Convert.ToDecimal(GetCurrentColumnValue("ImporteSubtotalMonedaFacturacionConIVA").ToString()));
        //}
    }
}
