using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonIndex.Models
{
    public class PokedexViewModel
    {
        public Pokemon  Pokemon { get; set; }
        public List<PokemonType> PokemonTypes { get; set; }
        public Pokemon EvolvesFrom { get; set; }
        public List<Pokemon> EvolvesTo { get; set; }
    }
}