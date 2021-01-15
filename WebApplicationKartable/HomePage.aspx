<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebApplicationKartable.HomePage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>بانک فرش بوم</title>
         <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
        <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link href="css/ionicons.min.css" rel="stylesheet" type="text/css" />
        <link href="css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/component.css" />
    <style>
        td{font-size:15px}
                .loading
        {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
    </style>
        <style type="text/css">
            @font-face {    
    src: url(farsiwebfont.ttf);
    font-family: FarsiWebFont;
}
.textbox {
             font-family:FarsiWebFont;
            line-height:normal;
            border-color:steelblue;
            background-color:#39cccc;
            color:black;
    }
.GridText {
            line-height:normal;
	        font-size: 14px;
            border-color:lemonchiffon;
            color:black;
    }
.lbl_alarm{
    font-size: 20px;
    font-weight:bolder;
    background-color:Aqua;
    border-radius: 35px;
    border-color:steelblue;
    text-align: center;  
}
.lnk{color:#003300;
     font-size: 22px;     
}
.lnk:hover {
                color:White;
            }
.dropdown1 {
            font-family:FarsiWebFont;
            line-height:normal;
            border-color:steelblue;
            background-color:lawngreen;
            color:black;
    }
    </style>
</head>
<body class="skin-blue" >
    <form id="form1" runat="server">
        <div class="body1">
        <header class="header">
            <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
            <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>
            <cc1:AutoCompleteExtender ServiceMethod="FilterSearch" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtContactsSearch"
            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
            OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
        </cc1:AutoCompleteExtender>
<cc1:AutoCompleteExtender ServiceMethod="FilterSearch2" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_product"
            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
            OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
    </cc1:AutoCompleteExtender>
            <a href="../HomePage.aspx" class="logo">
                <span>بانک فرش بوم</span>
            </a>
            <nav class="navbar navbar-static-top" role="navigation">
                <a href="#" class="navbar-btn sidebar-toggle" data-toggle="offcanvas" role="button">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </a>
                <div class="navbar-right">
                    <ul class="nav navbar-nav">
                        <li class="dropdown notifications-menu">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                <i class="fa fa-warning"></i>
                                <span class="label label-warning"><asp:Literal ID="literal_alarm_count" runat="server"></asp:Literal></span>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="header">سر رسید آلارم ها</li>
                                <li>
                                    <ul class="menu">
                                        <asp:Literal ID="literal_alarm" runat="server"></asp:Literal>
                                    </ul>
                                </li>
                                <li class="footer"><a href="Alarm.aspx">نمایش لیست</a></li>
                            </ul>
                        </li>
                        <li class="dropdown user user-menu">
                            <asp:Literal ID="literal_personal" runat="server"></asp:Literal>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>
        <div class="wrapper row-offcanvas row-offcanvas-left">
            <aside class="left-side sidebar-offcanvas">
                <section class="sidebar">
                    <div class="user-panel">
                        <asp:Literal runat="server" ID="literal_online"></asp:Literal>
                    </div>
                    <ul class="sidebar-menu">
                        <li class="active">
                            <a href="#">
                                <i class="fa fa-dashboard"></i><span></span>
                            </a>
                        </li>
                        <li>
                            <a href="../HomePage.aspx">
                                <i class="fa fa-laptop"></i> <span>صفحه من</span>
                            </a>
                        </li>
                        <li class="treeview">
                            <a href="#">
                                <i class="fa fa-table"></i>
                                <span>اطلاعات پایه</span>
                                <i class="fa fa-angle-left pull-right"></i>
                            </a>
                            <ul class="treeview-menu">
    <li><asp:LinkButton ID="lnk_personal" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Personel_Managment.aspx" >پرسنل</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_brands" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Brands.aspx" >گونه فرش</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_cities" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Cities.aspx" >شهر</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_colors" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Colors.aspx" >رنگ</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_sizes" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Sizes.aspx" >اندازه</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_carpet" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Carpet.aspx" >نوع فرش</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_plan" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/plan.aspx" >نقشه فرش</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_locate" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/location.aspx" >محل برگزاری</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_porz" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Sizes.aspx" > پرز</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_chele" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Porz.aspx" > چله</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_clue" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Chele.aspx" >سطح ارتباط </asp:LinkButton></li> 
    <li><asp:LinkButton ID="lnk_raj" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Raj.aspx" >رج شمار</asp:LinkButton></li>      
    <li><asp:LinkButton ID="lnk_bank" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/BankAccount.aspx" >حساب های بانکی</asp:LinkButton></li>    
                            </ul>
                        </li>
                        <li class="treeview">
                            <a href="#">
                                <i class="fa fa-th"></i>
                                <span>عملیات خرید و فروش</span>
                                <i class="fa fa-angle-left pull-right"></i>
                            </a>
                            <ul class="treeview-menu">  
   <li><asp:LinkButton ID="lnk_import" runat="server" PostBackUrl="~/Provider_Goods/ProductAssign.aspx" >ورود فرش</asp:LinkButton></li>                              
    <li><asp:LinkButton ID="lnk_supcust" runat="server" Enabled="false" PostBackUrl="~/Supcust/SupcustManagers.aspx" >مدیریت مشتری</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_firm" runat="server" Enabled="false" PostBackUrl="~/Provider_Goods/Provider_Managment.aspx" >مدیریت تامین کننده</asp:LinkButton></li>  
    <li><asp:LinkButton ID="lnk_Sale_price" runat="server" Enabled="false" PostBackUrl="~/Provider_Goods/Pricing.aspx" >قیمت گذاری</asp:LinkButton></li>  
    <li><asp:LinkButton ID="lnk_project" runat="server" Enabled="false" PostBackUrl="~/Project/ProjectManagment.aspx" >مدیریت نمایشگاه</asp:LinkButton></li>  
    <li><asp:LinkButton ID="lnk_opportunity_sale" runat="server" Enabled="false" PostBackUrl="~/Project/Opportunity_Managment.aspx" >انتقال فرصت فروش</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_factor" runat="server" Enabled="false" PostBackUrl="~/Supcust/FactorsManagment.aspx" >مدیریت فاکتورها</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_alarm" runat="server" Enabled="false" PostBackUrl="~/Alarm.aspx" >سیستم آلارم</asp:LinkButton></li>
    <li><asp:LinkButton ID="lnk_acceptgoods" runat="server" PostBackUrl="~/Provider_Goods/AcceptGoods.aspx" >پذیرش فرش</asp:LinkButton></li>
                            </ul>
                        </li>
                        <li class="treeview">
                            <a href="#">
                                <i class="fa fa-table"></i> <span>گزارش ها</span>
                                <i class="fa fa-angle-left pull-right"></i>
                            </a>
                            <ul class="treeview-menu">
                            <li><asp:LinkButton ID="lnk_sale" runat="server" Enabled="false" PostBackUrl="~/Sale_Report.aspx" >تحلیل مالی</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnk_reject" runat="server" Enabled="false" PostBackUrl="~/Project/CallBackReport.aspx" >لیست تحویل</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnk_main_callback" runat="server" Enabled="false" PostBackUrl="~/Project/MainCallBack.aspx" >لیست فراخوان</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnk_label" runat="server" Enabled="false" PostBackUrl="~/Project/LabelGenerator.aspx" >چاپ برچسب</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnk_complete" runat="server" Enabled="false" PostBackUrl="~/Provider_Goods/Complete.aspx" >تکمیل اطلاعات</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnk_settelment" runat="server" Enabled="false" PostBackUrl="~/Project/Settelment.aspx" >لیست تسویه حساب</asp:LinkButton></li>     
                            <li><asp:LinkButton ID="lnk_supcust_excel" runat="server" Enabled="false" PostBackUrl="~/Supcust/SupcustLevel_Export.aspx" >سطح ارتباط</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnk_all_goods" runat="server" Enabled="false" PostBackUrl="~/Archives/AllReports.aspx?snd=AllProviderGoods" >لیست فرش ها</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnk_supcust_report" runat="server" Enabled="false" PostBackUrl="~/Archives/AllReports.aspx?snd=supcust_list" >گزارش مشتری</asp:LinkButton></li>   
                           <li><asp:LinkButton ID="lnk_remain_carpet" runat="server" Enabled="false" PostBackUrl="~/Project/CallPerform.aspx" >مدیریت فراخوان</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lnk_factorreport" runat="server" Enabled="false" PostBackUrl="~/Supcust/RejectFactor.aspx" >گزارش مرجوعی</asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnk_customers" runat="server" Enabled="false" PostBackUrl="~/Supcust/CustomersFactors.aspx" >فاکتور مشتریان</asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnk_barcode_report" runat="server" Enabled="false" PostBackUrl="~/Project/ListOfBarcodes.aspx" >ورود بارکد</asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnk_Survey" runat="server" Enabled="false" PostBackUrl="~/Project/CallBackReportCouples.aspx" >بررسی نمایشگاه</asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkExcel" runat="server" Enabled="true" OnClick="lnkExcel_Click"  >خروجی اکسل</asp:LinkButton></li>
                            </ul>
                        </li>
                    </ul>
                </section>
            </aside>
            <aside class="right-side">
                <section class="content">
                    <div class="row">
                        <div class="col-lg-3 col-xs-6">
                            <asp:LinkButton ID="box_supcust" runat="server" Enabled="false" PostBackUrl="~/Supcust/Supcust.aspx?srl=-1" CssClass="small-box-footer">
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <h3 style="text-align:center">
                                        <sup style="font-size: 26px">مشتری جدید</sup>
                                    </h3>
                                    <p>
                                        <br />
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion-android-note"></i>
                                </div>
                                <i class="fa fa-arrow-circle-right"></i>&nbsp;&nbsp;ادامه&nbsp;&nbsp;
                            </div></asp:LinkButton>
                        </div>
                        <div class="col-lg-3 col-xs-6">
                            <asp:LinkButton ID="box_provider" runat="server" Enabled="false" PostBackUrl="~/Provider_Goods/ProductAssign.aspx" CssClass="small-box-footer">
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h3 style="text-align:center">
                                        <sup style="font-size: 26px">ورود فرش</sup>
                                    </h3>
                                    <p>
                                        <br />                                        
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion-briefcase"></i>
                                </div>
                            <i class="fa fa-arrow-circle-right"></i>&nbsp;&nbsp;ادامه&nbsp;&nbsp;
                            </div></asp:LinkButton>
                        </div>
                        <div class="col-lg-3 col-xs-6">
                            <asp:LinkButton ID="box_opportunity" runat="server" Enabled="false" PostBackUrl="~/Supcust/FactorsManagment.aspx" CssClass="small-box-footer">
                            <div class="small-box bg-yellow-gradient">
                                <div class="inner">
                                    <h3 style="text-align:center">
                                        <sup style="font-size: 26px">فاکتورها</sup>
                                    </h3>
                                    <p>
                                        <br />                                        
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-stats-bars"></i>
                                </div>
                                <i class="fa fa-arrow-circle-right"></i>&nbsp;&nbsp;ادامه&nbsp;&nbsp;
                            </div></asp:LinkButton>
                        </div>
                        <div class="col-lg-3 col-xs-6">
                            <asp:LinkButton ID="box_alarm" runat="server" Enabled="false" PostBackUrl="~/Project/ProjectManagment.aspx" CssClass="small-box-footer">
                            <div class="small-box bg-red-gradient">
                                <div class="inner">
                                    <h3 style="text-align:center">
                                        <sup style="font-size: 26px">مدیریت نمایشگاه</sup>
                                    </h3>
                                    <p>
                                        <br />                                        
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <i class="fa fa-arrow-circle-right"></i>&nbsp;&nbsp;ادامه&nbsp;&nbsp;
                            </div></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
            <div style="text-align:center;"><h2>نمایشگاه های فعال</h2></div>
                <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
            <div class="component">
<asp:SqlDataSource ID="source_size" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, size_title FROM dbo.inv_size"/>
<asp:SqlDataSource ID="source_brand" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, brand_name FROM dbo.inv_brand"/>
    <asp:Panel ID="Panel_grid" runat="server" CssClass="panelbackcolor">
    <table>
        <tr>
            <td>نام تامین کننده :</td>
            <td><asp:TextBox ID="txtContactsSearch" runat="server" Width="300"  CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
            <td>کد فرش :</td>
            <td><asp:TextBox ID="txt_product" runat="server" Width="300"  CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
            <td>گونه :</td>
            <td><asp:DropDownList ID="lst_city2" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>اندازه :</td>
            <td><asp:DropDownList ID="lst_size" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td><asp:Button ID="btn_filter" runat="server" CssClass="btn btn-primary" Text="فیلتر" OnClick="btn_filter_Click" /></td>
        </tr>
    </table>
</asp:Panel>
    <asp:GridView ID="gridview" runat="server" CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" onselectedindexchanged="gridview_SelectedIndexChanged" AllowPaging="true" PageIndex="15">
    <Columns>                      
    <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="70" ReadOnly="True"/>              
    <asp:BoundField DataField="u_date_time" HeaderText="تاریخ" ItemStyle-Width="100" ReadOnly="True"/> 
    <asp:BoundField DataField="full_name" HeaderText="مشتری" ItemStyle-Width="200" ReadOnly="True"/>
    <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="200" ReadOnly="True"/>   
    <asp:BoundField DataField="provider_name" HeaderText="نام تامین کننده" ItemStyle-Width="200" ReadOnly="True"/>
    <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="70" ReadOnly="True"/>       
    <asp:BoundField DataField="meet_title" HeaderText="فرصت فروش" ItemStyle-Width="150" ReadOnly="True"/>    
    <asp:CommandField ShowSelectButton ="true" HeaderText="فاکتور" SelectText="فاکتور" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
        </Columns>
         <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />     
    </asp:GridView>
        </div>   
    </div>
    </section>
    </aside><!-- /.right-side -->
        </div><!-- ./wrapper -->
        <script src="../js/Ajax_Code.js" type="text/javascript"></script>
        <script src="../js/bootstrap.min.js" type="text/javascript"></script>
        <script src="../js/AdminLTE/app.js" type="text/javascript"></script>
        <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>
</div>
    </form>
</body>
</html>
