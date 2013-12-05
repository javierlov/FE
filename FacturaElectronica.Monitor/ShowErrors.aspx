<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowErrors.aspx.cs" Inherits="FacturaElectronica.showerrors" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Errores del Comprobante</title>
    <link rel="stylesheet" type="text/css" href="App_Themes/common.css" />
    <style type="text/CSS">
    BODY {overflow-x:scroll;}
    </style>  
</head>
<body bgcolor="#6c8eb3">
    <form id="form1" runat="server">
    <table style="width: 700px;text-align:right;background-color:#6C8EB3;">
        <tr>
            <td align="right"><asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White"></asp:Label></td>
        </tr>
        <tr>
            <td align="center">
                <div style="position:absolute;top:50;left:0;width:100%;height:240px;overflow:auto;background-color:#6C8EB3">
                    <asp:GridView ID="gvErrors" runat="server" BackColor="White" 
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        GridLines="Vertical" Width="95%" AllowPaging="False" 
                        HorizontalAlign="Center">
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" HorizontalAlign="Left" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Font-Size="Small" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" CssClass="GVFixedHeader"/>
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <EmptyDataTemplate >
                            <b>No se encontraron errores para este comprobante.</b>
                        </EmptyDataTemplate>
                    </asp:GridView>  
                </div>  
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
