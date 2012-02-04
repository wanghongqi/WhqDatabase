<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBManage.aspx.cs" Inherits="WhqDatabase.Service.DBManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据库服务管理</title>
    <link href="_style/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        数据库当前状态：<asp:Literal runat="server" ID="litStatus"></asp:Literal><br />        
        <asp:Button runat="server" ID="btnStart" Text="启动" onclick="btnStart_Click" />
        <asp:Button runat="server" ID="btnStop" Text="停止" onclick="btnStop_Click" />
        <br />
        <br />
        新建数据库<br />
        名称：<asp:TextBox runat="server" ID="txtName"></asp:TextBox><br />
        路径：<asp:TextBox runat="server" ID="txtPath"></asp:TextBox><br />
        <asp:Button runat="server" ID="btnNew" Text="新建" onclick="btnNew_Click" />
        <asp:Button runat="server" ID="btnAdd" Text="附加" onclick="btnAdd_Click" />
    </div>
    </form>
</body>
</html>
