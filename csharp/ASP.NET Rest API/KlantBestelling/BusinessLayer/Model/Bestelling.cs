using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Manager;

namespace BusinessLayer.Model
{
    public class Bestelling
    {
        public Product Product { get; set; }
        public int Id { get; private set; }

        public int Aantal { get; private set; }
        public Bestelling()
        {
        }

        public Bestelling(Klant klant,Product product, int aantal)
        {
            
            setKlantId(klant.Id);
            setProduct(product);
            setAantal(aantal);
            
        }

        public void setProduct(Product product)
        {
            Product = product;
        }
        public void setKlantId(int id)
        {
            KlantId = id;
        }
        public void setAantal(int aantal)
        {
            if (aantal <= 0)
            {
                throw new BestellingException("Aantal mag niet lager of gelijk zijn dan nul");
            }

            Aantal = aantal;
        }


       
        
        
        
        public int KlantId { get; private set; }


    
}
}
