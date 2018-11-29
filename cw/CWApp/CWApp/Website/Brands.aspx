<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Brands.aspx.cs" Inherits="CWApp.Website.Brands" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center">
        <asp:Label ID="lblStatus" runat="server" CssClass="alert-success" />
    </div>
    <div class="row" style="padding-left: 0.5em">
        <div class="col" style="text-align: center; border-style:outset; background-color: lightcyan;">
            <h5>Brands</h5>
            Brand ID: <asp:Label ID="lblBrandID" runat="server"/>
            <br />
            Brand Name: <asp:TextBox ID="txtBrandName" runat="server"  CssClass ="Margin" />
            <asp:Button ID="btnNewBrand" runat="server" Text="New Brand" OnClick="btnNewBrand_Click"/>
            <asp:Button ID="btnUpdateBrand" runat="server" Text="Update Brand" OnClick="btnUpdateBrand_Click"/>
            <asp:Button ID="btnDeleteBrand" runat="server" Text="Remove Brand" OnClick="btnDeleteBrand_Click"/>
            <br />
        </div>
    </div>
    <br />
   <br />
    <div class="table">
        <asp:GridView ID="gvBrands" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvBrands_SelectedIndexChanged" Width="100%">
            <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:BoundField HeaderText="Brand ID" DataField="BrandID" />
                <asp:BoundField HeaderText="Brand Name" DataField="BrandName" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
