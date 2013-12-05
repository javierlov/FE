<%@ page language="C#" masterpagefile="~/CEMasterPage.master" autoeventwireup="true" inherits="FacturaElectronica.GeneralErrors, App_Web_42juvi0x" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript"  charset="utf-8">


onresize = function() 
{           
    resizeGridPanel();
}
        
function onLoad()
{
    resizeGridPanel();
}
 
function resizeGridPanel()
{
    //var panelH = document.documentElement.clientHeight - 185;
    document.getElementById("ctl00_ContentPlaceHolder1_pnlGridView").style.height = "100%";
}  

function getScrollBottom(p_oElem)
{
    var panH = (typeof window.innerHeight != 'undefined' ? window.innerHeight : document.body.offsetHeight);

    return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
} 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;text-align:right;background-color:#97A8B2;background:url(Images/backgrey.gif);height:30px">
        <tr>
            <td><asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White" Font-Names="Tahoma" Text="Errores Generales"></asp:Label></td>
        </tr>
    </table>
    <table style="width: 100%;text-align:right;background-color:#2C353C;border-top: 2px solid white;">
        <tr>
            <td align="left" valign="middle" style="font-weight:bold; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif;">&nbsp;
                  
            </td>
            <td>
                <asp:ImageButton ID="ImgReload" runat="server" ImageUrl="~/Images/refresh.gif" 
                    Width="23px" Height="23px" ImageAlign="Middle" ToolTip="Recargar listado" 
                    onclick="ImgReload_Click"/>&nbsp;
            </td>
        </tr>
    </table>
    <asp:Panel id="pnlGridView" runat="server" HorizontalAlign="Center" ScrollBars="Vertical">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CaptionAlign="Top" 
        CellPadding="3" DataSourceID="SqlDataSource1" ForeColor="#333333" 
        Width="98%" PageSize="15" ShowFooter="false">
        <PagerSettings FirstPageImageUrl="~/Images/left.gif" 
            LastPageImageUrl="~/Images/right.gif" Mode="NumericFirstLast" 
            NextPageImageUrl="~/Images/next.gif" PreviousPageImageUrl="~/Images/prev.gif" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Small" Height="20px" />
        <EmptyDataRowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Small" Height="20px" />
        <EmptyDataTemplate >
            <b>No se encontraron resultados.</b>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="ErrorID" HeaderText="ErrorID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ErrorID" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" 
                DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" />
            <asp:BoundField DataField="Seccion" HeaderText="Seccion" 
                SortExpression="Seccion" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" 
                SortExpression="Descripcion" />                
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#97A8B2" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" CssClass="GVFixedFooter" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#97A8B2" Font-Bold="True" ForeColor="White" Font-Size="Small" Height="20px" BorderColor="#ffffff" BorderStyle="Solid" BorderWidth="1px" CssClass="GVFixedHeader" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server">
    </asp:SqlDataSource>    
    </asp:Panel>
<script type="text/javascript">onLoad();</script>
</asp:Content>

