using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Execptions;
using BusinessLayer.Manager;
using BusinessLayer.Models;
using RestAPI.NewFolder.Input;
using RestAPI.NewFolder.Output;

namespace RestAPI.Controllers
{
    [Route("api/river")]
    [ApiController]
    public class RiverController : ControllerBase
    {
        private RiverManager _riverManager;
        private CountryManager _countryManager;

        public RiverController(IUnitOfWork uow)
        {
            _riverManager = new RiverManager(uow);
            _countryManager = new CountryManager(uow);

        }

        [HttpGet]
        [Route("Test")]
        public ActionResult<string> Test()
        {
            Logger.Log("River Test");
            /*http://localhost:1337/api/river/test */
            return "Hello world";
        }

        [HttpPost]
        public ActionResult<River> CreateRiverFromPost([FromBody] RiverJson rj)
        {
            Logger.Log("CreateRiverFromPost");
            try
            {
                List<Country> Countries = new List<Country>();
                foreach (var countryid in rj.countryid)
                {
                    var country = _countryManager.GetCountryById(countryid);
                    if (country == null)
                    {
                        throw new CountryException("Country Not Found");
                    }
                    Countries.Add(country);

                }



                var river = new River(rj.name, rj.length, Countries);

                _riverManager.Add(river);


                return CreatedAtAction(nameof(GetRiverById), new { id = river.Id }, RiverDTO(river));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }
        }


        [HttpGet("{id}")]
        public ActionResult<River> GetRiverById(int id)
        {
            Logger.Log("GetRiverById");
            try
            {
                var x = _riverManager.getById(id);


                return Ok(RiverDTO(x));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
        [HttpDelete("{id}")]
        public ActionResult<River> DeleteRiverById(int id)
        {
            try
            {
                var r = _riverManager.getById(id);

                if (r == null)
                {
                    return NotFound("River not found");
                }

                    _riverManager.Delete(r);


                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
        [HttpPut("{id}")]
        public ActionResult<River> UpdateRiverById(int id,[FromBody] RiverJson rj)
        {
            Logger.Log("UpdateRiverById");
            try
            {
                /*Eerst de river opvragen voor we fancy gaan beginnen doen  */
                var result = _riverManager.getById(rj.id);

                if (result == null)
                {
                    return BadRequest($"River id missmatch body: {rj.id} excpected: {result.Id}");
                }
                if (rj.id != id)
                {
                    return BadRequest("Body id not same as result id");
                }

                if (result.Name != rj.name)
                {
                    result.SetName(rj.name);
                }

                if (result.Length != rj.length)
                {
                    result.SetLength(rj.length);
                }

                List<Country> Countries = new List<Country>();
                foreach (var countryid in rj.countryid)
                {
                    var country = _countryManager.GetCountryById(countryid);
                    if (country == null)
                    {
                        throw new CountryException("Country Not Found");
                    }
                    Countries.Add(country);
                }

                _riverManager.Update(result);
                return Ok(RiverDTO(result));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        private dynamic RiverDTO(River river)
        {


            List<string> CountryURLS = new List<string>();

            foreach (var x in river.Countries)
            {
                CountryURLS.Add($"http://localhost:1337/api/continent/{x.Continent.Id}/Country/{x.Id}");
            }


            var data = new RiverDTO()
            {
                id = river.Id,
                Name = river.Name,
                Length = river.Length,
                Countries = CountryURLS

            };


            return data;
        }


    }
}
