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
using Microsoft.Extensions.Logging;
using RestAPI.NewFolder.Input;
using RestAPI.NewFolder.Output;

namespace RestAPI.Controllers
{
       [Route("api/continent/{continentId}/Country/{countryId}/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
   

        private ContinentManager _continentManager;
        private CountryManager _countryManager;
        private CityManager _cityManager;

        public CityController(IUnitOfWork uow)
        {
            _continentManager = new ContinentManager(uow);
            _cityManager = new CityManager(uow);
            _countryManager = new CountryManager(uow);
         
    }


        [HttpGet]
        [Route("Test")]
        public ActionResult<string> Test()
        {
            Logger.Log("Test page");
            /*http://localhost:1337/api/continent/1/Country/1/city/test */
            return "Hello world";
        }

        [HttpPost]
        public ActionResult<Country> CreateCityFromPost([FromBody] CityJson cj)
        {
            Logger.Log("CreateCityFromPost");
            try
            {
                int id = Convert.ToInt32(HttpContext.GetRouteValue("countryId").ToString().Trim());

                var country = _countryManager.GetCountryById(id);

                var c = new City(cj.Name, cj.Population,country,cj.isCapital,cj.Surface);

                _cityManager.Add(c);


                return CreatedAtAction(nameof(GetCityById), new {continentId = c.Country.Continent.Id, countryId = c.Country.Id,id = c.Id}, CityDTO(c));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }
        }


        [HttpGet("")]
        public ActionResult<City> ListAllCities()
        {
            Logger.Log("List All Cities");
            try
            {

                int continentId = Convert.ToInt32(HttpContext.GetRouteValue("continentId").ToString().Trim());
                int countryId = Convert.ToInt32(HttpContext.GetRouteValue("countryId").ToString().Trim());
                var country = _countryManager.GetCountryById(continentId);

                return Ok(CityUrlList(country.Cities,continentId,countryId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<City> GetCityById(int id)
        {
            Logger.Log("Get City By Id");
            try
            {
               
                var x = _cityManager.GetById(id);
                if (x == null)
                {
                    return NotFound("City Id bestaat niet");
                }

               var r = CityDTO(x);
                return Ok(r);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<City> DeleteCityById(int id)
        {
            Logger.Log("DeleteCityById");
            try
            {

                var x = _cityManager.GetById(id);
                if (x == null)
                {
                    return NotFound("City Id bestaat niet");
                }

                _cityManager.Delete(x);
                
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult<City> UpdateCityById(int id, [FromBody] CityJson cj)
        {
            Logger.Log("UpdateCityById");
            try
            {
                var result = _cityManager.GetById(id); /* Dit is de ID van de url, Nu moeten we nog kijken of deze klopt met de body */
                if (result != null)
                {

                    if (result.Id != cj.Id)
                    {
                        return NotFound("City ID komt niet overen met die van de body");
                    }

                    if (result.Name != cj.Name)
                    {
                        result.SetName(cj.Name); // Naam is geupdate
                    }

                    if (result.IsCapital != cj.isCapital)
                    {
                        result.SetCapital(cj.isCapital); // Capital Geupdate

                    }

                    if (result.Population != cj.Population)
                    {
                        result.SetPopulation(cj.Population); // Population is geupdate
                    }

                    if (result.Surface != cj.Surface)
                    {
                        result.SetSurface(cj.Surface);
                    }



                    _cityManager.Update(result);
                }
                else
                {
                    return NotFound("Continent niet gevonden met ID van url");

                }

                return Ok(CityDTO(result));

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        private dynamic CityDTO(City c)
        {



            var data = new CityDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Population = c.Population,
                Surface = c.Surface,
                Continent = $"http://localhost:1337/api/continent/{c.Country.Continent.Id}",
                Country = $"http://localhost:1337/api/continent/{c.Country.Continent.Id}/Country/{c.Country.Id}"
            };

            return data;
        }

        private dynamic CityUrlList(ICollection<City> cities, int continent, int country)
        {
            List<string> urlsList = new List<string>();

            foreach (var city in cities)
            {
                urlsList.Add($"http://localhost:1337/api/continent/{continent}/Country/{country}/city/{city.Id}");
            }


            return urlsList;
        }


    }

}
