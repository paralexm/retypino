using Microsoft.AspNetCore.Builder;
using PhotinoNET;
using PhotinoNET.Server;

namespace Retypino;

internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var app = PhotinoServer.CreateStaticFileServer(args, 9000, 100, "wwwroot", out string baseUrl);
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.RunAsync();

        string windowTitle = "Retypino!";

        var window = new PhotinoWindow()
            .SetTitle(windowTitle)
            .Center()
            .SetResizable(true)
            //.RegisterCustomSchemeHandler("app", (object sender, string scheme, string url, out string contentType) =>
            //{
            //    contentType = "text/javascript";
            //    return new MemoryStream(Encoding.UTF8.GetBytes(@"
            //            (() =>{
            //                window.setTimeout(() => {
            //                    alert(`🎉 Dynamically inserted JavaScript.`);
            //                }, 1000);
            //            })();
            //        "));
            //})
            // Most event handlers can be registered after the
            // PhotinoWindow was instantiated by calling a registration 
            // method like the following RegisterWebMessageReceivedHandler.
            // This could be added in the PhotinoWindowOptions if preferred.
            //.RegisterWebMessageReceivedHandler((object sender, string message) =>
            //{
            //    var window = (PhotinoWindow)sender;

            //    // The message argument is coming in from sendMessage.
            //    // "window.external.sendMessage(message: string)"
            //    string response = $"Received message: \"{message}\"";

            //    // Send a message back the to JavaScript event handler.
            //    // "window.external.receiveMessage(callback: Function)"
            //    window.SendWebMessage(response);
            //})
            .Load($"{baseUrl}/index.html");

        window.WaitForClose();
    }
}