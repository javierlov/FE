<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="FacturaElectronica.Impresion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impresión</title>
    <script src="js/docwrite.js"></script>
</head>
<body style="background-color:white;color:#000084;font-family:Tahoma;border:0;padding:0;vertical-align:top;">
    <div>
        <script type="text/javascript">
            createEmbed("<%=PDFName%>");

            var x = document.getElementById("iPDF");
            x.click();
            x.setActive();
            x.focus(); 

        </script>
        <!--<embed type="application/pdf" id="iPDF" src="<%=PDFName%>#toolbar=1&navpanes=0&scrollbar=0" width="100%" height="390px" ></embed>-->
    </div>
</body>
</html>
