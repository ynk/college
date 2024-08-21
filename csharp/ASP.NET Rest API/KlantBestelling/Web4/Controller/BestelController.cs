using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BusinessLayer.Manager;
using BusinessLayer.Model;
using BusinessLayer.Repositories;
using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using RestAPI.JsonResponse;
using RestAPI.Model;

namespace RestAPI.Controller
{
    [Route("api/klant/{klantId}/bestelling")]

    [ApiController]
    public class BestelController : ControllerBase
    {
        private BestelManager _bm;
        private KlantenManager _km;

        public BestelController(IUnitOfWork uow)
        {
            _bm = new BestelManager(uow);
            _km = new KlantenManager(uow);

        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _bm.VerwijderBestellingId(id);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound(e.Message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] BestellingModel bestelling)
        {
            try
            {
                string klant = HttpContext.GetRouteValue("klantId").ToString().Trim();
                var y = Int32.Parse(klant);

                if (bestelling.KlantId != y)
                {
                    return BadRequest($"Json bestelling id = {bestelling.KlantId} klant url = {y}, Missmatch!");
                }

                var x = _bm.ZoekBestellingOpId(id);
                Product p;
                if (!Enum.TryParse(bestelling.Product, out p))
                {
                    return BadRequest("input is invalid");
                }

                x.setAantal(bestelling.Aantal);
                //x.Aantal = bestelling.Aantal;
                x.setProduct(p);
                _bm.UpdateBestelling(x);
                BestellingJSON b = ConvertToOutput(x);
                return Ok(b);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound(e.Message);
            }

        }


        [HttpGet("{id}")]
        public ActionResult GetBestelling(int id)
        {
            string klant = HttpContext.GetRouteValue("klantId").ToString().Trim();
            var y = Int32.Parse(klant);

            var x = _km.ZoekKlantMetId(y);
            if (x == null)
                return NotFound();
            var b = _bm.ZoekBestellingOpId(id);

            if (b.KlantId == x.Id)
            {
                BestellingJSON data = ConvertToOutput(b);
                return Ok(data);
            }


            return NotFound("Klant id komt niet overeen met de bestelling klant id, check database");


        }

        [HttpGet]
        public ActionResult<List<Bestelling>> GetAlle(int klantId)
        {
            try
            {
                var x = _bm.GetAlleBestellingenVanKlant(klantId);

                return Ok(x);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }


        }

        [HttpPost]

        public ActionResult Post([FromBody] BestellingModel bestelling)
        {
            try
            {
                string klant = HttpContext.GetRouteValue("klantId").ToString().Trim();
                var y = Int32.Parse(klant);

                if (bestelling.KlantId != y)
                {
                    return BadRequest($"Json bestelling id = {bestelling.KlantId} klant url = {y}, Missmatch!");
                }

                var k = _km.ZoekKlantMetId(y);
                if (k == null)
                {
                    return BadRequest("Klant niet gevonden");
                }
                Product p;
                if (!Enum.TryParse(bestelling.Product, out p))
                {
                    return BadRequest("input is invalid");
                }

                var b = new Bestelling(k, p, bestelling.Aantal);
                _bm.VoegBestellingToe(b);
                return CreatedAtAction(nameof(GetBestelling), new {klantId = bestelling.KlantId, id = b.Id}, ConvertToOutput(b));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

        }




        private dynamic ConvertToOutput(Bestelling b)
        {
            
            var data = new BestellingJSON()
            {
                BestelingId = $"http://localhost:50051/api/Klant/{b.KlantId}/Bestelling/{b.Id}",
                KlantId = $"http://localhost:50051/api/Klant/{b.KlantId}",
                Product = b.Product.ToString(),
                Aantal = b.Aantal
            };
            return data;
        }
    }
}