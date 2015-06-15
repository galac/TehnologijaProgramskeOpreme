<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IzbiraPredmetovReferent2.aspx.cs" Inherits="TPOZdejPaZares.IzbiraPredmetovReferent2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
        <h3>Ustvari/spremeni predmetnik izbranega študenta</h3>
    <br />
    <div class="row" id="spremeniPredmetnik" runat="server" style="display:none;">
            <asp:Label runat="server" ID="feedbackLB" Visible="false" CssClass="label label-info"></asp:Label>
            <br />
            <asp:GridView runat="server" ID="seznamPredmetovGV" class="table table-striped" Width="650px" Visible="true" AutoGenerateColumns="false"
                DataKeyNames="SifraPredmeta, Kreditne, Ocena, IdPredmeta, IdVpisanegaPredmeta, SestavniDelPred" OnRowDataBound="seznamPredmetovGV_RowDataBound"
                OnRowEditing="seznamPredmetovGV_RowEditing" OnRowCancelingEdit="seznamPredmetovGV_RowCancelingEdit"
                OnRowDeleting="seznamPredmetovGV_RowDeleting">
             <Columns>
                 <asp:BoundField DataField="SifraPredmeta" HeaderText="Šifra predmeta"/>
                 <asp:BoundField DataField="Predmet" HeaderText="Predmet" />
                 <asp:BoundField DataField="Kreditne" HeaderText="Kreditne točke" />
                 <asp:BoundField DataField="Letnik" HeaderText="Letnik" />
                 <asp:BoundField DataField="Del predmeta" HeaderText="Del predmeta" />
                 <asp:BoundField DataField="Ocena" HeaderText="Ocena" />
                 <asp:BoundField DataField="IdPredmeta" HeaderText="IDPredmet" />
                 <asp:BoundField DataField="IdVpisanegaPredmeta" HeaderText="IDVpisaniPredmet" />
                 <asp:BoundField DataField="SestavniDelPred" HeaderText="SestavniDelPred" />
                 <asp:CommandField ShowEditButton="true" EditText="Spremeni"/>
                 <asp:CommandField ShowDeleteButton="true" DeleteText="Odstrani"/>
            </Columns>
            </asp:GridView>
        <asp:Label runat="server" ID="sestevekKreditnihTockLB"></asp:Label>
        <div runat="server" id="novPredmetDiv" style="display:none;">
            <asp:DropDownList runat="server" ID="dodajProstoIzbirniPredmetDDL_3kt" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;" ></asp:DropDownList>
            <asp:Button runat="server" ID="dodajProstoIzbirniPredmetBtn" Text="Dodaj" OnClick="dodajProstoIzbirniPredmetBtn_Click"/>
            <asp:Label runat="server" ID="dodajProstoIzbirniPredmetErrorLB" Text="Prosimo izberite predmet!" Visible="false" CssClass="label label-info"></asp:Label>
        </div>
    </div>

    <div class="row" id="spremeniPredmet" runat="server" style="display:none;">
        <asp:Label runat="server" ID="spremeniPredmetInfoLB">Spreminjate predmet</asp:Label>
        <br />
        <asp:DropDownList runat="server" ID="spremeniPredmetSeznamOpcijDDL" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;" 
            AutoPostBack="true" OnSelectedIndexChanged="spremeniPredmetSeznamOpcijDDL_SelectedIndexChanged"></asp:DropDownList>
        <br />
        <asp:Button runat="server" ID="potrdiSpremeniPredmet" Text="Spremeni" OnClick="potrdiSpremeniPredmet_Click"/>
        <asp:Button runat="server" ID="zavrziSpremeniPredmet" Text="Zavrzi spremembe" OnClick="zavrziSpremeniPredmet_Click"/>
        <asp:Label runat="server" ID="potrdiSpremeniPredmetErrorLB" Text="Prosimo izberite predmet!" Visible="false" CssClass="label label-info"></asp:Label>
    </div>

    <div class="row" id="novPredmetnik" runat="server" style="display:none;">
        <div id="drugiLetnikDiv" class="col-md-6" Style="display:none;" runat="server">
            <asp:Label runat="server">Izberi strokovni izbirni predmet:</asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="drugiStrokovniIzbirniDDL" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"
                AutoPostBack="true" OnSelectedIndexChanged="drugiStrokovniIzbirniDDL_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label runat="server" ID="drugiStrokovniIzbirniErrorLB" CssClass="label label-info" Visible="false" Text="Izberite predmet!"></asp:Label>
            <br />
            <asp:Label runat="server">Izberi prosto izbirni predmet:</asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="drugiProstoIzbirniDDL" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"
                AutoPostBack="true" OnSelectedIndexChanged="drugiProstoIzbirniDDL_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label runat="server" ID="drugiProstoIzbirniErrorLB" CssClass="label label-info" Visible="false" Text="Izberite predmet!"></asp:Label>
            <br />
            <asp:Label runat="server" ID="drugiProstoIzbirniLB_3kt" Visible="false">Izberi drugi prosto izbirni predmet:</asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="drugiProstoIzbirniDDL_3kt" Visible="false" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"
                AutoPostBack="true" OnSelectedIndexChanged="drugiProstoIzbirniDDL_3kt_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label runat="server" ID="drugiProstoIzbirni_3kt_ErrorLB" CssClass="label label-info" Visible="false" Text="Izberite predmet!"></asp:Label>
            <br />
            <asp:Button runat="server" ID="potrdiDrugiButton" Text="Potrdi" OnClick="potrdiDrugiButton_Click"/>
        </div>

        <div class="col-xs-12 col-sm-6 col-md-4" id="tretjiLetnikDiv" Style="display:none;" runat="server">
            <asp:Label runat="server">Izberi prvi modul:</asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="tretjiPrviModulDDL" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"
                AutoPostBack="true" OnSelectedIndexChanged="tretjiPrviModulDDL_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label runat="server" ID="tretjiPrviModulErrorLB" CssClass="label label-info" Visible="false" Text="Izberite modul!"></asp:Label>
            <br />
            <asp:Label runat="server">Izberi drugi modul:</asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="tretjiDrugiModulDDL" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"
                AutoPostBack="true" OnSelectedIndexChanged="tretjiDrugiModulDDL_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label runat="server" ID="tretjiDrugiModulErrorLB" CssClass="label label-info" Visible="false" Text="Izberite modul!"></asp:Label>
            <br />
            <asp:Label runat="server">Izberi prosto izbirni predmet:</asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="tretjiProstoIzbirniDDL" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"
                AutoPostBack="true" OnSelectedIndexChanged="tretjiProstoIzbirniDDL_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label runat="server" ID="tretjiProstoIzbirniErrorLB" CssClass="label label-info" Visible="false" Text="Izberite predmet!"></asp:Label>
            <br />
            <asp:Label runat="server" ID="tretjiProstoIzbirniLB_3kt" Visible="false">Izberi drugi prosto izbirni predmet:</asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="tretjiProstoIzbirniDDL_3kt" Visible="false" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"
                AutoPostBack="true" OnSelectedIndexChanged="tretjiProstoIzbirniDDL_3kt_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label runat="server" ID="tretjiProstoIzbirni_3kt_ErrorLB" CssClass="label label-info" Visible="false" Text="Izberite predmet!"></asp:Label>
            <br />

            <asp:Button runat="server" ID="potrdiTretjiButton" Text="Potrdi" OnClick="potrdiTretjiButton_Click"/>
        </div>

        <div class="col-xs-12 col-sm-6 col-md-4" id="tretjiLetnikPovprecjeDiv" Style="display:none;" runat="server">

            <asp:Label runat="server">Označi željene predmete:</asp:Label>
            <br />
            <asp:CheckBoxList runat="server" ID="tretjiPovprecjeCBL" AutoPostBack="true" OnSelectedIndexChanged="tretjiPovprecjeCBL_SelectedIndexChanged"></asp:CheckBoxList>
            <br />
            <asp:Label runat="server" ID="kTockeLB"></asp:Label>
            <br />
            <asp:Button runat="server" ID="potrdiTretjiPovprecjeButton" Text="Potrdi" OnClick="potrdiTretjiPovprecjeButton_Click" />
        </div>

    </div>
</asp:Content>
