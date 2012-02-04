<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataColumnEdit.aspx.cs" Inherits="WhqDatabase.Service.DataColumnEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        列名：<asp:TextBox runat="server" ID="txtColumnName" MaxLength="128"></asp:TextBox><br />
        类型：<asp:DropDownList runat="server" ID="ddlType">
                <asp:ListItem Value="1" Text="Byte"></asp:ListItem>
                <asp:ListItem Value="2" Text="Int"></asp:ListItem>
                <asp:ListItem Value="3" Text="Length"></asp:ListItem>
                <asp:ListItem Value="4" Text="String"></asp:ListItem>
              </asp:DropDownList><br />
        长度<asp:TextBox runat="server" ID="txtLength" ReadOnly="true" MaxLength="4" Text="4"></asp:TextBox><br />

        <asp:Button runat="server" ID="btnSubmit" Text="提交" onclick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
