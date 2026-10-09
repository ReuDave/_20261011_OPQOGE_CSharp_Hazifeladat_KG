using System;
using System.IO; // StreamReader miatt van meghívva
using System.Collections.Generic; // a List<> miatt van meghívva
using System.Linq; // LINQ
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // Reucov Dávid OPQOGE
    // https://github.com/ReuDave/_20261011_OPQOGE_CSharp_Hazifeladat_KG.git
    internal class Program
    {
        // vonaton idefelé megcsináltam a házit Kovásznai Gergőnek
        static void Main(string[] args)
        {
            // nyilvántartó rendszer konténer osztály inicializálása (jó rövid neve van)
            FoldHivatalIngatlanNyilvantartoRendszer FHINYR = new FoldHivatalIngatlanNyilvantartoRendszer();
            // a real_estate.txt a bin/Debug mappában van!
            StreamReader sr = new StreamReader("real_estate.txt", true);
            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Split(";");
                // használhattam volna if elágazásokat is, de a switch mellett döntöttem mert egész fix
                // számok, hogy hány darab adatból állnak össze a sorok
                // ingatlan: 2db pl.: 77463/A/9;6700000
                // termőföld: 4db pl.: 71001/G/21;39900000;erdo;11.42
                // lakó terület: 5db pl.: 99120 / 5/B/17;19900000;Vércsorog, Kossuth u. 18.;csaladi_haz;false
                // minden fajta ingatlan helyrajzi számmal és árral kezdődik, így őket nem kell egy case-be tenni
                string helyrajziszam = adatok[0];
                int ar = int.Parse(adatok[1]);
                switch (adatok.Length)
                {
                    case 2:
                        // ez egy sima ingatlan
                        Ingatlan ingatlan = new Ingatlan(helyrajziszam, ar);
                        // hozzáadom ingatlanként a nyilvántartáshoz
                        FHINYR.UjIngatlan(ingatlan);
                        break;
                    case 4:
                        // ez egy termőföld
                        // kiszedem a 3. és 4. adattagokat azokból a sorokból ahol van
                        // pl.: 71001/G/21;39900000;erdo;11.42
                        string muvelesiAg = adatok[2];
                        double terulet = double.Parse(
                             adatok[3].Replace('.', ',')); // át kellett alakítanom a 6.2-t -> 6,2-re!

                        // ide tettem minden helyrajzi szám elé egy nullát mert máskülönben jogosan hibát dob
                        TermoFold termoFold = new TermoFold(
                            "0" + helyrajziszam, ar, muvelesiAg, terulet
                        );
                        // hozzáadom a termőföldeket
                        FHINYR.UjIngatlan(termoFold);
                        break;
                    case 5:
                        // a lakóépületnek a 3. és 4. adattagja eltér a termőföldétől
                        string cim = adatok[2];
                        string fajta = adatok[3];
                        // hozzáadom az 5. adattagot is ami true/false
                        bool bontandoE = bool.Parse(adatok[4]);
                        LakoEpulet lakoEpulet = new LakoEpulet(helyrajziszam, ar, cim, fajta, bontandoE);
                        // hozzáadom lakóépületként
                        FHINYR.UjIngatlan(lakoEpulet);
                        break;
                }
            }
            // ki íratom a különböző feladatok értékeit
            Console.WriteLine("1. feladat:");
            Console.WriteLine($"A legdrágább ingatlan helyrajzi száma: {FHINYR.LegdragabbIngatlan.HelyrajziSzam}");
            Console.WriteLine($"A legdrágább ingatlan ára: {FHINYR.LegdragabbIngatlan.Ar} Ft");
            Console.WriteLine("\n2. feladat:");
            // ugyan a feladat leírás szőlő-t írt, de arra hibára fut jogosan mert nincsen egy sem
            // gyümölcsösre meg 1 darab van (16473/7/C/11;14600000;gyumolcsos;6.2)
            // 6.2 osztva 1-gyel = 6.2 
            Console.WriteLine($"A gyümölcsös termőföldek hektár területének átlaga: {FHINYR.AtlagosTerulet("gyumolcsos")}");
            // a társasház mint példa szintén nincsen a fájlban, így maradtam a csaladi_haz értéknél
            Console.WriteLine("\n3. feladat: Bontandó családi ház(ak) címe(i): ");
            foreach (var epulet in FHINYR.BontandoEpuletekCimei("csaladi_haz"))
            {
                Console.WriteLine(epulet); // kiírja a 2 címet amit talált!
            }
            // kész :)
            // remélem nem volt túl sok a komment, a gondolat menetemet írtam le!
            // Szép napot kívánok a tanár úrnak!!!
        }
    }
}