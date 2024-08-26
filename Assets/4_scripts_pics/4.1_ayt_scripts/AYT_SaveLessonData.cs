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
//    // 6-20. sat�rlarda AYT dersleri i�in girdi kutular� tan�mland�.

//    public TextMeshProUGUI warningText; // Uyar� metni tan�mland�.

//    public AYT_LessonData aytLessonData; // Ba�ka script'teki bir s�n�ftan bir nesne t�retildi.

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
//        // 29-43. sat�rlarda kullan�c�dan al�nan de�erler tam say�ya �evrildi, ��nk� bunlar�n matematiksel i�lemlerde kullan�lmas� gerekiyor.

//        bool isSay = IsSectionFilled(matematikCorrect, matematikWrong, matematikEmpty) && IsSectionFilled(fenCorrect, fenWrong, fenEmpty); // E�er buradan true d�nerse kullan�c� say�sal b�l�m i�in veri girmi�tir.
//        bool isEa = IsSectionFilled(matematikCorrect, matematikWrong, matematikEmpty) && IsSectionFilled(sos1Correct, sos1Wrong, sos1Empty); // E�er buradan true d�nerse kullan�c� e�it a��rl�k b�l�m� i�in veri girmi�tir.
//        bool isSoz = IsSectionFilled(sos1Correct, sos1Wrong, sos1Empty) && IsSectionFilled(sos2Correct, sos2Wrong, sos2Empty); // E�er buradan true d�nerse kullan�c� s�zel b�l�m i�in veri girmi�tir.

//        if ((isSay && isEa) || (isEa && isSoz) || (isSay && isSoz)) // Kullan�c� sadece bir alandan YKS'ye girece�i i�in tek bir b�l�me ait de�erler girmi� olmal�d�r. E�er birden fazla b�l�m i�in de�er girildiyse kullan�c�ya hata mesaj� g�sterilir.
//        {
//            warningText.text = "Yaln�zca tek bir alan i�in de�er girin.";
//            return;
//        }

//        if (isSay)
//        {
//            if (!IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) || !IsValidInput(fenCorrect, fenWrong, fenEmpty, 40))
//            {
//                warningText.text = "SAY b�l�m� i�in ge�erli de�erler girin. Matematik ve Fen toplamlar� 40 olmal�d�r ve negatif de�erler olmamal�d�r.";
//                return;
//            }
//        }
//        if (isEa)
//        {
//            if (!IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) || !IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40))
//            {
//                warningText.text = "EA b�l�m� i�in ge�erli de�erler girin. Matematik ve Sosyal 1 toplamlar� 40 olmal�d�r ve negatif de�erler olmamal�d�r.";
//                return;
//            }
//        }
//        if (isSoz)
//        {
//            if (!IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40) || !IsValidInput(sos2Correct, sos2Wrong, sos2Empty, 40))
//            {
//                warningText.text = "S�Z b�l�m� i�in ge�erli de�erler girin. Sosyal 1 ve Sosyal 2 toplamlar� 40 olmal�d�r ve negatif de�erler olmamal�d�r.";
//                return;
//            }
//        }
//        // 56-79. sat�rlarda kullan�c�n�n alan�na g�re girdi�i de�erlerde bir yanl��l�k olup olmad��� kontrol edilir. E�er gerekliyse hata mesaj� g�sterilir.

//        if (!isSay && !isEa && !isSoz) // Burada da kullan�c� hi�bir de�er girmeden kaydetmeye �al���rsa bu uyar� mesaj� g�sterilir.
//        {
//            warningText.text = "L�tfen bir b�l�m i�in ge�erli de�erler girin.";
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
//        // 88-102. sat�rlarda al�nan do�ru de�erlerin atamas� yap�l�r.

