using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Serilog;
using Serilog.Events;

namespace Bitmex.Client.Websocket.Sample.WinForms
{
    static class Program
    {
        private static StatsPresenter _presenter;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitLogging();

            var mainForm = new Form1();
            _presenter = new StatsPresenter(mainForm);

            Application.Run(mainForm);
        }

        private static void InitLogging()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var logPath = Path.Combine(executingDir, "logs", "verbose.log");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .WriteTo.Console(LogEventLevel.Information)
                .WriteTo.Debug(LogEventLevel.Debug)
                .CreateLogger();
        }
    }
}
