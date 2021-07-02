<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CallPerform.aspx.cs" Inherits="WebApplicationKartable.CallPerform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        .loading
        {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
        .modalBackground
        {
            background-color: black;
        }
        .modalPopup
        {
            background-color:whitesmoke;
            border:3px solid #ccc;
            padding:10px;
            height: 510px;
            width:610px;
            font-size:15px
        }
        .loading
        {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
</style>
        <link id="Link1" href="../Assets/default.css" rel="stylesheet" type="text/css" media="screen"
        runat="server" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        a,th{font-size:13px;font-weight:normal}
    </style>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_code FROM dbo.bas_project order by srl desc"/>
    <asp:SqlDataSource ID="source_provider" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, provider_name FROM dbo.bas_provider order by provider_name"/>
    <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>
        
    <div class="row">
        <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
        <asp:LinkButton ID="lnk_delete" CssClass="btn btn-primary" runat="server" Enabled="true" PostBackUrl="~/Project/CallBackEdit.aspx">حذف تخصیص از نمایشگاه</asp:LinkButton>
    </div>
    <table>
        <tr>
        <td>انتخاب نمایشگاه :</td>
        <td><asp:DropDownList ID="lst_project" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_project" DataTextField="project_code" DataValueField="srl"></asp:DropDownList></td>
        <td>تامین کننده :</td>
        <td><asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_provider" DataTextField="provider_name" DataValueField="srl"></asp:DropDownList></td>
        <td><asp:ImageButton ID="btn_report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="btn_report_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>
        </tr>
        <tr>
            <td>تعداد فرش ها :</td>
            <td><asp:TextBox ID="txt_count" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true"></asp:TextBox></td>
            <td><asp:Button ID="btn_assign" runat="server" CssClass="btn-facebook" Text="تخصیص به نمایشگاه" OnClick="btn_assign_Click"/></td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td colspan="2">
            <asp:UpdatePanel ID="upnlOutstanding" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <br />
                <asp:Button ID="lbRemoveFilterOutstanding" runat="server" CssClass="btn-facebook" Text="حذف فیلتر" OnClick="lbRemoveFilterOutstanding_Click"/>
            <asp:GridView ID="grdViewOutstanding" runat="server" AutoGenerateColumns="False"
                BackColor="#39cccc" BorderColor="#999999" BorderStyle="Solid" CellPadding="3" ForeColor="Black"
                GridLines="Both" CellSpacing="1" EmptyDataText="جستجو ناموفق" AllowPaging="true" PageIndex="20"
                CssClass="Grid" AllowSorting="true" OnPageIndexChanging="grdViewOutstanding_PageIndexChanging"
                OnRowDataBound="grdViewOutstanding_RowDataBound" OnSorting="grdViewOutstanding_Sorting">
                <FooterStyle BackColor="#CCCCCC" />
                <Columns>
            <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="60" />
            <asp:TemplateField SortExpression="code_igd" ItemStyle-Width="70">
                <HeaderTemplate>
                    <asp:LinkButton ID="lbcode_igd" runat="server" Text="کدفرش" CommandName="Sort" CommandArgument="code_igd" Width="50"></asp:LinkButton>
                    <br />
                    <asp:TextBox runat="server" ID="txtcode_igd" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("code_igd") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="size_title" ItemStyle-Width="70">
                <HeaderTemplate>
                    <asp:LinkButton ID="lbsize_title" runat="server" Text="اندازه" CommandName="Sort" CommandArgument="size_title" Width="50"></asp:LinkButton>
                    <br />
                    <asp:TextBox runat="server" ID="txtsize_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("size_title") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="carpet_title" ItemStyle-Width="70">
                <HeaderTemplate>
                    <asp:LinkButton ID="lbcarpet_title" runat="server" Text="نوع" CommandName="Sort" CommandArgument="carpet_title" Width="50"></asp:LinkButton>
                    <br />
                    <asp:TextBox runat="server" ID="txtcarpet_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("carpet_title") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="brand_name" ItemStyle-Width="70">
                <HeaderTemplate>
                    <asp:LinkButton ID="lbbrand_name" runat="server" Text="گونه" CommandName="Sort" CommandArgument="brand_name" Width="50"></asp:LinkButton>
                    <br />
                    <asp:TextBox runat="server" ID="txtbrand_name" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("brand_name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="buy_price" ItemStyle-Width="70">
                <HeaderTemplate>
                    <asp:LinkButton ID="lbbuy_price" runat="server" Text="خرید" CommandName="Sort" CommandArgument="buy_price" Width="50"></asp:LinkButton>
                    <br />
            <asp:TextBox runat="server" ID="txtbuy_price" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("buy_price") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="sale_price" ItemStyle-Width="70">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lbsale_price" runat="server" Text="فروش" CommandName="Sort" CommandArgument="sale_price" Width="50"></asp:LinkButton>
                        <br />
                <asp:TextBox runat="server" ID="txtsale_price" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#Eval("sale_price") %>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="u_date_time" ItemStyle-Width="70">
                <HeaderTemplate>
                <asp:LinkButton ID="lbu_date_time" runat="server" Text="تاریخ" CommandName="Sort" CommandArgument="u_date_time" Width="50"></asp:LinkButton>
                        <br />
                <asp:TextBox runat="server" ID="txtu_date_time" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#Eval("u_date_time") %>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="margin_profit" ItemStyle-Width="70">
                <HeaderTemplate>
                <asp:LinkButton ID="lbmargin_profit" runat="server" Text="حاشیه سود" CommandName="Sort" CommandArgument="margin_profit" Width="50"></asp:LinkButton>
                    <br />
                <asp:TextBox runat="server" ID="txtumargin_profit" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("margin_profit") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="percent_profit" ItemStyle-Width="70">
                <HeaderTemplate>
                <asp:LinkButton ID="lbpercent_profit" runat="server" Text="درصد سود" CommandName="Sort" CommandArgument="percent_profit" Width="50"></asp:LinkButton>
                        <br />
                <asp:TextBox runat="server" ID="txtupercent_profit" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#Eval("percent_profit") %>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:ImageField DataImageUrlField="title_igd" HeaderText="تصویر" ItemStyle-Width="300"></asp:ImageField> 
            <asp:TemplateField HeaderText="تائید نهایی">
                <ItemTemplate>
                    <asp:CheckBox ID="confirm" runat="server"  Width="100"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" CssClass="pgr" />
        <SelectedRowStyle BackColor="DarkOliveGreen" Font-Bold="True" ForeColor="White" />
        <HeaderStyle  Font-Bold="True" ForeColor="Black" Font-Size="13"/>
        <AlternatingRowStyle BackColor="Azure" />
        </asp:GridView>
        </ContentTemplate>
        </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
        <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
        function Confirm() {
            $("[data-toggle='offcanvas']").click(function (e) {
                e.preventDefault();

                //If window is small enough, enable sidebar push menu
                if ($(window).width() <= 992) {
                    $('.row-offcanvas').toggleClass('active');
                    $('.left-side').removeClass("collapse-left");
                    $(".right-side").removeClass("strech");
                    $('.row-offcanvas').toggleClass("relative");
                } else {
                    //Else, enable content streching
                    $('.left-side').toggleClass("collapse-left");
                    $(".right-side").toggleClass("strech");
                }
            });
        }
    </script>
</asp:Content>
