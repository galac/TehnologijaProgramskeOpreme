<%@ Page Title="Iskanje študentov" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Zetoni.aspx.cs" Inherits="TPOZdejPaZares.Zetoni" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>    
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
            <asp:GridView ID="GridViewIme" class="table table-striped" runat="server" OnSelectedIndexChanged="GridViewIme_SelectedIndexChanged" AutoGenerateColumns="false" 
                OnRowDataBound="GridViewIme_RowDataBound" DataKeyNames="idStudent, vpisnaStevilka">
                <Columns>
                         <asp:BoundField DataField="vpisnaStevilka" HeaderText="Vpisna številka" />
                         <asp:BoundField DataField="ime" HeaderText="Ime" />
                         <asp:BoundField DataField="priimek" HeaderText="Priimek" />
                         <asp:BoundField DataField="idStudent" HeaderText="idStudent" />
                         <asp:CommandField SelectText="seznam žetonov" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="LabelOpozorilo" runat="server" Text="Najden ni bil nobeden študent." class="label label-danger" Visible="False"></asp:Label>

            <asp:GridView ID="zetoniStudentaGV" class="table table-striped" runat="server" Visible="false" AutoGenerateColumns="false" DataKeyNames="IdZeton" ShowFooter="True"
                ShowHeaderWhenEmpty="true" OnRowDataBound="zetoniStudentaGV_RowDataBound" OnRowDeleting="zetoniStudentaGV_RowDeleting" OnRowEditing="zetoniStudentaGV_RowEditing"  
                OnRowCancelingEdit="zetoniStudentaGV_RowCancelingEdit" OnRowUpdating="zetoniStudentaGV_RowUpdating" OnSelectedIndexChanged="zetoniStudentaGV_SelectedIndexChanged">
                     <Columns>
                         <asp:TemplateField HeaderText="Študijski program">
                            <ItemTemplate>
                                <asp:Label ID="studijskiProgramLB" runat="server" Text='<%# Eval("StudijskiProgram") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="studijskiProgramDDL" AutoPostBack="true" class="btn btn-primary dropdown-toggle"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="newStudijskiProgramDDL" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Letnik">
                            <ItemTemplate>
                                <asp:Label ID="letnikLB" runat="server" Text='<%# Eval("Letnik") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="letnikDDL" AutoPostBack="true" class="btn btn-primary dropdown-toggle"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="newLetnikDDL" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Vrsta vpisa">
                            <ItemTemplate>
                                <asp:Label ID="vrstaVpisaLB" runat="server" Text='<%# Eval("VrstaVpisa") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="vrstaVpisaDDL" AutoPostBack="true" class="btn btn-primary dropdown-toggle"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="newVrstaVpisaDDL" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Način študija">
                            <ItemTemplate>
                                <asp:Label ID="nacinStudijaLB" runat="server" Text='<%# Eval("NacinStudija") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="nacinStudijaDDL" AutoPostBack="true" class="btn btn-primary dropdown-toggle"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="newNacinStudijaDDL" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Oblika študija">
                            <ItemTemplate>
                                <asp:Label ID="oblikaStudijaLB" runat="server" Text='<%# Eval("OblikaStudija") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="oblikaStudijaDDL" AutoPostBack="true" class="btn btn-primary dropdown-toggle"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="newOblikaStudijaDDL" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Prosta izbira v 3. letniku">
                            <ItemTemplate>
                                <asp:Label ID="prostaIzbiraLB" runat="server" Text='<%# Eval("prostaIzbira") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="prostaIzbiraDDL" AutoPostBack="true" class="btn btn-primary dropdown-toggle"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="newProstaIzbiraDDL" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Izkoriščen">
                            <ItemTemplate>
                                <asp:Label ID="izkoriscenLB" runat="server" Text='<%# Eval("izkoriscen") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="izkoriscenDDL" AutoPostBack="true" class="btn btn-primary dropdown-toggle"></asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="newIzkoriscenDDL" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="IdZeton" HeaderText="IDPredmet" />
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
                            <asp:LinkButton ID="LB_izbrisi" runat="server" CommandName="Delete" CssClass="btn btn-danger" Width="80px">Izbriši</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LB_cancel"  runat="server"  CssClass="btn btn-danger" Width="80px" CausesValidation="false" CommandName="Cancel">Prekliči</asp:LinkButton>                            
                        </EditItemTemplate>
<%--                        <FooterTemplate>
                            <asp:LinkButton ID="LB_preklici"  runat="server"  CssClass="btn btn-danger" Width="80px" CausesValidation="false">Reset</asp:LinkButton>                            
                        </FooterTemplate>--%>
                    </asp:TemplateField>
                    </Columns>
            </asp:GridView>



            <div runat="server" id="novZetonDiv" style="display: none;">
                <asp:Table runat="server" ID="novZetonTable" class="table table-striped">
                    <asp:TableRow>
                        <asp:TableCell>Študijski program</asp:TableCell>
                        <asp:TableCell>Letnik</asp:TableCell>
                        <asp:TableCell>Vrsta vpisa</asp:TableCell>
                        <asp:TableCell>Način študij</asp:TableCell>
                        <asp:TableCell>Oblika študija</asp:TableCell>
                        <asp:TableCell>Prosta izbira v 3. letniku</asp:TableCell>
                        <asp:TableCell>Izkoriščen</asp:TableCell>
                        <asp:TableCell></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                        <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="novStudijskiProgramDDL" width="200" runat="server"> </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="novLetnikDDL" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="novVrstaVpisaDDL" width="200" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="novNacinStudijaDDL" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="novOblikaStudijaDDL" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="novProstaIzbiraDDL" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" ID="novIzkoriscenDDL" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="dodajNovZeton" Text="Ustvari žeton" OnClick="dodajNovZeton_Click" CssClass="btn btn-success" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                
                                
                                
                                

                

            </div>

           <%-- <div runat="server" id="editZetonDiv" class="row">
                <asp:DropDownList runat="server" ID="drugiStrokovniIzbirniDDL" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"
                        AutoPostBack="true" OnSelectedIndexChanged="drugiStrokovniIzbirniDDL_SelectedIndexChanged"></asp:DropDownList>

            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
