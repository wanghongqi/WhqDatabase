<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="WhqDatabase.Service.Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="_style/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .menu_list{ list-style:none; margin:0px; padding:0px;}
        .menu_list li{ margin:10px; font-size:18px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <ul class="menu_list">
            <li><a href="DBManage.aspx" target="main">数据库服务管理</a></li>
            <asp:Repeater runat="server" ID="rptDatabase">
                <ItemTemplate>
                    <li><a href="DatabaseInfo.aspx?DatabaseID=<%#Eval("ID") %>" target="main"><%#Eval("Name") %></a></li>                    
                </ItemTemplate>
            </asp:Repeater>
        </ul>            
    </div>
    </form>
</body>
</html>
