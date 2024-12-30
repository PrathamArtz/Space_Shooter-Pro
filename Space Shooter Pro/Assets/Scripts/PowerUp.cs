//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int PowerUpID;
   
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if(transform.position.y < -4.6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>(); //Communicate with Player Script.
            if (player != null)
            {

                switch (PowerUpID)
                {
                    case 0:
                        player.TrippleShotActive(); //Use Player Script Variable.
                        break;
                    case 1:
                        player.SpeedPowerUpActice();
                        break;
                    case 2:
                        Debug.Log("Sheilds");
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }

            }
            Destroy(this.gameObject);
        }
    }
}
