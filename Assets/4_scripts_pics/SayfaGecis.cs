using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string sceneName) // Bu fonksiyon kendisine parametre olarak verilen sahneye ge�i�i sa�lar.
    {
        SceneManager.LoadScene(sceneName);
    }
}
