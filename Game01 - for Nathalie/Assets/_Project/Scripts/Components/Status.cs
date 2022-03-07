using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float health;

    public void TakeDamage(float value)
    {
        health -= value;
        GetComponent<Animator>().SetTrigger("hit");
    }
}
