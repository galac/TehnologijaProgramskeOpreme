<%@ Page Title="Prijava" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="TPOZdejPaZares.LoginForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" CssClass="h2" Text="Login"></asp:Label>
<br />
<table class="nav-justified table ">
    <tr class ="form-group">
        <td class="text-right">
            <label for="inputIme">Uporabnisko ime</label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxUser" class="form-control" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUser" runat="server" ControlToValidate="TextBoxUser" CssClass="label label-danger" EnableTheming="True" ErrorMessage="Prosim vnesite uporabinsko ime"></asp:RequiredFieldValidator>
       
            <asp:Label ID="label_napacniUser" runat="server" CssClass="label label-danger"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="text-right">
            <label for="inputIme">Geslo</label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxPassword" class="form-control" runat="server" TextMode="Password" Width="180px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPass" runat="server" ControlToValidate="TextBoxPassword" CssClass="label label-danger" ErrorMessage="Prosim vnesite geslo"></asp:RequiredFieldValidator>
  
            <asp:Label ID="label_napacnoGeslo" runat="server" CssClass="label label-danger  "></asp:Label>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <input type="hidden" id="hdn" runat="server" value="0" />
            <asp:Button ID="Button_login" runat="server" OnClick="Button_login_Click" Text="Prijava" CssClass="btn btn-primary" />
        </td>
        <td>
            &nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Pozabljeno geslo" CssClass="btn btn-primary" />
        </td>
        <td>
            &nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>
<br />
<br />
<br />
</asp:Content>
