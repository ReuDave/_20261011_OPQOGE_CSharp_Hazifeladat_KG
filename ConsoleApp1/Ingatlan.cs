using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // a szülő osztály ahol minden kezdődik
    internal class Ingatlan
    {
        private string helyrajziSzam;

        public string HelyrajziSzam
        {
            get
            {
                if (helyrajziSzam.Length < 3)
                {
                    throw new Exception("HIBA: nem lehet 3 karakternél rövidebb a helyrajzi szám!");

                }
                // szorgalmi:
                foreach (char karakter in helyrajziSzam)
                {
                    // a IsLetterOrDigit() megnézi hogy az adott karakter 0-9 vagy az ábécé valamely betűje
                    // és hogy / perjel-e, de ezek negálva vannak ! tehát itt a hibás eseteket kapjuk el
                    if (!char.IsLetterOrDigit(karakter) && karakter != '/')
                    {
                        throw new Exception("HIBA: Csak betűk, számok és perjel (/) állhat a helyrajzi számban!");
                    }
                }
                return helyrajziSzam;
            }

        }

        private int ar;

        public int Ar
        {
            get { return ar; }
        }
        // 1.ső konstruktor
        public Ingatlan(string _helyrajzi_szam, int _ar)
        {
            if (_ar % 100000 != 0 || _ar <= 0)
            {
                throw new Exception("HIBA: Az árnak 100 ezerrel oszthatónak kell lennie és egész pozitív értéknek!");
            }
            helyrajziSzam = _helyrajzi_szam;
            ar = _ar;
        }

        //2.dik konstruktor, 1 milliárd árral
        public Ingatlan(string _helyrajzi_szam) : this(_helyrajzi_szam, 1000000000)
        {

        }

        public void Dragitas(int noveles)
        {
            // ha az ár növelés 0-nál nagyobb akkor számít drágításnak
            if (noveles >= 0)
            {
                ar += noveles;
            }
            else
            {
                throw new Exception("HIBA: Nem lehet egy ingatlan olcsóbb!");
            }
        }

    }

}
