using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject gameOverUI;

    private void Awake()
    {
        ActivateMenu(menuUI);
    }

    public void ActivateMenu(GameObject menu)
    {
        DeactivateAllMenus();
        menu.SetActive(true);
    }
    private void DeactivateAllMenus()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        pauseUI.SetActive(false);
        winUI.SetActive(false);
        gameOverUI.SetActive(false);
    }
}