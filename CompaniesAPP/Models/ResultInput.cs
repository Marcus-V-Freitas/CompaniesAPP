namespace CompaniesAPP.Models;

public sealed class ResultInput
{
    public string Input { get; private set; }

    public bool IsValid { get; private set; }

    public string ErrorMessage { get; private set; }

    public ResultInput(string input, bool isValid, string errorMessage)
    {
        Input = input;
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }
}