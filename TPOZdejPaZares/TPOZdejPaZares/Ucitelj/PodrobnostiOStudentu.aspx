<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PodrobnostiOStudentu.aspx.cs" Inherits="TPOZdejPaZares.Ucitelj.PodrobnostiOStudentu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="http://cdn.syncfusion.com/13.1.0.21/js/web/flat-azure/ej.web.all.min.css" rel="stylesheet" />
    <!--scripts-->
    <script src=" http://cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js "></script>
    <script src="http://ajax.aspnetcdn.com/ajax/globalize/0.1.1/globalize.min.js"></script>
    <script src=" http://cdn.syncfusion.com/js/assets/external/jquery.easing.1.3.min.js "></script>
    <script src="http://cdn.syncfusion.com/13.1.0.21/js/web/ej.web.all.min.js"> </script>

    <script type="text/javascript">
        function setDatepickers()
        {
            $('.dummy').ejDatePicker({
                buttonText: "Izberite datum",
                watermarkText: "Vnesite datum",
                dateFormat: "dd.MM.yyyy",
                value: "",
                height: 43,
                width: "100%",
                locale: "sl-SI"
            });
            $('.dummy-time').ejTimePicker({
                timeFormat: "hh:mm tt",
                height: 43,
                width: "100%"
            });
            $('.dummy-number').ejNumericTextbox({
                height: 43,
                width: "100%"
            });
        }
    </script>


     <br />
    <h3>Podrobnosti o študentu</h3>
    <asp:DetailsView ID="DetailsViewPRO" runat="server" AutoGenerateRows="False" CssClass="table table-striped" Font-Bold="False">
        <FieldHeaderStyle Font-Bold="True" />
        <Fields>
            <asp:TemplateField HeaderText="OSEBNI PODATKI">
                <HeaderStyle CssClass="h4" />
            </asp:TemplateField>
            <asp:BoundField DataField="vpisnaStudenta" HeaderText="Vpisna številka" />
            <asp:BoundField DataField="ImeInPriimek" HeaderText="Priimek in ime" />   
            <asp:BoundField DataField="Telefon" HeaderText="Telefonska številka" />         
            <asp:BoundField DataField="mailStudenta" HeaderText="E-mail" />            
        </Fields>
        <HeaderStyle Font-Bold="False" />
    </asp:DetailsView>
    <br />
    <h3>Podrobnosti o sklepih o študentu</h3>
    <asp:Label ID="LblErrorBpro" runat="server" CssClass="label label-info" Visible="False">V bazi ni sklepov za študenta.</asp:Label>
    <asp:GridView ID="GV_sklepiPRO" class="table table-striped" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="idSklep" HeaderText="ID SKLEPA" />
            <asp:BoundField DataField="Organ" HeaderText="ORGAN" />
            <asp:BoundField DataField="VsebinaSklepa" HeaderText="VSEBINA SKLEPA" />
            <asp:BoundField DataField="DatumSprejetjaSklepa" HeaderText="DATUM SPREJETJA" DataFormatString="{0:dd. MM. yyyy}" />
            <asp:BoundField DataField="DatumVeljaveSklepa" HeaderText="DATUM VELJAVE" DataFormatString="{0:dd. MM. yyyy}" />
        </Columns>
    </asp:GridView>
    <br />
    <h3>Vnos individualne ocene</h3>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="DDL_VpisaniPredmeti" runat="server" 
                            AutoPostBack="True" OnSelectedIndexChanged="DDL_VpisaniPredmeti_SelectedIndexChanged"></asp:DropDownList>
             <asp:Label ID="LB_ErrorVpisaniP" runat="server" CssClass="label label-info" Visible="False" ></asp:Label>
             <br /><br /><br />

             <asp:GridView ID="GV_izpitniRoki" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" ShowFooter="true" 
                 OnRowCommand="GV_izpitniRoki_RowCommand" DataKeyNames="IDIzpitniRok,IzvedbaPredmeta_idIzvedbaPredmeta" >
                <Columns>
                    <asp:BoundField DataField="IDIzpitniRok" Visible="false" />
                    <asp:BoundField DataField="IzvedbaPredmeta_idIzvedbaPredmeta" Visible="false" />

                    <asp:TemplateField HeaderText="Št.">
                        <ItemTemplate>
                            <asp:Label ID="L_ZaporednaStevilka" runat="server" Text='<%# Eval("ZaporednaStevilka") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vrsta">
                        <ItemTemplate>
                            <asp:Label ID="L_VrstaIzpitnegaRoka" runat="server" Text='<%# Eval("VrstaIzpitnegaRoka") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="L_VrstaIzpitnegaRokaF" runat="server" Text="MIMO ROKA"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Datum">
                        <ItemTemplate>
                            <asp:Label ID="L_DatumIzpitnegaRoka" runat="server" Text='<%# Eval("DatumIzpitnegaRoka","{0:dd. MM. yyyy}") %>' Width="100px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_DatumIzpitnegaRoka" runat="server" CssClass="dummy form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_DatumIzpitnegaRoka" runat="server" ControlToValidate="TB_DatumIzpitnegaRoka" ErrorMessage="Vnesite legalen datum (dd.mm.llll)." ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"> </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFV_DatumIzpitnegaRoka" runat="server" ErrorMessage="Obvezno polje." ControlToValidate="TB_DatumIzpitnegaRoka" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Izberite izvedbo">
                        <FooterTemplate>
                             <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="DDL_izvedba" runat="server" >
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vnos končne ocene">
                        <ItemTemplate>
                            <asp:TextBox ID="TB_KoncnaOcena" runat="server" CssClass="form-control" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_Ura" runat="server" ControlToValidate="TB_KoncnaOcena" ErrorMessage="Vnesite 1 - 10." ValidationExpression="^[1-9]$|^10$" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"> </asp:RegularExpressionValidator>
                          </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_KoncnaOcenaF" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_Ura" runat="server" ControlToValidate="TB_KoncnaOcenaF" ErrorMessage="Vnesite 1 - 10." ValidationExpression="^[1-9]$|^10$" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"> </asp:RegularExpressionValidator>
                        </FooterTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_dodaj" runat="server" CommandName="Insert" CssClass="btn btn-info" Width="200px">Vnesi končno oceno</asp:LinkButton>
                        </ItemTemplate>                        
                        <FooterTemplate>
                            <asp:LinkButton ID="LB_dodajF" runat="server" CommandName="InsertFooter" ValidationGroup="ValGroup" CssClass="btn btn-info" Width="200px">Vnesi končno oceno</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="Izpis" runat="server" ></asp:Label>
        </ContentTemplate>        
    </asp:UpdatePanel>    
</asp:Content>
