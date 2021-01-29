using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject fadeOutPanel;

    public void PlayButton() {
        fadeOutPanel.SetActive(true);
    }

    public void QuitButton() {
        Application.Quit();
    }
}
