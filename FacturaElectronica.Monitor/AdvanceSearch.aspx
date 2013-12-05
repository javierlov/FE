<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/CEMasterPage.master" AutoEventWireup="true" CodeFile="AdvanceSearch.aspx.cs" Inherits="AdvanceSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="App_Themes/datepicker.css" type="text/css" />
<script type="text/javascript" src="js/date.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery.datePicker.js"></script>
<script type="text/javascript">
var CantRows = 0;

function addRow(data, value) 
{
    //add a row to the rows collection and get a reference to the newly added row
    var newRow = document.all("tblGrid").insertRow();
    var oCell  = newRow.insertCell();
           
    var NewInput = new String('<input type="hidden" name="fieldxxx" id="fieldxxx" value="' + value + '"/>');
    
    oCell.align="right";
    oCell.innerHTML = "<img src='Images/remove.gif' Align='AbsMiddle' onclick='removeRow(this);'/>" + data + NewInput.replace("xxx",newRow.rowIndex);
    
    oCell = newRow.insertCell();
    oCell.align="left";

    if (data == "Fecha")
    {
        NewInput = new String('<input type="text" name="valuexxx" id="valuexxx" class="date-pick" />');
        NewInput2 = new String('<input type="text" name="valuebxxx" id="valuebxxx" class="date-pick" />');

        oCell.innerHTML = NewInput.replace("xxx", newRow.rowIndex) + " >< " + NewInput2.replace("xxx", newRow.rowIndex);

        document.getElementById("value" + newRow.rowIndex).readOnly = "readonly";
        document.getElementById("valueb" + newRow.rowIndex).readOnly = "readonly";
    }
    else if (data == "Número de FC/NC/ND")
    {
        NewInput = new String('<input type="text" name="valuexxx" id="valuexxx" />');
        NewInput2 = new String('<input type="text" name="valuebxxx" id="valuebxxx" />');

        oCell.innerHTML = NewInput.replace("xxx", newRow.rowIndex) + " >< " + NewInput2.replace("xxx", newRow.rowIndex);
    }
    else
    {
        NewInput = new String('<input type="text" name="valuexxx" id="valuexxx" />');

        oCell.innerHTML = NewInput.replace("xxx", newRow.rowIndex);
    }
    calendarPickerOn();
}

function removeRow(src)
{  
    var oRow = src.parentElement.parentElement;  
        
    //once the row reference is obtained, delete it passing in its rowIndex   
    document.all("tblGrid").deleteRow(oRow.rowIndex);
}    
    
function calendarPickerOn()
{
    $(function()
    {
        $('.date-pick')
        .datePicker({createButton:false})
        .bind('click',
            function()
            {
	            $(this).dpDisplay();
	            this.blur();
	            return false;
            }
        );   
    });
}

function calendarPickerOff()
{
    $(function()
    {
        $('.date-pick')
        .datePicker({createButton:false})
        .unbind('click');       
    });
}

function calendarPicker(objSel, RunOnload)
{
    if(RunOnload == "false")
    {
        document.getElementById("ctl00_ContentPlaceHolder1_txtSearchText").value = "";
    }

    if(objSel.value == "Fecha")
    {
        calendarPickerOn();
        document.getElementById("ctl00_ContentPlaceHolder1_txtSearchText").readOnly ="readonly";
    }
    else
    {
        calendarPickerOff();
        
        if(objSel.value == "Mostrar Todo")
        {
            document.getElementById("ctl00_ContentPlaceHolder1_txtSearchText").readOnly ="readonly";
        }
        else
        {
            document.getElementById("ctl00_ContentPlaceHolder1_txtSearchText").readOnly = false;
        }   
    }
}    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;text-align:right;background-color:#97A8B2;background:url(Images/backgrey.gif);height:30px">
        <tr>
            <td><asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" ForeColor="White" Font-Names="Tahoma" Text="Busqueda Avanzada"></asp:Label></td>
        </tr>
    </table>
    <table style="width: 100%;text-align:right;background-color:#2C353C;border-top: 2px solid white;">
        <tr>
            <td align="left" valign="middle" style="font-weight:bold; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif;height:25px;">&nbsp;Buscar Por:
                <select id="ddlSearchBy">
                    <option selected>Mostrar Todo</option>
                    <option value="CompradorCodigoCliente">Cliente</option>
                    <option value="EstadoTransaccion">Estado</option>
                    <option value="FechaComprobante">Fecha</option>
                    <option value="NroLegal">Número de FC/NC/ND</option> 
                    <option value="PuntoVenta">Sucursal</option>                
                    <option value="Descripcion">Tipo</option>              
                </select>
                <input type="button" value="Agregar" onclick="addRow(document.getElementById('ddlSearchBy').options[ document.getElementById('ddlSearchBy').selectedIndex ].text, document.getElementById('ddlSearchBy').options[ document.getElementById('ddlSearchBy').selectedIndex ].value)" />
            </td>
        </tr>
    </table>
    <table style="width:100%;background-color:#97A8B2;" id="tblGrid">
        <tr><td style="width:40%;"></td><td style="width:60%"></td></tr>
    </table>
    <table style="width:100%;background-color:#2C353C;" border="1">
        <tr><td align="right">
            <asp:Button ID="btnSearch" Text="        Buscar        " runat="server" 
                onclick="btnSearch_Click" />
        </td></tr>
    </table>
</asp:Content>

