using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Transform playerTransform;
    private float followSpeed = 0.01f;

    private Vector3 velocity = Vector3.zero;

	void Start ()
    {
        playerTransform = GameObject.Find("Player").transform;
	}


    void LateUpdate()
    {
        Vector3 targetPos = playerTransform.position;
        targetPos.y = transform.position.y;
        targetPos.z = playerTransform.position.z - 10;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, followSpeed);
    }
}
