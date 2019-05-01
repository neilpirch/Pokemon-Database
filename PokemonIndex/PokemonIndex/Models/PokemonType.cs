using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PokemonIndex.Models
{
    public enum TypeEnum
    {
        Bug,
        Dragon,
        Electric,
        Fighting,
        Fire,
        Flying,
        Ghost,
        Grass,
        Ground,
        Ice,
        Normal,
        Poison,
        Psychic,
        Rock,
        Water
    }
    public class PokemonType
    {
        // Unique index to use as single attribute primary key
        public int Id { get; set; }

        //[ForeignKey("Pokemon")]
        public int PokemonId { get; set; }
        public TypeEnum Type { get; set; }

        //public virtual Pokemon Pokemon { get; set; }

    }
}