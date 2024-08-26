using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SensorController : MonoBehaviour
{
    public Button measureButton;
    public TMP_Text statusText;
    private string deviceName = "esp32"; // Deneyap Kart'�n mDNS ad�
    private int port = 80;

    void Start()
    {
        measureButton.onClick.AddListener(OnMeasureButtonClick);
    }

    public void OnMeasureButtonClick()
    {
        statusText.text = "�l��m yap�l�yor...";
        Debug.Log("�l��m butonuna bas�ld�.");
        string url = "http://" + deviceName + ".local:" + port + "/start";
        Debug.Log("URL: " + url);
        measureButton.interactable = false;
        
        StartCoroutine(GetHeartRate(url));
    }

    IEnumerator GetHeartRate(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        Debug.Log("HTTP iste�i g�nderildi.");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            statusText.text = "Hata: Sens�re ba�lan�lamad�.";
            measureButton.interactable = true;
            Debug.LogError("HTTP iste�i hatas�: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            //statusText.text = "�l��m Sonucu: " + response; zaten �ok h�zl� ge�iyor, buras� g�r�nm�yor.
            Debug.Log("HTTP yan�t� al�nd�: " + response);

            if (int.TryParse(response, out int heartRate))
            {
                PlayerPrefs.SetInt("HeartRate", heartRate);
                PlayerPrefs.Save(); // De�i�iklikleri hemen diske yaz
                Debug.Log("Nab�z verisi kaydedildi: " + heartRate);
                UnityEngine.SceneManagement.SceneManager.LoadScene("RENK5.4_ssonuc"); // BURASI DE���EB�L�R !!!
            }
            else
            {
                statusText.text = "Ge�ersiz �l��m sonucu.";
                measureButton.interactable = true;
                Debug.LogError("Ge�ersiz �l��m sonucu: " + response);
            }
        }
    }
}
