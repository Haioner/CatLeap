/*
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D playerLight;
    [SerializeField] private Color globalColor;
    public float dayToNightSpeed = 0.008f;
    public float nightToDaySpeed = 0.03f;
    bool isDay = true;

    private void Update()
    {
        if (!Game_Manager.isPlaying) return;
        
        SetTimer();
    }

    void SetTimer()
    {
        if (globalLight.intensity <= 0f)
            isDay = false;
        else if(globalLight.intensity >= 1f)
            isDay = true;

        if (isDay)
        {
            globalLight.intensity -= dayToNightSpeed * Time.deltaTime;
            playerLight.intensity += dayToNightSpeed * Time.deltaTime;
            globalColor.r -= dayToNightSpeed * 0.6f * Time.deltaTime;
            globalColor.g -= dayToNightSpeed * 0.6f * Time.deltaTime;
            globalLight.color = globalColor;
        }
        else
        {
            globalLight.intensity += nightToDaySpeed * Time.deltaTime;
            playerLight.intensity -= nightToDaySpeed * Time.deltaTime;
            globalColor = Color.white;
            globalLight.color = globalColor;
        }
    }
}
*/
