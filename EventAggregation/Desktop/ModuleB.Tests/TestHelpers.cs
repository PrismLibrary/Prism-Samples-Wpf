using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ModuleB.Tests
{
    public static class TestHelpers
    {
        public static void InvokeIfRequired(this Dispatcher dispatcher,
    Action action, DispatcherPriority priority)
        {
            if (!dispatcher.CheckAccess())
            {
                dispatcher.Invoke(priority, action);
            }
            else
            {
                action();
            }
        }
    }
}
