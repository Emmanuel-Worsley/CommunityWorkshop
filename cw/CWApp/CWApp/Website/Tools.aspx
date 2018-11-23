<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Tools.aspx.cs" Inherits="CWApp.Website.Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-left: 1em;">
        <h3>Tools</h3>
        <br />
        Tool ID: <asp:Label ID="lblToolID" runat="server" /><br />
        Description: <asp:TextBox ID="txtDescription" runat="server" /><br />
        Brand: <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="false" /><br />
        Active: <asp:CheckBox ID="chkActive" runat="server" /><br />
        <asp:Button ID="btnNewTool" runat="server" Text="Add new tool" OnClick="btnNewTool_Click"/>
        <asp:Button ID="btnUpdateTool" runat="server" Text="Update Tool" OnClick="btnUpdateTool_Click" />
        <asp:Button ID="btnDeleteTool" runat="server" Text="Remove Tool" OnClick="btnDeleteTool_Click" />
        <asp:GridView ID="gvToolList" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvToolList_SelectedIndexChanged">
                <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:BoundField HeaderText="Tool ID" DataField="ToolID" />
                <asp:BoundField HeaderText="Description" DataField="Description" />
                <asp:BoundField HeaderText="Brand" DataField="BrandName" />
                <asp:BoundField HeaderText="Active" DataField="Active" />
            </Columns>
        </asp:GridView>
        </div>
</asp:Content>
