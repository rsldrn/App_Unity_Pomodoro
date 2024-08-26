using UnityEngine;

public class Pipes : MonoBehaviour
{
    // Boruların hareket hızı
    public float speed = 5f;
    
    // Boruların yok edileceği sol kenar
    private float leftEdge;

    private void Start()
    {
        // Sol kenarın x koordinatını hesapla ve biraz daha sola kaydır (3 birim)
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 3f;
    }

    private void Update()
    {
        // Boruları sola doğru hareket ettir
        transform.position += speed * Time.deltaTime * Vector3.left;

        // Boru sol kenarın ötesine geçtiyse boruyu yok et
        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }
}
