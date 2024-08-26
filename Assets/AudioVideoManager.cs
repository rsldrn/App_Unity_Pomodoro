using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioVideoManager : MonoBehaviour
{
    // Ses kaynakları
    public AudioSource lofiSource;
    public AudioSource gitarSource;
    public AudioSource suSource;
    public AudioSource yagmurSource;
    public AudioSource piyanoSource;

    // Video oynatıcıları
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public VideoPlayer videoPlayer3;
    public VideoPlayer videoPlayer4;
    public VideoPlayer videoPlayer5;

    // Video görüntüeri
    public RawImage videoRawImage1;
    public RawImage videoRawImage2;
    public RawImage videoRawImage3;
    public RawImage videoRawImage4;
    public RawImage videoRawImage5;

    private void Start()
    {
        // Başlangıçta tüm videoları gizle
        videoRawImage1.gameObject.SetActive(false);
        videoRawImage2.gameObject.SetActive(false);
        videoRawImage3.gameObject.SetActive(false);
        videoRawImage4.gameObject.SetActive(false);
        videoRawImage5.gameObject.SetActive(false);
    }

    // Lofi müziği oynat
    public void PlayLofi()
    {
        ToggleSoundAndVideo(lofiSource, videoPlayer1, videoRawImage1);
    }

    // Gitar müziğini oynat
    public void PlayGitar()
    {
        ToggleSoundAndVideo(gitarSource, videoPlayer2, videoRawImage2);
    }

    // Su müziğini oynat
    public void PlaySu()
    {
        ToggleSoundAndVideo(suSource, videoPlayer3, videoRawImage3);
    }

    // Yağmur müziğini oynat
    public void PlayYagmur()
    {
        ToggleSoundAndVideo(yagmurSource, videoPlayer4, videoRawImage4);
    }

    // Piyano müziğini oynat 
    public void PlayAtes()
    {
        ToggleSoundAndVideo(piyanoSource, videoPlayer5, videoRawImage5);
    }

    private void ToggleSoundAndVideo(AudioSource audioSource, VideoPlayer videoPlayer, RawImage videoRawImage)
    {
        if (audioSource.isPlaying)
        {
            // Ses ve video oynuyorsa durdur
            audioSource.Stop();
            videoPlayer.Stop();
            videoRawImage.gameObject.SetActive(false);
        }
        else
        {
            // Tüm sesleri ve videoları durdur
            StopAllSoundsAndVideos();
            // Yeni ses ve videoyu başlat
            audioSource.Play();
            videoRawImage.gameObject.SetActive(true);
            videoPlayer.Play();
        }
    }

    private void StopAllSoundsAndVideos()
    {
        lofiSource.Stop();
        gitarSource.Stop();
        suSource.Stop();
        yagmurSource.Stop();
        piyanoSource.Stop();

        videoPlayer1.Stop();
        videoPlayer2.Stop();
        videoPlayer3.Stop();
        videoPlayer4.Stop();
        videoPlayer5.Stop();

        videoRawImage1.gameObject.SetActive(false);
        videoRawImage2.gameObject.SetActive(false);
        videoRawImage3.gameObject.SetActive(false);
        videoRawImage4.gameObject.SetActive(false);
        videoRawImage5.gameObject.SetActive(false);
    }
}
