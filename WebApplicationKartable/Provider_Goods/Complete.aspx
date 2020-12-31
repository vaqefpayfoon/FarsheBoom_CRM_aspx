<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Complete.aspx.cs" Inherits="WebApplicationKartable.Complete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="Link1" href="../Assets/default.css" rel="stylesheet" type="text/css" media="screen"
        runat="server" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        td{text-align:center}
    </style>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>
    <table>
        <tr>
            <td> 
                <asp:Button ID="btn_load" runat="server" CssClass="btn btn-primary" Text="فرش های موجود" OnClick="btn_load_Click"/>
            </td>
            <td>
                <asp:Button ID="btn_null" runat="server" CssClass="btn btn-primary" Text="تکمیل اطلاعات" OnClick="btn_null_Click"/>
            </td>
            <td>
                <asp:Button ID="btn_print" runat="server" CssClass="btn btn-primary" Text="چاپ لیستی" OnClick="btn_print_Click"/>
            </td>
            <td>
                <asp:Button ID="btn_print_pic" runat="server" CssClass="btn btn-primary" Text="چاپ با تصویر" OnClick="btn_print_pic_Click"/>
            </td>
        </tr>
    </table>
    <hr />
    <table class="style1">
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upnlOutstanding" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <br />
                        <asp:Button ID="lbRemoveFilterOutstanding" runat="server" CssClass="btn btn-primary" Text="حذف فیلتر" OnClick="lbRemoveFilterOutstanding_Click"/>
                        <asp:GridView ID="grdViewOutstanding" runat="server" AutoGenerateColumns="False"
                            BackColor="#39cccc" BorderColor="#999999" BorderStyle="Solid" CellPadding="3" ForeColor="Black"
                            GridLines="Both" CellSpacing="1" EmptyDataText="جستجو ناموفق"
                            CssClass="Grid" AllowSorting="true" OnPageIndexChanging="grdViewOutstanding_PageIndexChanging"
                            OnRowDataBound="grdViewOutstanding_RowDataBound" OnSorting="grdViewOutstanding_Sorting" OnSelectedIndexChanged="grdViewOutstanding_SelectedIndexChanged">
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                        <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="60" />
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
                            <asp:TemplateField SortExpression="carpet_title" ItemStyle-Width="80">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbcarpet_title" runat="server" Text="نوع" CommandName="Sort" CommandArgument="carpet_title" Width="80"></asp:LinkButton>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtcarpet_title" AutoPostBack="true" OnTextChanged="txt_TextChanged" Width="80"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("carpet_title") %>
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
</asp:Content>
