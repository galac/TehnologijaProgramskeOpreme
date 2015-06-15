<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="TPOZdejPaZares.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GV_izpitniRoki" runat="server" AutoGenerateColumns="false" class="table table-striped" DataKeyNames="IDIzpitniRok,IzvedbaPredmeta_idIzvedbaPredmeta" ShowFooter="True" ShowHeaderWhenEmpty="true">
                <Columns>
                    <asp:BoundField DataField="IDIzpitniRok" Visible="false" />
                    <asp:BoundField DataField="IzvedbaPredmeta_idIzvedbaPredmeta" Visible="false" />
                     <asp:ButtonField />                   
                    <asp:TemplateField HeaderText="Št. izpitnega roka">
                        <ItemTemplate>
                            <asp:Label ID="L_ZaporednaStevilka" runat="server" Text='<%# Eval("ZaporednaStevilka") %>' ></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_ZaporednaStevilka" runat="server" CssClass="form-control" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_ZaporednaStevilka" runat="server" ControlToValidate="TB_ZaporednaStevilka" ErrorMessage="Vnesite celo število." ValidationExpression="^\d+$" CssClass="label label-danger" ValidationGroup="ValGroup"> </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFV_ZaporednaStevilka" runat="server" ErrorMessage="Obvezno polje." ControlToValidate="TB_ZaporednaStevilka" CssClass="label label-danger" ValidationGroup="ValGroup"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vrsta">
                        <ItemTemplate>
                            <asp:Label ID="L_VrstaIzpitnegaRoka" runat="server" Text='<%# Eval("VrstaIzpitnegaRoka") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>                            
                             <asp:DropDownList CssClass="btn btn-primary dropdown-toggle" data-toggle="dropdown" ID="DDL_vrstaRoka" runat="server" >
                                 <asp:ListItem Selected="True" Value="redni">redni</asp:ListItem>
                                 <asp:ListItem Value="redni">izredni</asp:ListItem>
                             </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Datum">
                        <ItemTemplate>
                            <asp:Label ID="L_DatumIzpitnegaRoka" runat="server" Text='<%# Eval("DatumIzpitnegaRoka","{0:dd. MM. yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_DatumIzpitnegaRoka" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_DatumIzpitnegaRoka" runat="server" ControlToValidate="TB_DatumIzpitnegaRoka" ErrorMessage="Vnesite legalen datum (dd.mm.llll)." ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" CssClass="label label-danger" ValidationGroup="ValGroup"> </asp:RegularExpressionValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ura">
                        <ItemTemplate>
                            <asp:Label ID="L_Ura" runat="server" Text='<%# Eval("DatumIzpitnegaRoka", "{0:H:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_Ura" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prostor">
                        <ItemTemplate>
                            <asp:Label ID="L_Prostor" runat="server" Text='<%# Eval("Prostor") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_Prostor" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFV_Prostor" runat="server" ErrorMessage="Obvezno polje." ControlToValidate="TB_Prostor" CssClass="label label-danger" ValidationGroup="ValGroup"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Št. mest">
                        <ItemTemplate>
                            <asp:Label ID="L_SteviloMest" runat="server" Text='<%# Eval("SteviloMest") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TB_SteviloMest" runat="server" CssClass="form-control" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REV_SteviloMest" runat="server" ControlToValidate="TB_SteviloMest" ErrorMessage="Vnesite celo število ali pustite prazno." ValidationExpression="^\d+$" CssClass="label label-danger" ValidationGroup="ValGroup"> </asp:RegularExpressionValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_uredi" runat="server" CommandName="Select" CssClass="btn btn-info">Uredi</asp:LinkButton>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="dummy"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="LB_dodaj" runat="server" CommandName="Select" ValidationGroup="ValGroup" CssClass="btn btn-success">Dodaj</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
</asp:Content>
