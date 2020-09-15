using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotingSystem.DAL;

namespace VotingSystem.Controllers
{
    public class HomeController : Controller
    {
        private VotingContext db = new VotingContext();

        public ActionResult Index()
        {
            var voting = db.Votings.OrderByDescending(v => v.ID).FirstOrDefault();
            ViewBag.LastVotingId = voting == null ? -1 : voting.ID ;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}