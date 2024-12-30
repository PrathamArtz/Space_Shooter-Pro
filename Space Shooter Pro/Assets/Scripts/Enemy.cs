using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speedOfEnemy = 4.0f;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player != null)
        {
            Debug.LogError("Player is null.");
        }
        _animator = GetComponent<Animator>();
        if (_animator != null)
        {
            Debug.LogError("Animator is null.");
        }

        _boxCollider2D = GetComponent<BoxCollider2D>();
        

    }
    void Update()
    {
        transform.Translate (Vector3.down * _speedOfEnemy * Time.deltaTime);

        //Instantiate(this.gameObject, transform.position,);

        if(transform.position.y < -5.6f)
        {
            float randomX = Random.Range(9f, -9.5f);
            transform.position = new Vector3( randomX, 7.6f , 0f);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
           // other.transform.GetComponent<Player>().Damage();
           
           // Player player = other.transform.GetComponent<Player>();
            if(_player != null)
            {
                //player.Damage();
                _player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _boxCollider2D.isTrigger = false;
            Destroy(this.gameObject , 1.5f);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
           
            if (_player != null)
            {
                _player.AddScore();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _boxCollider2D.isTrigger = false;
            _boxCollider2D.enabled = false;
            Destroy(this.gameObject , 1.5f);
        }
    }
}
