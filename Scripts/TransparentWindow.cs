using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransparentWindow : MonoBehaviour
{
    // Start is called before the first frame update
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);


    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    private struct MARGINS
    {
        public int cxLeftWidht;
        public int cxRightWidht;
        public int cyTopHeightt;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    private void Start()
    {
        MessageBox(new IntPtr(0), "Hello World", "Hello Dialog", 0);

        IntPtr hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidht = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
    }
}
