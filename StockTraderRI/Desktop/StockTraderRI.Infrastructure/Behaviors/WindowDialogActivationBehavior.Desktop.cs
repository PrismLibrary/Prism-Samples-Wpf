

namespace StockTraderRI.Infrastructure.Behaviors
{
    /// <summary>
    /// Specifies the <see cref="DialogActivationBehavior"/> class for using the behavior on WPF.
    /// </summary>
    public class WindowDialogActivationBehavior : DialogActivationBehavior
    {
        /// <summary>
        /// Creates a wrapper for the WPF <see cref="System.Windows.Window"/>.
        /// </summary>
        /// <returns>Instance of the <see cref="System.Windows.Window"/> wrapper.</returns>
        protected override IWindow CreateWindow()
        {
            return new WindowWrapper();
        }
    }
}
