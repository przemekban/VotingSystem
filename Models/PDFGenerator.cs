﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VotingSystem.DAL;

namespace VotingSystem.Models
{
    public static class PDFGenerator
    {
        public static void Generate(Voting voting)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            string path = $"~/documents/Karta głosowania {voting.Name}.pdf";
            Renderer.RenderHtmlAsPdf(GenerateHtml(voting)).SaveAs(path);
        }

        private static string GenerateHtml(Voting voting)
        {
            var candidates = new VotingContext().Candidates.Where(c => c.VotingId == voting.ID).OrderBy(c => c.Surname).ToList();
            int i = 1;
            string s = 
                $"<h1>Głosowanie: {voting.Name}</h1><br>" +
                $"<h3>Szczegóły:</h3>" +
                $"<h5>Liczba wygrywających: {voting.NumberOfWinners}<br>" +
                $"Liczba uprawnionych do głosowania: {voting.NumberOfVoters}<br>" +
                $"Liczba kandydatów: {candidates.Count }</h5>" +
                $"<br><hr><br>" +
                $"<p> Pamiętaj, aby głos był ważny możesz oddać jedynie ustaloną ilość głosów. Oddanie ilości głosów większej niż {voting.NumberOfWinners} na kilku kandydatów spowoduje, iż głos będzie nieważny!<br> Po oddaniu głosu złóż kartkę na pół.<br>Głosowanie jest tajne! " +
                $"<br><hr><br>" +
                $"<h3>Kandydaci:</h3><br>" +
                $"<table style='width:100%;'><tr><th style='width:33%'>Imię</th><th style='width:33%'>Nazwisko</th><th style='width:33%'>Głos</th><tr>";

            foreach (var item in candidates)
            {
                if(i % 2 == 0)
                    s += $"<tr><td style='text-align:center; padding:10px;'>{item.FirstName}</td><td style='text-align:center; padding:10px;'>{item.Surname}</td><td style=' padding:10px;'><div style='margin-left:42.5%; width:15%; height:30px; border: 1px solid black;'></div></td>";
                else
                    s += $"<tr style='background-color:gray'><td style='text-align:center; padding:10px;'>{item.FirstName}</td><td style='text-align:center; padding:10px;'>{item.Surname}</td><td style=' padding:10px;'><div style='margin-left:42.5%; width:15%; height:30px; border: 1px solid black;'></div></td>";
                i++;
            }

            s += "</table>";

            return s;
        }

        public static void GenerateSummary(Voting voting)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            string path = $"~/documents/Podsumowanie głosowania {voting.Name}.pdf";
            Renderer.RenderHtmlAsPdf(GenerateHtmlSummary(voting)).SaveAs(path);
        }

        private static string GenerateHtmlSummary(Voting voting)
        {
            var candidates = new VotingContext().Candidates.Where(c => c.VotingId == voting.ID).OrderByDescending(c => c.VotesCount).ToList();
            int i = 1;
            string s =
                $"<h1>Głosowanie: {voting.Name}</h1><br>" +
                $"<h3>Szczegóły:</h3>" +
                $"<h5>Liczba wygrywających: {voting.NumberOfWinners}<br>" +
                $"Liczba uprawnionych do głosowania: {voting.NumberOfVoters}<br>" +
                $"Liczba kandydatów: {candidates.Count }</h5>" +
                $"<br><hr><br>" +
                $"<h3>Kandydaci:</h3><br>" +
                $"<table style='width:100%;'><tr><th style='width:33%'>Imię</th><th style='width:33%'>Nazwisko</th><th style='width:33%'>Wynik</th><tr>";

            foreach (var item in candidates)
            {
                if (i % 2 == 0)
                {
                    s += $"<tr><td style='text-align:center; padding:10px;'>{item.FirstName}</td><td style='text-align:center; padding:10px;'>{item.Surname}</td><td style='padding:10px; text-align:center;'>";
                    if (i <= voting.NumberOfWinners)
                    {
                        s += $"<p style='color:green;'>Osoba wygrywająca<br>Liczba głosów {item.VotesCount}</p>";
                    }
                    else
                    {
                        s += $"<p>Osoba przegrywająca<br>Liczba głosów {item.VotesCount}</p>";
                    }
                    s += $"</td>";
                }
                else
                {
                    s += $"<tr style='background-color:gray'><td style='text-align:center; padding:10px;'>{item.FirstName}</td><td style='text-align:center; padding:10px;'>{item.Surname}</td><td style='padding:10px; text-align:center;'>";
                    if(i <= voting.NumberOfWinners)
                    {
                        s += $"<p style='color:#7FFF00;'>Osoba wygrywająca<br>Liczba głosów {item.VotesCount}</p>";
                    }
                    else
                    {
                        s += $"<p>Osoba przegrywająca<br>Liczba głosów {item.VotesCount}</p>";
                    }
                    s += $"</td>";
                }
                i++;
            }

            s += "</table>";

            return s;
        }

        public static void GenerateCodes(Voting voting)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            string path = $"~/documents/Kody głosowania {voting.Name}.pdf";
            Renderer.RenderHtmlAsPdf(GenerateHtmlCodes(voting)).SaveAs(path);
        }

        private static string GenerateHtmlCodes(Voting voting)
        {
            var codes = new VotingContext().Votes.Where(v => v.VotingId == voting.ID).ToList();

            string s = "";
            foreach (var item in codes)
            {
                s += $"<div style='text-align:center;float:left;border-bottom:1px dotted black; border-right:1px dotted black; width:45%; padding: 15px;'><p>Głosowanie:   <b>{voting.Name}</b></p><p>Kod:   <b>{item.Code}</b></p></div>";
            }

            return s;
        }
    }
}
