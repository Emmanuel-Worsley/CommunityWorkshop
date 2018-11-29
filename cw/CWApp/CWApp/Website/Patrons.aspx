<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Patrons.aspx.cs" Inherits="CWApp.Website.Patrons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center">
        <asp:Label ID="lblStatus" runat="server" CssClass="alert-success" />
    </div>
    <div class="row" style="padding-left: 0.5em">
        <div class="col" style="text-align: center; border-style:outset; background-color: lightcyan;">
            <h5>Patrons</h5>
            Patron ID: <asp:Label ID="lblPatronID" runat="server"/>
            <br />
            Patron Name: <asp:TextBox ID="txtPatronName" runat="server"  CssClass ="Margin" />
            Contact Number: <asp:TextBox ID="txtContactNumber" runat="server" CssClass ="Margin"  />
            <asp:Button ID="btnNewPatron" runat="server" Text="New Patron" OnClick="btnNewPatron_Click"/>
            <asp:Button ID="btnUpdatePatron" runat="server" Text="Update Patron" OnClick="btnUpdatePatron_Click" />
            <asp:Button ID="btnDeletePatron" runat="server" Text="Remove Patron" OnClick="btnDeletePatron_Click"/>
            <br />
        </div>
    </div>
    <br />
   <br />
    <div class="table">
        <asp:GridView ID="gvPatrons" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvPatrons_SelectedIndexChanged" Width="100%">
            <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:BoundField HeaderText="Patron ID" DataField="PatronID" />
                <asp:BoundField HeaderText="Patron Name" DataField="PatronName" />
                <asp:BoundField HeaderText="Contact Number" DataField="ContactNumber" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
