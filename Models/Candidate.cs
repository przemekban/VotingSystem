using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Candidate
    {
        public int ID { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        public int VotingId { get; set; }

        public Voting Voting { get; set; }
        public override string ToString()
        {
            return FirstName+ " " + Surname;
        }
    }
}