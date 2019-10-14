using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeersAPI.Models;

namespace BeersAPI.Services
{
    public class BeersService : IBeersService
    {
        private static ConcurrentBag<Beer> _beers;

        static BeersService()
        {
            _beers = new ConcurrentBag<Beer>()
            {
                new Beer
                {
                    Id = Guid.NewGuid(),
                    Title = "Heineken 0.00",
                    NonAlcohol = true,
                    Volume = 500
                },
                new Beer
                {
                    Id = Guid.NewGuid(),
                    Title = "Calsberg",
                    NonAlcohol = false,
                    Volume = 300
                },
                new Beer
                {
                    Id = Guid.NewGuid(),
                    Title = "Vilniaus Tamsus",
                    NonAlcohol = false,
                    Volume = 333
                }
            };
        }

        public Task<Beer> Add(Beer beer)
        {
            _beers.Add(beer);
            return Task.FromResult(beer);
        }

        public Task<bool> Delete(Guid id)
        {
            _beers = new ConcurrentBag<Beer>(_beers.Where(x => x.Id != id));
            return Task.FromResult(true);
        }

        public Task<Beer> Get(Guid id)
        {
            return Task.FromResult(_beers.FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<Beer>> GetAll()
        {
            return Task.FromResult<IEnumerable<Beer>>(_beers);
        }

        public Task<bool> Update(Beer beer)
        {
            _beers = new ConcurrentBag<Beer>(_beers.Where(x => x.Id != beer.Id))
            {
                beer
            };
            return Task.FromResult(true);
        }
    }
}
