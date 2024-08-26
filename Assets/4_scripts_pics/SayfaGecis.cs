using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string sceneName) // Bu fonksiyon kendisine parametre olarak verilen sahneye geçiþi saðlar.
    {
        SceneManager.LoadScene(sceneName);
    }
}
