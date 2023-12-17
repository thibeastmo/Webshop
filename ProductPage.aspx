<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProductPage.aspx.cs" Inherits="ProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section id="Productinfo">
        <div id="productinfodiv" class="productinfodiv">
            <div id="lproductinfo">
                <asp:Image ID="imgproduct" runat="server" ImageUrl="images/fidget_spinner.jpg"></asp:Image>
            </div>
            <div id="rproductinfo">
                <div>
                    <asp:Label ID="lblProductnaam" runat="server" Text="DeProductNaam"></asp:Label>
                    <div>
                        <asp:Label ID="lblMerk" runat="server" Text="HetMerk"></asp:Label>
                        <asp:Label ID="lblPrijs" runat="server" Text="Deprijs"></asp:Label>
                        <asp:Button ID="btnInWinkelwagen" CssClass="btnwinkelwagen btnwinkelwagenx" runat="server" Text="In winkelwagen" OnClick="AddToWinkelwagen" />
                        <asp:Button ID="btnFavoriet"  CssClass="btnfavoriet btnfavorietx" runat="server" Text="Favoriet" OnClick="AddToFavorieten" />
                    </div>
                    <p id="pproduct" runat="server">Dit is een lange tekst dat niet vanuit code behind in deze site is gezet.</p>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

