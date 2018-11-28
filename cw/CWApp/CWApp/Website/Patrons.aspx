<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Patrons.aspx.cs" Inherits="CWApp.Website.Patrons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-left: 1em;">
        <h3>Patrons</h3>
        <div class="alert-success">
            <asp:label ID="lblStatus" runat="server" />
        </div>
        <br />
        Patron ID: <asp:Label ID="lblPatronID" runat="server"/><br />
        Patron Name: <asp:TextBox ID="txtPatronName" runat="server" /><br />
        <div style="padding-top: 0.3em">
        Contact Number: <asp:TextBox ID="txtContactNumber" runat="server" /> <br/>
        </div>
        <div style="padding-top: 0.3em">
        <asp:Button ID="btnNewPatron" runat="server" Text="New Patron" OnClick="btnNewPatron_Click"/>
        <asp:Button ID="btnUpdatePatron" runat="server" Text="Update Patron" OnClick="btnUpdatePatron_Click" />
        <asp:Button ID="btnDeletePatron" runat="server" Text="Remove Patron" OnClick="btnDeletePatron_Click" />
        <asp:GridView ID="gvPatrons" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvPatrons_SelectedIndexChanged">
                <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:BoundField HeaderText="Patron ID" DataField="PatronID" />
                <asp:BoundField HeaderText="Patron Name" DataField="PatronName" />
                <asp:BoundField HeaderText="Contact Number" DataField="ContactNumber" />
            </Columns>
        </asp:GridView>
        </div>
        </div>
</asp:Content>
