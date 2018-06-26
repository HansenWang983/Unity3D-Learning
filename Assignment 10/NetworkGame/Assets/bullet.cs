using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var hitCombat = hit.GetComponent<Combat>();
        if (hitCombat != null)
        {
            hitCombat.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
