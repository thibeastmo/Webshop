using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities.Tracking.Configuration;
using System.Web;

/// <summary>
/// Summary description for database
/// </summary>
public class database
{
    static string strConnstring = "webshopConnectionString";
    #region Product
    public static List<Product> readRowsProducten(int intRows, string strColumnameToSort)
    {
        List<Product> Rows = new List<Product>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "SELECT * FROM tblproducten ORDER BY " + strColumnameToSort + " DESC LIMIT 0," + intRows.ToString() + " ;";
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();

        while (reader.Read())
        {
            Product aProduct = new Product();
            aProduct.productID = Convert.ToInt32(reader["productID"]);
            aProduct.productnaam = reader["productnaam"].ToString();
            aProduct.merk = reader["merk"].ToString();
            aProduct.prijs = Convert.ToDecimal(reader["prijs"]);
            aProduct.subcategorie = reader["subcategorie"].ToString();
            aProduct.geslacht = reader["geslacht"].ToString();
            aProduct.afbeelding = reader["afbeelding"].ToString();
            aProduct.aantalkeerbekeken = Convert.ToInt32(reader["aantalkeerbekeken"]);
            aProduct.hoofdcategorie = reader["hoofdcategorie"].ToString();
            aProduct.Beschrijving = reader["beschrijving"].ToString();
            aProduct.oldproductID = Convert.ToInt32(reader["oldproductID"]);
            Rows.Add(aProduct);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return Rows;
    }
    public static Product readProduct(string strProductID)
    {
        Product aProduct = new Product();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblproducten where productID = @ProductID";
        mysqlcom.Parameters.Add("@ProductID", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@ProductID"].Value = strProductID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            aProduct.productID = Convert.ToInt32(reader["productID"]);
            aProduct.productnaam = reader["productnaam"].ToString();
            aProduct.merk = reader["merk"].ToString();
            aProduct.prijs = Convert.ToDecimal(reader["prijs"]);
            aProduct.subcategorie = reader["subcategorie"].ToString();
            aProduct.geslacht = reader["geslacht"].ToString();
            aProduct.afbeelding = reader["afbeelding"].ToString();
            aProduct.aantalkeerbekeken = Convert.ToInt32(reader["aantalkeerbekeken"]);
            aProduct.hoofdcategorie = reader["hoofdcategorie"].ToString();
            aProduct.Beschrijving = reader["beschrijving"].ToString();
            aProduct.oldproductID = Convert.ToInt32(reader["oldproductID"]);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return aProduct;
    }
    public static Product readProductbyOldproductID(string strOldproductID)
    {
        Product aProduct = new Product();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblproducten where oldproductID = @oldproductID";
        mysqlcom.Parameters.Add("@oldproductID", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@oldproductID"].Value = strOldproductID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            aProduct.productID = Convert.ToInt32(reader["productID"]);
            aProduct.productnaam = reader["productnaam"].ToString();
            aProduct.merk = reader["merk"].ToString();
            aProduct.prijs = Convert.ToDecimal(reader["prijs"]);
            aProduct.subcategorie = reader["subcategorie"].ToString();
            aProduct.geslacht = reader["geslacht"].ToString();
            aProduct.afbeelding = reader["afbeelding"].ToString();
            aProduct.aantalkeerbekeken = Convert.ToInt32(reader["aantalkeerbekeken"]);
            aProduct.hoofdcategorie = reader["hoofdcategorie"].ToString();
            aProduct.Beschrijving = reader["beschrijving"].ToString();
            aProduct.oldproductID = Convert.ToInt32(reader["oldproductID"]);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return aProduct;
    }
    public static List<Product> readProductlist()
    {
        string strCommand = "select * from tblproducten";
        List<string> x = readColumn(strCommand, null);
        if (x.Count > 0)
        {
            return readRowsProducten(x.Count, "productID");
        }
        else
        {
            return null;
        }
    }
    public static void editProduct(string strID, string strVariabele, string strColumn)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tblproducten SET " + strColumn + " = @variabele WHERE productID = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static long addProduct(Product newProduct)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblproducten (productnaam, merk, prijs, hoofdcategorie, geslacht, afbeelding, aantalkeerbekeken, subcategorie, beschrijving, oldproductID) VALUES (@productnaam, @merk, @prijs, @hoofdcategorie, @geslacht, @afbeelding, @aantalkeerbekeken, @subcategorie, @beschrijving, @oldproductID);";
        mysqlcom.Parameters.Add("@productnaam", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@productnaam"].Value = newProduct.productnaam;
        mysqlcom.Parameters.Add("@merk", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@merk"].Value = newProduct.merk;
        mysqlcom.Parameters.Add("@prijs", MySqlDbType.VarChar, 75);
        mysqlcom.Parameters["@prijs"].Value = newProduct.prijs.ToString().Replace(",", ".");
        mysqlcom.Parameters.Add("@hoofdcategorie", MySqlDbType.VarChar, 100);
        mysqlcom.Parameters["@hoofdcategorie"].Value = newProduct.hoofdcategorie;
        mysqlcom.Parameters.Add("@geslacht", MySqlDbType.VarChar, 125);
        mysqlcom.Parameters["@geslacht"].Value = newProduct.geslacht;
        mysqlcom.Parameters.Add("@afbeelding", MySqlDbType.VarChar, 150);
        mysqlcom.Parameters["@afbeelding"].Value = newProduct.afbeelding;
        mysqlcom.Parameters.Add("@aantalkeerbekeken", MySqlDbType.VarChar, 175);
        mysqlcom.Parameters["@aantalkeerbekeken"].Value = newProduct.aantalkeerbekeken;
        mysqlcom.Parameters.Add("@subcategorie", MySqlDbType.VarChar, 200);
        mysqlcom.Parameters["@subcategorie"].Value = newProduct.subcategorie;
        mysqlcom.Parameters.Add("@beschrijving", MySqlDbType.VarChar, 225);
        mysqlcom.Parameters["@beschrijving"].Value = newProduct.Beschrijving;
        mysqlcom.Parameters.Add("@oldproductID", MySqlDbType.VarChar, 250);
        mysqlcom.Parameters["@oldproductID"].Value = newProduct.oldproductID;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        long productID = mysqlcom.LastInsertedId;
        mysqalcon.Close();
        return productID;
    }
    public static void removeProduct(string strProductID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "DELETE FROM tblproducten WHERE productID = '" + strProductID + "';";
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    #endregion
    #region Klant
    public static Klant readKlant(string strKlantID)
    {
        Klant aKlant = new Klant();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblklanten where klantID = @KlantID";
        mysqlcom.Parameters.Add("@KlantID", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@KlantID"].Value = strKlantID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            aKlant.klantID = reader["klantID"].ToString();
            aKlant.voornaam = reader["voornaam"].ToString();
            aKlant.achternaam = reader["achternaam"].ToString();
            aKlant.mail = reader["mail"].ToString();
            aKlant.telefoonnummer = reader["telefoonnummer"].ToString();
            aKlant.wachtwoord = reader["wachtwoord"].ToString();
            aKlant.bevestigd = reader["bevestigd"].ToString();
            if (Convert.ToInt32(reader["admin"]) == 1)
            {
                aKlant.admin = true;
            }
            else
            {
                aKlant.admin = false;
            }
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return aKlant;
    }
    public static long addKlant(Klant newKlant)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblklanten (voornaam, achternaam, mail, telefoonnummer, wachtwoord, bevestigd) VALUES (@voornaam, @achternaam, @mail, @telefoonnummer, @wachtwoord, @bevestigd);";
        mysqlcom.Parameters.Add("@voornaam", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@voornaam"].Value = newKlant.voornaam;
        mysqlcom.Parameters.Add("@achternaam", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@achternaam"].Value = newKlant.achternaam;
        mysqlcom.Parameters.Add("@mail", MySqlDbType.VarChar, 75);
        mysqlcom.Parameters["@mail"].Value = newKlant.mail;
        mysqlcom.Parameters.Add("@telefoonnummer", MySqlDbType.VarChar, 100);
        mysqlcom.Parameters["@telefoonnummer"].Value = newKlant.telefoonnummer;
        mysqlcom.Parameters.Add("@wachtwoord", MySqlDbType.VarChar, 125);
        mysqlcom.Parameters["@wachtwoord"].Value = newKlant.wachtwoord;
        mysqlcom.Parameters.Add("@bevestigd", MySqlDbType.VarChar, 150);
        mysqlcom.Parameters["@bevestigd"].Value = newKlant.bevestigd;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        long klantID = mysqlcom.LastInsertedId;
        mysqalcon.Close();
        return klantID;
    }
    public static void editKlant(string strID, string strVariabele, string strColumn)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tblklanten SET " + strColumn + " = @variabele WHERE klantID = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    #endregion
    #region Winkelwagen
    public static List<Winkelwagen> readWinkelwagen(string strKlantID)
    {
        string strCommand = "select * from tblwinkelwagen where klantID = @variabele";
        List<string> x = readColumn(strCommand, strKlantID);
        if (x.Count > 0)
        {
            return readRowsWinkelwagen(x.Count, "klantID", strKlantID);
        }
        else
        {
            return null;
        }
    }
    public static long addWinkelwagen(string strKlantID, string strOldproductID, int intAmount)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblwinkelwagen (klantID, oldproductID, hoeveel) VALUES (@klantid, @oldproductID, @amount);";
        mysqlcom.Parameters.Add("@klantid", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@klantid"].Value = strKlantID;
        mysqlcom.Parameters.Add("@oldproductID", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@oldproductID"].Value = strOldproductID;
        mysqlcom.Parameters.Add("@amount", MySqlDbType.VarChar, 75);
        mysqlcom.Parameters["@amount"].Value = intAmount.ToString();
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        long dataID = mysqlcom.LastInsertedId;
        mysqalcon.Close();
        return dataID;
    }
    public static void editWinkelwagen(string strKlantID, string strOldproductID, string strVariabele, string strColumn)
    {
        if (strVariabele == "0" && strColumn == "Hoeveel")
        { //Verwijder uit database
            //Maak connectie met de databank
            MySqlConnection mysqalcon = new MySqlConnection();
            MySqlCommand mysqlcom = new MySqlCommand();
            mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
            //Commando-object
            mysqlcom.Connection = mysqalcon;
            mysqlcom.CommandText = "DELETE FROM tblwinkelwagen WHERE klantID = " + strKlantID + " AND oldproductID = '" + strOldproductID + "';";
            mysqalcon.Open();
            mysqlcom.ExecuteNonQuery();
            mysqalcon.Close();
        }
        else
        { //Pas gewoon aan
            //Maak connectie met de databank
            MySqlConnection mysqalcon = new MySqlConnection();
            MySqlCommand mysqlcom = new MySqlCommand();
            mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
            //Commando-object
            mysqlcom.Connection = mysqalcon;
            mysqlcom.CommandText = "UPDATE tblwinkelwagen SET " + strColumn + " = @variabele WHERE klantID = " + strKlantID + " AND oldproductID = '" + strOldproductID + "';";
            mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
            mysqlcom.Parameters["@variabele"].Value = strVariabele;
            mysqalcon.Open();
            mysqlcom.ExecuteNonQuery();
            mysqalcon.Close();
        }
    }
    public static void removeFromWinkelwagen(string strKlantID, string strOldproductID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "DELETE FROM tblwinkelwagen WHERE klantID = " + strKlantID + " AND oldproductID = '" + strOldproductID + "';";
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static List<Winkelwagen> readRowsWinkelwagen(int intRows, string strColumnameToSort, string strKlantID)
    {
        List<Winkelwagen> Rows = new List<Winkelwagen>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "SELECT * FROM tblwinkelwagen WHERE klantID = " + strKlantID + " ORDER BY " + strColumnameToSort + " DESC LIMIT 0," + intRows.ToString() + ";";
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();

        while (reader.Read())
        {
            Winkelwagen aWinkelwagen = new Winkelwagen();
            aWinkelwagen.xID = reader["xID"].ToString();
            aWinkelwagen.klantID = reader["klantID"].ToString();
            aWinkelwagen.oldproductID = reader["oldproductID"].ToString();
            aWinkelwagen.hoeveel = Convert.ToInt32(reader["hoeveel"]);
            Rows.Add(aWinkelwagen);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return Rows;
    }
    #endregion
    #region Favoriet
    public static List<Favorieten> readFavorieten(string strKlantID)
    {
        string strCommand = "select * from tblfavorieten where klantID = @variabele";
        List<string> x = readColumn(strCommand, strKlantID);
        if (x.Count > 0)
        {
            return readRowsFavorieten(x.Count, "klantID", strKlantID);
        }
        else
        {
            return null;
        }
    }
    public static long addFavoriet(string strKlantID, string strBesteldeID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblfavorieten (klantID, oldproductID) VALUES (@klantid, @oldproductID);";
        mysqlcom.Parameters.Add("@klantid", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@klantid"].Value = strKlantID;
        mysqlcom.Parameters.Add("@oldproductID", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@oldproductID"].Value = strBesteldeID;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        long xID = mysqlcom.LastInsertedId;
        mysqalcon.Close();
        return xID;
    }
    public static void removeFavoriet(string strKlantID, string strBesteldeID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "DELETE FROM tblfavorieten WHERE klantID = " + strKlantID + " AND oldproductID = '" + strBesteldeID + "';";
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static List<Favorieten> readRowsFavorieten(int intRows, string strColumnameToSort, string strKlantID)
    {
        List<Favorieten> Rows = new List<Favorieten>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "SELECT * FROM tblfavorieten WHERE klantID = " + strKlantID + " ORDER BY " + strColumnameToSort + " DESC LIMIT 0," + intRows.ToString() + ";";
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();

        while (reader.Read())
        {
            Favorieten aFavoriet = new Favorieten();
            aFavoriet.xID = reader["xID"].ToString();
            aFavoriet.klantID = reader["klantID"].ToString();
            aFavoriet.oldproductID = Convert.ToInt32(reader["oldproductID"]);
            Rows.Add(aFavoriet);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return Rows;
    }
    #endregion
    #region Bestelling
    public static List<Bestelling> readRowsBestelling(int intRows, string strColumnameToSort, int intBestellingID)
    {
        List<Bestelling> Rows = new List<Bestelling>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "SELECT * FROM tblbestellingen WHERE bestellingID = " + intBestellingID.ToString() + " ORDER BY " + strColumnameToSort + " DESC LIMIT 0," + intRows.ToString() + " ;";
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();

        while (reader.Read())
        {
            Bestelling aBestelling = new Bestelling();
            aBestelling.dataID = Convert.ToInt32(reader["dataID"]);
            aBestelling.bestellingID = Convert.ToInt32(reader["bestellingID"]);
            aBestelling.oldproductID = Convert.ToInt32(reader["oldproductID"]);
            aBestelling.hoeveel = Convert.ToInt32(reader["hoeveel"]);
            Rows.Add(aBestelling);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return Rows;
    }
    public static long addBestelling(string strBestellingID, string strOldroductID, int hoeveel)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblbestellingen (bestellingID, oldproductID, hoeveel) VALUES (@bestellingID, @oldproductID, @hoeveel);";
        mysqlcom.Parameters.Add("@bestellingID", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@bestellingID"].Value = strBestellingID;
        mysqlcom.Parameters.Add("@oldproductID", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@oldproductID"].Value = strOldroductID;
        mysqlcom.Parameters.Add("@hoeveel", MySqlDbType.VarChar, 75);
        mysqlcom.Parameters["@hoeveel"].Value = hoeveel;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        long bestellingID = mysqlcom.LastInsertedId;
        mysqalcon.Close();
        return bestellingID;
    }
    public static void removeBestelling(string strBestellingID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "DELETE FROM tblbestellingen WHERE bestellingID = '" + strBestellingID + "';";
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static List<Bestelling> readBestellinglist()
    {
        List<Bestelling> Rows = new List<Bestelling>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "SELECT * FROM tblbestellingen;";
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();

        while (reader.Read())
        {
            Bestelling aBestelling = new Bestelling();
            aBestelling.dataID = Convert.ToInt32(reader["dataID"]);
            aBestelling.bestellingID = Convert.ToInt32(reader["bestellingID"]);
            aBestelling.oldproductID = Convert.ToInt32(reader["oldproductID"]);
            aBestelling.hoeveel = Convert.ToInt32(reader["hoeveel"]);
            Rows.Add(aBestelling);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return Rows;
    }
    #endregion
    #region Overeenkomst
    public static List<Overeenkomst> readRowsOvereenkomst(int intRows, string strColumnameToSort, int intBestellingID)
    {
        List<Overeenkomst> Rows = new List<Overeenkomst>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "SELECT * FROM tblovereenkomst WHERE bestellingID = " + intBestellingID.ToString() + "  ORDER BY " + strColumnameToSort + " DESC LIMIT 0," + intRows.ToString() + ";";
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();

        while (reader.Read())
        {
            Overeenkomst aOvereenkomst = new Overeenkomst();
            aOvereenkomst.bestellingID = Convert.ToInt32(reader["bestellingID"]);
            aOvereenkomst.klantID = reader["klantID"].ToString();
            aOvereenkomst.datum = reader["datum"].ToString();
            aOvereenkomst.betaald = Convert.ToInt32(reader["betaald"]);
            aOvereenkomst.bezorgd = Convert.ToInt32(reader["bezorgd"]);
            Rows.Add(aOvereenkomst);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        //string strCommand = "SELECT * FROM tblbestellingen WHERE bestellingID = @variabele";
        //List<string> databestellinglijst = readColumn(strCommand, aOvereenkomst.bestellingID.ToString());
        //List<Bestelling> overeenkomstbestellinglijst = readRowsBestelling(databestellinglijst.Count(), "bestellingID", aOvereenkomst.bestellingID);
        //aOvereenkomst.Bestellingproducten = overeenkomstbestellinglijst;


        return Rows;
    }
    public static long addOvereenkomst(string strKlantID, string strDate, bool Betaald, bool Geleverd)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblovereenkomst (klantID, datum, betaald, bezorgd) VALUES (@klantID, @date, @betaald, @geleverd);";
        mysqlcom.Parameters.Add("@klantID", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@klantID"].Value = strKlantID;
        mysqlcom.Parameters.Add("@date", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@date"].Value = strDate;
        mysqlcom.Parameters.Add("@betaald", MySqlDbType.VarChar, 75);
        if (Betaald)
        {
            mysqlcom.Parameters["@betaald"].Value = "1";
        }
        else
        {
            mysqlcom.Parameters["@betaald"].Value = "0";
        }
        mysqlcom.Parameters.Add("@geleverd", MySqlDbType.VarChar, 100);
        if (Geleverd)
        {
            mysqlcom.Parameters["@geleverd"].Value = "1";
        }
        else
        {
            mysqlcom.Parameters["@geleverd"].Value = "0";
        }
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        long bestellingID = mysqlcom.LastInsertedId;
        return bestellingID;
    }
    public static void editOvereenkomst(string strID, string strVariabele, string strColumn)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tblovereenkomst SET " + strColumn + " = @variabele WHERE bestellingID = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static Overeenkomst readOvereenkomst(string strBestellingID)
    {
        Overeenkomst aOvereenkomst = new Overeenkomst();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblovereenkomst where bestellingID = @bestellingID";
        mysqlcom.Parameters.Add("@bestellingID", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@bestellingID"].Value = strBestellingID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            aOvereenkomst.bestellingID = Convert.ToInt32(reader["bestellingID"]);
            aOvereenkomst.klantID = reader["klantID"].ToString();
            aOvereenkomst.datum = reader["datum"].ToString();
            aOvereenkomst.betaald = Convert.ToInt32(reader["betaald"]);
            aOvereenkomst.bezorgd = Convert.ToInt32(reader["bezorgd"]);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        string strCommand = "SELECT bestellingID FROM tblbestellingen WHERE bestellingID = @variabele";
        List<string> databestellinglijst = readColumn(strCommand, aOvereenkomst.bestellingID.ToString());
        List<Bestelling> overeenkomstbestellinglijst = readRowsBestelling(databestellinglijst.Count(), "bestellingID", Convert.ToInt32(strBestellingID));
        aOvereenkomst.Bestellingproducten = overeenkomstbestellinglijst;

        return aOvereenkomst;
    }
    public static void removeOvereenkomst(string strBestellingID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "DELETE FROM tblovereenkomst WHERE bestellingID = '" + strBestellingID + "';";
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static List<Overeenkomst> readOvereenkomstlist()
    {
        List<Overeenkomst> Rows = new List<Overeenkomst>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "SELECT * FROM tblovereenkomst;";
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();

        while (reader.Read())
        {
            Overeenkomst aOvereenkomst = new Overeenkomst();
            aOvereenkomst.bestellingID = Convert.ToInt32(reader["bestellingID"]);
            aOvereenkomst.klantID = reader["klantID"].ToString();
            aOvereenkomst.datum = reader["datum"].ToString();
            aOvereenkomst.betaald = Convert.ToInt32(reader["betaald"]);
            aOvereenkomst.bezorgd = Convert.ToInt32(reader["bezorgd"]);
            Rows.Add(aOvereenkomst);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return Rows;
    }
    #endregion
    #region Oldproduct
    public static Oldproduct readOldproduct(string strOldproductID)
    {
        Oldproduct aOldproduct = new Oldproduct();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblOldproducten where OldproductID = @OldproductID";
        mysqlcom.Parameters.Add("@OldproductID", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@OldproductID"].Value = strOldproductID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            aOldproduct.oldproductID = Convert.ToInt32(reader["OldproductID"]);
            aOldproduct.productnaam = reader["productnaam"].ToString();
            aOldproduct.merk = reader["merk"].ToString();
            aOldproduct.prijs = Convert.ToDecimal(reader["prijs"]);
            aOldproduct.subcategorie = reader["subcategorie"].ToString();
            aOldproduct.geslacht = reader["geslacht"].ToString();
            aOldproduct.afbeelding = reader["afbeelding"].ToString();
            aOldproduct.aantalkeerbekeken = Convert.ToInt32(reader["aantalkeerbekeken"]);
            aOldproduct.hoofdcategorie = reader["hoofdcategorie"].ToString();
            aOldproduct.Beschrijving = reader["beschrijving"].ToString();
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return aOldproduct;
    }
    public static List<Oldproduct> readOldproductList()
    {
        string strCommand = "select * from tbloldproducten";
        List<string> x = readColumn(strCommand, null);
        if (x.Count > 0)
        {
            return readRowsOldproduct(x.Count, "oldproductID");
        }
        else
        {
            return null;
        }
    }
    public static long addOldproduct(Oldproduct newOldproduct)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tbloldproducten (productnaam, merk, prijs, hoofdcategorie, geslacht, afbeelding, aantalkeerbekeken, subcategorie, beschrijving) VALUES (@productnaam, @merk, @prijs, @hoofdcategorie, @geslacht, @afbeelding, @aantalkeerbekeken, @subcategorie, @beschrijving);";
        mysqlcom.Parameters.Add("@productnaam", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@productnaam"].Value = newOldproduct.productnaam;
        mysqlcom.Parameters.Add("@merk", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@merk"].Value = newOldproduct.merk;
        mysqlcom.Parameters.Add("@prijs", MySqlDbType.VarChar, 75);
        mysqlcom.Parameters["@prijs"].Value = newOldproduct.prijs.ToString().Replace(",", ".");
        mysqlcom.Parameters.Add("@hoofdcategorie", MySqlDbType.VarChar, 100);
        mysqlcom.Parameters["@hoofdcategorie"].Value = newOldproduct.hoofdcategorie;
        mysqlcom.Parameters.Add("@geslacht", MySqlDbType.VarChar, 125);
        mysqlcom.Parameters["@geslacht"].Value = newOldproduct.geslacht;
        mysqlcom.Parameters.Add("@afbeelding", MySqlDbType.VarChar, 150);
        mysqlcom.Parameters["@afbeelding"].Value = newOldproduct.afbeelding;
        mysqlcom.Parameters.Add("@aantalkeerbekeken", MySqlDbType.VarChar, 175);
        mysqlcom.Parameters["@aantalkeerbekeken"].Value = newOldproduct.aantalkeerbekeken;
        mysqlcom.Parameters.Add("@subcategorie", MySqlDbType.VarChar, 200);
        mysqlcom.Parameters["@subcategorie"].Value = newOldproduct.subcategorie;
        mysqlcom.Parameters.Add("@beschrijving", MySqlDbType.VarChar, 225);
        mysqlcom.Parameters["@beschrijving"].Value = newOldproduct.Beschrijving;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        long oldproductID = mysqlcom.LastInsertedId;
        mysqalcon.Close();
        return oldproductID;
    }
    public static List<Oldproduct> readRowsOldproduct(int intRows, string strColumnameToSort)
    {
        List<Oldproduct> Rows = new List<Oldproduct>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "SELECT * FROM tbloldproducten ORDER BY " + strColumnameToSort + " DESC LIMIT 0," + intRows.ToString() + " ;";
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();

        while (reader.Read())
        {
            Oldproduct aOldproduct = new Oldproduct();
            aOldproduct.oldproductID = Convert.ToInt32(reader["oldproductID"]);
            aOldproduct.productnaam = reader["productnaam"].ToString();
            aOldproduct.merk = reader["merk"].ToString();
            aOldproduct.prijs = Convert.ToDecimal(reader["prijs"]);
            aOldproduct.subcategorie = reader["subcategorie"].ToString();
            aOldproduct.geslacht = reader["geslacht"].ToString();
            aOldproduct.afbeelding = reader["afbeelding"].ToString();
            aOldproduct.aantalkeerbekeken = Convert.ToInt32(reader["aantalkeerbekeken"]);
            aOldproduct.hoofdcategorie = reader["hoofdcategorie"].ToString();
            aOldproduct.Beschrijving = reader["beschrijving"].ToString();
            Rows.Add(aOldproduct);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return Rows;
    }
    public static void editOldproduct(string strID, string strVariabele, string strColumn)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tbloldproducten SET " + strColumn + " = @variabele WHERE oldproductID = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    #endregion
    public static List<string> readColumn(string strCommand, string strVariabele)
    {
        List<string> aColumn = new List<string>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = strCommand;
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        while (reader.Read())
        {
            int intAmount = reader.FieldCount;
            if (intAmount != 0)
            {
                for (int intCounter = 0; intCounter < intAmount; intCounter++)
                {
                    try
                    {
                        aColumn.Add(reader.GetString(intCounter));
                    }
                    catch
                    {

                    }
                }
            }
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return aColumn;
    }
    public static void ExecuteCommand(string strCommand)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = strCommand;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static List<Product> csv_getAllProducts(string strSplitValue, string strPath)
    {
        List<string> lines = File.ReadAllLines(strPath).ToList();
        List<Product> x = new List<Product>();
        //Get users from the file
        foreach (var line in lines)
        { //naam - merk - prijs - hoofdcategorie - geslacht - afbeelding - aantalkeerbekeken - subcategorie - beschrijving bool isInt = false;
            
            string[] entries = line.Split(new string[] { strSplitValue }, StringSplitOptions.None); bool isInt = false;
            try
            {
                int inttemp = Convert.ToInt32(entries[6]);
                isInt = true;
            }
            catch
            {
                isInt = false;
            }
            if (isInt)
            {
                Product newProduct = new Product();
                newProduct.productnaam = entries[0];
                newProduct.merk = entries[1];
                newProduct.prijs = Convert.ToDecimal(entries[2].Replace(".", ","));
                newProduct.hoofdcategorie = entries[3];
                newProduct.geslacht = entries[4];
                newProduct.afbeelding = entries[5];
                newProduct.aantalkeerbekeken = Convert.ToInt32(entries[6]);
                newProduct.subcategorie = entries[7];
                newProduct.Beschrijving = entries[8];
                x.Add(newProduct);
            }
            else
            {
                Product newProduct = new Product();
                newProduct.productnaam = string.Empty;
                newProduct.merk = string.Empty;
                newProduct.prijs = 0m;
                newProduct.hoofdcategorie = string.Empty;
                newProduct.geslacht = string.Empty;
                newProduct.afbeelding = entries[5];
                newProduct.aantalkeerbekeken = 0;
                newProduct.subcategorie = string.Empty;
                newProduct.Beschrijving = string.Empty;
                x.Add(newProduct);
            }
        }
        return x;
    }
}