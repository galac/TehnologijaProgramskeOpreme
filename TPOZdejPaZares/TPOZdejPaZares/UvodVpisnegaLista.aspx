<%@ Page Title="Zajem vpisnega lista" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UvodVpisnegaLista.aspx.cs" Inherits="TPOZdejPaZares.UvodVpisnegaLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h3>Zajem vpisnega lista</h3>
    <br />

    <div class="form-group">
        <label for="buttonNov">Če se prvič vpisujete, nadaljujte s klikom na spodnji gumb</label>
    </div>
    <asp:Button ID="buttonNov" runat="server" Text="Prvi vpis" type="submit" CssClass="btn btn-primary" OnClick="buttonNov_Click" />
    <br /><br /><br />

    <div class="form-group">
        <label for="buttonVpisna">Če ste že bili kdaj vpisani, vnesite vašo vpisno številko</label>
        <asp:TextBox  class="form-control" type = "text" id = "inputVpisna" placeholder="63123456" runat = "server" />
     </div>
     <asp:Button ID="buttonVpisna" runat="server" Text="Vstopi z vpisno številko" type="submit" CssClass="btn btn-primary" OnClick="buttonVpisna_Click" />
</asp:Content>
