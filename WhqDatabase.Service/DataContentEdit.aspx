<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataContentEdit.aspx.cs" Inherits="WhqDatabase.Service.DataContentEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="_script/jquery-1.4.2.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tblData">
        <tr>
            <th>名称</th>
            <th>类型</th>
            <th>长度</th>
            <th>值</th>
        </tr>
        <asp:Repeater runat="server" ID="rptColumn">
        <ItemTemplate>
        <tr>
            <td><%#Eval("Name") %></td>
            <td><%#Eval("DataType")%></td>
            <td><%#Eval("DataLength")%></td>
            <td><%#(WhqDatabase.Kernel.DataContent.EContentType)Eval("DataType")== WhqDatabase.Kernel.DataContent.EContentType.String?
                    "<textarea name='txt"+Eval("Name")+"'></textarea>":
                    "<input type='text' name='txt"+Eval("Name")+"' />"%></td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table><br />
        <asp:Button runat="server" ID="btnSubmit" Text="提交" onclick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
