<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesWebForm.aspx.cs" Inherits="WebApplicationKartable.SalesWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @font-face {
            src: url(farsiwebfont.ttf);
            font-family: FarsiWebFont;
        }

        .textbox {
            font-family: FarsiWebFont;
            line-height: normal;
            border-color: steelblue;
            background-color: #39cccc;
            color: black;
        }

        .dropdown1 {
            font-family: FarsiWebFont;
            line-height: normal;
            border-color: steelblue;
            background-color: lawngreen;
            color: black;
        }

        .GridStart {
            direction: rtl;
            text-align: right;
            line-height: normal;
            text-align: right;
            font-family: FarsiWebFont;
            font-size: 14px;
            font-weight: bold;
            border-color: lemonchiffon;
            color: black;
        }

        .GridText {
            line-height: normal;
            font-size: 14px;
            border-color: greenyellow;
            color: black;
            font-family: FarsiWebFont;
        }

        .lbl_alarm {
            font-size: 20px;
            font-weight: bolder;
            background-color: Aqua;
            border-radius: 25px;
            border-color: steelblue;
            text-align: center;
        }

        .lnk {
            color: #003300;
            font-size: 22px;
        }
            .lnk:hover {
                color: White;
            }
    </style>
</head>
<body style="text-align:center;direction:rtl">

    <form id="form1" runat="server">

        <table style="width:100%">
        <tr>
            <td class="float-right" style="width:50%">
                <span>کد فرش :</span>
                <asp:TextBox TextMode="Number" ID="txt_product" runat="server" Width="150"  CssClass="textbox"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_list" runat="server" OnClick="btn_list_Click" Text="نمایش" CssClass="btn-facebook" />
            </td>
            <td style="width:50%;direction:ltr">
                <asp:Button ID="btn_logout" runat="server" OnClick="btn_logout_Click" Text="خروج" CssClass="btn-facebook" />
            </td>
        </tr>
    </table> 
    <hr />
<table>
    <tr>
        <td>
            <asp:Image ID="image1" runat="server" Height="300" Width="350" />
        </td>
    </tr>
    <tr>
        <td>
            <table>
        <tr>
            <td>کد تامین کننده :</td>
            <td>
            <asp:TextBox ID="txt_pcode" runat="server"  CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
        </td>
        </tr>
        <tr>
            <td>نوع فرش :</td>
            <td><asp:TextBox ID="txt_carpet_type" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>محل بافت :</td>
            <td><asp:TextBox ID="txt_brand_name" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>اندازه :</td>
            <td><asp:TextBox ID="txt_size" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>نقشه :</td>
            <td><asp:TextBox ID="txt_plan" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>رنگ زمینه :</td>
            <td><asp:TextBox ID="txt_color" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>رنگ حاشیه :</td>
            <td><asp:TextBox ID="txt_margin" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>نوع پرز :</td>
            <td><asp:TextBox ID="txt_porz" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>نوع چله :</td>
            <td><asp:TextBox ID="txt_chele" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
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
            <td>قیمت فروش :</td>
            <td><asp:TextBox ID="txt_sale" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>تخفیف نمایشگاه :</td>
            <td><asp:TextBox ID="txt_discount" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>
                قیمت نهایی :
            </td>
            <td>
                <asp:TextBox ID="txt_final_payment" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>

</table>
        </td>
    </tr>
</table>
    </form>
</body>
</html>
