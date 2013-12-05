<%@ Page Language="C#" EnableEventValidation="false"MasterPageFile="~/CEMasterPage.master" AutoEventWireup="true" EnableViewState="true" CodeFile="ActivityReg.aspx.cs" Inherits="FacturaElectronica.activityreg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="App_Themes/datepicker.css" type="text/css" />
<script type="text/javascript" src="js/date.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery.datePicker.js"></script>
<script language="javascript" type="text/javascript"  charset="utf-8">
    var w = screen.availWidth;
    var v = screen.availHeight - 50;

    var popW = 850;
    var popH = v;

    var leftPos = (w - popW) / 2;
    var topPos = 0;

    onresize = function () {
        resizeGridPanel();
    }

    function onLoad() {
        calendarPicker(document.getElementById("ctl00_ContentPlaceHolder1_ddlSearchBy"), "true");
        resizeGridPanel();
    }

    function resizeGridPanel() {
        // var panelH = document.documentElement.clientHeight - 185;
        document.getElementById("ctl00_ContentPlaceHolder1_pnlGridView").style.height = "100%";
    }

    function calendarPickerOn() {
        $(function () {
            $('.date-pick')
        .datePicker({ createButton: false })
        .bind('click',
            function () {
                $(this).dpDisplay();
                this.blur();
                return false;
            }
        );
        });
    }

    function calendarPickerOff() {
        $(function () {
            $('.date-pick')
        .datePicker({ createButton: false })
        .unbind('click');
        });
    }

    function calendarPicker(objSel, RunOnload) {
        if (RunOnload == "false") {
            document.getElementById("ctl00_ContentPlaceHolder1_txtSearchText").value = "";
        }

        if (objSel.value == "FechaComprobante") {
            calendarPickerOn();
            document.getElementById("ctl00_ContentPlaceHolder1_txtSearchText").readOnly = "readonly";
        }
        else {
            calendarPickerOff();

            if (objSel.value == "Mostrar Todo") {
                document.getElementById("ctl00_ContentPlaceHolder1_txtSearchText").readOnly = "readonly";
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_txtSearchText").readOnly = false;
            }
        }
    }

    function SelectAll(chkBox) {
        var iNumber = "0";

        for (var i = 2; i < 300; i++) {
            if (i < 10) {
                iNumber = "0" + new String(i);
            }
            else {
                iNumber = new String(i);
            }

            var elt = document.getElementById("ctl00_ContentPlaceHolder1_GridView1_ctl" + iNumber + "_chkSel");

            if (elt != null) {
                if (elt.disabled == false) {
                    elt.checked = document.getElementById("ctl00_ContentPlaceHolder1_GridView1_ctl01_chkSelHeader").checked;
                }
            }
        }
    }

    function getScrollBottom(p_oElem) {
        var panH = (typeof window.innerHeight != 'undefined' ? window.innerHeight : document.body.offsetHeight);

        return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
    }
</script>
<script type="text/javascript" language="JavaScript">

    var cX = 0; var cY = 0; var rX = 0; var rY = 0;
    function UpdateCursorPosition(e) { cX = e.pageX; cY = e.pageY; }
    function UpdateCursorPositionDocAll(e) { cX = event.clientX; cY = event.clientY; }
    if (document.all) { document.onmousemove = UpdateCursorPositionDocAll; }
    else { document.onmousemove = UpdateCursorPosition; }
    function AssignPosition(d) {
        if (self.pageYOffset) {
            rX = self.pageXOffset;
            rY = self.pageYOffset;
        }
        else if (document.documentElement && document.documentElement.scrollTop) {
            rX = document.documentElement.scrollLeft;
            rY = document.documentElement.scrollTop;
        }
        else if (document.body) {
            rX = document.body.scrollLeft;
            rY = document.body.scrollTop;
        }
        if (document.all) {
            cX += rX;
            cY += rY;
        }
        d.style.left = (cX + 10) + "px";
        d.style.top = (cY + 10) + "px";
    }
    function HideContent(d) {
        if (d.length < 1) { return; }
        document.getElementById(d).style.display = "none";
    }
    function ShowContent(d) {
        if (d.length < 1) { return; }
        var dd = document.getElementById(d);
        AssignPosition(dd);
        dd.style.display = "block";
    }
    function ReverseContentDisplay(d) {
        if (d.length < 1) { return; }
        var dd = document.getElementById(d);
        AssignPosition(dd);
        if (dd.style.display == "none") { dd.style.display = "block"; }
        else { dd.style.display = "none"; }
    }
</script>

