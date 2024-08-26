using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewAytLessonData", menuName = "AYT Lesson Data")]
public class AYT_LessonData : ScriptableObject
{
    public int aytSos1CorrectAnswers;
    public int aytSos1WrongAnswers;
    public int aytSos1EmptyAnswers;

    public int aytSos2CorrectAnswers;
    public int aytSos2WrongAnswers;
    public int aytSos2EmptyAnswers;

    public int aytMatematikCorrectAnswers;
    public int aytMatematikWrongAnswers;
    public int aytMatematikEmptyAnswers;

    public int aytFenCorrectAnswers;
    public int aytFenWrongAnswers;
    public int aytFenEmptyAnswers;

    public List<float> ayt_lastFiveNets = new List<float>();
}