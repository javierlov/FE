<%@ Page Language="C#" MasterPageFile="~/CEMasterPage.master" AutoEventWireup="true" CodeFile="AfipABM.aspx.cs" Inherits="Accendo.ComprobanteElectronico.afipabm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 100%;text-align:right;background-color:#97A8B2;background:url(Images/backgrey.gif);height:30px">
    <tr>
        <td><asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White" Font-Names="Tahoma"></asp:Label></td>
    </tr>
</table>
<table style="width: 100%;text-align:center;font-weight:bold">
    <tr>
        <td><asp:LinkButton ID="lnkCrear" runat="server" Text="Crear Item" Font-Bold="True" Font-Names="Tahoma" 
                ForeColor="#343648" Font-Size="14px"></asp:LinkButton></td>
    </tr>
</table>
          
<asp:GridView ID="gvAFIPTable" runat="server" AllowPaging="True" 
    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
    CellPadding="3" HorizontalAlign="Center"         
    ShowFooter="False" Width="60%" onrowdatabound="gvAFIPTable_RowDataBound" 
    PageSize="15" onpageindexchanging="gvAFIPTable_PageIndexChanging" 
        onrowcreated="gvAFIPTable_RowCreated">
    <PagerSettings FirstPageImageUrl="~/Images/left.gif" FirstPageText="Primero" 
        LastPageImageUrl="~/Images/right.gif" LastPageText="Ultimo" NextPageImageUrl="~/Images/next.gif" 
        NextPageText="Próxima" PreviousPageImageUrl="~/Images/prev.gif" 
        PreviousPageText="Anterior" Visible="true" />
    <RowStyle Font-Size="Small" Height="20px" ForeColor="#000066" />
    <PagerStyle BackColor="#669999" ForeColor="#000066" HorizontalAlign="Left" 
        Font-Size="Small" Height="18px" BorderStyle="None" VerticalAlign="Middle" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#97A8B2" Font-Bold="True" ForeColor="White" 
        Font-Size="Small" Height="20px" />
    <EditRowStyle Font-Size="XX-Small" Height="20px" />
    <EmptyDataRowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="Small" Height="20px" />
    <EmptyDataTemplate >
        <b>No se encontraron resultados.</b>
    </EmptyDataTemplate>
</asp:GridView>

<br />
<br />
<br />
<br />
</asp:Content>