//        float ayt_ToplamNet = (aytLessonData.aytSos1CorrectAnswers + aytLessonData.aytSos2CorrectAnswers + aytLessonData.aytMatematikCorrectAnswers + aytLessonData.aytFenCorrectAnswers)
//            - (aytLessonData.aytSos1WrongAnswers + aytLessonData.aytSos2WrongAnswers + aytLessonData.aytMatematikWrongAnswers + aytLessonData.aytFenWrongAnswers) / 4.0f; // Toplam net hesab� yap�l�r.

//        AYT_DataManager.aytInstance.AddNet(ayt_ToplamNet); // Elde edilen net de�eri kaydedilir.

//        Debug.Log("Net added: " + ayt_ToplamNet); // Bu sat�r�n kodun �al��mas�na bir katk�s� yoktur, uygulaman�n geli�tirme a�amas�nda her �eyin yolunda gidip gitmedi�ini anlayabilmek i�in konsol ��kt�s� al�n�r.

//        warningText.text = ""; // Bu sat�ra gelinmesi kullan�c�n�n herhangi bir yanl�� de�er girmedi�ini g�sterir, bu y�zden uyar� mesaj� silinir.
//    }

//    private int ParseInputField(TMP_InputField inputField) // Bu fonksiyon parametre ald��� string'i tam say� olarak d�nd�r�r, string bo�sa 0 d�nd�r�r.
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

//    private bool IsValidInput(int correct, int wrong, int empty, int totalQuestions) // Bu fonksiyon girdi kutular�na girilen de�erlerin do�rulu�unu kontrol etmek i�in kullan�l�r.
//    {
//        return (correct + wrong + empty == totalQuestions) && (correct >= 0 && wrong >= 0 && empty >= 0);
//    }

