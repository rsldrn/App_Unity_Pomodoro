using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTytLessonData", menuName = "TYT Lesson Data")]
public class TYT_LessonData : ScriptableObject
{
    public int tytTurkceCorrectAnswers;
    public int tytTurkceWrongAnswers;
    public int tytTurkceEmptyAnswers;

    public int tytMatematikCorrectAnswers;
    public int tytMatematikWrongAnswers;
    public int tytMatematikEmptyAnswers;

    public int tytFenCorrectAnswers;
    public int tytFenWrongAnswers;
    public int tytFenEmptyAnswers;

    public int tytSosyalCorrectAnswers;
    public int tytSosyalWrongAnswers;
    public int tytSosyalEmptyAnswers;

    public List<float> tyt_lastFiveNets = new List<float>();
}