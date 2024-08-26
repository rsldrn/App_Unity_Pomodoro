//using UnityEngine;
//using TMPro;

//public class TYT_SaveLessonData : MonoBehaviour
//{
//    public TMP_InputField tytTurkceCorrectInputField;
//    public TMP_InputField tytTurkceWrongInputField;
//    public TMP_InputField tytTurkceEmptyInputField;

//    public TMP_InputField tytSosyalCorrectInputField;
//    public TMP_InputField tytSosyalWrongInputField;
//    public TMP_InputField tytSosyalEmptyInputField;

//    public TMP_InputField tytMatematikCorrectInputField;
//    public TMP_InputField tytMatematikWrongInputField;
//    public TMP_InputField tytMatematikEmptyInputField;

//    public TMP_InputField tytFenCorrectInputField;
//    public TMP_InputField tytFenWrongInputField;
//    public TMP_InputField tytFenEmptyInputField;
//    // 6-20. satýrlarda TYT dersleri için girdi kutularý tanýmlanmýþtýr

//    public TextMeshProUGUI warningText; // Uyarý metni tanýmlandý

//    public TYT_LessonData tytLessonData; // Baþka script'teki bir sýnýftan bir nesne türetildi

//    public void TYT_SaveData()
//    {
//        int turkceCorrect = ParseInputField(tytTurkceCorrectInputField);
//        int turkceWrong = ParseInputField(tytTurkceWrongInputField);
//        int turkceEmpty = ParseInputField(tytTurkceEmptyInputField);

//        int sosyalCorrect = ParseInputField(tytSosyalCorrectInputField);
//        int sosyalWrong = ParseInputField(tytSosyalWrongInputField);
//        int sosyalEmpty = ParseInputField(tytSosyalEmptyInputField);

//        int matematikCorrect = ParseInputField(tytMatematikCorrectInputField);
//        int matematikWrong = ParseInputField(tytMatematikWrongInputField);
//        int matematikEmpty = ParseInputField(tytMatematikEmptyInputField);

//        int fenCorrect = ParseInputField(tytFenCorrectInputField);
//        int fenWrong = ParseInputField(tytFenWrongInputField);
//        int fenEmpty = ParseInputField(tytFenEmptyInputField);
//        // 29-43. satýrlarda kullanýcýdan alýnan deðerler tam sayýya çevrildi, çünkü bunlarýn matematiksel iþlemlerde kullanýlmasý gerekiyor.

//        if (!IsValidInput(turkceCorrect, turkceWrong, turkceEmpty, 40) ||
//            !IsValidInput(sosyalCorrect, sosyalWrong, sosyalEmpty, 20) ||
//            !IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) ||
//            !IsValidInput(fenCorrect, fenWrong, fenEmpty, 20)) // Burada girilen deðerlerde bir yanlýþlýk var ise uyarý metni güncellenir.
//        {
//            warningText.text = "Lütfen her ders için geçerli deðerler girin. Toplam soru sayýsý ve negatif deðerler kontrol edilmeli.";
//            return;
//        }

//        tytLessonData.tytTurkceCorrectAnswers = turkceCorrect;
//        tytLessonData.tytTurkceWrongAnswers = turkceWrong;
//        tytLessonData.tytTurkceEmptyAnswers = turkceEmpty;

//        tytLessonData.tytSosyalCorrectAnswers = sosyalCorrect;
//        tytLessonData.tytSosyalWrongAnswers = sosyalWrong;
//        tytLessonData.tytSosyalEmptyAnswers = sosyalEmpty;

//        tytLessonData.tytMatematikCorrectAnswers = matematikCorrect;
//        tytLessonData.tytMatematikWrongAnswers = matematikWrong;
//        tytLessonData.tytMatematikEmptyAnswers = matematikEmpty;

//        tytLessonData.tytFenCorrectAnswers = fenCorrect;
//        tytLessonData.tytFenWrongAnswers = fenWrong;
//        tytLessonData.tytFenEmptyAnswers = fenEmpty;
//        // 55-69. satýrlarda alýnan doðru deðerlerin atamasý yapýlýr.

