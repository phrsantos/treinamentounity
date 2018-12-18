using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Score shooter;

	private int damage = 10;

	void OnCollisionEnter(Collision collision){
		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();

		if (health != null){
			health.TakeDamage(damage);
            shooter.AddScoreToShooter(damage);
			Destroy(gameObject);
		}
	}

    public void SetShooter(Score shooterScore)
    {
        shooter = shooterScore;
    }
}
