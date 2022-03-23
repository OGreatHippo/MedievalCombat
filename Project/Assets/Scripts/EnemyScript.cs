using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private LimbHealth[] limbs;

    private int brokenLimbs;
    private float bleed;

    // Update is called once per frame
    void Update()
    {
        //print(brokenLimbs);
    }

    public float getBleedDamage(float damage)
    {
        return bleed = damage;
    }

    //private float setLimbHealth()
    //{
    //    for(int i = 0; i < limbs.Length; i++)
    //    {
    //        if (limbs[i].getBlackedOut() == false)
    //        {
    //            break;
    //        }

    //        return limbs[i].dealDamage(bleed / brokenLimbs);
    //    }
    //}
}
