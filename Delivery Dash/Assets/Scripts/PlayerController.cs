using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private float moveSpeed = 20;
    private float rotationSpeed = 20;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        Drive();
	}

    void Drive()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        Vector3.Normalize(movement);
        transform.position += movement;

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
        }
    }
}
