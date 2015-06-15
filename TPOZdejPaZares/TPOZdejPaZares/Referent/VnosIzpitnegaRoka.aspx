<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VnosIzpitnegaRoka.aspx.cs" Inherits="TPOZdejPaZares.Referent.VnosIzpitnegaRoka" %>
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

    <style>
        .pizdarija {
            display: none
        }
    </style>
       
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <h3>Vnesite podatke o predmetu</h3>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <h4>Izberite študijsko leto:</h4>
                        <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="DDL_Years" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Years_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <h4>Izberite predmet za izbrano študijsko leto:</h4>
                        <asp:DropDownList CssClass="btn btn-primary btn-block dropdown-toggle" ID="DDL_SubjectList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_SubjectList_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <h4 runat="server" id="header41">Izberite izvedbo predmeta: </h4>

            <asp:GridView ID="GV_subjects" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnSelectedIndexChanged="GV_subjects_SelectedIndexChanged" DataKeyNames="idIzvedbaPredmeta">
                <Columns>
                    <asp:BoundField DataField="StudijskiProgram" HeaderText="Študijski program" />
                    <asp:BoundField DataField="Letnik" HeaderText="Letnik" />
                    <asp:BoundField DataField="SestavniDelPred" HeaderText="Vrsta predmeta/modul" />
                    <asp:BoundField DataField="KreditneTocke" HeaderText="Št. kreditov" />
                    <asp:BoundField DataField="Izvajalci" HeaderText="Izvajalci" /> 
                    <asp:CommandField SelectText="urejanje izpitnih rokov" ShowSelectButton="True" />
                    <asp:BoundField DataField="idIzvedbaPredmeta" Visible="false" /> 
                </Columns>
                <SelectedRowStyle BorderColor="#2C3E50" BorderStyle="Solid" />
            </asp:GridView>

            <br />
            <h3 runat="server" id="header31">
                <asp:Label ID="L_header31" runat="server"></asp:Label></h3>
            <asp:Label ID="L_Error" runat="server" CssClass="label label-danger"></asp:Label>
            <asp:GridView ID="GV_izpitniRoki" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" 
                OnSelectedIndexChanged="GV_izpitniRoki_SelectedIndexChanged" DataKeyNames="IDIzpitniRok,IzvedbaPredmeta_idIzvedbaPredmeta" ShowFooter="True" ShowHeaderWhenEmpty="true" OnRowEditing="GV_izpitniRoki_RowEditing" OnRowDeleting="GV_izpitniRoki_RowDeleting" OnRowUpdating="GV_izpitniRoki_RowUpdating" OnRowCancelingEdit="GV_izpitniRoki_RowCancelingEdit" >
                <Columns>
                    <asp:BoundField DataField="IDIzpitniRok" Visible="false" />
                    <asp:BoundField DataField="IzvedbaPredmeta_idIzvedbaPredmeta" Visible="false" />

                    <asp:TemplateField HeaderText="Št.">
                        <ItemTemplate>
                            <asp:Label ID="L_ZaporednaStevilka" runat="server" Text='<%# Eval("ZaporednaStevilka") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vrsta">
                        <ItemTemplate>
                            <asp:Label ID="L_VrstaIzpitnegaRoka" runat="server" Text='<%# Eval("VrstaIzpitnegaRoka") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="DDL_vrstaRoka1" runat="server" >
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="DDL_vrstaRoka" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Datum">
                        <ItemTemplate>
                            <asp:Label ID="L_DatumIzpitnegaRoka" runat="server" Text='<%# Eval("DatumIzpitnegaRoka","{0:dd. MM. yyyy}") %>' Width="100px"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TB_DatumIzpitnegaRokaE" runat="server" CssClass="dummy form-control" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_DatumIzpitnegaRokaE" runat="server" ControlToValidate="TB_DatumIzpitnegaRokaE" ErrorMessage="Vnesite legalen datum (dd.mm.llll)." ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" CssClass="label label-danger" ValidationGroup="ValGroupE" Display="Dynamic"> </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFV_DatumIzpitnegaRokaE" runat="server" ErrorMessage="Obvezno polje." ControlToValidate="TB_DatumIzpitnegaRokaE" CssClass="label label-danger" ValidationGroup="ValGroupE" Display="Dynamic"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_DatumIzpitnegaRoka" runat="server" CssClass="dummy form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_DatumIzpitnegaRoka" runat="server" ControlToValidate="TB_DatumIzpitnegaRoka" ErrorMessage="Vnesite legalen datum (dd.mm.llll)." ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"> </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFV_DatumIzpitnegaRoka" runat="server" ErrorMessage="Obvezno polje." ControlToValidate="TB_DatumIzpitnegaRoka" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ura">
                        <ItemTemplate>
                            <asp:Label ID="L_Ura" runat="server" Text='<%# Eval("DatumIzpitnegaRoka", "{0:hh:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TB_UraE" runat="server" CssClass="form-control dummy-time" Text='<%# Eval("DatumIzpitnegaRoka", "{0:H:mm}") %>' ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_UraE" runat="server" ControlToValidate="TB_UraE" ErrorMessage="Vnesite britanski format (npr. 09:30 PM)." ValidationExpression="^(0?[1-9]|1[012])(:[0-5]\d) [APap][mM]$" CssClass="label label-danger" ValidationGroup="ValGroupE" Display="Dynamic"> </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFV_UraE" runat="server" ErrorMessage="Obvezno polje." ControlToValidate="TB_UraE" CssClass="label label-danger" ValidationGroup="ValGroupE" Display="Dynamic"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_Ura" runat="server" CssClass="form-control dummy-time"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_Ura" runat="server" ControlToValidate="TB_Ura" ErrorMessage="Vnesite 2-urni format (npr. 09:30)." ValidationExpression="^(0?[1-9]|1[012])(:[0-5]\d) [APap][mM]$" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"> </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFV_Ura" runat="server" ErrorMessage="Obvezno polje." ControlToValidate="TB_Ura" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prostor">
                        <ItemTemplate>
                            <asp:Label ID="L_Prostor" runat="server" Text='<%# Eval("Prostor") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TB_ProstorE" runat="server" CssClass="form-control" Text='<%# Eval("Prostor") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_Prostor" runat="server" CssClass="form-control"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Št. mest">
                        <ItemTemplate>
                            <asp:Label ID="L_SteviloMest" runat="server" Text='<%# Eval("SteviloMest") %>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TB_SteviloMestE" runat="server" CssClass="form-control dummy-number" Text='<%# Eval("SteviloPrijav") %>'>></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_SteviloMestE" runat="server" ControlToValidate="TB_SteviloMestE" ErrorMessage="Vnesite pozitivno število ali pustite prazno." ValidationExpression="^[1-9][0-9]*$" CssClass="label label-danger" ValidationGroup="ValGroupE" Display="Dynamic"> </asp:RegularExpressionValidator>
                          </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_SteviloMest" runat="server" CssClass="form-control dummy-number"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_SteviloMest" runat="server" ControlToValidate="TB_SteviloMest" ErrorMessage="Vnesite pozitivno število ali pustite prazno." ValidationExpression="^[1-9][0-9]*$" CssClass="label label-danger" ValidationGroup="ValGroup" Display="Dynamic"> </asp:RegularExpressionValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Št. prijav">
                        <ItemTemplate>
                            <%-- <asp:Label ID="L_SteviloPrijav" runat="server" Text='<%# Eval("SteviloPrijav") %>'></asp:Label> --%>
                        </ItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_uredi" runat="server" CommandName="Edit" CssClass="btn btn-info" Width="80px">Uredi</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LB_update" runat="server" CommandName="Update" CssClass="btn btn-success" Width="80px" ValidationGroup="ValGroupE">Potrdi</asp:LinkButton>
                             <asp:Label ID="L_ConfirmErrE" runat="server" CssClass="label label-danger" Visible="false" Text="Napaka pri vnosu."></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="LB_dodaj" runat="server" CommandName="Select" ValidationGroup="ValGroup" CssClass="btn btn-success" Width="80px">Dodaj</asp:LinkButton>
                            <asp:Label ID="L_ConfirmErr" runat="server" CssClass="label label-danger" Visible="false" Text="Napaka pri vnosu."></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <ItemTemplate>
                            <asp:LinkButton ID="LB_izbrisi" runat="server" CommandName="Delete" CssClass="btn btn-danger" Width="80px">Ukini</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LB_cancel"  runat="server"  CssClass="btn btn-danger" Width="80px" CausesValidation="false" CommandName="Cancel">Prekliči</asp:LinkButton>                            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="LB_preklici"  runat="server"  CssClass="btn btn-danger" Width="80px" CausesValidation="false" OnCommand="LinkButton_Command">Reset</asp:LinkButton>                            
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="Izpis" runat="server"></asp:Label>
            <asp:Button runat="server" Text="Button" ID="B_potrditevIzbrisa" OnClick="B_potrditevIzbrisa_Click" Width="0px" Height="0px" CssClass="pizdarija"/>
            <asp:Button runat="server" Text="Button" ID="B_potrditevSpremembe" OnClick="B_potrditevSpremembe_Click" Width="0px" Height="0px" CssClass="pizdarija"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
