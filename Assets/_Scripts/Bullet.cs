using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public string shooterName;

	void OnCollisionEnter(Collision collision){
		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();

        Debug.Log(shooterName);

		if (health != null){
			health.TakeDamage(10);

			Destroy(gameObject);
		}
	}

    public void SetShooter(string shooterNameFromInstantiation)
    {
        shooterName = shooterNameFromInstantiation;
    }
}
