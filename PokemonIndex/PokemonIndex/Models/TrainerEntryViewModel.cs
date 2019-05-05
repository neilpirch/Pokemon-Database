using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonIndex.Models
{
    public class TrainerEntryViewModel
    {
        public Trainer Trainer { get; set; }
        public List<PokedexViewModel> PokedexEntries { get; set; }
    }
}