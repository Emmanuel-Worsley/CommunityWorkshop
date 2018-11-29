<%@ Page Title="" Language="C#" MasterPageFile="~/CW.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="CWApp.Website.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function allowDrop(ev) {
            ev.preventDefault();
        }
        function drag(ev) {
            ev.dataTransfer.setData("text", ev.target.id);
        }
        function drop(ev) {
            ev.preventDefault();
            var data = ev.dataTransfer.getData("text");
            ev.target.appendChild(document.getElementById(data));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="display-3">Drag and Drop</h1>
    <div id="div1" ondrop="drop(event)" ondragover="allowDrop(event)" style="width: 100%; height: 10em; border: solid;">
    </div>
    <br />
    <img id="drag1" src="DragNDrop.jpg" draggable="true" ondragstart="drag(event)" width="336" height="69" />
</asp:Content>
