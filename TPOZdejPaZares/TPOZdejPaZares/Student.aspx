<%@ Page Title="Iskanje študentov" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="TPOZdejPaZares.Student1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h3>Išči po imenu in priimku</h3>
    <div class="form-group">
        <label for="inputIme">Ime</label>
        <asp:TextBox  class="form-control" type = "text" id = "inputIme" placeholder="Janez" runat = "server" />
    </div>
    <div class="form-group">
        <label for="inputPriimek">Priimek</label>
        <asp:TextBox  class="form-control" type = "text" id = "inputPriimek" placeholder="Novak" runat = "server" />
    </div>
        
    <asp:Button ID="buttonIme" runat="server" Text="Išči po imenu in priimku" type="submit" OnClick="buttonIme_Click" CssClass="btn btn-primary" />
    <h3>Išči po vpisni številki</h3>
    <div class="form-group">
        <label for="inputVpisna">Vpisna številka</label>
        <asp:TextBox  class="form-control" type = "text" id = "inputVpisna" placeholder="63120168" runat = "server" />
    </div>
    <asp:Button ID="buttonVpisna" runat="server" Text="Išči po vpisni številki" type="submit" OnClick="buttonVpisna_Click" CssClass="btn btn-primary" />
    <br /><br />
    <asp:GridView ID="GridViewIme" class="table table-striped" runat="server" OnSelectedIndexChanged="GridViewIme_SelectedIndexChanged" >
        <Columns>
            <asp:CommandField SelectText="podrobnosti" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>

    <asp:Button ID="buttonPdf" runat="server" Text="Shrani kot PDF" type="submit" OnClick="buttonExportToPdf_click" CssClass="btn btn-primary" Visible="false" />
    <asp:Button ID="buttonCsv" runat="server" Text="Shrani kot CSV" type="submit" OnClick="buttonExportToCsv_click" CssClass="btn btn-primary" Visible="false" />
    
    <asp:Label ID="LabelOpozorilo" runat="server" Text="Najden ni bil nobeden študent" class="label label-danger" Visible="False"></asp:Label>

</asp:Content>
