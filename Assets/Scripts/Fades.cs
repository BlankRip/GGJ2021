using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fades : MonoBehaviour
{
    [SerializeField] bool finalScene;
    [SerializeField] bool menuScene;
    [HideInInspector] public bool retry;

    private void SceneSwitch() {
        if(menuScene) {
            SceneManager.LoadScene(1);
        } else if(retry) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else {
            if(!finalScene) {
                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(nextSceneIndex);
            } else
                SceneManager.LoadScene(0);
        }
    }

    private void LockMovement() {
        if(GameManager.instance != null)
            GameManager.instance.playerScript.LockMovement();
    }

    private void SceneSwitch(int index) {
        SceneManager.LoadScene(index);
    }

    private void Deactivate() {
        if(GameManager.instance != null)
            GameManager.instance.playerScript.UnlockMovement();
        gameObject.SetActive(false);
    }
}
