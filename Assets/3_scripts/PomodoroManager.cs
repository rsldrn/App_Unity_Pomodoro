using UnityEngine.UI;
using UnityEngine;
public class PomodoroManager : MonoBehaviour
{
    public static PomodoroManager Instance; //PomodoroManager s�n�f�n�n yaln�zca bir �rne�inin olu�turulmas� ve bu �rne�in uygulama boyunca payla��lmas� sa�lan�l�r.

    public float workTime;
    public float breakTime;
    public int cycleCount;

    public float currentTime;
    public int currentCycle;
    public bool isWorking;
    public bool timerRunning;

    public bool timerPaused; // Zamanlay�c� duraklat�l�p duraklat�lmad���n� belirleyen bool ifade.

    private PomodoroManager manager;

    public float totalWorkTime; // Toplam �al��ma s�resi.

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Sahne de�i�ti�inde bu nesnenin yok edilmemesi sa�lan�r.
        }
        else
        {
            Destroy(gameObject); //Sadece bir �rnek olmas� sa�lan�r.
        }
    }

    //Zamanlay�c� ayarlar� PlayerPrefs ile kaydedilir.
    public void SaveSettings(float workTime, float breakTime, int cycleCount)
    {
        PlayerPrefs.SetFloat("WorkTime", workTime);
        PlayerPrefs.SetFloat("BreakTime", breakTime);
        PlayerPrefs.SetInt("CycleCount", cycleCount);
        PlayerPrefs.Save();
    }

    //Zamanlay�c� ayarlar� PlayerPrefs'ten y�klenir. 
    public void LoadSettings()
    {
        workTime = PlayerPrefs.GetFloat("WorkTime", 25 * 60); // Varsay�lan 25 dakika.
        breakTime = PlayerPrefs.GetFloat("BreakTime", 5 * 60); // Varsay�lan 5 dakika.
        cycleCount = PlayerPrefs.GetInt("CycleCount", 4); // Varsay�lan 4 d�ng�.
    }

    //Zamanlay�c�n�n mevcut durumu PlayerPrefs'e kaydedilir.
    public void SaveState()
    {
        PlayerPrefs.SetFloat("CurrentTime", currentTime);
        PlayerPrefs.SetInt("CurrentCycle", currentCycle);
        PlayerPrefs.SetInt("IsWorking", isWorking ? 1 : 0);
        PlayerPrefs.SetInt("TimerRunning", timerRunning ? 1 : 0);
        PlayerPrefs.SetFloat("TotalWorkTime", totalWorkTime); // Toplam �al��ma s�resini kaydedilir.
        PlayerPrefs.Save(); // PlayerPrefs verileri diske yaz�l�r.
    }

    //Kaydedilmi� zamanlay�c� durumu PlayerPrefs'ten y�klenir.
    public void LoadState()
    {
        currentTime = PlayerPrefs.GetFloat("CurrentTime", workTime);
        currentCycle = PlayerPrefs.GetInt("CurrentCycle", 0);
        isWorking = PlayerPrefs.GetInt("IsWorking", 1) == 1;
        timerRunning = PlayerPrefs.GetInt("TimerRunning", 0) == 1;
        totalWorkTime = PlayerPrefs.GetFloat("TotalWorkTime", 0); // Toplam �al��ma s�resi y�klenir.

    }

    //Uygulama duraklat�ld���nda �al���r.
    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            // Zamanlay�c� durdurulup uygulamadan ��k�ld�ktan sonra uygulamaya geri d�n�ld���nde zamanlay�c�n�n duraklat�lm�� halden devam etmesi sa�lan�r.
            if (!timerPaused && timerRunning)
            {
                timerRunning = false;
                timerPaused = true;
            }
            LoadState(); // Kaydedilmi� zamanlay�c� durumu y�klenir.
        }
    }

    //Uygulama kapat�ld���nda �al���r.
    private void OnApplicationQuit()
    {

        SaveState();  //Zamanlay�c�n�n mevcut durumu kaydedilir.

    }
    
}