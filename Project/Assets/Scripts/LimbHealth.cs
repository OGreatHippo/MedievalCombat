using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 40f;
    [SerializeField] private float currentHealth = 40f;

    [SerializeField] private float healthPercent;
    [SerializeField] private float colourChange;

    private Renderer objRenderer;
    private bool blackedOut = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        objRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        healthPercent = currentHealth / maxHealth * 100;

        colourChange = healthPercent / 100; 

        Color c = Color.Lerp(Color.red, Color.green, colourChange);

        objRenderer.material.color = c;

        if(currentHealth <= 0)
        {
            objRenderer.material.color = Color.black;
            blackedOut = true;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            currentHealth -= 6f;
        }
    }
}
