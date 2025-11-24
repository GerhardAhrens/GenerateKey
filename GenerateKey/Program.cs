namespace GenerateKey
{
    /* Imports from NET Framework */
    using System;

    using Core;

    /* Imports from ModernUI Framework */
    using ModernConsole.Menu;

    public class Program
    {
        private static void Main(string[] args)
        {
            SmartMenu.Menu("Key / LicenseKey Generator")
                .Add("Beliebige Zeichen", () => { MenuPoint1(); })
                .Add("Beliebige Zeichen mit (d) Zahlen, (A) Zeichen groß, (a) Zeichen klein", () => { MenuPoint2(); })
                .Add("Variable {d:4} Länge im Template", () => { MenuPoint3(); })
                .Add("Template mit konstaten Werten", () => { MenuPoint4(); })
                .Add("Template mit allen Varianten", () => { MenuPoint5(); })
                .Show();

        }

        private static void ApplicationExit()
        {
            Environment.Exit(0);
        }

        private static void MenuPoint1()
        {
            const string pattern = "xxxx-xxxx-xx-xx-xx-xxxx";
            MConsole.Clear();

            StringTemplate tmp = new StringTemplate(pattern);
            string result = tmp.GetResult();

            MConsole.Alert(pattern, "Template", ModernConsole.Message.ConsoleMessageType.Info);
            MConsole.Alert($"{result}", "Result Template", ModernConsole.Message.ConsoleMessageType.Info);

            MConsole.Wait();
        }

        private static void MenuPoint2()
        {
            const string pattern = "dddd-AAAA-aaaa";
            MConsole.Clear();

            StringTemplate tmp = new StringTemplate(pattern);
            string result = tmp.GetResult();

            MConsole.Alert(pattern, "Template", ModernConsole.Message.ConsoleMessageType.Info);
            MConsole.Alert($"{result}", "Result Template", ModernConsole.Message.ConsoleMessageType.Info);

            MConsole.Wait();
        }

        private static void MenuPoint3()
        {
            const string pattern = "{x:4}-xxxx-AAAA";
            MConsole.Clear();

            StringTemplate tmp = new StringTemplate(pattern);
            tmp.AddToken('x', "4242");
            string result = tmp.GetResult();

            MConsole.Alert(pattern, "Template", ModernConsole.Message.ConsoleMessageType.Info);
            MConsole.Alert($"{result}", "Result Template", ModernConsole.Message.ConsoleMessageType.Info);

            MConsole.Wait();
        }

        private static void MenuPoint4()
        {
            const string pattern = "xxxx-DD-WW-xxxx";
            MConsole.Clear();

            StringTemplate tmp = new StringTemplate(pattern);
            tmp.AddToken('D', "42");
            tmp.AddToken('W', "A1");
            string result = tmp.GetResult();

            MConsole.Alert(pattern, "String Template", ModernConsole.Message.ConsoleMessageType.Info);
            MConsole.Alert($"{result}", "String Template", ModernConsole.Message.ConsoleMessageType.Info);

            MConsole.Wait();
        }

        private static void MenuPoint5()
        {
            const string pattern = "[A:4]-[x:4]-DD-WW-[x:4]-[d:5]";
            MConsole.Clear();

            StringTemplate tmp = new StringTemplate(pattern);
            tmp.AddToken('D', "42");
            tmp.AddToken('W', "A1");
            string result = tmp.GetResult();

            MConsole.Alert(pattern, "String Template", ModernConsole.Message.ConsoleMessageType.Info);
            MConsole.Alert($"{result}", "String Template", ModernConsole.Message.ConsoleMessageType.Info);

            MConsole.Wait();
        }
    }
}
