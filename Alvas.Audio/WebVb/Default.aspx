<%@ Page Language="VB" AutoEventWireup="true"  CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Online Alvas.Audio WAVE Converter</title>
</head>
<body bgcolor="#f7f3e9">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:label id="lblResult" ForeColor="red" runat="server"></asp:label>
				<table width="100%" height="100%">
				<tr>
				<td width="50%" valign="top">

				<asp:label id="lblFile" runat="server">Audio File:</asp:label>
				<input id="filMyFile" type="file" runat="server"/>
				<asp:button id="cmdSend" runat="server" Text="Upload" onclick="cmdSend_Click"/>

				</td>
				<td width="50%" valign="top">
				<asp:button id="btnConvert" runat="server" Text="Convert"  Enabled="false"
                        onclick="btnConvert_Click"/>
				
                <asp:HyperLink ID="HyperLink1" Target="_blank" runat="server">Filename: </asp:HyperLink>		
				    <br />
				<asp:Literal id="lblInfo" runat="server" Visible="false"></asp:Literal>
				
				</td>
				</tr>

				<tr>
				<td width="50%" valign="top">
				<asp:ListBox ID="lbFiles" runat="server" Width="100%" height="500"
                        AutoPostBack="True" onselectedindexchanged="lbFiles_SelectedIndexChanged"></asp:ListBox>
				</td>
				<td width="50%" valign="top">
				<asp:ListBox ID="lbFormats" runat="server" Width="100%" AutoPostBack="True" height="500"
                        OnSelectedIndexChanged="lbFormats_SelectedIndexChanged"></asp:ListBox>
				</td>
				</tr>
				</table>
    </form>
</body>
</html>
