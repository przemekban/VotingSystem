using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VotingSystem.DAL;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    [Authorize]
    public class VotingsController : Controller
    {
        private VotingContext db = new VotingContext();
        private Random random = new Random();

        // GET: Votings
        public ActionResult Index()
        {
            return View(db.Votings.OrderByDescending(v => v.ID).ToList());
        }

        // GET: Votings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }
            voting.Candidates = db.Candidates.Where(c => c.VotingId == id).ToList();
            ViewBag.NumberOfCandidates = voting.Candidates.Count();
            return View(voting);
        }

        // GET: Votings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Votings/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,NumberOfWinners,NumberOfVoters")] Voting voting)
        {
            if (ModelState.IsValid)
            {
                db.Votings.Add(voting);
                for (int i = 0; i < voting.NumberOfVoters; i++)
                {
                    string code;
                    do
                    {
                        code = "";
                        for (int j = 0; j < 6; j++)
                        {
                            int choice = random.Next(3);
                            switch (choice)
                            {
                                case 0:
                                    code += (char)random.Next(48, 58);
                                    break;
                                case 1:
                                    code += (char)random.Next(65, 91);
                                    break;
                                case 2:
                                    code += (char)random.Next(97, 123);
                                    break;
                                default:
                                    break;
                            }
                        }
                    } while (db.Votes.FirstOrDefault(v => v.Code == code) != null);

                    Vote vote = new Vote() { Voting = voting, Code = code };
                    db.Votes.Add(vote);
                }
                db.SaveChanges();
                return RedirectToAction("Details/" + voting.ID);
            }

            return View(voting);
        }

        // GET: Votings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }
            return View(voting);
        }

        // POST: Votings/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,NumberOfWinners,NumberOfVoters")] Voting voting)
        {
            if (ModelState.IsValid)
            {
                List<Vote> votes = db.Votes.Where(v => v.VotingId == voting.ID).ToList();
                if (votes.Count < voting.NumberOfVoters)
                {
                    int difference = voting.NumberOfVoters - votes.Count;
                    for (int i = 0; i < difference; i++)
                    {
                        string code;
                        do
                        {
                            code = "";
                            for (int j = 0; j < 6; j++)
                            {
                                int choice = random.Next(3);
                                switch (choice)
                                {
                                    case 0:
                                        code += (char)random.Next(48, 58);
                                        break;
                                    case 1:
                                        code += (char)random.Next(65, 91);
                                        break;
                                    case 2:
                                        code += (char)random.Next(97, 123);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        } while (db.Votes.FirstOrDefault(v => v.Code == code) != null);

                        Vote vote = new Vote() { Voting = voting, Code = code };
                        db.Votes.Add(vote);
                    }
                }
                else
                {
                    int difference = votes.Count - voting.NumberOfVoters;
                    for (int i = 0; i < difference; i++)
                    {
                        db.Votes.Remove(votes[votes.Count - 1]);
                        votes.Remove(votes[votes.Count - 1]);
                    }
                }

                db.Entry(voting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/"+voting.ID);
            }
            return View(voting);
        }

        // GET: Votings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }
            return View(voting);
        }

        // POST: Votings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Vote> votes = db.Votes.Where(v => v.VotingId == id).ToList();
            foreach (var vote in votes)
            {
                db.Votes.Remove(vote);
            }
            Voting voting = db.Votings.Find(id);
            db.Votings.Remove(voting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult StartVoting(int id)
        {
            Voting voting = db.Votings.Find(id);
            voting.Active = true;
            db.Entry(voting).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details/" + id);
        }

        public ActionResult StopVoting(int id)
        {
            Voting voting = db.Votings.Find(id);
            voting.Active = false;
            db.Entry(voting).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details/" + id);
        }
    }
}
