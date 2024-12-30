using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private float _speedMultiplyer = 2f;
    [SerializeField]
    private float _lives = 3f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.3f;
    [SerializeField]
    private float _nextFire = 0f;
    private SpawnManager _spawnManager;
    private bool _isTrippleShotActive = false;
    private bool _isSpeedPowerUpActive = false;
    [SerializeField]
    private int _score = 0;
    private UIManager _uiManager;
    


    void Start()
    {
        Debug.Log(_isSpeedPowerUpActive);
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();


        if (_spawnManager != null)
        {
            Debug.LogError("The SpawnManager is null.");
        }

        if (_uiManager != null)
        {
            Debug.LogError("The Ui Manager is null.");
        }
    }

    void Update()
    {
        movement();

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            fireLaser();
        }
        
    }

    void movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        /* if (_isSpeedPowerUpActive == false)
         {
             transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

         }
         else
         {
             transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * ( _speed * _speedMultiplyer ) * Time.deltaTime);
         }*/

        /*
        //transform.Translate(Vector3.right * horizontalInput  * speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);*/

        playerBounds();
        
    }

    void playerBounds()
    {
        if (transform.position.y > 6f)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0);
        }
        else if (transform.position.y < -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0);
        }
        else if (transform.position.x < -9.612812f)
        {
            transform.position = new Vector3(9.431809f, transform.position.y, 0);
        }
        else if (transform.position.x > 9.431809f)
        {
            transform.position = new Vector3(-9.612812f, transform.position.y, 0);
        }
    }

    void fireLaser()
    {
            _nextFire = Time.time + _fireRate;

        if(_isTrippleShotActive == true)
        {
            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        } 
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.07f, 0), Quaternion.identity);
        }
        // "Quaternion.identity" =  This quaternion corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
    }

    public void Damage()
    {
       /* All three of these implementations is exact same Functionality.
        _lives = _lives - 1;
        _lives -= 1; */
        _lives--;

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }

    public void TrippleShotActive ()
    {
        _isTrippleShotActive = true;
        
        StartCoroutine(TrippleShotPowerDownRoutine()); // you can also use ""StartCoroutine("TrippleShotPowerDownRoutine");""
        //it start the coroutine method
    }

    IEnumerator TrippleShotPowerDownRoutine()    // this is a coroutine method 
    {
        yield return new WaitForSeconds(5.0f); // it wait for seconds.
        _isTrippleShotActive = false;
    }

    public void SpeedPowerUpActice()
    {
        _isSpeedPowerUpActive = true;
        _speed *= _speedMultiplyer;
        StartCoroutine(SpeedPowerUpRoutine());
    }

    IEnumerator SpeedPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedPowerUpActive = false;
        _speed /= _speedMultiplyer;
    }

}
