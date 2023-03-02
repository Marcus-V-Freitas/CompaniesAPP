namespace CompaniesAPP.CustomEventArgs;

public sealed class CustomPasteEventArgs : EventArgs
{
    [AllowNull]
    public string Value { get; set; }
}