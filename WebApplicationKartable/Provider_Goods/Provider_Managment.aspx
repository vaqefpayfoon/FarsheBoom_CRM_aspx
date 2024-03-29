﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Provider_Managment.aspx.cs" Inherits="WebApplicationKartable.Provider_Managment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    </style>
    <script type="text/javascript" src="Script/jquery.blockUI.js"></script>
    <script type="text/javascript">
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({
                    message: '<table align = "center"><tr><td>' +
                        '<img src="images/loadingAnim.gif"/></td></tr></table>',
                    css: {},
                    overlayCSS: {
                        backgroundColor: '#000000', opabrand: 0.6, border: '3px solid #63B2EB'
                    }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        }
        $(document).ready(function () {

            BlockUI("dvGrid");
            $.blockUI.defaults.css = {};
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center;">
        <h2>مدیریت تامین کننده ها</h2>
    </div>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>

    <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup" />
        <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="add_new" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="add_new_Click" /></td>
                <td style="padding-left: 5%;"></td>
                <td>
                    <asp:ImageButton ID="edit" ToolTip="ویرایش" runat="server" ImageUrl="~/images/Controls/edit.png" OnClick="edit_Click" ValidationGroup="RegisterUserValidationGroup" />
                </td>
                <td style="padding-left: 5%;"></td>
                <td>
                    <asp:ImageButton ID="delete" ToolTip="حذف" runat="server" ImageUrl="~/images/Controls/delete.png" OnClick="delete_Click" ValidationGroup="RegisterUserValidationGroup" /></td>
                <td style="padding-left: 5%;"></td>
                <td>
                    <asp:ImageButton ID="report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="report_Click" /></td>
            </tr>
        </table>
        <table>
            <tr>
                <td>نام تامین کننده :</td>
                <td>
                    <asp:TextBox ID="txtContactsSearch" runat="server" Width="300" CssClass="textbox"></asp:TextBox><asp:RequiredFieldValidator Display="None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txtContactsSearch" ErrorMessage="برای ویرایش یا حذف تامین کننده ای باید انتخاب شود"
                        ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>کد تامین کننده :</td>
                <td>
                    <asp:TextBox ID="txt_code" runat="server" Width="250" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>


           <div style ="direction:rtl;padding-right:5%">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
       <asp:GridView ID="GridView1" runat="server"  Width = "600px" AutoGenerateColumns = "false" CssClass="GridStart"
        HeaderStyle-BackColor = "#5D7B9D" AllowPaging ="true"  ShowFooter = "true"  
        OnPageIndexChanging = "OnPaging" onrowediting="EditCustomer"
        onrowupdating="UpdateCustomer"  onrowcancelingedit="CancelEdit"
        PageSize = "15" >
       <Columns>
        <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "کد ">
            <ItemTemplate>
                <asp:Label ID="lbl_srl" runat="server" Text='<%# Eval("srl")%>'></asp:Label>
            </ItemTemplate> 
        </asp:TemplateField>
    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "کد">
            <ItemTemplate>
                <asp:Label ID="lbl_provider_code" runat="server" Text='<%# Eval("provider_code")%>'></asp:Label>
            </ItemTemplate> 
            <EditItemTemplate>
                <asp:TextBox ID="txt_provider_code" runat="server" Text='<%# Eval("provider_code")%>'></asp:TextBox>
            </EditItemTemplate>  
            <FooterTemplate>
                <asp:TextBox ID="txt_provider_code_add" Width = "100px" MaxLength = "11" runat="server" CssClass="textbox"></asp:TextBox>
        </FooterTemplate> 
        </asp:TemplateField> 
        <asp:TemplateField ItemStyle-Width = "300px"  HeaderText = "عنوان">
            <ItemTemplate>
                <asp:Label ID="lbl_provider_name" runat="server" Text='<%# Eval("provider_name")%>'></asp:Label>
            </ItemTemplate> 
            <EditItemTemplate>
                <asp:TextBox ID="txt_provider_name" runat="server" Text='<%# Eval("provider_name")%>'></asp:TextBox>
            </EditItemTemplate>  
            <FooterTemplate>
                <asp:TextBox ID="txt_provider_name_add" Width = "300px" MaxLength = "50" runat="server" CssClass="textbox"></asp:TextBox>
            </FooterTemplate> 
        </asp:TemplateField>


        <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "تلفن">
            <ItemTemplate>
                <asp:Label ID="lbl_tel1" runat="server" Text='<%# Eval("tel1")%>'></asp:Label>
            </ItemTemplate> 
            <EditItemTemplate>
                <asp:TextBox ID="txt_tel1" runat="server" Text='<%# Eval("tel1")%>'></asp:TextBox>
            </EditItemTemplate>  
            <FooterTemplate>
                <asp:TextBox ID="txt_tel1_add" Width = "100px" MaxLength = "11" runat="server" CssClass="textbox"></asp:TextBox>
        </FooterTemplate> 
        </asp:TemplateField>


        <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "موبایل">
            <ItemTemplate>
                <asp:Label ID="lbl_cell_phone" runat="server" Text='<%# Eval("cell_phone")%>'></asp:Label>
            </ItemTemplate> 
            <EditItemTemplate>
                <asp:TextBox ID="txt_cell_phone" runat="server" Text='<%# Eval("cell_phone")%>'></asp:TextBox>
            </EditItemTemplate>  
            <FooterTemplate>
                <asp:TextBox ID="txt_cell_phone_add" Width = "100px" MaxLength = "12" runat="server" CssClass="textbox"></asp:TextBox>
        </FooterTemplate> 
        </asp:TemplateField> 

        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("srl")%>' 
                 OnClientClick = "return confirm('آیا مطمئن هستید ؟')"
                Text = "حذف" OnClick = "DeleteCustomer"  ControlStyle-ForeColor="Maroon" ControlStyle-Font-Bold="true">
                </asp:LinkButton>
            </ItemTemplate>
             <FooterTemplate>
                <asp:Button ID="btnAdd" runat="server" Text="درج" OnClick = "AddNewCustomer" CssClass="textbox"/>
            </FooterTemplate> 
        </asp:TemplateField> 
        <asp:CommandField ShowEditButton="True" EditText="ویرایش" UpdateText="تائید&nbsp;&nbsp;&nbsp;&nbsp;" CancelText="لغو" ControlStyle-Font-Bold="true" ControlStyle-ForeColor="Maroon"/>        
       </Columns> 
       <AlternatingRowStyle BackColor="#41b5ff" />   
    </asp:GridView> 
    </ContentTemplate> 
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID = "GridView1" /> 
    </Triggers> 
    </asp:UpdatePanel> 
        <br />
    </div>


    <cc1:AutoCompleteExtender ServiceMethod="FilterSearch" MinimumPrefixLength="2"
        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtContactsSearch"
        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
        OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
    </cc1:AutoCompleteExtender>
    <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>
</asp:Content>
