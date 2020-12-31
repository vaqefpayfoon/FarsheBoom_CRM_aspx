<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Firms.aspx.cs" Inherits="WebApplicationKartable.Firms" %>
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
  <style type="text/css">
     .GridStart
     {
         direction:rtl;
         text-align:right;
            line-height:normal;
            font-family:'B Nazanin' Arial;
	        font-size: 16px;
            border-color:lemonchiffon;
            color:black;
     }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="text-align:center;"><h2>تعریف اداره جات</h2></div>
       <div style ="direction:rtl;padding-right:5%;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
       <asp:GridView ID="GridView1" runat="server"  Width = "600px" 
        AutoGenerateColumns = "false" Font-Names = "Arial"  CssClass="GridStart"   Font-Size = "14pt" 
        HeaderStyle-BackColor = "Silver" AllowPaging ="true"  ShowFooter = "true"  
        OnPageIndexChanging = "OnPaging" onrowediting="Edit_hpl_group"
        onrowupdating="Update_hpl_group"  onrowcancelingedit="CancelEdit"
        PageSize = "10" >
       <Columns>
        <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "کد ">
            <ItemTemplate>
                <asp:Label ID="lbl_srl" runat="server" Text='<%# Eval("srl")%>'></asp:Label>
            </ItemTemplate> 
        </asp:TemplateField> 
        <asp:TemplateField ItemStyle-Width = "300px"  HeaderText = "نام اداره">
            <ItemTemplate>
                <asp:Label ID="lbl_firm_title" runat="server" Text='<%# Eval("firm_title")%>'></asp:Label>
            </ItemTemplate> 
            <EditItemTemplate>
                <asp:TextBox ID="txt_firm_title" runat="server" Text='<%# Eval("firm_title")%>'></asp:TextBox>
            </EditItemTemplate>  
            <FooterTemplate>
                <asp:TextBox ID="txt_firm_title_add" Width = "300px" MaxLength = "30" runat="server"></asp:TextBox>
            </FooterTemplate> 
        </asp:TemplateField> 
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("srl")%>' 
                 OnClientClick = "return confirm('آیا مطمئن هستید ؟')"
                Text = "حذف" OnClick = "Delete_hpl_group"  ControlStyle-ForeColor="Maroon" ControlStyle-Font-Bold="true">
                </asp:LinkButton>
            </ItemTemplate>
             <FooterTemplate>
                <asp:Button ID="btnAdd" runat="server" Text="درج" OnClick = "AddNew_hpl_group" ControlStyle-Font-Size="Medium"/>
            </FooterTemplate> 
        </asp:TemplateField> 
        <asp:CommandField ShowEditButton="True" EditText="ویرایش" UpdateText="تائید&nbsp;&nbsp;&nbsp;&nbsp;" CancelText="لغو" ControlStyle-Font-Bold="true" ControlStyle-ForeColor="Maroon"/>        
       </Columns> 
       <AlternatingRowStyle BackColor="#39cccc" />   
    </asp:GridView> 
    </ContentTemplate> 
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID = "GridView1" /> 
    </Triggers> 
    </asp:UpdatePanel> 
        <br />
        <asp:Panel ID="search_panel" runat="server"  CssClass="panelbackcolor">
           <asp:ImageButton ID="btn_return" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/HomePage.aspx" />
        </asp:Panel>
    </div>
</asp:Content>
