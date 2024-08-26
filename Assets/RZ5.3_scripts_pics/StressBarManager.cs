using UnityEngine;
using UnityEngine.UI;


public class StressBarManager : MonoBehaviour
{
    public Image stressBar;  //StressBar resmi
    public Image indicator;  //StressBar �zerindeki g�sterge


    private int maxScore = 21; // Maksimum stres seviyesi
    private int minScore = 0;  // Minimum stres seviyesi

    //StressBar�n en alt ve en �st pozisyonu ayarlan�r.
    public float minYPosition = -830f;
    public float maxYPosition = 520f;

    void Start()
    {
        // Kullan�c�n�n anketten ald��� toplam skor totalScore de�i�kenine atan�r.
        int totalScore = SurveyData.totalScore;


        // Stres bar �zerinde skoru g�sterecek fonksiyon �a��r�l�r.
        UpdateStressBar(totalScore);
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