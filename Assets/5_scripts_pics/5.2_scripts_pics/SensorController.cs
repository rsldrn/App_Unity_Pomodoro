using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SensorController : MonoBehaviour
{
    public Button measureButton;
    public TMP_Text statusText;
    private string deviceName = "esp32"; // Deneyap Kart'ýn mDNS adý
    private int port = 80;

    void Start()
    {
        measureButton.onClick.AddListener(OnMeasureButtonClick);
    }

    public void OnMeasureButtonClick()
    {
        statusText.text = "Ölçüm yapýlýyor...";
        Debug.Log("Ölçüm butonuna basýldý.");
        string url = "http://" + deviceName + ".local:" + port + "/start";
        Debug.Log("URL: " + url);
        measureButton.interactable = false;
        
        StartCoroutine(GetHeartRate(url));
    }

    IEnumerator GetHeartRate(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        Debug.Log("HTTP isteði gönderildi.");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            statusText.text = "Hata: Sensöre baðlanýlamadý.";
            measureButton.interactable = true;
            Debug.LogError("HTTP isteði hatasý: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            //statusText.text = "Ölçüm Sonucu: " + response; zaten çok hýzlý geçiyor, burasý görünmüyor.
            Debug.Log("HTTP yanýtý alýndý: " + response);

            if (int.TryParse(response, out int heartRate))
            {
                PlayerPrefs.SetInt("HeartRate", heartRate);
                PlayerPrefs.Save(); // Deðiþiklikleri hemen diske yaz
                Debug.Log("Nabýz verisi kaydedildi: " + heartRate);
                UnityEngine.SceneManagement.SceneManager.LoadScene("RENK5.4_ssonuc"); // BURASI DEÐÝÞEBÝLÝR !!!
            }
            else
            {
                statusText.text = "Geçersiz ölçüm sonucu.";
                measureButton.interactable = true;
                Debug.LogError("Geçersiz ölçüm sonucu: " + response);
            }
        }
    }
}
