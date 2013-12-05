<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="FacturaElectronica.download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Descargar</title>
</head>
<body style="background-color:white;color:#000084;font-family:Tahoma;padding-top:0px;">
    <form id="form1" runat="server">

        <div>
            <img src="Images/ajax-loader.gif" style="border:0;padding-top:0px" align="top" />   
        </div>
        <div style="width:190px;height:80px;overflow:auto;text-align:left;vertical-align:top;">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Size="Smaller"></asp:Label>
        </div>
    
    </form>
</body>
</html>
