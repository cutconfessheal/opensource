<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Research.WebApplication.Details" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Products</h1>
        <% var product = GetProduct(); %>    
        <% if(product != null) { %>
             <p><%: product.ProductName%> exists in <%: product.Category.CategoryName %> and supplied by <%: product.Supplier.CompanyName %>.</p>
        <% } else { %>
            <p>Selected product does not exists.</p>
        <% } %>
        <br />
        Loaded from cache: <%: NWIsCached["NWProducts"] %>.
    </div>
    </form>
</body>
</html>
