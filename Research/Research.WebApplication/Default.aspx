<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Research.WebApplication.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Products</h1>
        <ul>
        <% foreach(var entity in NWProducts) { %>
            <li><a href="Details.aspx?id=<%: entity.ProductID %>"><%: entity.ProductName %></a></li>
        <% } %>
        </ul>
        <br />
        Loaded from: <b><%: GetFrom %></b>
    </div>
    </form>
</body>
</html>
