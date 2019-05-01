using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokemonIndex.Models
{
    public class Pokemon
    {
        // Primary Key
        public int PokemonId { get; set; }
        public String Name { get; set; }
        [StringLength(150)]
        public String PokedexEntry { get; set; }
    }
}