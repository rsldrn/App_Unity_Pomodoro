
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
    public Image correctImage; // Do�ru cevap g�rseli
    public Image incorrectImage; // Yanl�� cevap g�rseli
    public Image questFrame; // Sorular�n oldu�u �er�eve
    public Button nextButton;

    private int score = 0;
    private string[] questions = { "Enzimler biyokimyasal reaksiyonlar� ba�lat�r.",
                                   "Osmanl� Devleti'nde \"Pen�ik Sistemi\"nin uygulanmas�ndaki temel ama� devlete sad�k ki�ilerden olu�an bir ordu kurmakt�r.",
                                   "N�tr halden anyona d�n��en bir taneci�in toplam tanecik say�s� artar.",
                                   "Karahanl�lar�n Do�u-Bat� Karahanl�lar olarak ikiye ayr�lmas� Ha�l� Seferlerinin bir sonucudur.",
                                   "Etki-tepki kuvvetleri sadece temas gerektiren kuvvetler i�in ge�erlidir.",
                                   "T�rkiye,Ekvator �zerinde bir konuma getirilseydi yer �ekilleri de�i�mezdi.",
                                   "Et�illerin ba��rsaklar�nda sel�loz sindiren bakteriler bulunur",
                                   "\"Sa�l�k raporu ba�vurular�n� kurul de�erlendirecek\" c�mlesinde topluluk ad� kullan�lm��t�r",
                                   "Sentriollerin e�lenmesi bitki h�cresinin h�cre d�ng�s�nde g�r�len bir olayd�r.",
                                   "S�cakl�k azald�k�a gaz molek�llerinin kinetik enerjisi azal�r.",
                                   "�slam dinine g�re insanlara verilmi� olan kaza-kader s�n�rlar� �er�evesinde hareket imkan� tan�yan �zg�r irade \"K�lli �rade\"dir.",
                                   "Molek�ler kristallerin ayn� ko�ullarda erime noktas� iyonik kristallerinkinden d���kt�r.",
                                   "S�rt�nmeli bir y�zeyde at�lan cismin yava�lamas� dengelenmi� kuvvet etkisinde oldu�unu g�sterir.",
                                   "Filogenetik s�n�fland�rmada ayn� tak�mda oldu�u bilinen canl�lar�n �ubeleri farkl� olabilir.",
                                   "\"Araba h�z�n� alamam��,ilerideki kanala u�mu�.\" ba��ml� s�ral� bir c�mledir."
    };
    private bool[] answers = { false, true, true, false, false, true, false, true, false, true, false, true, false, false, true };
    private int currentQuestionIndex = 0;

    private List<int> questionIndices = new List<int>();
    private bool hasAnswered = false; // Kullan�c�n�n cevap verip vermedi�ini izlemek i�in

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
        hasAnswered = false; // Kullan�c� hen�z cevap vermedi


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

        hasAnswered = true; // Kullan�c� cevap verdi

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
            warnText.text = "L�tfen bir cevap se�in.";
            warnText.gameObject.SetActive(true);
            nextButton.interactable = false;
            return;
        }
        currentQuestionIndex++;
        LoadNextQuestion();
    }
}
