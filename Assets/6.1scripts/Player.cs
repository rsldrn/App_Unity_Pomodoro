using UnityEngine;

public class Player : MonoBehaviour
{
    // SpriteRenderer referansı ve sprite dizisi
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    
    // Oyuncunun hareket yönü
    private Vector3 direction;
    
    // Yerçekimi ve oyuncunun zıplama gücü
    public float gravity = -9.8f;
    public float strength = 5f;

    private void Awake()
    {
        // SpriteRenderer bileşenini al
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Sprite animasyonunu başlat
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }
    
    private void OnEnable()
    {
        // Oyuncunun başlangıç pozisyonunu ve yönünü ayarla
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        // Boşluk tuşuna veya fareye tıklama ile zıplama
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
        }

        // Dokunmatik giriş ile zıplama
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                direction = Vector3.up * strength;
            }
        }

        // Yerçekimini uygula ve pozisyonu güncelle
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        // Sprite indeksini arttır
        spriteIndex++;
        
        // Eğer sprite dizisinin sonuna gelindiyse başa dön
        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        // SpriteRenderer bileşenini güncelle
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Engel ile çarpışma kontrolü
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        // Skor artırma bölgesi ile çarpışma kontrolü
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
