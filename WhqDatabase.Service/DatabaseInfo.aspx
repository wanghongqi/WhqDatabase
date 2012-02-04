<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseInfo.aspx.cs" Inherits="WhqDatabase.Service.DatabaseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        数据库名称：&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtName" />
        <asp:Button runat="server" ID="btnUpdate" Text="改名" onclick="btnUpdate_Click" /><br />
        操作：<asp:Button runat="server" ID="btnDelete" Text="删除" onclick="btnDelete_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnRemove" Text="分离" onclick="btnRemove_Click" />
        &nbsp;&nbsp;<input type="button" value="新建表" onclick="location='DataTableEdit.aspx?DatabaseID=<%=DB.ID %>'" />
        <br />
        <br />
        表
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <th width="70%">表名</th>
            <th>操作</th>
        </tr>
        <asp:Repeater runat="server" ID="rptTable" onitemcommand="rptTable_ItemCommand">
            <ItemTemplate>
            <tr>
                <td><a href="DataContentList.aspx?DatabaseID=<%=DB.ID %>&DataTableID=<%#Eval("ID") %>"><%#Eval("Name") %></a></td>
                <td><a href="DataTableInfo.aspx?DatabaseID=<%=DB.ID %>&DataTableID=<%#Eval("ID") %>">修改</a>
                    &nbsp;&nbsp;<asp:LinkButton runat="server" ID="btnTableDelete" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" Text="删除"></asp:LinkButton></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
