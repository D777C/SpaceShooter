using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //speed variable
    [SerializeField]
    private float _speed = 4.0f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Enemy move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if the enemy goes off the screen it respawns at a random location
        if (transform.position.y <= -7f)
        {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is other player it destroys the enemy and damages the player
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        //if other is the laser destroy the enemy and destroy the laser
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        {

        }
    }
}
