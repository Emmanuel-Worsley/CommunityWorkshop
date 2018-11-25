﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="CWApp.Website.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="padding-left: 2em;">
            <div class="col-4; display-5" style="border-style: outset; background-color: aqua;">
                <h5>Tool Reports</h5>
                <asp:DropDownList ID="ddlTools" runat="server" OnSelectedIndexChanged="ddlTools_SelectedIndexChanged" AutoPostBack="true" />
                <asp:Button ID="btnCheckedOutTools" runat="server" Text="Checked Out Tools" OnClick="btnCheckedOutTools_Click" />
                <br />
                <asp:Button ID="btnActiveTools" runat="server" Text="Load Tools" OnClick="btnActiveTools_Click" />
                <asp:CheckBox ID="chkActiveCheck" runat="server" />
                Active
            </div>
            <div class="col-4; display-5" style="border-style: outset; background-color: yellow; padding-left: 0.5em;">
                <h5>Patron Reports</h5>
                <asp:DropDownList ID="ddlPatrons" runat="server" OnSelectedIndexChanged="ddlPatrons_SelectedIndexChanged" AutoPostBack="true" />
            </div>
            <div class="col-4; display-5" style="border-style: outset; background-color: red; padding-left: 0.5em;">
                <h4>Test</h4>
            </div>
        </div>
    </div>
    <div class="table; display-5">
        <asp:GridView ID="gvShowReports" runat="server" AutoGenerateColumns="true">
            <HeaderStyle CssClass="active; align-content-center;" />
        </asp:GridView>
    </div>
    <br />
    <asp:Button ID="btnSave" runat="server" Text="Export Report" OnClick="btnSave_Click" />
</asp:Content>
