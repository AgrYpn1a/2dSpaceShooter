using UnityEngine;
using System.Collections;

public class AutoDestructParticles : MonoBehaviour
{
    // when game object becomes active
    [SerializeField]
    private bool DeactivateOnly;

    void OnEnable()
    {
        StartCoroutine(CheckIfAlive());
    }

    IEnumerator CheckIfAlive()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (!this.GetComponent<ParticleSystem>().IsAlive(true))
            {
                if (this.DeactivateOnly)
                {
                    this.gameObject.SetActive(false);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
