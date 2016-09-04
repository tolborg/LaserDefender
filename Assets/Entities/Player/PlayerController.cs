﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 250f;


	float xmin;
	float xmax;

	void Start() {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}


	void Fire() {
		Vector3 offset = new Vector3(0f, 1f, 0);
		GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
	}

	void Update() {

		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.0001f, firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}


		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}


		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);



	
	}


	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log("Player hit!");

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
