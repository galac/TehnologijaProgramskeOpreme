<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UvodVKartotecniList.aspx.cs" Inherits="TPOZdejPaZares.Referent.UvodVKartotecniList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h3>Izpis kartotečnega lista</h3>
    <br /><br />

    <div class="form-group">
        <label for="buttonVpisna">Vnesite vpisno številko študenta:</label>
        <asp:TextBox  class="form-control" type = "text" id = "inputVpisna" placeholder="63123456" runat = "server" />
     </div>
     <asp:Button ID="buttonVpisna" runat="server" Text="Vstopi z vpisno številko" type="submit" CssClass="btn btn-primary" OnClick="buttonVpisna_Click" />

</asp:Content>
