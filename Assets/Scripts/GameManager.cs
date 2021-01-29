using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text dialogueTextSpace;
    public GameObject dialoguePannel;
    public Player playerScript;
    public Camera2DPlatformer gameCamScript;

    private void Awake() {
        if(instance == null)
            instance = this;
        
        playerScript = FindObjectOfType<Player>();
        gameCamScript = FindObjectOfType<Camera2DPlatformer>();

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
}
