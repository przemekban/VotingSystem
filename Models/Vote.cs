using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Vote
    {
        public int ID { get; set; }
        [Display(Name = "Kod")]
        public string Code { get; set; }
        public bool Voted { get; set; } = false;
        public int VotingId { get; set; }

        public Voting Voting { get; set; }
    }
}