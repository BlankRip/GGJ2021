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

        fadeOutPanel.SetActive(true);
    }
}
