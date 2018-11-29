<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Tools.aspx.cs" Inherits="CWApp.Website.Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center">
        <asp:Label ID="lblStatus" runat="server" CssClass="alert-success" />
    </div>
    <div class="row" style="padding-left: 0.5em">
        <div class="col" style="text-align: center; border-style:outset; background-color: lightcyan;">
            <h5>Tools</h5>
            Tool ID: <asp:Label ID="lblToolID" runat="server"/>
            <br />
            Tool Type: <asp:TextBox ID="txtToolType" runat="server" CssClass="Margin"/>
            Comment: <asp:TextBox ID="txtComment" runat="server" CssClass="Margin"/>
            Brand: <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="false" CssClass="Margin" />
            Active: <asp:CheckBox ID="chkActive" runat="server" CssClass="Margin" />
            <asp:Button ID="btnNewTool" runat="server" Text="Add new tool" OnClick="btnNewTool_Click"/>
            <asp:Button ID="btnUpdateTool" runat="server" Text="Update Tool" OnClick="btnUpdateTool_Click" />
            <asp:Button ID="btnDeleteTool" runat="server" Text="Remove Tool" OnClick="btnDeleteTool_Click" />
        </div>
    </div>
    <br />
    <br />
    <div class="table">
        <asp:GridView ID="gvToolList" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gvToolList_SelectedIndexChanged" Width="100%">
            <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:BoundField HeaderText="Tool ID" DataField="ToolID" />
                <asp:BoundField HeaderText="Tool Type" DataField="ToolType" />
                <asp:BoundField HeaderText="Comment" DataField="Comment" />
                <asp:BoundField HeaderText="Brand" DataField="BrandName" />
                <asp:BoundField HeaderText="Active" DataField="Active" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
