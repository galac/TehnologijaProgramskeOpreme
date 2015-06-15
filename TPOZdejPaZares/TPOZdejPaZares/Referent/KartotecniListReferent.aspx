<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KartotecniListReferent.aspx.cs" Inherits="TPOZdejPaZares.Referent.KartotecniListReferent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style type="text/css">
    .leftPanel
    {
        width:900px;
        float:left;
        margin-right: 3000px;
        position:relative;
    }
    .rightPanel
    {
        width:445px;
        float:left;
        position:relative;
    }
</style>
    <br /><br /><br /><br />
    <input type="button" onclick="printDiv()" value="Sprintaj kartotečni list" class="btn btn-primary" />
    <br /><br /><br />
    <asp:RadioButton class="radio-inline" value="zadnjePolaganje" ID="radioZadnjePolaganje" runat="server" Checked="true" GroupName="groupPolaganje" Text="Izpiši samo zadnje polaganje" OnCheckedChanged="radioZadnjePolaganje_CheckedChanged" AutoPostBack="True" />
    <asp:RadioButton class="radio-inline" value="vsaPolaganja" ID="radioVsaPolaganja" runat="server" GroupName="groupPolaganje" Text="Izpiši vsa polaganja" OnCheckedChanged="radioZadnjePolaganje_CheckedChanged" AutoPostBack="True" />
    <br /> <br /><br />   
    <asp:UpdatePanel ID="UpdatePanelPage" runat="server">
        <ContentTemplate>
            <fieldset>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </fieldset>
        </ContentTemplate>

    </asp:UpdatePanel>

    <script>
        function printDiv() {
            var printContents = document.getElementById('<%= UpdatePanelPage.ClientID %>').innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
</asp:Content>
