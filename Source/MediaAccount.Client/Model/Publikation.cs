﻿namespace Krowiorsch.MediaAccount.Model
{
    public class Publikation
    {
        public string PublikationId { get; set; }
        public string Publikationsname { get; set; }
        public string TvSenderName { get; set; }
        public string PublikationsLand { get; set; }
        public int Reichweite { get; set; }
        public long? Visits { get; set; }
        public long? PageImpressions { get; set; }
        public int VerkaufteAuflage { get; set; }
        public int VerbreiteteAuflage { get; set; }
        public int GedruckteAuflage { get; set; }
        public string Bundesland { get; set; }
        public string Nielsengebiet { get; set; }
        public object WerbepreisProSekunde { get; set; }
        public object IvwNummer { get; set; }
        public object IvwNameOnline { get; set; }
        public object ZimpelNr { get; set; }
        public double WerbepreisC1 { get; set; }
        public double WerbepreisC2 { get; set; }
        public double WerbepreisC3 { get; set; }
        public double WerbepreisC4 { get; set; }
        public decimal? Bannerpreis { get; set; }
        public decimal? BannerpreisBasis { get; set; }
        
        public string SendungsName { get; set; }
        public decimal? Sendungslaenge { get; set; }
        public string Ausgabennummer { get; set; }
        public string Erscheinungszyklus { get; set; }
        public string ErscheinungszyklusEnglisch { get; set; }
        public int Satzhoehe { get; set; }
        public int Satzbreite { get; set; }
        public string Sprache { get; set; }
        public string LandNameEnglisch { get; set; }
        public Verlag Verlag { get; set; }
        public Redaktion Redaktion { get; set; }
    }
}