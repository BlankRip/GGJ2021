using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            NextLevel();
    }

    public void NextLevel() {
        GameManager.instance.playerScript.LockMovement();
        GameManager.instance.fadeOut.gameObject.SetActive(true);
    }
}
