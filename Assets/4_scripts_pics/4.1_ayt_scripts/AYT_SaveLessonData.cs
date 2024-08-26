//using UnityEngine;
//using TMPro;

//public class AYT_SaveLessonData : MonoBehaviour
//{
//    public TMP_InputField aytSos1CorrectInputField;
//    public TMP_InputField aytSos1WrongInputField;
//    public TMP_InputField aytSos1EmptyInputField;

//    public TMP_InputField aytSos2CorrectInputField;
//    public TMP_InputField aytSos2WrongInputField;
//    public TMP_InputField aytSos2EmptyInputField;

//    public TMP_InputField aytMatematikCorrectInputField;
//    public TMP_InputField aytMatematikWrongInputField;
//    public TMP_InputField aytMatematikEmptyInputField;

//    public TMP_InputField aytFenCorrectInputField;
//    public TMP_InputField aytFenWrongInputField;
//    public TMP_InputField aytFenEmptyInputField;
//    // 6-20. satýrlarda AYT dersleri için girdi kutularý tanýmlandý.

//    public TextMeshProUGUI warningText; // Uyarý metni tanýmlandý.

//    public AYT_LessonData aytLessonData; // Baþka script'teki bir sýnýftan bir nesne türetildi.

//    public void AYT_SaveData()
//    {
//        int sos1Correct = ParseInputField(aytSos1CorrectInputField);
//        int sos1Wrong = ParseInputField(aytSos1WrongInputField);
//        int sos1Empty = ParseInputField(aytSos1EmptyInputField);

//        int sos2Correct = ParseInputField(aytSos2CorrectInputField);
//        int sos2Wrong = ParseInputField(aytSos2WrongInputField);
//        int sos2Empty = ParseInputField(aytSos2EmptyInputField);

//        int matematikCorrect = ParseInputField(aytMatematikCorrectInputField);
//        int matematikWrong = ParseInputField(aytMatematikWrongInputField);
//        int matematikEmpty = ParseInputField(aytMatematikEmptyInputField);

//        int fenCorrect = ParseInputField(aytFenCorrectInputField);
//        int fenWrong = ParseInputField(aytFenWrongInputField);
//        int fenEmpty = ParseInputField(aytFenEmptyInputField);
//        // 29-43. satýrlarda kullanýcýdan alýnan deðerler tam sayýya çevrildi, çünkü bunlarýn matematiksel iþlemlerde kullanýlmasý gerekiyor.

//        bool isSay = IsSectionFilled(matematikCorrect, matematikWrong, matematikEmpty) && IsSectionFilled(fenCorrect, fenWrong, fenEmpty); // Eðer buradan true dönerse kullanýcý sayýsal bölüm için veri girmiþtir.
//        bool isEa = IsSectionFilled(matematikCorrect, matematikWrong, matematikEmpty) && IsSectionFilled(sos1Correct, sos1Wrong, sos1Empty); // Eðer buradan true dönerse kullanýcý eþit aðýrlýk bölümü için veri girmiþtir.
//        bool isSoz = IsSectionFilled(sos1Correct, sos1Wrong, sos1Empty) && IsSectionFilled(sos2Correct, sos2Wrong, sos2Empty); // Eðer buradan true dönerse kullanýcý sözel bölüm için veri girmiþtir.

//        if ((isSay && isEa) || (isEa && isSoz) || (isSay && isSoz)) // Kullanýcý sadece bir alandan YKS'ye gireceði için tek bir bölüme ait deðerler girmiþ olmalýdýr. Eðer birden fazla bölüm için deðer girildiyse kullanýcýya hata mesajý gösterilir.
//        {
//            warningText.text = "Yalnýzca tek bir alan için deðer girin.";
//            return;
//        }

//        if (isSay)
//        {
//            if (!IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) || !IsValidInput(fenCorrect, fenWrong, fenEmpty, 40))
//            {
//                warningText.text = "SAY bölümü için geçerli deðerler girin. Matematik ve Fen toplamlarý 40 olmalýdýr ve negatif deðerler olmamalýdýr.";
//                return;
//            }
//        }
//        if (isEa)
//        {
//            if (!IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) || !IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40))
//            {
//                warningText.text = "EA bölümü için geçerli deðerler girin. Matematik ve Sosyal 1 toplamlarý 40 olmalýdýr ve negatif deðerler olmamalýdýr.";
//                return;
//            }
//        }
//        if (isSoz)
//        {
//            if (!IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40) || !IsValidInput(sos2Correct, sos2Wrong, sos2Empty, 40))
//            {
//                warningText.text = "SÖZ bölümü için geçerli deðerler girin. Sosyal 1 ve Sosyal 2 toplamlarý 40 olmalýdýr ve negatif deðerler olmamalýdýr.";
//                return;
//            }
//        }
//        // 56-79. satýrlarda kullanýcýnýn alanýna göre girdiði deðerlerde bir yanlýþlýk olup olmadýðý kontrol edilir. Eðer gerekliyse hata mesajý gösterilir.

