using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private const float PollingTime = 1f;
    private float time;
    private int frameCount;

    private void Start()
    {
        if (!OptionsScreen.showFPSCounter) return;
        fpsText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!OptionsScreen.showFPSCounter) return;

        time += Time.deltaTime;
        frameCount++;

        if (time >= PollingTime)
        {
            var frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = $"{frameRate} FPS";
            time -= PollingTime;
            frameCount = 0;
        }
    }
}