<style type="text/css">
.hiddencol
{
    display:none;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;text-align:right;background-color:#97A8B2;background:url(Images/backgrey.gif);height:30px">
        <tr>
            <td><asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White" Font-Names="Tahoma" Text="Comprobantes Procesados"></asp:Label></td>
        </tr>
    </table>
    <table style="width: 100%;text-align:right;background-color:#2C353C;border-top: 2px solid white;">
        <tr>
            <td align="left" valign="middle" style="font-weight:bold; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif;">&nbsp;Buscar por: 
                <asp:DropDownList ID="ddlSearchBy" runat="server" Height="20px" >
                    <asp:ListItem Selected="True">Mostrar Todo</asp:ListItem>
                    <asp:ListItem Value="CompradorCodigoCliente">Cliente</asp:ListItem>
                    <asp:ListItem Value="EstadoTransaccion">Estado</asp:ListItem>
                    <asp:ListItem Value="FechaComprobante">Fecha</asp:ListItem>
                    <asp:ListItem Value="NroLegal">Número de FC/NC/ND</asp:ListItem> 
                    <asp:ListItem Value="PuntoVenta">Sucursal</asp:ListItem>                
                    <asp:ListItem Value="Descripcion">Tipo</asp:ListItem>
                </asp:DropDownList>&nbsp;<asp:TextBox CssClass="date-pick" ID="txtSearchText" runat="server" Height="16px"></asp:TextBox>

                    &nbsp;<asp:ImageButton 
                    ID="btnSearch" runat="server"  Width="23px" Height="23px" ImageUrl="~/Images/search.gif" ToolTip="Buscar" 
                    ImageAlign="AbsMiddle" onclick="btnSearch_Click" />  
                    
                    &nbsp;&nbsp;<asp:ImageButton ID="btnDownloadSel" runat="server"  Width="23px" Height="23px" Visible="false"
                    ImageUrl="~/Images/down.jpg" ToolTip="Descargar Items Seleccionados" 
                    ImageAlign="AbsMiddle" onclick="btnDownloadSel_Click" OnClientClick=""/>   
                     
                    &nbsp;&nbsp;<asp:ImageButton ID="btnMail" runat="server"  Width="23px" Height="23px"
                    ImageUrl="~/Images/mail.gif" ToolTip="Enviar por Email" 
                    ImageAlign="AbsMiddle" OnClientClick="" onclick="btnMail_Click"/>   
                    
                    &nbsp;&nbsp;<a href="javascript:ReverseContentDisplay('uniquename1');"><img width="23px" height="23px"
                    src="Images/dprint.gif" alt="Descargar para Impresión" style="border:0px;vertical-align:middle;" /></a>
                    
                    <div id="uniquename1" style="display:none;position:absolute;font-size:smaller;font-weight:lighter;border-style: solid;background-color: white;color:Black;padding:5px;width:100px;">
                        <asp:CheckBox runat="server" ID="chkOriginal" Text="Original" /><br />
                        <asp:CheckBox runat="server" ID="chkDuplicado" Text="Duplicado" /><br />
                        <asp:CheckBox runat="server" ID="chkTriplicado" Text="Triplicado" /><br />
                        <asp:CheckBox runat="server" ID="chkCopia" Text="Copia" /><br /><br />
                        <div><asp:Button runat="server" ID="btnPrint" OnClientClick="ReverseContentDisplay('uniquename1');" Text="Enviar" 
                            onclick="btnPrint_Click" /></div>
                    </div>
               
            </td>
            <td>
                <asp:ImageButton ID="ImgReload" runat="server" ImageUrl="~/Images/refresh.gif" 
                    Width="23px" Height="23px" ImageAlign="Middle" ToolTip="Recargar listado" 
                    onclick="ImgReload_Click"/>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="background-color:#97A8B2;text-align:left;color:#FFFFFF;font-size:small;font-weight:bold;"><asp:Label ID="lblConditionInfo" runat="server" ForeColor="Black" Font-Names="Tahoma" Visible="false"></asp:Label></td>
        </tr>
    </table>
    <asp:Panel id="pnlGridView" runat="server" 
        HorizontalAlign="Center" ScrollBars="Vertical" BorderWidth="0">
        
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CaptionAlign="Top" 
        CellPadding="3" DataSourceID="SqlDataSource1" ForeColor="#333333" 
        Width="98%" ShowFooter="True" onrowdatabound="GridView1_RowDataBound" EnableViewState="true" >
        <PagerSettings FirstPageImageUrl="~/Images/left.gif" 
            LastPageImageUrl="~/Images/right.gif" Mode="NumericFirstLast" 
            NextPageImageUrl="~/Images/next.gif" PreviousPageImageUrl="~/Images/prev.gif" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Small" Height="20px" />
        <EmptyDataRowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Small" Height="20px" />
        <EmptyDataTemplate >
            <b>No se encontraron resultados.</b>
        </EmptyDataTemplate>
        <Columns>                
            <asp:TemplateField HeaderText="#"> 
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelHeader" runat="server"></asp:CheckBox>
                </HeaderTemplate>               
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSel" runat="server" EnableViewState="true" ></asp:CheckBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" />                      
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
                <HeaderStyle HorizontalAlign="Center" CssClass="hiddencol" />
                <ControlStyle BorderColor="#97A8B2" BorderStyle="Solid" BorderWidth="1px" />
                <ItemStyle HorizontalAlign="Right" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" CssClass="hiddencol" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaComprobante" HeaderText="Fecha" SortExpression="FechaComprobante" 
                DataFormatString="{0:dd/MM/yyyy}" >
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="CompradorCodigoCliente" HeaderText="Cliente" 
                SortExpression="CompradorCodigoCliente" >
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="Descripcion" HeaderText="Tipo" SortExpression="Descripcion" >
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" Width="105px" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="PuntoVenta" HeaderText="Sucursal" 
                SortExpression="PuntoVenta" >
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="NroLegal" HeaderText="Número de FC/NC/ND" ReadOnly="True" 
                SortExpression="NroLegal">
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px"/>
            </asp:BoundField>
            <asp:BoundField DataField="FACTORI" HeaderText="Comprobante Asociado" ReadOnly="True" 
                SortExpression="FACTORI" >
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" Width="110px" />
            </asp:BoundField>
            <asp:BoundField DataField="CAE" HeaderText="CAE" ReadOnly="True" 
                SortExpression="CAE" >
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" SortExpression="FechaVencimiento" 
                DataFormatString="{0:dd/MM/yyyy}" >
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" BorderWidth="1px" Width="5%"/>
            </asp:BoundField>
            <asp:BoundField DataField="CodigoMoneda" HeaderText="Moneda" 
                SortExpression="CodigoMoneda" >
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="ImporteMonedaFacturacion" HeaderText="Importe" 
                SortExpression="ImporteMonedaFacturacion" >
                <ItemStyle HorizontalAlign="Right" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Estado" SortExpression="EstadoTransaccion">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EstadoTransaccion") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>                    
                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("EstadoTransaccion") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle BorderColor="#97A8B2" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="CodigoAFIP" HeaderText="CodigoAFIP" 
                SortExpression="CodigoAFIP" Visible="False" />
            <asp:TemplateField HeaderText="Descargar PDF">                
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                <asp:ImageButton ID="ImgOriginal" runat="server" ImageUrl="~/Images/cbteori.jpg" Width="18px" Height="18px" ImageAlign="Middle" ToolTip="Descargar comprobante Original"/>&nbsp;&nbsp;
                <asp:ImageButton ID="ImgDuplicado" runat="server"  ImageUrl="~/Images/cbtedup.jpg" Width="18px" Height="18px" ImageAlign="Middle" ToolTip="Descargar comprobante Duplicado"/>&nbsp;&nbsp;
                <asp:ImageButton ID="ImgTriplicado" runat="server"  ImageUrl="~/Images/cbtetri.jpg" Width="18px" Height="18px" ImageAlign="Middle" ToolTip="Descargar comprobante Triplicado"/>&nbsp;&nbsp;    
                <!--
                <asp:ImageButton ID="ImgCopia" runat="server"  ImageUrl="~/Images/cbtecop.jpg" Width="18px" Height="18px" ImageAlign="Middle" ToolTip="Descargar comprobante Copia"/>                                    
                -->
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" />                      
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Log">                
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="ImgLogs" runat="server" ImageUrl="~/Images/logs.gif" Width="18px" Height="18px" ImageAlign="Middle" ToolTip="Mostrar log" />&nbsp;&nbsp;
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderColor="#97A8B2" BorderStyle="Solid" 
                    BorderWidth="1px" />                      
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
        <PagerStyle BackColor="#97A8B2" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" CssClass="GVFixedFooter" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#97A8B2" Font-Bold="True" ForeColor="White" Font-Size="Small" Height="25px" BorderColor="#ffffff" BorderStyle="Solid" BorderWidth="1px" CssClass="GVFixedHeader" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    </asp:Panel>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server">
    </asp:SqlDataSource>
    <script type="text/javascript">        onLoad();</script>
</asp:Content>

