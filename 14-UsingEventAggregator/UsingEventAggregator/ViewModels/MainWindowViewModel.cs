namespace UsingEventAggregator.ViewModels
{
    using Prism.Mvvm;

    /// <summary>
    ///     Class MainWindowViewModel.
    ///     Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    /// <summary>
    ///     Class MainWindowViewModel.
    ///     Implements the <see cref="BindableBase" />
    /// </summary>
    /// <seealso cref="BindableBase" />
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        ///     The title
        /// </summary>
        /// <summary>
        ///     The title
        /// </summary>
        private string _title = "Prism Unity Application";

        /// <summary>
        ///     Title for the application
        /// </summary>
        /// <value>The title.</value>
        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}