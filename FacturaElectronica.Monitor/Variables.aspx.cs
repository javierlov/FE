using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Variables : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WriteText("Variables : ");

        WriteText("<br>...........<br> GetConnestionStringXMLSetting :");
        try
        {
            WriteText(FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting());
        }
        catch (Exception exp)
        {
            WriteText("Fallo GetConnestionStringXMLSetting : " + exp.Message);
        }


        WriteText("<br>...........<br> GetpathConfig :");
        try
        {
            WriteText(FacturaElectronica.DBEngine.SQLEngine.GetpathConfig());
        }
        catch (Exception exp)
        {
            WriteText("Fallo GetpathConfig : " + exp.Message);
        }

    }

    
    public void WriteText(string wtext)
    {
        Response.Write(wtext);
    }
     
}