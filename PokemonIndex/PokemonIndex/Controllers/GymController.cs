﻿using System;
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
                List<Trainer> trainers = db.Trainers.Where(c => c.GymLocation.Equals(id)).ToList();
                List<TrainerPokemon> tp = new List<TrainerPokemon>();
                List<Pokemon> pokemon = new List<Pokemon>();
                foreach (var t in trainers)
                {
                    tp.Add(db.TrainerPokemons.Where(c => c.TrainerId.Equals(t.TrainerID)).FirstOrDefault());
                }
                foreach (var t in tp)
                {
                    pokemon.Add(db.Pokemons.Where(c => c.PokemonId.Equals(t.PokemonId)).FirstOrDefault());
                }
                Gym gym = db.Gyms.Where(c => c.GymLocation.Equals(id)).First();
                List<PokemonType> pokemontype = new List<PokemonType>();
                foreach (var p in pokemon)
                {
                    pokemontype.Add(db.PokemonTypes.Where(c => c.PokemonId.Equals(p.PokemonId)).FirstOrDefault());
                }

                Gymformation result = new Gymformation
                {
                    Pokemons = pokemon,
                    Trainers = trainers,
                    PokemonTypes = pokemontype,
                    Gym = gym,
                    TrainerPokemons = tp


                };
                return View(result);
            }

            return View();
           
        }
    }
}