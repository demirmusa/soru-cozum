using Shouldly;
using Soru3.YirmibirOyunu;
using Xunit;

namespace Soru3.Tests
{
    public class Oyuncu_Tests
    {
        [Fact]
        public void KartEkle_Tests()
        {
            var oyuncu = new Oyuncu("Test");
            oyuncu.Kartlar.Count.ShouldBe(0);

            oyuncu.KartEkle(new IskambilKarti(KartTipi.Karo, KartDegeri.As));
            oyuncu.Kartlar.Count.ShouldBe(1);

            oyuncu.KartAlmayiBırak();

            oyuncu.KartEkle(new IskambilKarti(KartTipi.Karo, KartDegeri.Iki));
            oyuncu.Kartlar.Count.ShouldBe(1);
        }
    }
}