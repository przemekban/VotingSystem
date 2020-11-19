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
            string ReportURL = Server.MapPath("~") + $"Karta głosowania {voting.Name}.pdf";

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

            string ReportURL = Server.MapPath("~") + $"Podsumowanie głosowania {voting.Name}.pdf";

            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }

        public FileResult GetReport(string votingName, int id)
        {
            string ReportURL;
            if(id == 0)
            {
                ReportURL = $"~/Podsumowanie głosowania {votingName}.pdf";
            }
            else
            {
                ReportURL = $"~/Karta głosowania {votingName}.pdf";
            }
            
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }
    }
}