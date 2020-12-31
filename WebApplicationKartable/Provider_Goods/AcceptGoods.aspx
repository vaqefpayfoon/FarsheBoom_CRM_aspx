<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AcceptGoods.aspx.cs" Inherits="WebApplicationKartable.AcceptGoods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="text-align:center"><h2>ورود فرش</h2></div>
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>          
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
<asp:SqlDataSource ID="source_provider" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, provider_name FROM dbo.bas_provider order by provider_name"/>
    <table>
        <tr>
            <td>تامین کننده :</td>
            <td><asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_provider" DataTextField="provider_name" DataValueField="srl"></asp:DropDownList></td>
            <td><asp:Button ID="btn_filter" runat="server" CssClass="btn-facebook" OnClick="btn_filter_Click" Text="نمایش فرش ها" /></td>
        </tr>
    </table>
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
            <td><asp:FileUpload id="upload" runat="server" Width="150px" /></td>
        </tr> 
        <tr>
            <td>کد فرش بوم :</td>
            <td><asp:TextBox ID="txt_code" runat="server" CssClass="textbox"  Width="150px" ReadOnly="true"   MaxLength="100"></asp:TextBox><asp:Label ID="Label1" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_code" ErrorMessage="کد فرش اجباریست" ToolTip="کد فرش  اجباریست" ValidationGroup="RegisterUserValidationGroup2"></asp:RequiredFieldValidator>
            </td>
            <td>کد تامین کننده :</td>
            <td><asp:TextBox ID="txt_pcode" runat="server" CssClass="textbox"  Width="150px"   MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>نوع :</td>
        <td><asp:DropDownList ID="lst_carpet" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_carpet" DataTextField="carpet_title" DataValueField="srl"></asp:DropDownList></td>
        <td>طول :</td>
        <td><asp:TextBox ID="txt_lenght" runat="server" CssClass="textbox"  Width="150px"   MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr>
        <td>گونه :</td>
        <td><asp:DropDownList ID="lst_brand" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
        <td>عرض :</td>
        <td><asp:TextBox ID="txt_weight" runat="server" CssClass="textbox"  Width="150px"   MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr>
        <td>اندازه :</td>
        <td><asp:DropDownList ID="lst_size" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
        <td>رنگ متن :</td>
        <td><asp:DropDownList ID="lst_color" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>

        </tr>
        <tr>
            <td>نقشه فرش :</td>
            <td><asp:DropDownList ID="lst_plan" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_plan" DataTextField="plan_title" DataValueField="srl"></asp:DropDownList></td>
            <td>رنگ حاشیه :</td>
            <td><asp:DropDownList ID="lst_color2" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_color" DataTextField="color_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td> ارزیابی :</td>
        <td><asp:DropDownList ID="lst_value" runat="server" CssClass="dropdown1"  Width="150px">
                <asp:ListItem  Value="1" Text="عادی"></asp:ListItem>
                <asp:ListItem  Value="2" Text="مورد پسند"></asp:ListItem>
            </asp:DropDownList></td>
        <td>پرز :</td>
        <td><asp:DropDownList ID="lst_porz" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_porz" DataTextField="porz_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td> وضعیت :</td>
        <td><asp:DropDownList ID="lst_status" runat="server" CssClass="dropdown1"  Width="150px">
                <asp:ListItem  Value="0" Text="موجود نزد تامین کننده"></asp:ListItem>
                <asp:ListItem  Value="1" Text="فروش توسط تامین کننده"></asp:ListItem>
                <asp:ListItem  Value="2" Text="غیرفعال"></asp:ListItem>
                <asp:ListItem  Value="3" Text="آماده نیست"></asp:ListItem>
                <asp:ListItem  Value="4" Text="فروش توسط فرش بوم"></asp:ListItem>
                <asp:ListItem  Value="5" Text="فرش مرجوعی"></asp:ListItem>
            </asp:DropDownList></td>
        <td>چله :</td>
        <td><asp:DropDownList ID="lst_chele" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_chele" DataTextField="chele_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td>رج شمار :</td>
        <td><asp:DropDownList ID="lst_raj" runat="server" CssClass="dropdown1"  Width="150px" DataSourceID="source_raj" DataTextField="raj_title" DataValueField="srl"></asp:DropDownList></td>
            <td>توضیحات :</td>
            <td><asp:TextBox ID="txt_describtion" runat="server" CssClass="textbox"  Width="150px"></asp:TextBox></td>
        </tr>
    </table>
            </td>
            <td>
                <asp:Image ID="image1" runat="server" style="max-height:800px;max-width:600px;height:auto;width:auto;"  />
            </td>
        </tr>
    </table>

    <br />
    <asp:Panel ID="Panel1" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
                     <td><asp:Button ID="btn_save_delete" runat="server" CssClass="btn-facebook" OnClick="btn_save_delete_Click" Text="ذخیره و حذف" /></td>
                </tr> 
            </table>
        </asp:Panel>
    <br />
        <asp:GridView ID="gridview" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" onselectedindexchanged="gridview_SelectedIndexChanged" OnPageIndexChanging = "gridview_PageIndexChanging" Width="900px">
        <Columns>                      
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="200" ReadOnly="True"/>   
            <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="color_name" HeaderText="رنگ" ItemStyle-Width="150" ReadOnly="True"/>
            <asp:BoundField DataField="plan_title" HeaderText="نقشه" ItemStyle-Width="150" ReadOnly="True"/>
            <asp:BoundField DataField="carpet_title" HeaderText="نوع" ItemStyle-Width="150" ReadOnly="True"/>
            <asp:BoundField DataField="chele_title" HeaderText="چله" ItemStyle-Width="150" ReadOnly="True"/>
            <asp:BoundField DataField="porz_title" HeaderText="پرز" ItemStyle-Width="150" ReadOnly="True"/>
            <asp:CommandField ShowSelectButton ="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
            <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("srl")%>' 
                 OnClientClick = "return confirm('آیا مطمئن هستید ؟')"
                Text = "حذف" OnClick = "DeleteCustomer"  ControlStyle-ForeColor="Maroon" ControlStyle-Font-Bold="true">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
         <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />     
    </asp:GridView>
</asp:Content>

