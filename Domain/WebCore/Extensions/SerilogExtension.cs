using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using WebCore.Models;

namespace WebCore.Extensions;

public static class SerilogExtension
{
    public static LoggerConfiguration Api(
        this LoggerSinkConfiguration loggerConfiguration,
        IOptions<TelegramBotCredential>? botCredential,
        LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose)
    {
        if (loggerConfiguration == null)
            throw new ArgumentNullException(nameof(loggerConfiguration));

        return loggerConfiguration.Sink(
            new ApiSink(botCredential),
            restrictedToMinimumLevel);
    }
}

public class ApiSink : ILogEventSink
{
    private readonly TelegramBotCredential _botCredential;
    public ApiSink(IOptions<TelegramBotCredential>? botCredential)
    {
        _botCredential = botCredential.Value;
    }
    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage();
        
        message += $"\n#{_botCredential.Project}";

        if (logEvent.Exception != null)
            message += $"\n\nException:\n{logEvent.Exception.GetType().FullName}: {logEvent.Exception.Message}\n{logEvent.Exception.StackTrace}";

        using var client = new HttpClient();
        var response = client.GetAsync($"{_botCredential.Domain}/bot{_botCredential.Token}/sendMessage?chat_id={_botCredential.ChatId}&text={Uri.EscapeDataString(message)}").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Log ma'lumotlari jo'natilishda xatolik yuz berdi: {response.StatusCode}");
    }
}