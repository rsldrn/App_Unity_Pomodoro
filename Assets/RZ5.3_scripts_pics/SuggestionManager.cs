using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SuggestionManager : MonoBehaviour
{
    public TextMeshProUGUI stressLevelText;   //Stres düzeyini gösteren text.
    public TextMeshProUGUI suggestionText;    //Stres seviyesinin durumuna göre kullanıcıya gösterilen öneri texti.
    public Button refreshButton;              //Aynı stres seviyesi için önerileri yenileyen buton.

    private string[][] suggestions = new string[][]  
    {
        new string[] //Düşük stres seviyesi için öneriler.
        {
            "Egzersiz Yapın: Hafif tempolu yürüyüş veya yoga gibi aktiviteler stres hormonlarını azaltır ve ruh halinizi iyileştirir.",
            "Meditasyon ve Derin Nefes Alma Egzersizleri: Bu teknikler sakinleşmeye yardımcı olur ve stresi azaltır.",
            "Doğada Zaman Geçirin: Doğada vakit geçirmek zihinsel sağlık üzerinde olumlu etkiler yapar.",
            "Sanatsal Aktiviteler: Resim yapmak, müzik dinlemek veya enstrüman çalmak zihninizi meşgul ederek stresi azaltır.",
            "Dengeli Beslenme: Sağlıklı bir diyet stresi yönetmede önemli bir rol oynar. Örneğin, avokado ve ceviz gibi besinler ruh halini düzenler.",
            "Günlük Tutma: Duygularınızı yazıya dökmek stresin etkilerini hafifletebilir.",
            "Hobilerle Uğraşın: Keyif aldığınız aktivitelerle meşgul olmak stres seviyenizi düşük tutar.",
            "Kısa Molalar Verin: Düzenli aralıklarla kısa molalar vermek zihinsel yorgunluğu azaltır."
        },
        new string[] //Orta stres seviyesi için öneriler.
        {
            "Düzenli Uyku: Yeterli uyku almak, vücudunuzu ve zihninizi yenileyerek stresle başa çıkmanıza yardımcı olur.",
            "Fiziksel Aktiviteyi Arttırın: Koşu, yüzme gibi daha yoğun egzersizler stres hormonlarını azaltır ve endorfin salgılar.",
            "Sosyal Bağlantılar: Aile ve arkadaşlarla zaman geçirmek, destek sisteminizi güçlendirir ve stresle başa çıkmayı kolaylaştırır.",
            "Biofeedback ve Gevşeme Teknikleri: Vücut fonksiyonlarını kontrol etmeyi öğrenmek stres yönetiminde etkili olabilir.",
            "Rutin Oluşturma: Günlük bir rutin oluşturmak, belirsizlikleri azaltır ve stresi kontrol altında tutar.",
            "Hafif Streching ve Yoga: Bu aktiviteler fiziksel ve zihinsel gevşemeyi teşvik eder.",
            "Destek Gruplarına Katılın: Benzer deneyimlere sahip insanlarla iletişim kurmak, stresin etkilerini hafifletebilir.",
            "Kendinize Zaman Ayırın: Gün içinde kendinize dinlenme ve yenilenme zamanı ayırmak stresle başa çıkmanıza yardımcı olur."
        },
        new string[] //Yüksek stres seviyesi için öneriler.
        {
            "Profesyonel Yardım Alın: Terapi veya danışmanlık, stres yönetiminde çok etkili olabilir.",
            "Yoğun Egzersiz: Daha yoğun fiziksel aktiviteler (örneğin, ağırlık kaldırma) stresi azaltmada etkili olabilir.",
            "Mindfulness ve Meditasyon: Bu teknikler, yüksek stres seviyelerinde bile zihninizi sakinleştirir.",
            "Derin Nefes Alma Egzersizleri: Nefes egzersizleri vücut ve zihin üzerinde hızlı bir rahatlama etkisi yaratır.",
            "Eğlenceli Aktiviteler: Sevdiğiniz bir aktiviteyle meşgul olmak, yoğun stres altında bile rahatlamanıza yardımcı olabilir.",
            "Kafein ve Alkol Tüketimini Azaltın: Bu maddeler stres seviyelerini arttırabilir, bu yüzden tüketimi sınırlamak önemlidir.",
            "Zaman Yönetimi: Görevlerinizi önceliklendirmek ve yönetmek, stres seviyenizi azaltır.",
            "Stresi Azaltan Takviyeler: Melatonin, Ashwagandha ve B vitaminleri gibi takviyeler, stresle başa çıkmada yardımcı olabilir."
        }
    };

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshSuggestions); //Refresh butonuna basıldığında RefreshSuggestions fonksiyonu çağırılır.
        DisplaySuggestions(SurveyData.totalScore);             //SurveyData.totalScore kullanılarak öneriler gösterilir.

    }

    void DisplaySuggestions(float stressLevel)   //Anket sonucuna göre stres seviyesi text'inde ne yazacağını belirleyen fonksiyon.
    {
        if (stressLevel < 7)
        {
            stressLevelText.text = "Düsük Stres Seviyesi";
            suggestionText.text = GetRandomSuggestion(0);
        }
        else if (stressLevel < 14)
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

    string GetRandomSuggestion(int index)  //Önerileri belli bir sırası olmadan random bir şekilde ayarlayan fonksiyon.
    {
        string[] selectedSuggestions = suggestions[index];
        int randomIndex = Random.Range(0, selectedSuggestions.Length);
        return selectedSuggestions[randomIndex];
    }

    void RefreshSuggestions()     //Önerileri yenileyip gösteren fonksiyon.
    {
        DisplaySuggestions(SurveyData.totalScore);
        
    }
}


