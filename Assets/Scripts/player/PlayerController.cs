using UnityEngine;
using System.Collections;

/*
    Variables holding information such as ship speed, weapon type etc.
    are separated into classes for the sake of better representation
    in Unity's inspector view
*/
[System.Serializable]
public class Boundaries
{
    public float top, bottom, left, right;
}

[System.Serializable]
public class ShipStats
{
    /*
        This class will hold basic information about
        ship movement, health points, shield etc.
    */
    public float speed;
    public float rotationDegree;
    public int hitPoints;
}

[System.Serializable]
public class Weapons
{
    /*
        This class holds information about current
        used weapons

        Assuming there will be different weapon types 
        throughout the game a need was found for separating
        information about weapon to a class of its own
    */

    // laser gun
    public GameObject LaserType;

    // array of HardPoints mounting laser guns
    // in case laser is fired from more than one hard point
    public Transform LaserHP;

    // speed of a laser
    public float GunLaserSpeed;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform playerSprite;
    [SerializeField]
    private ParticleSystem p_engineThrust;

    // define controls
    [SerializeField]
    KeyCode fire;

    /*
        ----------------------
        | CURRENT SHIP STATS |
        ----------------------
    */

    private float shipSpeed = 0f;
    private float shipRotationDegree = 0f;

    private float projectileSpeed = 0f;
    private GameObject weapon = null;
    private Transform weaponHp;

    [SerializeField]
    private ParticleSystem hitPart;

    [SerializeField]
    Weapons weapons = new Weapons();
    [SerializeField]
    ShipStats stats = new ShipStats();
    [SerializeField]
    Boundaries boundaries = new Boundaries();

    void Awake()
    {
        // set stats
        this.shipSpeed = this.stats.speed;
        this.shipRotationDegree = this.stats.rotationDegree;

        this.projectileSpeed = this.weapons.GunLaserSpeed;
        this.weapon = this.weapons.LaserType;
        this.weaponHp = this.weapons.LaserHP;
    }

    void Update()
    {
        // get move direction based on user input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(horizontalInput,
            verticalInput);

        // move the ship
        this.transform.Translate(moveDirection.normalized * this.shipSpeed * Time.deltaTime);

        // calculate rotation based on movement direction (just a visual)
        if (horizontalInput > 0)
            this.playerSprite.eulerAngles = new Vector3(0, this.shipRotationDegree, 0);
        else if (horizontalInput < 0)
            this.playerSprite.eulerAngles = new Vector3(0, -this.shipRotationDegree, 0);
        else
            this.playerSprite.eulerAngles = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(fire))
        {
            shoot();
        }

        // apply movement restriction
        this.transform.position = this.restrictPosition();
    }

    private void shoot()
    {
        GameObject projectile = Instantiate(this.weapon, weaponHp.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * this.projectileSpeed);
    }

    // restrict movement within boundaries
    private Vector2 restrictPosition()
    {
        // Mathf.Clamp() keeps value between min and max given
        return new Vector2(Mathf.Clamp(this.transform.position.x, this.boundaries.left, this.boundaries.right),
            Mathf.Clamp(this.transform.position.y, this.boundaries.bottom, this.boundaries.top));
    }

    public void ApplyDamage()
    {
        this.stats.hitPoints -= GlobalStats.instance.enemyDamage;
        this.hitPart.gameObject.SetActive(true);
        this.Death();
    }

    public void Death()
    {
        if (this.stats.hitPoints <= 0)
        {
            Debug.Log("Player dead!");
            Score.selfInstance.SaveScore();
            Application.LoadLevel("menu");
        }
    }
}
