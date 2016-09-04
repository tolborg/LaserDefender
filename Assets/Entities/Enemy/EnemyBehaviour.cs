using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150f;
	public float projectileSpeed = 1;
	public GameObject projectile;
	public float shotsPerSeconds = 0.5f;


	void Update() {
		float probability = Time.deltaTime * shotsPerSeconds;

		if (Random.value < probability) {
			Fire();
		}
	}


	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
	}

	void OnTriggerEnter2D(Collider2D collider) {

		Projectile missile = collider.gameObject.GetComponent<Projectile>();

		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();

			if (health <= 0) {
				Destroy(gameObject);
			}

		}
	}
		

}
