using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PokemonIndex.Models;

namespace PokemonIndex.Controllers
{
    public class GymController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Gym
        public ActionResult Index(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                Gym gym = db.Gyms.Where(c => c.GymLocation.ToLower().Contains(id.ToLower())).FirstOrDefault();
                if(gym == null)
                {
                    return (View());
                }
                var trainerList = new List<TrainerView>();
                var trainers = db.Trainers.Where(c => c.GymLocation.Equals(gym.GymLocation)).ToList();
                
                foreach(var trainee in trainers)
                {
                    TrainerView trainerView = new TrainerView();
                    Trainer trainer = db.Trainers.Where(c => c.TrainerID.Equals(trainee.TrainerID)).FirstOrDefault();
                    trainerView.TrainerName = trainer.TrainerName;
                    List<TrainerPokemon> tp = db.TrainerPokemons.Where(c => c.TrainerId.Equals(trainer.TrainerID)).ToList();

                    trainerView.Pokemon = new List<PokeInfo>();
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
                    if (evolution != null)
                    {
                           evol = db.Pokemons.Where(c => c.PokemonId.Equals(evolution.EvolveFromId)).FirstOrDefault().Name;
                    }
                    else
                    {
                           evol = "none";
                    }
                                                trainerView.Pokemon.Add(new PokeInfo { Name = pokemon.Name, Id = pokemon.PokemonId, Level = p.Level, Description = pokemon.PokedexEntry, Types = typeString, Evolution = evol });
                    }
                    trainerList.Add(trainerView);
                }                
                Gymformation result = new Gymformation
                {
                    Trainers = trainerList,
                    Gym = gym,
                };
                return View("Result",result);
            }

            return View();
           
        }

        public ActionResult Result(string id)
        {
            return RedirectToAction("Index", id);
        }
    }
}