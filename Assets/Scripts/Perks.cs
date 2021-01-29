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

    [SerializeField] bool applyToSecond = true;
    [SerializeField] MinMax jumpUp = new MinMax(1.5f, 2.1f);
    [SerializeField] MinMax jumpDown = new MinMax(0.4f, 0.7f);

    [SerializeField] MinMax decayUp = new MinMax(1.8f, 3f);
    [SerializeField] MinMax decayDown = new MinMax(0.25f, 0.65f);
    private Player theScript;
    private float initialSpeed;
    private float initialJump;
    private float initialDecay;
    
    float timer = 0;
    [SerializeField] float timeToSwitch;

    List<string> inactivePerks = new List<string>{ "Speed", "Jump", "Dash", "Decay", "DoubleJump"};
    List<string> activePerks = new List<string>();

    private void Start() {
        theScript = GameManager.instance.playerScript;
        initialSpeed = theScript.speed;
        initialJump = theScript.jumpeForce;
        initialDecay = theScript.healthDcayRate;
        for (int i = 0; i < 3; i++)
        {
            PickPerk();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToSwitch)
        {
            Switch();
            timer = 0;
        }
    }

    private void Switch()
    {
        int rand = Random.Range(0, activePerks.Count);
        string toRemove = activePerks[rand];
        PickPerk();
        activePerks.Remove(toRemove);
        inactivePerks.Add(toRemove);
    }

    private void PickPerk() {
        int rand = Random.Range(0, inactivePerks.Count);
        switch (inactivePerks[rand])
        {
            case "Speed": 
                SpeedPerks();
                break;
            case "Jump":
                JumpPerks();
                break;
            case "DoubleJump":
                DoubleJump();
                break;
            case "Dash":
                DashingMan();
                break;
            case "Decay":
                DecayPerks();
                break;
        }
    }

    private void SpeedPerks() {
        inactivePerks.Remove("Speed");
        activePerks.Add("Speed");
        float picker = Random.Range(0.5f, 2.5f);
        if(picker > 1.2f) {
            //then increase perk
            picker = Random.Range(speedUp.min, speedUp.max);
        } else {
            //Then Decrease perk
            picker = Random.Range(speedDown.min, speedDown.max);
        }
        theScript.jumpeForce = initialSpeed * picker;
    }

    private void JumpPerks() {
        inactivePerks.Remove("Speed");
        activePerks.Add("Speed");
        float picker = Random.Range(0.5f, 2.5f);
        if(picker > 1.4f) {
            //then increase perk
            picker = Random.Range(jumpUp.min, jumpUp.max);
        } else {
            //Then Decrease perk
            picker = Random.Range(jumpDown.min, jumpDown.max);
        }
        float jumpeForces = initialJump * picker;
        theScript.jumpeForce = jumpeForces;
        if(applyToSecond)
            theScript.secondJumpeForce = jumpeForces;
    }

    private void DecayPerks() {
        inactivePerks.Remove("Decay");
        activePerks.Add("Decay");
        float picker = Random.Range(0.5f, 3.4f);
        if(picker > 0.4f && picker < 1.75f) {
            //then increase perk
            picker = Random.Range(decayUp.min, decayUp.max);
        } else if(picker >= 1.75f && picker < 2.2f) {
            //Then Heal
            picker = -1;
        } else if (picker >= 2.2f) {
            //then decrease perk
            picker = Random.Range(decayUp.min, decayUp.max);
        }
        theScript.healthDcayRate = initialDecay * picker;
    }

    private void DoubleJump() {
        inactivePerks.Remove("DoubleJump");
        activePerks.Add("DoubleJump");
        theScript.jumpenni = true;
    }

    private void DashingMan() {
        inactivePerks.Remove("Dash");
        activePerks.Add("Dash");
        theScript.dasher = true;
    }

    private void ResetStats() {
        theScript.speed = initialSpeed;
        theScript.jumpeForce = initialJump;
        if(applyToSecond)
            theScript.secondJumpeForce = initialJump;
        theScript.healthDcayRate = initialDecay;
        theScript.jumpenni = false;
        theScript.dasher = false;
    }
}
