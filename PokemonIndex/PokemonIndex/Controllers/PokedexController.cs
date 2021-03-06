﻿using PokemonIndex.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PokemonIndex.Controllers
{
    public class PokedexController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: PokedexViewModel
        
        public async Task<ActionResult> Index(string id)
        {
            var searchResults = from p in db.Pokemons
                          select p;
            var pokemonId = 1;
            var pokemonTypes = db.PokemonTypes;
            var pokemons = db.Pokemons;
            var evolutions = db.Evolutions;
            List<Pokemon> searchResultList = new List<Pokemon>();
            List<PokedexViewModel> indexList = new List<PokedexViewModel>();

            if (!String.IsNullOrEmpty(id))
            {
                foreach(String str in Enum.GetNames(typeof(TypeEnum)))
                {
                    if (id == str)
                    {
                        var typeSearchResults = from p in db.Pokemons
                                            select p;
                        TypeEnum typeEnum = (TypeEnum)Enum.Parse(typeof(TypeEnum), id, true);

                        {
                            var typeSearchIds = pokemonTypes
                                .Where(t => t.Type == typeEnum)
                                .Select(t => t.PokemonId);
                            List<int> pokemonIdList = await typeSearchIds.ToListAsync();

                            foreach (int item in pokemonIdList)
                            {
                                searchResults = pokemons.Where(t => t.PokemonId == item);
                                var tempList = await searchResults.ToListAsync();
                                foreach (Pokemon pokemon in tempList)
                                {
                                    searchResultList.Add(pokemon);
                                }
                            }

                            foreach (Pokemon item in searchResultList)
                            {
                                pokemonId = item.PokemonId;
                                var types = pokemonTypes.Where(t => (int)(t.PokemonId) == pokemonId);
                                List<PokemonType> typesList = await types.ToListAsync();

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

                                foreach (int evo in evolvesToIdList)
                                {
                                    evolvesTo.Add(pokemons
                                    .Where(p => p.PokemonId == evo)
                                    .FirstOrDefault());
                                }

                                var viewModel = new PokedexViewModel
                                {
                                    Pokemon = item,
                                    PokemonTypes = typesList,
                                    EvolvesFrom = evolvesFrom,
                                    EvolvesTo = evolvesTo
                                };
                                indexList.Add(viewModel);
                            }
                        }
                        return View("Result", indexList);
                    }
                }
                searchResults = pokemons.Where(s => s.Name.Contains(id));
                searchResultList = await searchResults.ToListAsync();
                foreach(Pokemon item in searchResultList)
                {
                    pokemonId = item.PokemonId;
                    var types = pokemonTypes.Where(t => (int)(t.PokemonId) == pokemonId);
                    List<PokemonType> typesList = await types.ToListAsync();

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

                    foreach (int evo in evolvesToIdList)
                    {
                        evolvesTo.Add(pokemons
                        .Where(p => p.PokemonId == evo)
                        .FirstOrDefault());
                    }

                    var viewModel = new PokedexViewModel
                    {
                        Pokemon = item,
                        PokemonTypes = typesList,
                        EvolvesFrom = evolvesFrom,
                        EvolvesTo = evolvesTo
                    };
                    indexList.Add(viewModel);
                }
                return View("Result", indexList);
            }

            return View();
           
        }

        public ActionResult Result(string id)
        {
            return RedirectToAction("Index", id);
        }

        [Route("ResultByType")] // Combines to define the route template "Pokedex/ResultByType"
        public async Task<ActionResult> ResultByType(int pokemonType)
        {
            var searchResults = from p in db.Pokemons
                                select p;
            var pokemonId = 1;
            var pokemonTypes = db.PokemonTypes;
            var pokemons = db.Pokemons;
            var evolutions = db.Evolutions;
            TypeEnum typeEnum = (TypeEnum)pokemonType;


            List<PokedexViewModel> indexList = new List<PokedexViewModel>();

            {
                var typeSearchResults = pokemonTypes
                    .Where(t => t.Type == typeEnum)
                    .Select(t => t.PokemonId);
                List<int> pokemonIdList = await typeSearchResults.ToListAsync();

                List<Pokemon> searchResultList = new List<Pokemon>();

                foreach (int item in pokemonIdList)
                {
                    searchResults = pokemons.Where(t => t.PokemonId == item);
                    var tempList = await searchResults.ToListAsync();
                    foreach(Pokemon pokemon in tempList)
                    {
                        searchResultList.Add(pokemon);
                    }
                }

                foreach (Pokemon item in searchResultList)
                {
                    pokemonId = item.PokemonId;
                    var types = pokemonTypes.Where(t => (int)(t.PokemonId) == pokemonId);
                    List<PokemonType> typesList = await types.ToListAsync();

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

                    foreach (int evo in evolvesToIdList)
                    {
                        evolvesTo.Add(pokemons
                        .Where(p => p.PokemonId == evo)
                        .FirstOrDefault());
                    }

                    var viewModel = new PokedexViewModel
                    {
                        Pokemon = item,
                        PokemonTypes = typesList,
                        EvolvesFrom = evolvesFrom,
                        EvolvesTo = evolvesTo
                    };
                    indexList.Add(viewModel);
                }
            }
            return View("Result", indexList);
        }
    }
}