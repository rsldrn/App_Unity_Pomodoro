using UnityEngine;
using TMPro;

public class ResultController : MonoBehaviour
{
    public TMP_Text heartRateText;
    public TMP_Text stressLevelText;

    void Start()
    {
        int heartRate = PlayerPrefs.GetInt("HeartRate");
        heartRateText.text = heartRate + " BPM";

        if (heartRate > 0)
        {
            string stressLevel = GetStressLevel(heartRate);
            stressLevelText.text = stressLevel;
        }
        else
        {
            stressLevelText.text = "Geçersiz nabýz deðeri.";
        }
    }

    string GetStressLevel(int heartRate)
    {
        if (heartRate < 50)
            return "Hata!";
        else if (heartRate < 80)
            return "Düsük stres seviyesi";
        else if (heartRate < 100)
            return "Orta stres seviyesi";
        else
            return "Yüksek stres seviyesi";
    }
}
