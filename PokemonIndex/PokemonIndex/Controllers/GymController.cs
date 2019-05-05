using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public async System.Threading.Tasks.Task<ActionResult> Index(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                Gym gym = db.Gyms.Where(c => c.GymLocation.ToLower().Contains(id.ToLower())).First();
                var pokemonTypes = db.PokemonTypes;
                var evolutions = db.Evolutions;
                var pokemons = db.Pokemons;
                List<Trainer> trainers = db.Trainers.Where(c => c.GymLocation.Equals(gym.GymLocation)).ToList();
                List<TrainerEntryViewModel> trainerEntries = new List<TrainerEntryViewModel>();
                foreach (var t in trainers)
                {
                    List<TrainerPokemon> tp = new List<TrainerPokemon>();
                    List<Pokemon> pokemon = new List<Pokemon>();
                    List<PokedexViewModel> indexList = new List<PokedexViewModel>();

                    // new code
                    var tpTemp = db.TrainerPokemons.Where(c => c.TrainerId.Equals(t.TrainerID));

                    foreach (var p in tpTemp)
                    {
                        tp.Add(p);
                    }
                    
                    foreach (var i in tp)
                    {
                        pokemon.Add(db.Pokemons.Where(c => c.PokemonId.Equals(i.PokemonId)).FirstOrDefault());
                    }
                    foreach (Pokemon item in pokemon)
                    {
                        int pokemonId = item.PokemonId;
                        var types = pokemonTypes.Where(y => (int)(y.PokemonId) == pokemonId);
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
                    //end new code
                    
                    var entry = new TrainerEntryViewModel
                    {
                        Trainer = t,
                        PokedexEntries = indexList
                    };
                    trainerEntries.Add(entry);
                }
                
                Gymformation result = new Gymformation
                {
                    Gym = gym,
                    TrainerEntries = trainerEntries,
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