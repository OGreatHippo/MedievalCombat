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
    private bool blackedOut = false;
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
        limbHealthPercent = currentLimbHealth / maxLimbHealth * 100;

        colourChange = limbHealthPercent / 100;

        Color c = Color.Lerp(Color.red, Color.green, colourChange);

        objRenderer.material.color = c;

        if (currentLimbHealth <= 0)
        {
            objRenderer.material.color = Color.black;
            blackedOut = true;
        }

        if(bleeding)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().emissionRate = bleedAmount;
            setHealth(bleed * Time.deltaTime);
        }
    }

    public float getHealth()
    {
        return currentLimbHealth;
    }

    public void setHealth(float hp)
    {
        currentLimbHealth -= hp;
    }

    public bool getBlackedOut()
    {
        return blackedOut;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            setHealth(collision.gameObject.GetComponent<WeaponScript>().getDamage());

            if(collision.gameObject.GetComponent<WeaponScript>().getDamage() >= maxLimbHealth)
            {
                bleed = 100f;
                bleedAmount = 100f;
                currentBleed = bleedAmount;
                bleeding = true;
                gameObject.transform.parent = null;
                print("DECAPITATIOOOOOOOOOOON");
            }

            if(currentBleed < 100f)
            {
                if (collision.gameObject.GetComponent<WeaponScript>().getDamage() > maxLimbHealth / 2 && collision.gameObject.GetComponent<WeaponScript>().getDamage() < maxLimbHealth)
                {
                    bleed = 1f;
                    bleedAmount = 10f;
                    currentBleed = bleedAmount;
                    bleeding = true;
                    print("MANY BLOOD! HANDLE IT!");
                }

                if(currentBleed < 10f)
                {
                    if (collision.gameObject.GetComponent<WeaponScript>().getDamage() > maxLimbHealth / 4! & collision.gameObject.GetComponent<WeaponScript>().getDamage() < maxLimbHealth / 2)
                    {
                        bleed = 0.1f;
                        bleedAmount = 1f;
                        currentBleed = bleedAmount;
                        bleeding = true;
                        print("Blood,  blood, BLOOD! ... and bits of sick");
                    }
                }
            }
            print(collision.gameObject.GetComponent<WeaponScript>().getDamage());
        }
    }
}
