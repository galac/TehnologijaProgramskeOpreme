<%@ Page Title="Uvažanje podatkov" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParsanjePodatkov.aspx.cs" Inherits="TPOZdejPaZares.ParsanjePodatkov" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h3>Izberite datoteko za uvoz podatkov</h3>
    <asp:FileUpload ID="FileForParseUpload" runat="server" CssClass='btn btn-default' />
    <br />
    <asp:Button ID="Btn_Parse" runat="server" CssClass="btn btn-primary" OnClick="Btn_Parse_Click" Text="Shrani študente iz datoteke" />
    <br />
    <asp:Label ID="LblError" runat="server" CssClass="label label-danger"></asp:Label>
    <br />
    <asp:Label ID="LblInfo" runat="server" CssClass="label label-success" ></asp:Label>
</asp:Content>
