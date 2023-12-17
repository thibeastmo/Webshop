<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WinkelwagenPage.aspx.cs" Inherits="WinkelwagenPage" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="winkelwagendiv">
        <h1>Winkelwagen</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upnlWinkelwagen" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlWinkelwagen" runat="server" CssClass="pnlWinkelwagen">
                    <div id="winkelwagentop"><span></span><span></span><span>Aantal</span><span>Prijs per stuk</span><span>Subtotaal</span></div>
                    <div id="itemlist" runat="server">
                    <%--HIER KOMEN DE ITEMS VAN DE WINKELWAGEN VIA CODE BEHIND IN--%>
                    </div>
                    <div id="winkelwagentotaal">
                        <span></span>
                        <span></span>
                        <span></span>
                        <span>Totaal</span>
                        <asp:Label ID="lblTotaal" runat="server" Text="€100,00"></asp:Label>
                    <asp:Button ID="btnBetalen" CssClass="button" runat="server" Text="Betalen" OnClick="btnBetalen_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlNoInWinkelwagen" runat="server" CssClass="pnlNoInWinkelwagen">
                    <p>
                        Je hebt nog geen artikelen in je winkelwagen. Klik <a href="Default.aspx">hier</a> om verder te winkelen.<br />
                        Of bekijk je favorieten <asp:Button ID="btnGoToFavorieten" runat="server" Text="hier" OnClick="btnGoToFavorieten_Click" />.
                    </p>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

