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
    <style type="text/css">
        @font-face {
            src: url(farsiwebfont.ttf);
            font-family: FarsiWebFont;
        }

        .textbox {
            font-family: FarsiWebFont;
            line-height: normal;
            border-color: steelblue;
            background-color: #39cccc;
            color: black;
        }

        .dropdown1 {
            font-family: FarsiWebFont;
            line-height: normal;
            border-color: steelblue;
            background-color: lawngreen;
            color: black;
        }

        .GridStart {
            direction: rtl;
            text-align: right;
            line-height: normal;
            text-align: right;
            font-family: FarsiWebFont;
            font-size: 14px;
            font-weight: bold;
            border-color: lemonchiffon;
            color: black;
        }

        .GridText {
            line-height: normal;
            font-size: 14px;
            border-color: greenyellow;
            color: black;
            font-family: FarsiWebFont;
        }

        .lbl_alarm {
            font-size: 20px;
            font-weight: bolder;
            background-color: Aqua;
            border-radius: 25px;
            border-color: steelblue;
            text-align: center;
        }

        .lnk {
            color: #003300;
            font-size: 22px;
        }
            .lnk:hover {
                color: White;
            }
            a{
                cursor: pointer
            }
    </style>
</head>
<body class="skin-blue">
    <form id="form1" runat="server">
        <div class="body1">
<header class="header">
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
                                    <i class="fa fa-laptop"></i><span>صفحه من</span>
                                </a>
                            </li>
                            <li class="treeview">
                                <a href="#">
                                    <i class="fa fa-table"></i>
                                    <span>اطلاعات پایه</span>
                                    <i class="fa fa-angle-left pull-right"></i>
                                </a>
                                <ul class="treeview-menu">
                                    <li>
                                        <asp:LinkButton ID="lnk_personal" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Personel_Managment.aspx">پرسنل</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_brands" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Brands.aspx">گونه فرش</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_colors" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Colors.aspx">رنگ</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_sizes" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Sizes.aspx">اندازه</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_carpet" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Carpet.aspx">نوع فرش</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_plan" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/plan.aspx">نقشه فرش</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_porz" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Sizes.aspx"> پرز</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_chele" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/Porz.aspx"> چله</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_bank" runat="server" Enabled="false" PostBackUrl="~/BaseInformation/BankAccount.aspx">حساب های بانکی</asp:LinkButton></li>
                                </ul>
                            </li>
                            <li class="treeview">
                                <a href="#">
                                    <i class="fa fa-th"></i>
                                    <span>عملیات خرید و فروش</span>
                                    <i class="fa fa-angle-left pull-right"></i>
                                </a>
                                <ul class="treeview-menu">
                                    <li>
                                        <asp:LinkButton ID="lnk_import" runat="server" PostBackUrl="~/Provider_Goods/ProductAssign.aspx">ورود فرش</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_supcust" runat="server" Enabled="false" PostBackUrl="~/Supcust/SupcustManagers.aspx">مدیریت مشتری</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_firm" runat="server" Enabled="false" PostBackUrl="~/Provider_Goods/Provider_Managment.aspx">مدیریت تامین کننده</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_Sale_price" runat="server" Enabled="false" PostBackUrl="~/Provider_Goods/Pricing.aspx">قیمت گذاری</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_project" runat="server" Enabled="false" PostBackUrl="~/Project/ProjectManagment.aspx">مدیریت نمایشگاه</asp:LinkButton></li>

                                    <li>
                                        <asp:LinkButton ID="lnk_factor" runat="server" Enabled="false" PostBackUrl="~/Supcust/FactorsManagment.aspx">مدیریت فاکتورها</asp:LinkButton></li>
