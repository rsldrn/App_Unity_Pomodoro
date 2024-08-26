
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TrueFalseGame : MonoBehaviour
{
    public TMP_Text titleText;
    public Button playButton;
    public Button exitButton;
    public TMP_Text questionText;
    public Button trueButton;
    public Button falseButton;
    public TMP_Text scoreText;
    public TMP_Text gameOverText;
    public TMP_Text warnText;
    public TMP_Text questCountText;
    public Image correctImage; // Doðru cevap görseli
    public Image incorrectImage; // Yanlýþ cevap görseli
    public Image questFrame; // Sorularýn olduðu çerçeve
    public Button nextButton;

    private int score = 0;
    private string[] questions = { "Enzimler biyokimyasal reaksiyonlarý baþlatýr.",
                                   "Osmanlý Devleti'nde \"Pençik Sistemi\"nin uygulanmasýndaki temel amaç devlete sadýk kiþilerden oluþan bir ordu kurmaktýr.",
                                   "Nötr halden anyona dönüþen bir taneciðin toplam tanecik sayýsý artar.",
                                   "Karahanlýlarýn Doðu-Batý Karahanlýlar olarak ikiye ayrýlmasý Haçlý Seferlerinin bir sonucudur.",
                                   "Etki-tepki kuvvetleri sadece temas gerektiren kuvvetler için geçerlidir.",
                                   "Türkiye,Ekvator üzerinde bir konuma getirilseydi yer þekilleri deðiþmezdi.",
                                   "Etçillerin baðýrsaklarýnda selüloz sindiren bakteriler bulunur",
                                   "\"Saðlýk raporu baþvurularýný kurul deðerlendirecek\" cümlesinde topluluk adý kullanýlmýþtýr",
                                   "Sentriollerin eþlenmesi bitki hücresinin hücre döngüsünde görülen bir olaydýr.",
                                   "Sýcaklýk azaldýkça gaz moleküllerinin kinetik enerjisi azalýr.",
                                   "Ýslam dinine göre insanlara verilmiþ olan kaza-kader sýnýrlarý çerçevesinde hareket imkaný tanýyan özgür irade \"Külli Ýrade\"dir.",
                                   "Moleküler kristallerin ayný koþullarda erime noktasý iyonik kristallerinkinden düþüktür.",
                                   "Sürtünmeli bir yüzeyde atýlan cismin yavaþlamasý dengelenmiþ kuvvet etkisinde olduðunu gösterir.",
                                   "Filogenetik sýnýflandýrmada ayný takýmda olduðu bilinen canlýlarýn þubeleri farklý olabilir.",
                                   "\"Araba hýzýný alamamýþ,ilerideki kanala uçmuþ.\" baðýmlý sýralý bir cümledir."
    };
    private bool[] answers = { false, true, true, false, false, true, false, true, false, true, false, true, false, false, true };
    private int currentQuestionIndex = 0;

    private List<int> questionIndices = new List<int>();
    private bool hasAnswered = false; // Kullanýcýnýn cevap verip vermediðini izlemek için

    void Start()
    {
        ShowStartScreen();
        nextButton.onClick.AddListener(LoadNextQuestionOnClick);
    }

    public void ShowStartScreen()
    {
        titleText.text = "True          False";
        playButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        questionText.gameObject.SetActive(false);
        questCountText.gameObject.SetActive(false);
        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        correctImage.gameObject.SetActive(false);
        incorrectImage.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        questFrame.gameObject.SetActive(false);
        warnText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        playButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        questionText.gameObject.SetActive(true);
        questCountText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        trueButton.gameObject.SetActive(true);
        falseButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        questFrame.gameObject.SetActive(true);

        score = 0;
        currentQuestionIndex = 0;

        questCountText.text = (currentQuestionIndex + 1) + "/15";
        scoreText.text = "Skor: " + score;
        ShuffleQuestions();
        LoadNextQuestion();
    }

    void ShuffleQuestions()
    {
        questionIndices.Clear();
        for (int i = 0; i < questions.Length; i++)
        {
            questionIndices.Add(i);
        }
        for (int i = 0; i < questionIndices.Count; i++)
        {
            int temp = questionIndices[i];
            int randomIndex = Random.Range(i, questionIndices.Count);
            questionIndices[i] = questionIndices[randomIndex];
            questionIndices[randomIndex] = temp;
        }
    }

    void LoadNextQuestion()
    {
        
        correctImage.gameObject.SetActive(false);
        incorrectImage.gameObject.SetActive(false);
        trueButton.interactable = true;
        falseButton.interactable = true;
        nextButton.interactable = true;
        hasAnswered = false; // Kullanýcý henüz cevap vermedi


        if (currentQuestionIndex < questionIndices.Count)
        {
            questCountText.text = (currentQuestionIndex + 1) + "/15";
            questionText.text = questions[questionIndices[currentQuestionIndex]];
            warnText.gameObject.SetActive(false);

        }
        else
        {

            gameOverText.text = "Oyun Bitti!\nSkor: " + score;

            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            questFrame.gameObject.SetActive(false);
            questionText.gameObject.SetActive(false);
            questCountText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(true);
            playButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
    }

    public void Answer(bool isTrue)
    {
        if (hasAnswered)
            return;

        hasAnswered = true; // Kullanýcý cevap verdi

        if (answers[questionIndices[currentQuestionIndex]] == isTrue)
        {
            score++;
            scoreText.text = "Skor: " + score;
            ShowCorrectFeedback();
        }
        else
        {
            ShowIncorrectFeedback();
        }

        trueButton.interactable = false;
        falseButton.interactable = false;
        nextButton.interactable = true;
        warnText.gameObject.SetActive(false);
    }

    void ShowCorrectFeedback()
    {
        correctImage.gameObject.SetActive(true);
    }

    void ShowIncorrectFeedback()
    {
        incorrectImage.gameObject.SetActive(true);
    }

    void LoadNextQuestionOnClick()
    {
        if (!hasAnswered)
        {
            warnText.text = "Lütfen bir cevap seçin.";
            warnText.gameObject.SetActive(true);
            nextButton.interactable = false;
            return;
        }
        currentQuestionIndex++;
        LoadNextQuestion();
    }
}
