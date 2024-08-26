using UnityEngine;
using UnityEngine.UI;


public class StressBarManager : MonoBehaviour
{
    public Image stressBar;  //StressBar resmi
    public Image indicator;  //StressBar üzerindeki gösterge


    private int maxScore = 21; // Maksimum stres seviyesi
    private int minScore = 0;  // Minimum stres seviyesi

    //StressBarýn en alt ve en üst pozisyonu ayarlanýr.
    public float minYPosition = -830f;
    public float maxYPosition = 520f;

    void Start()
    {
        // Kullanýcýnýn anketten aldýðý toplam skor totalScore deðiþkenine atanýr.
        int totalScore = SurveyData.totalScore;


        // Stres bar üzerinde skoru gösterecek fonksiyon çaðýrýlýr.
        UpdateStressBar(totalScore);
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