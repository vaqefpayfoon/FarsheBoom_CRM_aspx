<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Provider.aspx.cs" Inherits="WebApplicationKartable.Provider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">      
    <link rel="stylesheet" href="../css/ControlStyle.css" type="text/css" />
    <%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
    <link rel="stylesheet" href="../css/StyleSheetDate.css" type="text/css" />
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="source_city" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, city_name FROM dbo.bas_city"/>
<pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
        <div style="text-align:center"><h2>تعریف تامین کننده</h2></div>
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>
   <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>          
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td>کد تامین کننده :</td>
            <td><asp:TextBox ID="txt_provider_code" runat="server" CssClass="textbox" Width="180px" MaxLength="100"></asp:TextBox><asp:Label ID="Label3" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_provider_code"
                ErrorMessage="کد تامین کننده  اجباریست" ToolTip="کد تامین کننده  اجباریست"
                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
        </tr> 
        <tr>
            <td>نام تامین کننده :</td>
            <td><asp:TextBox ID="txt_provider_name" runat="server" CssClass="textbox" Width="180px" MaxLength="100"></asp:TextBox><asp:Label ID="lblName" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_provider_name"
                ErrorMessage="نام تامین کننده اجباریست" ToolTip="نام تامین کننده  اجباریست"
                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
            <td>برند تجاری :</td>
            <td><asp:TextBox ID="txt_related_person" runat="server" CssClass="textbox" Width="180px" MaxLength="100"></asp:TextBox>
            </td> 
        </tr>     
        <tr>
            <td>تلفن اول :</td>
            <td><asp:TextBox ID="txt_tel1" runat="server" CssClass="textbox"  Width="180px" MaxLength="12"></asp:TextBox></td>
            <td>تلفن دوم :</td>
            <td><asp:TextBox ID="txt_tel2" runat="server" CssClass="textbox"  Width="180px" MaxLength="12"></asp:TextBox></td>
        </tr>
        <tr>
            <td>همراه :</td>
            <td><asp:TextBox ID="txt_cell_phone" runat="server" CssClass="textbox"  Width="180px" MaxLength="12"></asp:TextBox></td>
            <td>فکس :</td>
            <td><asp:TextBox ID="txt_fax1" runat="server" CssClass="textbox"  Width="180px" MaxLength="12"></asp:TextBox></td>
        </tr>
        <tr>
        <td>نوع فعالیت :</td>
        <td><asp:DropDownList ID="lst_type" runat="server" CssClass="dropdown1"  Width="180px">
                <asp:ListItem  Value="1" Text="..."></asp:ListItem>
                <asp:ListItem  Value="2" Text="تولید کننده"></asp:ListItem>
                <asp:ListItem  Value="3" Text="پخش کننده"></asp:ListItem>
            </asp:DropDownList></td>
        <td>نام شهر :</td>
        <td><asp:DropDownList ID="lst_city" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_city" DataTextField="city_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>کلمه عبور :</td>
            <td colspan="3"><asp:TextBox ID="txt_desc" runat="server" CssClass="textbox"  Width="570px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>آدرس  :</td>
            <td colspan="3"><asp:TextBox ID="txt_address" runat="server" CssClass="textbox"  Width="570px"></asp:TextBox></td>
        </tr>
    </table>
        <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
            <td><asp:ImageButton ID="btn_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="btn_save_Click" ValidationGroup="RegisterUserValidationGroup"/></td>
            <td><asp:ImageButton ID="btn_return" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/Provider_Goods/Provider_Managment.aspx"/></td>
                </tr>            
            </table>
        </asp:Panel>
    <hr />
    <div style="text-align:center;"><h2>تعریف فرش های این تامین کننده</h2></div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup2"/> 
<asp:SqlDataSource ID="source_brand" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, brand_name FROM dbo.inv_brand"/>
    <asp:SqlDataSource ID="source_color" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, color_name FROM dbo.inv_color"/> 
    <asp:SqlDataSource ID="source_size" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, size_title FROM dbo.inv_size"/> 
