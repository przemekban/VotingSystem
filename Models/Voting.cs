using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Voting
    {
        public int ID { get; set; }
        [Display(Name = "Nazwa głosowania")]
        public string Name { get; set; }
        [Display(Name = "Liczba wygrywających")]
        public int NumberOfWinners { get; set; }
        [Display(Name = "Liczba uprawnionych do głosowania")]
        public int NumberOfVoters { get; set; }
        [Display(Name = "Aktywne?")]
        public bool Active { get; set; }

        public List<Candidate> Candidates { get; set; }
    }
}