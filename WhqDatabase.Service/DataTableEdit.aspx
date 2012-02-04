<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataTableEdit.aspx.cs" Inherits="WhqDatabase.Service.DataTableEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="_script/jquery-1.4.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tr = '<tr class="info">   <td><input type="text" name="txtColumnName" maxlength="128" /></td>   <td>       <select name="ddlType">           <option value="1">Byte</option>           <option value="2" selected>Int</option>           <option value="3">Long</option>           <option value="4">String</option>       </select>   </td>   <td><input type="text" name="txtLength" value="4" readonly="true" style="width:25px" maxlength="4" /></td>   </tr>';
        $(function () {
            //初始列
            $("#tblColumn").append(tr).append(tr).append(tr).append(tr);
            $("select[name=ddlType]").live("change", function () {
                switch ($(this).val()) {
                    case "1":
                        $(this).parent().parent().find("input[name=txtLength]").val(1).attr("readonly", true);
                        break;
                    case "2":
                        $(this).parent().parent().find("input[name=txtLength]").val(2).attr("readonly", true);
                        break;
                    case "3":
                        $(this).parent().parent().find("input[name=txtLength]").val(4).attr("readonly", true);
                        break;
                    case "4":
                        $(this).parent().parent().find("input[name=txtLength]").val(32).attr("readonly", false);
                        break;
                }
            });
            //提交验证
            $("#btnSubmit").click(function () {
                if ($("#txtName").val().length == 0) {
                    alert("表名称不能为空！");
                    return false;
                }
                var intColumnCount = 0;
                for (var i = 0; i < $("#tblColumn tr.info").length; i++) {
                    var obj = $($("#tblColumn tr.info")[i]);
                    if (obj.find("input[name=txtColumnName]").val().length > 0) {
                        if (obj.find("input[name=txtLength]").val().length == 0) {
                            alert("长度不能为空！");
                            return false;
                        }
                        intColumnCount++;
                    }
                }
                if (intColumnCount == 0) {
                    alert("表中至少要有一列！");
                    return false;
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        表名称：<asp:TextBox runat="server" ID="txtName"></asp:TextBox><br />
        列
        <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tblColumn">
        <tr>
        <th>列名</th>
        <th>类型</th>
        <th>长度</th>
        </tr>
        </table><br />
        <asp:Button runat="server" ID="btnSubmit" Text="提交" onclick="btnSubmit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="btnColumnAdd" value="添加列" onclick='$("#tblColumn").append(tr)' />
    </div>
    </form>
</body>
</html>
