using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MinMax {
    public float min;
    public float max;

    public MinMax() {
        min = max = 0;
    }

    public MinMax(float mini, float maxi) {
        min = mini;
        max = maxi;
    }
}

public class Perks : MonoBehaviour
{
    [SerializeField] MinMax speedUp = new MinMax(1.6f, 3.3f);
    [SerializeField] MinMax speedDown = new MinMax(0.5f, 0.75f);
    private Player theScript;
    private float initialSpeed;
    private float initialJump;
    private float initialDecay;

    private void Start() {
        theScript = GameManager.instance.playerScript;
        initialSpeed = theScript.speed;
        initialJump = theScript.jumpeForce;
        initialDecay = theScript.healthDcayRate;
    }

    private void SpeedPerks() {
        float picker = Random.Range(0.5f, 2.5f);
        if(picker > 1.3) {
            //then increase perk
            picker = Random.Range(speedUp.min, speedUp.max);
        } else {
            //Then Decrease perk
            picker = Random.Range(speedDown.min, speedDown.max);
        }
        theScript.speed = initialSpeed * picker;        
    }

    private void ResetStats() {
        theScript.speed = initialSpeed;
        theScript.jumpeForce = initialJump;
        theScript.healthDcayRate = initialDecay;
        theScript.jumpenni = false;
        theScript.dasher = false;
    }
}
