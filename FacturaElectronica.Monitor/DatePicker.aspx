<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatePicker.aspx.cs" Inherits="DatePicker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:calendar id="Calendar1" runat="server" showgridlines="True" bordercolor="Black">
			<todaydaystyle forecolor="White" backcolor="#FFCC66"></todaydaystyle>
			<selectorstyle backcolor="#FFCC66"></selectorstyle>
			<nextprevstyle font-size="9pt" forecolor="#FFFFCC"></nextprevstyle>
			<dayheaderstyle height="1px" backcolor="#FFCC66"></dayheaderstyle>
			<selecteddaystyle font-bold="True" backcolor="#CCCCFF"></selecteddaystyle>
			<titlestyle font-size="9pt" font-bold="True" forecolor="#FFFFCC" backcolor="#990000"></titlestyle>
			<othermonthdaystyle forecolor="#CC9966"></othermonthdaystyle>
		</asp:calendar>
    </div>
    </form>
</body>
</html>
