using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Klant
/// </summary>
public class Klant
{
    public string klantID { get; set; }
    public string voornaam { get; set; }
    public string achternaam { get; set; }
    public string mail { get; set; }
    public string telefoonnummer { get; set; }
    public string wachtwoord { get; set; }
    public string bevestigd { get; set; }
    public bool admin { get; set; }
}