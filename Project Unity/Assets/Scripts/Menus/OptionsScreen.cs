using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    [Header("Toggles")]
    public Toggle fullscreenTog, vsyncTog, fpsCounterTog;

    [Header("Other Stuff")]
    public static bool showFPSCounter;

    private void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;
        fpsCounterTog.isOn = false;

        if (QualitySettings.vSyncCount == 0)
            vsyncTog.isOn = false;
        else
            vsyncTog.isOn = true;
    }

    public void ApplyGraphics()
    {
        Screen.fullScreen = fullscreenTog.isOn;

        if (vsyncTog.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    public void ApplyFPS()
    {
        showFPSCounter = fpsCounterTog.isOn;
    }
}