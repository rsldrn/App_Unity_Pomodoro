using UnityEngine.UI;
using UnityEngine;
public class PomodoroManager : MonoBehaviour
{
    public static PomodoroManager Instance; //PomodoroManager sýnýfýnýn yalnýzca bir örneðinin oluþturulmasý ve bu örneðin uygulama boyunca paylaþýlmasý saðlanýlýr.

    public float workTime;
    public float breakTime;
    public int cycleCount;

    public float currentTime;
    public int currentCycle;
    public bool isWorking;
    public bool timerRunning;

    public bool timerPaused; // Zamanlayýcý duraklatýlýp duraklatýlmadýðýný belirleyen bool ifade.

    private PomodoroManager manager;

    public float totalWorkTime; // Toplam çalýþma süresi.

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Sahne deðiþtiðinde bu nesnenin yok edilmemesi saðlanýr.
        }
        else
        {
            Destroy(gameObject); //Sadece bir örnek olmasý saðlanýr.
        }
    }

    //Zamanlayýcý ayarlarý PlayerPrefs ile kaydedilir.
    public void SaveSettings(float workTime, float breakTime, int cycleCount)
    {
        PlayerPrefs.SetFloat("WorkTime", workTime);
        PlayerPrefs.SetFloat("BreakTime", breakTime);
        PlayerPrefs.SetInt("CycleCount", cycleCount);
        PlayerPrefs.Save();
    }

    //Zamanlayýcý ayarlarý PlayerPrefs'ten yüklenir. 
    public void LoadSettings()
    {
        workTime = PlayerPrefs.GetFloat("WorkTime", 25 * 60); // Varsayýlan 25 dakika.
        breakTime = PlayerPrefs.GetFloat("BreakTime", 5 * 60); // Varsayýlan 5 dakika.
        cycleCount = PlayerPrefs.GetInt("CycleCount", 4); // Varsayýlan 4 döngü.
    }

    //Zamanlayýcýnýn mevcut durumu PlayerPrefs'e kaydedilir.
    public void SaveState()
    {
        PlayerPrefs.SetFloat("CurrentTime", currentTime);
        PlayerPrefs.SetInt("CurrentCycle", currentCycle);
        PlayerPrefs.SetInt("IsWorking", isWorking ? 1 : 0);
        PlayerPrefs.SetInt("TimerRunning", timerRunning ? 1 : 0);
        PlayerPrefs.SetFloat("TotalWorkTime", totalWorkTime); // Toplam çalýþma süresini kaydedilir.
        PlayerPrefs.Save(); // PlayerPrefs verileri diske yazýlýr.
    }

    //Kaydedilmiþ zamanlayýcý durumu PlayerPrefs'ten yüklenir.
    public void LoadState()
    {
        currentTime = PlayerPrefs.GetFloat("CurrentTime", workTime);
        currentCycle = PlayerPrefs.GetInt("CurrentCycle", 0);
        isWorking = PlayerPrefs.GetInt("IsWorking", 1) == 1;
        timerRunning = PlayerPrefs.GetInt("TimerRunning", 0) == 1;
        totalWorkTime = PlayerPrefs.GetFloat("TotalWorkTime", 0); // Toplam çalýþma süresi yüklenir.

    }

    //Uygulama duraklatýldýðýnda çalýþýr.
    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            // Zamanlayýcý durdurulup uygulamadan çýkýldýktan sonra uygulamaya geri dönüldüðünde zamanlayýcýnýn duraklatýlmýþ halden devam etmesi saðlanýr.
            if (!timerPaused && timerRunning)
            {
                timerRunning = false;
                timerPaused = true;
            }
            LoadState(); // Kaydedilmiþ zamanlayýcý durumu yüklenir.
        }
    }

    //Uygulama kapatýldýðýnda çalýþýr.
    private void OnApplicationQuit()
    {

        SaveState();  //Zamanlayýcýnýn mevcut durumu kaydedilir.

    }
    
}