using PokemonIndex.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PokemonIndex.Controllers
{
    public class PokedexViewModelController : Controller
    {
        //Create DB Instance
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View();
        }

        
        public async Task<ActionResult> _PokedexEntry(int id)
        {

            var pokemonId = id;
            //Create DB CustomerAccount Table Instance
            var pokemonTypes = db.PokemonTypes;
            var pokemons = db.Pokemons;
            var evolutions = db.Evolutions;

            //QUERY: Get all Pokedex info tied to this PokemonId
            Pokemon pokemon = pokemons.Where(p => p.PokemonId == pokemonId).First();

            var types = pokemonTypes.Where(t => (int)(t.PokemonId) == pokemonId);
            List < PokemonType > typesList = await types.ToListAsync();

            var evolvesFromId = evolutions
                .Where(e => e.EvolveToId == pokemonId)
                .Select(e => e.EvolveFromId)
                .FirstOrDefault();

            var evolvesFrom = pokemons
                .Where(p => p.PokemonId == evolvesFromId)
                .FirstOrDefault();

            var evolvesToEvolutions = evolutions
                .Where(e => e.EvolveFromId == pokemonId)
                .Select(e => e.EvolveToId);

            List<int> evolvesToIdList = await evolvesToEvolutions.ToListAsync();

            List<Pokemon> evolvesTo = new List<Pokemon>();

            foreach ( int item in evolvesToIdList)
            {
                evolvesTo.Add(pokemons
                .Where(p => p.PokemonId == item)
                .FirstOrDefault());
            }

            var viewModel = new PokedexViewModel
            {
                Pokemon = pokemon,
                PokemonTypes = typesList,
                EvolvesFrom = evolvesFrom,
                EvolvesTo = evolvesTo
            };
            return View(viewModel);
        }

        public ActionResult IndexResult(int id)
        {
            return RedirectToAction("Index", id);
        }
    }
}