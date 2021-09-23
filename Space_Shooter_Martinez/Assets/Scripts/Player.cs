using System.Collections; //namespace Unity default
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variable requirements * public or private reference, data type (int, float, bool, string), a name. Optional value assigned.
    [SerializeField] //allows for changing in the inspector
    private float _speed = 2.5f; //float is a number a decimal and we're creating the _speed variable with a 2.5 value
    //create a prefab variable for the lazer
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    void Start() // Start is called before the first frame update
    {
        transform.position = new Vector3(0, 0, 0); //access transform component to set new position to 0, 0, 0
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()    // Update is called once per frame
    {
        PlayerMovement();
        //if the space button is pushed then the lazer gets instantiated (created)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }


    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //defining the horizontal input and using Unity default names.
        float verticalInput = Input.GetAxis("Vertical"); //define verticle input variable using Unity default name.

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime); //new Vector3 (1, 0, 0) * player horizontal input *speed variable * real time
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); //create transform.Translate for vertical input.

        //if the player's y value is > 5 or < -5 the player will stop at those locations
        if (transform.position.y >= 5f)
        {
            transform.position = new Vector3(transform.position.x, 5f, 0);
        }
        else if (transform.position.y <= -5f)
        {
            transform.position = new Vector3(transform.position.x, -5f, 0);
        }

        //if the player's x value is > 13 or < -13 the player well stop at those locations
        if (transform.position.x >= 9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.5f)

        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
    }
    public void Damage()
    {
        _lives -= 1;
        _uiManager.UpdateLives(_lives); //links tu UI Manager to update the current lives

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}

/*
        Optimizatized Options
       combine variables: transform.Translate(new Vector3(horizontalInput, verticalInput, verticalInput, 0) * _speed *Time.deltaTime);

       or combine variablesand create a new diretion variable:
       Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
       transform.Translate((direction * _speed *Time.deltaTime);           */
