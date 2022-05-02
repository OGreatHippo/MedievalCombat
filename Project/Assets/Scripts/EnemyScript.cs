using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private LimbHealth[] limbs;

    private float bleed;

    // Update is called once per frame
    private void Update()
    {
        dealBleedDamage();
    }

    public float getBleedDamage(float damage)
    {
        return bleed = damage;
    }

    private void dealBleedDamage()
    {
        float damage;

        damage = bleed;

        for (int i = 0; i < limbs.Length; i++)
        {
            limbs[i].dealDamage(damage * Time.deltaTime);
            
            if(limbs[i].name == "Head" && limbs[i].getHealth() <= 0 || limbs[i].name == "Thorax" && limbs[i].getHealth() <= 0)
            {
                killEnemy();
            }

            if(limbs[i] == null)
            {
                return;
            }
        }
    }

    private void killEnemy()
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            limbs[i].dealDamage(1000);
        }
    }
}
