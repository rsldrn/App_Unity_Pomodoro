using UnityEngine;
using UnityEngine.UI;

public class BadgeManager : MonoBehaviour
{
    public Image[] badgeImages; // Rozet görüntüleri
    public float[] badgeThresholds; // Rozetlerin kilidini açmak için gereken süreler
    public Sprite[] colorfulBadges; // Renkli rozetlerin sprite'larý

    private PomodoroManager manager;

    private void Start()
    {
        manager = PomodoroManager.Instance;  //Singleton PomodoroManager örneði alýnýr.

        if (manager == null)
        {
            Debug.LogError("PomodoroManager instance is not found.");
            return;
        }
        LoadBadges();  // Uygulama baþlarken önceden kazanýlan rozetler yüklenir.
        UpdateBadges();  // Uygulama baþlarken rozetler güncellenir.
    }

    private void Update()
    {
        UpdateBadges();
    }

    private void UpdateBadges()
    {
        for (int i = 0; i < badgeImages.Length; i++)
        {
            if (manager.totalWorkTime >= badgeThresholds[i])  //Toplam çalýþma süresi, rozet kilitlerini açmak için gereken eþik deðerlerini geçtiðinde 
                                                              //rozet sprite'larý renkli hale getirilir.
            {
                badgeImages[i].sprite = colorfulBadges[i];
            }
        }
    }
    public void SaveBadges() //Açýlan rozetlerin durumu PlayerPrefs ile kaydedilir.
    {
        for (int i = 0; i < badgeImages.Length; i++)
        {
            PlayerPrefs.SetInt("Badge_" + i, manager.totalWorkTime >= badgeThresholds[i] ? 1 : 0);
        }
        PlayerPrefs.Save(); // Deðiþiklikler diske atýlýr.
    }

    public void LoadBadges() //Daha önce kazanýlmýþ rozetler PlayerPrefs'ten yüklenir.
    {
        for (int i = 0; i < badgeImages.Length; i++)
        {
            if (PlayerPrefs.GetInt("Badge_" + i, 0) == 1)
            {
                badgeImages[i].sprite = colorfulBadges[i];
            }
        }
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (manager != null)
        {
            if (pauseStatus)
            {
                SaveBadges();  //Uygulama duraklatýldýðýnda rozet durumu kaydedilir.
            }
            else
            {
                LoadBadges();  //Uygulama geri açýldýðýnda rozet durumu yüklenir.
            }
        }
    }
    private void OnDisable()  //Uygulama kapatýldýðýnda rozet durumu kaydedilir.
    {
        SaveBadges();
    }
}