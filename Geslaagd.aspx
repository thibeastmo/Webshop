<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Geslaagd.aspx.cs" Inherits="Geslaagd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="geslaagddiv" runat="server" class="geslaagddiv">
        <div id="betalinggeslaagddiv" runat="server">
            <h1>Betaling is gelukt!</h1>
            <p>
                Bedankt voor uw bestelling!
            </p>
        </div>
        <div id="betalingmisluktdiv" runat="server">
            <h1>Betaling mislukt!</h1>
            <p>
                Oei! De betaling is mislukt.
            </p>
        </div>
    </div>
</asp:Content>

