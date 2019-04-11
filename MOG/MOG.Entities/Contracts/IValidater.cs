namespace MOG.Entities.Contracts
{
    public interface IValidater
    {
        string CreditString { get; set; }
        Models.StatementType ValidateText(string text);
    }
}
