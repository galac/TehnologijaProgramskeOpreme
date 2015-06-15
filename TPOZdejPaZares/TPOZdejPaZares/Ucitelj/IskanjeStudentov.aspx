<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IskanjeStudentov.aspx.cs" Inherits="TPOZdejPaZares.Ucitelj.IskanjeStudentov" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .gridViewPager td {
	        padding-left: 4px;
	        padding-right: 4px;
	        padding-top: 1px;
	        padding-bottom: 2px;
        }
    </style>
     <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-6 col-sm-6">
                    <div class="form-group">
                        <h3>Išči po imenu in priimku</h3>
                        <div class="form-group">
                            <label for="inputIme">Ime</label>
                            <asp:TextBox class="form-control" type="text" ID="inputIme" placeholder="Janez" runat="server" OnTextChanged="inputIme_TextChanged" AutoPostBack="true" />
                        </div>
                        <div class="form-group">
                            <label for="inputPriimek">Priimek</label>
                            <asp:TextBox class="form-control" type="text" ID="inputPriimek" placeholder="Novak" runat="server" OnTextChanged="inputPriimek_TextChanged" AutoPostBack="true" />
                        </div>

                        <asp:Button ID="buttonIme" runat="server" Text="Išči po imenu in priimku" type="submit" OnClick="buttonIme_Click" CssClass="btn btn-primary" />
                    </div>
                </div>
                <div class="col-xs-6 col-sm-6">
                    <div class="form-group">
                        <h3>Išči po vpisni številki</h3>
                        <div class="form-group">
                            <label for="inputVpisna">Vpisna številka</label>
                            <asp:TextBox class="form-control" type="text" ID="inputVpisna" placeholder="63120168" runat="server" OnTextChanged="inputVpisna_TextChanged" AutoPostBack="true" />
                        </div>
                        <asp:Button ID="buttonVpisna" runat="server" Text="Išči po vpisni številki" type="submit" OnClick="buttonVpisna_Click" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
            <br />
            <br />

            <asp:GridView ID="GridViewIme" class="table table-striped" runat="server" OnSelectedIndexChanged="GridViewIme_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField SelectText="podrobnosti" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="LabelOpozorilo" runat="server" Text="Najden ni bil noben študent." class="label label-danger" Visible="False"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
