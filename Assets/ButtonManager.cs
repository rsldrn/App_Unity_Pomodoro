using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject mainButton;
    public GameObject[] otherButtons;

    private void Start()
    {
        HideOtherButtons();
    }

    public void ShowOtherButtons()
    {
        foreach (GameObject button in otherButtons)
        {
            button.SetActive(true);
        }
    }

    public void HideOtherButtons()
    {
        foreach (GameObject button in otherButtons)
        {
            button.SetActive(false);
        }
    }
}
