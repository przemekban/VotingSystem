using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotingSystem.DAL;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class PdfGenerateController : Controller
    { 
        public ActionResult Create(int? votingId)
        {
            if (votingId == null)
                throw new ArgumentNullException();

            var db = new VotingContext();
            var voting = db.Votings.Find(votingId);

            PDFGenerator.Generate(voting);
            
            return Redirect($"/Votings/Details/{votingId}");
        }
    }
}