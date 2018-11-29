<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Loans.aspx.cs" Inherits="CWApp.Website.Loans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label CssClass="alert-success" ID="lblStatus" runat="server" />
    <div class="row" style="padding-left: 0.5em;">
        <div class="col" style="text-align: center; border-style:outset; background-color: lightcyan;">
            <h5>Loans</h5>
            <div>
                Loan ID: <asp:Label ID="lblLoanID" runat="server" />
            </div>
            Patrons: <asp:DropDownList ID="ddlPatrons" runat="server" CssClass="Margin" />
            Workstation: <asp:TextBox ID="txtWorkstation" runat="server" CssClass="Margin" />
            Tools: <asp:DropDownList ID="ddlTools" runat="server" CssClass="Margin" />
            <asp:Button ID="btnNewLoan" runat="server" Text="New Loan" OnClick="btnNewLoan_Click" />
            <asp:Button ID="btnDeleteLoan" runat="server" Text="Delete Loan" OnClick="btnDeleteLoan_Click" />
            <asp:Button ID="btnLoanReturn" runat="server" Text="Return Loan" OnClick="btnLoanReturn_Click" />
            <br />
        </div>
    </div>
    <br />
    <br />
    <div class="table">
        <asp:GridView ID="gvLoans" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvLoans_SelectedIndexChanged" Width="100%">
            <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:BoundField HeaderText="Loan ID" DataField="LoanID" />
                <asp:BoundField HeaderText="Rented By" DataField="PatronName" />
                <asp:BoundField HeaderText="Tool Rented" DataField="ToolType" />
                <asp:BoundField HeaderText="Workstation" DataField="WorkStation" />
                <asp:BoundField HeaderText="Date of Rental" DataField="DateLoaned" />
                <asp:BoundField HeaderText="Date of Return" DataField="DateReturn" />
                <asp:BoundField HeaderText="Authorised By" DataField="StaffName" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
