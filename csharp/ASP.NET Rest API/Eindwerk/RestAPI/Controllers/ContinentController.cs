using System.Collections.Generic;
using BusinessLayer.Execptions;
using RestAPI.NewFolder.Output;

namespace RestAPI.Controllers
{
    using BusinessLayer;
    using BusinessLayer.Manager;
    using BusinessLayer.Models;
    using Microsoft.AspNetCore.Mvc;
    using RestAPI.NewFolder;
    using System;

    /// <summary>
    /// Defines the <see cref="ContinentController" />.
    /// </summary>
    [Route("api/continent")]
    [ApiController]
    public class ContinentController : ControllerBase
    {
        /// <summary>
        /// Defines the _continentManager.
        /// </summary>
        private ContinentManager _continentManager;

  
       
        



        /// <summary>
        /// Initializes a new instance of the <see cref="ContinentController"/> class.
        /// </summary>
        /// <param name="uow">The uow<see cref="IUnitOfWork"/>.</param>
        public ContinentController(IUnitOfWork uow)
        {
            _continentManager = new ContinentManager(uow);

        

        }

        /// <summary>
        /// The Test.
        /// </summary>
        /// <returns>The <see cref="ActionResult{string}"/>.</returns>
        [HttpGet]
        [Route("Test")]
        public ActionResult<string> Test()
        {
            Logger.Log("Test Continent");
            return "Hello world";
        }

        /// <summary>
        /// The CreateContinentPost.
        /// </summary>
        /// <param name="cj">The cj<see cref="ContinentJson"/>.</param>
        /// <returns>The <see cref="ActionResult{Continent}"/>.</returns>
        [HttpPost]
        public ActionResult<Continent> CreateContinentPost([FromBody] ContinentJson cj)
        {
            Logger.Log(" CreateContinentPost");
            try
            {
                var c = new Continent(cj.Name);
                _continentManager.AddContinent(c);

                return CreatedAtAction(nameof(GetContinentById), new { id = c.Id }, ContinentToDto(c));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }
        }

        /// <summary>
        /// The GetContinentById.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="ActionResult{Continent}"/>.</returns>
        [HttpGet("{id}")]
        public ActionResult<Continent> GetContinentById(int id)
        {
            Logger.Log(" Get Continent by id");
            try
            {
                var x = _continentManager.FindContinentById(id);
                if (x == null)
                {
                    return NotFound("Continent Id bestaat niet");
                }
                return Ok(ContinentToDto(x));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// The DeleteContinentById.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="ActionResult{Continent}"/>.</returns>
        [HttpDelete("{id}")]
        public ActionResult<Continent> DeleteContinentById(int id)
        {
            Logger.Log(" Get Continent by id");
            try
            {
                var result = _continentManager.FindContinentById(id);
                if (result == null)
                {
                    throw new ContinentException($"Continent not found with id: {id}");
                }
                _continentManager.Remove(result);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// The UpdateContinentById.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="cj">The cj<see cref="ContinentJson"/>.</param>
        /// <returns>The <see cref="ActionResult{Continent}"/>.</returns>
        [HttpPut("{id}")]
        public ActionResult<Continent> UpdateContinentById(int id, [FromBody] ContinentJson cj)
        {
            Logger.Log("  UpdateContinentById ");
            try
            {
                var result = _continentManager.FindContinentById(id); /* Dit is de ID van de url, Nu moeten we nog kijken of deze klopt met de body */
                if (result != null)
                {

                 
                    if (result.Id != cj.Id)
                    {
                        return NotFound("Continent ID komt niet overen met die van de body");
                    }

                    result.SetName(cj.Name); //Ik laat het alleen toe om de naam te veranderen, de rest moet gedaan worden door andere endpoints

                    _continentManager.Update(result);
                }
                else
                {
                    return NotFound("Continent niet gevonden met ID van url");

                }

                return Ok(result);

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        private dynamic ContinentToDto(Continent continent)
        {
   
            List<string> Urls = new List<string>();


            foreach (var Country in continent.Countries)
            {
                Urls.Add($"http://localhost:1337/api/continent/1/Country/{Country.Id}");
            }

            var response = new ContinentDTO()
            {
                Id = continent.Id,
                Name = continent.Name,
                
                Population = continent.GetPopulation(),
                Surface = continent.GetSurface(),
                Countries = Urls
            };
            return response;
        }

    }
}
