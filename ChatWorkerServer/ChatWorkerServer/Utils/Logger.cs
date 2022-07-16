using System;

public static class Logger
{
    public static Action<string> Info => delegate(string s) { Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(s); Console.ForegroundColor = ConsoleColor.White; };
    public static Action<string> Warning => delegate (string s) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(s); Console.ForegroundColor = ConsoleColor.White; };
    public static Action<string> Error => delegate (string s) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(s); Console.ForegroundColor = ConsoleColor.White; };
}