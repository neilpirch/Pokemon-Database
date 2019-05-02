using PokemonIndex.Models;
using System;
using System.Collections.Generic;
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
            var pokemon = from p in db.Pokemons
                          select p;

            if (!String.IsNullOrEmpty(id))
            {
                pokemon = pokemon.Where(s => s.Name.Contains(id));
                return View("Result",pokemon.ToList());
            }

            return View();

        }


    }
}