using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float playerSpeed = 8.0f;

    private float speed;
    private int weaponType;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;
    public bool shieldUp;

    private float horizontalScreenLimit = 11.3f;
    private float verticalScreenLimit = 8f;

    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    void Start()
    {
        playerSpeed = 6f;
        // This function is called at the start of the game
        speed = 5.0f;
        weaponType = 1;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    void Update()

    {
        //This function is called every frame; 60 frames/second
        Movement();
    }
    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.PlaySound(2);
    }

    IEnumerator ShieldPowerDown()
    {
        shieldUp = false;
        yield return new WaitForSeconds(3f);
        shieldPrefab.SetActive(false);
        gameManager.PlaySound(2);
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    //Picked up speed
                    speed = 10f;
                    StartCoroutine(SpeedPowerDown());
                    thrusterPrefab.SetActive(true);
                    break;
                case 2:
                    weaponType = 2; //Picked up double weapon
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 3:
                    weaponType = 3; //Picked up triple weapon
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 4:
                    //Picked up shield
                    StartCoroutine(ShieldPowerDown());
                    shieldPrefab.SetActive(true);
                    if (!shieldUp)
                    {
                        shieldUp = true;
                    }
                    break;
            }
        }
    }

    void Movement()
    {
        // Read the input from the player

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y > 0 || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
    }
    }
