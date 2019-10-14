using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeersAPI.DTOs;
using BeersAPI.Models;
using BeersAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeersService _beersService;

        public BeersController(IBeersService beersService)
        {
            _beersService = beersService;
        }

        // GET: api/Beers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var beers = await _beersService.GetAll();

            if (beers == null)
            {
                return NotFound();
            }

            var data = beers.Select(item => new BeerDto
            {
                BeerId = item.Id,
                Title = item.Title,
                Volume = item.Volume,
                NonAlcohol = item.NonAlcohol,
                DrankQuantity = item.DrankQuantity
            });

            return new OkObjectResult(data);
        }

        // GET: api/Beers/5
        [HttpGet("{beerId}", Name = "Get")]
        public async Task<IActionResult> Get(string beerId)
        {
            if (Guid.TryParse(beerId, out Guid id))
            {
                var beer = await _beersService.Get(id);

                if (beer == null)
                {
                    return NotFound();
                }

                var data = new BeerDto
                {
                    BeerId = beer.Id,
                    Title = beer.Title,
                    Volume = beer.Volume,
                    NonAlcohol = beer.NonAlcohol,
                    DrankQuantity = beer.DrankQuantity
                };

                return new OkObjectResult(data);
            }

            return NotFound();
        }

        // POST: api/Beers
        [HttpPost]
        public IActionResult Post([FromBody] CreateBeerDto newBeer)
        {
            if (newBeer == null)
            {
                return BadRequest();
            }

            var beer = new Beer
            {
                Id = Guid.NewGuid(),
                Title = newBeer.Title,
                NonAlcohol = newBeer.NonAlcohol,
                Volume = newBeer.Volume,
                DrankQuantity = 0
            };

            _beersService.Add(beer);

            return new OkObjectResult(beer);
        }

        // PUT: api/Beers/5
        [HttpPut("{beerId}")]
        public async Task<IActionResult> Put(string beerId, [FromBody] UpdateBeerDto updateBeer)
        {
            if (Guid.TryParse(beerId, out Guid id))
            {
                var beer = await _beersService.Get(id);

                if (beer == null)
                {
                    return NotFound();
                }
                else
                {
                    beer.Title = updateBeer.Title;
                    beer.NonAlcohol = updateBeer.NonAlcohol;
                    beer.Volume = updateBeer.Volume;
                    beer.DrankQuantity = updateBeer.DrankQuantity;

                    var increased = await _beersService.Update(beer);

                    return new OkObjectResult(increased);
                }
            }

            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{beerId}")]
        public async Task<IActionResult> Delete(string beerId)
        {
            if (Guid.TryParse(beerId, out Guid id))
            {
                var beer = await _beersService.Get(id);

                if (beer == null)
                {
                    return NotFound();
                }

                else
                {
                    var removed = await _beersService.Delete(id);
                    return new OkObjectResult(removed);
                }
            }

            return NotFound();
        }

        [HttpPut("{beerId}/increase")]
        public async Task<IActionResult> Put(string beerId)
        {
            if (Guid.TryParse(beerId, out Guid id))
            {
                var beer = await _beersService.Get(id);

                if (beer == null)
                {
                    return NotFound();
                }

                else
                {
                    beer.DrankQuantity++;
                    var increased = await _beersService.Update(beer);

                    return new OkObjectResult(increased);
                }
            }

            return NotFound();
        }
    }
}
