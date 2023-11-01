<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProductAssign.aspx.cs" Inherits="WebApplicationKartable.ProductAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <div style="text-align: center">
        <h2>ورود فرش</h2>
    </div>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
        ValidationGroup="RegisterUserValidationGroup" />
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <asp:SqlDataSource ID="source_provider" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, provider_name FROM dbo.bas_provider order by provider_name" />
    <table>
        <tr>
            <td>تامین کننده :</td>
            <td>
                <asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_provider" DataTextField="provider_name" DataValueField="srl"></asp:DropDownList></td>
            <td>
                <asp:Button ID="btn_filter" runat="server" CssClass="btn-facebook" OnClick="btn_filter_Click" Text="نمایش فرش ها" /></td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"
        ValidationGroup="RegisterUserValidationGroup2" />
    <asp:SqlDataSource ID="source_brand" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, brand_name FROM dbo.inv_brand" />
    <asp:SqlDataSource ID="source_color" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, color_name FROM dbo.inv_color" />
    <asp:SqlDataSource ID="source_size" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, size_title FROM dbo.inv_size" />
    <asp:SqlDataSource ID="source_porz" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, porz_title FROM dbo.inv_porz" />
    <asp:SqlDataSource ID="source_chele" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, chele_title FROM dbo.inv_chele" />
    <asp:SqlDataSource ID="source_plan" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, plan_title FROM dbo.inv_plan" />
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>تصویر فرش :</td>
                        <td>
                            <asp:FileUpload ID="upload" runat="server" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td>کد فرش بوم :</td>
                        <td>
                            <asp:TextBox ID="txt_code" runat="server" CssClass="textbox" Width="150px" ReadOnly="true" MaxLength="100"></asp:TextBox><asp:Label ID="Label1" Text="*" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator Display="None" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_code" ErrorMessage="کد فرش اجباریست" ToolTip="کد فرش  اجباریست" ValidationGroup="RegisterUserValidationGroup2"></asp:RequiredFieldValidator>
                        </td>
                        <td>کد تامین کننده :</td>
                        <td>
                            <asp:TextBox ID="txt_pcode" runat="server" CssClass="textbox" Width="150px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>طول :</td>
                        <td>
                            <asp:TextBox ID="txt_lenght" runat="server" CssClass="textbox" Width="150px" MaxLength="30"></asp:TextBox></td>
                        <td>عرض :</td>
                        <td>
                            <asp:TextBox ID="txt_weight" runat="server" CssClass="textbox" Width="150px" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>گونه :</td>
                        <td>
                            <asp:DropDownList ID="lst_brand" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
                        <td>رنگ متن :</td>
                        <td>
                            <asp:DropDownList ID="lst_color" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>اندازه :</td>
                        <td>
                            <asp:DropDownList ID="lst_size" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
                        <td>رنگ حاشیه :</td>
                        <td>
                            <asp:DropDownList ID="lst_color2" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>

                    </tr>
                    <tr>
                        <td>نقشه فرش :</td>
                        <td>
                            <asp:DropDownList ID="lst_plan" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_plan" DataTextField="plan_title" DataValueField="srl"></asp:DropDownList></td>
                        <td>پرز :</td>
                        <td>
                            <asp:DropDownList ID="lst_porz" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_porz" DataTextField="porz_title" DataValueField="srl"></asp:DropDownList></td>
                    </tr>
                    <tr>
                    <td>موقعیت :</td>
                        <td>
                            <asp:DropDownList ID="lst_status" runat="server" CssClass="dropdown1" Width="150px">
                                <asp:ListItem Value="0" Text="موجود"></asp:ListItem>
                                <%--<asp:ListItem Value="3" Text="ناموجود"></asp:ListItem>--%>
                                <asp:ListItem Value="4" Text="فروش"></asp:ListItem>
                                <asp:ListItem Value="2" Text="حذف"></asp:ListItem>
                                <%--<asp:ListItem Value="5" Text="فرش مرجوعی"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td>چله :</td>
                        <td>
                            <asp:DropDownList ID="lst_chele" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_chele" DataTextField="chele_title" DataValueField="srl"></asp:DropDownList></td>
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
                                                <td>قیمت خانگی :</td>
                        <td>
                            <asp:TextBox ID="txt_price_home" runat="server" CssClass="textbox" Width="150px" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>توضیحات  :</td>
                        <td colspan="3">
                            <asp:TextBox ID="txt_plan_desc" runat="server" CssClass="textbox" Width="580px"></asp:TextBox></td>
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
                    <asp:ImageButton ID="ImageButton_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="ImageButton_save_Click" ValidationGroup="RegisterUserValidationGroup2" /></td>
                <td>
                    <asp:ImageButton ID="ImageButton_addnew" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="ImageButton_addnew_Click" /></td>
                <td>
                    <asp:ImageButton ID="ImageButton_delete" ToolTip="حذف" runat="server" ImageUrl="~/images/Controls/delete.png" OnClick="ImageButton_delete_Click" /></td>
                <td>
                    <asp:ImageButton ID="ImageButton2" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/Provider_Goods/Provider_Managment.aspx" /></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
                    <div class="row col-md-12" style="direction: ltr; text-align: left">
                <asp:Button ID="Button1" runat="server" CssClass="btn-facebook" Text="بارگذاری فرش" OnClick="btn_load_Click" />
            </div>
        <table class="style1">
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upnlOutstanding" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <br />
                        <asp:Button ID="lbRemoveFilterOutstanding" runat="server" CssClass="btn btn-primary" Text="حذف فیلتر" OnClick="lbRemoveFilterOutstanding_Click"/>
                        <asp:GridView ID="grdViewOutstanding" runat="server" AutoGenerateColumns="False"
                            BackColor="#39cccc" BorderColor="#999999" BorderStyle="Solid" CellPadding="3" ForeColor="Black"
                            GridLines="Both" CellSpacing="1" EmptyDataText="جستجو ناموفق"  AllowPaging ="true" PageSize="40" CssClass="Grid" AllowSorting="true" OnPageIndexChanging="grdViewOutstanding_PageIndexChanging"
                            OnRowDataBound="grdViewOutstanding_RowDataBound" OnSorting="grdViewOutstanding_Sorting" OnSelectedIndexChanged="grdViewOutstanding_SelectedIndexChanged">
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                        <asp:BoundField DataField="code_igd" HeaderText="کد" ItemStyle-Width="60" />
                        <asp:TemplateField SortExpression="provider_name" ItemStyle-Width="110">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbprovider_name" runat="server" Text="تامین کننده" CommandName="Sort" CommandArgument="provider_name" Width="110"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtprovider_name" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="110"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("provider_name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="provider_code" ItemStyle-Width="110">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbprovider_code" runat="server" Text="کد تامین کننده" CommandName="Sort" CommandArgument="provider_code" Width="110"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtprovider_code" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="110"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("provider_code") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="code_igd" ItemStyle-Width="70">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbcode_igd" runat="server" Text="کدفرش" CommandName="Sort" CommandArgument="code_igd" Width="70"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtcode_igd" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="70"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("code_igd") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="brand_name" ItemStyle-Width="70">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbbrand_name" runat="server" Text="گونه" CommandName="Sort" CommandArgument="brand_name" Width="70"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtbrand_name" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="70"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("brand_name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="size_title" ItemStyle-Width="70">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbsize_title" runat="server" Text="اندازه" CommandName="Sort" CommandArgument="size_title" Width="70"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtsize_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="70"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("size_title") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField SortExpression="u_buy" ItemStyle-Width="80">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbu_buy" runat="server" Text="خرید متری" CommandName="Sort" CommandArgument="u_buy" Width="80"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtu_buy" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="80"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("u_buy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField SortExpression="color_name" ItemStyle-Width="80">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbcolor_name" runat="server" Text="رنگ متن" CommandName="Sort" CommandArgument="color_name" Width="80"></asp:LinkButton>
                                        <br />
                                <asp:TextBox runat="server" ID="txtcolor_name" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="80"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("color_name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField SortExpression="porz_title" ItemStyle-Width="80">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbporz_title" runat="server" Text="پرز" CommandName="Sort" CommandArgument="porz_title" Width="80"></asp:LinkButton>
                                        <br />
                                <asp:TextBox runat="server" ID="txtporz_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="80"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("porz_title") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField SortExpression="chele_title" ItemStyle-Width="80">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbchele_title" runat="server" Text="چله" CommandName="Sort" CommandArgument="chele_title" Width="80"></asp:LinkButton>
                                        <br />
                                <asp:TextBox runat="server" ID="txtchele_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="80"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("chele_title") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField SortExpression="plan_title" ItemStyle-Width="60">
                                    <HeaderTemplate>
                            <asp:LinkButton ID="lbplan_title" runat="server" Text="نقشه" CommandName="Sort" CommandArgument="plan_title" Width="60"></asp:LinkButton>
                                        <br />
                                <asp:TextBox runat="server" ID="txtplan_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="60"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("plan_title") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">  
                                        <ItemTemplate>  
                                            <asp:CheckBox ID="chk_delete" runat="server" />  
                                        </ItemTemplate>  
                                    </asp:TemplateField> 
        <asp:CommandField ShowSelectButton="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
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

    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_price_home" Mask="9,999,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
</asp:Content>
