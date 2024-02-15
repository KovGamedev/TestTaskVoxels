using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScreenshotGrabber
{
    [MenuItem("Screenshot/Grab")]
    public static void Grab()
    {
        if (Directory.Exists("Screenshots") == false)
            Directory.CreateDirectory("Screenshots");

        ScreenCapture.CaptureScreenshot($"Screenshots/Screenshot_{DateTime.Now.ToFileTime()}.png", 1);
    }
}
