using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soru3.YirmibirOyunu
{
    public class Oyun
    {
        public List<Oyuncu> _oyuncular = new List<Oyuncu>();

        private List<IskambilKarti> _kartlar = new List<IskambilKarti>();

        public Oyun(int oyuncuSayisi)
        {
            if (oyuncuSayisi > 26)
            {
                throw new Exception("21 oyunu maksimum 26 kişiyle oynanabilir");
            }

            if (oyuncuSayisi < 2)
            {
                throw new Exception("21 oyunu en az 2 kişiyle oynanabilir");
            }

            for (int i = 0; i < oyuncuSayisi; i++)
            {
                _oyuncular.Add(new Oyuncu($"Oyuncu{i}"));
            }

            KartlariTanimla();
            BaslangicKartlariniDagit();
        }

        private void KartlariTanimla()
        {
            foreach (var kartTipi in (KartTipi[]) Enum.GetValues(typeof(KartTipi)))
            {
                foreach (var kartDegeri in (KartDegeri[]) Enum.GetValues(typeof(KartDegeri)))
                {
                    _kartlar.Add(new IskambilKarti(kartTipi, kartDegeri));
                }
            }

            var rnd = new Random();
            _kartlar = _kartlar.OrderBy(x => rnd.Next()).ToList();
        }

        private void BaslangicKartlariniDagit()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var oyuncu in _oyuncular)
                {
                    oyuncu.KartEkle(RastgeleKartGetir());
                }
            }
        }

        private IskambilKarti RastgeleKartGetir()
        {
            var rnd = new Random();
            var randomKart = _kartlar[rnd.Next(0, _kartlar.Count)];
            _kartlar.Remove(randomKart);
            return randomKart;
        }

        public bool OyunBittiMi() => _kartlar.Count == 0 || _oyuncular.All(x => x.OyuncuKartAlmayiBirakti);

        public IEnumerable<Oyuncu> AktifOyuncular()
        {
            foreach (var oyuncu in _oyuncular.Where(o => !o.OyuncuKartAlmayiBirakti))
            {
                yield return oyuncu;
            }
        }

        public void KartEkle(Oyuncu oyuncu)
        {
            oyuncu.KartEkle(RastgeleKartGetir());
        }

        public string Sonuclar()
        {
            if (!OyunBittiMi())
            {
                throw new Exception("Oyun devam ediyor!");
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _oyuncular.Count; i++)
            {
                var toplamDeger = ToplamDeger(_oyuncular[i]);
                sb.AppendLine($"{_oyuncular[i].Ad} elindeki kartların toplamı: {toplamDeger}");
                if (toplamDeger > 21)
                {
                    sb.Append($" (Toplam 21'i geçtiği için oyuncu elendi!!!)");
                }
            }

            var birinci = _oyuncular
                .Where(o => ToplamDeger(o) < 22)
                .OrderByDescending(o => ToplamDeger(o))
                .FirstOrDefault();
            if (birinci != null)
            {
                sb.Append("---");
                sb.Append("Kazanan: " + birinci.Ad);
            }
            else
            {
                sb.Append("Kazanan YOK!");
            }

            return sb.ToString();
        }

        public int ToplamDeger(Oyuncu oyuncu)
        {
            int sayisalDeger = 0;
            foreach (var iskambilKarti in oyuncu.Kartlar)
            {
                sayisalDeger += KartinYirmibirOyunuIcinSayisalDegeriniDon(iskambilKarti);
            }

            return sayisalDeger;
        }

        private int KartinYirmibirOyunuIcinSayisalDegeriniDon(IskambilKarti iskambilKarti)
        {
            switch (iskambilKarti.KartDegeri)
            {
                case KartDegeri.As:
                    return 11;
                case KartDegeri.J:
                case KartDegeri.Q:
                case KartDegeri.K:
                    return 10;
                default:
                    return (int) iskambilKarti.KartDegeri;
            }
        }
    }
}