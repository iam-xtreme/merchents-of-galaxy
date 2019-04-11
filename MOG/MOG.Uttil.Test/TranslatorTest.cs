using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOG.Utilities.Extension;
using NSubstitute;
using System.Collections.Generic;

namespace MOG.Uttil.Test
{
    [TestClass]
    public class TranslatorTest
    {
        Entities.Contracts.IValidater validater;
        Entities.Contracts.IGalacticTransaction transaction;
        Utilities.Translator translator;
        [TestInitialize]
        public void Initialize()
        {
            transaction = Substitute.For<Entities.Contracts.IGalacticTransaction>();
            transaction.Comodities.Returns(new List<string>{ "Gold", "Silver", "Iron" });

            validater = Substitute.For<Entities.Contracts.IValidater>();
            validater.CreditString = "Credits";

            translator = new Utilities.Translator(transaction, validater);
        }
        [TestMethod]
        public void Set_Unit_Command_Text()
        {
            var text = "glob is I";
            validater.ValidateText(text).Returns(Entities.Models.StatementType.InfNumConv);
            translator.Execute(text);
            transaction.Received().AddUnits("glob", 'I');
        }

        [TestMethod]
        public void Set_Comodities_Called_After_Setting_Units_Test()
        {
            var text = "glob glob Silver is 34 Credits";
            transaction.GalacticUnits.Returns(new List<string> { "glob" });
            validater.ValidateText(text).Returns(Entities.Models.StatementType.InfComPric);
            translator.Execute(text);
            transaction.Received().AddComodity("Silver", Arg.Any<string []>(), 34);
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
