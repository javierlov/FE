<%@ page language="C#" masterpagefile="~/CEMasterPage.master" autoeventwireup="true" inherits="Accendo.ComprobanteElectronico._default, App_Web_mlfm25ox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style0
        {
        	font-family: Tahoma;
            text-align: center;
            font-weight: bold;
            font-size:14px;
            vertical-align:middle;
        }
        .style00
        {
        	font-family: Tahoma;
            text-align: center;
            font-size:14px;
            width: 50%;
            vertical-align:middle;
        }
        .style1
        {
        	font-family: Tahoma;
            text-align: right;
            font-size:14px;
            width: 50%;
            vertical-align:middle;
        }
        .style2
        {
        	font-family: Tahoma;
            text-align: left;
            font-size:14px;
            width: 50%;
            vertical-align:middle;
        }
        .style3
        {
            font-family: Tahoma;
            text-align: left;
            font-size: 14px;
            vertical-align: middle;
        }
        .style4
        {
            font-family: Tahoma;
            text-align: center;
            font-weight: bold;
            font-size: 14px;
            vertical-align: middle;
            width: 514px;
        }
        .style5
        {
            width: 514px;
        }
        .style6
        {
            font-family: Tahoma;
            text-align: right;
            font-size: 14px;
            width: 300px;
            vertical-align: middle;
        }
        .style7
        {
            font-family: Tahoma;
            text-align: right;
            font-size: 14px;
            width: 61%;
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;text-align:right;background-color:#97A8B2;background:url(Images/backgrey.gif);height:30px">
    <tr>
        <td><asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White" Font-Names="Tahoma" Text="Inicio"></asp:Label></td>
    </tr>
</table>
<br />
<table align="center" style="text-align:left;border: solid 1px white" width="98%">
    <tr>
        <td style="width:50%;text-align:right">
            Seleccione una Empresa:&nbsp;
        </td>
        <td style="width:50%">
            <asp:DropDownList runat="server" ID="ddlEmpresa" Width="350px" 
                onselectedindexchanged="ddlEmpresa_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </td>
    </tr>
</table>
<br />
<table align="center" width="98%">
<tr style="vertical-align:top;">
<td style="width:50%">
<table align="center" style="text-align:center;border: solid 1px white" width="100%">
    <tr>
        <td class="style4">
            Verificar Autorización a los Servicios
        </td>
    </tr>  
    <tr>
        <td>
            <table style="text-align:center;" align="center">
            <tr>
                <td colspan="2" style="height:4px"></td>
            </tr>
            <tr>
                <td class="style1">Exportacion</td><td class="style2">&nbsp;-&nbsp;<asp:Label 
                    runat="server" ID="lblWSAAExportacion" Width="150px" ReadOnly="true" 
                    Text="Sin Comprobar" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"></asp:Label></td>
            </tr>
            </table>

            <table style="text-align:center;" align="center">
            <tr>
                <td colspan="2" style="height:4px"></td>
            </tr>
            <tr>
                <td class="style1">Bienes y Servicios</td><td class="style2">&nbsp;-&nbsp;<asp:Label 
                    runat="server" ID="lblWSAABienes" Width="150px" ReadOnly="true" 
                    Text="Sin Comprobar" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"></asp:Label></td>
            </tr>
            </table>

            <table style="text-align:center;" align="center">
            <tr>
                <td colspan="2" style="height:4px"></td>
            </tr>
            <tr>
                <td class="style1">Bienes de Capital</td><td class="style2">&nbsp;-&nbsp;<asp:Label 
                    runat="server" ID="lblWSAACapital" Width="150px" ReadOnly="true" 
                    Text="Sin Comprobar" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"></asp:Label></td>
            </tr>
            </table>

            <table style="text-align:center;" align="center">
            <tr>
                <td style="height:4px"></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCheckWSAA" 
                        runat="server" Text=" Comprobar " Height="20px" Font-Size="X-Small" 
                        Width="100px" onclick="btnCheckWSAA_Click"/>
                </td>
            </tr>
            <tr>
                <td style="height:4px"></td>
            </tr>
            </table>
        </td>
    </tr>
</table>
</td>
<td style="width:50%">
<table align="center" style="text-align:center;border: solid 1px white" width="100%">
    <tr>
        <td class="style0">
            Verificar Disponibilidad de los Servicios
        </td>
    </tr>   
    <tr>
        <td>
            <table style="text-align:center;" align="center">
            <tr>
                <td colspan="2" style="height:4px"></td>
            </tr>
            <tr>
                <td class="style1">Exportacion</td><td class="style2">&nbsp;-&nbsp;<asp:Label runat="server" ID="lblExportacion" Width="150px" ReadOnly="true" Text="Sin Comprobar" Font-Bold="true" Font-Names="Tahoma" Font-Size="Small"></asp:Label></td>
            </tr>
            </table>

            <table style="text-align:center;" align="center">
            <tr>
                <td colspan="2" style="height:4px"></td>
            </tr>
            <tr>
                <td class="style1">Bienes y Servicios</td><td class="style2">&nbsp;-&nbsp;<asp:Label runat="server" ID="lblBienes" Width="150px" ReadOnly="true" Text="Sin Comprobar" Font-Bold="true" Font-Names="Tahoma" Font-Size="Small"></asp:Label></td>
            </tr>
            </table>

            <table style="text-align:center;" align="center">
            <tr>
                <td colspan="2" style="height:4px"></td>
            </tr>
            <tr>
                <td class="style1">Bienes de Capital</td><td class="style2">&nbsp;-&nbsp;<asp:Label runat="server" ID="lblCapital" Width="150px" ReadOnly="true" Text="Sin Comprobar" Font-Bold="true" Font-Names="Tahoma" Font-Size="Small"></asp:Label></td>
            </tr>
            </table>

            <table style="text-align:center;" align="center">
            <tr>
                <td colspan="2" style="height:4px"></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCheckAfipBCService" 
                        runat="server" Text=" Comprobar " Height="20px" Font-Size="X-Small" 
                        Width="100px" onclick="btnCheckAfipService_Click"/>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height:4px"></td>
            </tr>
            </table>
        </td>
    </tr>
</table>
</td>
</tr>
</table>

<table align="center" width="98%">
<tr>
    <td style="height:4px"></td>
</tr>
</table>
<table align="center" width="98%">
<tr style="vertical-align:top;">
<td style="width:50%">
    <table style="text-align:center;border: solid 1px white;height:200px;vertical-align:top;" width="100%">
    <tr>
        <td colspan="2" style="height:4px"></td>
    </tr>
    <tr>
        <td colspan="2" class="style0">Próximo Número de Comprobante</td>
    </tr>
    <tr>
        <td class="style7">Nro:</td>
        <td align="left"> 
            &nbsp;<asp:Label runat="server" ID="lblNumCbte" Width="350px" ReadOnly="true" Text="XXXXXXXX" Font-Bold="true" Font-Names="Tahoma" Font-Size="Small" ></asp:Label>  
        </td>
    </tr>
    <tr>
        <td class="style7">
            Sucursal:&nbsp;&nbsp;
        </td>
        <td align="left"> 
            <asp:DropDownList ID="ddlSucursales" runat="server" Height="23px" Font-Size="Small" Width="150px" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style7">
            Tipo de Comprobante:&nbsp;&nbsp;
        </td>
        <td align="left"> 
            <asp:DropDownList ID="ddlTipoComprobante" runat="server" Height="23px" Font-Size="Small" Width="150px" >
            <asp:ListItem  Value="1" Text="01 - Factura A"></asp:ListItem>
            <asp:ListItem  Value="2" Text="02 - Nota de Débito A"></asp:ListItem>
            <asp:ListItem  Value="3" Text="03 - Nota de Crédito A"></asp:ListItem>
            <asp:ListItem  Value="6" Text="06 - Factura B"></asp:ListItem>
            <asp:ListItem  Value="7" Text="07 - Nota de Débito B"></asp:ListItem>
            <asp:ListItem  Value="8" Text="08 - Nota de Crédito B"></asp:ListItem>        
            <asp:ListItem  Value="19" Text="19 - Factura E"></asp:ListItem>
            <asp:ListItem  Value="20" Text="20 - Nota de Débito E"></asp:ListItem>
            <asp:ListItem  Value="21" Text="21 - Nota de Crédito E"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height:4px"></td>
    </tr>
    <tr>
        <td colspan="2" class="style0"><asp:Button ID="btnObtenerNum" runat="server" Text=" Obtener " 
                Height="20px" Font-Size="X-Small" Width="100px" 
                onclick="btnObtenerNum_Click" /> </td>
    </tr>
    <tr>
        <td colspan="2" style="height:4px"></td>
    </tr>
    </table>
</td>
<td style="width:50%">
    <table style="text-align:center;border: solid 1px white;height:200px;vertical-align:top;" align="center" width="100%">
    <tr>
        <td colspan="2" style="height:4px"></td>
    </tr>
    <tr>
        <td colspan="2" class="style0">Consultar CAE</td>
    </tr>
    <tr>
        <td class="style6">Nro CAE:</td>
        <td align="left"> 
            &nbsp;<asp:Label runat="server" ID="lblCAE" Width="350px" Text="XXXXXXXX" Font-Bold="true" Font-Names="Tahoma" Font-Size="Small" ></asp:Label>  
        </td>
    </tr>
    <tr>
        <td class="style6">Fecha Vto:</td>
        <td align="left"> 
            &nbsp;<asp:Label runat="server" ID="lblVto" Width="350px" Text="XX/XX/XXXX" Font-Bold="true" Font-Names="Tahoma" Font-Size="Small" ></asp:Label>  
        </td>
    </tr>
    <tr>
        <td class="style6">
            Nro Comprobante:&nbsp;&nbsp;
        </td>
        <td align="left"> 
            <asp:TextBox ID="txtNroComprobante" runat="server" Width="145px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style6">
            Sucursal:&nbsp;&nbsp;
        </td>
        <td align="left"> 
            <asp:DropDownList 
                ID="ddlSucursalCAE" runat="server" Height="23px" Font-Size="Small" 
                Width="150px" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style6">
            Tipo de Comprobante:&nbsp;&nbsp;
        </td>
        <td align="left"> 
            <asp:DropDownList 
                ID="ddlTipoComprobanteCAE" runat="server" Height="23px" Font-Size="Small" 
                Width="150px" >
            <asp:ListItem  Value="1" Text="01 - Factura A"></asp:ListItem>
            <asp:ListItem  Value="2" Text="02 - Nota de Débito A"></asp:ListItem>
            <asp:ListItem  Value="3" Text="03 - Nota de Crédito A"></asp:ListItem>
            <asp:ListItem  Value="6" Text="06 - Factura B"></asp:ListItem>
            <asp:ListItem  Value="7" Text="07 - Nota de Débito B"></asp:ListItem>
            <asp:ListItem  Value="8" Text="08 - Nota de Crédito B"></asp:ListItem>        
            <asp:ListItem  Value="19" Text="19 - Factura E"></asp:ListItem>
            <asp:ListItem  Value="20" Text="20 - Nota de Débito E"></asp:ListItem>
            <asp:ListItem  Value="21" Text="21 - Nota de Crédito E"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height:4px"></td>
    </tr>
    <tr>
        <td colspan="2" class="style0"><asp:Button ID="btnGetCAE" runat="server" Text=" Obtener " 
                Height="20px" Font-Size="X-Small" Width="100px" 
                onclick="btnGetCAE_Click" /> </td>
    </tr>
    <tr>
        <td colspan="2" style="height:4px"></td>
    </tr>
    </table>
</td>
</tr>
</table>
<table align="center" width="98%">
<tr>
    <td style="height:4px"></td>
</tr>
</table>
<table style="text-align:center;border: solid 1px white;" align="center" width="98%">
<tr>
    <td class="style0">Actualizar Tablas de AFIP</td>
</tr>
<tr>
    <td>
        <table width="50%">
        <tr>
            <td class="style3"><asp:CheckBox ID="chkIncoterms" runat="server" Checked="false" />&nbsp;Incoterms</td>
            <td class="style3"><asp:CheckBox ID="chkUnidadMedida" runat="server" Checked="false" />&nbsp;Unidad de Medida</td>
            <td class="style3"><asp:CheckBox ID="chkZona" runat="server" Checked="false" />&nbsp;Zona</td>
        </tr>
        <tr>
            <td class="style3"><asp:CheckBox ID="chkMoneda" runat="server" Checked="false" />&nbsp;Moneda</td>
            <td class="style3"><asp:CheckBox ID="chkTipoNCM" runat="server" Checked="false" />&nbsp;Tipo NCM</td>
            <td class="style3"><asp:CheckBox ID="chkJurisdiccion" runat="server" Checked="false" />&nbsp;Jurisdiccion IIBB</td>
        </tr>
        <tr>
            <td class="style3"><asp:CheckBox ID="chkPais" runat="server" Checked="false" />&nbsp;Pais</td>
            <td class="style3"><asp:CheckBox ID="chkTasaIVA" runat="server" Checked="false" />&nbsp;Tasa IVA</td>
            <td class="style3"><asp:CheckBox ID="chkOperacion" runat="server" Checked="false" />&nbsp;Operacion</td>
        </tr>
        <tr>
            <td class="style3"><asp:CheckBox ID="chkTipoCbte" runat="server" Checked="false" />&nbsp;Tipo Comprobante</td>
            <td class="style3"><asp:CheckBox ID="chkTipoDoc" runat="server" Checked="false" />&nbsp;Tipo Documento</td>
            <td class="style3"><asp:CheckBox ID="chkTipoResponsable" runat="server" Checked="false" />&nbsp;Tipo Responsable</td>
        </tr>
        <tr>
            <td  colspan="3" class="style0">
                <asp:Button ID="btnUpdate" runat="server" Text=" Actualizar " 
                    Height="20px" Font-Size="X-Small" Width="100px" onclick="btnUpdate_Click"/> 
            </td>
        </tr>
        </table>
        <table align="center" width="98%">
        <tr>
        <td>
            <asp:Label id="lblErrorMessages" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
        </tr>
        </table>
    </td>
</tr> 
</table>   
</asp:Content>

