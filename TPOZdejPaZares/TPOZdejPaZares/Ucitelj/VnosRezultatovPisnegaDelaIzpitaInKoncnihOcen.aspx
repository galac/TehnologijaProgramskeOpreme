<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VnosRezultatovPisnegaDelaIzpitaInKoncnihOcen.aspx.cs" Inherits="TPOZdejPaZares.Ucitelj.VnosRezultatovPisnegaDelaIzpitaInKoncnihOcen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="http://cdn.syncfusion.com/13.1.0.21/js/web/flat-azure/ej.web.all.min.css" rel="stylesheet" />
    <!--scripts-->
    <script src=" http://cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js "></script>
    <script src="http://ajax.aspnetcdn.com/ajax/globalize/0.1.1/globalize.min.js"></script>
    <script src=" http://cdn.syncfusion.com/js/assets/external/jquery.easing.1.3.min.js "></script>
    <script src="http://cdn.syncfusion.com/13.1.0.21/js/web/ej.web.all.min.js"> </script>
  
    <style>
        .pizdarija {
            display: none
        }
    </style>
       
    <script type="text/javascript">
        function EEECheck() {


            var validator;


            for (var i = 0; i < Page_Validators.length; i++) {
                validator = Page_Validators[i];
                ValidatorValidate(validator);

                // validation fails if at least one validator fails
                if (!validator.isvalid) {
                    alert('Napaka pri vnosu. Preverite vpisane točke in ocene.');
                    <%-- $("#<%= GV_izpitniRoki.Visible ? GV_izpitniRoki.FooterRow.FindControl("L_errVal").ClientID : "" %> ").show();--%>
                    return;
                }
            }
        }
        </script>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <h3>Vnesite podatke o predmetu</h3>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <h4>Izberite študijsko leto:</h4>
                        <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="DDL_Years" runat="server" 
                            AutoPostBack="True" OnSelectedIndexChanged="DDL_Years_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <h4>Izberite predmet za izbrano študijsko leto:</h4>
                        <asp:DropDownList CssClass="btn btn-primary btn-block dropdown-toggle" ID="DDL_SubjectList" runat="server" 
                            AutoPostBack="True" OnSelectedIndexChanged="DDL_SubjectList_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <h4 runat="server" id="header41">Izberite izvedbo predmeta: </h4>


            <%-- GRIDVIEW ZA IZPIS IZVEDB PREDMETOV --%>
            <asp:GridView ID="GV_subjects" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" 
                OnSelectedIndexChanged="GV_subjects_SelectedIndexChanged" DataKeyNames="idIzvedbaPredmeta">
                <Columns>
                    <asp:BoundField DataField="StudijskiProgram" HeaderText="Študijski program" />
                    <asp:BoundField DataField="Letnik" HeaderText="Letnik" />
                    <asp:BoundField DataField="SestavniDelPred" HeaderText="Vrsta predmeta/modul" />
                    <asp:BoundField DataField="KreditneTocke" HeaderText="Pt. kreditov" />
                    <asp:BoundField DataField="Izvajalci" HeaderText="Izvajalci" /> 
                    <asp:CommandField SelectText="prikaz izpitnih rokov" ShowSelectButton="True" />
                    <asp:BoundField DataField="idIzvedbaPredmeta" Visible="false" /> 
                </Columns>
                <SelectedRowStyle BorderColor="#2C3E50" BorderStyle="Solid" />
            </asp:GridView>

            <br />
            <h3 runat="server" id="header31">
                <asp:Label ID="L_header31" runat="server"></asp:Label>
            </h3>           
            

            <%-- GRIDVIEW ZA IZPIS IZPITNIH ROKOV --%>
            <asp:Label ID="L_Error" runat="server" CssClass="label label-danger"></asp:Label>

            <asp:GridView ID="GV_izpitniRoki" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" 
                DataKeyNames="IDIzpitniRok,IzvedbaPredmeta_idIzvedbaPredmeta" OnRowCommand="GV_izpitniRoki_RowCommand" >
                <SelectedRowStyle BorderColor="#2C3E50" BorderStyle="Solid" />
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
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Datum">
                        <ItemTemplate>
                            <asp:Label ID="L_DatumIzpitnegaRoka" runat="server" Text='<%# Eval("DatumIzpitnegaRoka","{0:dd. MM. yyyy}") %>' Width="100px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ura">
                        <ItemTemplate>
                            <asp:Label ID="L_Ura" runat="server" Text='<%# Eval("DatumIzpitnegaRoka", "{0:hh:mm}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prostor">
                        <ItemTemplate>
                            <asp:Label ID="L_Prostor" runat="server" Text='<%# Eval("Prostor") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Št. mest">
                        <ItemTemplate>
                            <asp:Label ID="L_SteviloMest" runat="server" Text='<%# Eval("SteviloMest") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Št. prijav">
                        <ItemTemplate>
                            <asp:Label ID="L_SteviloPrijav" runat="server" Text='<%# Eval("SteviloPrijav") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false" >
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_prikaziSeznam" runat="server" Width="257px"></asp:LinkButton>   
                            <asp:Label ID="L_rokiInfo" runat="server" Text="Rok še ni bil izveden." CssClass="label label-info"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <br />
            <h3 runat="server" id="h111">
                <asp:Label ID="L_headerSeznamStudentov" runat="server"></asp:Label>
            </h3>  
            <asp:Label ID="LB_errorSeznam" runat="server" CssClass="label label-danger" Visible="false"></asp:Label>


             <%-- GRIDVIEW ZA IZPIS SEZNAMA ŠTUDENTOV NA IZBRANEM IZPITEM ROKU - ocene --%>
            <asp:GridView ID="GV_seznamStudentov_O" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" 
                DataKeyNames="idStudent, IdPrijavaNaIzpit" OnRowCommand="GV_seznamStudentov_O_RowCommand" ShowFooter="true">
                <Columns>
                    <asp:BoundField DataField="idStudent" Visible="false" />
                    <asp:BoundField DataField="IdPrijavaNaIzpit" Visible="false" />

                    <asp:TemplateField HeaderText="Št.">
                        <ItemTemplate>
                            <asp:Label ID="L_ZaporednaStevilkaO" runat="server" Text='<%# Eval("ZaporednaStevilka") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vpisna">
                        <ItemTemplate>
                            <asp:Label ID="L_VpisnaStevilkaO" runat="server" Text='<%# Eval("VpisnaStevilka") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Priimek in ime">
                        <ItemTemplate>
                            <asp:Label ID="L_PriimekInImeO" runat="server" Text='<%# Eval("PriimekInIme") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leto poslušanja">
                        <ItemTemplate>
                            <asp:Label ID="L_StudijskoLetoO" runat="server" Text='<%# Eval("StudijskoLeto") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Polaganje">
                        <ItemTemplate>
                            <asp:Label ID="L_PolaganjeO" runat="server" Text='<%# Eval("Polaganje") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="L_StatusO" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> --%>
                    <asp:TemplateField HeaderText="Vnos točk pisnega dela izpita">
                        <ItemTemplate>
                            <asp:TextBox ID="TB_VnosTock" runat="server" CssClass="form-control" Text='<%# Eval("StTock") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_VnosTock" runat="server" ControlToValidate="TB_VnosTock" ErrorMessage="0 - 100 točk | VP" 
                                ValidationExpression="^[1-9][0-9]?$|^100$|^0$|^[vV][pP]$" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="LB_vnesiOceneO" runat="server" Width="100%" Text="VNESI VSE TOČKE IN OCENE" ValidationGroup="ValGroup" 
                                CssClass="btn btn-info" CommandName="CMD_vnosOcenO" OnClientClick="EEECheck();" CausesValidation="true"></asp:LinkButton> 
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vnos končnih ocen">
                        <ItemTemplate>
                            <asp:TextBox ID="TB_VnosOcenO" runat="server" CssClass="form-control" Text='<%# Eval("OcenaIzpita") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_VnosOcenO" runat="server" ControlToValidate="TB_VnosOcenO" ErrorMessage="ocena 1 - 10 | VP" 
                                ValidationExpression="^[1-9]$|^10$|^[vV][pP]$" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic" >
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="L_errVal" runat="server" Text="Napaka pri vnosu." CssClass="label label-danger" Visible="false"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>            

            <asp:Label ID="Izpis" runat="server"></asp:Label>
            
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
