<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentSearchDetailsREF.aspx.cs" Inherits="TPOZdejPaZares.StudentSearchDetailsREF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h3>Podrobnosti o študentu</h3>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CssClass="table table-striped" Font-Bold="False">
        <FieldHeaderStyle Font-Bold="True" />
        <Fields>
            <asp:TemplateField HeaderText="OSEBNI PODATKI">
                <HeaderStyle CssClass="h4" />
            </asp:TemplateField>
            <asp:BoundField DataField="ImeInPriimek" HeaderText="Ime in priimek" />
            <asp:BoundField DataField="vpisnaStudenta" HeaderText="Vpisna številka" />
            <asp:BoundField DataField="mailStudenta" HeaderText="E-mail" />
            <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
            <asp:BoundField DataField="Spol" HeaderText="Spol" />
            <asp:BoundField DataField="EMSO" HeaderText="EMŠO" />
            <asp:BoundField DataField="DavcnaStevilka" HeaderText="Davčna številka" />
            <asp:TemplateField></asp:TemplateField>
            <asp:TemplateField HeaderText="PODATKI O ROJSTVU">
                <HeaderStyle CssClass="h4" />
            </asp:TemplateField>
            <asp:BoundField DataField="DatumRojstva" HeaderText="Datum rojstva" />
            <asp:BoundField DataField="DrzavaRojstva" HeaderText="Država rojstva" />
            <asp:BoundField DataField="Drzavljanstvo" HeaderText="Državljanstvo" />
            <asp:TemplateField></asp:TemplateField>
            <asp:TemplateField HeaderText="PODATKI O STALNEM PREBIVALIŠČU">
                <HeaderStyle CssClass="h4" />
            </asp:TemplateField>
            <asp:BoundField DataField="Naslov" HeaderText="Stalni naslov" />
            <asp:BoundField DataField="Posta" HeaderText="Pošta" />
            <asp:BoundField DataField="posta_idPosta" HeaderText="Poštna številka" />            
            <asp:BoundField DataField="Obcina" HeaderText="Občina" />
            <asp:BoundField DataField="Drzava" HeaderText="Država" />
            <asp:TemplateField></asp:TemplateField>
            <asp:TemplateField HeaderText="PODATKI O ZAČASNEM PREBIVALIŠČU">
                <HeaderStyle CssClass="h4" />
            </asp:TemplateField>
            <asp:BoundField DataField="NaslovZacasni" HeaderText="Začasni naslov" />
            <asp:BoundField DataField="ZacasnaPosta" HeaderText="Pošta" />
            <asp:BoundField DataField="ZacasnaPostnaStevilka" HeaderText="Poštna številka" />            
            <asp:BoundField DataField="ZacasnaObcina" HeaderText="Občina" />
            <asp:BoundField DataField="ZacasnaDrzava" HeaderText="Država" />
        </Fields>
        <HeaderStyle Font-Bold="False" />
    </asp:DetailsView>
    <br />
    <h3>Podrobnosti o vpisih študenta po letih</h3>
    <asp:Label ID="LblErrorA" runat="server" CssClass="label label-info" Visible="False">V bazi ni vpisov za študenta.</asp:Label>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    </asp:PlaceHolder>
    <br />
    <h3>Podrobnosti o sklepih o študentu</h3>
    <asp:Label ID="LblErrorB" runat="server" CssClass="label label-info" Visible="False">V bazi ni sklepov za študenta.</asp:Label>
    <asp:GridView ID="GV_sklepi" class="table table-striped" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="idSklep" HeaderText="ID SKLEPA" />
            <asp:BoundField DataField="Organ" HeaderText="ORGAN" />
            <asp:BoundField DataField="VsebinaSklepa" HeaderText="VSEBINA SKLEPA" />
            <asp:BoundField DataField="DatumSprejetjaSklepa" HeaderText="DATUM SPREJETJA" DataFormatString="{0:dd.MM.yyyy}" />
            <asp:BoundField DataField="DatumVeljaveSklepa" HeaderText="DATUM VELJAVE" DataFormatString="{0:dd.MM.yyyy}" />
        </Columns>

    </asp:GridView>
</asp:Content>
