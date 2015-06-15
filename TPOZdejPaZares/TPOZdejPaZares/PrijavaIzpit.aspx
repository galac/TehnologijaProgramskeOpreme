<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrijavaIzpit.aspx.cs" Inherits="TPOZdejPaZares.PrijavaIzpit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <br />
<br />
    <br />
    <br />
    
       <asp:GridView ID="GVIzbiraStudenta" runat="server" AllowSorting="True" class="table table-striped" AutoGenerateColumns="False" DataKeyNames="idStudent" SortExpression="priimekStudenta" Enabled="False" OnSelectedIndexChanged="GVIzbiraStudenta_SelectedIndexChanged" Visible="False">
        <Columns>
            <asp:BoundField DataField="idStudent" HeaderText="idStudent" Visible="false" ReadOnly="True" SortExpression="idStudent" />
            <asp:BoundField DataField="imeStudenta" HeaderText="Ime" ReadOnly="True" SortExpression="imeStudenta" />
            <asp:BoundField DataField="priimekStudenta" HeaderText=" Priimek" ReadOnly="True" SortExpression="priimekStudenta" />
            <asp:BoundField DataField="mailStudenta" HeaderText=" Email" ReadOnly="True" SortExpression="mailStudenta" />
            <asp:BoundField DataField="vpisnaStudenta" HeaderText="Vpisna Številka" ReadOnly="True" SortExpression="vpisnaStudenta" />
            <asp:CommandField ShowSelectButton="True" SelectText="Izberi" />
        </Columns>
    </asp:GridView>
    <br />

  
    <asp:Label ID="Label3" runat="server" Text="Razpisani roki"></asp:Label>
<asp:GridView ID="GVRoki" runat="server" AutoGenerateColumns="False" class="table table-striped" DataKeyNames="idVpisaP, idRoka" OnSelectedIndexChanged="GVRoki_SelectedIndexChanged" SelectedIndex="0" CellSpacing="-1" SortExpression="Datum" Width="778px">
<Columns>


    <asp:BoundField DataField="imePredmeta" HeaderText="Ime" />
    <asp:BoundField DataField="izvajalecPredmeta" HeaderText="Ime" />
    <asp:BoundField DataField="izvajalecPriimek" HeaderText="" />
    <asp:BoundField DataField="vrstaRoka" HeaderText="Vrsta roka" />
    <asp:BoundField DataField="Datum"  HeaderText="Datum" />
    <asp:BoundField DataField="prostor" HeaderText="Prostor" />
    <asp:BoundField DataField="idRoka" Visible="false"  HeaderText="idRoka"/>
    <asp:BoundField DataField="idVpisaP" Visible="false"  HeaderText="idVpisaP"/>
    <asp:CommandField SelectText="Prijavi se na rok" ShowSelectButton="True" />
</Columns>
<SelectedRowStyle BorderColor="#2C3E50" BorderStyle="Solid" />
</asp:GridView>
    <div runat="server" id="DivPrijava">
    <br />
<asp:Button ID="BPrijava" runat="server" OnClick="BPrijava_Click" Text="Prijava" CssClass="btn-default active" />
<asp:Button ID="BOdjava" runat="server" OnClick="BOdjava_Click" Text="Odjava" CssClass="btn-default disabled" />
    <br />
<asp:Label ID="LSteviloPrijav" runat="server" Text=""></asp:Label>
    <br />
<asp:Label ID="LabelPrijava" runat="server" Text="Label"></asp:Label>
    <br />
<asp:Label ID="LSpremembe" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Vsi predmeti"></asp:Label>
      <br />
    </div>

<asp:GridView ID="GVStariRoki" runat="server" AutoGenerateColumns="False" class="table table-striped" DataKeyNames="idVpisaP" SelectedIndex="-1" CellSpacing="-1" SortExpression="Datum">
<Columns>


    <asp:BoundField DataField="imePredmeta" HeaderText="Ime" />
    <asp:BoundField DataField="izvajalecPredmeta" HeaderText="Ime" />
    <asp:BoundField DataField="izvajalecPriimek" HeaderText="" />
    <asp:BoundField DataField="Ocena"  HeaderText="Ocena" />
    <asp:BoundField DataField="idVpisaP" Visible="false"  HeaderText="idVpisaP"/>
</Columns>
<SelectedRowStyle BorderColor="#2C3E50" BorderStyle="Solid" />
</asp:GridView>

</asp:Content>

