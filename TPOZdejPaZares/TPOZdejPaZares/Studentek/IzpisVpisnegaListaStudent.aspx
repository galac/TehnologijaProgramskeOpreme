<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="IzpisVpisnegaListaStudent.aspx.cs" Inherits="TPOZdejPaZares.Studentek.IzpisVpisnegaListaStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <input type="button" onclick="printDiv()" value="Sprintaj vpisni list" class="btn btn-primary" />
    <asp:UpdatePanel ID="UpdatePanelPage" runat="server">
        <ContentTemplate>
            <fieldset>
            <asp:Label ID="LabelPage" runat="server" Text="Label"></asp:Label>
            <asp:GridView ID="GridViewPage" runat="server" class="table table-striped" Width ="920px"></asp:GridView>
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
