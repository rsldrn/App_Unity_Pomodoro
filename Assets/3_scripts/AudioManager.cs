using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource lofiAudio;
    public AudioSource guitarAudio;
    public AudioSource waterAudio;
    public AudioSource rainAudio;
    public AudioSource fireAudio;

    public Button lofiButton;
    public Button guitarButton;
    public Button waterButton;
    public Button rainButton;
    public Button fireButton;

    // Audio flagleri
    private bool isLofiPlaying = false;
    private bool isGuitarPlaying = false;
    private bool isWaterPlaying = false;
    private bool isRainPlaying = false;
    private bool isFirePlaying = false;

    private void Start()
    {
        lofiButton.onClick.AddListener(() => ToggleAudio(lofiAudio, ref isLofiPlaying));
        guitarButton.onClick.AddListener(() => ToggleAudio(guitarAudio, ref isGuitarPlaying));
        waterButton.onClick.AddListener(() => ToggleAudio(waterAudio, ref isWaterPlaying));
        rainButton.onClick.AddListener(() => ToggleAudio(rainAudio, ref isRainPlaying));
        fireButton.onClick.AddListener(() => ToggleAudio(fireAudio, ref isFirePlaying));
    }

    private void ToggleAudio(AudioSource audio, ref bool isPlaying)
    {
        if (isPlaying)
        {
            // Ses çalýyorsa durdur
            audio.Pause();
        }
        else
        {
            // Tüm sesleri durdur
            StopAllAudio();

            // Seçilen sesi oynat
            audio.Play();
        }

        // Çalma durumunu tersine çevir
        isPlaying = !isPlaying;
    }

    private void StopAllAudio()
    {
        lofiAudio.Stop();
        guitarAudio.Stop();
        waterAudio.Stop();
        rainAudio.Stop();
        fireAudio.Stop();

        // Tüm çalma durumlarýný sýfýrla
        isLofiPlaying = false;
        isGuitarPlaying = false;
        isWaterPlaying = false;
        isRainPlaying = false;
        isFirePlaying = false;
    }
}