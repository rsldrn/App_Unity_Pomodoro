using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;  //Anket sonucunun ekranda g�z�kebilmesi i�in TextMeshPro �gesi kullan�ld�.

    void Start()
    {
        //Total score'un y�zdelik olarak g�sterilmesi i�in gereken hesaplamalar yap�ld�, int t�r�ne �evrilip score de�i�kenine atand�. 
        int score = Convert.ToInt32(SurveyData.totalScore * 85.0f / 21.0f);  //Anket sonucunda stres seviyesinin %100 �eklinde g�z�kmesinin 
                                                                             //yan�lt�c� bir etkisi olaca�� d���n�ld��� i�in maksimum stress seviyesi %85 ��kacak �ekilde ayarlanm��t�r.
        resultText.text = (score).ToString() + "%"; //score de�i�keni, string ifadeye �evrilip resulText'e "%" i�areti eklenerek atan�r.
    }
}