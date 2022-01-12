// See https://aka.ms/new-console-template for more information
using Microsoft.Toolkit.Uwp.Notifications;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

try
{
    //Process.Start(@"C:\Windows\System32\notepad.exe");

    // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
    new ToastContentBuilder()
        .AddArgument("action", "viewConversation")
        .AddArgument("conversationId", 9813)
        .AddText("テストテスト")
        .AddText("テストですよー")
        .Show();

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.ReadLine();