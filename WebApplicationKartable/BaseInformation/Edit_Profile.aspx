<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Edit_Profile.aspx.cs" Inherits="WebApplicationKartable.Edit_Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">     
    <link rel="stylesheet" href="../css/ControlStyle.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div style="text-align:center"><h2>تعریف پرسنل</h2></div>
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>                     
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td>نام :</td>
            <td><asp:TextBox ID="txt_first_name" runat="server" CssClass="textbox"  Width="180px"   MaxLength="40"></asp:TextBox><asp:Label ID="lblName" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_first_name"
                ErrorMessage="نام  اجباریست" ToolTip="نام  اجباریست"
                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
            <td>نام خانوادگی :</td>
            <td><asp:TextBox ID="txt_last_name" runat="server" CssClass="textbox"  Width="180px"  MaxLength="40"></asp:TextBox><asp:Label ID="Label1" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_last_name"
                ErrorMessage="نام خانوادگی  اجباریست" ToolTip="نام خانوادگی  اجباریست"
                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
        </tr>      
        <tr>
            <td>شماره پرسنلی :</td>
            <td><asp:TextBox ID="txt_hpl_no" runat="server" CssClass="textbox"  Width="180px"   MaxLength="10"></asp:TextBox></td>
            <td>کد ملی :</td>
            <td><asp:TextBox ID="txt_meli_code" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox></td>
        </tr>
        <tr>
            <td>تلفن :</td>
            <td><asp:TextBox ID="txt_tel" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox></td>
            <td>همراه :</td>
            <td><asp:TextBox ID="txt_mobile" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:TextBox ID="txt_username" runat="server" CssClass="textbox"  Width="180px" Visible="false"   MaxLength="15"></asp:TextBox><asp:Label ID="Label2" Text=""  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_username"
                ErrorMessage="نام کاربری اجباریست" ToolTip="نام کاربری اجباریست"
                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
            <td></td>
            <td><asp:TextBox ID="txt_password" runat="server" CssClass="textbox"  Width="180px"  MaxLength="15" Visible="false"></asp:TextBox><asp:Label ID="Label3" Text=""  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_password"
                ErrorMessage="کلمه عبور اجباریست" ToolTip="کلمه عبور اجباریست"
                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
        </tr> 
        <tr>
            <td>آدرس منزل :</td>
            <td colspan="3"><asp:TextBox ID="txt_address" runat="server" CssClass="textbox"  Width="650px" MaxLength="70"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Email :</td>
            <td colspan="3"><asp:TextBox ID="txt_mail" runat="server" CssClass="textbox"  Width="650px" MaxLength="50" ></asp:TextBox></td>
        </tr>
    </table>
            <asp:FileUpload id="upload" runat="server" />
            <br />
            <asp:Image ID="image1" runat="server" Height="300" Width="350" />
        <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
            <td><asp:ImageButton ID="btn_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="btn_save_Click" ValidationGroup="RegisterUserValidationGroup"/></td>
                </tr>               
            </table>
        </asp:Panel>
</asp:Content>
