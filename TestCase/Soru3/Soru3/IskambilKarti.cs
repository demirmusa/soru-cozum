namespace Soru3
{
    public class IskambilKarti
    {
        public KartTipi KartTipi { get; private set; }
        public KartDegeri KartDegeri { get; private set; }

        public IskambilKarti(KartTipi kartTipi, KartDegeri kartDegeri)
        {
            KartTipi = kartTipi;
            KartDegeri = kartDegeri;
        }
    }
}