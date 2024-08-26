using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))] // Grafik �izgilerinin �izilebilmesi i�in, �izgilerin �izilece�i Game Object'te Canvas Renderer bile�eninin bulunmas� gerekir. Bu sat�rda bu zorunlu k�l�n�yor.
public class CustomLineDrawer : MaskableGraphic // �izgilerin grafi�in s�n�rlar�ndan ta�mamas� gerekti�inden 
{
    public List<Vector2> points; // Netleri ifade eden noktalar� i�erecek liste tan�mlan�r.
    
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear(); // Noktalar�n pozisyonlar� de�i�ebildi�inden �nce �izgiler temizlenir.

        if (points == null || points.Count < 2) // Nokta olmamas� ya da 1 tane olmas� durumunda �izgi �izilemeyece�inden fonksiyondan ��k�lr.
            return;

        float width = 5f; // �izginin kal�nl��� belirlenir.
        Vector2 prev = points[0];

        for (int i = 1; i < points.Count; i++) // Bu d�ng�de s�ras� ile grafikteki ilk noktadan son noktaya kadar ard���k her iki nokta aras�na AddLineSegment() fonksiyonu ile �izgi �izilir.
        {
            Vector2 curr = points[i];
            AddLineSegment(vh, prev, curr, width);
            prev = curr;
        }
    }

    private void AddLineSegment(VertexHelper vh, Vector2 start, Vector2 end, float width) // Bu fonksiyon ard���k iki noktan�n bir �izgiyle birle�tirilmesini sa�lar.
    {
        Vector2 dir = (end - start).normalized; // �ki noktay� kullanarak y�n vekt�r� elde edilir. Boyu 1 birimdir, y�n belirtir.
        Vector2 normal = new Vector2(-dir.y, dir.x); // �ki nokta ara�ndaki normal vekt�r� olu�turulur.

        Vector2 v0 = start - normal * width;
        Vector2 v1 = start + normal * width;
        Vector2 v2 = end - normal * width;
        Vector2 v3 = end + normal * width;

        int idx = vh.currentVertCount;

        vh.AddVert(v0, color, new Vector2(0, 0));
        vh.AddVert(v1, color, new Vector2(0, 1));
        vh.AddVert(v2, color, new Vector2(1, 0));
        vh.AddVert(v3, color, new Vector2(1, 1));

        vh.AddTriangle(idx, idx + 1, idx + 2);
        vh.AddTriangle(idx + 2, idx + 1, idx + 3);
    }
}
