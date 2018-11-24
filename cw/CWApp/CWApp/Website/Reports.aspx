<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="CWApp.Website.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="display-5" style="padding-left: 2em;">
            <h2>Reports On Tools:</h2>
            <asp:DropDownList ID="ddlTools" runat="server" />
            <asp:Button ID="btnCheckedOutTools" runat="server" Text="Checked Out Tools" /> <br />
            <asp:Button ID="btnActiveTools" runat="server" Text="Load Tools" />
            <asp:CheckBox ID="chkActive" runat="server" /> Active
        </div>
    </div>
</asp:Content>
