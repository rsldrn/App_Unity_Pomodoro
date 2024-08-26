using UnityEngine;
using UnityEngine.UI;

public class BadgeManager : MonoBehaviour
{
    public Image[] badgeImages; // Rozet g�r�nt�leri
    public float[] badgeThresholds; // Rozetlerin kilidini a�mak i�in gereken s�reler
    public Sprite[] colorfulBadges; // Renkli rozetlerin sprite'lar�

    private PomodoroManager manager;

    private void Start()
    {
        manager = PomodoroManager.Instance;  //Singleton PomodoroManager �rne�i al�n�r.

        if (manager == null)
        {
            Debug.LogError("PomodoroManager instance is not found.");
            return;
        }
        LoadBadges();  // Uygulama ba�larken �nceden kazan�lan rozetler y�klenir.
        UpdateBadges();  // Uygulama ba�larken rozetler g�ncellenir.
    }

    private void Update()
    {
        UpdateBadges();
    }

    private void UpdateBadges()
    {
        for (int i = 0; i < badgeImages.Length; i++)
        {
            if (manager.totalWorkTime >= badgeThresholds[i])  //Toplam �al��ma s�resi, rozet kilitlerini a�mak i�in gereken e�ik de�erlerini ge�ti�inde 
                                                              //rozet sprite'lar� renkli hale getirilir.
            {
                badgeImages[i].sprite = colorfulBadges[i];
            }
        }
    }
    public void SaveBadges() //A��lan rozetlerin durumu PlayerPrefs ile kaydedilir.
    {
        for (int i = 0; i < badgeImages.Length; i++)
        {
            PlayerPrefs.SetInt("Badge_" + i, manager.totalWorkTime >= badgeThresholds[i] ? 1 : 0);
        }
        PlayerPrefs.Save(); // De�i�iklikler diske at�l�r.
    }

    public void LoadBadges() //Daha �nce kazan�lm�� rozetler PlayerPrefs'ten y�klenir.
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
                SaveBadges();  //Uygulama duraklat�ld���nda rozet durumu kaydedilir.
            }
            else
            {
                LoadBadges();  //Uygulama geri a��ld���nda rozet durumu y�klenir.
            }
        }
    }
    private void OnDisable()  //Uygulama kapat�ld���nda rozet durumu kaydedilir.
    {
        SaveBadges();
    }
}