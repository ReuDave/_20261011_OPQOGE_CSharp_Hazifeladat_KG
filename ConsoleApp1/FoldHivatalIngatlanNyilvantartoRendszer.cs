using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class FoldHivatalIngatlanNyilvantartoRendszer
    {
        private List<Ingatlan> ingatlanok_lista = new List<Ingatlan>();

        // Új ingatlan hozzáadása metódus
        public void UjIngatlan(Ingatlan ujIngatlan)
        {
            if (ujIngatlan == null)
            {
                throw new Exception("HIBA: Az ingatlan nem lehet null!");
            }

            // Ellenőrizzük, hogy van-e már ilyen helyrajzi szám a .Any() segítségével
            if (ingatlanok_lista.Any(i =>
                i.HelyrajziSzam.Equals(ujIngatlan.HelyrajziSzam)))
            {
                throw new Exception(
                    "HIBA: Már létezik ingatlan ezzel a helyrajzi számmal!"
                );
            }
            // ha nem fut hibára, akkor hozzáadja
            ingatlanok_lista.Add(ujIngatlan);
        }

        // A legdrágább ingatlan
        public Ingatlan LegdragabbIngatlan
        {
            get
            {
                // ha egy ingatlant sem talál akkor hibát kezelünk
                if (ingatlanok_lista.Count == 0)
                {
                    throw new Exception("Nincs ingatlan a rendszerben!");
                }
                // máskülönben csökkenő sorrendben ár alapján a top 1-et kimentjük
                return ingatlanok_lista
                    .OrderByDescending(i => i.Ar)
                    .First(); // itt lehet akár .FirstOrDefault()
            }
        }

        // Az adott művelési ágú termőföldek átlagos területe
        public double AtlagosTerulet(string muvelesiAg)
        {
            var foldteruletek = ingatlanok_lista
                .OfType<TermoFold>() // kiválogatjuk a Termőföldeket
                .Where(t => t.MuvelesiAg.Equals(muvelesiAg)) // ahol a paraméterrel egyezik a müvelési ág
                .ToList(); // egy List-ába kimentjük

            // ha egy sincs akkor hibát kezelünk
            if (foldteruletek.Count == 0)
            {
                throw new Exception(
                    "Nincs ilyen művelési ágú termőföld a rendszerben!"
                );
            }
            // ha nincs hiba akkor a terület alapján átlagolunk
            return foldteruletek.Average(t => t.Terulet);
        }

        // Bontandó lakóépületek címeinek metódus
        public List<string> BontandoEpuletekCimei(string fajta)
        {
            return ingatlanok_lista
                .OfType<LakoEpulet>() // lakóépületekre vagyunk kíváncsiak
                .Where(l => 
                    l.Fajta.Equals(fajta) // ahol a lekóépület fajtája egyezik a paraméterrel
                    && l.BontandoE) // és a boontandó értéke true
                .Select(l => l.Cim) // ezen találatokból nekünk csak az adottak címe kell
                .ToList(); // amit egy List-ába adunk vissza
        }
    }
}