//        if (!isSay && !isEa && !isSoz) // Burada da kullanýcý hiçbir deðer girmeden kaydetmeye çalýþýrsa bu uyarý mesajý gösterilir.
//        {
//            warningText.text = "Lütfen bir bölüm için geçerli deðerler girin.";
//            return;
//        }

//        aytLessonData.aytSos1CorrectAnswers = sos1Correct;
//        aytLessonData.aytSos1WrongAnswers = sos1Wrong;
//        aytLessonData.aytSos1EmptyAnswers = sos1Empty;

//        aytLessonData.aytSos2CorrectAnswers = sos2Correct;
//        aytLessonData.aytSos2WrongAnswers = sos2Wrong;
//        aytLessonData.aytSos2EmptyAnswers = sos2Empty;

//        aytLessonData.aytMatematikCorrectAnswers = matematikCorrect;
//        aytLessonData.aytMatematikWrongAnswers = matematikWrong;
//        aytLessonData.aytMatematikEmptyAnswers = matematikEmpty;

//        aytLessonData.aytFenCorrectAnswers = fenCorrect;
//        aytLessonData.aytFenWrongAnswers = fenWrong;
//        aytLessonData.aytFenEmptyAnswers = fenEmpty;
//        // 88-102. satýrlarda alýnan doðru deðerlerin atamasý yapýlýr.

//        float ayt_ToplamNet = (aytLessonData.aytSos1CorrectAnswers + aytLessonData.aytSos2CorrectAnswers + aytLessonData.aytMatematikCorrectAnswers + aytLessonData.aytFenCorrectAnswers)
//            - (aytLessonData.aytSos1WrongAnswers + aytLessonData.aytSos2WrongAnswers + aytLessonData.aytMatematikWrongAnswers + aytLessonData.aytFenWrongAnswers) / 4.0f; // Toplam net hesabý yapýlýr.

//        AYT_DataManager.aytInstance.AddNet(ayt_ToplamNet); // Elde edilen net deðeri kaydedilir.

//        Debug.Log("Net added: " + ayt_ToplamNet); // Bu satýrýn kodun çalýþmasýna bir katkýsý yoktur, uygulamanýn geliþtirme aþamasýnda her þeyin yolunda gidip gitmediðini anlayabilmek için konsol çýktýsý alýnýr.

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

//    private bool IsSectionFilled(int correct, int wrong, int empty) // Bu fonksiyon herhangi bir derse ait verilen parametreleri kullanarak veri girilip girilmediðini kontrol etmek için kullanýlýr.
//    {
//        return (correct > 0 || wrong > 0 || empty > 0);
//    }
//}

using UnityEngine;
using TMPro;

public class AYT_SaveLessonData : MonoBehaviour
{
    public TMP_InputField aytSos1CorrectInputField;
    public TMP_InputField aytSos1WrongInputField;
    public TMP_InputField aytSos1EmptyInputField;

    public TMP_InputField aytSos2CorrectInputField;
    public TMP_InputField aytSos2WrongInputField;
    public TMP_InputField aytSos2EmptyInputField;

    public TMP_InputField aytMatematikCorrectInputField;
    public TMP_InputField aytMatematikWrongInputField;
    public TMP_InputField aytMatematikEmptyInputField;

    public TMP_InputField aytFenCorrectInputField;
    public TMP_InputField aytFenWrongInputField;
    public TMP_InputField aytFenEmptyInputField;
    // 6-20. satýrlarda AYT dersleri için girdi kutularý tanýmlandý.

    public TextMeshProUGUI warningText; // Uyarý metni tanýmlandý.

    public AYT_LessonData aytLessonData; // Baþka script'teki bir sýnýftan bir nesne türetildi.