<asp:SqlDataSource ID="source_carpet" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, carpet_title FROM dbo.inv_carpet"/>
<asp:SqlDataSource ID="source_porz" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, porz_title FROM dbo.inv_porz"/>
<asp:SqlDataSource ID="source_chele" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, chele_title FROM dbo.inv_chele"/>
<asp:SqlDataSource ID="source_plan" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, plan_title FROM dbo.inv_plan"/>
    <asp:SqlDataSource ID="source_raj" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, raj_title FROM inv_raj"/>
    <table>
        <tr>
            <td>
                    <table>
        <tr>
            <td>تصویر فرش :</td>
            <td><asp:FileUpload id="upload" runat="server" Width="180px" /></td>
        </tr> 
        <tr>
            <td>کد فرش بوم :</td>
            <td><asp:TextBox ID="txt_code" runat="server" CssClass="textbox"  Width="180px"   MaxLength="100"></asp:TextBox><asp:Label ID="Label1" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_code" ErrorMessage="کد فرش اجباریست" ToolTip="کد فرش  اجباریست" ValidationGroup="RegisterUserValidationGroup2"></asp:RequiredFieldValidator>
            </td>
            <td>کد تامین کننده :</td>
            <td><asp:TextBox ID="txt_pcode" runat="server" CssClass="textbox"  Width="180px"   MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>نوع :</td>
        <td><asp:DropDownList ID="lst_carpet" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_carpet" DataTextField="carpet_title" DataValueField="srl"></asp:DropDownList></td>
        <td>طول :</td>
        <td><asp:TextBox ID="txt_lenght" runat="server" CssClass="textbox"  Width="180px"   MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr>
        <td>گونه :</td>
        <td><asp:DropDownList ID="lst_brand" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
        <td>عرض :</td>
        <td><asp:TextBox ID="txt_weight" runat="server" CssClass="textbox"  Width="180px"   MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr>
        <td>اندازه :</td>
        <td><asp:DropDownList ID="lst_size" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
        <td>رنگ متن :</td>
        <td><asp:DropDownList ID="lst_color" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>

        </tr>
        <tr>
            <td>نقشه فرش :</td>
            <td><asp:DropDownList ID="lst_plan" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_plan" DataTextField="plan_title" DataValueField="srl"></asp:DropDownList></td>
            <td>رنگ حاشیه :</td>
            <td><asp:DropDownList ID="lst_color2" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>

        <td> ارزیابی :</td>
        <td><asp:DropDownList ID="lst_value" runat="server" CssClass="dropdown1"  Width="180px">
                <asp:ListItem  Value="1" Text="عادی"></asp:ListItem>
                <asp:ListItem  Value="2" Text="مورد پسند"></asp:ListItem>
            </asp:DropDownList></td>
        <td>پرز :</td>
        <td><asp:DropDownList ID="lst_porz" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_porz" DataTextField="porz_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td> وضعیت :</td>
        <td><asp:DropDownList ID="lst_status" runat="server" CssClass="dropdown1"  Width="180px">
                <asp:ListItem  Value="0" Text="موجود نزد تامین کننده"></asp:ListItem>
                <asp:ListItem  Value="1" Text="فروش توسط تامین کننده"></asp:ListItem>
                <asp:ListItem  Value="2" Text="غیرفعال"></asp:ListItem>
                <asp:ListItem  Value="3" Text="آماده نیست"></asp:ListItem>
                <asp:ListItem  Value="4" Text="فروش توسط فرش بوم"></asp:ListItem>
                <asp:ListItem  Value="5" Text="فرش مرجوعی"></asp:ListItem>
            </asp:DropDownList></td>
        <td>چله :</td>
        <td><asp:DropDownList ID="lst_chele" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_chele" DataTextField="chele_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td>رج شمار :</td>
        <td><asp:DropDownList ID="lst_raj" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_raj" DataTextField="raj_title" DataValueField="srl"></asp:DropDownList></td>
            <td>توضیحات :</td>
            <td><asp:TextBox ID="txt_describtion" runat="server" CssClass="textbox"  Width="300px"></asp:TextBox></td>
        </tr>
    </table>
            </td>
            <td>
                <asp:Image ID="image1" runat="server" Height="300" Width="350" />
            </td>
        </tr>
    </table>

    <br />
    <asp:Panel ID="Panel1" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
            <td><asp:ImageButton ID="ImageButton_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="ImageButton_save_Click" ValidationGroup="RegisterUserValidationGroup2"/></td>
            <td><asp:ImageButton ID="ImageButton_addnew" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="ImageButton_addnew_Click"/></td>
            <td><asp:ImageButton ID="ImageButton2" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/Provider_Goods/Provider_Managment.aspx"/></td></tr> 
            </table>
        </asp:Panel>
    <br />
        <asp:GridView ID="gridview" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" AllowPaging ="true" PageSize = "10" onselectedindexchanged="gridview_SelectedIndexChanged" OnPageIndexChanging = "gridview_PageIndexChanging">
        <Columns>                      
            <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="70" ReadOnly="True"/>
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="200" ReadOnly="True"/>   
            <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="color_name" HeaderText="رنگ" ItemStyle-Width="150" ReadOnly="True"/>
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

</asp:Content>
