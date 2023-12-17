<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="defaultbody">
        <div id="meestBekeken" runat="server" class="mbekekenlist">
            <h1>Populairste producten!</h1>
            <div runat="server" id="mbekekenproducten">
                <%--De meest bekeken items komen hier tevoorschijn door code behind--%>
            </div>
        </div>
        <div id="hoofdcategorieen" runat="server" class="hoofdcategorieen">
            <%--De hoofdcategorieen komen hier tevoorschijn door code behind--%>
        </div>
    </div>
</asp:Content>

