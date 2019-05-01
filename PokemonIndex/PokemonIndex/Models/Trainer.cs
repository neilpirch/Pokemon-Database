using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PokemonIndex.Models
{
    public class Trainer
    {
        // Primary Key
        public int TrainerID { get; set; }

        public String TrainerName { get; set; }

        //[ForeignKey("Gym")]
        public String GymLocation { get; set; }

        //public virtual Gym Gym { get; set; }
    }
}