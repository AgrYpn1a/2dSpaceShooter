using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private string topBorderTag = "";
    [SerializeField]
    private string enemyTag = "";

    // when a laser projectile collides with something, we have to
    // figure out what it was, and then depending on the result
    // do the particular action
    void OnTriggerEnter2D(Collider2D other)
    {
        /*
            OnTriggerEnter2D() will get the information about the object
            projectile has collided with and save it in variable 'other' 

            Then we compare tags in order to identify the object 'other'
        */

        // if projectile hits top border, we want it to be destroyed
        if (other.gameObject.tag == topBorderTag)
        {
            Destroy(this.gameObject);
            return;
        }
        // however, if projectile hits enemy we want to apply damage
        if (other.gameObject.tag == enemyTag)
        {
            // apply damage
            other.GetComponent<IEnemyAI>().ApplyDamage();
            Destroy(this.gameObject);
            return;
        }
    }
}
