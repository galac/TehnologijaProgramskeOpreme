<%@ Page Title="Pozabljeno geslo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PozabljenoGeslo.aspx.cs" Inherits="TPOZdejPaZares.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <input type="hidden" id="hdn" runat="server" value="0" />
    <br />
    <asp:Label ID="Label4" runat="server" CssClass="h2" Text="Pozabljeno geslo"></asp:Label>
    <br />
    <table class="nav-justified" style="padding: 5px">
        <tr>
            <td style="padding: 6px;" class="text-right">
                <asp:Label ID="Label1" runat="server" Text="Uporabnisko ime" style="text-align: right"></asp:Label>
            </td>
            <td style="padding: 6px">
                <asp:TextBox ID="TextBox_User" runat="server" Width="135px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vnesite uporabnisko ime" ControlToValidate="TextBox_priimek" CssClass="label-warning"></asp:RequiredFieldValidator>
            </td>
            <td style="height: 26px">
                <asp:Label ID="label_pozabljeno" runat="server" CssClass="label-warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 6px;" class="text-right">
                <asp:Label ID="Label2" runat="server" style="text-align: right" Text="Ime"></asp:Label>
            </td>
            <td style="padding: 6px">
                <asp:TextBox ID="TextBox_ime" runat="server" Width="135px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vnesite ime" ControlToValidate="TextBox_priimek" CssClass="label-warning"></asp:RequiredFieldValidator>
            </td>
            <td style="height: 42px">
                </td>
        </tr>
        <tr>
            <td style="padding: 6px;" class="text-right">
                <asp:Label ID="Label3" runat="server" style="text-align: right" Text="Priimek"></asp:Label>
            </td>
            <td style="padding: 6px;">
                <asp:TextBox ID="TextBox_priimek" runat="server" Width="135px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vnesite priimek" ControlToValidate="TextBox_priimek" CssClass="label-warning"></asp:RequiredFieldValidator>
            </td>
            <td style="height: 24px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="padding: 6px">
                &nbsp;</td>
            <td rowspan="1" style="padding: 6px">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="PonastaviGeslo" CssClass="btn btn-primary" />
            </td>
            <td style="height: 61px">
                </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
</table>
</asp:Content>
