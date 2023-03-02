namespace CompaniesAPP.Shared.Components;

public sealed partial class CNPJInputMask
{
    private bool _preventDefault = false;

    [Parameter, AllowNull]
    public string ErrorMessage { get; set; }

    [Parameter, AllowNull]
    public string Field { get; set; }

    [Parameter, AllowNull]
    public string Placeholder { get; set; }

    [Parameter, AllowNull]
    public string Value { get; set; }

    [Parameter, AllowNull]
    public EventCallback<ResultInput> ValueChanged { get; set; }

    private void ValidateMask(KeyboardEventArgs e)
    {
        _preventDefault = e.Key != null && (e.Key.Any(x => !Char.IsNumber(x)) && e.Key != "Backspace" && !(e.CtrlKey && (e.Key == "Control" || e.Key == "v"))) || (Value.Length == 18 && e.Key != "Backspace");
    }

    private void ValidateMask(CustomPasteEventArgs args)
    {
        _preventDefault = !Brazil.IsValidCNPJ(args.Value);
    }
    private async Task ValidateMask(ChangeEventArgs args)
    {
        await ValidateCNPJ((string)args.Value!);
    }

    private Task ValidateCNPJ(string cnpj)
    {
        var inputValue = ApplyMask(cnpj);
        bool isValid = Brazil.IsValidCNPJ(inputValue);

        if (string.IsNullOrEmpty(inputValue) || isValid)
        {
            ErrorMessage = string.Empty;
        }
        else
        {
            ErrorMessage = "Formato inválido";
        }

        return ValueChanged.InvokeAsync(new(inputValue, isValid, ErrorMessage));
    }

    private static string ApplyMask(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        value = new string(value.Where(char.IsNumber).ToArray());

        if (value.Length > 14)
            value = value[..14];

        if (value.Length > 2)
            value = value.Insert(2, ".");
        if (value.Length > 6)
            value = value.Insert(6, ".");
        if (value.Length > 10)
            value = value.Insert(10, "/");
        if (value.Length > 15)
            value = value.Insert(15, "-");

        return value;
    }
}