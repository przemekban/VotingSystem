using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using VotingSystem.DAL;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class VotesController : Controller
    {
        private VotingContext db = new VotingContext();
        public ActionResult DetailsS(string code)
        {
            if (code == null)
            {
                ViewBag.Text = "Nie podano kodu";
                return View();
            }

            Vote vote = db.Votes.
                Include(v => v.Voting).
                Include(v => v.Voting.Candidates)
                .Where(v => v.Code.Equals(code)).FirstOrDefault();
            if (vote == null)
            {
                ViewBag.Text = "Nie znaleziono kodu";
                return View();
            }
            if (vote.Voted) {
                ViewBag.Text = "Oddano już głos z tego kodu";
                return View();
            }
            if (!vote.Voting.Active) {
                ViewBag.Text = "Głosowanie nieaktywne";
                return View();
            }
            ViewBag.Code = vote.ID;
            
            return View(vote);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsP(int code, List<string> candidates)
        {
            Vote vote = db.Votes.
                Include(v => v.Voting).
                Include(v => v.Voting.Candidates)
                .Where(v => v.ID == code).FirstOrDefault();
            List<int> candidateID = new List<int>();
            foreach (var item in candidates)
            {
                if (!item.Equals("false"))
                {
                    candidateID.Add(int.Parse(item));
                }
            }
            vote.Voted = true;
            if (candidateID.Count <= vote.Voting.NumberOfWinners) { 
                List<Candidate> candidates1 = vote.Voting.Candidates;
                foreach (var item in candidates1)
                {
                    if (candidateID.Any(c => c == item.ID))
                    {
                        item.VotesCount++;
                    }
                }
                ViewBag.Text = "Oddano głos";
            }else{
                ViewBag.Text = "Oddano głos nieważny";
            }
            db.SaveChanges();
            return View();
        }
    }
}
