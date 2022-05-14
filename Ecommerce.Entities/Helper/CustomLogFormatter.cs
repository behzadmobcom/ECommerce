using Serilog.Events;
using Serilog.Formatting;

namespace Entities.Helper;

public class CustomLogFormatter : ITextFormatter
{
    public void Format(LogEvent logEvent, TextWriter output)
    {
        output.WriteLine("----------------------------------------------------------------------------------");
        output.WriteLine($"Timestamp - {logEvent.Timestamp} | Level - {logEvent.Level}");
        output.WriteLine($"Message => {logEvent.MessageTemplate} ");
        output.WriteLine("----------------------------------------------------------------------------------");
        foreach (var logEventPropertyValue in logEvent.Properties)
            output.WriteLine(logEventPropertyValue.Key + " : " + logEventPropertyValue.Value);

        if (logEvent.Exception != null)
        {
            output.WriteLine("------------------------------Exception Details-----------------------------------");
            output.Write("Exception - {0}", logEvent.Exception);
            output.Write("StackTrace - {0}", logEvent.Exception.StackTrace);
            output.Write("Message - {0}", logEvent.Exception.Message);
            output.Write("Source - {0}", logEvent.Exception.Source);
            output.Write("InnerException - {0}", logEvent.Exception.InnerException);
            output.WriteLine("----------------------------------------------------------------------------------");
        }
    }
}