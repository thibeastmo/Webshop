using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for Methods
/// </summary>
public class Methods
{
    private static int minAmount = 10;
    private static int MaxAmount = 25;
    public static string CreatePublicToken()
    {
        Random randomLength = new Random();
        int stringLength = randomLength.Next(minAmount, (MaxAmount + 1));
        string strToken = string.Empty;
        for (int intCounter = 0; intCounter < stringLength; intCounter++)
        {
            Random newRandomCharacter = new Random();
            Thread.Sleep(10);
            strToken += Convert.ToString(newRandomCharacter.Next(0, 10));
        }
        return strToken;
    }
    public static bool Send(string strsendTo, string strsendFrom, string strsubject, string strbody)
    {
        SmtpClient client = new SmtpClient();
        MailMessage msg = new MailMessage();
        try
        {
            //setup SMTP Host Here
            client.Host = "smtp.gmail.com";
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Port = 587;
            System.Net.NetworkCredential smtpCreds = new System.Net.NetworkCredential("thimo@fmgraphics.be", "FMGR4ph18w!");
            client.Credentials = smtpCreds;

            //convert strings to MailAdresses
            MailAddress to = new MailAddress(strsendTo);
            MailAddress from = new MailAddress(strsendFrom);

            //set up message settings
            msg.Subject = strsubject;
            msg.Body = strbody;
            msg.From = from;
            msg.To.Add(to);
            msg.IsBodyHtml = true;

            //Send mail
            client.Send(msg);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public static int CreateBestelling(List<Winkelwagen> winkelwagenlijst, string strDatum, bool Betaald, bool Geleverd)
    {
        string bestellingID = string.Empty;
        int intBestellingID;
        if (winkelwagenlijst != null)
        {
            string strKlantID = winkelwagenlijst[0].klantID;
            bool worked = true;
            try
            {
                bestellingID = Convert.ToString(database.addOvereenkomst(strKlantID, strDatum, Betaald, Geleverd));
            }
            catch (Exception ex)
            {
                worked = false;
                Console.WriteLine(ex.Message);
            }
            if (worked && bestellingID != string.Empty)
            {
                foreach (Winkelwagen item in winkelwagenlijst)
                {
                    try
                    {
                        database.addBestelling(bestellingID, item.oldproductID, item.hoeveel);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        if (bestellingID != string.Empty)
        {
            try
            {
                intBestellingID = Convert.ToInt32(bestellingID);
            }
            catch (Exception ex)
            {
                intBestellingID = 0;
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            intBestellingID = 0;
        }
        return intBestellingID;
    }
    public static void clearCSV(string WhichOne)
    {
        bool validstring = false; 
        string strPath = string.Empty;
        if (WhichOne != string.Empty || WhichOne != null)
        {
            if (WhichOne == "oldproduct")
            {
                validstring = true;
                strPath = "csv/oldproductcsv.csv";
            }
            else if (WhichOne == "product")
            {
                validstring = true;
                strPath = "csv/bestellingencsv.csv";
            }
            else if (WhichOne == "bestellingen")
            {
                validstring = true;
                strPath = "csv/bestellingencsv.csv";
            }
            else if (WhichOne == "productenbestellingencsv")
            {
                validstring = true;
                strPath = "csv/productenbestellingencsv.csv";
            }
            else
            {
                validstring = false;
            }
        }
        strPath = HttpContext.Current.Server.MapPath(strPath);
        if (File.Exists(strPath) && validstring)
        {
            File.WriteAllText(strPath, string.Empty);
        }
    }
    public static string createCSV(string WhichOne, string strSplitValue)
    {
        string strPath = string.Empty;
        try
        {
            List<Product> productlijst = new List<Product>();
            List<Oldproduct> Oldproductlijst = new List<Oldproduct>();
            List<Overeenkomst> Overeenkomstlijst = new List<Overeenkomst>();
            List<Bestelling> Bestellinglijst = new List<Bestelling>();
            if (WhichOne != string.Empty || WhichOne != null)
            {
                if (WhichOne == "oldproduct")
                {
                    strPath = "csv/oldproductcsv.csv";
                    strPath = HttpContext.Current.Server.MapPath(strPath);
                    Oldproductlijst = database.readOldproductList();
                }
                else if (WhichOne == "product")
                {
                    clearCSV(WhichOne);
                    strPath = "csv/productcsv.csv";
                    productlijst = database.readProductlist();
                    strPath = HttpContext.Current.Server.MapPath(strPath);
                }
                else if (WhichOne == "bestellingen")
                {
                    clearCSV(WhichOne);
                    strPath = "csv/bestellingencsv.csv";
                    Overeenkomstlijst = database.readOvereenkomstlist();
                    strPath = HttpContext.Current.Server.MapPath(strPath);
                }
                else if (WhichOne == "productenbestellingen")
                {
                    clearCSV(WhichOne);
                    strPath = "csv/productenbestellingencsv.csv";
                    Bestellinglijst = database.readBestellinglist();
                    strPath = HttpContext.Current.Server.MapPath(strPath);
                }
            }
            List<string> output = new List<string>();
            if (WhichOne == "product")
            {
                if (productlijst != null)
                {
                    output.Add("naam" + strSplitValue + "merk" + strSplitValue + "prijs" + strSplitValue + "hoofdcategorie" + strSplitValue + "geslacht" + strSplitValue + "afbeelding" + strSplitValue + "aantalkeerbekeken" + strSplitValue + "subcategorie" + strSplitValue + "beschrijving");
                    foreach (var aProduct in productlijst)
                    {
                        output.Add($"{ aProduct.productnaam}{strSplitValue}{aProduct.merk}{strSplitValue}{aProduct.prijs}{strSplitValue}{aProduct.hoofdcategorie}{strSplitValue}{aProduct.geslacht}{strSplitValue}{aProduct.afbeelding}{strSplitValue}{aProduct.aantalkeerbekeken}{strSplitValue}{aProduct.subcategorie}{strSplitValue}{aProduct.Beschrijving}");
                    }
                }
            }
            else if (WhichOne == "oldproduct")
            {
                if (Oldproductlijst != null)
                {
                    output.Add("naam" + strSplitValue + "merk" + strSplitValue + "prijs" + strSplitValue + "hoofdcategorie" + strSplitValue + "geslacht" + strSplitValue + "afbeelding" + strSplitValue + "aantalkeerbekeken" + strSplitValue + "subcategorie" + strSplitValue + "beschrijving");
                    foreach (var aProduct in Oldproductlijst)
                    { //naam - merk - prijs - hoofdcategorie - geslacht - afbeelding - aantalkeerbekeken - subcategorie - beschrijving
                        output.Add($"{ aProduct.productnaam}{strSplitValue}{aProduct.merk}{strSplitValue}{aProduct.prijs}{strSplitValue}{aProduct.hoofdcategorie}{strSplitValue}{aProduct.geslacht}{strSplitValue}{aProduct.afbeelding}{strSplitValue}{aProduct.aantalkeerbekeken}{strSplitValue}{aProduct.subcategorie}{strSplitValue}{aProduct.Beschrijving}");
                    }
                }
            }
            else if (WhichOne == "bestellingen")
            {
                if (Overeenkomstlijst != null)
                {
                    output.Add("bestellingID" + strSplitValue + "klantID" + strSplitValue + "datum" + strSplitValue + "betaald/niet betaald" + strSplitValue + "bezorgd/niet bezorgd");
                    foreach (var aOvereenkomst in Overeenkomstlijst)
                    { //bestellingID - klantID - datum - betaald - bezorgd
                        string strBezorgd = string.Empty;
                        string strBetaald = string.Empty;
                        if (aOvereenkomst.bezorgd == 0)
                        {
                            strBezorgd = "Niet bezorgd";
                        }
                        else
                        {
                            strBezorgd = "Bezorgd";
                        }
                        if (aOvereenkomst.betaald == 0)
                        {
                            strBetaald = "Niet betaald";
                        }
                        else
                        {
                            strBetaald = "Betaald";
                        }
                        output.Add($"{ aOvereenkomst.bestellingID}{strSplitValue}{aOvereenkomst.klantID}{strSplitValue}{aOvereenkomst.datum}{strSplitValue}{strBetaald}{strSplitValue}{strBezorgd}");
                    }
                }
            }
            else if (WhichOne == "productenbestellingen")
            {
                if (Bestellinglijst != null)
                {
                    output.Add("bestellingID" + strSplitValue + "hoeveel" + strSplitValue + "naam" + strSplitValue + "merk" + strSplitValue + "prijs" + strSplitValue + "hoofdcategorie" + strSplitValue + "geslacht" + strSplitValue + "afbeelding" + strSplitValue + "subcategorie" + strSplitValue + "beschrijving");
                    foreach (var aBestelling in Bestellinglijst)
                    { //bestellingID - hoeveel - naam - merk - prijs - hoofdcategorie - geslacht - afbeelding - subcategorie - beschrijving
                        Oldproduct aOldproduct = new Oldproduct();
                        try
                        {
                            aOldproduct = database.readOldproduct(aBestelling.oldproductID.ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (aOldproduct != null)
                        {
                            output.Add($"{aBestelling.bestellingID}{strSplitValue}{aBestelling.hoeveel}{strSplitValue}{aOldproduct.productnaam}{strSplitValue}{aOldproduct.merk}{strSplitValue}{aOldproduct.prijs}{strSplitValue}{aOldproduct.hoofdcategorie}{strSplitValue}{aOldproduct.geslacht}{strSplitValue}{aOldproduct.afbeelding}{strSplitValue}{aOldproduct.subcategorie}{strSplitValue}{aOldproduct.Beschrijving}{strSplitValue}");
                        }
                        else
                        {
                            int intMax = 8;
                            string strOutput = aBestelling.bestellingID.ToString() + strSplitValue + aBestelling.hoeveel.ToString() + strSplitValue;
                            for (int intCounter = 0; intCounter < intMax; intCounter++)
                            {
                                if (intCounter+1 == intMax)
                                {
                                    strOutput += "Waarde kon niet geladen worden";
                                }
                                else
                                {
                                    strOutput += "Waarde kon niet geladen worden" + strSplitValue;
                                }
                            }
                            output.Add(strOutput);
                        }
                    }
                }
            }
            File.WriteAllLines(strPath, output);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return strPath;
    }
}
       