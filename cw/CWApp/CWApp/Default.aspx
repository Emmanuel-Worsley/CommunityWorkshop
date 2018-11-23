<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CWApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Community Workshop: Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-left: 0.5em;">
            <h2 class="display-2"">Community Workshop Login:</h2>
            <p class="display-4";">Please enter your username and password</p>
            <br />
            <br />
            <asp:Login ID="ctlLogin" runat="server" OnAuthenticate="ctlLogin_Authenticate" />
        </div>
    </form>
</body>
</html>
