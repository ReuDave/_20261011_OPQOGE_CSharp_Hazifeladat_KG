namespace ConsoleApp1
{

    internal class TermoFold : Ingatlan
    {
        private string muvelesiAg;
        private double terulet;
        // a művelési ág és a terület readonly értékek
        public string MuvelesiAg
        {
            get { return muvelesiAg; }
        }

        public double Terulet
        {
            get { return terulet; }
        }

        // 4 paraméteres konstruktor
        public TermoFold(
            string _helyrajziSzam,
            int _hektaronkentiAr,
            string _muvelesiAg,
            double _terulet
        ) : base(_helyrajziSzam, _hektaronkentiAr)
        {
            // ha nem 0-val kezdődik a helyrajzi szám
            if (!_helyrajziSzam.StartsWith("0"))
            {
                throw new Exception(
                    "HIBA: A helyrajzi számnak 0-val kell kezdődnie!"
                );
            }

            if (_terulet <= 0)
            {
                throw new Exception(
                    "HIBA: A területnek pozitívnak kell lennie!"
                );
            }

            muvelesiAg = _muvelesiAg;
            terulet = _terulet;
        }

        // Hektáronkénti ár növelése
        public void Dragit(int osszeg)
        {
            // ha a hozzáadott összeg 0 vagy annál kisebb, az olcsósítás volna
            if (osszeg <= 0)
            {
                throw new Exception(
                    "HIBA: Csak drágítani lehet!"
                );
            }

            base.Dragitas(osszeg);
        }

        // A teljes terület ára metódus
        public double TeljesAr()
        {
            return Ar * terulet;
        }
    }

}