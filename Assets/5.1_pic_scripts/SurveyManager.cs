using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SurveyManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;   //Anket sorularýný tutmak için TextMeshPro ögesi.
    public Button[] optionButtons;         //Dört adet cevap seçeneði için oluþturulmuþ Button dizisi.
    public Button nextButton;              //Bir sonraki soruya geçmek için oluþturulan buton.
    public Button previousButton;          //Bir önceki soruya geçmek için oluþturulan buton.
    public TextMeshProUGUI warnText;       //Kullanýcýnýn herhangi bir seçeneði seçmemesi durumunda ekrana verilen uyarý ögesi.

    public Image circleProgressBar;        //Kullanýcýnýn kaçýncý soruda olduðunu görmesini saðlayan image ögesi.

    public Sprite nextArrowSprite;         //Buton resimlerinin sprite halleri.
    public Sprite checkmarkSprite;

    private int currentQuestionIndex = 0;
    private int totalScore = 0;
    private int[] selectedAnswers;

    public string[] questions = {
        "Son bir hafta içinde kendinizi ne sýklýkla stresli hissettiniz?",
        "Son zamanlarda baþ aðrýsý, mide problemleri, uyku sorunlarý, kas gerginliði,  kalp çarpýntýsý, yorgunluk gibi fiziksel belirtilerden kaç tanesini yaþadýnýz?",
        "Konsantrasyon sorunlarý yaþýyor musunuz?",
        "Son bir hafta içinde kendinizi ne sýklýkla huzursuz veya gergin hissettiniz?",
        "Stresli hissettiðinizde konuþabileceðiniz birine sahip misiniz?",
        "Kendinize ayýrdýðýnýz boþ zamanlarda rahatlamayý baþarabiliyor musunuz?",
        "Okul/sýnavlar, aile, sosyal iliþkiler, kiþisel saðlýk, gelecek endiþesi, maddi durum gibi faktörlerden kaç tanesi sizi strese sokar?"
    };

    public string[][] answers = {
        new string[] { "Hiç", "Nadiren", "Bazen", "Sýk sýk", "Her zaman" },
        new string[] { "Hiçbiri", "1-2", "3-4", "5-6" },
        new string[] { "Hiç", "Nadiren", "Bazen", "Sýk sýk", "Her zaman" },
        new string[] { "Hiç", "Nadiren", "Bazen", "Sýk sýk", "Her zaman" },
        new string[] { "Evet, her zaman", "Evet, bazen", "Hayýr, nadiren", "Hayýr, hiç" },
        new string[] { "Evet, her zaman", "Evet, bazen", "Hayýr, nadiren", "Hayýr, hiç" },
        new string[] { "Hiçbiri", "1-2", "3-4", "5-6" }
    };

    void Start()
    {
        selectedAnswers = new int[questions.Length];

        nextButton.onClick.AddListener(NextQuestion);   //"nextButton" butonuna her týklandýðýnda, NextQuestion fonksiyonu çaðýrýlmasý için onClick.AddListener kullanýldý.
        previousButton.onClick.AddListener(PreviousQuestion);

        for (int i = 0; i < optionButtons.Length; i++)      //Kullanýcýnýn her bir cevap butonuna týkladýðýnda SelectAnswer fonksiyonunu çaðýrmak için döngü kullanýldý.
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => SelectAnswer(index + 1));
        }

        UpdateQuestion();
        circleProgressBar.fillAmount = currentQuestionIndex / (float)questions.Length; //Kullanýcýnýn ilerlemesini göstermek adýna circleProgressBar ile fillAmount kullanýldý.
    }

    void UpdateQuestion()
    {
        warnText.text = " ";
        questionText.text = questions[currentQuestionIndex];
        circleProgressBar.fillAmount = currentQuestionIndex / (float)questions.Length;

        // Her bir seçenek düðmesini kontrol etmek için döngü baþlatýlýr.
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < answers[currentQuestionIndex].Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[currentQuestionIndex][i]; //Döngüyle beraber seçilen cevap textini, butonun sahip olduðu text nesnesine atar.


                optionButtons[i].image.color = Color.white;
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }

        // Seçilen cevabýn rengi yeþile çevrilir.
        if (selectedAnswers[currentQuestionIndex] != 0)
        {
            optionButtons[selectedAnswers[currentQuestionIndex] - 1].image.color = Color.green;
        }

        previousButton.gameObject.SetActive(currentQuestionIndex > 0);    //Ýlk soruda previousButton objesinin gösterilmemesi adýna currentQuestionIndex > 0 koþulu aranýldý.

        //Son soruda kullanýcýya anketin bittiðini göstermek adýna nextArrow ýmage objesinin checkmark ýmage objesine dönüþümü saðlandý.
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
        Color selectedColor = new Color(120 / 255f, 114 / 255f, 222 / 255f); // Seçilen seçeneðin, #7872DE rengi olmasý ayarlandý.

        //Kullanýcý, ayný soruda cevap seçeneðini deðiþtirdiðinde eski cevabýnýn puaný toplam score deðerine eklenmemesi için koþul ifadeleri kullanýldý.
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
        if (selectedAnswers[currentQuestionIndex] == 0) //Kullanýcý herhangi bir cevabý iþaretlemediði sürece sonraki soruya geçmesi önlenir.
        {
            warnText.text = "Lütfen bir cevap seçin.";
            Debug.LogWarning("Lütfen bir cevap seçin.");
            return;
        }

        if (currentQuestionIndex < questions.Length - 1) // Eðer mevcut soru indeksi, toplam soru sayýsýnýn bir eksiðinden fazla deðilse bir sonraki soruya geçilir.
                                                         // Aksi halde, toplam puan totalScore deðiþkenine atanýr ve RENKRZ5.3_ssonuc sahnesine geçiþ yapýlýr.
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
        if (currentQuestionIndex > 0) // Eðer mevcut soru indeksi sýfýrdan büyükse bir önceki soruya geçilir.
        {
            currentQuestionIndex--;
            UpdateQuestion();
            circleProgressBar.fillAmount = currentQuestionIndex / (float)questions.Length;
        }
    }
}

