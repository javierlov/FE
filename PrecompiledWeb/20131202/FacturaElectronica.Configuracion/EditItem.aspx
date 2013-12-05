<%@ page language="C#" masterpagefile="~/CEMasterPage.master" autoeventwireup="true" inherits="Accendo.ComprobanteElectronico.edititem, App_Web_mlfm25ox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
        	font-family: Tahoma;
            text-align: right;
            font-weight: bold;
            font-size:14px;
            width: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 100%;text-align:right;background-color:#97A8B2;background:url(Images/backgrey.gif);height:30px">
    <tr>
        <td><asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White" Font-Names="Tahoma"></asp:Label></td>
    </tr>
</table>
<br />
<table>
<asp:Panel ID="pnEmpresaID" runat="server">
<tr><td class="style1"><asp:Label ID="lblEmpresaID" Text="Empresa ID:" runat="server"></asp:Label>&nbsp;</td><td><asp:DropDownList runat="server" 
        ID="ddlEmpresaID" Width="350px"></asp:DropDownList></td></tr>
</asp:Panel>
<asp:Panel ID="pnCodigoEmpresa" runat="server">
<tr><td class="style1"><asp:Label ID="lblCodigoEmpresa" Text="Codigo Empresa:" runat="server"></asp:Label>&nbsp;</td><td><asp:TextBox runat="server" 
        ID="txtCodigoEmpresa" Width="350px"></asp:TextBox></td></tr>
</asp:Panel>
<asp:Panel ID="pnDDLCodigoAFIP" runat="server">
<tr><td class="style1"><asp:Label ID="lblDDLCodigoAFIP" Text="Codigo AFIP:" runat="server"></asp:Label>&nbsp;</td><td><asp:DropDownList runat="server" 
        ID="ddlCodigoAFIP" Width="356px"></asp:DropDownList></td></tr>
</asp:Panel>
<asp:Panel ID="pnTXTCodigoAFIP" runat="server">
<tr><td class="style1"><asp:Label ID="lblCodigoAFIP" Text="Codigo AFIP:" runat="server"></asp:Label>&nbsp;</td><td><asp:TextBox runat="server" 
        ID="txtCodigoAFIP" Width="350px"></asp:TextBox></td></tr>
</asp:Panel>
<asp:Panel ID="pnDescripcion" runat="server">
<tr><td class="style1"><asp:Label ID="lblDescripcion" Text="Descripcion:" runat="server"></asp:Label>&nbsp;</td><td><asp:TextBox runat="server" 
        ID="txtDescripcion" Width="350px"></asp:TextBox></td></tr>
</asp:Panel>
<asp:Panel ID="pnEmpresa" runat="server">

</asp:Panel>
<tr><td>&nbsp;</td>
    <td align="left"><table width="100%"><tr><td><asp:Button ID="btnModificar" 
                runat="server" Text="Modificar" onclick="btnModificar_Click" Width="120px" /></td><td align="right">
            <asp:Button ID="btnEliminar" 
                runat="server" Text="Eliminar" Width="120px" onclick="btnEliminar_Click" /></td></tr></table></td></tr> 
</table>
</asp:Content>

