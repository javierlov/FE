using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

public partial class AdvanceSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        XmlDocument xmlSearchQuery = new XmlDocument();

        xmlSearchQuery = CreateXmlDocument();

        Session["xmlSearchQuery"] = xmlSearchQuery.InnerXml;

        Response.Redirect("activityreg.aspx");
    }

    private XmlDocument CreateXmlDocument()
    {
        XmlDocument xmlReturn = new XmlDocument();
        XmlNode xRoot;
        XmlNode xNode;
        XmlNode xxNode;

        xRoot = xmlReturn.CreateElement("search");
        xmlReturn.AppendChild(xRoot);

        for (int i = 0; i < 30; i++)
        {
            object objField = Request.Form.Get("field" + i.ToString());
            object objValue = Request.Form.Get("value" + i.ToString());
            object objValueB = Request.Form.Get("valueb" + i.ToString());

            if (objField != null && objValue != null)
            {
                xNode = xmlReturn.CreateElement("condition");
                xRoot.AppendChild(xNode);

                xxNode = xmlReturn.CreateElement("field");
                xxNode.InnerText = objField.ToString();
                xNode.AppendChild(xxNode);

                xxNode = xmlReturn.CreateElement("value");
                xxNode.InnerText = objValue.ToString();
                xNode.AppendChild(xxNode);

                if (objValueB != null)
                {
                    xxNode = xmlReturn.CreateElement("valueb");
                    xxNode.InnerText = objValueB.ToString();
                    xNode.AppendChild(xxNode);
                }
            }
        }

        return xmlReturn;
    }
}
