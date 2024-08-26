using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;  //Anket sonucunun ekranda gözükebilmesi için TextMeshPro ögesi kullanýldý.

    void Start()
    {
        //Total score'un yüzdelik olarak gösterilmesi için gereken hesaplamalar yapýldý, int türüne çevrilip score deðiþkenine atandý. 
        int score = Convert.ToInt32(SurveyData.totalScore * 85.0f / 21.0f);  //Anket sonucunda stres seviyesinin %100 þeklinde gözükmesinin 
                                                                             //yanýltýcý bir etkisi olacaðý düþünüldüðü için maksimum stress seviyesi %85 çýkacak þekilde ayarlanmýþtýr.
        resultText.text = (score).ToString() + "%"; //score deðiþkeni, string ifadeye çevrilip resulText'e "%" iþareti eklenerek atanýr.
    }
}