//        float tyt_ToplamNet = (tytLessonData.tytTurkceCorrectAnswers + tytLessonData.tytSosyalCorrectAnswers + tytLessonData.tytMatematikCorrectAnswers + tytLessonData.tytFenCorrectAnswers)
//            - (tytLessonData.tytTurkceWrongAnswers + tytLessonData.tytSosyalWrongAnswers + tytLessonData.tytMatematikWrongAnswers + tytLessonData.tytFenWrongAnswers) / 4.0f; // Toplam net hesabý yapýlýr.

//        TYT_DataManager.tytInstance.AddNet(tyt_ToplamNet); // Elde edilen net deðeri kaydedilir.

//        Debug.Log("Net added: " + tyt_ToplamNet); // Bu satýrýn kodun çalýþmasýna bir katkýsý yoktur, uygulamanýn geliþtirme aþamasýnda her þeyin yolunda gidip gitmediðini anlayabilmek için konsol çýktýsý alýnýr.

//        warningText.text = ""; // Bu satýra gelinmesi kullanýcýnýn herhangi bir yanlýþ deðer girmediðini gösterir, bu yüzden uyarý mesajý silinir.
//    }

//    private int ParseInputField(TMP_InputField inputField) // Bu fonksiyon parametre aldýðý string'i tam sayý olarak döndürür, string boþsa 0 döndürür.
//    {
//        if (string.IsNullOrEmpty(inputField.text))
//        {
//            return 0;
//        }
//        else
//        {
//            return int.Parse(inputField.text);
//        }
//    }

//    private bool IsValidInput(int correct, int wrong, int empty, int totalQuestions) // Bu fonksiyon girdi kutularýna girilen deðerlerin doðruluðunu kontrol etmek için kullanýlýr.
//    {
//        return (correct + wrong + empty == totalQuestions) && (correct >= 0 && wrong >= 0 && empty >= 0);
//    }
//}

using UnityEngine;
using TMPro;

public class TYT_SaveLessonData : MonoBehaviour
{
    public TMP_InputField tytTurkceCorrectInputField;
    public TMP_InputField tytTurkceWrongInputField;
    public TMP_InputField tytTurkceEmptyInputField;

    public TMP_InputField tytSosyalCorrectInputField;
    public TMP_InputField tytSosyalWrongInputField;
    public TMP_InputField tytSosyalEmptyInputField;

    public TMP_InputField tytMatematikCorrectInputField;
    public TMP_InputField tytMatematikWrongInputField;
    public TMP_InputField tytMatematikEmptyInputField;

    public TMP_InputField tytFenCorrectInputField;
    public TMP_InputField tytFenWrongInputField;
    public TMP_InputField tytFenEmptyInputField;
    // 6-20. satýrlarda TYT dersleri için girdi kutularý tanýmlanmýþtýr

    public TextMeshProUGUI warningText; // Uyarý metni tanýmlandý

    public TYT_LessonData tytLessonData; // Baþka script'teki bir sýnýftan bir nesne türetildi

