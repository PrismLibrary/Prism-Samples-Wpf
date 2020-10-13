using System;
using System.IO;
using System.Text;
using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;

namespace LoggingWithSerilog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ILoggerFacade _logger;
        private string _title = "Prism Serilog WPF Demo";

        public MainWindowViewModel(ILoggerFacade logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            LogDebugCommand = new DelegateCommand(LogDebug);
            LogInformationCommand = new DelegateCommand(LogInformation);
            LogWarningCommand = new DelegateCommand(LogWarning);
            LogExceptionCommand = new DelegateCommand(LogException);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Text
        {
            get
            {
                const string logFileName = "DemoLog.txt";

                if (!File.Exists(logFileName))
                {
                    return null;
                }

                // FileShare.ReadWrite required for Serilog to continue writing - File.ReadAllText doesn't allow that
                using (var stream = new FileStream(logFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public DelegateCommand LogDebugCommand { get; }
        public DelegateCommand LogInformationCommand { get; }
        public DelegateCommand LogWarningCommand { get; }
        public DelegateCommand LogExceptionCommand { get; }

        private void LogDebug()
        {
            _logger.Log("This is a Debug message!", Category.Debug, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogInformation()
        {
            _logger.Log("This is an Information message!", Category.Info, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogWarning()
        {
            _logger.Log("This is an Warning message!", Category.Warn, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogException()
        {
            _logger.Log("This is an Exception message!", Category.Exception, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }
    }
}
