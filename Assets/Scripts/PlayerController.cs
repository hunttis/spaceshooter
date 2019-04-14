using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
  public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

  public Rigidbody rb;
  public float speed;
  public float tilt;
  public Boundary boundary;
  public GameObject shot;
  public Transform shotSpawn;
  public float fireRate = 0.5f;
  private float nextFire = 0.0f;
  private AudioSource fireAudio;

  void Start() {
    rb = GetComponent<Rigidbody>();
    fireAudio = GetComponent<AudioSource>();
  }

  void FixedUpdate() {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    rb.velocity = movement * speed;

    rb.position = new Vector3(
      Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
      0.0f,
      Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
    );

    rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
  }
  
  void Update() {

    if (Input.GetButton("Fire1") && Time.time > nextFire) {
      nextFire = Time.time + fireRate;
      Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
      fireAudio.Play();
    }
  }

}
