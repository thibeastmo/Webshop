<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Betalen.aspx.cs" Inherits="Betalien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div>
            <h1>Betaling</h1>
            <asp:Button ID="btnBetaald" CssClass="button" runat="server" OnClick="btnBetaald_Click" Text="Betaal" />
        </div>
    </div>
</asp:Content>

