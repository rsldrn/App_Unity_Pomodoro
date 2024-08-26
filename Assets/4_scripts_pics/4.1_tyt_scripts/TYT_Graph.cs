using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro kullanýmý için

public class TYT_Graph : MonoBehaviour
{
   
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private GameObject linePrefab;  // Çizgi prefab'ý referansý
    [SerializeField] private GameObject textPrefab;  // Metin prefab'ý referansý
    private RectTransform graphContainer;
    private List<GameObject> circleList = new List<GameObject>();
    private List<GameObject> lineList = new List<GameObject>();  // Çizgiler için liste
    private List<GameObject> textList = new List<GameObject>();  // Metinler için liste
    private CustomLineDrawer lineDrawer;

    private void Start()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        lineDrawer = graphContainer.gameObject.AddComponent<CustomLineDrawer>();

        //// Çizgi rengini burada ayarlayýn
        //lineDrawer.color = new Color32(0x1D, 0x26, 0x3B, 0xFF);  // #1D263B rengini ayarlayýn

        // Çizgi rengini burada ayarlayýn
        lineDrawer.color = new Color32(0x78, 0x72, 0xDE, 0xFF);  // #7872DE rengini ayarlayýn

        
        RefreshGraph();
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(50, 50);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void UpdateGraph(List<float> values)
    {
        // Eski öðeleri temizle
        foreach (GameObject circle in circleList)
        {
            Destroy(circle);
        }
        foreach (GameObject line in lineList)
        {
            Destroy(line);
        }
        foreach (GameObject text in textList)
        {
            Destroy(text);
        }

        circleList.Clear();
        lineList.Clear();
        textList.Clear();

        float xSpacing = 150f;
        List<Vector2> positions = new List<Vector2>();

        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = xSpacing * (i + 1);
            float yPosition = values[i] * 7f + 18;
            GameObject circle = CreateCircle(new Vector2(xPosition, yPosition));
            circleList.Add(circle);

            // Convert the anchored position to local position
            Vector2 anchoredPos = new Vector2(xPosition, yPosition);
            positions.Add(anchoredPos);

            // Çizgi oluþturma
            GameObject line = Instantiate(linePrefab, graphContainer);
            RectTransform lineRectTransform = line.GetComponent<RectTransform>();
            lineRectTransform.anchoredPosition = new Vector2(-472, yPosition - 485);
            lineList.Add(line);

            // Metin oluþturma
            GameObject text = Instantiate(textPrefab, graphContainer);
            RectTransform textRectTransform = text.GetComponent<RectTransform>();
            textRectTransform.anchoredPosition = new Vector2(xPosition - 488, yPosition - 445);
            TextMeshProUGUI textComponent = text.GetComponent<TextMeshProUGUI>();
            textComponent.text = values[i].ToString("F2"); // Net deðeri yazma
            textList.Add(text);
        }

        lineDrawer.points = positions;
        lineDrawer.SetVerticesDirty();  // Çizgiyi yeniden çizmeyi tetikle
    }
    public void RefreshGraph()
    {
        if (TYT_DataManager.tytInstance != null && TYT_DataManager.tytInstance.tytLastFiveNets != null)
        {
            Debug.Log("Graph update called with data: " + string.Join(", ", TYT_DataManager.tytInstance.tytLastFiveNets));
            UpdateGraph(TYT_DataManager.tytInstance.tytLastFiveNets);
        }
        else
        {
            Debug.Log("TYT_DataManager.tytInstance or tytLastFiveNets is null");
        }
    }
}