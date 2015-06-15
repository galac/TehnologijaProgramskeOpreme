<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PotrditevVpisa.aspx.cs" Inherits="TPOZdejPaZares.PotrditevVpisa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <br />
    <br />
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Text="Vpisani Studenti"></asp:Label>
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" class="table table-striped" DataKeyNames="idVpisa" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Ime" HeaderText="Ime" />
            <asp:BoundField DataField="Priimek" HeaderText="Priimek" />
            <asp:BoundField DataField="VpisnaStevilka" HeaderText="VpisnaStevilka" />
            <asp:BoundField DataField="idStudent" Visible="false" />
            <asp:BoundField DataField="idVpisa" Visible="false" />
            <asp:CommandField SelectText="Preglej vpis" ShowSelectButton="True" />
        </Columns>
        <SelectedRowStyle BorderColor="#2C3E50" BorderStyle="Solid" />
    </asp:GridView>
    <asp:Label ID="Label4" runat="server" Text="Nepotrjeni vpisi"></asp:Label>
    <asp:GridView ID="GridView2" runat="server"  AutoGenerateColumns="False" class="table table-striped" DataKeyNames="idVpisa" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Ime" HeaderText="Ime" />
            <asp:BoundField DataField="Priimek" HeaderText="Priimek" />
            <asp:BoundField DataField="VpisnaStevilka" HeaderText="VpisnaStevilka" />
            <asp:BoundField DataField="idStudent" Visible="false" />
            <asp:BoundField DataField="idVpisa" Visible="false" />
            <asp:CommandField SelectText="Preglej vpis" ShowSelectButton="True" />
        </Columns>
        <SelectedRowStyle BorderColor="#2C3E50" BorderStyle="Solid" />
    </asp:GridView>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" Height="50px" Width="213px" DataKeyNames="idVpis">
        <Fields>
            <asp:BoundField DataField="idVpis" HeaderText="ID Vpisa" ReadOnly="True" SortExpression="idVpis" Visible ="false" />
            <asp:BoundField DataField="VrstaVpisa_idVrstaVpisa" HeaderText="Vrsta Vpisa" SortExpression="VrstaVpisa_idVrstaVpisa" />
            <asp:BoundField DataField="OblikaStudija_idOblikaStudija" HeaderText="Oblika Studija" SortExpression="OblikaStudija_idOblikaStudija" />
            <asp:BoundField DataField="Letnik_idLetnik" HeaderText="Letnik" SortExpression="Letnik_idLetnik" />
            <asp:BoundField DataField="StudijskiProgram_idStudijskiProgram" HeaderText="Studijski Program" SortExpression="StudijskiProgram_idStudijskiProgram" />
            <asp:BoundField DataField="NacinStudija_idNacinStudija" HeaderText="Nacin Studija" SortExpression="NacinStudija_idNacinStudija" />
            <asp:BoundField DataField="Student_idStudent1" HeaderText="Student_idStudent1" SortExpression="Student_idStudent1" Visible ="false"/>
            <asp:BoundField DataField="Potrjen" HeaderText="Potrjen" SortExpression="Potrjen" />
            <asp:BoundField DataField="StudijskoLetoDV" HeaderText="StudijskoLeto" SortExpression="StudijskoLeto" />
        </Fields>
    </asp:DetailsView>
    <asp:DetailsView ID="DetailsView2" runat="server" AutoGenerateRows="False" DataKeyNames="idVpis" Height="50px" Width="125px" >
        <Fields>
            <asp:BoundField DataField="idVpis" HeaderText="idVpis" ReadOnly="True" SortExpression="idVpis" />
            <asp:BoundField DataField="VrstaVpisa_idVrstaVpisa" HeaderText="VrstaVpisa_idVrstaVpisa" SortExpression="VrstaVpisa_idVrstaVpisa" />
            <asp:BoundField DataField="OblikaStudija_idOblikaStudija" HeaderText="OblikaStudija_idOblikaStudija" SortExpression="OblikaStudija_idOblikaStudija" />
            <asp:BoundField DataField="Letnik_idLetnik" HeaderText="Letnik_idLetnik" SortExpression="Letnik_idLetnik" />
            <asp:BoundField DataField="StudijskiProgram_idStudijskiProgram" HeaderText="StudijskiProgram_idStudijskiProgram" SortExpression="StudijskiProgram_idStudijskiProgram" />
            <asp:BoundField DataField="NacinStudija_idNacinStudija" HeaderText="NacinStudija_idNacinStudija" SortExpression="NacinStudija_idNacinStudija" />
            <asp:BoundField DataField="Student_idStudent1" HeaderText="Student_idStudent1" SortExpression="Student_idStudent1" />
            <asp:BoundField DataField="Potrjen" HeaderText="Potrjen" SortExpression="Potrjen" />
            <asp:BoundField DataField="StudijskoLeto" HeaderText="StudijskoLeto" SortExpression="StudijskoLeto" />
        </Fields>
    </asp:DetailsView>
    <asp:Button ID="bPotrdiVpis" runat="server" OnClick="bPotrdiVpis_Click" Text="Potrdi Vpis" Visible="False" />
    <asp:Button ID="bPotrdiloOVpisu" runat="server" OnClick="bPotrdiloOVpisu_Click" Text="Potrdilo O Vpisu" Visible="False" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    </asp:Content>
