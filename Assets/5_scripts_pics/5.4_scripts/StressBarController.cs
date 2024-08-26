using UnityEngine;
using UnityEngine.UI;


public class StressBarController : MonoBehaviour
{
    public Image stressBar;  //StressBar resmi
    public Image indicator;  //StressBar üzerindeki gösterge


    private int maxScore = 120; // Maksimum nabýz 120 alýndý
    private int minScore = 50;  // Minimum nabýz 40 alýndý

    //StressBarýn en alt ve en üst pozisyonu ayarlanýr.
    public float minYPosition = -830f;
    public float maxYPosition = 520f;

    void Start()
    {
        // Kullanýcýnýn nabýz deðeri heartRate deðiþkenine atanýr.
        int heartRate = PlayerPrefs.GetInt("HeartRate");

        // Nabýz deðeri sýnýrlarýn dýþýnda ise ilgili sýnýra eþitlenir. Bu, nabýz deðeri belirlenen sýnýrlar dýþýnda kalsa bile stres barýnda doðru çýktýyý görmeye yarar.
        if (heartRate < minScore)
        {
            heartRate = minScore;
        }
        if (heartRate > maxScore)
        {
            heartRate = maxScore;
        }

        // Stres bar üzerinde stres derecesini gösterecek fonksiyon çaðýrýlýr.
        UpdateStressBar(heartRate);
    }

    void UpdateStressBar(int score)
    {
        //Skor, minScore ile maxScore arasýnda ölçeklendirilir (0 ile 1 aralýðýnda bir deðer).
        float normalizedScore = Mathf.InverseLerp(minScore, maxScore, score);

        //Gösterge, stres bar üzerinde uygun pozisyona yerleþtirilir.
        RectTransform indicatorRect = indicator.GetComponent<RectTransform>();

        //Ölçeklendirilmiþ skora göre göstergenin Y pozisyonu hesaplanýr.
        float indicatorPositionY = Mathf.Lerp(minYPosition, maxYPosition, normalizedScore);

        //Göstergenin Y eksenindeki pozisyonu hesaplanan deðere göre ayarlanýr.
        indicatorRect.anchoredPosition = new Vector2(indicatorRect.anchoredPosition.x, indicatorPositionY);
    }
}