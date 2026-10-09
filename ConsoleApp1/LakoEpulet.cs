namespace ConsoleApp1
{
    internal class LakoEpulet : Ingatlan
    {
        private string cim;

        public string Cim
        {
            get { return cim; }
            set
            {
                // ha a cím értéke üres vagy csak 1 vagy több szóközzel egyenlő
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("HIBA: A cím nem lehet üres!");
                }
                // máskülön elfogadható az értéke
                cim = value;
            }
        }

        // a fajta és a bontandó-e read-only értékek
        private string fajta;

        public string Fajta
        {
            get { return fajta; }
        }

        private bool bontandoE;

        public bool BontandoE
        {
            get { return bontandoE; }
        }

        // 5 paraméteres konstruktor
        public LakoEpulet(
            string _helyrajziSzam,
            int _ar,
            string _cim,
            string _fajta,
            bool _bontandoE
        ) : base(_helyrajziSzam, _ar) // Ingatlan szülő osztályból megkapja a helyrajzi számot és árat
        {
            Cim = _cim;
            fajta = _fajta;
            bontandoE = _bontandoE;
        }

        // 3 paraméteres konstruktor leírásnak megfelelően
        public LakoEpulet(
            string _helyrajziSzam,
            int _ar,
            string _cim
        ) : this(_helyrajziSzam, _ar, _cim, "családi ház", false)
        {
        }
    }

}