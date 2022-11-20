using Micro.Auth;
using Micro.Framework;
using Micro.Handlers;
using Micro.Identity;
using Micro.Messaging;
using VideoHub.Services.Users.Core;
using VideoHub.Services.Users.Core.Commands;
using VideoHub.Services.Users.Core.Queries;
using VideoHub.Services.Users.Core.Services;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Services.AddCore(builder.Configuration);

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

app.MapGet("/users", async (string? username, IDispatcher dispatcher) =>
        Results.Ok(await dispatcher.QueryAsync(new GetUsers(username))))
    .WithTags("Users").WithName("Get users");

app.MapGet("/users/{userId:long}", async (long userId, IDispatcher dispatcher) =>
{
    var user = await dispatcher.QueryAsync(new GetUser(userId));
    return user is null ? Results.NotFound() : Results.Ok(user);
}).WithTags("Users").WithName("Get user");

app.MapGet("/me", async (IDispatcher dispatcher, HttpContext context) =>
{
    var account = await dispatcher.QueryAsync(new GetAccount(context.UserId()));
    return account is null ? Results.NotFound() : Results.Ok(account);
}).RequireAuthorization().WithTags("Account").WithName("Get account");

app.MapPost("/sign-up", async (SignUp command, IDispatcher dispatcher, IIdGen idGen) =>
{
    await dispatcher.SendAsync(command with {UserId = idGen.Create()});
    return Results.NoContent();
}).WithTags("Account").WithName("Sign up");

app.MapPost("/sign-in", async (SignIn command, IDispatcher dispatcher, ITokenStorage storage) =>
{
    await dispatcher.SendAsync(command);
    var jwt = storage.Get();
    return Results.Ok(jwt);
}).WithTags("Account").WithName("Sign in");

app.MapGet("/users/{userId:long}/subscription", async (long userId, IDispatcher dispatcher) =>
{
    var subscription = await dispatcher.QueryAsync(new GetUserSubscription(userId));
    return subscription is null ? Results.NotFound() : Results.Ok(subscription);
}).WithTags("Subscriptions").WithName("Get user subscription");

app.MapPut("/users/{userId:long}/subscription", async (long userId, UpdateUserSubscription command,
    IDispatcher dispatcher) =>
{
    await dispatcher.SendAsync(command with {UserId = userId});
    return Results.NoContent();
}).WithTags("Subscriptions").WithName("Update user subscription").RequireAuthorization("admin");

app.UseMicroFramework();

app.Subscribe()
    .Command<UpdateUserSubscription>();

app.Run();
