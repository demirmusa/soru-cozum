using System.Collections.Generic;
using System.Text;

namespace Soru3.YirmibirOyunu
{
    /// <summary>
    /// 21 oyununda oyuncunun elinde tuttuğu kağıtları yönetmek için kullanılan sınıf
    /// </summary>
    public class Oyuncu
    {
        public string Ad { get; }

        public bool OyuncuKartAlmayiBirakti { get; private set; }

        public List<IskambilKarti> Kartlar = new List<IskambilKarti>();

        public Oyuncu(string ad)
        {
            Ad = ad;
        }

        public void KartAlmayiBırak()
        {
            OyuncuKartAlmayiBirakti = true;
        }

        public void KartEkle(IskambilKarti iskambilKarti)
        {
            if (OyuncuKartAlmayiBirakti)
            {
                return;
            }

            Kartlar.Add(iskambilKarti);
        }

        public string KartlariListele()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var iskambilKarti in Kartlar)
            {
                sb.Append($"{iskambilKarti.KartTipi} {iskambilKarti.KartDegeri},");
            }

            return sb.ToString();
        }
    }
}