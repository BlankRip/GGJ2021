using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpikeTrap : GeneralTraps
{
    [SerializeField] float damage = 100;

    protected override void TrapMovement() { }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            GameManager.instance.playerScript.Damaged(damage);
        }
    }
}
