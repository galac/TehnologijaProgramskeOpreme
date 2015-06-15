<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpravljanjeSklepov.aspx.cs" Inherits="TPOZdejPaZares.Referent.UpravljanjeSklepov" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <br />
     <h3>Študenti</h3>
    <asp:GridView ID="GridView1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="table" DataKeyNames="idStudent" EnablePersistedSelection="True" SelectedIndex="0">
        <Columns>
            <asp:CommandField ShowSelectButton="True" CancelText="Prekliči" DeleteText="Izbriši" EditText="Uredi" InsertText="Vstavi" NewText="Novo" SelectText="Izberi" UpdateText="Posodobi" />
            <asp:BoundField DataField="idStudent" HeaderText="idStudent" ReadOnly="True" SortExpression="idStudent" Visible="False" />
            <asp:BoundField DataField="imeStudenta" HeaderText="Ime" ReadOnly="True" SortExpression="imeStudenta" />
            <asp:BoundField DataField="priimekStudenta" HeaderText="Priimek" ReadOnly="True" SortExpression="priimekStudenta" />
            <asp:BoundField DataField="vpisnaStudenta" HeaderText="Vpisna Številka" ReadOnly="True" SortExpression="vpisnaStudenta" />
        </Columns>
        <SelectedRowStyle BackColor="#6699FF" />
    </asp:GridView>
    <h3>Sklepi</h3>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="idSklep" AllowPaging="True" AllowSorting="True" CssClass="table" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" CancelText="Prekliči" DeleteText="Izbriši" EditText="Uredi" InsertText="Vstavi" NewText="Novo" SelectText="Izberi" UpdateText="Posodobi" />
            <asp:BoundField DataField="idSklep" HeaderText="idSklep" ReadOnly="True" SortExpression="idSklep" Visible="False" />
            <asp:TemplateField HeaderText="Vsebina" SortExpression="VsebinaSklepa">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" TextMode="multiline" columns="40" Rows="5" Text='<%# Bind("VsebinaSklepa") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("VsebinaSklepa") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Student_IdStudenta" HeaderText="Student_IdStudenta" SortExpression="Student_IdStudenta" Visible="False" />
            <asp:BoundField DataField="Student_idStudent" HeaderText="Student_idStudent" SortExpression="Student_idStudent" Visible="False" />
            <asp:BoundField DataField="Organ" HeaderText="Organ" SortExpression="Organ" />
            <asp:TemplateField HeaderText="Datum Sprejetja" SortExpression="DatumSprejetjaSklepa">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DatumSprejetjaSklepa") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegID" ControlToValidate="TextBox2" runat="server" 
                     ErrorMessage="* DD.MM.LLLL" ValidationExpression="^([1-9]|0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[012]).(19|20)\d\d$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("DatumSprejetjaSklepa") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DatumVeljave" SortExpression="DatumVeljaveSklepa">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DatumVeljaveSklepa") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegID2" ControlToValidate="TextBox1" runat="server" 
                     ErrorMessage="* DD.MM.LLLL" ValidationExpression="^([1-9]|0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[012]).(19|20)\d\d$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("DatumVeljaveSklepa") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <h3>Novi Sklep</h3>
    <table class="nav-justified" style="padding-right: 10px; margin-right: 10px;">
        <tr>
            <td class="text-right" style="padding: 10px">
               <label>Vsebina Sklepa</label>
            </td>
            <td>
                <asp:TextBox ID="vsebinaInput" runat="server" TextMode="multiline" columns="40" Rows="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right" style="padding: 10px">
                 <label>Datum</label>
            </td>
            <td style="height: 29px">
                <asp:TextBox ID="datumInput" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegID" ControlToValidate="datumInput" runat="server" 
                     ErrorMessage="* DD.MM.LLLL" ValidationExpression="^([1-9]|0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[012]).(19|20)\d\d$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="text-right" style="padding: 10px">
                <label>Datum Veljavnosti</label>
            </td>
            <td style="height: 26px">
                <asp:TextBox ID="VeljavnostInput" runat="server"></asp:TextBox>
               <asp:RegularExpressionValidator ID="RegID3" ControlToValidate="VeljavnostInput" runat="server" 
                     ErrorMessage="* DD.MM.LLLL" ValidationExpression="^([1-9]|0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[012]).(19|20)\d\d$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="text-right" style="padding: 10px">
                 <label>Organ</label>
            </td>
            <td style="height: 26px">
                <asp:TextBox ID="organInput" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">&nbsp;</td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Oddaj Sklep" CssClass="btn btn-primary" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
