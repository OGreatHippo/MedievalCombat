using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private float weaponMass;

    private bool showValues = true;
    private float acceleration;
    private float force;
    private float damage;

    private Vector3 mLastPosition;

    [SerializeField] private Text accelerationText;
    [SerializeField] private Text forceText;

    private void Update()
    {
        forceConversion();

        if(showValues)
        {
            accelerationText.gameObject.SetActive(true);
            accelerationText.text = "Acceleration = " + acceleration.ToString() + "m/s^2";

            forceText.gameObject.SetActive(true);
            forceText.text = "Force            = " + force.ToString() + "N";
        }

        else
        {
            accelerationText.gameObject.SetActive(false);
            forceText.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            showValues = !showValues;
        }
    }

    private float forceConversion()
    {
        acceleration = (transform.position - this.mLastPosition).magnitude / Time.deltaTime;

        this.mLastPosition = transform.position;

        force = weaponMass * acceleration;

        damage = force / 100;

        return damage;
    }
}

