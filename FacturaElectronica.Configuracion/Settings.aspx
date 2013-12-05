<%@ Page Language="C#" MasterPageFile="~/CEMasterPage.master" AutoEventWireup="true"
    CodeFile="Settings.aspx.cs" Inherits="Accendo.ComprobanteElectronico.settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style0
        {
            font-family: Tahoma;
            text-align: center;
            font-weight: bold;
            font-size: 14px;
        }
        .style1
        {
            font-family: Tahoma;
            text-align: right;
            font-size: 14px;
            width: 35%;
        }
        .style2
        {
            font-family: Tahoma;
            text-align: left;
            font-weight: bold;
            font-size: 14px;
            width: 65%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%; text-align: right; background-color: #97A8B2; background: url(Images/backgrey.gif);
        height: 30px">
        <tr>
            <td>
                <asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White" Font-Names="Tahoma"
                    Text="Configuraci&oacute;n General"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table align="center" style="text-align:left;border: solid 1px white" width="98%">
        <tr>
            <td class="style1">
                Seleccione una Empresa:&nbsp;
            </td>
            <td class="style2">
                <asp:DropDownList runat="server" ID="ddlEmpresa" Width="350px" AutoPostBack="true" 
                    onselectedindexchanged="ddlEmpresa_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table align="center" style="text-align:center;border: solid 1px white" width="98%">
        <tr>
            <td colspan="2" class="style0">
                General
            </td>
        </tr>
        <tr>
            <td class="style1">
                ID Empresa:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="EmpresaID" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                CUIT Empresa:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="CUITEmpresaOperaServicio" Width="350px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="style1">
                Activar Empresa:&nbsp;
            </td>
            <td class="style2">
                <asp:CheckBox runat="server" ID="chkEmpresa" Width="350px"></asp:CheckBox>
            </td>
        </tr>

        <tr>
            <td class="style1">
                Activar Debug:&nbsp;
            </td>
            <td class="style2">
                <asp:CheckBox runat="server" ID="chkDebug" Width="350px"></asp:CheckBox>
            </td>
        </tr>
    </table>
    <br />    
    
       <table align="center" style="text-align:center;border: solid 1px white" width="98%">
        <tr>
            <td colspan="2" class="style0">
                WebServices
            </td>
        </tr>    
        <tr>
            <td class="style1">
                Url Servicio Web FE:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="UrlFEWebService" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Url Servicio Web Impresion:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="UrlPrintWebService" Width="350px"></asp:TextBox>
            </td>
        </tr>  
        <tr>
            <td class="style1">
                Url AFIP wsaa:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="UrlAFIPwsaa" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Url AFIP wsfe:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="UrlAFIPwsfe" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Url AFIP wsfex:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="UrlAFIPwsfex" Width="350px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="style1">
                Url AFIP wsbfe:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="UrlAFIPwsbfe" Width="350px"></asp:TextBox>
            </td>
        </tr>
     </table>
     <br /> 
    <table align="center" style="text-align:center;border: solid 1px white" width="98%">
        <tr>
            <td colspan="2" class="style0">
                Servicio
            </td>
        </tr>
        <tr>
            <td class="style1">
                Tipo Entrada:&nbsp;
            </td>
            <td class="style2">
                <asp:DropDownList runat="server" ID="ddlTipoEntrada" Width="350px">
                    <asp:ListItem Text="File System" Value="FS"></asp:ListItem>
                    <asp:ListItem Text="Microsoft SQL  Server" Value="MSSQL"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Entrada:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathEntrada" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Entrada Adicional:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathEntradaExtra" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Tipo Salida:&nbsp;
            </td>
            <td class="style2">
                <asp:DropDownList runat="server" ID="ddlTipoSalida" Width="350px">
                    <asp:ListItem Text="File System" Value="FS"></asp:ListItem>
                    <asp:ListItem Text="Microsoft SQL  Server" Value="MSSQL"></asp:ListItem>                
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Salida:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathSalida" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Historico:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathHistorico" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Debug:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathDebug" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Certificado:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathCertificate" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Archivos de Coneccion:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathConnectionFiles" Width="350px"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td class="style1">
                Impresion:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathImpresion" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Temporales:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="PathTemporales" Width="350px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <table align="center" style="text-align:center;border: solid 1px white" width="98%">
        <tr>
            <td colspan="2" class="style0">
                Correo
            </td>
        </tr>
        <tr>
            <td class="style1">
                Servidor SMTP:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="txtSMTPServer" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Usuario SMTP:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="txtSMTPUser" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Clave SMTP:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="txtSMTPPassword" Width="350px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Mail From:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="txtSMTPFrom" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Asunto:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="txtAsunto" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Mensaje:&nbsp;
            </td>
            <td class="style2">
                <asp:TextBox runat="server" ID="txtMessage" Width="350px" TextMode="MultiLine" Rows="8"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table align="center" style="text-align:center;" width="98%">
        <tr>
            <td>
                <asp:Button ID="btnSend" runat="server" Text="     Guardar    " OnClick="btnSend_Click" />
            </td>
        </tr>
    </table>
    
</asp:Content>
