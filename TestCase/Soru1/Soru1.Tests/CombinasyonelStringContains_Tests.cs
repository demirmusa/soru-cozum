using Shouldly;
using Xunit;

namespace Soru1.Tests
{
    public class CombinasyonelStringContains_Tests
    {
        [Theory]
        [InlineData("baba", "abab", true)]
        [InlineData("baba", "abc", false)]
        [InlineData("lds", "loodos", true)]
        public void KontrolEt_Tests(string string1, string string2, bool sonuc)
        {
            var combinasyonelStringContains = new CombinasyonelStringContains();
            combinasyonelStringContains.KontrolEt(string1, string2).ShouldBe(sonuc);
        }
    }
}