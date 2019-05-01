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
}