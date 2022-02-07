using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbHealth : MonoBehaviour
{
    [SerializeField] private float maxLimbHealth = 40f;
    [SerializeField] private float currentLimbHealth = 40f;

    [SerializeField] private float limbHealthPercent;
    [SerializeField] private float colourChange;

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

        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    currentLimbHealth -= 6f;
        //}
    }

    public float getHealth()
    {
        return currentLimbHealth;
    }

    public void setHealth(float hp)
    {
        currentLimbHealth -= hp;
    }
}
