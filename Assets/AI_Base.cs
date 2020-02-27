using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Base : MonoBehaviour
{
    [SerializeField]
    public float Health;
    [SerializeField]
    public float Speed;
    [SerializeField]
    public float Attack;
    [SerializeField]
    public float CritChance;
    [SerializeField]
    public GameObject[] Player;
    [SerializeField]
    public float armored;
    

    private void Awake()
    {
        Player = GameObject.FindGameObjectsWithTag("Player");
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
    }

    public void Update()
    {
        if(Health<=0)
        {
            // drop Currency
            Destroy(gameObject);
        }
    }
}
