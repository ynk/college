using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Manager;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Routing;
using RestAPI.NewFolder;
using RestAPI.NewFolder.Output;

namespace RestAPI.Controllers
{
    [Route("api/continent/{continentId}/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private ContinentManager _continentManager;
        private CountryManager _countryManager;

        public CountryController(IUnitOfWork uow)
        {
            _continentManager = new ContinentManager(uow);

            _countryManager = new CountryManager(uow);

        }

        [HttpGet]
        [Route("Test")]
        public ActionResult<string> Test()
        {
            Logger.Log("  Test Country ");
            /*http://localhost:1337/api/continent/1/Country/test */
            return "Hello world";
        }


        [HttpGet]
        [Route("")]
        public ActionResult<string> ListAllCountries()
        {
            Logger.Log("  List all Country ");
            int id = Convert.ToInt32(HttpContext.GetRouteValue("continentId").ToString().Trim());

            var continent = _continentManager.FindContinentById(id);

            var list = _countryManager.GetAllByContinent(continent);


            if (list.Count == 0)
            {
                return NotFound("Continent has no countrys");
            }
            var x = CountryListToDTO(list);

            
            


            return Ok(x);

        }





        [HttpGet("{id}")]
        public ActionResult<Country> GetCountryById(int id)
        {
            Logger.Log("  Get Country By Id ");
            try
            {
                int urlid = Convert.ToInt32(HttpContext.GetRouteValue("continentId").ToString().Trim());


                var x = _countryManager.GetCountryById(id);
                if (x == null)
                {
                    return NotFound("Country Id bestaat niet");
                }

                return Ok(CountryDTO(x));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost]
        public ActionResult<Country> CreateCountryPost([FromBody] CountryJson cj)
        {
            Logger.Log("  CreateCountryPost ");
            try
            {
                int id = Convert.ToInt32(HttpContext.GetRouteValue("continentId").ToString().Trim());

                var continent = _continentManager.FindContinentById(id);

                var c = new Country(cj.Name,cj.Population, continent);

                _countryManager.Add(c);


                return CreatedAtAction(nameof(GetCountryById), new { continentId = c.Continent.Id,Id = c.Id }, CountryDTO(c));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Continent> DeleteCountryById(int id)
        {
            Logger.Log("DeleteCountryById");

            try
            {
                int urlid = Convert.ToInt32(HttpContext.GetRouteValue("continentId").ToString().Trim());

                var country = _countryManager.GetCountryById(id);

                if (country == null)
                {
                    return NotFound("Country was not found so nothing got deleted");
                }
                _countryManager.Remove(country);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Continent> UpdateCountryById(int id, [FromBody] CountryJson cj)
        {
            Logger.Log("Update Country By Id");
            try
            {
                var result = _countryManager.GetCountryById(id); /* Dit is de ID van de url, Nu moeten we nog kijken of deze klopt met de body */
                if (result != null)
                {
                    if (cj.Id == 0 || cj.Id == result.Id)
                    { // Als in de body geen ID zit is dit auto 0 dus kunnen we ook de naam updaten
                        result.SetName(cj.Name);
                        result.SetPopulation(cj.Population);
                        _countryManager.Update(result);
                        return (Ok(CountryDTO(result)));
                    }
                    else if (cj.Id != result.Id)
                    {
                        return BadRequest($"Body is = {cj.Id} but expected = {result.Id}");
                    }

      
                }
                else
                {
                    return NotFound("Country niet gevonden");

                }

                return Ok(result);

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        private dynamic CountryListToDTO(List<Country> country)
        {
            List<string> Urls = new List<string>();

            foreach (var x in country)
            {
                Urls.Add($"http://localhost:1337/api/continent/{x.Continent.Id}/Country/{x.Id}");
            }

            return Urls;
        }
        private dynamic CountryDTO( Country country)
        {


            List<string> CityURL = new List<string>();

            foreach (var x in country.Cities)
            {
                CityURL.Add($"http://localhost:1337/api/continent/{country.Continent.Id}/Country/{country.Id}/city/{x.Id}");
            }
            List<string> RiverUrls = new List<string>();

            foreach (var river in country.Rivers)
            {
                RiverUrls.Add($"http://localhost:1337/api/river/{river.Id}");
            }

            var data = new CountryDTO()
            {
                Id = country.Id,
                Name = country.Name,
                Population = country.Population,
                CitiesSum = country.GetPopulationFromCities(),
                Surface = country.GetSurface(),
                Continent = ($"http://localhost:1337/api/continent/{country.Continent.Id}"),
                Cities = CityURL,
                Rivers = RiverUrls
            };


            return data;
        }

    }
}
