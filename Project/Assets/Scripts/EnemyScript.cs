using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private List<GameObject> limbs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < limbs.Count; i++)
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
