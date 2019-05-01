using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PokemonIndex.Models
{
    public class Evolution
    {
        public int Id { get; set; }
        public int EvolveFromId { get; set; }
        public int EvolveToId { get; set; }
    }
}