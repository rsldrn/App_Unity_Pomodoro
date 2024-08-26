using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Oyuncu ve UI elemanları referansları
    public Player player;
    public Text scoreText;
    public Text bestScoreText; // En iyi skoru gösterecek Text
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject exitButton;

    // Oyun skorları
    private int score;
    private int bestScore;

    private void Awake()
    {
        // Uygulama hedef kare hızını ayarla
        Application.targetFrameRate = 60;

        // Game Over ve Exit butonlarını devre dışı bırak
        gameOver.SetActive(false);
        exitButton.SetActive(true);

        // En iyi skoru yükle
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best: " + bestScore;

        // Oyunu durdur
        Pause();
    }

    // Oyunu başlat
    public void Play()
    {
        // Skoru sıfırla ve güncelle
        score = 0;
        scoreText.text = score.ToString();

        // Butonları ve Game Over ekranını gizle
        playButton.SetActive(false);
        gameOver.SetActive(false);
        exitButton.SetActive(false);

        // Oyunu başlat
        Time.timeScale = 1f;
        player.enabled = true;

        // Tüm boruları bul ve yok et
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    // Oyun bitiş işlemleri
    public void GameOver()
    {
        // Butonları ve Game Over ekranını göster
        playButton.SetActive(true);
        gameOver.SetActive(true);
        exitButton.SetActive(true);

        // En iyi skoru güncelle
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        // En iyi skoru ekranda güncelle
        bestScoreText.text = "Best: " + bestScore;
        
        // Oyunu durdur
        Pause();
    }

    // Oyunu durdur
    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    // Skoru arttır ve ekranda güncelle
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
