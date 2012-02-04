<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataContentList.aspx.cs" Inherits="WhqDatabase.Service.DataContentList" %>

<%@ Register src="UserControl/PageBar.ascx" tagname="PageBar" tagprefix="whquc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <input type="button" value="添加" onclick="location='DataContentEdit.aspx?DatabaseID=<%=Table.Database.ID %>&DataTableID=<%=Table.ID %>'" />
        <br />
        <whquc:PageBar ID="pageBarTop" runat="server" OnChangeNum="pageBarTop_ChangeNum" />
        
        <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tblData">
        <tr>
        <asp:Repeater runat="server" ID="rptColumn">
        <ItemTemplate>
            <th><%#Eval("Name") %></th>
        </ItemTemplate>
        </asp:Repeater>
            <th>操作</th>
        </tr>        
        <asp:Repeater runat="server" ID="rptData" onitemcommand="rptData_ItemCommand">
        <ItemTemplate>
            <tr>
                <td><%#string.Join("</td><td>",WhqDatabase.Service.Common.ArrayUtil.ArrObjToStr((object[])Eval("Contents"))) %></td>
                <td>
                    <a href="DataContentEdit.aspx?DatabaseID=<%=Table.Database.ID %>&DataTableID=<%=Table.ID %>&RowIndex=<%#Eval("RowIndex") %>">编辑</a>
                    <asp:LinkButton runat="server" ID="btnDelete" Text="删除" CommandName="Delete" CommandArgument='<%#Eval("RowIndex") %>' OnClientClick="return confirm('是否要删除该行')"></asp:LinkButton></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
