using System;
using System.Collections;
using FacturaElectronica.DBEngine;

public partial class Variables : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WriteText("Variables : ");        
        
        WriteText("<br>...........<br> GetConnestionStringXMLSetting :");        
        try
        {WriteText(FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting());}
        catch (Exception exp)
        {WriteText("Fallo GetConnestionStringXMLSetting : " + exp.Message);}


        WriteText("<br>...........<br> GetpathConfig :");
        try
        { WriteText(FacturaElectronica.DBEngine.SQLEngine.GetpathConfig()); }
        catch (Exception exp)
        { WriteText("Fallo GetpathConfig : " + exp.Message); }


        WriteText("<br>...........<br> GetGeneralSettings :");
        try
        { GeneralSet(); }
        catch (Exception exp)
        { WriteText("Fallo GetGeneralSettings : " + exp.Message); }
    }

    private void GeneralSet()
    {
        System.Collections.Hashtable parametros = new Hashtable();
        FacturaElectronica.DBEngine.SQLEngine sqle = new SQLEngine();
        parametros = sqle.GetGeneralSettings("3");
        
        foreach (DictionaryEntry uno in parametros)
        {
            WriteText(string.Format("<br>[{0}]=[{1}]",
                uno.Key, 
                uno.Value));
        }

    }

    public void WriteText(string wtext)
    {
        Response.Write(wtext);
    }
     
}