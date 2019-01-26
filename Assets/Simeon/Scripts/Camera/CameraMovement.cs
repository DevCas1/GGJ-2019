using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float lerpSpeed = 4;
    public float YDistance = 1.5f;
    public float ZDistance = 10;

    public Transform target;

    Vector3 TargetPos()
    {
        return new Vector3(target.position.x, target.position.y + YDistance, target.position.z + - ZDistance);
    }

	void Update () {
        transform.position = Vector3.Lerp(transform.position, TargetPos(), lerpSpeed * Time.deltaTime);
	}
}
