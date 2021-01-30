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
    [Header("Speed Stuff")]
    [SerializeField] GameObject sUpObj;
    [SerializeField] GameObject sDownObj;
    [SerializeField] MinMax speedUp = new MinMax(1.6f, 3.3f);
    [SerializeField] MinMax speedDown = new MinMax(0.5f, 0.75f);

    [Header("Jump Stuff")]
    [SerializeField] bool applyToSecond = true;
    [SerializeField] GameObject jUpObj;
    [SerializeField] GameObject jDownObj;
    [SerializeField] MinMax jumpUp = new MinMax(1.5f, 2.1f);
    [SerializeField] MinMax jumpDown = new MinMax(0.4f, 0.7f);

    [Header("Decay Stuff")]
    [SerializeField] GameObject dUpObj;
    [SerializeField] GameObject dDownObj;
    [SerializeField] GameObject hpGenObj;
    [SerializeField] MinMax decayUp = new MinMax(1.8f, 3f);
    [SerializeField] MinMax decayDown = new MinMax(0.25f, 0.65f);

    [Header("Ability Stuff")]
    [SerializeField] GameObject djObj;
    [SerializeField] GameObject ddObj;

    [Header("Spawn Locals")]
    [SerializeField] GameObject[] slots = new GameObject[3];

    [Header("Other Requirements")]
    [SerializeField] float timeToSwitch;
    [SerializeField] AudioClip perkSwitch;

    private Player theScript;
    private float initialSpeed;
    private float initialJump;
    private float initialDecay;
    
    float timer = 0;

    List<string> inactivePerks = new List<string>{ "Speed", "Jump", "Dash", "Decay", "DoubleJump"};
    string[] activePerks = new string[3];
    private GameObject[] slottedIn = new GameObject[3];

    private void Start() {
        theScript = GameManager.instance.playerScript;
        initialSpeed = theScript.speed;
        initialJump = theScript.jumpeForce;
        initialDecay = theScript.healthDcayRate;
        for (int i = 0; i < 3; i++)
        {
            PickPerk(i);
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
        for (int i = 0; i < 3; i++)
        {
            string needRemove = activePerks[i];
            PickPerk(i);
            ResetStats(needRemove);
            inactivePerks.Add(needRemove);
        }

        GameManager.instance.playerScript.playerSFXSRC.PlayOneShot(perkSwitch);
    }   

    private void SwithSlotUI(int slotId, GameObject obj) {
        obj.transform.position = slots[slotId].transform.position;
        slottedIn[slotId]?.SetActive(false);
        slottedIn[slotId] = obj;
        slottedIn[slotId]?.SetActive(true);
    }

    private void PickPerk(int slotId) {
        int rand = Random.Range(0, inactivePerks.Count);
        switch (inactivePerks[rand])
        {
            case "Speed": 
                SpeedPerks(slotId);
                break;
            case "Jump":
                JumpPerks(slotId);
                break;
            case "DoubleJump":
                DoubleJump(slotId);
                break;
            case "Dash":
                DashingMan(slotId);
                break;
            case "Decay":
                DecayPerks(slotId);
                break;
        }
    }

    private void SpeedPerks(int slotId) {
        inactivePerks.Remove("Speed");
        //activePerks.Add("Speed");
        activePerks[slotId] = "Speed";
        float picker = Random.Range(0.5f, 2.5f);
        if(picker > 1.2f) {
            //then increase perk
            SwithSlotUI(slotId, sUpObj);
            picker = Random.Range(speedUp.min, speedUp.max);
        } else {
            //Then Decrease perk
            SwithSlotUI(slotId, sDownObj);
            picker = Random.Range(speedDown.min, speedDown.max);
        }
        theScript.speed = initialSpeed * picker;
    }

    private void JumpPerks(int slotId) {
        inactivePerks.Remove("Jump");
        //activePerks.Add("Jump");
        activePerks[slotId] = "Jump";
        float picker = Random.Range(0.5f, 2.5f);
        if(picker > 1.4f) {
            //then increase perk
            SwithSlotUI(slotId, jUpObj);
            picker = Random.Range(jumpUp.min, jumpUp.max);
        } else {
            //Then Decrease perk
            SwithSlotUI(slotId, jDownObj);
            picker = Random.Range(jumpDown.min, jumpDown.max);
        }
        float jumpeForces = initialJump * picker;
        theScript.jumpeForce = jumpeForces;
        if(applyToSecond)
            theScript.secondJumpeForce = jumpeForces;
    }

    private void DecayPerks(int slotId) {
        inactivePerks.Remove("Decay");
        //activePerks.Add("Decay");
        activePerks[slotId] = "Decay";
        float picker = Random.Range(0.5f, 3.4f);
        if(picker > 0.4f && picker < 1.75f) {
            //then increase perk
            SwithSlotUI(slotId, dUpObj);
            picker = Random.Range(decayUp.min, decayUp.max);
        } else if(picker >= 1.75f && picker < 2.2f) {
            //Then Heal
            SwithSlotUI(slotId, hpGenObj);
            picker = -1;
        } else if (picker >= 2.2f) {
            //then decrease perk
            SwithSlotUI(slotId, dDownObj);
            picker = Random.Range(decayUp.min, decayUp.max);
        }
        theScript.healthDcayRate = initialDecay * picker;
    }

    private void DoubleJump(int slotId) {
        inactivePerks.Remove("DoubleJump");
        activePerks[slotId] = "DoubleJump";
        //activePerks.Add("DoubleJump");
        SwithSlotUI(slotId, djObj);
        theScript.jumpenni = true;
    }

    private void DashingMan(int slotId) {
        inactivePerks.Remove("Dash");
        activePerks[slotId] = "Dash";
        //activePerks.Add("Dash");
        SwithSlotUI(slotId, ddObj);
        theScript.dasher = true;
    }

    private void ResetStats(string theThingy) {
        switch(theThingy) {
            case "Speed": 
                theScript.speed = initialSpeed;
                break;
            case "Jump":
                theScript.jumpeForce = initialJump;
                if(applyToSecond)
                    theScript.secondJumpeForce = initialJump;
                break;
            case "DoubleJump":
                theScript.jumpenni = false;
                break;
            case "Dash":
                theScript.dasher = false;
                break;
            case "Decay":
                theScript.healthDcayRate = initialDecay;
                break;
        }
    }
}
