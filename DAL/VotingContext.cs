using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VotingSystem.Models;

namespace VotingSystem.DAL
{
    public class VotingContext : DbContext
    {
        public VotingContext()
            : base("DefaultConnection")
        { }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Voting> Votings { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}