using System;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Targets;

namespace BlackDesertLogin.Utils
{
    public static class Funcs
    {
        private static readonly Random Randomizer = new Random((int)DateTime.Now.Ticks);

        public static Random Random()
        {
            return Randomizer;
        }

        public static int GetRoundedUtc()
        {
            return (int) Math.Round((double) (GetCurrentMilliseconds()/1000));
        }

        private static readonly DateTime StaticDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentMilliseconds()
        {
            return (long)(DateTime.UtcNow - StaticDate).TotalMilliseconds;
        }

        private static readonly string[] Baths;

        static Funcs()
        {
            Baths = new string[256];
            for (int i = 0; i < 256; i++)
                Baths[i] = String.Format("{0:X2}", i);
        }

        public static string ToHex(this byte[] array)
        {
            StringBuilder builder = new StringBuilder(array.Length*2);

            for (int i = 0; i < array.Length; i++)
                builder.Append(Baths[array[i]]);

            return builder.ToString();
        }
        public static LoggingConfiguration NLogDefaultConfiguration
        {
            get
            {
                var config = new LoggingConfiguration();

                var consoleTarget = new ColoredConsoleTarget
                {
                    Layout =
                        "${time} | ${message}${onexception:${newline}EXCEPTION OCCURRED${newline}${exception:format=tostring}}",
                    UseDefaultRowHighlightingRules = false
                };
                config.AddTarget("console", consoleTarget);

                var fileTarget = new FileTarget
                {
                    Layout =
                        "${time} | ${message}${onexception:${newline}EXCEPTION OCCURRED${newline}${exception:format=tostring}}",
                    FileName = "${basedir}/Logs/${shortdate}/${level}.txt"
                };
                config.AddTarget("file", fileTarget);

                consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule("level == LogLevel.Info",
                                                                                      ConsoleOutputColor.Gray,
                                                                                      ConsoleOutputColor.Black));
                consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule("level == LogLevel.Warn",
                                                                                      ConsoleOutputColor.Yellow,
                                                                                      ConsoleOutputColor.Black));
                consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule("level == LogLevel.Error",
                                                                                      ConsoleOutputColor.Red,
                                                                                      ConsoleOutputColor.Black));
                consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule("level == LogLevel.Fatal",
                                                                                      ConsoleOutputColor.Red,
                                                                                      ConsoleOutputColor.White));

                LoggingRule rule1 = new LoggingRule("*", NLog.LogLevel.Debug, consoleTarget);
                config.LoggingRules.Add(rule1);

                LoggingRule rule2 = new LoggingRule("*", NLog.LogLevel.Debug, fileTarget);
                config.LoggingRules.Add(rule2);

                return config;
            }
        }
        public static string FormatHex(this byte[] data)
        {
            StringBuilder builder = new StringBuilder(data.Length * 4);

            int count = 0;
            int pass = 1;
            foreach (byte b in data)
            {
                if (count == 0)
                    builder.AppendFormat("{0,-6}\t", "[" + (pass - 1) * 16 + "]");

                count++;
                builder.Append(b.ToString("X2"));
                if (count == 4 || count == 8 || count == 12)
                    builder.Append(" ");
                if (count == 16)
                {
                    builder.Append("\t");
                    for (int i = (pass * count) - 16; i < (pass * count); i++)
                    {
                        char c = (char)data[i];
                        if (c > 0x1f && c < 0x80)
                            builder.Append(c);
                        else
                            builder.Append(".");
                    }
                    builder.Append("\r\n");
                    count = 0;
                    pass++;
                }
            }

            return builder.ToString();
        }

        public static byte[] ToBytes(this String hexString)
        {
            try
            {
                byte[] result = new byte[hexString.Length/2];

                for (int index = 0; index < result.Length; index++)
                {
                    string byteValue = hexString.Substring(index*2, 2);
                    result[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid hex string: {0}", hexString);
                throw;
            }
        }

        public static bool IsLuck(byte chance)
        {
            if (chance >= 100)
                return true;

            if (chance <= 0)
                return false;

            return new Random().Next(0, 100) <= chance;
        }
    }
}