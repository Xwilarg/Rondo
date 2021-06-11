using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

DiscordSocketClient _client = new(new()
{
    LogLevel = LogSeverity.Verbose
});

_client.Log += (e) =>
{
    Console.WriteLine(e);
    return Task.CompletedTask;
};

await _client.LoginAsync(TokenType.Bot, File.ReadAllText("token.txt"));
await _client.StartAsync();

await Task.Delay(-1);