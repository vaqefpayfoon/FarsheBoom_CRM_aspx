<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Pricing.aspx.cs" Inherits="WebApplicationKartable.Pricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .loading {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }

        .modalBackground {
            background-color: black;
        }

        .modalPopup {
            background-color: whitesmoke;
            border: 3px solid #ccc;
            padding: 10px;
            height: 510px;
            width: 610px;
            font-size: 15px
        }

        .loading {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
    </style>
    <link id="Link1" href="../Assets/default.css" rel="stylesheet" type="text/css" media="screen"
        runat="server" />
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        a, th {
            font-size: 13px;
            font-weight: normal
        }
    </style>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"
        ValidationGroup="RegisterUserValidationGroup" />
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red"
        ValidationGroup="RegisterUserValidationGroup2" />
    <asp:SqlDataSource ID="source_provider" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, provider_name FROM dbo.bas_provider order by provider_name" />
    <asp:SqlDataSource ID="source_brand" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, brand_name FROM dbo.inv_brand" />
    <asp:SqlDataSource ID="source_color" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, color_name FROM dbo.inv_color" />
    <asp:SqlDataSource ID="source_size" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, size_title FROM dbo.inv_size" />
    <asp:SqlDataSource ID="source_porz" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, porz_title FROM dbo.inv_porz" />
    <asp:SqlDataSource ID="source_chele" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, chele_title FROM dbo.inv_chele" />
    <asp:SqlDataSource ID="source_plan" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, plan_title FROM dbo.inv_plan" />
    <asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_code FROM dbo.bas_project order by srl desc" />
    <table>
        <tr>
            <td>تامین کننده :</td>
            <td>
                <asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_provider" DataTextField="provider_name" DataValueField="srl"></asp:DropDownList></td>
            <td>
                <asp:Button ID="btn_without_price" runat="server" CssClass="btn-facebook" OnClick="btn_without_price_Click" Text="بدون قیمت ها" /></td>
            <td>
                <asp:Button ID="btn_with_price" runat="server" CssClass="btn-facebook" OnClick="btn_with_price_Click" Text="تمام فرش ها" /></td>
            <td>
                <asp:Button ID="btn_current_event" runat="server" CssClass="btn-facebook" OnClick="btn_current_event_Click" Text="فرش های تخصیص یافته نمایشگاه جاری" /></td>
        </tr>
    </table>
    <table>
                <tr style="border-color: navy; border-width: 2px;">
            <td>
                <asp:DropDownList ID="lst_search_brand" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
            <td>
                <asp:Button ID="btn_search_brand" runat="server" CssClass="btn-facebook" Text="جستجو بین تمام گونه ها" OnClick="btn_search_brand_Click" />
            </td>            
            <td>
            <asp:DropDownList ID="lst_search_size" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
            <td>
                <asp:Button ID="btn_search_size" runat="server" CssClass="btn-facebook" Text="جستجو بین تمام اندازه ها" OnClick="btn_search_size_Click" />
            </td>
            <td>
                <asp:Button ID="btn_load_without_price" runat="server" CssClass="btn-facebook" Text="تمام بدون قیمت ها" OnClick="btn_load_without_price_Click" /></td>
            <td>
                <asp:Button ID="btn_cache_all_data" runat="server" CssClass="btn-facebook" Text="تمام فرش ها" OnClick="btn_cache_all_data_Click" /></td>
        </tr>
    </table>
    <br style="border-color: navy; border-width: 2px;" />
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>تصویر فرش :</td>
                        <td>
                            <asp:FileUpload ID="upload" runat="server" Width="150px" /></td>
                        <td>مساحت :</td>
                        <td>
                            <asp:TextBox ID="txt_area" runat="server" CssClass="textbox" Width="150px" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>کد فرش بوم :</td>
                        <td>
                            <asp:TextBox ID="txt_code" runat="server" CssClass="textbox" Width="150px" ReadOnly="true" MaxLength="100"></asp:TextBox><asp:Label ID="Label1" Text="*" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator Display="None" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_code" ErrorMessage="کد فرش اجباریست" ToolTip="کد فرش  اجباریست" ValidationGroup="RegisterUserValidationGroup2"></asp:RequiredFieldValidator>
                        </td>
                        <td>کد تامین کننده :</td>
                        <td>
                            <asp:TextBox ID="txt_pcode" runat="server" CssClass="textbox" Width="150px" MaxLength="100"></asp:TextBox><asp:Label ID="Label2" Text="*" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator Display="None" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_code" ErrorMessage="کد فرش اجباریست" ToolTip="کد فرش  اجباریست" ValidationGroup="RegisterUserValidationGroup2"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>پرز :</td>
                        <td>
                            <asp:DropDownList ID="lst_porz" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_porz" DataTextField="porz_title" DataValueField="srl"></asp:DropDownList></td>
                        <td>طول :</td>
                        <td>
                            <asp:TextBox ID="txt_lenght" runat="server" CssClass="textbox" Width="150px" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>گونه :</td>
                        <td>
                            <asp:DropDownList ID="lst_brand" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
                        <td>عرض :</td>
                        <td>
                            <asp:TextBox ID="txt_weight" runat="server" CssClass="textbox" Width="150px" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>اندازه :</td>
                        <td>
                            <asp:DropDownList ID="lst_size" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
                        <td>رنگ متن :</td>
                        <td>
                            <asp:DropDownList ID="lst_color" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>

                    </tr>
                    <tr>
                        <td>نقشه فرش :</td>
                        <td>
                            <asp:DropDownList ID="lst_plan" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_plan" DataTextField="plan_title" DataValueField="srl"></asp:DropDownList></td>
                        <td>رنگ حاشیه :</td>
                        <td>
                            <asp:DropDownList ID="lst_color2" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>موقعیت :</td>
                        <td>
                            <asp:DropDownList ID="lst_status" runat="server" CssClass="dropdown1" Width="150px">
                                <asp:ListItem Value="0" Text="موجود نزد تامین کننده"></asp:ListItem>
                                <asp:ListItem Value="1" Text="فروش توسط تامین کننده"></asp:ListItem>
                                <asp:ListItem Value="2" Text="غیرفعال"></asp:ListItem>
                                <asp:ListItem Value="3" Text="آماده نیست"></asp:ListItem>
                                <asp:ListItem Value="4" Text="فروش توسط فرش بوم"></asp:ListItem>
                            </asp:DropDownList></td>
                        <td>چله :</td>
                        <td>
                            <asp:DropDownList ID="lst_chele" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_chele" DataTextField="chele_title" DataValueField="srl"></asp:DropDownList></td>
                    </tr>
                    </tr>
<%--                    <tr>
                        <td>دورنگی :</td>
                        <td>
                            <asp:CheckBox ID="chk_dorangi" runat="server"></asp:CheckBox></td>
                        <td>رفو :</td>
                        <td>
                            <asp:CheckBox ID="chk_rofo" runat="server"></asp:CheckBox></td>
                    </tr>--%>
                    <tr>
                        <td>جفت :</td>
                        <td>
                            <asp:CheckBox ID="chk_kaji" runat="server"></asp:CheckBox></td>
                        <td>قدیم بافت :</td>
                        <td>
                            <asp:CheckBox ID="chk_badbaf" runat="server"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td>پا خورده :</td>
                        <td>
                            <asp:CheckBox ID="chk_pakhordegi" runat="server"></asp:CheckBox></td>
<%--                        <td>پارگی :</td>
                        <td>
                            <asp:CheckBox ID="chk_tear" runat="server"></asp:CheckBox></td>--%>
                    </tr>
                    <tr>
                        <td>رج شمار :</td>
                        <td>
                            <asp:TextBox ID="txt_raj" runat="server" CssClass="textbox" Width="150px" MaxLength="2"></asp:TextBox>
                        </td>
                        <td>تاریخ :</td>
                        <td>
                            <asp:TextBox ID="txt_u_date_time" runat="server" CssClass="textbox" Width="150px" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>نمایش در سایت</td>
                        <td>
                            <asp:CheckBox ID="chk_choose" runat="server" Checked="false"></asp:CheckBox></td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:Image ID="image1" runat="server" Style="max-height: 800px; max-width: 600px; height: auto; width: auto;" />
            </td>
        </tr>
    </table>

    <br />
    <asp:Panel ID="Panel1" runat="server" CssClass="panelbackcolor">
        <table>
            <tr>
                <td>
                    <asp:Button ID="ImageButton_save" runat="server" CssClass="btn-facebook" Text="ذخیره فرش" OnClick="ImageButton_save_Click" ValidationGroup="RegisterUserValidationGroup2" /></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:Button AccessKey="n" ID="btn_next" runat="server" CssClass="btn-facebook" Font-Names="Tahoma" Font-Size="16" Text="<<" OnClick="btn_next_ServerClick" ToolTip="فرش بعدی" /></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:Button AccessKey="p" ID="btn_previouse" runat="server" CssClass="btn-facebook" Font-Names="Tahoma" Font-Size="16" Text=">>" OnClick="btn_previous_ServerClick" ToolTip="فرش قبلی" /></td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <table>
        <tr>
            <td style="width: 35%;">
                <table>
                    <tr>
                        <td>مبلغ خرید :</td>
                        <td>
                            <asp:TextBox ID="txt_buy" runat="server" CssClass="textbox" Width="150px" MaxLength="12"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>مبلغ فروش :</td>
                        <td>
                            <asp:TextBox ID="txt_sell" runat="server" CssClass="textbox" Width="150px" MaxLength="12"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>تخفیف واحد :</td>
                        <td>
                            <asp:TextBox ID="txt_discount" runat="server" CssClass="textbox" Width="150px" MaxLength="3"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>قیمت خانگی :</td>
                        <td>
                            <asp:TextBox ID="txt_price_home" runat="server" CssClass="textbox" Width="150px" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_save" runat="server" CssClass="btn-facebook" Text="ذخیره قیمت" OnClick="ImageButton2_save_Click" ValidationGroup="RegisterUserValidationGroup" /></td>
                    </tr>
                </table>
            </td>
            <td style="width: 30%; padding-right: 7%">
                <table>
                    <tr>
                        <td>
                            <p>
                                <asp:Label ID="lbl_project_goods" runat="server" CssClass="text-blue">
                                </asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="lst_project" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_project" DataTextField="project_code" DataValueField="srl"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_assign" runat="server" CssClass="btn-facebook" Text="تخصیص به نمایشگاه" OnClick="btn_assign_Click" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_delete_assign" runat="server" CssClass="btn-facebook" Text="حذف تخصیص" OnClick="btn_delete_assign_Click" /></td>
                    </tr>
                </table>
            </td>
            <td style="width: 35%; padding-right: 7%">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_Margin" runat="server" CssClass="text-blue"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_UnitMargin" runat="server" CssClass="text-blue"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_MarginRatio" runat="server" CssClass="text-blue"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCompaire" runat="server" CssClass="text-blue"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="row">
        <hr />
    </div>

    <div class="row">
        <table>
            <tr>
                <td>
                    <asp:Button ID="assign_selected" runat="server" CssClass="btn-facebook" Text="تخصیص دسته ای به نمایشگاه" OnClick="assign_selected_Click" />
                </td>
                <td>
                    <asp:Button ID="delete_selected" runat="server" CssClass="btn-facebook" Text="حذف دسته ای" OnClick="delete_selected_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table class="style1">
        <tr>
            <td style="direction: ltr; text-align: left">
                <asp:Button ID="Button1" runat="server" CssClass="btn-facebook" Text="بارگذاری فرش" OnClick="btn_load_Click" />
            </td>
            <td>
                <asp:Label ID="lblCount" runat="server" CssClass="text-blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upnlOutstanding" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <br />
                        <%--<asp:Button ID="lbRemoveFilterOutstanding" runat="server" CssClass="btn-facebook" Text="حذف فیلتر" OnClick="lbRemoveFilterOutstanding_Click"/>--%>
                        <asp:GridView ID="grdViewOutstanding" runat="server" AutoGenerateColumns="False"
                            BackColor="#39cccc" BorderColor="#999999" BorderStyle="Solid" CellPadding="3" ForeColor="Black"
                            GridLines="Both" CellSpacing="1" EmptyDataText="جستجو ناموفق" AllowPaging="true" PageSize="40" ShowFooter="true" CssClass="Grid" AllowSorting="true" OnPageIndexChanging="grdViewOutstanding_PageIndexChanging"
                            OnRowDataBound="grdViewOutstanding_RowDataBound" OnSorting="grdViewOutstanding_Sorting" OnSelectedIndexChanged="grdViewOutstanding_SelectedIndexChanged">
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="40" />
                                <asp:TemplateField SortExpression="provider_name" ItemStyle-Width="100">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbprovider_name" runat="server" Text="تامین کننده" CommandName="Sort" CommandArgument="provider_name" Width="90"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtprovider_name" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="90"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("provider_name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="code_igd" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbcode_igd" runat="server" Text="کدفرش" CommandName="Sort" CommandArgument="code_igd" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtcode_igd" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("code_igd") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="brand_name" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbbrand_name" runat="server" Text="گونه" CommandName="Sort" CommandArgument="brand_name" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtbrand_name" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("brand_name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="size_title" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbsize_title" runat="server" Text="اندازه" CommandName="Sort" CommandArgument="size_title" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtsize_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("size_title") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
<%--                                <asp:TemplateField SortExpression="carpet_title" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbcarpet_title" runat="server" Text="نوع" CommandName="Sort" CommandArgument="carpet_title" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtcarpet_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("carpet_title") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--                            <asp:TemplateField SortExpression="area" ItemStyle-Width="40">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbarea" runat="server" Text="مساحت" CommandName="Sort" CommandArgument="area" Width="40"></asp:LinkButton>
                                        <br />
                                <asp:TextBox runat="server" ID="txtarea" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="40"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("area") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField SortExpression="discount" ItemStyle-Width="40">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbdiscount" runat="server" Text="درصد" CommandName="Sort" CommandArgument="discount" Width="40"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtdiscount" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="40"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("discount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="provider_code" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbprovider_code" runat="server" Text="کد تامین کننده" CommandName="Sort" CommandArgument="provider_code" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtprovider_code" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("provider_code") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="buy_price" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbbuy_price" runat="server" Text="خرید" CommandName="Sort" CommandArgument="buy_price" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtbuy_price" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("buy_price") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="sale_price" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbsale_price" runat="server" Text="فروش" CommandName="Sort" CommandArgument="sale_price" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtsale_price" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("sale_price") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="u_buy" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbu_buy" runat="server" Text="خرید متری" CommandName="Sort" CommandArgument="u_buy" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtu_buy" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("u_buy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="u_sale" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbu_sale" runat="server" Text="فروش متری" CommandName="Sort" CommandArgument="u_sale" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtu_sale" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("u_sale") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="discount_amount" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbdiscount_amount" runat="server" Text="تخفیف" CommandName="Sort" CommandArgument="discount_amount" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtdiscount_amount" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("discount_amount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="final_sale" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbfinal_sale" runat="server" Text="مبلغ فروش" CommandName="Sort" CommandArgument="final_sale" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtfinal_sale" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("final_sale") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="u_date_time" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbu_date_time" runat="server" Text="تاریخ" CommandName="Sort" CommandArgument="u_date_time" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtu_date_time" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("u_date_time") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="margin_profit" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbmargin_profit" runat="server" Text="حاشیه سود" CommandName="Sort" CommandArgument="margin_profit" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtumargin_profit" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("margin_profit") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="percent_profit" ItemStyle-Width="50">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbpercent_profit" runat="server" Text="درصد سود" CommandName="Sort" CommandArgument="percent_profit" Width="50"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtupercent_profit" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="50"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("percent_profit") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ImageField DataImageUrlField="title_igd" HeaderText="تصویر" ItemStyle-Height="100" ItemStyle-Width="100"></asp:ImageField> 
                                <asp:TemplateField HeaderText="">  
                                        <ItemTemplate>  
                                            <asp:CheckBox ID="chk_delete" runat="server" />  
                                        </ItemTemplate>  
                                    </asp:TemplateField> 

                                <asp:CommandField ShowSelectButton="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="40" ControlStyle-ForeColor="Maroon" />
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="pgr" />
                            <SelectedRowStyle BackColor="DarkOliveGreen" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="Black" Font-Size="13" />
                            <AlternatingRowStyle BackColor="Azure" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
    </table>
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_sell" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_buy" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_discount" Mask="99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
    <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txt_price_home" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
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
