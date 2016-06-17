using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win_UserProfile_Bkp
{
    public static class MyWPFfoldBrowsXten
    {
        public static System.Windows.Forms.IWin32Window GetIWin32Window(this System.Windows.Media.Visual visual)
        {
            var source = System.Windows.PresentationSource.FromVisual(visual) as System.Windows.Interop.HwndSource;
            System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
            return win;
        }

        private class OldWindow : System.Windows.Forms.IWin32Window
        {
            private readonly System.IntPtr _handle;
            public OldWindow(System.IntPtr handle)
            {
                _handle = handle;
            }

            // IWin32Window Members
            System.IntPtr System.Windows.Forms.IWin32Window.Handle
            {
                get { return _handle; }
            }
        }
    }

    
    // TRY NEW !!!!!!!!


    //    public static System.Windows.Forms.IWin32Window GetIWin32Window(this System.Windows.Media.Visual visual)
    //    {
    //        var source = System.Windows.PresentationSource.FromVisual(visual) as System.Windows.Interop.HwndSource;
    //        System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
    //        return win;
    //    }

    //    private class OldWindow : System.Windows.Forms.IWin32Window
    //    {
    //        private readonly System.IntPtr _handle;
    //        public OldWindow(System.IntPtr handle)
    //        {
    //            _handle = handle;
    //        }

    //        #region IWin32Window Members
    //        System.IntPtr System.Windows.Forms.IWin32Window.Handle
    //        {
    //            get { return _handle; }
    //        }
    //        #endregion
    //    }
    //}
}
