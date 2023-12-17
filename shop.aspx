<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="shop.aspx.cs" Inherits="_Shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="upVoorkeuren" runat="server" class="upVoorkeuren">
        <ContentTemplate>
            <div id="voorkeuren" runat="server" class="preferencesdiv">
                <div id="merken" runat="server">
                    <%--De voorkeuren komen hier vanuit code behind in--%>
                </div>
                <div id="Categorieen" runat="server">
                    <%--De voorkeuren komen hier vanuit code behind in--%>
                </div>
                <div id="Geslachten" runat="server">
                    <%--De voorkeuren komen hier vanuit code behind in--%>
                </div>
                <div id="prijzen" class="prijzen" runat="server">
                    <h4>Prijs:</h4>

                    <div>
                        <span>€</span>
                        <asp:Panel ID="pnlLaagstePrijs" DefaultButton="btnSetLaagstePrijs" runat="server">
                            <%--<input type="number" runat="server" id="txtLaagstePrijs" placeholder="0,00" step="0.01" />--%>
                            <asp:TextBox ID="txtLaagstePrijs" runat="server" type="number" step="0"></asp:TextBox>
                            <asp:Button ID="btnSetLaagstePrijs" CssClass="prijsbutton" runat="server" Text="" OnClick="btnSetLaagstePrijs_Click" />
                        </asp:Panel>
                        <span>tot €</span>
                        <asp:Panel ID="pnlHoogstePrijs" DefaultButton="btnSetHoogstePrijs" runat="server">
                            <%--<input type="number" runat="server" id="txtHoogstePrijs" placeholder="100,00" step="0.01" />--%>
                            <asp:TextBox ID="txtHoogstePrijs" runat="server" type="number" step="0"></asp:TextBox>
                            <asp:Button ID="btnSetHoogstePrijs" CssClass="prijsbutton" runat="server" Text="" OnClick="btnSetHoogstePrijs_Click" />
                        </asp:Panel>
                    </div>

                    <label class="voorkeursoort" runat="server" id="lblPrijsOplopend">
                        Stijgend
                        <%--<input type="radio" name="radio" runat="server" id="rbOplopend" AutoPostBack="true" OnCheckedChanged="AddPreference"/>--%>
                        <asp:RadioButton ID="rbStijgend" runat="server" AutoPostBack="true" />
                        <span class="checkmark" style="border-radius: 50%"></span>
                    </label>
                    <label class="voorkeursoort" runat="server" id="lblPrijsDalend">
                        Dalend
                        <%--<input type="radio" name="radio" runat="server" id="rbDalend" AutoPostBack="true" OnCheckedChanged="AddPreference" />--%>
                        <asp:RadioButton ID="rbDalend" runat="server" AutoPostBack="true" />
                        <span class="checkmark" style="border-radius: 50%"></span>
                    </label>
                    <label class="voorkeursoort" runat="server" id="lblPrijsGeen">
                        Meest bezocht
                        <%--<input type="radio" name="radio" checked runat="server" id="rbMeestBezocht" AutoPostBack="true" OnCheckedChanged="AddPreference" />--%>
                        <asp:RadioButton ID="rbMeestBezocht" runat="server" AutoPostBack="true" />
                        <span class="checkmark" style="border-radius: 50%"></span>
                    </label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upItems" runat="server" class="upItems">
        <ContentTemplate>
            <div id="divNoItems" class="divNoItems" runat="server">
                Er zijn geen producten gevonden met deze voorkeuren.
                <br />
                Controleer de zoekterm of voorkeuren indien deze wel kloppen.
            </div>
            <div id="deitems" runat="server" class="deitems">
                <div class="itemlist" id="itemlijst" runat="server">
                    <%--De producten komen hier vanuit code behind in--%>
                </div>
                <div id="thepages">
                    <asp:Button ID="btnPreviousPage" runat="server" Text="" OnClick="btnPreviousPage_Click" Style="background-image: url('../images/arrow_left.png');" />
                    <span id="pages">
                        <asp:Label ID="currentPage" runat="server" Text=""></asp:Label>/
                        <asp:Label ID="totalPages" runat="server" Text=""></asp:Label>
                    </span>
                    <asp:Button ID="btnNextPage" runat="server" Text="" OnClick="btnNextPage_Click" Style="background-image: url('../images/arrow_right.png');" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


