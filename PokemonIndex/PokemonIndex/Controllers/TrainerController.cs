using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PokemonIndex.Models;

namespace PokemonIndex.Controllers
{
    public class TrainerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Trainer
        public ActionResult Index(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                TrainerView trainerView = new TrainerView();
                Trainer trainer = db.Trainers.Where(c => c.TrainerName.Contains(id)).FirstOrDefault();
                trainerView.TrainerName = trainer.TrainerName;
                List<TrainerPokemon> tp = db.TrainerPokemons.Where(c => c.TrainerId.Equals(trainer.TrainerID)).ToList();



                foreach (var p in tp)
                {
                    var pokemon = db.Pokemons.Where(c => c.PokemonId.Equals(p.PokemonId)).FirstOrDefault();
                    var poketypes = db.PokemonTypes.Where(c => c.PokemonId.Equals(pokemon.PokemonId)).ToList();
                    var evolution = db.Evolutions.Where(c => c.EvolveFromId.Equals(pokemon.PokemonId)).FirstOrDefault();
                    List<string> typeString = new List<string>();
                    foreach (var t in poketypes)
                    {
                        typeString.Add(t.Type.ToString());
                    }
                    String evol;
                    if (!(evolution == null))
                    {
                        evol = "";
                    }
                    else
                    {
                        evol = db.Pokemons.Where(c => c.PokemonId.Equals(evolution.EvolveToId)).FirstOrDefault().Name;
                    }
                    trainerView.Pokemon = new List<PokeInfo>();
                    trainerView.Pokemon.Add(new PokeInfo { Name = pokemon.Name, Id = pokemon.PokemonId, Level = p.Level, Description = pokemon.PokedexEntry, Types = typeString, Evolution = evol});
                }
                return View("Result", trainerView);


            }

            return View();
        }
        // GET:Trainer/Result
        // Redirects new searches from result page to index method
        public ActionResult Result(string id)
        {
            return RedirectToAction("Index", id);
        }
    }


    public class TrainerView
    {
        public string TrainerName { get; set; }
        public List<PokeInfo> Pokemon { get; set; }

    }
    public class PokeInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public List<string> Types { get; set; }
        public string Evolution { get; set; }
    }
}