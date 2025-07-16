<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authorized.aspx.cs" Inherits="Auth0DomainAndClientOnly.Authorized" %>

<!DOCTYPE html>
<html>
<head>
    <title>Authorized Page</title>
    <!-- Bootstrap CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">

    <div class="container mt-5">
        <div class="card shadow p-4">
            <h2 class="text-success mb-4">Welcome to the Authorized Page</h2>
            
            <p class="lead">
                You are logged in as: <strong><%= Context.User.Identity.Name %></strong>
            </p>

            <div class="mt-4">
                <a href="Default.aspx" class="btn btn-primary me-2">Go to Home Page</a>
                <a href="Logout.aspx" class="btn btn-danger">Logout</a>
            </div>
        </div>
    </div>

</body>
</html>
