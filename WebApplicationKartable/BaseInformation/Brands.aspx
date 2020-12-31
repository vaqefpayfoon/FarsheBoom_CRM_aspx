<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Brands.aspx.cs" Inherits="WebApplicationKartable.Brands" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript" src = "Script/jquery.blockUI.js"></script>
<script type = "text/javascript">
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
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="text-align:center;"><h2>گونه</h2></div>
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
        <asp:TemplateField ItemStyle-Width = "300px"  HeaderText = "عنوان">
            <ItemTemplate>
                <asp:Label ID="lbl_provider_name" runat="server" Text='<%# Eval("brand_name")%>'></asp:Label>
            </ItemTemplate> 
            <EditItemTemplate>
                <asp:TextBox ID="txt_brand_name" runat="server" Text='<%# Eval("brand_name")%>'></asp:TextBox>
            </EditItemTemplate>  
            <FooterTemplate>
                <asp:TextBox ID="txt_brand_name_add" Width = "300px" MaxLength = "30" runat="server" CssClass="textbox"></asp:TextBox>
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
</asp:Content>
