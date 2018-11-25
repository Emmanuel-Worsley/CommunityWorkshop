<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Loans.aspx.cs" Inherits="CWApp.Website.Loans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-left: 1em;">
        <h3>Loans</h3>
        <div>
            <asp:Label CssClass="alert-success" ID="lblStatus" runat="server" />
        </div>
        <br />
        Loan ID: <asp:Label ID="lblLoanID" runat="server" />
        <br/>
        Patrons: <asp:DropDownList ID="ddlPatrons" runat="server" />
        <br />
        Workstation: <asp:TextBox ID="txtWorkstation" runat="server" />
        <br />
        Tools: <asp:DropDownList ID="ddlTools" runat="server" />
        <br />
        <asp:Button ID="btnNewLoan" runat="server" Text="New Loan" OnClick="btnNewLoan_Click" />
        <asp:Button ID="btnDeleteLoan" runat="server" Text="Delete Loan" OnClick="btnDeleteLoan_Click" />
        <asp:Button ID="btnLoanReturn" runat="server" Text="Return Loan" OnClick="btnLoanReturn_Click" />
        <br />
        <br />
        <asp:GridView ID="gvLoans" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvLoans_SelectedIndexChanged">
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
