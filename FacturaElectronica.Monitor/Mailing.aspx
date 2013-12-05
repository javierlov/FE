<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mailing.aspx.cs" Inherits="FacturaElectronica.Mailing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enviar PDFs</title>
    <link rel="stylesheet" type="text/css" href="App_Themes/common.css" />
    <style type="text/CSS">
         #header {
         position : fixed;
         width : 100%;
         height : 10%;
         top : 0;
         right : 0;
         bottom : auto;
         left : 0;
         border-bottom : 2px solid #cccccc;
         }

         #content {
         position : fixed;
         top : 10%;
         bottom : 100px;
         width : 630px;
         height : 170px;
         margin : 0px;
         padding-right : 12px;         
         padding-left : 5px;
         color : #000000;
         overflow : auto;        
         }
         
         #footer {
         position: fixed;
         width: 100%;
         height: 30px;
         top: auto;
         right: 0;
         bottom: 0;
         border-top : 2px solid #cccccc;
         }
    </style>  
</head>
<body style="margin:0;padding:0;">
    <form id="form1" runat="server">
    <div id="header">
    <table style="width: 100%;text-align:left;background-color:#97A8B2;background:url(Images/backgrey.gif);height:30px">
        <tr>
            <td><asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White" Font-Names="Tahoma" Text="Envio de Comprobantes"></asp:Label></td>
        </tr>
    </table>  
    </div>
    <div id="content">
    <asp:Table ID="tblComprobantes" runat="server"></asp:Table>
    <asp:Label ID="lblErrors" runat="server"></asp:Label>
    </div>
    <div id="footer">
    <table width="100%" style="text-align:center">
        <tr>
            <td>
                <asp:Button ID="btnSend" runat="server" Text="    Enviar    " 
                    onclick="btnSend_Click" />&nbsp;&nbsp;
                <asp:Button ID="BbtnCancel" runat="server" Text="    Cerrar    " 
                    onclick="BbtnCancel_Click" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