    public void TYT_SaveData()
    {
        int turkceCorrect = ParseInputField(tytTurkceCorrectInputField);
        int turkceWrong = ParseInputField(tytTurkceWrongInputField);
        int turkceEmpty = ParseInputField(tytTurkceEmptyInputField);

        int sosyalCorrect = ParseInputField(tytSosyalCorrectInputField);
        int sosyalWrong = ParseInputField(tytSosyalWrongInputField);
        int sosyalEmpty = ParseInputField(tytSosyalEmptyInputField);

        int matematikCorrect = ParseInputField(tytMatematikCorrectInputField);
        int matematikWrong = ParseInputField(tytMatematikWrongInputField);
        int matematikEmpty = ParseInputField(tytMatematikEmptyInputField);

        int fenCorrect = ParseInputField(tytFenCorrectInputField);
        int fenWrong = ParseInputField(tytFenWrongInputField);
        int fenEmpty = ParseInputField(tytFenEmptyInputField);
        // 29-43. satýrlarda kullanýcýdan alýnan deðerler tam sayýya çevrildi, çünkü bunlarýn matematiksel iþlemlerde kullanýlmasý gerekiyor.

        if (turkceCorrect == -1 || turkceWrong == -1 || turkceEmpty == -1 ||
            sosyalCorrect == -1 || sosyalWrong == -1 || sosyalEmpty == -1 ||
            matematikCorrect == -1 || matematikWrong == -1 || matematikEmpty == -1 ||
            fenCorrect == -1 || fenWrong == -1 || fenEmpty == -1)
        {
            warningText.text = "Lütfen sadece sayýsal deðerler girin.";
            return;
        }

        if (!IsValidInput(turkceCorrect, turkceWrong, turkceEmpty, 40) ||
            !IsValidInput(sosyalCorrect, sosyalWrong, sosyalEmpty, 20) ||
            !IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) ||
            !IsValidInput(fenCorrect, fenWrong, fenEmpty, 20)) // Burada girilen deðerlerde bir yanlýþlýk var ise uyarý metni güncellenir.
        {
            warningText.text = "Lütfen her ders için geçerli deðerler girin. Toplam soru sayýsý ve negatif deðerler kontrol edilmeli.";
            return;
        }

        tytLessonData.tytTurkceCorrectAnswers = turkceCorrect;
        tytLessonData.tytTurkceWrongAnswers = turkceWrong;
        tytLessonData.tytTurkceEmptyAnswers = turkceEmpty;

        tytLessonData.tytSosyalCorrectAnswers = sosyalCorrect;
        tytLessonData.tytSosyalWrongAnswers = sosyalWrong;
        tytLessonData.tytSosyalEmptyAnswers = sosyalEmpty;

        tytLessonData.tytMatematikCorrectAnswers = matematikCorrect;
        tytLessonData.tytMatematikWrongAnswers = matematikWrong;
        tytLessonData.tytMatematikEmptyAnswers = matematikEmpty;

        tytLessonData.tytFenCorrectAnswers = fenCorrect;
        tytLessonData.tytFenWrongAnswers = fenWrong;
        tytLessonData.tytFenEmptyAnswers = fenEmpty;
        // 55-69. satýrlarda alýnan doðru deðerlerin atamasý yapýlýr.

        float tyt_ToplamNet = (tytLessonData.tytTurkceCorrectAnswers + tytLessonData.tytSosyalCorrectAnswers + tytLessonData.tytMatematikCorrectAnswers + tytLessonData.tytFenCorrectAnswers)
            - (tytLessonData.tytTurkceWrongAnswers + tytLessonData.tytSosyalWrongAnswers + tytLessonData.tytMatematikWrongAnswers + tytLessonData.tytFenWrongAnswers) / 4.0f; // Toplam net hesabý yapýlýr.

        TYT_DataManager.tytInstance.AddNet(tyt_ToplamNet); // Elde edilen net deðeri kaydedilir.

        Debug.Log("Net added: " + tyt_ToplamNet); // Bu satýrýn kodun çalýþmasýna bir katkýsý yoktur, uygulamanýn geliþtirme aþamasýnda her þeyin yolunda gidip gitmediðini anlayabilmek için konsol çýktýsý alýnýr.

        warningText.text = ""; // Bu satýra gelinmesi kullanýcýnýn herhangi bir yanlýþ deðer girmediðini gösterir, bu yüzden uyarý mesajý silinir.
    }

    private int ParseInputField(TMP_InputField inputField) // Bu fonksiyon parametre aldýðý string'i tam sayý olarak döndürür, string boþsa 0 döndürür.
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return 0;
        }
        else
        {
            if (int.TryParse(inputField.text, out int result))
            {
                return result;
            }
            else
            {
                return -1; // Eðer string sayý deðilse -1 döneriz
            }
        }
    }

    private bool IsValidInput(int correct, int wrong, int empty, int totalQuestions) // Bu fonksiyon girdi kutularýna girilen deðerlerin doðruluðunu kontrol etmek için kullanýlýr.
    {
        return (correct + wrong + empty == totalQuestions) && (correct >= 0 && wrong >= 0 && empty >= 0);
    }
}
