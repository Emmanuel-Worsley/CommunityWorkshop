<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="CWApp.Website.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblStatus" CssClass="alert-danger" runat="server" />
    <div class="row" style="padding-left: 0.5em;">
        <div class="col-4" style="border-style: outset; background-color: lightcyan;">
            <h5>Tool Reports</h5>
            <asp:DropDownList ID="ddlTools" runat="server" />
            <asp:Button ID="loadToolHistory" runat="server" Text="Load History" OnClick="loadToolHistory_Click" />
            <asp:Button ID="btnCheckedOutTools" runat="server" Text="Checked Out Tools" OnClick="btnCheckedOutTools_Click" />
            <asp:Button ID="btnActiveTools" runat="server" Text="Load Tools" OnClick="btnActiveTools_Click" />
            <asp:CheckBox ID="chkActiveCheck" runat="server" />
            Active
        </div>
        <div class="col-4" style="border-style: outset; background-color: lightcyan; padding-left: 0.5em;">
            <h5>Patron Reports</h5>
            <asp:DropDownList ID="ddlPatrons" runat="server" />
            <asp:Button ID="btnLoadPatronHistory" runat="server" Text="Load History" OnClick="btnLoadPatronHistory_Click" />
        </div>
        <div class="col-4" style="border-style: outset; background-color: lightcyan; padding-left: 0.5em;">
            <h5>Brand Reports</h5>
            <asp:DropDownList ID="ddlBrands" runat="server" />
            <asp:Button ID="btnLoadBrandHistory" runat="server" Text="Load History" OnClick="btnLoadBrandHistory_Click" />
            <asp:CheckBox ID="chkBrandsActiveCheck" runat="server" />
            Active
        </div>
    </div>
    <br />
    <div class="container-fluid" style="text-align: center; width: 100%;">
        <div class="table-">
            <br />
            <asp:GridView ID="gvShowReports" runat="server" AutoGenerateColumns="true" Width="100%">
            </asp:GridView>
            <br />
            <asp:Button ID="btnExport" runat="server" Text="Export Report" OnClick="btnSave_Click" Font-Size="Large" />
        </div>
    </div>

    <br />
</asp:Content>
