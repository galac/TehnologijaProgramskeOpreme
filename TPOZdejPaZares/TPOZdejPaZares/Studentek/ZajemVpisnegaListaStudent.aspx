<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZajemVpisnegaListaStudent.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="TPOZdejPaZares.Studentek.ZajemVpisnegaListaStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <h3>Zajem vpisnega lista</h3>
    <br />
    <asp:TextBox class="form-control" id='TextBoxZeton' type = "text" runat="server" Visible="false" ReadOnly="True" />
    <br />
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>EMŠO:</h4>
                <asp:TextBox class="form-control" id='emso' type = "text" runat="server" />
                <asp:RequiredFieldValidator ID="EMSOValidator" runat="server" ControlToValidate="emso" CssClass="label label-danger" Display="Dynamic" ErrorMessage="Vpiši EMŠO!" ValidationGroup="AllValidators"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Davčna številka:</h4>
                <asp:TextBox class="form-control" id='davcna' type = "text" runat="server" />
                <asp:RequiredFieldValidator ID="DavcnaValidator" runat="server" ControlToValidate="davcna" CssClass="label label-danger" Display="Dynamic" ErrorMessage="Vpiši davčno!" ValidationGroup="AllValidators"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Vpisna številka:</h4>
                <asp:TextBox class="form-control" id='vpisna' type = "text" runat="server" ReadOnly="True" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Ime:</h4>
                <asp:TextBox class="form-control" id='ime' type = "text" runat="server" />
                <asp:RequiredFieldValidator ID="ImeValidator" runat="server" ControlToValidate="ime" CssClass="label label-danger" Display="Dynamic" ErrorMessage="Vpiši ime!" ValidationGroup="AllValidators"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Priimek:</h4>
                <asp:TextBox class="form-control" id='priimek' type = "text" runat="server" />
                <asp:RequiredFieldValidator ID="PriimekValidator" runat="server" ControlToValidate="priimek" CssClass="label label-danger" Display="Dynamic" ErrorMessage="Vpiši priimek!" ValidationGroup="AllValidators"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Spol:</h4>
                <asp:RadioButton class="radio-inline" value="moski" ID="radioMoski" runat="server" Checked="true" GroupName="groupSpol" Text="Moški" />
                <asp:RadioButton class="radio-inline" value="zenski" ID="radioZenski" runat="server" GroupName="groupSpol" Text="Ženski" />
            </div>
        </div>
    </div>

    <hr />

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Email:</h4>
                <asp:TextBox class="form-control" id='email' type = "text" runat="server" />
                <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="email" CssClass="label label-danger" Display="Dynamic" ErrorMessage="Vpiši email!" ValidationGroup="AllValidators"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Prenosni telefon:</h4>
                <asp:TextBox class="form-control" id='telefon' type = "text" runat="server" />
            </div>
        </div>
        
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Državljanstvo:</h4>
                <asp:DropDownList class="btn btn-primary dropdown-toggle" data-toggle="dropdown" ID="DropDownDrzavljanstvo" runat="server"></asp:DropDownList>
           </div>
        </div>
    </div>
    <br />

    <hr />

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Datum rojstva:</h4>
                <asp:TextBox class="form-control" id='datumRojstva' type = "text" runat="server" />
                <script type="text/javascript">
                    $(function () {
                        $('#MainContent_datumRojstva').datetimepicker({
                            format: 'YYYY-MM-DD'
                        });
                    });
                </script>
                <asp:RequiredFieldValidator ID="DatumRojstvaValidator" runat="server" ControlToValidate="datumRojstva" CssClass="label label-danger" Display="Dynamic" ErrorMessage="Izberi datum!" ValidationGroup="AllValidators"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Kraj rojstva:</h4>
                <asp:TextBox class="form-control" id='krajRojstva' type = "text" runat="server" />
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Občina rojstva:</h4>
                <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownObcinaRojstva" runat="server"></asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Država rojstva:</h4>
                <asp:DropDownList class="btn btn-primary dropdown-toggle" data-toggle="dropdown" ID="DropDownDrzavaRojstva" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownDrzavaRojstva_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
    </div>

    <hr />

    <div class="row">
        <div class="col-md-1 col-md-offset-2">
            <h4>Vročanje:</h4>
        </div>
        <div class="col-md-3">
            <h4>Naslov:</h4>
        </div>
        <div class="col-md-2">
            <h4>Občina:</h4>
        </div>
        <div class="col-md-2">
            <h4>Poštna številka:</h4>
        </div>
        <div class="col-md-2">
            <h4>Država:</h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <h4>Stalno bivališče:</h4>
        </div>
        <div class="col-md-1">
            <asp:RadioButton class="radio" value="stalno" ID="radioStalno" runat="server" Checked="true" GroupName="groupBivalisce" style="width: 50%; margin: 0 auto;" />
        </div>
        <div class="col-md-3">
            <div class="row"><asp:TextBox class="form-control" id='stalniNaslov' type = "text" runat="server" /></div>
        </div>
        <div class="col-md-2">
            <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownObcinaStalna" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <div class="row"><asp:TextBox class="form-control" id='stalnaPosta' type = "text" runat="server" /></div>
        </div>
        <div class="col-md-2">
            <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownDrzavaStalna" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownDrzavaStalna_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <h4>Začasno bivališče:</h4>
        </div>
        <div class="col-md-1">
            <asp:RadioButton class="radio" value="zacasno" ID="radioZacasno" runat="server" GroupName="groupBivalisce" style="width: 50%; margin: 0 auto;" />
        </div>
        <div class="col-md-3">
            <div class="row"><asp:TextBox class="form-control" id='zacasniNaslov' type = "text" runat="server" /></div>
        </div>
        <div class="col-md-2">
            <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownObcinaZacasna" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <div class="row"><asp:TextBox class="form-control" id='zacasnaPosta' type = "text" runat="server" /></div>
        </div>
        <div class="col-md-2">
            <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownDrzavaZacasna" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownDrzavaZacasna_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>

    <br />
    <hr />

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Študijski program:</h4>
                <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownStudijskiProgram" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownStudijskiProgram_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Letnik:</h4>
                <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownLetnik" runat="server"></asp:DropDownList>
            </div>
        </div>
        
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Vrsta vpisa:</h4>
                <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownVrstaVpisa" runat="server"></asp:DropDownList>
           </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Izobrazba po Klasius:</h4>
                <asp:TextBox class="form-control" id='TextBoxIzobrazba' type = "text" ReadOnly="true" runat="server" />
            </div>
        </div>
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Način študija:</h4>
                <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownNacinStudija" runat="server"></asp:DropDownList>
            </div>
        </div>
        
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="form-group">
                <h4>Oblika študija:</h4>
                <asp:DropDownList class="btn btn-primary btn-block dropdown-toggle" data-toggle="dropdown" ID="DropDownOblikaStudija" runat="server"></asp:DropDownList>
           </div>
        </div>
    </div>

    <br />
    <asp:TextBox class="form-control" id='TextBoxNapake' type = "text" ReadOnly="true" runat="server" Visible="False" Width="800px" />


    <br />
    <asp:Button ID="buttonNadaljuj" runat="server" Text="Nadaljuj" type="submit" CssClass="btn btn-primary" OnClick="buttonNadaljuj_Click"/>

</asp:Content>
