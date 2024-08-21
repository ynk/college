using System;
using System.Collections.Generic;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities
{
    public class Delivery
    {
        public int Id { get; set; }
        public DateTime DatumOntvangst { get; set; }
        public DateTime DatumLevering { get; set; }
        public List<DeliveryComic> DeliveryLines { get; set; }

        public void AddDeliveryLine(DeliveryComic deliveryComic)
        {
            if (deliveryComic != null)
            {
                deliveryComic.Comic.AddAantal(deliveryComic.Aantal);
                DeliveryLines.Add(deliveryComic);
            }
            else
            {
                throw new DeliveryException("DeliveryComic mag niet null zijn");
            }
        }
    }
}