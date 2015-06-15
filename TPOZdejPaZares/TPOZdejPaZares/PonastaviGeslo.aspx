<%@ Page Title="Ponastavi geslo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PonastaviGeslo.aspx.cs" Inherits="TPOZdejPaZares.PonastaviGeslo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
        <tr>
            <td style="height: 21px"></td>
            <td class="text-left" style="height: 21px"></td>
            <td style="height: 21px"></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 23px">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="text-left" style="height: 23px">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td style="height: 23px"></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="text-left">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="text-left">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="text-left">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
