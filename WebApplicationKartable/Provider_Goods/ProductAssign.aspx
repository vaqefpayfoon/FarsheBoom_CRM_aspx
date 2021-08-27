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
                                <asp:ListItem Value="3" Text="ناموجود"></asp:ListItem>
                                <asp:ListItem Value="4" Text="فروش توسط فرش بوم"></asp:ListItem>
                                <asp:ListItem Value="2" Text="حذف توسط فرش بوم"></asp:ListItem>
                                <%--<asp:ListItem Value="5" Text="فرش مرجوعی"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td>چله :</td>
                        <td>
                            <asp:DropDownList ID="lst_chele" runat="server" CssClass="dropdown1" Width="150px" DataSourceID="source_chele" DataTextField="chele_title" DataValueField="srl"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>دورنگی :</td>
                        <td>
                            <asp:CheckBox ID="chk_dorangi" runat="server"></asp:CheckBox></td>
                        <td>رفو :</td>
                        <td>
                            <asp:CheckBox ID="chk_rofo" runat="server"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td>کجی :</td>
                        <td>
                            <asp:CheckBox ID="chk_kaji" runat="server"></asp:CheckBox></td>
                        <td>بد بافت :</td>
                        <td>
                            <asp:CheckBox ID="chk_badbaf" runat="server"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td>پا خوردگی :</td>
                        <td>
                            <asp:CheckBox ID="chk_pakhordegi" runat="server"></asp:CheckBox></td>
                        <td>پارگی :</td>
                        <td>
                            <asp:CheckBox ID="chk_tear" runat="server"></asp:CheckBox></td>
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
    <asp:GridView ID="gridview" runat="server" CssClass="textbox" AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D" HeaderStyle-ForeColor="White" OnSelectedIndexChanged="gridview_SelectedIndexChanged" AllowPaging="true" PageSize="14" OnPageIndexChanging="gridview_PageIndexChanging" Width="900px">
        <Columns>
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="100" ReadOnly="True" />
            <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="200" ReadOnly="True" />
            <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="100" ReadOnly="True" />
            <asp:BoundField DataField="color_name" HeaderText="رنگ" ItemStyle-Width="150" ReadOnly="True" />
            <asp:BoundField DataField="plan_title" HeaderText="نقشه" ItemStyle-Width="150" ReadOnly="True" />
            <asp:BoundField DataField="carpet_title" HeaderText="نوع" ItemStyle-Width="150" ReadOnly="True" />
            <asp:BoundField DataField="chele_title" HeaderText="چله" ItemStyle-Width="150" ReadOnly="True" />
            <asp:BoundField DataField="porz_title" HeaderText="پرز" ItemStyle-Width="150" ReadOnly="True" />
            <asp:TemplateField HeaderText="حذف">  
                    <ItemTemplate>  
                        <asp:CheckBox ID="chk_delete" runat="server" />  
                    </ItemTemplate>  
                </asp:TemplateField> 
            <asp:CommandField ShowSelectButton="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
        </Columns>
        <AlternatingRowStyle BackColor="Azure" />
        <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />
    </asp:GridView>
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_price_home" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
</asp:Content>
