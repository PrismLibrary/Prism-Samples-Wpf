using Prism.Interactivity.InteractionRequest;

namespace UsingPopupWindowAction.Notifications
{
    public interface ICustomNotification : IConfirmation
    {
        string SelectedItem { get; set; }
    }
}
