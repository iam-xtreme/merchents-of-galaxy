using System.Collections.Generic;

namespace MOG.Entities.Contracts
{
    public interface IGalacticTransaction
    {
        List<string> Comodities { get; }
        List<string> GalacticUnits { get; }

        void AddComodity(string name, string[] units, int credits);
        void AddUnits(string gUnit, char roman);
        int CalculateCreditsForComodity(string name, string[] units);
        int GetArabic(string[] units);
    }
}