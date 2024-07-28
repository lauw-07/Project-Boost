using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(1, 0, 0);
    public float smoothSpeed = 0.125f;

    void LateUpdate() {
        Vector3 desiredPos = new Vector3(player.position.x, transform.position.y, transform.position.z) + offset;

        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
    }
}
