// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AcceptanceTestLibrary.UIAWrapper
{
    public static class MouseEvents
    {
        private const UInt32 MouseEventLeftDown = 0x0002;

        private const UInt32 MouseEventLeftUp = 0x0004;

        private const UInt32 MouseEventRightDown = 0x0008;

        private const UInt32 MouseEventRightUp = 0x00010; 

        [DllImport("user32.dll")]

        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

        public static void Click()
        {

            mouse_event(MouseEventLeftDown, 0, 0, 0, IntPtr.Zero);

            mouse_event(MouseEventLeftUp, 0, 0, 0, IntPtr.Zero);

        }

        public static void RightClick()
        {
            mouse_event(MouseEventRightDown, 0, 0, 0, IntPtr.Zero);
            mouse_event(MouseEventRightUp, 0, 0, 0, IntPtr.Zero);         

        } 

    }
}
