using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PokemonIndex.Models
{
    public class TrainerPokemon
    {
        // Unique index to use as single attribute primary key
        public int Id { get; set; }

        //[ForeignKey("Trainer")]
        public int TrainerId { get; set; }
        //[ForeignKey("Pokemon")]
        public int PokemonId { get; set; }
        public int Level { get; set; }

        //public virtual Trainer Trainer { get; set; }
        //public virtual Pokemon Pokemon { get; set; }


    }
}