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

    public class TrainerView
    {
        public string TrainerName { get; set; }
        public List<PokeInfo> Pokemon { get; set; }

    }
    public class PokeInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public List<string> Types { get; set; }
        public string Evolution { get; set; }
    }

}