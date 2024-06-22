using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    private void Start()
    {
        if (OptionsScreen.showFPSCounter)
        {
            fpsText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (OptionsScreen.showFPSCounter)
        {
            time += Time.deltaTime;

            frameCount++;

            if (time >= pollingTime)
            {
                int frameRate = Mathf.RoundToInt(frameCount / time);
                fpsText.text = frameRate.ToString() + " FPS";

                time -= pollingTime;
                frameCount = 0;
            }
        }
    }
}