using PokemonIndex.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokemonIndex.Controllers
{
    public class PokedexController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: PokedexViewModel
        
        public ActionResult Index(string id)
        {
            

            if (!String.IsNullOrEmpty(id))
            {
                List<Pokemon> pokemon = db.Pokemons.Where(c => c.Name.Contains(id)).ToList();
                List<Evolution> evolution = new List<Evolution>();
                foreach(var p in pokemon)
                {
                    evolution.Add(db.Evolutions.Where(c => c.EvolveFromId.Equals(p.PokemonId)).FirstOrDefault());
                }
                List<Pokemon> pokemon2 = new List<Pokemon>();
                foreach(var e in evolution)
                {
                    pokemon2.Add(db.Pokemons.Where(c => c.PokemonId.Equals(e.EvolveToId)).FirstOrDefault());
                }

                Pokedex pokedex = new Pokedex
                {
                    Pokemon1 = pokemon,
                    Pokemon2 = pokemon2,
                    Evolutions = evolution
                };
                
               
                return View("Result",pokedex);
            }

            return View();

        }

        public ActionResult Result()
        {
            return RedirectToAction("Index");
        }


    }
}