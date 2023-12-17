using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Voorkeur
/// </summary>
public class Voorkeur
{
    public List<string> merk { get; set; }
    public List<string> geslacht { get; set; }
    public List<string> categorie { get; set; }
    public decimal Minimumprijs { get; set; }
    public decimal Maximumprijs { get; set; }
    public Voorkeur ()
    {
        merk = new List<string>();
        geslacht = new List<string>();
        categorie = new List<string>();
        //prijs = new List<string>();
    }
}