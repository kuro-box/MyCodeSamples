using System;
using System.Linq;

namespace K.Helpers.Commands
{
    public static class TypesExtensions
    {
        public static string Fill(this string messageOrFormatString, params object[] args)
        {
            return string.Format(messageOrFormatString, args);
        }

        public static void Print(this string messageOrFormatString, params object[] args)
        {
            if (string.IsNullOrEmpty(messageOrFormatString))
                throw new ArgumentNullException("messageOrFormatString");

            var printString = args == null || !args.Any() ? messageOrFormatString : messageOrFormatString.Fill(args);
            Console.WriteLine(printString);
        }
    }
}
