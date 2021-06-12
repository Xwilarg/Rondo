using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

DiscordSocketClient _client = new(new()
{
    LogLevel = LogSeverity.Verbose
});

ITextChannel _logChannel = null;

_client.Log += (e) =>
{
    Console.WriteLine(e);
    return Task.CompletedTask;
};
_client.Ready += () =>
{
    _logChannel = _client.GetGuild(446053427714326528).GetTextChannel(853071337030549504);
    return Task.CompletedTask;
};
_client.MessageReceived += async (arg) =>
{
    SocketUserMessage msg = arg as SocketUserMessage;
    if (msg == null)
    {
        return;
    }
    // Copy logs messages
    if (msg.Channel is ITextChannel chan)
    {
        if (chan.GuildId == 832001341865197579 && chan.Id == 832026028523126794)
        {
            await _logChannel.SendMessageAsync(msg.Content, embed: msg.Embeds.FirstOrDefault());
        }
    }
};
_client.ReactionAdded += async (msg, chan, react) =>
{
    if (chan is ITextChannel tChan && tChan.GuildId == 446054909377511437 && msg.Id == 853067193968492574)
    {
        if (react.Emote.Name == "🌟")
        {
            if (react.User.Value is IGuildUser gUser)
            {
                await gUser.AddRoleAsync(853071016733573140);
            }
        }
        else if (react.Emote.Name == "🔞")
        {
            if (react.User.Value is IGuildUser gUser)
            {
                await gUser.AddRoleAsync(853071055127183410);
            }
        }
        else if (react.Emote.Name == "📰")
        {
            if (react.User.Value is IGuildUser gUser)
            {
                await gUser.AddRoleAsync(853071085582286878);
            }
        }
    }
};

await _client.LoginAsync(TokenType.Bot, File.ReadAllText("token.txt"));
await _client.StartAsync();

await Task.Delay(-1);