<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="ProfilePage" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="Profielpagina">
        <div>
            <div id="profielknoppen">
                <asp:Button ID="btnGegevens" runat="server" Text="Gegevens" OnClick="btnClick" />
                <asp:Button ID="btnFavorieten" runat="server" Text="Favorieten" OnClick="btnClick" />
                <asp:Button ID="btnWinkelwagen" runat="server" Text="Winkelwagen" OnClick="btnClick" />
                <asp:Button ID="btnBestellingen" runat="server" Text="Bestellingen" OnClick="btnClick" />
                <asp:Button ID="btnNaarSite" runat="server" Text="Naar site" OnClick="btnClick" />
                <asp:Button ID="btnLogUit" runat="server" Text="Log uit" OnClick="btnClick" />
                <asp:Button ID="btnUploadtab" runat="server" Text="Upload" OnClick="btnClick" />
                <asp:Button ID="btnOverzicht" runat="server" Text="Overzicht" OnClick="btnClick" />
            </div>
            <div id="profielgegevens">
                <asp:Panel ID="pnlGegevens" runat="server" CssClass="pnlgegevens">
                    <h2>Persoonlijke gegevens</h2>
                    <label class="toggle">
                        <asp:CheckBox ID="cbToggle" runat="server" OnCheckedChanged="cbToggle_CheckedChanged" AutoPostBack="true" />
                        <span class="slider round"></span>
                    </label>
                    <span>Bewerken inschakelen</span>
                    <asp:Label ID="lblErrorGegegevens" CssClass="lble" runat="server" Text=""></asp:Label>
                    <br />
                    <div id="persgegdiv">
                        <asp:Label ID="lblVoornaam" runat="server" Text="Voornaam: "></asp:Label>
                        <asp:TextBox ID="txtVoornaam" runat="server" placeholder="Voornaam"></asp:TextBox>
                        <asp:Label ID="lblAchternaam" runat="server" Text="Achternaam: "></asp:Label>
                        <asp:TextBox ID="txtAchternaam" runat="server" placeholder="Achternaam"></asp:TextBox>
                        <asp:Label ID="lblTel" runat="server" Text="Telefoonnummer: "></asp:Label>
                        <asp:TextBox ID="txtTel" runat="server" placeholder="Telefoonnummer" TextMode="Phone"></asp:TextBox>
                        <asp:Label ID="lblMail" runat="server" Text="Mail: "></asp:Label>
                        <asp:TextBox ID="txtMail" runat="server" placeholder="Mail" TextMode="Email"></asp:TextBox>
                        <asp:Label ID="lblWachtwoord" runat="server" Text="Wachtwoord: "></asp:Label>
                        <div>
                            <asp:TextBox ID="txtWachtwoord1" runat="server" placeholder="Wachtwoord" TextMode="Password"></asp:TextBox>
                            <asp:TextBox ID="txtWachtwoord2" CssClass="wachtwoordtwee" runat="server" placeholder="Bevestig wachtwoord" TextMode="Password" ToolTip="Dit wachtwoord moet hetzelfde zijn als het andere wachtwoord dat je hierboven hebt getypt. Dit is een controle om er zeker van te zijn dat het wachtwoord correct getypt is."></asp:TextBox>
                        </div>
                    </div>
                    <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Opslaan" OnClick="btnSave_Click" />
                </asp:Panel>
                <asp:Panel ID="pnlFavorieten" runat="server">
                    <h2>Favorieten</h2>
                    <div id="divNoFavorieten" class="divNoItems" runat="server">
                        Je hebt nog geen favorieten.
                    </div>
                    <div id="fitems" runat="server" class="fitems">
                        <%--De producten komen hier vanuit code behind in--%>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlBestellingen" runat="server">
                    <h2>Bestellingen</h2>
                    <div id="mainbestellingdiv" runat="server" class="mainbestellingdiv">
                        <%--De bestellingen komen hier vanuit code behind in--%>
                    </div>
                    <div id="nobestellingdiv" class="divNoItems" runat="server">
                        <p>
                            U heeft nog geen bestellingen gemaakt.
                        </p>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlUpload" runat="server">
                    <h2>Upload</h2>
                    <div id="uploaduitleg">
                        <span>Als je een nieuw csv bestand importeert dan worden deze producten in 2 datalijsten gezet:</span><br />
                        <ul>
                            <li>datalijst van producten die in de webshop beschikbaar zijn (Productenlijst A)</li>
                            <li>datalijst van producten die ooit in de webshop beschikbaar waren (Productenlijst B)</li>
                        </ul>
                        <span>Dit dient voor het behouden van favorieten en bestellingen zelfs nadat de producten niet meer beschikbaar zijn in de webshop.</span><br />
                        <span>De splitwaarde kan gebruikt worden als waarde waarmee je de variabelen wilt opsplitsen in het csv bestand <strong>en</strong> als waarde waarmee de variabelen, in het zelf gemaakte csv bestand, opgesplitst zijn.</span>
                    </div>
                    <div id="uploaddiv" runat="server" class="uploaddiv">
                        <asp:Label ID="lblUpload" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txtSplitValue" runat="server" placeholder="Splitwaarde"></asp:TextBox>
                        <div>
                            <div>
                                <asp:Label ID="lblUploadUpload" runat="server" Text="Upload een csv bestand door het te importeren en op de knop 'Upload' te klikken."></asp:Label>
                                <div>
                                    <label class="file-upload">
                                        <span class="button fubutton">Bladeren</span>
                                        <%--<asp:Button ID="btnfu" CssClass="button fubutton" runat="server" Text="Bladeren" OnClick="btnfu_Click" />--%>
                                        <span class="futxt" id="futxt" runat="server"></span>
                                        <%--<asp:TextBox ID="futxt" CssClass="futxt" runat="server"></asp:TextBox>--%>
                                        <asp:FileUpload ID="fuUploadproducten" CssClass="fuinput" runat="server" onchange="changefutxt(this)"></asp:FileUpload>
                                        <%--<asp:FileUpload ID="fuUploadproducten" CssClass="fuinput" runat="server" onchange="this.form.submit()"></asp:FileUpload>--%>
                                    </label>
                                    <asp:Button ID="btnUpload" CssClass="button" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                </div>
                            </div>
                            <div>
                                <asp:Label ID="lblGenerateProductCSV" runat="server" Text="Genereer een csv bestand van productenlijst A."></asp:Label>
                                <asp:Button ID="btnGenerateProductCSV" CssClass="button" runat="server" Text="Genereer" OnClick="btnGenerateProductCSV_Click" />
                            </div>
                            <div>
                                <asp:Label ID="lblGenerateOldProductCSV" runat="server" Text="Genereer een csv bestand van productenlijst B."></asp:Label>
                                <asp:Button ID="btnGenerateOldProductCSV" CssClass="button" runat="server" Text="Genereer" OnClick="btnGenerateOldProductCSV_Click" />
                            </div>
                        </div>
                    </div>
                    <script>
                        function changefutxt(input) {
                            x = document.getElementById('<%= fuUploadproducten.ClientID %>').value;
                            x = x.split('\\');
                            y = x[x.length - 1];
                            document.getElementById('<%= futxt.ClientID %>').innerHTML = y;
                        }
                    </script>
                </asp:Panel>
                <asp:Panel ID="pnlOverzicht" runat="server">
                    <h2>Overzicht</h2>
                    <div id="overzichtdiv" runat="server" class="overzichtdiv">
                        <asp:Label ID="lblOverzichtError" runat="server" Text=""></asp:Label>
                        <div>
                            <div id="genereeroverzicht">
                                <div>
                                    <div>
                                        <span>Genereer een csv bestand van alle Bestellingen</span>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnGenereerBestellingen" runat="server" Text="Genereer" CssClass="button" OnClick="btnGenereerBestellingen_Click" />
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <span>Genereer een csv bestand van alle producten van alle bestellingen</span>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnGenereerProductenBestellingen" runat="server" Text="Genereer" CssClass="button" OnClick="btnGenereerProductenBestellingen_Click" />
                                    </div>
                                </div>
                            </div>
                            <div id="klantidoverzicht">
                                <div>
                                    <asp:TextBox ID="txtKlantIDInput" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnKlantIDInput" CssClass="button" runat="server" Text="Bekijk" OnClick="btnKlantIDInput_Click" />
                                </div>
                                <div>
                                    <div>
                                        <asp:Label ID="lblOverzichtHeaderKlantID" runat="server" Text="KlantID"></asp:Label>
                                        <asp:Label ID="lblOverzichtHeaderVoornaam" runat="server" Text="Voornaam"></asp:Label>
                                        <asp:Label ID="lblOverzichtHeaderAchternaam" runat="server" Text="Achternaam"></asp:Label>
                                        <asp:Label ID="lblOverzichtHeaderMail" runat="server" Text="Mail"></asp:Label>
                                        <asp:Label ID="lblOverzichtHeaderTelefoonnummer" runat="server" Text="Telefoonnummer"></asp:Label>
                                        <asp:Label ID="lblOverzichtHeaderBevestigd" runat="server" Text="Bevestigd"></asp:Label>
                                        <asp:Label ID="lblOverzichtHeaderAdmin" runat="server" Text="Rol"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblOverzichtKlantID" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblOverzichtVoornaam" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblOverzichtAchternaam" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblOverzichtMail" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblOverzichtTelefoonnummer" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblOverzichtBevestigd" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblOverzichtAdmin" runat="server" Text=""></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>

