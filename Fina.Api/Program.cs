using Fina.Api;
using Fina.Api.Common.Api;
using Fina.Api.Data;
using Fina.Api.Endpoints;
using Fina.Api.Handlers;
using Fina.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

//builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

//builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
//builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();


var app = builder.Build();


if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
//app.UseSecurity();
app.MapEndpoints();

//app.MapGet("/", () => "Hello World!");


app.Run();

//"Server=DESKTOP-BLIKA9R; Database=Fina; Uid=sa; Pwd=!Z2x525q1; TrustServerCertificate=true;"
//"Server=DESKTOP-BLIKA9R; Database=Fina; Uid=sa; Pwd=!Z2x525q1; Trusted_Connection=false; TrustServerCertificate=true;"