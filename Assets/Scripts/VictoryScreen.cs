using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] GameObject fadeOutPanel;
    [SerializeField] VideoClip creditsClip;

    private void Start() {
        StartCoroutine(TriggerOnVideoEnd());
    }

    private IEnumerator TriggerOnVideoEnd() {
        yield return new WaitForSeconds((float)creditsClip.length);
        //Setting game progress to be game completed
        if(ProgressTracker.instance != null)
            ProgressTracker.instance.GameCompleted();

        fadeOutPanel.SetActive(true);
    }
}
