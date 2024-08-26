using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SuggestionController : MonoBehaviour
{
    public TextMeshProUGUI stressLevelText; // Stres düzeyini gösteren text.
    public TextMeshProUGUI suggestionText; // Stres seviyesinin durumuna göre kullanýcýya gösterilen öneri texti.
    public Button refreshButton; // Ayný stres seviyesi için önerileri yenileyen buton.

    private string[][] suggestions = new string[][]
    {
        new string[] // Düþük stres seviyesi için öneriler.
        {
            "Egzersiz Yapýn: Hafif tempolu yürüyüþ veya yoga gibi aktiviteler stres hormonlarýný azaltýr ve ruh halinizi iyileþtirir.",
            "Meditasyon ve Derin Nefes Alma Egzersizleri: Bu teknikler sakinleþmeye yardýmcý olur ve stresi azaltýr.",
            "Doðada Zaman Geçirin: Doðada vakit geçirmek zihinsel saðlýk üzerinde olumlu etkiler yapar.",
            "Sanatsal Aktiviteler: Resim yapmak, müzik dinlemek veya enstrüman çalmak zihninizi meþgul ederek stresi azaltýr.",
            "Dengeli Beslenme: Saðlýklý bir diyet stresi yönetmede önemli bir rol oynar. Örneðin, avokado ve ceviz gibi besinler ruh halini düzenler.",
            "Günlük Tutma: Duygularýnýzý yazýya dökmek stresin etkilerini hafifletebilir.",
            "Hobilerle Uðraþýn: Keyif aldýðýnýz aktivitelerle meþgul olmak stres seviyenizi düþük tutar.",
            "Kýsa Molalar Verin: Düzenli aralýklarla kýsa molalar vermek zihinsel yorgunluðu azaltýr."
        },
        new string[] // Orta stres seviyesi için öneriler.
        {
            "Düzenli Uyku: Yeterli uyku almak, vücudunuzu ve zihninizi yenileyerek stresle baþa çýkmanýza yardýmcý olur.",
            "Fiziksel Aktiviteyi Arttýrýn: Koþu, yüzme gibi daha yoðun egzersizler stres hormonlarýný azaltýr ve endorfin salgýlar.",
            "Sosyal Baðlantýlar: Aile ve arkadaþlarla zaman geçirmek, destek sisteminizi güçlendirir ve stresle baþa çýkmayý kolaylaþtýrýr.",
            "Biofeedback ve Gevþeme Teknikleri: Vücut fonksiyonlarýný kontrol etmeyi öðrenmek stres yönetiminde etkili olabilir.",
            "Rutin Oluþturma: Günlük bir rutin oluþturmak, belirsizlikleri azaltýr ve stresi kontrol altýnda tutar.",
            "Hafif Streching ve Yoga: Bu aktiviteler fiziksel ve zihinsel gevþemeyi teþvik eder.",
            "Destek Gruplarýna Katýlýn: Benzer deneyimlere sahip insanlarla iletiþim kurmak, stresin etkilerini hafifletebilir.",
            "Kendinize Zaman Ayýrýn: Gün içinde kendinize dinlenme ve yenilenme zamaný ayýrmak stresle baþa çýkmanýza yardýmcý olur."
        },
        new string[] // Yüksek stres seviyesi için öneriler.
        {
            "Profesyonel Yardým Alýn: Terapi veya danýþmanlýk, stres yönetiminde çok etkili olabilir.",
            "Yoðun Egzersiz: Daha yoðun fiziksel aktiviteler (örneðin, aðýrlýk kaldýrma) stresi azaltmada etkili olabilir.",
            "Mindfulness ve Meditasyon: Bu teknikler, yüksek stres seviyelerinde bile zihninizi sakinleþtirir.",
            "Derin Nefes Alma Egzersizleri: Nefes egzersizleri vücut ve zihin üzerinde hýzlý bir rahatlama etkisi yaratýr.",
            "Eðlenceli Aktiviteler: Sevdiðiniz bir aktiviteyle meþgul olmak, yoðun stres altýnda bile rahatlamanýza yardýmcý olabilir.",
            "Kafein ve Alkol Tüketimini Azaltýn: Bu maddeler stres seviyelerini arttýrabilir, bu yüzden tüketimi sýnýrlamak önemlidir.",
            "Zaman Yönetimi: Görevlerinizi önceliklendirmek ve yönetmek, stres seviyenizi azaltýr.",
            "Stresi Azaltan Takviyeler: Melatonin, Ashwagandha ve B vitaminleri gibi takviyeler, stresle baþa çýkmada yardýmcý olabilir."
        }
    };

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshSuggestions); // Refresh butonuna basýldýðýnda RefreshSuggestions fonksiyonu çaðýrýlýr.
        DisplaySuggestions(PlayerPrefs.GetInt("HeartRate")); // Nabýz deðeri kullanýlarak öneriler gösterilir.

    }

    void DisplaySuggestions(float heartRate) // Anket sonucuna göre stres seviyesi text'inde ne yazacaðýný belirleyen fonksiyon.
    {
        if (heartRate < 50)
        {
            suggestionText.text = "Ölçüm esnasýnda parmaðýnýzý kaldýrmayýn ve tekrar deneyin.";
        }
        else if (heartRate < 80)
        {
            stressLevelText.text = "Düsük Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(0);
        }
        else if (heartRate < 100)
        {
            stressLevelText.text = "Orta Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(1);
        }
        else
        {
            stressLevelText.text = "Yüksek Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(2);
        }

    }

    string GetRandomSuggestion(int index) // Önerileri belli bir sýrasý olmadan random bir þekilde ayarlayan fonksiyon.
    {
        string[] selectedSuggestions = suggestions[index];
        int randomIndex = Random.Range(0, selectedSuggestions.Length);
        return selectedSuggestions[randomIndex];
    }

    void RefreshSuggestions() //Önerileri yenileyip gösteren fonksiyon.
    {
        DisplaySuggestions(PlayerPrefs.GetInt("HeartRate"));

    }
}


