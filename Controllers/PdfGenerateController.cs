using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotingSystem.DAL;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class PdfGenerateController : Controller
    { 
        public FileResult Create(int? votingId)
        {
            if (votingId == null)
                throw new ArgumentNullException();

            var db = new VotingContext();
            var voting = db.Votings.Find(votingId);

            PDFGenerator.Generate(voting);
            string ReportURL = Server.MapPath("~/documents/") + $"Karta głosowania {voting.Name}.pdf";

            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }

        public FileResult CreateSummary(int? votingId)
        {
            if (votingId == null)
                throw new ArgumentNullException();

            var db = new VotingContext();
            var voting = db.Votings.Find(votingId);

            PDFGenerator.GenerateSummary(voting);

            string ReportURL = Server.MapPath("~/documents/") + $"Podsumowanie głosowania {voting.Name}.pdf";

            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }
        public FileResult CreateCodes(int? votingId)
        {
            if (votingId == null)
                throw new ArgumentNullException();

            var db = new VotingContext();
            var voting = db.Votings.Find(votingId);

            PDFGenerator.GenerateCodes(voting);

            string ReportURL = Server.MapPath("~/documents/") + $"Kody głosowania {voting.Name}.pdf";

            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }
    }
}