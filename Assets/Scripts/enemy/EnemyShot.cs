using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour
{
    /*
        Similar to Laser class, this will cause damage to player
    */
    [SerializeField]
    private string bottomBorderTag;
    [SerializeField]
    private string playerTag;

    void OnTriggerEnter2D(Collider2D other)
    {
        /*
            OnTriggerEnter2D() will get the information about the object
            projectile has collided with and save it in variable 'other' 

            Then we compare tags in order to identify the object 'other'
        */

        // if projectile hits top border, we want it to be destroyed
        if (other.gameObject.tag == bottomBorderTag)
        {
            Destroy(this.gameObject);
            return;
        }
        // however, if projectile hits enemy we want to apply damage
        if (other.gameObject.tag == playerTag)
        {
            // apply damage
            other.GetComponent<PlayerController>().ApplyDamage();
            Destroy(this.gameObject);
            return;
        }
    }
}
