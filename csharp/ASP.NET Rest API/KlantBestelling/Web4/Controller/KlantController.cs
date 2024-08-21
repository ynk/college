using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using BusinessLayer.Manager;
using BusinessLayer.Model;

using BusinessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.JsonResponse;
using RestAPI.Model;

namespace RestAPI.Controller
{
    [Route("api/klant")]
    [ApiController]
    public class KlantController : ControllerBase
    {

        private KlantenManager _km;



        public KlantController(IUnitOfWork uow)
        {
            _km = new KlantenManager(uow);

        }

        //api/Klant/test/

        [HttpGet]
        [Route("Test")]
        public ActionResult<string> Test()
        {
            return "Hello world";
        }

        [HttpGet]
        public List<Klant> Get()
        {
            try
            {
                return (List<Klant>) _km.AlleKlanten();
            }
            catch
            {
                Response.StatusCode = 400;
                return null;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<KlantJSON> GetKlant(int id)
        {
            try
            {
                Klant x = _km.ZoekKlantMetId(id);
                KlantJSON data = ConvertToKlantResponse(x);
                return Ok(data);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
                
            }
            
        }

        [HttpPost]
        public ActionResult<Klant> Post([FromBody] KlantModel klant)
        {
            try
            {
                var k = new Klant(klant.Naam, klant.Adres);
                _km.VoegKlantToe(k);
                return CreatedAtAction(nameof(GetKlant), new {id = k.Id}, ConvertToKlantResponse(k));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _km.VerwijderKlant(id);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }





        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] KlantModel klant)
        {

            try
            {
                
                Klant x = _km.ZoekKlantMetId(id);
                if (klant.KlantId != x.Id)
                    return BadRequest("KlantId in body error");
                x.SetAdress(klant.Adres);
                x.SetNaam(klant.Naam);
                _km.UpdateKlant(x);
                KlantJSON data = ConvertToKlantResponse(x);
                return Ok(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }


        }

        private dynamic ConvertToKlantResponse(Klant klant)
        {
            var data = new KlantJSON
            {
                KlantId = "https://localhost:50051/api/Klant/"+klant.Id,
                Naam = klant.Naam,
                Adres = klant.Adres,
                Bestellingen = klant.Bestellingen.Select(x => $"http://localhost:50051/api/Klant/{klant.Id}/Bestelling/{x.Id}")
            };
            return data;
        }

    }

    }

