<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllReports.aspx.cs" Inherits="WebApplicationKartable.AllReports" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>گزارش</title>
    <link rel="alternate" type="application/rss+xml" title="RSS 2.0"/>
	<link rel="stylesheet" type="text/css" href="../css/StyleSheet1.css" />
	<script type="text/javascript" src="../Script/JavaScript3.js"></script>
	<script type="text/javascript" src="../Script/JavaScript4.js"></script>
	<script type="text/javascript" class="init">
	    $(document).ready(function () {
	        $('#example').dataTable({
	            "pagingType": "full_numbers"
	        });
	    });
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;"><h2><asp:Label ID="lbl_header" runat="server"></asp:Label></h2></div>
        <div style="text-align:center;"><h2><asp:Label ID="lbl" runat="server" ForeColor="Maroon"></asp:Label></h2></div>
    <div style="direction:rtl;">
    <asp:Literal ID="literal_report" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
