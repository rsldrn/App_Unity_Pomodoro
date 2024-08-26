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
//    // 6-20. sat�rlarda TYT dersleri i�in girdi kutular� tan�mlanm��t�r

//    public TextMeshProUGUI warningText; // Uyar� metni tan�mland�

//    public TYT_LessonData tytLessonData; // Ba�ka script'teki bir s�n�ftan bir nesne t�retildi

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
//        // 29-43. sat�rlarda kullan�c�dan al�nan de�erler tam say�ya �evrildi, ��nk� bunlar�n matematiksel i�lemlerde kullan�lmas� gerekiyor.

//        if (!IsValidInput(turkceCorrect, turkceWrong, turkceEmpty, 40) ||
//            !IsValidInput(sosyalCorrect, sosyalWrong, sosyalEmpty, 20) ||
//            !IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) ||
//            !IsValidInput(fenCorrect, fenWrong, fenEmpty, 20)) // Burada girilen de�erlerde bir yanl��l�k var ise uyar� metni g�ncellenir.
//        {
//            warningText.text = "L�tfen her ders i�in ge�erli de�erler girin. Toplam soru say�s� ve negatif de�erler kontrol edilmeli.";
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
//        // 55-69. sat�rlarda al�nan do�ru de�erlerin atamas� yap�l�r.

//        float tyt_ToplamNet = (tytLessonData.tytTurkceCorrectAnswers + tytLessonData.tytSosyalCorrectAnswers + tytLessonData.tytMatematikCorrectAnswers + tytLessonData.tytFenCorrectAnswers)
//            - (tytLessonData.tytTurkceWrongAnswers + tytLessonData.tytSosyalWrongAnswers + tytLessonData.tytMatematikWrongAnswers + tytLessonData.tytFenWrongAnswers) / 4.0f; // Toplam net hesab� yap�l�r.

//        TYT_DataManager.tytInstance.AddNet(tyt_ToplamNet); // Elde edilen net de�eri kaydedilir.

//        Debug.Log("Net added: " + tyt_ToplamNet); // Bu sat�r�n kodun �al��mas�na bir katk�s� yoktur, uygulaman�n geli�tirme a�amas�nda her �eyin yolunda gidip gitmedi�ini anlayabilmek i�in konsol ��kt�s� al�n�r.

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
    // 6-20. sat�rlarda TYT dersleri i�in girdi kutular� tan�mlanm��t�r

    public TextMeshProUGUI warningText; // Uyar� metni tan�mland�

    public TYT_LessonData tytLessonData; // Ba�ka script'teki bir s�n�ftan bir nesne t�retildi

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
        // 29-43. sat�rlarda kullan�c�dan al�nan de�erler tam say�ya �evrildi, ��nk� bunlar�n matematiksel i�lemlerde kullan�lmas� gerekiyor.

        if (turkceCorrect == -1 || turkceWrong == -1 || turkceEmpty == -1 ||
            sosyalCorrect == -1 || sosyalWrong == -1 || sosyalEmpty == -1 ||
            matematikCorrect == -1 || matematikWrong == -1 || matematikEmpty == -1 ||
            fenCorrect == -1 || fenWrong == -1 || fenEmpty == -1)
        {
            warningText.text = "L�tfen sadece say�sal de�erler girin.";
            return;
        }

        if (!IsValidInput(turkceCorrect, turkceWrong, turkceEmpty, 40) ||
            !IsValidInput(sosyalCorrect, sosyalWrong, sosyalEmpty, 20) ||
            !IsValidInput(matematikCorrect, matematikWrong, matematikEmpty, 40) ||
            !IsValidInput(fenCorrect, fenWrong, fenEmpty, 20)) // Burada girilen de�erlerde bir yanl��l�k var ise uyar� metni g�ncellenir.
        {
            warningText.text = "L�tfen her ders i�in ge�erli de�erler girin. Toplam soru say�s� ve negatif de�erler kontrol edilmeli.";
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
        // 55-69. sat�rlarda al�nan do�ru de�erlerin atamas� yap�l�r.

        float tyt_ToplamNet = (tytLessonData.tytTurkceCorrectAnswers + tytLessonData.tytSosyalCorrectAnswers + tytLessonData.tytMatematikCorrectAnswers + tytLessonData.tytFenCorrectAnswers)
            - (tytLessonData.tytTurkceWrongAnswers + tytLessonData.tytSosyalWrongAnswers + tytLessonData.tytMatematikWrongAnswers + tytLessonData.tytFenWrongAnswers) / 4.0f; // Toplam net hesab� yap�l�r.

        TYT_DataManager.tytInstance.AddNet(tyt_ToplamNet); // Elde edilen net de�eri kaydedilir.

        Debug.Log("Net added: " + tyt_ToplamNet); // Bu sat�r�n kodun �al��mas�na bir katk�s� yoktur, uygulaman�n geli�tirme a�amas�nda her �eyin yolunda gidip gitmedi�ini anlayabilmek i�in konsol ��kt�s� al�n�r.

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
}
