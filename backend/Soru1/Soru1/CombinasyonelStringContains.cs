using System.Collections.Generic;
using System.Linq;

namespace Soru1
{
    public class CombinasyonelStringContains
    {
        /// <summary>
        /// Verilen 2 string değerinin herhangi bir kombinasyonunun bir diğerinde olup olmadığını döner.
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public bool KontrolEt(string string1, string string2)
        {
            //biri diğerini içeriyorsa direkt true dön
            if (string1.Contains(string2) || string2.Contains(string1))
            {
                return true;
            }

            //stringleri char dizilerine böl ve bunlarıda var <char,stringde bulunma sayısı> olacak şekilde dictionarye at
            Dictionary<char, int> string1CharDictionary = new Dictionary<char, int>();
            foreach (var item in string1.ToCharArray().GroupBy(x => x))
            {
                string1CharDictionary.Add(item.Key, item.Count());
            }

            Dictionary<char, int> string2CharDistionary = new Dictionary<char, int>();
            foreach (var item in string2.ToCharArray().GroupBy(x => x))
            {
                string2CharDistionary.Add(item.Key, item.Count());
            }

            //dictionaryleri kontrol et
            return KaynakAranilaninTamaminiIceriyorMu(string1CharDictionary, string2CharDistionary) ||
                   KaynakAranilaninTamaminiIceriyorMu(string2CharDistionary, string1CharDictionary);
        }

        private bool KaynakAranilaninTamaminiIceriyorMu(Dictionary<char, int> kaynakDictionary, Dictionary<char, int> aranilanDictionary)
        {
            foreach (var searchItem in aranilanDictionary)
            {
                //eğer kaynak stringe ait dictionaryde aranılan char değeri yoksa, kaynak string diğer stringin herhangi bir kombinasyonunu içeremez.
                //örn: abab, cba: c karakteri ilk dizide olmadığı için ilk dizi ikinci dizinin herhangi bir kombinasyonunu içeremez.
                if (!kaynakDictionary.ContainsKey(searchItem.Key))
                {
                    return false;
                }

                //eğer kaynak stringe ait dictionaryde aranılan karakter en az aranılan sayıda yoksa, kaynak string diğer stringin herhangi bir kombinasyonunu içeremez.
                //örn: ababc, ccba: c karakteri ilk dizide yalnızca bir tane olduğundan, ilk dizi ikinci dizinin herhangi bir kombinasyonunu içeremez.
                if (kaynakDictionary[searchItem.Key] < searchItem.Value)
                {
                    return false;
                }

                //kaynak stringe ait dictionary aranılan karakteri en az aranılan sayıda içerdiği için bu karakter kombinasyon oluşumunda bir engel teşkil etmez
            }

            //herhangi bir sorun yoksa true dön
            return true;
        }
    }
}