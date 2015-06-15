<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Predmeti.aspx.cs" Inherits="TPOZdejPaZares.Predmeti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
        <h3>Urejanje predmetov</h3>
    <br />
    <div class="row">    
        <div class="col-xs-12 col-sm-6 col-md-4" id="Div1" runat="server" style="width:40em;margin-right:2em;">
            <asp:Label runat="server" ID="feedbackLB" CssClass="label label-info"></asp:Label>
            <asp:GridView runat="server" ID="seznamPredmetovGV" class="table table-striped" AutoGenerateColumns="False"
                 DataKeyNames="SifraPredmeta" width="40em" OnRowEditing="seznamPredmetovGV_RowEditing" 
                 OnRowDeleting="seznamPredmetovGV_RowDeleting" OnRowCancelingEdit="seznamPredmetovGV_RowCancelingEdit"
                 OnRowUpdating="seznamPredmetovGV_RowUpdating">
             <Columns>
                 <asp:BoundField DataField="SifraPredmeta" HeaderText="Šifra predmeta" />
                <asp:BoundField DataField="Predmet" HeaderText="Predmet" />
                 <asp:BoundField DataField="Kreditne" HeaderText="Kreditne točke" />
                <asp:CommandField ShowEditButton="true" />
                <asp:CommandField ShowDeleteButton="true" />
            </Columns>
            </asp:GridView>
        </div>

        <div class="col-xs-12 col-sm-6 col-md-4" id="dodajPredmet" runat="server">
                <h4>Dodaj nov predmet</h4>
            <br />
            <div class="form-group">
                <asp:Label runat="server">Ime predmeta:</asp:Label>
                <asp:TextBox runat="server" ID="novPredmetImeTB" class="form-control"></asp:TextBox>
                <asp:Label runat="server">Šifra predmeta:</asp:Label>
                <asp:TextBox runat="server" ID="novPredmetSifraTB" class="form-control"></asp:TextBox>
                <asp:Label runat="server">Kreditne točke:</asp:Label>
                <asp:TextBox runat="server" ID="novPredmetKreditneTB" class="form-control"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="dodajPredmetButton" Text="Dodaj" OnClick="dodajPredmetButton_Click"/>
               <br />
               <asp:Label runat="server" ID="uspesnoDodanLB" Visible="false" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>
