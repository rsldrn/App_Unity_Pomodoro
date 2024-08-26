using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Paralaks efektinin hızını belirleyen değişken
    public float animationSpeed = 1f;

    // MeshRenderer referansı
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        // MeshRenderer bileşenini al
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Texture offsetini güncelleyerek paralaks efektini uygula
        // X ekseninde texture offsetini animationSpeed ile çarparak ve deltaTime ile çarparak kaydır
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
