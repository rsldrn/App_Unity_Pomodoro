using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SurveyManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;   //Anket sorular�n� tutmak i�in TextMeshPro �gesi.
    public Button[] optionButtons;         //D�rt adet cevap se�ene�i i�in olu�turulmu� Button dizisi.
    public Button nextButton;              //Bir sonraki soruya ge�mek i�in olu�turulan buton.
    public Button previousButton;          //Bir �nceki soruya ge�mek i�in olu�turulan buton.
    public TextMeshProUGUI warnText;       //Kullan�c�n�n herhangi bir se�ene�i se�memesi durumunda ekrana verilen uyar� �gesi.

    public Image circleProgressBar;        //Kullan�c�n�n ka��nc� soruda oldu�unu g�rmesini sa�layan image �gesi.

    public Sprite nextArrowSprite;         //Buton resimlerinin sprite halleri.
    public Sprite checkmarkSprite;

    private int currentQuestionIndex = 0;
    private int totalScore = 0;
    private int[] selectedAnswers;

    public string[] questions = {
        "Son bir hafta i�inde kendinizi ne s�kl�kla stresli hissettiniz?",
        "Son zamanlarda ba� a�r�s�, mide problemleri, uyku sorunlar�, kas gerginli�i,  kalp �arp�nt�s�, yorgunluk gibi fiziksel belirtilerden ka� tanesini ya�ad�n�z?",
        "Konsantrasyon sorunlar� ya��yor musunuz?",
        "Son bir hafta i�inde kendinizi ne s�kl�kla huzursuz veya gergin hissettiniz?",
        "Stresli hissetti�inizde konu�abilece�iniz birine sahip misiniz?",
        "Kendinize ay�rd���n�z bo� zamanlarda rahatlamay� ba�arabiliyor musunuz?",
        "Okul/s�navlar, aile, sosyal ili�kiler, ki�isel sa�l�k, gelecek endi�esi, maddi durum gibi fakt�rlerden ka� tanesi sizi strese sokar?"
    };

    public string[][] answers = {
        new string[] { "Hi�", "Nadiren", "Bazen", "S�k s�k", "Her zaman" },
        new string[] { "Hi�biri", "1-2", "3-4", "5-6" },
        new string[] { "Hi�", "Nadiren", "Bazen", "S�k s�k", "Her zaman" },
        new string[] { "Hi�", "Nadiren", "Bazen", "S�k s�k", "Her zaman" },
        new string[] { "Evet, her zaman", "Evet, bazen", "Hay�r, nadiren", "Hay�r, hi�" },
        new string[] { "Evet, her zaman", "Evet, bazen", "Hay�r, nadiren", "Hay�r, hi�" },
        new string[] { "Hi�biri", "1-2", "3-4", "5-6" }
    };

    void Start()
    {
        selectedAnswers = new int[questions.Length];

        nextButton.onClick.AddListener(NextQuestion);   //"nextButton" butonuna her t�kland���nda, NextQuestion fonksiyonu �a��r�lmas� i�in onClick.AddListener kullan�ld�.
        previousButton.onClick.AddListener(PreviousQuestion);

        for (int i = 0; i < optionButtons.Length; i++)      //Kullan�c�n�n her bir cevap butonuna t�klad���nda SelectAnswer fonksiyonunu �a��rmak i�in d�ng� kullan�ld�.
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => SelectAnswer(index + 1));
        }

        UpdateQuestion();
        circleProgressBar.fillAmount = currentQuestionIndex / (float)questions.Length; //Kullan�c�n�n ilerlemesini g�stermek ad�na circleProgressBar ile fillAmount kullan�ld�.
    }

    void UpdateQuestion()
    {
        warnText.text = " ";
        questionText.text = questions[currentQuestionIndex];
        circleProgressBar.fillAmount = currentQuestionIndex / (float)questions.Length;

        // Her bir se�enek d��mesini kontrol etmek i�in d�ng� ba�lat�l�r.
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < answers[currentQuestionIndex].Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[currentQuestionIndex][i]; //D�ng�yle beraber se�ilen cevap textini, butonun sahip oldu�u text nesnesine atar.


                optionButtons[i].image.color = Color.white;
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }

        // Se�ilen cevab�n rengi ye�ile �evrilir.
        if (selectedAnswers[currentQuestionIndex] != 0)
        {
            optionButtons[selectedAnswers[currentQuestionIndex] - 1].image.color = Color.green;
        }

        previousButton.gameObject.SetActive(currentQuestionIndex > 0);    //�lk soruda previousButton objesinin g�sterilmemesi ad�na currentQuestionIndex > 0 ko�ulu aran�ld�.

        //Son soruda kullan�c�ya anketin bitti�ini g�stermek ad�na nextArrow �mage objesinin checkmark �mage objesine d�n���m� sa�land�.
        if (currentQuestionIndex == questions.Length - 1)
        {
            nextButton.GetComponent<Image>().sprite = checkmarkSprite;
        }
        else
        {
            nextButton.GetComponent<Image>().sprite = nextArrowSprite;
        }
    }

    void SelectAnswer(int answerIndex)
    {
        Color selectedColor = new Color(120 / 255f, 114 / 255f, 222 / 255f); // Se�ilen se�ene�in, #7872DE rengi olmas� ayarland�.

        //Kullan�c�, ayn� soruda cevap se�ene�ini de�i�tirdi�inde eski cevab�n�n puan� toplam score de�erine eklenmemesi i�in ko�ul ifadeleri kullan�ld�.
        if (selectedAnswers[currentQuestionIndex] != 0)
        {
            totalScore -= selectedAnswers[currentQuestionIndex] - 1;
        }
        selectedAnswers[currentQuestionIndex] = answerIndex;
        totalScore += answerIndex - 1;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].image.color = Color.white;
        }

        optionButtons[answerIndex - 1].image.color = selectedColor;
    }

    void NextQuestion()
    {
        if (selectedAnswers[currentQuestionIndex] == 0) //Kullan�c� herhangi bir cevab� i�aretlemedi�i s�rece sonraki soruya ge�mesi �nlenir.
        {
            warnText.text = "L�tfen bir cevap se�in.";
            Debug.LogWarning("L�tfen bir cevap se�in.");
            return;
        }

        if (currentQuestionIndex < questions.Length - 1) // E�er mevcut soru indeksi, toplam soru say�s�n�n bir eksi�inden fazla de�ilse bir sonraki soruya ge�ilir.
                                                         // Aksi halde, toplam puan totalScore de�i�kenine atan�r ve RENKRZ5.3_ssonuc sahnesine ge�i� yap�l�r.
        {
            currentQuestionIndex++;
            UpdateQuestion();
            circleProgressBar.fillAmount = currentQuestionIndex / (float)questions.Length;
        }
        else
        {
            SurveyData.totalScore = totalScore;
            SceneManager.LoadScene("RENKRZ5.3_ssonuc");
        }
    }

    void PreviousQuestion()
    {
        if (currentQuestionIndex > 0) // E�er mevcut soru indeksi s�f�rdan b�y�kse bir �nceki soruya ge�ilir.
        {
            currentQuestionIndex--;
            UpdateQuestion();
            circleProgressBar.fillAmount = currentQuestionIndex / (float)questions.Length;
        }
    }
}

