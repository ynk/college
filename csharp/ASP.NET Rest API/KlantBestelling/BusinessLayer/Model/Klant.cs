using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Model
{
    public class Klant
    {

        public Klant()
        {
        }

        public int Id { get; private set; }
        public string Naam
        {
            get;
            private set;
        }

        public string Adres
        {
            get;
            private set;
        }

        public void SetNaam(string naam)
        {
            var x = naam.Trim();
            if (x.Length <= 0 || String.IsNullOrWhiteSpace(x))
            {
                throw new KlantException("Naam is leeg");
            }

            if (x == Adres)
            {
                throw new KlantException("Naam is hetzelfde als adres");
            }

            Naam = x;
        }
        public void SetAdress(string adress)
        {
            var x = adress.Trim();
            if (x.Length < 10)
            {
                throw new KlantException("Adres is te kort");
            }
            if (x == Naam)
            {
                throw new KlantException("Adres is hetzelfde als naam");
            }
            Adres = x;
        }

        public List<Bestelling> Bestellingen { get; private set; }

  
        public Klant(int klantId)
        {
            Id = klantId;
        }

        public Klant(string naam, string adres)
        {
            SetNaam(naam);
            SetAdress(adres);
            SetBestelling(new List<Bestelling>());
           
        }

        public Klant(int klantId, string naam, string adres)
        {
            Id = klantId;
            SetNaam(naam);
            SetAdress(adres);
            SetBestelling(new List<Bestelling>());
          
        }

        public void SetBestelling(List<Bestelling> b)
        {
            Bestellingen = b;
        }

        public Klant(int klantId, string naam, string adres, List<Bestelling> bestellingen)
        {
            Id = klantId;
            Naam = naam;
            Adres = adres;
            SetBestelling(bestellingen);
        }

        public IReadOnlyList<Bestelling> GetBestellingen()
        {
            return Bestellingen.AsReadOnly();
        }

        public void VoegBestellingToe(Bestelling b)
        {
            if (!Bestellingen.Contains(b))
            {
                Bestellingen.Add(b);
                
            }
            else
            {
                throw new KlantException("Klant heeft deze bestelling al");

            }
        }
        public void VerwijderBestelling(Bestelling bestelling)
        {
            if (Bestellingen.Contains(bestelling))
            {
                Bestellingen.Remove(bestelling);
            }
            throw new KlantException("KlantService: bestelling does not exist");
        }
    }
}
