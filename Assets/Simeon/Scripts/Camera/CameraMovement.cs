using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float lerpSpeed = 4;
    public float YDistance = 1.5f;
    public float ZDistance = 10;

    public Transform target1;
    public Transform target2;

    float X_TargetDistance()
    {
        return target1.position.x - target2.position.x;
    }

    Vector3 TargetPos()
    {
        return new Vector3(0, YDistance, Mathf.Clamp(-ZDistance - X_TargetDistance(), -15, -5));
    }

    void Update()
    {
        Vector3 middlePos = Vector3.Lerp(target1.position, target2.position, 0.5f);
        transform.position = Vector3.Lerp(transform.position, middlePos + TargetPos(), lerpSpeed * Time.deltaTime);
    }
}
