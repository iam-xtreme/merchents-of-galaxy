using System.Collections.Generic;
using MOG.Utilities.Extension;

namespace MOG.Utilities
{
    public class GalacticTransaction : Entities.Contracts.IGalacticTransaction
    {
        Dictionary<string, char> unitRomanMap;
        Dictionary<string, float> comodities;
        public GalacticTransaction(string[] _comodities)
        {
            unitRomanMap = new Dictionary<string, char>();
            comodities = new Dictionary<string, float>();

            if (_comodities != null && _comodities.Length > 0)
                foreach (var item in _comodities)
                    comodities[item] = 0;

        }

        public List<string> Comodities
        {
            get
            {
                return new List<string>(comodities.Keys);
            }
        }

        public List<string> GalacticUnits
        {
            get
            {
                return new List<string>(unitRomanMap.Keys);
            }
        }

        public void AddUnits(string gUnit, char roman) => unitRomanMap[gUnit] = roman;

        public int GetArabic(string[] units)
        {
            var roman = string.Empty;
            if (units != null && units.Length > 0)
                foreach (var unit in units)
                {
                    roman = string.Concat(roman, unitRomanMap[unit]);
                }
            return roman.ToArabic();
        }

        public void AddComodity(string name, string[] units, int credits)
        {
            var arabicUnits = GetArabic(units);
            if (arabicUnits > 0)
                comodities[name] = (float)credits / arabicUnits;
        }

        public int CalculateCreditsForComodity(string name, string[] units)
        {
            var rate = comodities[name];
            var unitsInArabic = GetArabic(units);
            return (int)(rate * unitsInArabic);
        }
    }
}
