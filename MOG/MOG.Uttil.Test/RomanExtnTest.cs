using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOG.Utilities.Extension;

namespace MOG.Uttil.Test
{
    [TestClass]
    public class RomanExtnTest
    {
        [TestMethod]
        public void To_Roman_Test()
        {
            var num = 24;
            var roman = num.ToRoman();
            Assert.AreEqual(roman, "XXIV");
        }

        [TestMethod]
        public void To_Arabic_Test()
        {
            var roman = "XLII";
            var num = roman.ToArabic();
            Assert.AreEqual(num, 42);
        }
    }
}
