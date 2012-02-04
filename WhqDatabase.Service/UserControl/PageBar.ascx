<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageBar.ascx.cs" Inherits="WhqDatabase.Service.UserControl.PageBar" %>
总<asp:Literal runat="server" ID="litRowCount"></asp:Literal>行，
        第<asp:Literal runat="server" ID="litPageNum"></asp:Literal>/<asp:Literal runat="server" ID="litPageCount"></asp:Literal>页
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton runat="server" ID="lbFirst" Text="首页" onclick="lbFirst_Click"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lbPrev" Text="上一页" onclick="lbPrev_Click"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lbNext" Text="下一页" onclick="lbNext_Click"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lbLast" Text="尾页" onclick="lbLast_Click"></asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="txtPageNum" Width="25"></asp:TextBox>
        <asp:Button runat="server" ID="btnPageGo" Text="跳转" onclick="btnPageGo_Click" /> <br />