    public void AYT_SaveData()
    {
        int sos1Correct = ParseInputField(aytSos1CorrectInputField);
        int sos1Wrong = ParseInputField(aytSos1WrongInputField);
        int sos1Empty = ParseInputField(aytSos1EmptyInputField);

        int sos2Correct = ParseInputField(aytSos2CorrectInputField);
        int sos2Wrong = ParseInputField(aytSos2WrongInputField);
        int sos2Empty = ParseInputField(aytSos2EmptyInputField);

        int matematikCorrect = ParseInputField(aytMatematikCorrectInputField);
        int matematikWrong = ParseInputField(aytMatematikWrongInputField);
        int matematikEmpty = ParseInputField(aytMatematikEmptyInputField);

        int fenCorrect = ParseInputField(aytFenCorrectInputField);
        int fenWrong = ParseInputField(aytFenWrongInputField);
        int fenEmpty = ParseInputField(aytFenEmptyInputField);
        // 29-43. satýrlarda kullanýcýdan alýnan deðerler tam sayýya çevrildi, çünkü bunlarýn matematiksel iþlemlerde kullanýlmasý gerekiyor.

        bool isSay = IsSectionFilled(matematikCorrect, matematikWrong, matematikEmpty) && IsSectionFilled(fenCorrect, fenWrong, fenEmpty); // Eðer buradan true dönerse kullanýcý sayýsal bölüm için veri girmiþtir.
        bool isEa = IsSectionFilled(matematikCorrect, matematikWrong, matematikEmpty) && IsSectionFilled(sos1Correct, sos1Wrong, sos1Empty); // Eðer buradan true dönerse kullanýcý eþit aðýrlýk bölümü için veri girmiþtir.
        bool isSoz = IsSectionFilled(sos1Correct, sos1Wrong, sos1Empty) && IsSectionFilled(sos2Correct, sos2Wrong, sos2Empty); // Eðer buradan true dönerse kullanýcý sözel bölüm için veri girmiþtir.

        if ((isSay && isEa) || (isEa && isSoz) || (isSay && isSoz)) // Kullanýcý sadece bir alandan YKS'ye gireceði için tek bir bölüme ait deðerler girmiþ olmalýdýr. Eðer birden fazla bölüm için deðer girildiyse kullanýcýya hata mesajý gösterilir.
        {
            warningText.text = "Yalnýzca tek bir alan için deðer girin.";
            return;
        }

        if (sos1Correct == -1 || sos1Wrong == -1 || sos1Empty == -1 ||
            sos2Correct == -1 || sos2Wrong == -1 || sos2Empty == -1 ||
            matematikCorrect == -1 || matematikWrong == -1 || matematikEmpty == -1 ||
            fenCorrect == -1 || fenWrong == -1 || fenEmpty == -1)
        {
            warningText.text = "Lütfen sadece sayýsal deðerler girin.";
            return;
        }

        if (isSay)
        {
            if (!IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) || !IsValidInput(fenCorrect, fenWrong, fenEmpty, 40))
            {
                warningText.text = "SAY bölümü için geçerli deðerler girin. Matematik ve Fen toplamlarý 40 olmalýdýr ve negatif deðerler olmamalýdýr.";
                return;
            }
        }
        if (isEa)
        {
            if (!IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) || !IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40))
            {
                warningText.text = "EA bölümü için geçerli deðerler girin. Matematik ve Sosyal 1 toplamlarý 40 olmalýdýr ve negatif deðerler olmamalýdýr.";
                return;
            }
        }
        if (isSoz)
        {
            if (!IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40) || !IsValidInput(sos2Correct, sos2Wrong, sos2Empty, 40))
            {
                warningText.text = "SÖZ bölümü için geçerli deðerler girin. Sosyal 1 ve Sosyal 2 toplamlarý 40 olmalýdýr ve negatif deðerler olmamalýdýr.";
                return;
            }
        }
        // 56-79. satýrlarda kullanýcýnýn alanýna göre girdiði deðerlerde bir yanlýþlýk olup olmadýðý kontrol edilir. Eðer gerekliyse hata mesajý gösterilir.

        if (!isSay && !isEa && !isSoz) // Burada da kullanýcý hiçbir deðer girmeden kaydetmeye çalýþýrsa bu uyarý mesajý gösterilir.
        {
            warningText.text = "Lütfen bir bölüm için geçerli deðerler girin.";
            return;
        }

        aytLessonData.aytSos1CorrectAnswers = sos1Correct;
        aytLessonData.aytSos1WrongAnswers = sos1Wrong;
        aytLessonData.aytSos1EmptyAnswers = sos1Empty;

        aytLessonData.aytSos2CorrectAnswers = sos2Correct;
        aytLessonData.aytSos2WrongAnswers = sos2Wrong;
        aytLessonData.aytSos2EmptyAnswers = sos2Empty;

        aytLessonData.aytMatematikCorrectAnswers = matematikCorrect;
        aytLessonData.aytMatematikWrongAnswers = matematikWrong;
        aytLessonData.aytMatematikEmptyAnswers = matematikEmpty;

        aytLessonData.aytFenCorrectAnswers = fenCorrect;
        aytLessonData.aytFenWrongAnswers = fenWrong;
        aytLessonData.aytFenEmptyAnswers = fenEmpty;
        // 88-102. satýrlarda alýnan doðru deðerlerin atamasý yapýlýr.

        float ayt_ToplamNet = (aytLessonData.aytSos1CorrectAnswers + aytLessonData.aytSos2CorrectAnswers + aytLessonData.aytMatematikCorrectAnswers + aytLessonData.aytFenCorrectAnswers)
            - (aytLessonData.aytSos1WrongAnswers + aytLessonData.aytSos2WrongAnswers + aytLessonData.aytMatematikWrongAnswers + aytLessonData.aytFenWrongAnswers) / 4.0f; // Toplam net hesabý yapýlýr.

        AYT_DataManager.aytInstance.AddNet(ayt_ToplamNet); // Elde edilen net deðeri kaydedilir.

        Debug.Log("Net added: " + ayt_ToplamNet); // Bu satýrýn kodun çalýþmasýna bir katkýsý yoktur, uygulamanýn geliþtirme aþamasýnda her þeyin yolunda gidip gitmediðini anlayabilmek için konsol çýktýsý alýnýr.

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

    private bool IsSectionFilled(int correct, int wrong, int empty) // Bu fonksiyon herhangi bir derse ait verilen parametreleri kullanarak veri girilip girilmediðini kontrol etmek için kullanýlýr.
    {
        return (correct > 0 || wrong > 0 || empty > 0);
    }
}
