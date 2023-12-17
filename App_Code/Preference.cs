using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Preference
/// </summary>
public class Preference
{
    public string Hoofdcategorie { get; set; }
    public List<string> Merken { get; set; }
    public List<string> Geslachten { get; set; }
    public List<string> Subcategorieen { get; set; }
    public int Prijsvolgorde { get; set; }
    public decimal Minimumprijs { get; set; }
    public decimal Maximumprijs { get; set; }
    public bool minpSetByUser { get; set; }
    public bool maxpSetByUser { get; set; }
    public string QueryString { get; set; }
    public Preference ()
    {
        Merken = new List<string>();
        Geslachten = new List<string>();
        Subcategorieen = new List<string>();
        Minimumprijs = 0.00m;
        Maximumprijs = 9999999.00m;
    }
}