//    private bool IsSectionFilled(int correct, int wrong, int empty) // Bu fonksiyon herhangi bir derse ait verilen parametreleri kullanarak veri girilip girilmedi�ini kontrol etmek i�in kullan�l�r.
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
    // 6-20. sat�rlarda AYT dersleri i�in girdi kutular� tan�mland�.

    public TextMeshProUGUI warningText; // Uyar� metni tan�mland�.

    public AYT_LessonData aytLessonData; // Ba�ka script'teki bir s�n�ftan bir nesne t�retildi.

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
        // 29-43. sat�rlarda kullan�c�dan al�nan de�erler tam say�ya �evrildi, ��nk� bunlar�n matematiksel i�lemlerde kullan�lmas� gerekiyor.

        bool isSay = IsSectionFilled(matematikCorrect, matematikWrong, matematikEmpty) && IsSectionFilled(fenCorrect, fenWrong, fenEmpty); // E�er buradan true d�nerse kullan�c� say�sal b�l�m i�in veri girmi�tir.
        bool isEa = IsSectionFilled(matematikCorrect, matematikWrong, matematikEmpty) && IsSectionFilled(sos1Correct, sos1Wrong, sos1Empty); // E�er buradan true d�nerse kullan�c� e�it a��rl�k b�l�m� i�in veri girmi�tir.
        bool isSoz = IsSectionFilled(sos1Correct, sos1Wrong, sos1Empty) && IsSectionFilled(sos2Correct, sos2Wrong, sos2Empty); // E�er buradan true d�nerse kullan�c� s�zel b�l�m i�in veri girmi�tir.

        if ((isSay && isEa) || (isEa && isSoz) || (isSay && isSoz)) // Kullan�c� sadece bir alandan YKS'ye girece�i i�in tek bir b�l�me ait de�erler girmi� olmal�d�r. E�er birden fazla b�l�m i�in de�er girildiyse kullan�c�ya hata mesaj� g�sterilir.
        {
            warningText.text = "Yaln�zca tek bir alan i�in de�er girin.";
            return;
        }

        if (sos1Correct == -1 || sos1Wrong == -1 || sos1Empty == -1 ||
            sos2Correct == -1 || sos2Wrong == -1 || sos2Empty == -1 ||
            matematikCorrect == -1 || matematikWrong == -1 || matematikEmpty == -1 ||
            fenCorrect == -1 || fenWrong == -1 || fenEmpty == -1)
        {
            warningText.text = "L�tfen sadece say�sal de�erler girin.";
            return;
        }

        if (isSay)
        {
            if (!IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) || !IsValidInput(fenCorrect, fenWrong, fenEmpty, 40))
            {
                warningText.text = "SAY b�l�m� i�in ge�erli de�erler girin. Matematik ve Fen toplamlar� 40 olmal�d�r ve negatif de�erler olmamal�d�r.";
                return;
            }
        }
        if (isEa)
        {
            if (!IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) || !IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40))
            {
                warningText.text = "EA b�l�m� i�in ge�erli de�erler girin. Matematik ve Sosyal 1 toplamlar� 40 olmal�d�r ve negatif de�erler olmamal�d�r.";
                return;
            }
        }
        if (isSoz)
        {
            if (!IsValidInput(sos1Correct, sos1Wrong, sos1Empty, 40) || !IsValidInput(sos2Correct, sos2Wrong, sos2Empty, 40))
            {
                warningText.text = "S�Z b�l�m� i�in ge�erli de�erler girin. Sosyal 1 ve Sosyal 2 toplamlar� 40 olmal�d�r ve negatif de�erler olmamal�d�r.";
                return;
            }
        }
        // 56-79. sat�rlarda kullan�c�n�n alan�na g�re girdi�i de�erlerde bir yanl��l�k olup olmad��� kontrol edilir. E�er gerekliyse hata mesaj� g�sterilir.

        if (!isSay && !isEa && !isSoz) // Burada da kullan�c� hi�bir de�er girmeden kaydetmeye �al���rsa bu uyar� mesaj� g�sterilir.
        {
            warningText.text = "L�tfen bir b�l�m i�in ge�erli de�erler girin.";
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
        // 88-102. sat�rlarda al�nan do�ru de�erlerin atamas� yap�l�r.

        float ayt_ToplamNet = (aytLessonData.aytSos1CorrectAnswers + aytLessonData.aytSos2CorrectAnswers + aytLessonData.aytMatematikCorrectAnswers + aytLessonData.aytFenCorrectAnswers)
            - (aytLessonData.aytSos1WrongAnswers + aytLessonData.aytSos2WrongAnswers + aytLessonData.aytMatematikWrongAnswers + aytLessonData.aytFenWrongAnswers) / 4.0f; // Toplam net hesab� yap�l�r.

        AYT_DataManager.aytInstance.AddNet(ayt_ToplamNet); // Elde edilen net de�eri kaydedilir.

        Debug.Log("Net added: " + ayt_ToplamNet); // Bu sat�r�n kodun �al��mas�na bir katk�s� yoktur, uygulaman�n geli�tirme a�amas�nda her �eyin yolunda gidip gitmedi�ini anlayabilmek i�in konsol ��kt�s� al�n�r.

        warningText.text = ""; // Bu sat�ra gelinmesi kullan�c�n�n herhangi bir yanl�� de�er girmedi�ini g�sterir, bu y�zden uyar� mesaj� silinir.
    }

    private int ParseInputField(TMP_InputField inputField) // Bu fonksiyon parametre ald��� string'i tam say� olarak d�nd�r�r, string bo�sa 0 d�nd�r�r.
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
                return -1; // E�er string say� de�ilse -1 d�neriz
            }
        }
    }

    private bool IsValidInput(int correct, int wrong, int empty, int totalQuestions) // Bu fonksiyon girdi kutular�na girilen de�erlerin do�rulu�unu kontrol etmek i�in kullan�l�r.
    {
        return (correct + wrong + empty == totalQuestions) && (correct >= 0 && wrong >= 0 && empty >= 0);
    }

    private bool IsSectionFilled(int correct, int wrong, int empty) // Bu fonksiyon herhangi bir derse ait verilen parametreleri kullanarak veri girilip girilmedi�ini kontrol etmek i�in kullan�l�r.
    {
        return (correct > 0 || wrong > 0 || empty > 0);
    }
}
