<%@ Page Title="Predmetnik" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Predmetnik.aspx.cs" Inherits="TPOZdejPaZares.Predmetnik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
        <h3>Predmetnik</h3>
    <br />

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <asp:Label runat="server">Izberi študijski program:</asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="StudijskiProgramDDL" OnSelectedIndexChanged="StudijskiProgramDDL_SelectedIndexChanged" 
                AutoPostBack="true" class="btn btn-primary dropdown-toggle" Style="margin-bottom:1em;"></asp:DropDownList>
            <br />

            <asp:Label runat="server" ID="feedbackLB" Visible="false" CssClass="label label-info"></asp:Label>
            <br />
            <asp:GridView runat="server" ID="seznamPredmetovGV" class="table table-striped" Width="650px" Visible="false" AutoGenerateColumns="false"
                OnRowDeleting="seznamPredmetovGV_RowDeleting" OnRowEditing="seznamPredmetovGV_RowEditing" OnRowCancelingEdit="seznamPredmetovGV_RowCancelingEdit"
                OnRowUpdating="seznamPredmetovGV_RowUpdating" DataKeyNames="SifraPredmeta, IdStudPredmeta">
             <Columns>
                 <asp:BoundField DataField="SifraPredmeta" HeaderText="Šifra predmeta"/>
                <asp:BoundField DataField="Predmet" HeaderText="Predmet" />
                 <asp:BoundField DataField="Kreditne" HeaderText="Kreditne točke" />
                <asp:BoundField DataField="Letnik" HeaderText="Letnik" />
                <asp:BoundField DataField="Del predmeta" HeaderText="Del predmeta" />
                <asp:BoundField DataField="IdStudPredmeta" HeaderText="ID" />
                <asp:CommandField ShowEditButton="true" />
                <asp:CommandField ShowDeleteButton="true" />
            </Columns>
            </asp:GridView>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4" id="editPredmet" runat="server"
            style="display:none;position:relative;left:-26.5em;top:3em;margin-bottom:2em;">
            <br />
                <h4>Spremeni predmet</h4>
            <br />
            <div class="form-group">
                <asp:HiddenField runat="server" ID="hiddenIdPredmetStudPrograma_Edit" />
                <asp:Label runat="server" Style="margin-right:5.56em;">Ime predmeta:&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                <asp:TextBox runat="server" ID="editPredmetImeTB" class="form-control" Enabled="false"></asp:TextBox>
                <br />
                <asp:Label runat="server">Šifra predmeta:&nbsp;&nbsp;&nbsp;</asp:Label>
                <asp:TextBox runat="server" Style="margin-right:6em;" ID="editPredmetSifraTB" class="form-control" Enabled="false"></asp:TextBox>
                <br />
                <asp:Label runat="server">Kreditne točke:&nbsp;&nbsp;&nbsp;</asp:Label>
                <asp:TextBox runat="server" Style="margin-right:6em;" ID="editPredmetKreditneTB" class="form-control" Enabled="false"></asp:TextBox>
                <br />
                <asp:Label runat="server">Razvrstitev (obvezni/strokovni izbirni/prosti izbirni/modul:)</asp:Label><br />
                <asp:DropDownList runat="server" ID="SestavniDelPredDDL_Edit" OnSelectedIndexChanged="SestavniDelPredDDL_Edit_SelectedIndexChanged" 
                    CssClass="btn btn-sm btn-default dropdown-toggle" AutoPostBack="true"></asp:DropDownList>
                <br />
                <asp:Label runat="server" ID="editLetnikLB">Letnik:</asp:Label><br />
                <asp:DropDownList runat="server" ID="LetnikDDL_Edit" CssClass="btn btn-sm btn-default dropdown-toggle"></asp:DropDownList>

                <br />
                <br />
                <asp:Button runat="server" ID="editPredmetButton" onclick="editPredmetButton_Click" Text="Posodobi" CssClass="btn btn-primary"/>
                <asp:Button runat="server" ID="cancelEditPredmetButton" onclick="cancelEditPredmetButton_Click" Text="Zavrzi" CssClass="btn btn-primary"/>

            </div>
        </div>
    </div>
    <div class="row">

        <div class="col-xs-12 col-sm-6 col-md-4" id="dodajPredmet" style="display:none" runat="server">
            <br /><br />
                <h4>Dodaj nov predmet</h4>
            <br />
            <div class="form-group">
                <asp:Label runat="server">Seznam obstoječih predmetov:</asp:Label><br />
                <asp:DropDownList runat="server" ID="seznamVsehPredmetovDDL" CssClass="btn btn-sm btn-default dropdown-toggle"></asp:DropDownList>
                <br />
                <asp:Label runat="server">Razvrstitev (obvezni/strokovni izbirni/prosti izbirni/modul)</asp:Label><br />
                <asp:DropDownList runat="server" ID="SestavniDelPredDDL" OnSelectedIndexChanged="SestavniDelPredDDL_SelectedIndexChanged" 
                    CssClass="btn btn-sm btn-default dropdown-toggle" AutoPostBack="true"></asp:DropDownList>
                <br />
                <asp:Label runat="server" ID="LetnikLabel" Visible="false">Letnik:</asp:Label><br />
                <asp:DropDownList runat="server" ID="LetnikDDL" Visible="false" CssClass="btn btn-sm btn-default dropdown-toggle"></asp:DropDownList>

                <br />
                <asp:Button runat="server" ID="dodajPredmetButton" onclick="dodajPredmetButton_Click" Text="Dodaj" CssClass="btn btn-primary"/>
               <br />
            </div>
        </div>
    </div>

</asp:Content>
