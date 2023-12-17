<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bevestiging.aspx.cs" Inherits="Bevestiging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="bevestigingdiv" runat="server" class="bevestigingsdiv">
        <div id="registratievoltooiendiv" runat="server" class="registratievoltooiendiv">
            <h1>Registreren is bijna voltooid!</h1>
            <p>
                Om uw registratie te voltooien moet u, met de bevestigingsmail, uw mail bevestigen.
                    Heeft u uw mail niet aangekregen? Stuur de mail opnieuw door 
                    <asp:Button ID="btnMailResend" runat="server" Text="hier" OnClick="btnMailResend_Click" />
                te klikken
                of wijzig uw mail door een andere e-mailadres in te vullen om de mail naar te verzenden.
            </p>
            <div>
                <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                <asp:Button ID="btnNewMailResend" runat="server" Text="Verzenden" OnClick="btnNewMailResend_Click" />
            </div>
        </div>
        <div id="registratiebevestigingdiv" runat="server" class="registratiebevestigingdiv">
            <h1>Uw registratie is voltooid!</h1>
            <p>
                Hartelijk dank voor uw registratie. Nu kan u ongelimiteerd shoppen in onze webshop!<br />
                Klik <a href="Default.aspx">hier</a> om naar onze hoofdpagina te gaan.
            </p>
        </div>
        <div id="registratieerrordiv" runat="server" class="registratieerrordiv">
            <h1>Uw registratie is mislukt!</h1>
            <p>
                Uw registratie kon niet bevestigd worden vanwege een foute code. Indien u denkt dat dit een technisch probleem is, gelieve dan contact op te nemen met de gegevens onderaan de pagina.<br />
                Klik <a href="Default.aspx">hier</a> om naar onze hoofdpagina te gaan.
            </p>
        </div>
    </div>
</asp:Content>

