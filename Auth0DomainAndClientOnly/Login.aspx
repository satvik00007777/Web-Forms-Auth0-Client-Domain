<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Auth0DomainAndClientOnly.Login" %>

<!DOCTYPE html>
<html>
<head>
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Login with Email</h2>
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Enter your email" />
            <br /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <br /><br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
