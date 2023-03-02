namespace CompaniesAPP.Shared.Components;

public sealed partial class PopupAlert
{
    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }

    [Parameter]
    public string? HeaderText { get; set; }

    [Parameter]
    public string? BodyText { get; set; }

    [Parameter, AllowNull]
    public string Type { get; set; }

    public async Task Show(string bodyText, string headerText = "", string type = "danger")
    {
        Type = type;
        HeaderText = headerText;
        BodyText = bodyText;
        IsVisible = true;
        StateHasChanged();

        await Task.Delay(8000);
        Close();
    }

    private void Close()
    {
        HeaderText = string.Empty;
        BodyText = string.Empty;
        IsVisible = false;
        StateHasChanged();
    }
}