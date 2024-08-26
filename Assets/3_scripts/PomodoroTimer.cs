using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PomodoroTimer : MonoBehaviour
{
    public TMP_InputField workTimeInput;  //Kullan�c�dan girdi alabilmek i�in olu�turdu�umuz �geler.
    public TMP_InputField breakTimeInput;
    public TMP_InputField cycleCountInput;

    public TextMeshProUGUI timerText;   //Zamanlay�c�n�n text �gesi.
    public TextMeshProUGUI setStatusText;   //Kullan�c�n�n ders �al��ma s�resi i�inde mi yoksa mola s�resi i�erisinde mi oldu�unu g�steren text �gemiz.
    public TextMeshProUGUI errorMessageText; // Hata mesajlar� i�in TextMeshPro alan�
    public Button startButton;  //Zamanlay�c�n�n i�levselli�i i�in eklenen oynat, durdur, yenile butonlar�.
    public Button stopButton;
    public Button resetButton;
    public Button saveButton;   //Girdileri kaydetmek i�in kullan�lan buton.

    public Button switchToGameButton;  //��erik sayfas�na ge�meyi sa�layan buton.

    public Image progressBar;   //Zamanlay�c�n�n image �gesi.

    private PomodoroManager manager;  //PomodoroManager s�n�f�ndan bir �rnek olu�turuldu.

    private void Start()
    {
        manager = PomodoroManager.Instance;

        if (manager == null)
        {
            Debug.LogError("PomodoroManager instance is not found.");
            return;
        }

        startButton.onClick.AddListener(StartTimer);   //Butonlara t�klan�ld���nda ilgili fonksiyonlar�n�n �a��r�lmas�.
        stopButton.onClick.AddListener(StopTimer);
        resetButton.onClick.AddListener(ResetTimer);
        saveButton.onClick.AddListener(SaveSettings);

        workTimeInput.onValueChanged.AddListener(delegate { ValidateInputFields(); }); //Kullan�c�n� girdi�i de�erlerin ge�erlili�ine g�re buton i�levselli�ini de�i�tiren fonksiyon �a��r�l�r.
        breakTimeInput.onValueChanged.AddListener(delegate { ValidateInputFields(); });
        cycleCountInput.onValueChanged.AddListener(delegate { ValidateInputFields(); });

        //Ayarlar ve durum PlayerPrefs'ten y�klenir.
        manager.LoadSettings();
        manager.LoadState();
        //Giri� alanlar� PomodoroManager'dan y�klenir.
        LoadInputs();

        // Timer texti, progress bar�, set status texti g�ncelleyecek fonksiyonlar �a��r�l�r.
        UpdateTimerText();
        UpdateProgressBar();
        UpdateSetStatusText();

        switchToGame();  //switchToGameButton'un aktifli�ini kontrol eden fonksiyon �a�r�l�r.
    }

    private void Update()
    {
        if (manager == null)
        {
            return;
        }

        if (manager.timerRunning)
        {
            manager.currentTime -= Time.deltaTime; //Zamanlay�c� g�ncellenir.

            if (manager.isWorking)
            {
                manager.totalWorkTime += Time.deltaTime; // Toplam �al��ma s�resini g�ncellenir
                Debug.Log("toplam cal�sma s�resi: " + manager.totalWorkTime);
            }

            if (manager.currentTime <= 0)
            {
                if (manager.isWorking)
                {
                    manager.currentCycle++; //Set say�s� artt�r�ld�.
                    if (manager.currentCycle >= manager.cycleCount) //Kullan�c�n�n girdi�i set say�s� tamamland���nda ger�ekle�tirelecek i�lemler.
                    {
                        manager.timerRunning = false;
                        timerText.text = "Done!";
                        progressBar.fillAmount = 1;
                        setStatusText.text = "Completed";
                        return;
                    }
                    manager.isWorking = false;  //Mola s�resine ge�ilir.
                    manager.currentTime = manager.breakTime;
                }
                else
                {
                    manager.isWorking = true; //�al��ma zaman�na ge�ilir.
                    manager.currentTime = manager.workTime;
                }
            }

            UpdateTimerText();
            UpdateProgressBar();
            UpdateSetStatusText();

            switchToGame();
        }
    }

    private void StartTimer()  //Zamanlay�c�y� ba�latmaya veya durdurulan zamanlay�c�y� devam ettiren fonksiyon.
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

    //Zamanlay�c�y� durduran fonksiyon.
    private void StopTimer()
    {
        if (manager != null)
        {
            manager.timerRunning = false;
        }

        switchToGame();
    }

    //Zamanlay�c�y� t�m setler ile birlikte s�f�rlayan fonksiyon.
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
            ClearErrorMessage(); //Hata mesaj� temizlenir.

            switchToGame();
        }
    }

    private void SaveSettings()
    {
        if (manager != null)
        {
            if (ValidateInputs())
            {
                float workTime = float.Parse(workTimeInput.text) * 60;   //work time ve break time de�erleri saniyeye �evrilir.
                float breakTime = float.Parse(breakTimeInput.text) * 60;
                int cycles = int.Parse(cycleCountInput.text);

                manager.SaveSettings(workTime, breakTime, cycles); //Pomodoro ayarlar�n� kaydeder ve sonras�nda yeniden y�klenir.
                manager.LoadSettings();
                ResetTimer();
                EnableButtons(true); // Giri�ler ge�erli oldu�unda butonlar etkinle�tirilir.
            }
            else
            {
                EnableButtons(false); // Giri�ler ge�ersiz oldu�unda butonlar devre d��� b�rak�l�r.
            }
        }
    }

    private void LoadInputs()
    {
        if (manager != null)
        {
            workTimeInput.text = (manager.workTime / 60).ToString();    //De�erler string t�r� ifadeye �evrilerek text objelerine atan�r.
            breakTimeInput.text = (manager.breakTime / 60).ToString();
            cycleCountInput.text = manager.cycleCount.ToString();
        }
    }

    private void switchToGame()
    {
        if (!manager.isWorking)
        {
            switchToGameButton.interactable = true;   //Mola zaman� i�erisindeyse buton aktif hale gelir.
        }
        else
        {
            switchToGameButton.interactable = false;  //Ders zaman� i�erisindeyse buton inaktif hale gelir.
        }
    }

    private void ValidateInputFields()
    {
        if (ValidateInputs())
        {
            ClearErrorMessage();
            EnableButtons(true); // Giri�ler ge�erli oldu�unda butonlar etkinle�tirilir.
        }
        else
        {
            EnableButtons(false); // Giri�ler ge�ersiz oldu�unda butonlar devre d��� b�rak�l�r.
        }
    }

    private bool ValidateInputs()
    {
        ClearErrorMessage();

        float workTime;
        float breakTime;
        int cycles;

        //Girilen de�erlerin uygun olup olunmad��� kontrol edilir.
        if (!float.TryParse(workTimeInput.text, out workTime) || !float.TryParse(breakTimeInput.text, out breakTime) || !int.TryParse(cycleCountInput.text, out cycles))
        {
            ShowErrorMessage("Ge�ersiz de�er girildi. Yaln�zca say�sal de�erler kabul edilir.");
            return false;
        }

        if (workTime <= 0 || breakTime <= 0 || cycles <= 0)
        {
            ShowErrorMessage("De�erler s�f�rdan b�y�k olmal�d�r.");
            return false;
        }

        return true;
    }

    private void ShowErrorMessage(string message) //Ge�ersiz giri�ler i�in hata mesajlar�n�n verildi�i fonksiyon.
    {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
    }

    private void ClearErrorMessage()  //Hata mesajlar�n�n temizlendi�i fonksiyon.
    {
        errorMessageText.text = "";
        errorMessageText.gameObject.SetActive(false);
    }

    private void EnableButtons(bool enable)  //Butonlar�n aktifli�ini kontrol eden fonksiyon.
    {
        startButton.interactable = enable;
        stopButton.interactable = enable;
        resetButton.interactable = enable;
    }

    private void UpdateTimerText()   //Timer textinin g�ncellendi�i fonkiyon.
    {
        if (manager != null)
        {
            int minutes = Mathf.FloorToInt(manager.currentTime / 60F);          //Zamanlay�c�n�n mevcut s�resini dakika ve saniye cinsinden ayarlan�r ve string 
            int seconds = Mathf.FloorToInt(manager.currentTime % 60F);          //ifadesine �evrilerek timer textine atan�r.
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void UpdateProgressBar() //Progress bar�n g�ncellendi�i fonksiyon.
    {
        if (manager != null)
        {
            float totalTime = manager.isWorking ? manager.workTime : manager.breakTime;
            progressBar.fillAmount = manager.currentTime / totalTime;
        }
    }

    private void UpdateSetStatusText()  // Set status textinin g�ncellendi�i fonksiyon.
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
                manager.LoadState(); // Kaydedilmi� zamanlay�c� durumu y�klenir.
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
            manager.SaveState();  //Uygulamadan ��k�ld���nda zamanlay�c�n�n mevcut durumu PlayerPref ile kaydedilir.
 �������}
    }
}


