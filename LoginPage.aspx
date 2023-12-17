<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="Login">
        <div id="logindiv">
            <div id="llogin">
                <asp:Panel ID="pnlLogin" runat="server" DefaultButton="LoginButton">
                    <h2>Aanmelden</h2>
                    <div>
                        <asp:Label runat="server" AssociatedControlID="UserName" ID="UserNameLabel">Mail:</asp:Label>
                        <asp:TextBox runat="server" ID="UserName" placeholder="Mail"></asp:TextBox>
                        <asp:Label runat="server" CssClass="aanmeldenlast" AssociatedControlID="Password" ID="PasswordLabel">Wachtwoord:</asp:Label>
                        <asp:TextBox runat="server" CssClass="aanmeldenlast" TextMode="Password" ID="Password" placeholder="Wachtwoord"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblErrorAanemlden" CssClass="erroraanmelden" runat="server" Text=""></asp:Label><%--Verkeerde wachtwoord of mail. Probeer opnieuw!--%>
                    <label id="aanmeldenlbl" class="customcb">
                        <a>Onthoud mij</a>
                        <input type="checkbox" id ="cbRememberMe" runat="server"/>
                        <span class="checkmark"></span>
                    </label>
                    <asp:Button runat="server" CommandName="Login" Text="Aanmelden" ValidationGroup="Login1" ID="LoginButton" OnClick="LoginButton_Click"></asp:Button>
                </asp:Panel>
            </div>
            <div id="rlogin">
                <asp:Panel ID="pnlRegistreren" runat="server" DefaultButton="btnRegistreren">
                    <h2>Registreren</h2>
                    <div>
                        <asp:Label ID="lblVoornaam" runat="server" Text="Voornaam: "></asp:Label>
                        <asp:TextBox ID="txtVoornaam" runat="server" placeholder="Voornaam"></asp:TextBox>
                        <asp:Label ID="lblAchternaam" runat="server" Text="Achternaam: "></asp:Label>
                        <asp:TextBox ID="txtAchternaam" runat="server" placeholder="Achternaam"></asp:TextBox>
                        <asp:Label ID="lblTel" runat="server" Text="Telefoonnummer: "></asp:Label>
                        <asp:TextBox ID="txtTel" runat="server" placeholder="Telefoonnummer" TextMode="Phone"></asp:TextBox>
                        <asp:Label ID="lblMail" runat="server" Text="Mail: "></asp:Label>
                        <asp:TextBox ID="txtMail" runat="server" placeholder="Mail" TextMode="Email"></asp:TextBox>
                        <asp:Label ID="lblWachtwoord1" runat="server" Text="Wachtwoord: "></asp:Label>
                        <div id="pwachtwoorddiv">
                            <asp:TextBox ID="txtWachtwoord1" runat="server" placeholder="Wachtwoord" TextMode="Password"></asp:TextBox>
                            <asp:TextBox ID="txtWachtwoord2" runat="server" placeholder="Bevestig wachtwoord" TextMode="Password" ToolTip="Dit wachtwoord moet hetzelfde zijn als het andere wachtwoord dat je hierboven hebt getypt. Dit is een controle om er zeker van te zijn dat het wachtwoord correct getypt is."></asp:TextBox>
                        </div>
                    </div>
                    <asp:Label ID="lblErrorRegistreren" runat="server" Text=""></asp:Label><%--Niet alle gegevens zijn ingevuld!--%>
                    <asp:Button ID="btnRegistreren" runat="server" Text="Registreer" OnClick="btnRegistreren_Click" />
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>

