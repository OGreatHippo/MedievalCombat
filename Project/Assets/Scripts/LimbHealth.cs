using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbHealth : MonoBehaviour
{
    [SerializeField] private float maxLimbHealth = 40f;
    [SerializeField] private float currentLimbHealth = 40f;

    private float limbHealthPercent;
    private float colourChange;

    private Renderer objRenderer;
    private bool blackedOut = false;

    // Start is called before the first frame update
    void Start()
    {
        currentLimbHealth = maxLimbHealth;
        objRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        limbHealthPercent = currentLimbHealth / maxLimbHealth * 100;

        colourChange = limbHealthPercent / 100; 

        Color c = Color.Lerp(Color.red, Color.green, colourChange);

        objRenderer.material.color = c;

        if(currentLimbHealth <= 0)
        {
            objRenderer.material.color = Color.black;
            blackedOut = true;
        }
    }

    public float getHealth()
    {
        return currentLimbHealth;
    }

    private void setHealth(float hp)
    {
        currentLimbHealth -= hp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            setHealth(collision.gameObject.GetComponent<WeaponScript>().getDamage());

            if(collision.gameObject.GetComponent<WeaponScript>().getDamage() >= maxLimbHealth)
            {
                print("DECAPITATIOOOOOOOOOOON");
            }

            if (collision.gameObject.GetComponent<WeaponScript>().getDamage() > maxLimbHealth / 2 && collision.gameObject.GetComponent<WeaponScript>().getDamage() < maxLimbHealth)
            {
                print("MANY BLOOD! HANDLE IT!");
            }

            if (collision.gameObject.GetComponent<WeaponScript>().getDamage() > maxLimbHealth / 4 !& collision.gameObject.GetComponent<WeaponScript>().getDamage() < maxLimbHealth / 2)
            {
                print("Blood,  blood, BLOOD! ... and bits of sick");
            }

            print(collision.gameObject.GetComponent<WeaponScript>().getDamage());
        }
    }
}
