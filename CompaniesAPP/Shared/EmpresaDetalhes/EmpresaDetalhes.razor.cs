namespace CompaniesAPP.Shared.EmpresaDetalhes;

public sealed partial class EmpresaDetalhes
{
    [Parameter, AllowNull]
    public Company Company { get; set; }
}