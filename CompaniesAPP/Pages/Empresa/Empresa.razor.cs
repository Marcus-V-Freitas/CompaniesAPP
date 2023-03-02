namespace CompaniesAPP.Pages.Empresa;

public sealed partial class Empresa
{
    private bool _isLoading { get; set; }

    private bool _isValid { get; set; }

    private string _errorMessage { get; set; } = string.Empty;

    private string _inputText { get; set; } = string.Empty;

    [AllowNull]
    private Company _company;

    [Inject, AllowNull]
    private CompaniesData _companiesData { get; set; }

    [Inject, AllowNull]
    private PopupAlert _popupAlert { get; set; }

    [Inject, AllowNull]
    private ILocalStorage _localStorage { get; set; }

    private void Limpar()
    {
        _inputText = string.Empty;
        _errorMessage = string.Empty;
        _company = null;
    }

    private void CheckIfValueIsValid(ResultInput result)
    {
        _inputText = result.Input;
        _isValid = result.IsValid;
        _errorMessage = result.ErrorMessage;
    }

    private async Task<bool> CheckIfExistsInCache()
    {
        _company = await _localStorage.GetObjectAsync<Company>(_inputText);

        return _company != null;
    }

    private async Task HandleSubmit()
    {
        _company = null!;

        if (!_isValid)
        {
            await _popupAlert.Show("O CNPJ informado ainda não está válido.", "Atenção");
            return;
        }

        try
        {
            _isLoading = true;

            bool cacheExists = await CheckIfExistsInCache();

            if (cacheExists)
            {
                return;
            }

            _company = await _companiesData.GetCompanyByCNPJ(_inputText);
            await _localStorage.SaveObjectAsync(_inputText, _company);
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }

        if (_company == null)
        {
            await _popupAlert.Show("Nenhum registro foi encontrado.", "Atenção", "warning");
        }
    }
}