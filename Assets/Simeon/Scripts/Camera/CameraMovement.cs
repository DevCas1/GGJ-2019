using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float lerpSpeed = 4;
    public float YDistance = 0.5f;

    public Transform target1;
    public Transform target2;

    float X_TargetDistance()
    {
        return target1.position.x - target2.position.x;
    }
    float Y_TargetDistance()
    {
        return target1.position.y - target2.position.y;
    }

    Vector3 TargetPos()
    {
        return new Vector3(0, YDistance, Mathf.Clamp(-X_TargetDistance() / 2 + (Y_TargetDistance() / 2), -20, -3));
    }

    void Update()
    {
        Vector3 middlePos = Vector3.Lerp(target1.position, target2.position, 0.5f);
        transform.position = Vector3.Lerp(transform.position, middlePos + TargetPos(), lerpSpeed * Time.deltaTime);
    }
}
