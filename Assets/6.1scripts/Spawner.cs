using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Spawn edilecek prefab
    public GameObject prefab;
    
    // Spawn oranı ve yükseklik sınırları
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 1f;

    private void OnEnable()
    {
        // Spawn fonksiyonunu belirli aralıklarla çağır
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        // Spawn fonksiyonunu iptal et
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        // Prefabı instantiate et (oluştur)
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);

        // Yeni prefabın pozisyonunu rastgele bir yükseklik ile ayarla
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
