<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IzbiraPredmetov.aspx.cs" Inherits="TPOZdejPaZares.IzbiraPredmetov" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>   
            <br />
                <h3>Izbor predmetov</h3>
            <br />

            <div class="row" id="predmetnikPrviLetnik" runat="server" style="display:none;">
                    <%--<asp:Label runat="server" ID="Label1" Visible="false" CssClass="label label-info"></asp:Label>--%>
                    <br />
                    <asp:GridView runat="server" ID="predmetnikPrviLetnikGV" class="table table-striped" Width="650px" Visible="false" AutoGenerateColumns="false"
                        DataKeyNames="SifraPredmeta, Kreditne, Ocena, IdPredmeta, IdVpisanegaPredmeta, SestavniDelPred" OnRowDataBound="predmetnikPrviLetnikGV_RowDataBound">
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
                    </Columns>
                    </asp:GridView>
            </div>

            <div class="row">
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
                    <asp:Button runat="server" ID="potrdiDrugiButton" CssClass="btn btn-primary" Text="Potrdi" OnClick="potrdiDrugiButton_Click"/>
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

                    <asp:Button runat="server" ID="potrdiTretjiButton" Text="Potrdi" CssClass="btn btn-primary" OnClick="potrdiTretjiButton_Click"/>
                </div>

                <div class="col-xs-12 col-sm-6 col-md-4" id="tretjiLetnikPovprecjeDiv" Style="display:none;" runat="server">

                    <asp:Label runat="server">Označi željene predmete:</asp:Label>
                    <br />
                    <asp:CheckBoxList runat="server" ID="tretjiPovprecjeCBL" AutoPostBack="true" OnSelectedIndexChanged="tretjiPovprecjeCBL_SelectedIndexChanged"></asp:CheckBoxList>
                    <br />
                    <asp:Label runat="server" ID="kTockeLB"></asp:Label>
                    <br />
                    <asp:Button runat="server" ID="potrdiTretjiPovprecjeButton" Text="Potrdi" CssClass="btn btn-primary" OnClick="potrdiTretjiPovprecjeButton_Click" />
                </div>

                <asp:Button runat="server" ID="vpisniListBTN" Text="Izpiši vpisni list" CssClass="btn btn-primary" OnClick="vpisniListBTN_Click" Visible="false" />

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
