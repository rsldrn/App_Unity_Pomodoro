using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PomodoroTimer : MonoBehaviour
{
    public TMP_InputField workTimeInput;  //Kullanýcýdan girdi alabilmek için oluþturduðumuz ögeler.
    public TMP_InputField breakTimeInput;
    public TMP_InputField cycleCountInput;

    public TextMeshProUGUI timerText;   //Zamanlayýcýnýn text ögesi.
    public TextMeshProUGUI setStatusText;   //Kullanýcýnýn ders çalýþma süresi içinde mi yoksa mola süresi içerisinde mi olduðunu gösteren text ögemiz.
    public TextMeshProUGUI errorMessageText; // Hata mesajlarý için TextMeshPro alaný
    public Button startButton;  //Zamanlayýcýnýn iþlevselliði için eklenen oynat, durdur, yenile butonlarý.
    public Button stopButton;
    public Button resetButton;
    public Button saveButton;   //Girdileri kaydetmek için kullanýlan buton.

    public Button switchToGameButton;  //Ýçerik sayfasýna geçmeyi saðlayan buton.

    public Image progressBar;   //Zamanlayýcýnýn image ögesi.

    private PomodoroManager manager;  //PomodoroManager sýnýfýndan bir örnek oluþturuldu.

    private void Start()
    {
        manager = PomodoroManager.Instance;

        if (manager == null)
        {
            Debug.LogError("PomodoroManager instance is not found.");
            return;
        }

        startButton.onClick.AddListener(StartTimer);   //Butonlara týklanýldýðýnda ilgili fonksiyonlarýnýn çaðýrýlmasý.
        stopButton.onClick.AddListener(StopTimer);
        resetButton.onClick.AddListener(ResetTimer);
        saveButton.onClick.AddListener(SaveSettings);

        workTimeInput.onValueChanged.AddListener(delegate { ValidateInputFields(); }); //Kullanýcýný girdiði deðerlerin geçerliliðine göre buton iþlevselliðini deðiþtiren fonksiyon çaðýrýlýr.
        breakTimeInput.onValueChanged.AddListener(delegate { ValidateInputFields(); });
        cycleCountInput.onValueChanged.AddListener(delegate { ValidateInputFields(); });

        //Ayarlar ve durum PlayerPrefs'ten yüklenir.
        manager.LoadSettings();
        manager.LoadState();
        //Giriþ alanlarý PomodoroManager'dan yüklenir.
        LoadInputs();

        // Timer texti, progress barý, set status texti güncelleyecek fonksiyonlar çaðýrýlýr.
        UpdateTimerText();
        UpdateProgressBar();
        UpdateSetStatusText();

        switchToGame();  //switchToGameButton'un aktifliðini kontrol eden fonksiyon çaðrýlýr.
    }

    private void Update()
    {
        if (manager == null)
        {
            return;
        }

        if (manager.timerRunning)
        {
            manager.currentTime -= Time.deltaTime; //Zamanlayýcý güncellenir.

            if (manager.isWorking)
            {
                manager.totalWorkTime += Time.deltaTime; // Toplam çalýþma süresini güncellenir
                Debug.Log("toplam calýsma süresi: " + manager.totalWorkTime);
            }

            if (manager.currentTime <= 0)
            {
                if (manager.isWorking)
                {
                    manager.currentCycle++; //Set sayýsý arttýrýldý.
                    if (manager.currentCycle >= manager.cycleCount) //Kullanýcýnýn girdiði set sayýsý tamamlandýðýnda gerçekleþtirelecek iþlemler.
                    {
                        manager.timerRunning = false;
                        timerText.text = "Done!";
                        progressBar.fillAmount = 1;
                        setStatusText.text = "Completed";
                        return;
                    }
                    manager.isWorking = false;  //Mola süresine geçilir.
                    manager.currentTime = manager.breakTime;
                }
                else
                {
                    manager.isWorking = true; //Çalýþma zamanýna geçilir.
                    manager.currentTime = manager.workTime;
                }
            }

            UpdateTimerText();
            UpdateProgressBar();
            UpdateSetStatusText();

            switchToGame();
        }
    }

    private void StartTimer()  //Zamanlayýcýyý baþlatmaya veya durdurulan zamanlayýcýyý devam ettiren fonksiyon.
    {
        if (manager != null && !manager.timerRunning)
        {
            if (manager.currentTime <= 0)
            {
                manager.currentTime = manager.isWorking ? manager.workTime : manager.breakTime;
            }
            manager.timerRunning = true;
            UpdateProgressBar();
            UpdateSetStatusText();

            switchToGame();
        }
    }

    //Zamanlayýcýyý durduran fonksiyon.
    private void StopTimer()
    {
        if (manager != null)
        {
            manager.timerRunning = false;
        }

        switchToGame();
    }

    //Zamanlayýcýyý tüm setler ile birlikte sýfýrlayan fonksiyon.
    private void ResetTimer()
    {
        if (manager != null)
        {
            manager.timerRunning = false;
            manager.currentTime = manager.workTime;
            manager.currentCycle = 0;
            manager.isWorking = true;
            UpdateTimerText();
            UpdateProgressBar();
            UpdateSetStatusText();
            UpdateSetStatusText();
            ClearErrorMessage(); //Hata mesajý temizlenir.

            switchToGame();
        }
    }

    private void SaveSettings()
    {
        if (manager != null)
        {
            if (ValidateInputs())
            {
                float workTime = float.Parse(workTimeInput.text) * 60;   //work time ve break time deðerleri saniyeye çevrilir.
                float breakTime = float.Parse(breakTimeInput.text) * 60;
                int cycles = int.Parse(cycleCountInput.text);

                manager.SaveSettings(workTime, breakTime, cycles); //Pomodoro ayarlarýný kaydeder ve sonrasýnda yeniden yüklenir.
                manager.LoadSettings();
                ResetTimer();
                EnableButtons(true); // Giriþler geçerli olduðunda butonlar etkinleþtirilir.
            }
            else
            {
                EnableButtons(false); // Giriþler geçersiz olduðunda butonlar devre dýþý býrakýlýr.
            }
        }
    }

    private void LoadInputs()
    {
        if (manager != null)
        {
            workTimeInput.text = (manager.workTime / 60).ToString();    //Deðerler string türü ifadeye çevrilerek text objelerine atanýr.
            breakTimeInput.text = (manager.breakTime / 60).ToString();
            cycleCountInput.text = manager.cycleCount.ToString();
        }
    }

    private void switchToGame()
    {
        if (!manager.isWorking)
        {
            switchToGameButton.interactable = true;   //Mola zamaný içerisindeyse buton aktif hale gelir.
        }
        else
        {
            switchToGameButton.interactable = false;  //Ders zamaný içerisindeyse buton inaktif hale gelir.
        }
    }

    private void ValidateInputFields()
    {
        if (ValidateInputs())
        {
            ClearErrorMessage();
            EnableButtons(true); // Giriþler geçerli olduðunda butonlar etkinleþtirilir.
        }
        else
        {
            EnableButtons(false); // Giriþler geçersiz olduðunda butonlar devre dýþý býrakýlýr.
        }
    }

    private bool ValidateInputs()
    {
        ClearErrorMessage();

        float workTime;
        float breakTime;
        int cycles;

        //Girilen deðerlerin uygun olup olunmadýðý kontrol edilir.
        if (!float.TryParse(workTimeInput.text, out workTime) || !float.TryParse(breakTimeInput.text, out breakTime) || !int.TryParse(cycleCountInput.text, out cycles))
        {
            ShowErrorMessage("Geçersiz deðer girildi. Yalnýzca sayýsal deðerler kabul edilir.");
            return false;
        }

        if (workTime <= 0 || breakTime <= 0 || cycles <= 0)
        {
            ShowErrorMessage("Deðerler sýfýrdan büyük olmalýdýr.");
            return false;
        }

        return true;
    }

    private void ShowErrorMessage(string message) //Geçersiz giriþler için hata mesajlarýnýn verildiði fonksiyon.
    {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
    }

    private void ClearErrorMessage()  //Hata mesajlarýnýn temizlendiði fonksiyon.
    {
        errorMessageText.text = "";
        errorMessageText.gameObject.SetActive(false);
    }

    private void EnableButtons(bool enable)  //Butonlarýn aktifliðini kontrol eden fonksiyon.
    {
        startButton.interactable = enable;
        stopButton.interactable = enable;
        resetButton.interactable = enable;
    }

    private void UpdateTimerText()   //Timer textinin güncellendiði fonkiyon.
    {
        if (manager != null)
        {
            int minutes = Mathf.FloorToInt(manager.currentTime / 60F);          //Zamanlayýcýnýn mevcut süresini dakika ve saniye cinsinden ayarlanýr ve string 
            int seconds = Mathf.FloorToInt(manager.currentTime % 60F);          //ifadesine çevrilerek timer textine atanýr.
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void UpdateProgressBar() //Progress barýn güncellendiði fonksiyon.
    {
        if (manager != null)
        {
            float totalTime = manager.isWorking ? manager.workTime : manager.breakTime;
            progressBar.fillAmount = manager.currentTime / totalTime;
        }
    }

    private void UpdateSetStatusText()  // Set status textinin güncellendiði fonksiyon.
    {
        if (manager != null)
        {
            string status = manager.isWorking ? "DERS" : "MOLA";
            string setText = (manager.currentCycle == 0 && !manager.timerRunning && !manager.isWorking)
                ? "HAZIRSAN BASLA"
                : $"{manager.currentCycle + (manager.isWorking ? 1 : 0)}. SET {status}";

            setStatusText.text = setText;
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (manager != null)
        {
            if (pauseStatus)
            {
                manager.LoadState(); // Kaydedilmiþ zamanlayýcý durumu yüklenir.
                UpdateTimerText();
                UpdateProgressBar();
                UpdateSetStatusText();
            }
        }
    }

    private void OnDisable()
    {
        if (manager != null)
        {
            manager.SaveState();  //Uygulamadan çýkýldýðýnda zamanlayýcýnýn mevcut durumu PlayerPref ile kaydedilir.
        }
    }
}


