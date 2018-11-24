<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Loans.aspx.cs" Inherits="CWApp.Website.Loans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-left: 1em;">
        <h3>Loans</h3>
        <div class="alert-danger">
            <asp:Label ID="lblStatus" runat="server" />
        </div>
        <br />
        <asp:GridView ID="gvLoans" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:BoundField HeaderText="Loan ID" DataField="LoanID" />
                <asp:BoundField HeaderText="Rented By" DataField="PatronName" />
                <asp:BoundField HeaderText="Tool Rented" DataField="ToolType" />
                <asp:BoundField HeaderText="Workstation" DataField="WorkStation" />
                <asp:BoundField HeaderText="Date of Rental" DataField="DateLoaned" />
                <asp:BoundField HeaderText="Date of Return" DataField="DateReturn"/>
                <asp:BoundField HeaderText="Authorised By" DataField="StaffName" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
