using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject[] limbs;

    // Start is called before the first frame update
    void Start()
    {
        limbs = GameObject.FindGameObjectsWithTag("EnemyLimb");

        for (int i = 0; i < limbs.Length; i++)
        {
            maxHealth += gameObject.GetComponentInChildren<LimbHealth>().getHealth();
        }

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
