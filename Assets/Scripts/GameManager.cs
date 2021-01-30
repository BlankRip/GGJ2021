using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text dialogueTextSpace;
    public GameObject dialoguePannel;
    public Fades fadeOut;
    public Player playerScript;
    public Camera2DPlatformer gameCamScript;

    private void Awake() {
        if(instance == null)
            instance = this;
        
        playerScript = FindObjectOfType<Player>();
        gameCamScript = FindObjectOfType<Camera2DPlatformer>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver() {
        Debug.Log("<color=red>GAME-OVER</color>");
        fadeOut.retry = true;
        fadeOut.gameObject.SetActive(true);
    }
}
