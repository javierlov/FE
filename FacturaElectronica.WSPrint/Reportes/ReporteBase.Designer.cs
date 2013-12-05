namespace FacturaElectronica.PrintEngine
{
    public partial class ReporteBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator code39ExtendedGenerator1 = new DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator();
            DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator interleaved2of5Generator1 = new DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteBase));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.lblTituloRapipago = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBarCodeRapipago = new DevExpress.XtraReports.UI.XRBarCode();
            this.txtFechaVto = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCae = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtBarCodeCbte = new DevExpress.XtraReports.UI.XRBarCode();
            this.lbl4 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDomicilio = new DevExpress.XtraReports.UI.XRLabel();
            this.txtNombreEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl6 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl7 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtNumCodIIBB = new DevExpress.XtraReports.UI.XRLabel();
            this.txtNumIIBB = new DevExpress.XtraReports.UI.XRLabel();
            this.txtLugarDeEntrega = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl3 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTipoIva = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCondiciondeVenta = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl5 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtNumCuit = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.rtbDatosEmpresa2 = new DevExpress.XtraReports.UI.XRRichText();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.controlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.txtNumCodigo = new DevExpress.XtraReports.UI.XRLabel();
            this.txtLetraCbte = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTipoCbte = new DevExpress.XtraReports.UI.XRLabel();
            this.rtbDatosEmpresa3 = new DevExpress.XtraReports.UI.XRRichText();
            this.lbl18 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTipo = new DevExpress.XtraReports.UI.XRLabel();
            this.txtNroCbte = new DevExpress.XtraReports.UI.XRLabel();
            this.picLogoCabecera = new DevExpress.XtraReports.UI.XRPictureBox();
            this.rtbDatosEmpresa = new DevExpress.XtraReports.UI.XRRichText();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.CbteDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellDetalle = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellLabelDetalle = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellLabelTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbl16 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl10 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtImporteIIBBMonedaFacturacion = new DevExpress.XtraReports.UI.XRLabel();
            this.txtMoneda = new DevExpress.XtraReports.UI.XRLabel();
            this.txtTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.txtSon = new DevExpress.XtraReports.UI.XRLabel();
            this.txtRefInterna = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl15 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl11 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTableImportes = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lbl20 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtUsuario = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.txtDAGRUF = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl17 = new DevExpress.XtraReports.UI.XRLabel();
            this.txCPer = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl19 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtFechaCbte = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
            this.xrCrossBandBox1 = new DevExpress.XtraReports.UI.XRCrossBandBox();
            this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.GroupFooter4 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.txtCodeRapipago = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine7 = new DevExpress.XtraReports.UI.XRLine();
            this.picLogoRapipago = new DevExpress.XtraReports.UI.XRPictureBox();
            this.txtBarcodeRapipago2 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter5 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.txtObservaciones3 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter0 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.txtInformacionAdicional = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTituloPercepcionIIBBDocsB = new DevExpress.XtraReports.UI.XRLabel();
            this.txtPercepcionIIBBDocsB = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.rtbDatosEmpresa2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbDatosEmpresa3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbDatosEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CbteDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableImportes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // PageFooter
            // 
            this.PageFooter.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.PageFooter.Dpi = 254F;
            this.PageFooter.HeightF = 64F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.StylePriority.UseBorders = false;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPageInfo1.Dpi = 254F;
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrPageInfo1.Format = "{0} / {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(24.44045F, 0F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(1914.948F, 46.21167F);
            this.xrPageInfo1.StylePriority.UseBorders = false;
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblTituloRapipago
            // 
            this.lblTituloRapipago.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblTituloRapipago.BorderWidth = 0;
            this.lblTituloRapipago.Dpi = 254F;
            this.lblTituloRapipago.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTituloRapipago.LocationFloat = new DevExpress.Utils.PointFloat(297.5095F, 58.20833F);
            this.lblTituloRapipago.Name = "lblTituloRapipago";
            this.lblTituloRapipago.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTituloRapipago.SizeF = new System.Drawing.SizeF(1626.491F, 58.41999F);
            this.lblTituloRapipago.StylePriority.UseBorders = false;
            this.lblTituloRapipago.StylePriority.UseBorderWidth = false;
            this.lblTituloRapipago.StylePriority.UseFont = false;
            this.lblTituloRapipago.StylePriority.UseTextAlignment = false;
            this.lblTituloRapipago.Text = "    Código Rapipago";
            this.lblTituloRapipago.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtBarCodeRapipago
            // 
            this.txtBarCodeRapipago.AutoModule = true;
            this.txtBarCodeRapipago.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtBarCodeRapipago.Dpi = 254F;
            this.txtBarCodeRapipago.Font = new System.Drawing.Font("Arial", 7F);
            this.txtBarCodeRapipago.LocationFloat = new DevExpress.Utils.PointFloat(297.5095F, 116.6284F);
            this.txtBarCodeRapipago.Module = 10F;
            this.txtBarCodeRapipago.Name = "txtBarCodeRapipago";
            this.txtBarCodeRapipago.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
            this.txtBarCodeRapipago.ShowText = false;
            this.txtBarCodeRapipago.SizeF = new System.Drawing.SizeF(1626.49F, 93.69773F);
            this.txtBarCodeRapipago.StylePriority.UseBorders = false;
            this.txtBarCodeRapipago.StylePriority.UseFont = false;
            this.txtBarCodeRapipago.StylePriority.UseTextAlignment = false;
            code39ExtendedGenerator1.WideNarrowRatio = 3F;
            this.txtBarCodeRapipago.Symbology = code39ExtendedGenerator1;
            this.txtBarCodeRapipago.Text = "893010006000000130004150011290000221080";
            this.txtBarCodeRapipago.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            this.txtBarCodeRapipago.Visible = false;
            // 
            // txtFechaVto
            // 
            this.txtFechaVto.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtFechaVto.Dpi = 254F;
            this.txtFechaVto.Font = new System.Drawing.Font("Arial", 9F);
            this.txtFechaVto.LocationFloat = new DevExpress.Utils.PointFloat(1660.194F, 58.41988F);
            this.txtFechaVto.Name = "txtFechaVto";
            this.txtFechaVto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtFechaVto.SizeF = new System.Drawing.SizeF(277.2358F, 58.41988F);
            this.txtFechaVto.StylePriority.UseBorders = false;
            this.txtFechaVto.StylePriority.UseFont = false;
            this.txtFechaVto.StylePriority.UseTextAlignment = false;
            this.txtFechaVto.Text = "N/A";
            this.txtFechaVto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtCae
            // 
            this.txtCae.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtCae.Dpi = 254F;
            this.txtCae.Font = new System.Drawing.Font("Arial", 9F);
            this.txtCae.LocationFloat = new DevExpress.Utils.PointFloat(1660.194F, 0F);
            this.txtCae.Name = "txtCae";
            this.txtCae.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtCae.SizeF = new System.Drawing.SizeF(277.2358F, 58.4199F);
            this.txtCae.StylePriority.UseBorders = false;
            this.txtCae.StylePriority.UseFont = false;
            this.txtCae.StylePriority.UseTextAlignment = false;
            this.txtCae.Text = "N/A";
            this.txtCae.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1451.759F, 58.41956F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(208.4346F, 58.42002F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Fecha Vto.:";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(1451.759F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(208.4348F, 58.42004F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "C.A.E.:";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtBarCodeCbte
            // 
            this.txtBarCodeCbte.AutoModule = true;
            this.txtBarCodeCbte.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtBarCodeCbte.Dpi = 254F;
            this.txtBarCodeCbte.Font = new System.Drawing.Font("Arial", 9F);
            this.txtBarCodeCbte.LocationFloat = new DevExpress.Utils.PointFloat(26.95794F, 0F);
            this.txtBarCodeCbte.Module = 10F;
            this.txtBarCodeCbte.Name = "txtBarCodeCbte";
            this.txtBarCodeCbte.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
            this.txtBarCodeCbte.SizeF = new System.Drawing.SizeF(1424.801F, 116.6282F);
            this.txtBarCodeCbte.StylePriority.UseBorders = false;
            this.txtBarCodeCbte.StylePriority.UseFont = false;
            this.txtBarCodeCbte.StylePriority.UseTextAlignment = false;
            interleaved2of5Generator1.CalcCheckSum = false;
            interleaved2of5Generator1.WideNarrowRatio = 2.5F;
            this.txtBarCodeCbte.Symbology = interleaved2of5Generator1;
            this.txtBarCodeCbte.Text = "3067663107719000227066105732510201003221";
            this.txtBarCodeCbte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // lbl4
            // 
            this.lbl4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl4.BorderWidth = 0;
            this.lbl4.Dpi = 254F;
            this.lbl4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl4.LocationFloat = new DevExpress.Utils.PointFloat(11.75925F, 882.1585F);
            this.lbl4.Name = "lbl4";
            this.lbl4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl4.SizeF = new System.Drawing.SizeF(325.4376F, 58.42004F);
            this.lbl4.StylePriority.UseBorders = false;
            this.lbl4.StylePriority.UseBorderWidth = false;
            this.lbl4.StylePriority.UseFont = false;
            this.lbl4.StylePriority.UseTextAlignment = false;
            this.lbl4.Text = "Condición de Venta:";
            this.lbl4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtDomicilio
            // 
            this.txtDomicilio.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtDomicilio.BorderWidth = 0;
            this.txtDomicilio.Dpi = 254F;
            this.txtDomicilio.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDomicilio.LocationFloat = new DevExpress.Utils.PointFloat(238.0552F, 619.5861F);
            this.txtDomicilio.Name = "txtDomicilio";
            this.txtDomicilio.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtDomicilio.SizeF = new System.Drawing.SizeF(1197.828F, 58.42004F);
            this.txtDomicilio.StylePriority.UseBorders = false;
            this.txtDomicilio.StylePriority.UseBorderWidth = false;
            this.txtDomicilio.StylePriority.UseFont = false;
            this.txtDomicilio.StylePriority.UseTextAlignment = false;
            this.txtDomicilio.Text = "N/A";
            this.txtDomicilio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtNombreEmpresa.BorderWidth = 0;
            this.txtNombreEmpresa.Dpi = 254F;
            this.txtNombreEmpresa.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNombreEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(238.0552F, 561.1661F);
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtNombreEmpresa.SizeF = new System.Drawing.SizeF(1197.828F, 58.4201F);
            this.txtNombreEmpresa.StylePriority.UseBorders = false;
            this.txtNombreEmpresa.StylePriority.UseBorderWidth = false;
            this.txtNombreEmpresa.StylePriority.UseFont = false;
            this.txtNombreEmpresa.StylePriority.UseTextAlignment = false;
            this.txtNombreEmpresa.Text = "N/A";
            this.txtNombreEmpresa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl2
            // 
            this.lbl2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl2.BorderWidth = 0;
            this.lbl2.Dpi = 254F;
            this.lbl2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl2.LocationFloat = new DevExpress.Utils.PointFloat(13.15928F, 619.5861F);
            this.lbl2.Name = "lbl2";
            this.lbl2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl2.SizeF = new System.Drawing.SizeF(224.8959F, 58.42004F);
            this.lbl2.StylePriority.UseBorders = false;
            this.lbl2.StylePriority.UseBorderWidth = false;
            this.lbl2.StylePriority.UseFont = false;
            this.lbl2.StylePriority.UseTextAlignment = false;
            this.lbl2.Text = "Domicilio:";
            this.lbl2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl6
            // 
            this.lbl6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl6.BorderWidth = 0;
            this.lbl6.Dpi = 254F;
            this.lbl6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl6.LocationFloat = new DevExpress.Utils.PointFloat(1435.884F, 736.4257F);
            this.lbl6.Name = "lbl6";
            this.lbl6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl6.SizeF = new System.Drawing.SizeF(224.3096F, 58.42004F);
            this.lbl6.StylePriority.UseBorders = false;
            this.lbl6.StylePriority.UseBorderWidth = false;
            this.lbl6.StylePriority.UseFont = false;
            this.lbl6.StylePriority.UseTextAlignment = false;
            this.lbl6.Text = "Nro. IIBB      :";
            this.lbl6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl7
            // 
            this.lbl7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl7.BorderWidth = 0;
            this.lbl7.Dpi = 254F;
            this.lbl7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl7.LocationFloat = new DevExpress.Utils.PointFloat(13.15928F, 736.4258F);
            this.lbl7.Name = "lbl7";
            this.lbl7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl7.SizeF = new System.Drawing.SizeF(224.8951F, 58.42004F);
            this.lbl7.StylePriority.UseBorders = false;
            this.lbl7.StylePriority.UseBorderWidth = false;
            this.lbl7.StylePriority.UseFont = false;
            this.lbl7.StylePriority.UseTextAlignment = false;
            this.lbl7.Text = "Cat. IIBB:";
            this.lbl7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtNumCodIIBB
            // 
            this.txtNumCodIIBB.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtNumCodIIBB.BorderWidth = 0;
            this.txtNumCodIIBB.Dpi = 254F;
            this.txtNumCodIIBB.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNumCodIIBB.LocationFloat = new DevExpress.Utils.PointFloat(238.0553F, 736.4261F);
            this.txtNumCodIIBB.Name = "txtNumCodIIBB";
            this.txtNumCodIIBB.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtNumCodIIBB.SizeF = new System.Drawing.SizeF(1197.828F, 58.41986F);
            this.txtNumCodIIBB.StylePriority.UseBorders = false;
            this.txtNumCodIIBB.StylePriority.UseBorderWidth = false;
            this.txtNumCodIIBB.StylePriority.UseFont = false;
            this.txtNumCodIIBB.StylePriority.UseTextAlignment = false;
            this.txtNumCodIIBB.Text = "N/A";
            this.txtNumCodIIBB.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtNumIIBB
            // 
            this.txtNumIIBB.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtNumIIBB.BorderWidth = 0;
            this.txtNumIIBB.Dpi = 254F;
            this.txtNumIIBB.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNumIIBB.LocationFloat = new DevExpress.Utils.PointFloat(1660.194F, 736.4261F);
            this.txtNumIIBB.Name = "txtNumIIBB";
            this.txtNumIIBB.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtNumIIBB.SizeF = new System.Drawing.SizeF(277.2344F, 58.42004F);
            this.txtNumIIBB.StylePriority.UseBorders = false;
            this.txtNumIIBB.StylePriority.UseBorderWidth = false;
            this.txtNumIIBB.StylePriority.UseFont = false;
            this.txtNumIIBB.StylePriority.UseTextAlignment = false;
            this.txtNumIIBB.Text = "N/A";
            this.txtNumIIBB.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtLugarDeEntrega
            // 
            this.txtLugarDeEntrega.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtLugarDeEntrega.BorderWidth = 0;
            this.txtLugarDeEntrega.Dpi = 254F;
            this.txtLugarDeEntrega.Font = new System.Drawing.Font("Arial", 9F);
            this.txtLugarDeEntrega.LocationFloat = new DevExpress.Utils.PointFloat(11.20103F, 940.5786F);
            this.txtLugarDeEntrega.Name = "txtLugarDeEntrega";
            this.txtLugarDeEntrega.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtLugarDeEntrega.SizeF = new System.Drawing.SizeF(1926.228F, 58.41998F);
            this.txtLugarDeEntrega.StylePriority.UseBorders = false;
            this.txtLugarDeEntrega.StylePriority.UseBorderWidth = false;
            this.txtLugarDeEntrega.StylePriority.UseFont = false;
            this.txtLugarDeEntrega.StylePriority.UseTextAlignment = false;
            this.txtLugarDeEntrega.Text = "Observaciones1 - ObservacionesCabecera";
            this.txtLugarDeEntrega.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl1
            // 
            this.lbl1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl1.BorderWidth = 0;
            this.lbl1.Dpi = 254F;
            this.lbl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl1.LocationFloat = new DevExpress.Utils.PointFloat(13.15928F, 561.1661F);
            this.lbl1.Name = "lbl1";
            this.lbl1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl1.SizeF = new System.Drawing.SizeF(224.8959F, 58.41998F);
            this.lbl1.StylePriority.UseBorders = false;
            this.lbl1.StylePriority.UseBorderWidth = false;
            this.lbl1.StylePriority.UseFont = false;
            this.lbl1.StylePriority.UseTextAlignment = false;
            this.lbl1.Text = "Sr/es:";
            this.lbl1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl3
            // 
            this.lbl3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl3.BorderWidth = 0;
            this.lbl3.Dpi = 254F;
            this.lbl3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl3.LocationFloat = new DevExpress.Utils.PointFloat(11.75925F, 678.0059F);
            this.lbl3.Name = "lbl3";
            this.lbl3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl3.SizeF = new System.Drawing.SizeF(226.296F, 58.42004F);
            this.lbl3.StylePriority.UseBorders = false;
            this.lbl3.StylePriority.UseBorderWidth = false;
            this.lbl3.StylePriority.UseFont = false;
            this.lbl3.StylePriority.UseTextAlignment = false;
            this.lbl3.Text = "Cond. IVA:";
            this.lbl3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtTipoIva
            // 
            this.txtTipoIva.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtTipoIva.BorderWidth = 0;
            this.txtTipoIva.Dpi = 254F;
            this.txtTipoIva.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTipoIva.LocationFloat = new DevExpress.Utils.PointFloat(238.0552F, 678.0059F);
            this.txtTipoIva.Name = "txtTipoIva";
            this.txtTipoIva.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtTipoIva.SizeF = new System.Drawing.SizeF(1197.828F, 58.42004F);
            this.txtTipoIva.StylePriority.UseBorders = false;
            this.txtTipoIva.StylePriority.UseBorderWidth = false;
            this.txtTipoIva.StylePriority.UseFont = false;
            this.txtTipoIva.StylePriority.UseTextAlignment = false;
            this.txtTipoIva.Text = "N/A";
            this.txtTipoIva.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtCondiciondeVenta
            // 
            this.txtCondiciondeVenta.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtCondiciondeVenta.BorderWidth = 0;
            this.txtCondiciondeVenta.Dpi = 254F;
            this.txtCondiciondeVenta.Font = new System.Drawing.Font("Arial", 9F);
            this.txtCondiciondeVenta.LocationFloat = new DevExpress.Utils.PointFloat(337.1967F, 882.1585F);
            this.txtCondiciondeVenta.Name = "txtCondiciondeVenta";
            this.txtCondiciondeVenta.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtCondiciondeVenta.SizeF = new System.Drawing.SizeF(1600.232F, 58.42004F);
            this.txtCondiciondeVenta.StylePriority.UseBorders = false;
            this.txtCondiciondeVenta.StylePriority.UseBorderWidth = false;
            this.txtCondiciondeVenta.StylePriority.UseFont = false;
            this.txtCondiciondeVenta.StylePriority.UseTextAlignment = false;
            this.txtCondiciondeVenta.Text = "N/A";
            this.txtCondiciondeVenta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl5
            // 
            this.lbl5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl5.BorderWidth = 0;
            this.lbl5.Dpi = 254F;
            this.lbl5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl5.LocationFloat = new DevExpress.Utils.PointFloat(1435.884F, 678.0059F);
            this.lbl5.Name = "lbl5";
            this.lbl5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl5.SizeF = new System.Drawing.SizeF(224.3099F, 58.41998F);
            this.lbl5.StylePriority.UseBorders = false;
            this.lbl5.StylePriority.UseBorderWidth = false;
            this.lbl5.StylePriority.UseFont = false;
            this.lbl5.StylePriority.UseTextAlignment = false;
            this.lbl5.Text = "C.U.I.T./D.N.I:";
            this.lbl5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtNumCuit
            // 
            this.txtNumCuit.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtNumCuit.BorderWidth = 0;
            this.txtNumCuit.Dpi = 254F;
            this.txtNumCuit.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNumCuit.LocationFloat = new DevExpress.Utils.PointFloat(1660.194F, 678.0059F);
            this.txtNumCuit.Name = "txtNumCuit";
            this.txtNumCuit.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtNumCuit.SizeF = new System.Drawing.SizeF(277.2344F, 58.41998F);
            this.txtNumCuit.StylePriority.UseBorders = false;
            this.txtNumCuit.StylePriority.UseBorderWidth = false;
            this.txtNumCuit.StylePriority.UseFont = false;
            this.txtNumCuit.StylePriority.UseTextAlignment = false;
            this.txtNumCuit.Text = "99999999999";
            this.txtNumCuit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Condition = "[DataSource.RowCount]  <= 10";
            // 
            // 
            // 
            this.formattingRule1.Formatting.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.formattingRule1.Name = "formattingRule1";
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 76F;
            this.TopMargin.Name = "TopMargin";
            // 
            // rtbDatosEmpresa2
            // 
            this.rtbDatosEmpresa2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.rtbDatosEmpresa2.BorderWidth = 0;
            this.rtbDatosEmpresa2.Dpi = 254F;
            this.rtbDatosEmpresa2.Font = new System.Drawing.Font("Arial", 9F);
            this.rtbDatosEmpresa2.LocationFloat = new DevExpress.Utils.PointFloat(1112.326F, 293.264F);
            this.rtbDatosEmpresa2.Name = "rtbDatosEmpresa2";
            this.rtbDatosEmpresa2.SerializableRtfString = resources.GetString("rtbDatosEmpresa2.SerializableRtfString");
            this.rtbDatosEmpresa2.SizeF = new System.Drawing.SizeF(323.5568F, 233.8497F);
            this.rtbDatosEmpresa2.StylePriority.UseBorders = false;
            this.rtbDatosEmpresa2.StylePriority.UseBorderWidth = false;
            this.rtbDatosEmpresa2.StylePriority.UseFont = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 76F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // controlStyle1
            // 
            this.controlStyle1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.controlStyle1.Name = "controlStyle1";
            // 
            // txtNumCodigo
            // 
            this.txtNumCodigo.Dpi = 254F;
            this.txtNumCodigo.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNumCodigo.LocationFloat = new DevExpress.Utils.PointFloat(916.2706F, 160.0398F);
            this.txtNumCodigo.Name = "txtNumCodigo";
            this.txtNumCodigo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtNumCodigo.SizeF = new System.Drawing.SizeF(224.0837F, 49.56897F);
            this.txtNumCodigo.StylePriority.UseFont = false;
            this.txtNumCodigo.StylePriority.UseTextAlignment = false;
            this.txtNumCodigo.Text = "Código N° 01";
            this.txtNumCodigo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtLetraCbte
            // 
            this.txtLetraCbte.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txtLetraCbte.BorderWidth = 1;
            this.txtLetraCbte.Dpi = 254F;
            this.txtLetraCbte.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.txtLetraCbte.ForeColor = System.Drawing.Color.Black;
            this.txtLetraCbte.LocationFloat = new DevExpress.Utils.PointFloat(942.7291F, 5.291667F);
            this.txtLetraCbte.Name = "txtLetraCbte";
            this.txtLetraCbte.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtLetraCbte.SizeF = new System.Drawing.SizeF(173.8126F, 154.7481F);
            this.txtLetraCbte.StylePriority.UseBorders = false;
            this.txtLetraCbte.StylePriority.UseBorderWidth = false;
            this.txtLetraCbte.StylePriority.UseFont = false;
            this.txtLetraCbte.StylePriority.UseForeColor = false;
            this.txtLetraCbte.StylePriority.UseTextAlignment = false;
            this.txtLetraCbte.Text = "A";
            this.txtLetraCbte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtTipoCbte
            // 
            this.txtTipoCbte.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtTipoCbte.BorderWidth = 0;
            this.txtTipoCbte.Dpi = 254F;
            this.txtTipoCbte.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.txtTipoCbte.LocationFloat = new DevExpress.Utils.PointFloat(384.9361F, 79.375F);
            this.txtTipoCbte.Name = "txtTipoCbte";
            this.txtTipoCbte.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtTipoCbte.SizeF = new System.Drawing.SizeF(449.7915F, 57.1499F);
            this.txtTipoCbte.StylePriority.UseBorders = false;
            this.txtTipoCbte.StylePriority.UseBorderWidth = false;
            this.txtTipoCbte.StylePriority.UseFont = false;
            this.txtTipoCbte.StylePriority.UseTextAlignment = false;
            this.txtTipoCbte.Text = "ORIGINAL";
            this.txtTipoCbte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rtbDatosEmpresa3
            // 
            this.rtbDatosEmpresa3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.rtbDatosEmpresa3.Dpi = 254F;
            this.rtbDatosEmpresa3.Font = new System.Drawing.Font("Arial", 9F);
            this.rtbDatosEmpresa3.LocationFloat = new DevExpress.Utils.PointFloat(1435.883F, 293.2643F);
            this.rtbDatosEmpresa3.Name = "rtbDatosEmpresa3";
            this.rtbDatosEmpresa3.SerializableRtfString = resources.GetString("rtbDatosEmpresa3.SerializableRtfString");
            this.rtbDatosEmpresa3.SizeF = new System.Drawing.SizeF(320.1437F, 233.8495F);
            this.rtbDatosEmpresa3.StylePriority.UseBorders = false;
            this.rtbDatosEmpresa3.StylePriority.UseFont = false;
            // 
            // lbl18
            // 
            this.lbl18.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl18.Dpi = 254F;
            this.lbl18.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lbl18.LocationFloat = new DevExpress.Utils.PointFloat(1234.833F, 118.0041F);
            this.lbl18.Name = "lbl18";
            this.lbl18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl18.SizeF = new System.Drawing.SizeF(201.05F, 58.42006F);
            this.lbl18.StylePriority.UseBorders = false;
            this.lbl18.StylePriority.UseFont = false;
            this.lbl18.StylePriority.UseTextAlignment = false;
            this.lbl18.Text = "Número:";
            this.lbl18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtTipo
            // 
            this.txtTipo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtTipo.BorderWidth = 0;
            this.txtTipo.Dpi = 254F;
            this.txtTipo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.txtTipo.LocationFloat = new DevExpress.Utils.PointFloat(1234.833F, 63.5001F);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtTipo.SizeF = new System.Drawing.SizeF(704.5552F, 54.50399F);
            this.txtTipo.StylePriority.UseBorders = false;
            this.txtTipo.StylePriority.UseBorderWidth = false;
            this.txtTipo.StylePriority.UseFont = false;
            this.txtTipo.Text = "Factura";
            // 
            // txtNroCbte
            // 
            this.txtNroCbte.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtNroCbte.BorderWidth = 0;
            this.txtNroCbte.Dpi = 254F;
            this.txtNroCbte.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.txtNroCbte.LocationFloat = new DevExpress.Utils.PointFloat(1435.883F, 118.0042F);
            this.txtNroCbte.Name = "txtNroCbte";
            this.txtNroCbte.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtNroCbte.SizeF = new System.Drawing.SizeF(503.5051F, 58.42001F);
            this.txtNroCbte.StylePriority.UseBorders = false;
            this.txtNroCbte.StylePriority.UseBorderWidth = false;
            this.txtNroCbte.StylePriority.UseFont = false;
            this.txtNroCbte.StylePriority.UseTextAlignment = false;
            this.txtNroCbte.Text = "0000 - 00000000";
            this.txtNroCbte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // picLogoCabecera
            // 
            this.picLogoCabecera.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.picLogoCabecera.Dpi = 254F;
            this.picLogoCabecera.Image = ((System.Drawing.Image)(resources.GetObject("picLogoCabecera.Image")));
            this.picLogoCabecera.LocationFloat = new DevExpress.Utils.PointFloat(24.44045F, 25.00001F);
            this.picLogoCabecera.Name = "picLogoCabecera";
            this.picLogoCabecera.SizeF = new System.Drawing.SizeF(288F, 268.2643F);
            this.picLogoCabecera.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.picLogoCabecera.StylePriority.UseBorders = false;
            // 
            // rtbDatosEmpresa
            // 
            this.rtbDatosEmpresa.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.rtbDatosEmpresa.BorderWidth = 0;
            this.rtbDatosEmpresa.Dpi = 254F;
            this.rtbDatosEmpresa.Font = new System.Drawing.Font("Arial", 9F);
            this.rtbDatosEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(24.44045F, 293.2643F);
            this.rtbDatosEmpresa.Name = "rtbDatosEmpresa";
            this.rtbDatosEmpresa.SerializableRtfString = resources.GetString("rtbDatosEmpresa.SerializableRtfString");
            this.rtbDatosEmpresa.SizeF = new System.Drawing.SizeF(963.0895F, 233.8495F);
            this.rtbDatosEmpresa.StylePriority.UseBorders = false;
            this.rtbDatosEmpresa.StylePriority.UseBorderWidth = false;
            this.rtbDatosEmpresa.StylePriority.UseFont = false;
            // 
            // Detail
            // 
            this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.CbteDetails});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 58F;
            this.Detail.KeepTogetherWithDetailReports = true;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            this.Detail.StylePriority.UseBorders = false;
            // 
            // CbteDetails
            // 
            this.CbteDetails.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.CbteDetails.Dpi = 254F;
            this.CbteDetails.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.CbteDetails.LocationFloat = new DevExpress.Utils.PointFloat(24.99993F, 0F);
            this.CbteDetails.Name = "CbteDetails";
            this.CbteDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.CbteDetails.SizeF = new System.Drawing.SizeF(1899F, 57.99999F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellDetalle,
            this.xrTableCellTotal});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCellDetalle
            // 
            this.xrTableCellDetalle.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCellDetalle.CanGrow = false;
            this.xrTableCellDetalle.Dpi = 254F;
            this.xrTableCellDetalle.Font = new System.Drawing.Font("Arial", 9F);
            this.xrTableCellDetalle.Name = "xrTableCellDetalle";
            this.xrTableCellDetalle.Padding = new DevExpress.XtraPrinting.PaddingInfo(20, 0, 0, 0, 254F);
            this.xrTableCellDetalle.StylePriority.UseBorders = false;
            this.xrTableCellDetalle.StylePriority.UseFont = false;
            this.xrTableCellDetalle.StylePriority.UsePadding = false;
            this.xrTableCellDetalle.StylePriority.UseTextAlignment = false;
            this.xrTableCellDetalle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCellDetalle.Weight = 1.8727769701826953D;
            this.xrTableCellDetalle.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCellDetalle_BeforePrint);
            // 
            // xrTableCellTotal
            // 
            this.xrTableCellTotal.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCellTotal.CanGrow = false;
            this.xrTableCellTotal.Dpi = 254F;
            this.xrTableCellTotal.Font = new System.Drawing.Font("Arial", 9F);
            this.xrTableCellTotal.Name = "xrTableCellTotal";
            this.xrTableCellTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 20, 0, 0, 254F);
            this.xrTableCellTotal.StylePriority.UseBorders = false;
            this.xrTableCellTotal.StylePriority.UseFont = false;
            this.xrTableCellTotal.StylePriority.UsePadding = false;
            this.xrTableCellTotal.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:$0.00}";
            this.xrTableCellTotal.Summary = xrSummary1;
            this.xrTableCellTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCellTotal.Weight = 0.372420957732656D;
            this.xrTableCellTotal.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCellTotal_BeforePrint);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
            this.GroupHeader1.HeightF = 62.61805F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrPanel1
            // 
            this.xrPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.xrPanel1.Dpi = 254F;
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(7.9375F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(1929.492F, 62.61805F);
            this.xrPanel1.StylePriority.UseBackColor = false;
            this.xrPanel1.StylePriority.UseBorders = false;
            // 
            // xrTable2
            // 
            this.xrTable2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable2.Dpi = 254F;
            this.xrTable2.Font = new System.Drawing.Font("Arial", 8F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(19.02048F, 2.999978F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1897.042F, 52.91668F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellLabelDetalle,
            this.xrTableCellLabelTotal});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCellLabelDetalle
            // 
            this.xrTableCellLabelDetalle.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCellLabelDetalle.CanGrow = false;
            this.xrTableCellLabelDetalle.Dpi = 254F;
            this.xrTableCellLabelDetalle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelDetalle.Name = "xrTableCellLabelDetalle";
            this.xrTableCellLabelDetalle.StylePriority.UseBorders = false;
            this.xrTableCellLabelDetalle.StylePriority.UseFont = false;
            this.xrTableCellLabelDetalle.StylePriority.UseTextAlignment = false;
            this.xrTableCellLabelDetalle.Text = "Detalle";
            this.xrTableCellLabelDetalle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellLabelDetalle.Weight = 1.8270245785531971D;
            // 
            // xrTableCellLabelTotal
            // 
            this.xrTableCellLabelTotal.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCellLabelTotal.CanGrow = false;
            this.xrTableCellLabelTotal.Dpi = 254F;
            this.xrTableCellLabelTotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCellLabelTotal.Name = "xrTableCellLabelTotal";
            this.xrTableCellLabelTotal.StylePriority.UseBorders = false;
            this.xrTableCellLabelTotal.StylePriority.UseFont = false;
            this.xrTableCellLabelTotal.StylePriority.UseTextAlignment = false;
            this.xrTableCellLabelTotal.Text = "Importe";
            this.xrTableCellLabelTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCellLabelTotal.Weight = 0.36377217139191731D;
            // 
            // lbl16
            // 
            this.lbl16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl16.Dpi = 254F;
            this.lbl16.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.lbl16.LocationFloat = new DevExpress.Utils.PointFloat(24.44045F, 25F);
            this.lbl16.Multiline = true;
            this.lbl16.Name = "lbl16";
            this.lbl16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl16.SizeF = new System.Drawing.SizeF(1262.982F, 58.42005F);
            this.lbl16.StylePriority.UseBorders = false;
            this.lbl16.StylePriority.UseFont = false;
            this.lbl16.StylePriority.UseTextAlignment = false;
            this.lbl16.Text = "NO SE ACEPTAN DÉBITOS SIN LA DOCUMENTACIÓN RESPALDATORIA QUE ORIGINÓ LA FACTURACI" +
                "ÓN";
            this.lbl16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl10
            // 
            this.lbl10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl10.Dpi = 254F;
            this.lbl10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl10.LocationFloat = new DevExpress.Utils.PointFloat(1287.424F, 438.6439F);
            this.lbl10.Name = "lbl10";
            this.lbl10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl10.SizeF = new System.Drawing.SizeF(445.8513F, 58.42003F);
            this.lbl10.StylePriority.UseBorders = false;
            this.lbl10.StylePriority.UseFont = false;
            this.lbl10.StylePriority.UseTextAlignment = false;
            this.lbl10.Text = "TasaIIBB";
            this.lbl10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtImporteIIBBMonedaFacturacion
            // 
            this.txtImporteIIBBMonedaFacturacion.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtImporteIIBBMonedaFacturacion.Dpi = 254F;
            this.txtImporteIIBBMonedaFacturacion.Font = new System.Drawing.Font("Arial", 9F);
            this.txtImporteIIBBMonedaFacturacion.LocationFloat = new DevExpress.Utils.PointFloat(1733.275F, 438.6439F);
            this.txtImporteIIBBMonedaFacturacion.Name = "txtImporteIIBBMonedaFacturacion";
            this.txtImporteIIBBMonedaFacturacion.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtImporteIIBBMonedaFacturacion.SizeF = new System.Drawing.SizeF(204.1533F, 58.42004F);
            this.txtImporteIIBBMonedaFacturacion.StylePriority.UseBorders = false;
            this.txtImporteIIBBMonedaFacturacion.StylePriority.UseFont = false;
            this.txtImporteIIBBMonedaFacturacion.StylePriority.UseTextAlignment = false;
            this.txtImporteIIBBMonedaFacturacion.Text = "0,00";
            this.txtImporteIIBBMonedaFacturacion.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtMoneda
            // 
            this.txtMoneda.BorderColor = System.Drawing.SystemColors.ControlText;
            this.txtMoneda.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtMoneda.Dpi = 254F;
            this.txtMoneda.Font = new System.Drawing.Font("Arial", 9F);
            this.txtMoneda.LocationFloat = new DevExpress.Utils.PointFloat(108.7474F, 557.918F);
            this.txtMoneda.Name = "txtMoneda";
            this.txtMoneda.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtMoneda.SizeF = new System.Drawing.SizeF(457.6516F, 54.48273F);
            this.txtMoneda.StylePriority.UseBorderColor = false;
            this.txtMoneda.StylePriority.UseBorders = false;
            this.txtMoneda.StylePriority.UseFont = false;
            this.txtMoneda.StylePriority.UseTextAlignment = false;
            this.txtMoneda.Text = "N/A";
            this.txtMoneda.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtTotal
            // 
            this.txtTotal.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtTotal.Dpi = 254F;
            this.txtTotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtTotal.LocationFloat = new DevExpress.Utils.PointFloat(1735.234F, 557.9185F);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtTotal.SizeF = new System.Drawing.SizeF(202.1931F, 54.48257F);
            this.txtTotal.StylePriority.UseBorders = false;
            this.txtTotal.StylePriority.UseFont = false;
            this.txtTotal.StylePriority.UseTextAlignment = false;
            this.txtTotal.Text = "0,00";
            this.txtTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtSon
            // 
            this.txtSon.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtSon.Dpi = 254F;
            this.txtSon.Font = new System.Drawing.Font("Arial", 9F);
            this.txtSon.LocationFloat = new DevExpress.Utils.PointFloat(566.3989F, 557.918F);
            this.txtSon.Multiline = true;
            this.txtSon.Name = "txtSon";
            this.txtSon.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtSon.SizeF = new System.Drawing.SizeF(1020.086F, 54.48267F);
            this.txtSon.StylePriority.UseBorders = false;
            this.txtSon.StylePriority.UseFont = false;
            this.txtSon.StylePriority.UseTextAlignment = false;
            this.txtSon.Text = "N/A";
            this.txtSon.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtRefInterna
            // 
            this.txtRefInterna.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtRefInterna.Dpi = 254F;
            this.txtRefInterna.Font = new System.Drawing.Font("Arial", 8F);
            this.txtRefInterna.LocationFloat = new DevExpress.Utils.PointFloat(24.99993F, 436.9038F);
            this.txtRefInterna.Multiline = true;
            this.txtRefInterna.Name = "txtRefInterna";
            this.txtRefInterna.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtRefInterna.SizeF = new System.Drawing.SizeF(1261.023F, 60.16006F);
            this.txtRefInterna.StylePriority.UseBorders = false;
            this.txtRefInterna.StylePriority.UseFont = false;
            this.txtRefInterna.StylePriority.UseTextAlignment = false;
            this.txtRefInterna.Text = "N/A";
            this.txtRefInterna.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.txtRefInterna.Visible = false;
            // 
            // lbl15
            // 
            this.lbl15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl15.Dpi = 254F;
            this.lbl15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl15.LocationFloat = new DevExpress.Utils.PointFloat(31.26424F, 557.918F);
            this.lbl15.Multiline = true;
            this.lbl15.Name = "lbl15";
            this.lbl15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl15.SizeF = new System.Drawing.SizeF(77.48311F, 54.48279F);
            this.lbl15.StylePriority.UseBorders = false;
            this.lbl15.StylePriority.UseFont = false;
            this.lbl15.StylePriority.UseTextAlignment = false;
            this.lbl15.Text = "Son";
            this.lbl15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl11
            // 
            this.lbl11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl11.Dpi = 254F;
            this.lbl11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lbl11.LocationFloat = new DevExpress.Utils.PointFloat(1586.485F, 557.9185F);
            this.lbl11.Name = "lbl11";
            this.lbl11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl11.SizeF = new System.Drawing.SizeF(146.7897F, 54.4826F);
            this.lbl11.StylePriority.UseBorders = false;
            this.lbl11.StylePriority.UseFont = false;
            this.lbl11.StylePriority.UseTextAlignment = false;
            this.lbl11.Text = "Total:";
            this.lbl11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableImportes,
            this.xrLine5,
            this.xrLine1,
            this.lbl16,
            this.lbl10,
            this.txtImporteIIBBMonedaFacturacion,
            this.txtMoneda,
            this.txtTotal,
            this.txtSon,
            this.lbl15,
            this.lbl11,
            this.txtRefInterna});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 624F;
            this.GroupFooter1.KeepTogether = true;
            this.GroupFooter1.Level = 1;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.PrintAtBottom = true;
            // 
            // xrTableImportes
            // 
            this.xrTableImportes.Dpi = 254F;
            this.xrTableImportes.LocationFloat = new DevExpress.Utils.PointFloat(1291.219F, 24.99998F);
            this.xrTableImportes.Name = "xrTableImportes";
            this.xrTableImportes.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTableImportes.SizeF = new System.Drawing.SizeF(648.6548F, 58.42007F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow3.Dpi = 254F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.11925408071809107D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell1.Weight = 1.3296762795873804D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 0.61289330588297275D;
            // 
            // xrLine5
            // 
            this.xrLine5.Dpi = 254F;
            this.xrLine5.LineWidth = 3;
            this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(8.995494F, 508.7056F);
            this.xrLine5.Name = "xrLine5";
            this.xrLine5.SizeF = new System.Drawing.SizeF(1930.392F, 23.81244F);
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(7.9375F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(1929.491F, 23.8125F);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbl20,
            this.txtUsuario,
            this.xrLine4,
            this.xrLine3,
            this.xrLine2,
            this.txtDAGRUF,
            this.lbl17,
            this.txCPer,
            this.lbl19,
            this.txtFechaCbte,
            this.txtLetraCbte,
            this.txtNumCodigo,
            this.rtbDatosEmpresa,
            this.picLogoCabecera,
            this.txtTipoCbte,
            this.txtTipo,
            this.txtNroCbte,
            this.lbl18,
            this.rtbDatosEmpresa2,
            this.rtbDatosEmpresa3,
            this.txtNombreEmpresa,
            this.txtNumCodIIBB,
            this.txtNumIIBB,
            this.txtLugarDeEntrega,
            this.txtDomicilio,
            this.lbl7,
            this.lbl2,
            this.lbl1,
            this.lbl3,
            this.lbl4,
            this.txtTipoIva,
            this.txtCondiciondeVenta,
            this.lbl5,
            this.txtNumCuit,
            this.lbl6});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 998.9986F;
            this.PageHeader.Name = "PageHeader";
            // 
            // lbl20
            // 
            this.lbl20.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl20.Dpi = 254F;
            this.lbl20.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lbl20.LocationFloat = new DevExpress.Utils.PointFloat(1234.833F, 234.8441F);
            this.lbl20.Name = "lbl20";
            this.lbl20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl20.SizeF = new System.Drawing.SizeF(201.05F, 58.42006F);
            this.lbl20.StylePriority.UseBorders = false;
            this.lbl20.StylePriority.UseFont = false;
            this.lbl20.StylePriority.UseTextAlignment = false;
            this.lbl20.Text = "Usuario:";
            this.lbl20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbl20.Visible = false;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtUsuario.BorderWidth = 0;
            this.txtUsuario.Dpi = 254F;
            this.txtUsuario.Font = new System.Drawing.Font("Arial", 10F);
            this.txtUsuario.LocationFloat = new DevExpress.Utils.PointFloat(1435.884F, 619.5861F);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtUsuario.SizeF = new System.Drawing.SizeF(501.5441F, 58.41998F);
            this.txtUsuario.StylePriority.UseBorders = false;
            this.txtUsuario.StylePriority.UseBorderWidth = false;
            this.txtUsuario.StylePriority.UseFont = false;
            this.txtUsuario.StylePriority.UseTextAlignment = false;
            this.txtUsuario.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLine4
            // 
            this.xrLine4.Dpi = 254F;
            this.xrLine4.LineWidth = 3;
            this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(7.937177F, 858.346F);
            this.xrLine4.Name = "xrLine4";
            this.xrLine4.SizeF = new System.Drawing.SizeF(1931.937F, 23.8125F);
            // 
            // xrLine3
            // 
            this.xrLine3.Dpi = 254F;
            this.xrLine3.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine3.LineWidth = 3;
            this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(1014.416F, 209.6087F);
            this.xrLine3.Name = "xrLine3";
            this.xrLine3.SizeF = new System.Drawing.SizeF(31.75F, 327.7449F);
            // 
            // xrLine2
            // 
            this.xrLine2.Dpi = 254F;
            this.xrLine2.LineWidth = 3;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(7.937177F, 537.3536F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(1931.937F, 23.8125F);
            // 
            // txtDAGRUF
            // 
            this.txtDAGRUF.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtDAGRUF.BorderWidth = 0;
            this.txtDAGRUF.Dpi = 254F;
            this.txtDAGRUF.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDAGRUF.LocationFloat = new DevExpress.Utils.PointFloat(1609.004F, 486.871F);
            this.txtDAGRUF.Name = "txtDAGRUF";
            this.txtDAGRUF.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtDAGRUF.SizeF = new System.Drawing.SizeF(328.4242F, 50.48248F);
            this.txtDAGRUF.StylePriority.UseBorders = false;
            this.txtDAGRUF.StylePriority.UseBorderWidth = false;
            this.txtDAGRUF.StylePriority.UseFont = false;
            this.txtDAGRUF.StylePriority.UseTextAlignment = false;
            this.txtDAGRUF.Text = "Cheques Rechazados";
            this.txtDAGRUF.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lbl17
            // 
            this.lbl17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl17.BorderWidth = 0;
            this.lbl17.Dpi = 254F;
            this.lbl17.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lbl17.LocationFloat = new DevExpress.Utils.PointFloat(1234.833F, 5.291667F);
            this.lbl17.Name = "lbl17";
            this.lbl17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl17.SizeF = new System.Drawing.SizeF(704.5552F, 58.20843F);
            this.lbl17.StylePriority.UseBorders = false;
            this.lbl17.StylePriority.UseBorderWidth = false;
            this.lbl17.StylePriority.UseFont = false;
            this.lbl17.StylePriority.UseTextAlignment = false;
            this.lbl17.Text = "COMPROBANTE ELECTRÓNICO";
            this.lbl17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txCPer
            // 
            this.txCPer.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txCPer.BorderWidth = 0;
            this.txCPer.Dpi = 254F;
            this.txCPer.Font = new System.Drawing.Font("Arial", 9F);
            this.txCPer.LocationFloat = new DevExpress.Utils.PointFloat(1435.883F, 561.1661F);
            this.txCPer.Name = "txCPer";
            this.txCPer.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txCPer.SizeF = new System.Drawing.SizeF(501.546F, 58.42004F);
            this.txCPer.StylePriority.UseBorders = false;
            this.txCPer.StylePriority.UseBorderWidth = false;
            this.txCPer.StylePriority.UseFont = false;
            this.txCPer.StylePriority.UseTextAlignment = false;
            this.txCPer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl19
            // 
            this.lbl19.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl19.Dpi = 254F;
            this.lbl19.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lbl19.LocationFloat = new DevExpress.Utils.PointFloat(1234.833F, 176.4242F);
            this.lbl19.Name = "lbl19";
            this.lbl19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl19.SizeF = new System.Drawing.SizeF(201.05F, 58.42006F);
            this.lbl19.StylePriority.UseBorders = false;
            this.lbl19.StylePriority.UseFont = false;
            this.lbl19.StylePriority.UseTextAlignment = false;
            this.lbl19.Text = "Fecha:";
            this.lbl19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtFechaCbte
            // 
            this.txtFechaCbte.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtFechaCbte.BorderWidth = 0;
            this.txtFechaCbte.Dpi = 254F;
            this.txtFechaCbte.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.txtFechaCbte.LocationFloat = new DevExpress.Utils.PointFloat(1435.883F, 176.4242F);
            this.txtFechaCbte.Name = "txtFechaCbte";
            this.txtFechaCbte.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtFechaCbte.SizeF = new System.Drawing.SizeF(503.5051F, 58.41998F);
            this.txtFechaCbte.StylePriority.UseBorders = false;
            this.txtFechaCbte.StylePriority.UseBorderWidth = false;
            this.txtFechaCbte.StylePriority.UseFont = false;
            this.txtFechaCbte.StylePriority.UseTextAlignment = false;
            this.txtFechaCbte.Text = "00/00/0000";
            this.txtFechaCbte.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine6});
            this.GroupFooter2.Dpi = 254F;
            this.GroupFooter2.HeightF = 23.8125F;
            this.GroupFooter2.Level = 2;
            this.GroupFooter2.Name = "GroupFooter2";
            this.GroupFooter2.PrintAtBottom = true;
            // 
            // xrLine6
            // 
            this.xrLine6.Dpi = 254F;
            this.xrLine6.LineWidth = 3;
            this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(7.937126F, 0F);
            this.xrLine6.Name = "xrLine6";
            this.xrLine6.SizeF = new System.Drawing.SizeF(1931.937F, 23.8125F);
            // 
            // xrCrossBandBox1
            // 
            this.xrCrossBandBox1.Dpi = 254F;
            this.xrCrossBandBox1.EndBand = this.PageFooter;
            this.xrCrossBandBox1.EndPointFloat = new DevExpress.Utils.PointFloat(2.645833F, 61.74978F);
            this.xrCrossBandBox1.LocationFloat = new DevExpress.Utils.PointFloat(2.645833F, 0F);
            this.xrCrossBandBox1.Name = "xrCrossBandBox1";
            this.xrCrossBandBox1.StartBand = this.PageHeader;
            this.xrCrossBandBox1.StartPointFloat = new DevExpress.Utils.PointFloat(2.645833F, 0F);
            this.xrCrossBandBox1.WidthF = 1942.52F;
            // 
            // GroupFooter3
            // 
            this.GroupFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtBarCodeCbte,
            this.xrLabel2,
            this.xrLabel3,
            this.txtCae,
            this.txtFechaVto});
            this.GroupFooter3.Dpi = 254F;
            this.GroupFooter3.HeightF = 116.8398F;
            this.GroupFooter3.Level = 3;
            this.GroupFooter3.Name = "GroupFooter3";
            this.GroupFooter3.PrintAtBottom = true;
            // 
            // GroupFooter4
            // 
            this.GroupFooter4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtCodeRapipago,
            this.xrLine7,
            this.picLogoRapipago,
            this.lblTituloRapipago,
            this.txtBarCodeRapipago,
            this.txtBarcodeRapipago2});
            this.GroupFooter4.Dpi = 254F;
            this.GroupFooter4.HeightF = 303F;
            this.GroupFooter4.Level = 4;
            this.GroupFooter4.Name = "GroupFooter4";
            this.GroupFooter4.PrintAtBottom = true;
            // 
            // txtCodeRapipago
            // 
            this.txtCodeRapipago.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtCodeRapipago.BorderWidth = 0;
            this.txtCodeRapipago.Dpi = 254F;
            this.txtCodeRapipago.Font = new System.Drawing.Font("Arial", 9F);
            this.txtCodeRapipago.LocationFloat = new DevExpress.Utils.PointFloat(297.5095F, 210.326F);
            this.txtCodeRapipago.Name = "txtCodeRapipago";
            this.txtCodeRapipago.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtCodeRapipago.SizeF = new System.Drawing.SizeF(1626.491F, 58.41998F);
            this.txtCodeRapipago.StylePriority.UseBorders = false;
            this.txtCodeRapipago.StylePriority.UseBorderWidth = false;
            this.txtCodeRapipago.StylePriority.UseFont = false;
            this.txtCodeRapipago.StylePriority.UseTextAlignment = false;
            this.txtCodeRapipago.Text = "893010006000000130004150011290000221080";
            this.txtCodeRapipago.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLine7
            // 
            this.xrLine7.Dpi = 254F;
            this.xrLine7.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.xrLine7.LineWidth = 3;
            this.xrLine7.LocationFloat = new DevExpress.Utils.PointFloat(7.937026F, 0F);
            this.xrLine7.Name = "xrLine7";
            this.xrLine7.SizeF = new System.Drawing.SizeF(1931.937F, 23.8125F);
            // 
            // picLogoRapipago
            // 
            this.picLogoRapipago.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.picLogoRapipago.Dpi = 254F;
            this.picLogoRapipago.Image = ((System.Drawing.Image)(resources.GetObject("picLogoRapipago.Image")));
            this.picLogoRapipago.LocationFloat = new DevExpress.Utils.PointFloat(24.44045F, 25F);
            this.picLogoRapipago.Name = "picLogoRapipago";
            this.picLogoRapipago.SizeF = new System.Drawing.SizeF(253.3022F, 278F);
            this.picLogoRapipago.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.picLogoRapipago.StylePriority.UseBorders = false;
            // 
            // txtBarcodeRapipago2
            // 
            this.txtBarcodeRapipago2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtBarcodeRapipago2.BorderWidth = 0;
            this.txtBarcodeRapipago2.Dpi = 254F;
            this.txtBarcodeRapipago2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.txtBarcodeRapipago2.LocationFloat = new DevExpress.Utils.PointFloat(297.5095F, 116.6284F);
            this.txtBarcodeRapipago2.Name = "txtBarcodeRapipago2";
            this.txtBarcodeRapipago2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtBarcodeRapipago2.SizeF = new System.Drawing.SizeF(1626.491F, 93.69776F);
            this.txtBarcodeRapipago2.StylePriority.UseBorders = false;
            this.txtBarcodeRapipago2.StylePriority.UseBorderWidth = false;
            this.txtBarcodeRapipago2.StylePriority.UseFont = false;
            this.txtBarcodeRapipago2.StylePriority.UseTextAlignment = false;
            this.txtBarcodeRapipago2.Text = "*893010006000000130004150011290000221080*";
            this.txtBarcodeRapipago2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupFooter5
            // 
            this.GroupFooter5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtObservaciones3});
            this.GroupFooter5.Dpi = 254F;
            this.GroupFooter5.HeightF = 58.42004F;
            this.GroupFooter5.Level = 5;
            this.GroupFooter5.Name = "GroupFooter5";
            this.GroupFooter5.PrintAtBottom = true;
            // 
            // txtObservaciones3
            // 
            this.txtObservaciones3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtObservaciones3.Dpi = 254F;
            this.txtObservaciones3.Font = new System.Drawing.Font("Arial", 9F);
            this.txtObservaciones3.LocationFloat = new DevExpress.Utils.PointFloat(24.44045F, 0F);
            this.txtObservaciones3.Name = "txtObservaciones3";
            this.txtObservaciones3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtObservaciones3.SizeF = new System.Drawing.SizeF(1914.947F, 58.42004F);
            this.txtObservaciones3.StylePriority.UseBorders = false;
            this.txtObservaciones3.StylePriority.UseFont = false;
            this.txtObservaciones3.StylePriority.UseTextAlignment = false;
            this.txtObservaciones3.Text = "Observaciones3 - ObservacionesPie";
            this.txtObservaciones3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupFooter0
            // 
            this.GroupFooter0.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtInformacionAdicional,
            this.lblTituloPercepcionIIBBDocsB,
            this.txtPercepcionIIBBDocsB});
            this.GroupFooter0.Dpi = 254F;
            this.GroupFooter0.HeightF = 169F;
            this.GroupFooter0.Name = "GroupFooter0";
            this.GroupFooter0.Visible = false;
            // 
            // txtInformacionAdicional
            // 
            this.txtInformacionAdicional.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtInformacionAdicional.BorderWidth = 0;
            this.txtInformacionAdicional.Dpi = 254F;
            this.txtInformacionAdicional.Font = new System.Drawing.Font("Arial", 9F);
            this.txtInformacionAdicional.LocationFloat = new DevExpress.Utils.PointFloat(26.958F, 27.16F);
            this.txtInformacionAdicional.Name = "txtInformacionAdicional";
            this.txtInformacionAdicional.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtInformacionAdicional.SizeF = new System.Drawing.SizeF(1897.042F, 58.41998F);
            this.txtInformacionAdicional.StylePriority.UseBorders = false;
            this.txtInformacionAdicional.StylePriority.UseBorderWidth = false;
            this.txtInformacionAdicional.StylePriority.UseFont = false;
            this.txtInformacionAdicional.StylePriority.UseTextAlignment = false;
            this.txtInformacionAdicional.Text = "Observaciones2 - InformacionAdic";
            this.txtInformacionAdicional.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblTituloPercepcionIIBBDocsB
            // 
            this.lblTituloPercepcionIIBBDocsB.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblTituloPercepcionIIBBDocsB.BorderWidth = 0;
            this.lblTituloPercepcionIIBBDocsB.Dpi = 254F;
            this.lblTituloPercepcionIIBBDocsB.Font = new System.Drawing.Font("Arial", 9F);
            this.lblTituloPercepcionIIBBDocsB.LocationFloat = new DevExpress.Utils.PointFloat(26.95798F, 85.58002F);
            this.lblTituloPercepcionIIBBDocsB.Name = "lblTituloPercepcionIIBBDocsB";
            this.lblTituloPercepcionIIBBDocsB.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTituloPercepcionIIBBDocsB.SizeF = new System.Drawing.SizeF(535.1347F, 58.41998F);
            this.lblTituloPercepcionIIBBDocsB.StylePriority.UseBorders = false;
            this.lblTituloPercepcionIIBBDocsB.StylePriority.UseBorderWidth = false;
            this.lblTituloPercepcionIIBBDocsB.StylePriority.UseFont = false;
            this.lblTituloPercepcionIIBBDocsB.StylePriority.UseTextAlignment = false;
            this.lblTituloPercepcionIIBBDocsB.Text = "TasaIIBB";
            this.lblTituloPercepcionIIBBDocsB.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txtPercepcionIIBBDocsB
            // 
            this.txtPercepcionIIBBDocsB.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txtPercepcionIIBBDocsB.BorderWidth = 0;
            this.txtPercepcionIIBBDocsB.Dpi = 254F;
            this.txtPercepcionIIBBDocsB.Font = new System.Drawing.Font("Arial", 9F);
            this.txtPercepcionIIBBDocsB.LocationFloat = new DevExpress.Utils.PointFloat(1609.004F, 85.58002F);
            this.txtPercepcionIIBBDocsB.Name = "txtPercepcionIIBBDocsB";
            this.txtPercepcionIIBBDocsB.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.txtPercepcionIIBBDocsB.SizeF = new System.Drawing.SizeF(314.9948F, 58.41998F);
            this.txtPercepcionIIBBDocsB.StylePriority.UseBorders = false;
            this.txtPercepcionIIBBDocsB.StylePriority.UseBorderWidth = false;
            this.txtPercepcionIIBBDocsB.StylePriority.UseFont = false;
            this.txtPercepcionIIBBDocsB.StylePriority.UseTextAlignment = false;
            this.txtPercepcionIIBBDocsB.Text = "0,00";
            this.txtPercepcionIIBBDocsB.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // ReporteBase
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.Detail,
            this.BottomMargin,
            this.PageFooter,
            this.GroupHeader1,
            this.GroupFooter1,
            this.PageHeader,
            this.GroupFooter2,
            this.GroupFooter3,
            this.GroupFooter4,
            this.GroupFooter5,
            this.GroupFooter0});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandBox1});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(76, 76, 76, 76);
            this.PageHeight = 2969;
            this.PageWidth = 2101;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.controlStyle1});
            this.Version = "10.1";
            ((System.ComponentModel.ISupportInitialize)(this.rtbDatosEmpresa2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbDatosEmpresa3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbDatosEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CbteDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableImportes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        public DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.XRLabel txtFechaVto;
        public DevExpress.XtraReports.UI.XRLabel txtCae;
        public DevExpress.XtraReports.UI.XRLabel xrLabel3;
        public DevExpress.XtraReports.UI.XRLabel xrLabel2;
        public DevExpress.XtraReports.UI.XRBarCode txtBarCodeCbte;
        public DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        public DevExpress.XtraReports.UI.XRLabel lbl4;
        public DevExpress.XtraReports.UI.XRLabel txtDomicilio;
        public DevExpress.XtraReports.UI.XRLabel txtNombreEmpresa;
        public DevExpress.XtraReports.UI.XRLabel lbl2;
        public DevExpress.XtraReports.UI.XRLabel lbl6;
        public DevExpress.XtraReports.UI.XRLabel lbl1;
        public DevExpress.XtraReports.UI.XRLabel lbl3;
        public DevExpress.XtraReports.UI.XRLabel txtTipoIva;
        public DevExpress.XtraReports.UI.XRLabel txtCondiciondeVenta;
        public DevExpress.XtraReports.UI.XRLabel lbl5;
        public DevExpress.XtraReports.UI.XRLabel txtNumCuit;
        public DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        public DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        public DevExpress.XtraReports.UI.XRRichText rtbDatosEmpresa2;
        public DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        public DevExpress.XtraReports.UI.XRLabel txtNumCodigo;
        public DevExpress.XtraReports.UI.XRLabel txtLetraCbte;
        public DevExpress.XtraReports.UI.XRLabel lbl18;
        public DevExpress.XtraReports.UI.XRLabel txtTipo;
        public DevExpress.XtraReports.UI.XRLabel txtNroCbte;
        public DevExpress.XtraReports.UI.XRRichText rtbDatosEmpresa;
        public DevExpress.XtraReports.UI.DetailBand Detail;
        public DevExpress.XtraReports.UI.XRRichText rtbDatosEmpresa3;
        public DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        public DevExpress.XtraReports.UI.XRLabel txtMoneda;
        public DevExpress.XtraReports.UI.XRLabel txtSon;
        public DevExpress.XtraReports.UI.XRLabel txtRefInterna;
        public DevExpress.XtraReports.UI.XRLabel lbl15;
        public DevExpress.XtraReports.UI.XRTable CbteDetails;
        public DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCellDetalle;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCellTotal;
        public DevExpress.XtraReports.UI.XRLabel txtTipoCbte;
        public DevExpress.XtraReports.UI.XRLabel lbl10;
        public DevExpress.XtraReports.UI.XRLabel txtImporteIIBBMonedaFacturacion;
        public DevExpress.XtraReports.UI.XRLabel txtTotal;
        public DevExpress.XtraReports.UI.XRLabel lbl11;
        public DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        public DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        public DevExpress.XtraReports.UI.XRPictureBox picLogoCabecera;
        public DevExpress.XtraReports.UI.XRTable xrTable2;
        public DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCellLabelDetalle;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCellLabelTotal;
        public DevExpress.XtraReports.UI.XRLabel txtLugarDeEntrega;
        public DevExpress.XtraReports.UI.XRLabel lbl16;
        public DevExpress.XtraReports.UI.XRLabel lbl19;
        public DevExpress.XtraReports.UI.XRLabel txtFechaCbte;
        public DevExpress.XtraReports.UI.XRLabel txCPer;
        public DevExpress.XtraReports.UI.XRLabel lblTituloRapipago;
        public DevExpress.XtraReports.UI.XRBarCode txtBarCodeRapipago;
        public DevExpress.XtraReports.UI.XRLabel txtNumIIBB;
        public DevExpress.XtraReports.UI.XRLabel lbl17;
        public DevExpress.XtraReports.UI.XRLabel lbl7;
        public DevExpress.XtraReports.UI.XRLabel txtNumCodIIBB;
        public DevExpress.XtraReports.UI.XRLabel txtDAGRUF;
        private DevExpress.XtraReports.UI.XRControlStyle controlStyle1;
        private DevExpress.XtraReports.UI.XRCrossBandBox xrCrossBandBox1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        public DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
        public DevExpress.XtraReports.UI.GroupFooterBand GroupFooter3;
        public DevExpress.XtraReports.UI.GroupFooterBand GroupFooter4;
        private DevExpress.XtraReports.UI.XRLine xrLine3;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        public DevExpress.XtraReports.UI.XRLabel txtObservaciones3;
        private DevExpress.XtraReports.UI.XRLine xrLine5;
        private DevExpress.XtraReports.UI.XRLine xrLine4;
        private DevExpress.XtraReports.UI.XRLine xrLine6;
        private DevExpress.XtraReports.UI.XRLine xrLine7;
        public DevExpress.XtraReports.UI.XRPictureBox picLogoRapipago;
        public DevExpress.XtraReports.UI.XRLabel lbl20;
        public DevExpress.XtraReports.UI.XRLabel txtUsuario;
        public DevExpress.XtraReports.UI.XRLabel txtCodeRapipago;
        public DevExpress.XtraReports.UI.GroupFooterBand GroupFooter5;
        public DevExpress.XtraReports.UI.GroupFooterBand GroupFooter0;
        public DevExpress.XtraReports.UI.XRLabel txtPercepcionIIBBDocsB;
        public DevExpress.XtraReports.UI.XRLabel lblTituloPercepcionIIBBDocsB;
        public DevExpress.XtraReports.UI.XRLine xrLine1;
        public DevExpress.XtraReports.UI.XRLabel txtBarcodeRapipago2;
        public DevExpress.XtraReports.UI.XRLabel txtInformacionAdicional;
        public DevExpress.XtraReports.UI.XRTable xrTableImportes;
        public DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell2;


    }
}
