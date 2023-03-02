var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<CompaniesData>();
builder.Services.AddSingleton<PopupAlert>();
builder.Services.AddSingleton<ILocalStorage, LocalStorage>();

await builder.Build().RunAsync();
