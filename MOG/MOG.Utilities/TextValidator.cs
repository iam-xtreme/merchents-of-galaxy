using MOG.Entities.Models;

namespace MOG.Utilities
{
    public class TextValidator : Entities.Contracts.IValidater
    {
        string creditString;
        public TextValidator(string _creditString = "Credits")
        {
            creditString = _creditString;
        }

        public string CreditString { get => creditString; set => creditString = value; }

        public StatementType ValidateText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text.EndsWith("?"))
                {
                    return text.Contains(creditString) ? StatementType.QueComPric : StatementType.QueNumConv;
                }
                else
                {
                    return text.Contains(creditString) ? StatementType.InfComPric : StatementType.InfNumConv;
                }
            }
            return StatementType.Undefined;
        }
    }
}
