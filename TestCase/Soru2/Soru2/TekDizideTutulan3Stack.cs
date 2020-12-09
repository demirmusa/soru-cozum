using System;

namespace Soru2
{
    public class TekDizideTutulan3Stack
    {
        private object[] _dizi = new object[3];
        private int[] _currentIndexes;

        public TekDizideTutulan3Stack()
        {
            //ekside başla, ilk push çağırıldığında stacklere ait next indexler 0, 1, 2 olarak gelecek
            _currentIndexes = new[] {-3, -2, -1};
        }

        public void Push(int stackIndex, object item)
        {
            //3 stack var, index 2 den büyük olamaz
            if (stackIndex > 2)
            {
                throw new IndexOutOfRangeException("İndex 2'den büyük olamaz");
            }

            var nextIndex = GetNextIndex(stackIndex);
            _dizi[nextIndex] = item;
            //şuanki indexi 1 eleman arttır. Mod3 mantığıyla çalıştığı için 3 artacak
            _currentIndexes[stackIndex] += 3;
        }

        private int GetNextIndex(int stackIndex)
        {
            //şuanki indexin üzerine 3 eklenirse bir sonraki index ortaya çıkar
            //örn:
            //Örnek dizi şu şekilde olsun: [s11,s21,s31,s12,s22,s32,...]
            //Şuanki index=1 ve s21 i gösteriyorsa sonraki index 4 olacak ve s22yi gösterecek  
            var nextIndex = _currentIndexes[stackIndex] + 3;
            if (_dizi.Length <= nextIndex)
            {
                //dizi yeterince büyük değilse, diziyi genişlet
                Array.Resize(ref _dizi, _dizi.Length * 2);
            }

            return nextIndex;
        }

        public object Pop(int stackIndex)
        {
            //3 stack var, index 2 den büyük olamaz
            if (stackIndex > 2)
            {
                throw new IndexOutOfRangeException("İndex 2'den büyük olamaz");
            }

            //şuanki indexi al
            var currentIndex = _currentIndexes[stackIndex];
            if (currentIndex < 0)
            {
                throw new InvalidOperationException("Stack Empty");
            }

            //şuanki indexi 1 eleman azalt. Mod3 mantığıyla çalıştığı için 3 azaltılacak
            _currentIndexes[stackIndex] -= 3;

            //elemanı dön
            return _dizi[currentIndex];
        }
    }
}