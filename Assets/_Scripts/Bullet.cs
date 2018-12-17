using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public string shooterName;

	private int damage = 10;

	void OnCollisionEnter(Collision collision){
		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();
		var score = hit.GetComponent<Score>();

		if (health != null){
			health.TakeDamage(damage);
			score.AddScoreToShooter(damage, shooterName);
			Destroy(gameObject);
		}
	}

    public void SetShooter(string shooterNameFromInstantiation)
    {
        shooterName = shooterNameFromInstantiation;
    }
}
