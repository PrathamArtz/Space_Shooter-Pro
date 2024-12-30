using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8.0f;

    void Update()
    {
        transform.Translate(Vector3.up * _speed *  Time.deltaTime);

        if (transform.position.y > 8f)
        {

            if(transform.parent != null) // it checks the gameobject has parent.
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}
