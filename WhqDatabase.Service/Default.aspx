<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WhqDatabase.Service.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="_style/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body{ width:1000px; height:700px; margin:0px auto;}
        .top{ height:50px; font-size:24px; font-weight:bold;}
        .middle{ height:500px;}
        .menu{ width:185px; height:500px; float:left; display:inline;}
        .main{ width:800px; height:500px; float:right; display:inline;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top">
        数据库管理系统
    </div>
    <div class="middle">
        <div class="menu">
            <iframe id="menu" name="menu" width="185" height="500" frameborder="0" src="Menu.aspx"></iframe>
        </div>
        <div class="main">
            <iframe id="main" name="main" width="500" height="500" frameborder="0" src="DBManage.aspx"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
