using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))] // Grafik çizgilerinin çizilebilmesi için, çizgilerin çizileceði Game Object'te Canvas Renderer bileþeninin bulunmasý gerekir. Bu satýrda bu zorunlu kýlýnýyor.
public class CustomLineDrawer : MaskableGraphic // Çizgilerin grafiðin sýnýrlarýndan taþmamasý gerektiðinden 
{
    public List<Vector2> points; // Netleri ifade eden noktalarý içerecek liste tanýmlanýr.
    
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear(); // Noktalarýn pozisyonlarý deðiþebildiðinden önce çizgiler temizlenir.

        if (points == null || points.Count < 2) // Nokta olmamasý ya da 1 tane olmasý durumunda çizgi çizilemeyeceðinden fonksiyondan çýkýlr.
            return;

        float width = 5f; // Çizginin kalýnlýðý belirlenir.
        Vector2 prev = points[0];

        for (int i = 1; i < points.Count; i++) // Bu döngüde sýrasý ile grafikteki ilk noktadan son noktaya kadar ardýþýk her iki nokta arasýna AddLineSegment() fonksiyonu ile çizgi çizilir.
        {
            Vector2 curr = points[i];
            AddLineSegment(vh, prev, curr, width);
            prev = curr;
        }
    }

    private void AddLineSegment(VertexHelper vh, Vector2 start, Vector2 end, float width) // Bu fonksiyon ardýþýk iki noktanýn bir çizgiyle birleþtirilmesini saðlar.
    {
        Vector2 dir = (end - start).normalized; // Ýki noktayý kullanarak yön vektörü elde edilir. Boyu 1 birimdir, yön belirtir.
        Vector2 normal = new Vector2(-dir.y, dir.x); // Ýki nokta araýndaki normal vektörü oluþturulur.

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
