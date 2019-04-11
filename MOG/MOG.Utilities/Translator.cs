using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MOG.Utilities
{
    public class Translator
    {
        Entities.Contracts.IGalacticTransaction transaction;
        Entities.Contracts.IValidater validater;
        public Translator(Entities.Contracts.IGalacticTransaction _transaction, Entities.Contracts.IValidater _validater)
        {
            transaction = _transaction;
            validater = _validater;
        }

        public Entities.Models.Response Execute(string text)
        {
            var response = new Entities.Models.Response { Status = true };
            switch (validater.ValidateText(text))
            {
                case Entities.Models.StatementType.InfComPric:
                    ExtractComodityDetails(text);
                    break;

                case Entities.Models.StatementType.InfNumConv:
                    ReadNumericDetails(text);
                    break;

                case Entities.Models.StatementType.QueComPric:
                    response.Message = CalculateComodityPrice(text);
                    response.Status = !string.IsNullOrEmpty(response.Message);
                    break;

                case Entities.Models.StatementType.QueNumConv:
                    response.Message = ConvertToNumeric(text);
                    response.Status = !string.IsNullOrEmpty(response.Message);
                    break;

                default:
                    response.Status = false;
                    break;
            }
            if(!response.Status)
                response.Message = "I have no idea what you are talking about";

            return response;
        }

        private string ConvertToNumeric(string text)
        {
            string comodity;
            var gUnits = GetGalacticUnits(text, out comodity);

            if (string.IsNullOrEmpty(comodity) && gUnits.Length > 0)
            {
                var amount = transaction.GetArabic(gUnits);
                return string.Format($"{string.Join(" ", gUnits)} is {amount}");
            }
            return string.Empty;

        }

        private string CalculateComodityPrice(string text)
        {
            string comodity;
            var gUnits = GetGalacticUnits(text, out comodity);

            if (!string.IsNullOrEmpty(comodity) && gUnits.Length > 0)
            {
                var amount = transaction.CalculateCreditsForComodity(comodity, gUnits);
                return string.Format($"{string.Join(" ", gUnits)} {comodity} is {amount} {validater.CreditString}");
            }
            return string.Empty;
        }

        private void ReadNumericDetails(string text)
        {

            var array = text.Split(' ');
            if (array.Count() == 3 && array[1].Equals("is", StringComparison.CurrentCultureIgnoreCase))
            {
                transaction.AddUnits(array[0], array[2][0]);
            }
        }

        private void ExtractComodityDetails(string text)
        {
            var number = Regex.Match(text, @"\d+").Value;
            int credits = 0;
            int.TryParse(number, out credits);
            string comodity;
            var gUnits = GetGalacticUnits(text, out comodity);

            if (gUnits.Length > 0 && credits > 0 && !string.IsNullOrEmpty(comodity))
            {
                transaction.AddComodity(comodity, gUnits, credits);
            }
        }

        private string[] GetGalacticUnits(string text, out string comodity)
        {
            var gUnits = new List<string>();
            comodity = string.Empty;
            foreach (var item in text.Split(' '))
            {
                if (transaction.Comodities.Contains(item))
                    comodity = item;

                if (transaction.GalacticUnits.Contains(item))
                    gUnits.Add(item);
            }
            return gUnits.ToArray();
        }
    }
}
