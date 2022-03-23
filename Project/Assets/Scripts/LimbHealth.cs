using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbHealth : MonoBehaviour
{
    [SerializeField] private float maxLimbHealth = 40f;
    [SerializeField] private float currentLimbHealth = 40f;
    private float bleed;
    private float bleedAmount;
    private float currentBleed;

    private float limbHealthPercent;
    private float colourChange;

    private Renderer objRenderer;
    private bool bleeding = false;

    // Start is called before the first frame update
    private void Start()
    {
        currentLimbHealth = maxLimbHealth;
        objRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        changeColour();

        if (bleeding)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().emissionRate = bleedAmount;
            dealDamage(bleed * Time.deltaTime);
        }

        //if(currentLimbHealth <= 0)
        //{
        //    gameObject.GetComponentInParent<EnemyScript>().getBleedDamage(currentLimbHealth);
        //}
    }

    public float getHealth()
    {
        return currentLimbHealth;
    }

    public bool getBlackedOut()
    {
        return currentLimbHealth <= 0;
    }

    public void dealDamage(float damage)
    {
        if (damage >= maxLimbHealth)
        {
            bleed = 100f;
            bleedAmount = 100f;
            currentBleed = bleedAmount;
            bleeding = true;
            gameObject.transform.parent = null;
        }

        if (currentBleed < 100f)
        {
            if (damage > maxLimbHealth / 2 && damage < maxLimbHealth)
            {
                bleed = 1f;
                bleedAmount = 10f;
                currentBleed = bleedAmount;
                bleeding = true;
            }

            if (currentBleed < 10f)
            {
                if (damage > maxLimbHealth / 4! & damage < maxLimbHealth / 2)
                {
                    bleed = 0.1f;
                    bleedAmount = 1f;
                    currentBleed = bleedAmount;
                    bleeding = true;
                }
            }
        }

        currentLimbHealth -= damage;
    }

    private void changeColour()
    {
        limbHealthPercent = currentLimbHealth / maxLimbHealth * 100;

        colourChange = limbHealthPercent / 100;

        Color c = Color.Lerp(Color.red, Color.green, colourChange);

        objRenderer.material.color = c;

        if (currentLimbHealth <= 0)
        {
            objRenderer.material.color = Color.black;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            dealDamage(collision.gameObject.GetComponent<WeaponScript>().getDamage());
        }
    }
}
