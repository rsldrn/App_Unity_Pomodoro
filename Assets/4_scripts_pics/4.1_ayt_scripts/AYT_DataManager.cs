using System.Collections.Generic;
using UnityEngine;

public class AYT_DataManager : MonoBehaviour
{
    public static AYT_DataManager aytInstance { get; private set; }
    public List<float> aytLastFiveNets = new List<float>();

    private void Awake()
    {
        if (aytInstance == null)
        {
            aytInstance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddNet(float net)
    {
        if (aytLastFiveNets.Count >= 5)
        {
            aytLastFiveNets.RemoveAt(0);
        }
        aytLastFiveNets.Add(net);
        SaveData();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("AYT_NetCount", aytLastFiveNets.Count);
        for (int i = 0; i < aytLastFiveNets.Count; i++)
        {
            PlayerPrefs.SetFloat("AYT_Net" + i, aytLastFiveNets[i]);
            Debug.Log("Saved AYT_Net" + i + ": " + aytLastFiveNets[i]);
        }
        PlayerPrefs.Save();
        Debug.Log("Data saved");
    }

    public void LoadData()
    {
        aytLastFiveNets.Clear();
        int count = PlayerPrefs.GetInt("AYT_NetCount", 0);
        Debug.Log("Loading data. AYT_NetCount: " + count);
        for (int i = 0; i < count; i++)
        {
            //aytLastFiveNets.Add(PlayerPrefs.GetFloat("Net" + i, 0));

            float value = PlayerPrefs.GetFloat("AYT_Net" + i, 0);
            aytLastFiveNets.Add(value);
            Debug.Log("Loaded AYT_Net" + i + ": " + value);

        }
        Debug.Log("Total AYT_Nets Loaded: " + string.Join(", ", aytLastFiveNets));
    }
}
