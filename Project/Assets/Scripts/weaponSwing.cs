using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwing : MonoBehaviour
{
    private Vector3 mOffset;

    private float mZCoord;
    private void OnMouseDown()
    {
        mOffset = transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
}
