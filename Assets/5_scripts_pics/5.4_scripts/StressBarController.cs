using UnityEngine;
using UnityEngine.UI;


public class StressBarController : MonoBehaviour
{
    public Image stressBar;  //StressBar resmi
    public Image indicator;  //StressBar �zerindeki g�sterge


    private int maxScore = 120; // Maksimum nab�z 120 al�nd�
    private int minScore = 50;  // Minimum nab�z 40 al�nd�

    //StressBar�n en alt ve en �st pozisyonu ayarlan�r.
    public float minYPosition = -830f;
    public float maxYPosition = 520f;

    void Start()
    {
        // Kullan�c�n�n nab�z de�eri heartRate de�i�kenine atan�r.
        int heartRate = PlayerPrefs.GetInt("HeartRate");

        // Nab�z de�eri s�n�rlar�n d���nda ise ilgili s�n�ra e�itlenir. Bu, nab�z de�eri belirlenen s�n�rlar d���nda kalsa bile stres bar�nda do�ru ��kt�y� g�rmeye yarar.
        if (heartRate < minScore)
        {
            heartRate = minScore;
        }
        if (heartRate > maxScore)
        {
            heartRate = maxScore;
        }

        // Stres bar �zerinde stres derecesini g�sterecek fonksiyon �a��r�l�r.
        UpdateStressBar(heartRate);
    }

    void UpdateStressBar(int score)
    {
        //Skor, minScore ile maxScore aras�nda �l�eklendirilir (0 ile 1 aral���nda bir de�er).
        float normalizedScore = Mathf.InverseLerp(minScore, maxScore, score);

        //G�sterge, stres bar �zerinde uygun pozisyona yerle�tirilir.
        RectTransform indicatorRect = indicator.GetComponent<RectTransform>();

        //�l�eklendirilmi� skora g�re g�stergenin Y pozisyonu hesaplan�r.
        float indicatorPositionY = Mathf.Lerp(minYPosition, maxYPosition, normalizedScore);

        //G�stergenin Y eksenindeki pozisyonu hesaplanan de�ere g�re ayarlan�r.
        indicatorRect.anchoredPosition = new Vector2(indicatorRect.anchoredPosition.x, indicatorPositionY);
    }
}