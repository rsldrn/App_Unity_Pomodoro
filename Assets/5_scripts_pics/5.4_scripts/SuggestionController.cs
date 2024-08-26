using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SuggestionController : MonoBehaviour
{
    public TextMeshProUGUI stressLevelText; // Stres d�zeyini g�steren text.
    public TextMeshProUGUI suggestionText; // Stres seviyesinin durumuna g�re kullan�c�ya g�sterilen �neri texti.
    public Button refreshButton; // Ayn� stres seviyesi i�in �nerileri yenileyen buton.

    private string[][] suggestions = new string[][]
    {
        new string[] // D���k stres seviyesi i�in �neriler.
        {
            "Egzersiz Yap�n: Hafif tempolu y�r�y�� veya yoga gibi aktiviteler stres hormonlar�n� azalt�r ve ruh halinizi iyile�tirir.",
            "Meditasyon ve Derin Nefes Alma Egzersizleri: Bu teknikler sakinle�meye yard�mc� olur ve stresi azalt�r.",
            "Do�ada Zaman Ge�irin: Do�ada vakit ge�irmek zihinsel sa�l�k �zerinde olumlu etkiler yapar.",
            "Sanatsal Aktiviteler: Resim yapmak, m�zik dinlemek veya enstr�man �almak zihninizi me�gul ederek stresi azalt�r.",
            "Dengeli Beslenme: Sa�l�kl� bir diyet stresi y�netmede �nemli bir rol oynar. �rne�in, avokado ve ceviz gibi besinler ruh halini d�zenler.",
            "G�nl�k Tutma: Duygular�n�z� yaz�ya d�kmek stresin etkilerini hafifletebilir.",
            "Hobilerle U�ra��n: Keyif ald���n�z aktivitelerle me�gul olmak stres seviyenizi d���k tutar.",
            "K�sa Molalar Verin: D�zenli aral�klarla k�sa molalar vermek zihinsel yorgunlu�u azalt�r."
        },
        new string[] // Orta stres seviyesi i�in �neriler.
        {
            "D�zenli Uyku: Yeterli uyku almak, v�cudunuzu ve zihninizi yenileyerek stresle ba�a ��kman�za yard�mc� olur.",
            "Fiziksel Aktiviteyi Artt�r�n: Ko�u, y�zme gibi daha yo�un egzersizler stres hormonlar�n� azalt�r ve endorfin salg�lar.",
            "Sosyal Ba�lant�lar: Aile ve arkada�larla zaman ge�irmek, destek sisteminizi g��lendirir ve stresle ba�a ��kmay� kolayla�t�r�r.",
            "Biofeedback ve Gev�eme Teknikleri: V�cut fonksiyonlar�n� kontrol etmeyi ��renmek stres y�netiminde etkili olabilir.",
            "Rutin Olu�turma: G�nl�k bir rutin olu�turmak, belirsizlikleri azalt�r ve stresi kontrol alt�nda tutar.",
            "Hafif Streching ve Yoga: Bu aktiviteler fiziksel ve zihinsel gev�emeyi te�vik eder.",
            "Destek Gruplar�na Kat�l�n: Benzer deneyimlere sahip insanlarla ileti�im kurmak, stresin etkilerini hafifletebilir.",
            "Kendinize Zaman Ay�r�n: G�n i�inde kendinize dinlenme ve yenilenme zaman� ay�rmak stresle ba�a ��kman�za yard�mc� olur."
        },
        new string[] // Y�ksek stres seviyesi i�in �neriler.
        {
            "Profesyonel Yard�m Al�n: Terapi veya dan��manl�k, stres y�netiminde �ok etkili olabilir.",
            "Yo�un Egzersiz: Daha yo�un fiziksel aktiviteler (�rne�in, a��rl�k kald�rma) stresi azaltmada etkili olabilir.",
            "Mindfulness ve Meditasyon: Bu teknikler, y�ksek stres seviyelerinde bile zihninizi sakinle�tirir.",
            "Derin Nefes Alma Egzersizleri: Nefes egzersizleri v�cut ve zihin �zerinde h�zl� bir rahatlama etkisi yarat�r.",
            "E�lenceli Aktiviteler: Sevdi�iniz bir aktiviteyle me�gul olmak, yo�un stres alt�nda bile rahatlaman�za yard�mc� olabilir.",
            "Kafein ve Alkol T�ketimini Azalt�n: Bu maddeler stres seviyelerini artt�rabilir, bu y�zden t�ketimi s�n�rlamak �nemlidir.",
            "Zaman Y�netimi: G�revlerinizi �nceliklendirmek ve y�netmek, stres seviyenizi azalt�r.",
            "Stresi Azaltan Takviyeler: Melatonin, Ashwagandha ve B vitaminleri gibi takviyeler, stresle ba�a ��kmada yard�mc� olabilir."
        }
    };

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshSuggestions); // Refresh butonuna bas�ld���nda RefreshSuggestions fonksiyonu �a��r�l�r.
        DisplaySuggestions(PlayerPrefs.GetInt("HeartRate")); // Nab�z de�eri kullan�larak �neriler g�sterilir.

    }

    void DisplaySuggestions(float heartRate) // Anket sonucuna g�re stres seviyesi text'inde ne yazaca��n� belirleyen fonksiyon.
    {
        if (heartRate < 50)
        {
            suggestionText.text = "�l��m esnas�nda parma��n�z� kald�rmay�n ve tekrar deneyin.";
        }
        else if (heartRate < 80)
        {
            stressLevelText.text = "D�s�k Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(0);
        }
        else if (heartRate < 100)
        {
            stressLevelText.text = "Orta Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(1);
        }
        else
        {
            stressLevelText.text = "Y�ksek Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(2);
        }

    }

    string GetRandomSuggestion(int index) // �nerileri belli bir s�ras� olmadan random bir �ekilde ayarlayan fonksiyon.
    {
        string[] selectedSuggestions = suggestions[index];
        int randomIndex = Random.Range(0, selectedSuggestions.Length);
        return selectedSuggestions[randomIndex];
    }

    void RefreshSuggestions() //�nerileri yenileyip g�steren fonksiyon.
    {
        DisplaySuggestions(PlayerPrefs.GetInt("HeartRate"));

    }
}


