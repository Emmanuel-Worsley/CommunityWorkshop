<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CWApp.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class ="topNav">
        <a href="/Tools/Default.aspx">Home</a>
        <a href="/Tools/Tools.aspx">Tools</a>
        <a href="/Loans/Loans.aspx">Loans</a>
        <a class="active" href="Reports.aspx">Reports</a>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