<li>
                                        <asp:LinkButton ID="lnk_remain_carpet" runat="server" Enabled="false" PostBackUrl="~/Project/CallPerform.aspx">مدیریت فراخوان</asp:LinkButton></li>
                                </ul>
                            </li>
                            <li class="treeview">
                                <a href="#">
                                    <i class="fa fa-table"></i><span>گزارش ها</span>
                                    <i class="fa fa-angle-left pull-right"></i>
                                </a>
                                <ul class="treeview-menu">
                                    <li>
                                        <asp:LinkButton ID="lnk_sale" runat="server" Enabled="false" PostBackUrl="~/Sale_Report.aspx">تحلیل مالی</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_reject" runat="server" Enabled="false" PostBackUrl="~/Project/CallBackReport.aspx">لیست تحویل</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_main_callback" runat="server" Enabled="false" PostBackUrl="~/Project/MainCallBack.aspx">لیست فراخوان</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_complete" runat="server" Enabled="false" PostBackUrl="~/Provider_Goods/Complete.aspx">تکمیل اطلاعات</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_settelment" runat="server" Enabled="true" PostBackUrl="~/Project/Settelment.aspx">لیست تسویه حساب</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_all_goods" runat="server" Enabled="false" PostBackUrl="~/Archives/AllReports.aspx?snd=AllProviderGoods">لیست فرش ها</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_supcustReport" runat="server" Enabled="false" PostBackUrl="~/SupcustReport.aspx">گزارش مشتریان</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_supcust_buyers" runat="server" Enabled="false" PostBackUrl="~/Archives/AllReports.aspx?snd=Buyers">گزارش خریداران</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_supcust_excel" runat="server" Enabled="true" OnClick="lnkExcel_supcust_Click">اکسل خریدارن</asp:LinkButton></li>
                                    <%--<li>
                                        <asp:LinkButton ID="lnk_supcust_audience" runat="server" Enabled="false" PostBackUrl="~/Archives/AllReports.aspx?snd=Audience">لیست مخاطبین</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_audience_excel" runat="server" Enabled="false" OnClick="lnkExcel_audience_Click">اکسل مخاطبین</asp:LinkButton></li>--%>
<%--                                    <li>
                                        <asp:LinkButton ID="lnk_supcust_report" runat="server" Enabled="false" PostBackUrl="~/Archives/AllReports.aspx?snd=supcust_list">لیست تمام مشتریان</asp:LinkButton></li>--%>

                                    <li>
                                        <asp:LinkButton ID="lnk_customers" runat="server" Enabled="false" PostBackUrl="~/Supcust/CustomersFactors.aspx">فاکتور مشتریان</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_prices" runat="server" OnClick="lnkExcel_Click">لیست قیمت ها</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnk_Survey" runat="server" Enabled="false" PostBackUrl="~/Project/CallBackReportCouples.aspx">بررسی نمایشگاه</asp:LinkButton></li>
                                </ul>
                            </li>
                        </ul>
                    </section>
                </aside>
                <aside class="right-side">
                    <section class="content">
                        <div class="row">
                            <div class="col-lg-3 col-xs-6">
                        <a id="box_supcust" runat="server" href="~/Provider_Goods/Pricing.aspx" class="small-box-footer" >
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <h3 style="text-align:center">
                                        <sup style="font-size: 26px">قیمت گذاری</sup>
                                    </h3>
                                    <p>
                                        <br />
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion-android-note"></i>
                                </div>
                                <i class="fa fa-arrow-circle-right"></i>&nbsp;&nbsp;ادامه&nbsp;&nbsp;
                            </div>
                                </a>
                            </div>
                            <div class="col-lg-3 col-xs-6">
                                <a id="box_provider" runat="server" href="~/Project/MainCallBack.aspx" class="small-box-footer">
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h3 style="text-align:center">
                                        <sup style="font-size: 26px">لیست فرخوان</sup>
                                    </h3>
                                    <p>
                                        <br />                                        
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion-briefcase"></i>
                                </div>
                            <i class="fa fa-arrow-circle-right"></i>&nbsp;&nbsp;ادامه&nbsp;&nbsp;
                            </div></a>
                            </div>
                            <div class="col-lg-3 col-xs-6">
                                <a id="box_opportunity" runat="server" href="~/Project/Settelment.aspx" class="small-box-footer">
                            <div class="small-box bg-yellow-gradient">
                                <div class="inner">
                                    <h3 style="text-align:center">
                                        <sup style="font-size: 26px">لیست تحویل</sup>
                                    </h3>
                                    <p>
                                        <br />                                        
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-stats-bars"></i>
                                </div>
                                <i class="fa fa-arrow-circle-right"></i>&nbsp;&nbsp;ادامه&nbsp;&nbsp;
                            </div></a>
                            </div>
                            <div class="col-lg-3 col-xs-6">
                                <a id="box_alarm" runat="server" href="~/Project/ProjectManagment.aspx" class="small-box-footer">
                            <div class="small-box bg-red-gradient">
                                <div class="inner">
                                    <h3 style="text-align:center">
                                        <sup style="font-size: 26px">لیست تسویه حساب</sup>
                                    </h3>
                                    <p>
                                        <br />                                        
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <i class="fa fa-arrow-circle-right"></i>&nbsp;&nbsp;ادامه&nbsp;&nbsp;
                            </div></a>
                            </div>
                        </div>

                    </section>
                </aside>
                <!-- /.right-side -->
            </div>
            <!-- ./wrapper -->
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
