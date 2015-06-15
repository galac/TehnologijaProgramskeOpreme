<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IzvajalciPredmetov.aspx.cs" Inherits="TPOZdejPaZares.Referent.IzvajalciPredmetov" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <br />   

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
            <h4 runat="server" id="header41">Izvajalci: </h4>

            <asp:GridView ID="GV_izvajalci" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" 
                OnRowDataBound="GV_izvajalci_RowDataBound" DataKeyNames="idProfesor" OnRowDeleting="GV_izvajalci_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ime" HeaderText="Ime" />
                    <asp:BoundField DataField="priimek" HeaderText="Priimek" />
                    <asp:BoundField DataField="idProfesor" HeaderText="idProfesor" />
                     <%--<asp:CommandField ShowEditButton="true" EditText="Uredi"/>--%>
                     <asp:CommandField ShowDeleteButton="true" DeleteText="Izbriši"/>
                </Columns>
                <SelectedRowStyle BorderColor="#2C3E50" BorderStyle="Solid" />
            </asp:GridView>

            <asp:DropDownList CssClass="btn btn-primary btn-block dropdown-toggle" ID="DDL_seznam_profesorjev" runat="server" Visible="false">

            </asp:DropDownList>
            <asp:Button runat="server" ID="dodajIzvajalcaBTN" OnClick="dodajIzvajalcaBTN_Click" Visible="false" Text="Dodaj profesorja"/>

        </ContentTemplate>
    </asp:UpdatePanel>   

</asp:Content>
