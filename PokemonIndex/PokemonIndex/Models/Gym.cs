using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PokemonIndex.Models
{
    public class Gym
    {
        [Key]
        public String GymLocation { get; set; }
        public TypeEnum Type { get; set; }

        public string LeaderName { get; set; }


    }

    public class Gymformation
    {
        public List<TrainerPokemon> TrainerPokemons { get; set; }
        public List<Pokemon> Pokemons { get; set; }
        public List<Trainer> Trainers { get; set; }
        public List<PokemonType> PokemonTypes { get; set; }
        public Gym Gym { get; set; }
    }

}