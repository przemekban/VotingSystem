using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Vote
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public bool Voted { get; set; } = false;
        public int VotingId { get; set; }

        public Voting Voting { get; set; }
    }
}