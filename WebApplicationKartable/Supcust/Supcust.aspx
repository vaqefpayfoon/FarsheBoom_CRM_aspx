<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Supcust.aspx.cs" Inherits="WebApplicationKartable.Supcust" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">      
    <link rel="stylesheet" href="../css/ControlStyle.css" type="text/css" />
    <%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <link rel="stylesheet" href="../css/StyleSheetDate.css" type="text/css" />   
            <style type="text/css">
        .loading
        {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="source_city" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, city_name FROM dbo.bas_city"/>
<asp:SqlDataSource ID="source_brand" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, brand_name FROM dbo.inv_brand"/>
 <asp:SqlDataSource ID="source_clue" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, meet_title FROM dbo.bas_sale_clue"/>
<pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
        <div style="text-align:center"><h2>مشتری</h2></div>
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/> 
   <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>          
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td>نام مشتری :</td>
            <td colspan="3"><asp:TextBox ID="txt_full_name" runat="server" CssClass="textbox"  Width="580px"   MaxLength="100"></asp:TextBox><asp:Label ID="lblName" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_full_name"
                ErrorMessage="نام مشتری اجباریست" ToolTip="نام مشتری اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
        </tr>
        <tr>
        <td>سطح ارتباط :</td>
        <td><asp:DropDownList ID="lst_clue" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_clue" DataTextField="meet_title" DataValueField="srl"></asp:DropDownList></td>
        <td>نحوه آشنایی :</td>
        <td><asp:DropDownList ID="lst_meet" runat="server" CssClass="dropdown1"  Width="180px">
            <asp:ListItem  Value="1" Text="..."></asp:ListItem>
            <asp:ListItem  Value="2" Text="اینستاگرام"></asp:ListItem>
            <asp:ListItem  Value="3" Text="تلگرام"></asp:ListItem>
            <asp:ListItem  Value="4" Text="فیس بوک"></asp:ListItem>
            <asp:ListItem  Value="5" Text="وب سایت فرش بوم"></asp:ListItem>
            <asp:ListItem  Value="6" Text="آگهی نامه"></asp:ListItem>
            <asp:ListItem  Value="7" Text="پوستر و کاتالوگ"></asp:ListItem>
            <asp:ListItem  Value="8" Text="محلی"></asp:ListItem>
            <asp:ListItem  Value="9" Text="دوستان"></asp:ListItem>
            <asp:ListItem  Value="10" Text="سایر"></asp:ListItem>
            </asp:DropDownList></td>
        </tr>      
        <tr>
            <td>جنسیت :</td>
            <td><asp:DropDownList ID="lst_sex" runat="server" CssClass="dropdown1"  Width="180px">
            <asp:ListItem  Value="1" Text="..."></asp:ListItem>
            <asp:ListItem  Value="2" Text="خانم"></asp:ListItem>
            <asp:ListItem  Value="3" Text="آقا"></asp:ListItem>
            </asp:DropDownList></td>
        <td>نام شهر :</td>
        <td><asp:DropDownList ID="lst_city" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_city" DataTextField="city_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td>سن :</td>
        <td><asp:DropDownList ID="lst_age" runat="server" CssClass="dropdown1"  Width="180px">
            <asp:ListItem  Value="4" Text="..."></asp:ListItem>
            <asp:ListItem  Value="1" Text="35 تا 60 سال"></asp:ListItem>
            <asp:ListItem  Value="2" Text="زیر 35 سال"></asp:ListItem>
            <asp:ListItem  Value="3" Text="بالای 60 سال"></asp:ListItem>
            </asp:DropDownList></td> 
       <td>تاریخ :</td>
            <td><asp:TextBox ID="txt_u_date_time" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>تلفن :</td>
            <td><asp:TextBox ID="txt_tel1" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox></td>
            <td>موبایل :</td>
            <td><asp:TextBox ID="txt_cell_phone" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox></td>
        </tr>
        <tr>
            <td>آدرس  :</td>
            <td colspan="3"><asp:TextBox ID="txt_address" runat="server" CssClass="textbox"  Width="580px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>توضیحات  :</td>
            <td colspan="3"><asp:TextBox ID="txt_desc" runat="server" CssClass="textbox"  Width="580px"></asp:TextBox></td>
        </tr>
    </table>
        <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
            <td><asp:ImageButton ID="btn_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="btn_save_Click" ValidationGroup="RegisterUserValidationGroup"/></td>
            <td><asp:ImageButton ID="btn_return" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/Supcust/SupcustManagers.aspx"/></td>
                </tr>               
            </table>
        </asp:Panel>
    <asp:SqlDataSource ID="source_color" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, color_name FROM dbo.inv_color"/> 
    <asp:SqlDataSource ID="source_size" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, size_title FROM dbo.inv_size"/> 
<asp:SqlDataSource ID="source_carpet" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, carpet_title FROM dbo.inv_carpet"/>
    <asp:SqlDataSource ID="source_plan" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, plan_title FROM dbo.inv_plan"/>
<div><br /><hr /></div>
    <div style="text-align:center"><h2>ثبت سفارشات مشتری</h2></div>
    <table>
        <tr>
            <td>نقشه فرش :</td>
            <td><asp:DropDownList ID="lst_plan" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_plan" DataTextField="plan_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td>نوع :</td>
            <td><asp:DropDownList ID="lst_carpet" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_carpet" DataTextField="carpet_title" DataValueField="srl"></asp:DropDownList></td>
        <td>اندازه :</td>
            <td><asp:DropDownList ID="lst_size" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td>رنگ متن :</td>
        <td><asp:DropDownList ID="lst_color" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>
        <td>گونه :</td>
        <td><asp:DropDownList ID="lst_city2" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>از مبلغ :</td>
            <td><asp:TextBox ID="txt_from_price" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox></td>
            <td>تا مبلغ :</td>
            <td><asp:TextBox ID="txt_to_price" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox></td>
        </tr>
        <tr>
            <td>توضیحات  :</td>
            <td colspan="3"><asp:TextBox ID="txt_describtion" runat="server" CssClass="textbox"  Width="550px"></asp:TextBox></td>
        </tr>
    </table>
        <br />
        <asp:Panel ID="Panel1" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
            <td><asp:ImageButton ID="ImageButton_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="ImageButton_save_Click" /></td>
            <td><asp:ImageButton ID="ImageButton_addnew" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="ImageButton_addnew_Click"/></td></tr>               
            </table>
        </asp:Panel>
        <asp:GridView ID="gridview" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" onselectedindexchanged="gridview_SelectedIndexChanged">
        <Columns>                      
            <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="70" ReadOnly="True"/>         
            <asp:BoundField DataField="u_date_time" HeaderText="تاریخ" ItemStyle-Width="150" ReadOnly="True"/>     
            <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="200" ReadOnly="True"/>        
            <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="200" ReadOnly="True"/>    
            <asp:BoundField DataField="color_name" HeaderText="رنگ" ItemStyle-Width="100" ReadOnly="True"/>              
            <asp:BoundField DataField="carpet_title" HeaderText="نوع فرش" ItemStyle-Width="100" ReadOnly="True"/>      
            <asp:CommandField ShowSelectButton ="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
            <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("srl")%>' 
                 OnClientClick = "return confirm('آیا مطمئن هستید ؟')"
                Text = "حذف" OnClick = "lnkRemove_Click"  ControlStyle-ForeColor="Maroon" ControlStyle-Font-Bold="true">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField> 
        </Columns>
         <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />     
    </asp:GridView>
    <div><br /><hr /></div>
    <div style="text-align:center"><h2>ثبت کالای مشتری</h2></div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup2"/> 
    <table>
        <tr>
            <td>کد فرش :</td>
            <td><asp:TextBox ID="txt_product" runat="server" Width="300"  CssClass="textbox"></asp:TextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_product" ErrorMessage="فرش را انتخاب کنید" ValidationGroup="RegisterUserValidationGroup2"></asp:RequiredFieldValidator></td>
        <td>تاریخ :</td>
            <td><pdc:PersianDateTextBox ID="txt_register_date" runat="server"  IconUrl="~/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px" CssClass="textbox"></pdc:PersianDateTextBox></td>
        </tr>
    </table>
        <asp:Panel ID="Panel2" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
            <td><asp:ImageButton ID="ImageButton2_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="ImageButton2_save_Click" ValidationGroup="RegisterUserValidationGroup2"/></td>
            <td><asp:ImageButton ID="ImageButton2_addnew" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="ImageButton2_addnew_Click"/></td></tr>               
            </table>
        </asp:Panel>
        <cc1:AutoCompleteExtender ServiceMethod="FilterSearch2" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_product"
            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
            OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
        </cc1:AutoCompleteExtender>
<br />
    <asp:GridView ID="gridview2" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" onselectedindexchanged="gridview2_SelectedIndexChanged">
        <Columns>                      
            <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="70" ReadOnly="True"/>         
            <asp:BoundField DataField="u_date_time" HeaderText="تاریخ" ItemStyle-Width="150" ReadOnly="True"/>
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="200" ReadOnly="True"/>
            <asp:CommandField ShowSelectButton ="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
            <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkRemove2" runat="server" CommandArgument = '<%# Eval("srl")%>' 
                 OnClientClick = "return confirm('آیا مطمئن هستید ؟')"
                Text = "حذف" OnClick = "lnkRemove2_Click"  ControlStyle-ForeColor="Maroon" ControlStyle-Font-Bold="true">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField> 
        </Columns>
         <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />     
    </asp:GridView>
    <br /><br /><br />
    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_from_price" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_to_price" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
        <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>
</asp:Content>
