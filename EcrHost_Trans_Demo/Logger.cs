using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;

namespace WiseShare
{
    public class Logger
    {
        public static TextBox OutputTextBox = null;
        public static string LastErrorMsg = "";

        private static readonly string _logDirPath = 
            Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logs");

        private static bool LogFile(string msg, string verb, DateTime time)
        {
            lock (_logDirPath)
            {
                try
                {
                    if (!Directory.Exists(_logDirPath))
                    {
                        return false;//Directory.CreateDirectory(_logDirPath);
                    }

                    string logFilePath = Path.Combine(_logDirPath, time.ToString("yyyy-MM-dd") + ".log");

                    using (StreamWriter sw = new StreamWriter(logFilePath, true))
                    {
                        sw.WriteLine($"{time:HH:mm:ss.fff} {verb}: {msg}");
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        public static void Info(string msg, TextBox outputTextBox = null)
        {
            DateTime now = DateTime.Now;

            LogFile(msg, "I", now);

            TextBox textBox = outputTextBox ?? OutputTextBox;
            if (textBox != null)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.BeginInvoke(new MethodInvoker(() => PrintInfo(textBox, msg, now)));
                }
                else
                {
                    PrintInfo(textBox, msg, now);
                }
            }
        }

        public static void Error(string msg, TextBox outputTextBox = null)
        {
            LastErrorMsg = msg;
            DateTime now = DateTime.Now;

            LogFile(msg, "E", now);

            TextBox textBox = outputTextBox ?? OutputTextBox;
            if (textBox != null)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.BeginInvoke(new MethodInvoker(() => PrintError(textBox, msg, now)));
                }
                else
                {
                    PrintError(textBox, msg, now);
                }
            }
        }

        public static void LogBinaryData(string msg, byte[] data, TextBox outputTextBox = null)
        {
            DateTime now = DateTime.Now;

            StringBuilder bs = new StringBuilder(msg, 256);

            if (data != null && data.Length > 0)
            {
                bs.Append("\r\n");

                int numLines = (data.Length + 15) / 16;

                for (int line = 0; line < numLines; ++line)
                {
                    bs.Append($"{line * 16:X4} ");

                    int indexEnd = Math.Min((line + 1) * 16, data.Length);
                    for (int i = line * 16; i < indexEnd; ++i)
                    {
                        bs.Append($"{data[i]:X2} ");
                    }

                    bs.Append(new string(' ', ((line + 1) * 16 - indexEnd) * 3 + 2));

                    for (int i = line * 16; i < indexEnd; ++i)
                    {
                        if (data[i] >= 0x20 && data[i] <= 0x7E)
                        {
                            bs.Append((char)data[i]);
                        }
                        else
                        {
                            bs.Append('.');
                        }

                    }

                    if (line + 1 < numLines)
                    {
                        bs.Append("\r\n");
                    }
                }
            }

            LogFile(bs.ToString(), "I", now);

            TextBox textBox = outputTextBox ?? OutputTextBox;
            if (textBox != null)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.BeginInvoke(new MethodInvoker(() => PrintBinaryData(textBox, msg, data, now)));
                }
                else
                {
                    PrintBinaryData(textBox, msg, data, now);
                }
            }
        }

        public static void PrintText(TextBox outputTextBox, string text, Color? color = null)
        {
            try
            {
                int numLines = outputTextBox.Lines.Length;
                if (numLines > 500)
                {
                    outputTextBox.Select(0, outputTextBox.GetFirstCharIndexFromLine(numLines - 500));
                    outputTextBox.SelectedText = "";
                }

                if (color == null)
                {
                    outputTextBox.AppendText(text);
                }
                else
                {
                    outputTextBox.SelectionStart = outputTextBox.TextLength;
                    outputTextBox.SelectionLength = 0;
                    //outputTextBox.SelectionColor = (Color)color;

                    outputTextBox.AppendText(text);

                    //outputTextBox.SelectionColor = outputTextBox.ForeColor;
                }

                outputTextBox.ScrollToCaret();
            }
            catch
            {
            }
        }

        public static void PrintInfo(TextBox outputTextBox, string msg, DateTime time)
        {
            PrintText(outputTextBox, $"{time:HH:mm:ss.fff} {msg}\r\n");
        }

        public static void PrintError(TextBox outputTextBox, string msg, DateTime time)
        {
            PrintText(outputTextBox, $"{time:HH:mm:ss.fff} {msg}\r\n", Color.Red);
        }

        public static void PrintBinaryData(TextBox outputTextBox, string msg, byte[] data, DateTime time)
        {
            PrintText(outputTextBox, $"{time:HH:mm:ss.fff} {msg}\r\n");

            StringBuilder bs = new StringBuilder(128);
            int numLines = (data.Length + 15) / 16;

            for (int line = 0; line < numLines; ++line)
            {
                PrintText(outputTextBox, $"{line * 16:X4} ", Color.Blue);

                int indexEnd = Math.Min((line + 1) * 16, data.Length);
                for (int i = line * 16; i < indexEnd; ++i)
                {
                    bs.Append($"{data[i]:X2} ");
                }

                bs.Append(new string(' ', ((line + 1) * 16 - indexEnd) * 3 + 2));

                for (int i = line * 16; i < indexEnd; ++i)
                {
                    if (data[i] >= 0x20 && data[i] <= 0x7E)
                    {
                        bs.Append((char)data[i]);
                    }
                    else
                    {
                        bs.Append('.');
                    }
                }

                bs.Append("\r\n");
                PrintText(outputTextBox, bs.ToString());

                bs.Clear();
            }
        }
    }
}
