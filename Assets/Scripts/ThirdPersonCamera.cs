using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float distance = -7f;
    public float height = 6f;
    public float smoothSpeed = 5f;

    void LateUpdate()
  {
        Vector3 desiredPosition = player.position - player.forward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        Vector3 lookAtPosition = player.position;


        transform.LookAt(lookAtPosition);


        // transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }
}
