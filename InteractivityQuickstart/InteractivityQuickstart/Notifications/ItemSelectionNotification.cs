using Prism.Interactivity.InteractionRequest;
using System.Collections.Generic;

namespace InteractivityQuickstart.Notifications
{
    public class ItemSelectionNotification : Confirmation
    {
        public ItemSelectionNotification()
        {
            this.Items = new List<string>();
            this.SelectedItem = null;
        }

        public ItemSelectionNotification(IEnumerable<string> items)
            : this()
        {
            foreach(string item in items)
            {
                this.Items.Add(item);
            }
        }

        public IList<string> Items { get; private set; }

        public string SelectedItem { get; set; }
    }
}
