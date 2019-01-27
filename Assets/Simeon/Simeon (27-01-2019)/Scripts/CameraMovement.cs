using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraMovementSpeed = 4;
    public float YDistance = 0.5f;
    public float ZDistanceAdjust = 1;
    public float middlePosAdjust = 0.5f;

    public Transform penguin;
    public Transform bear;

    float X_TargetDistance()
    {
        if (penguin.position.x < bear.position.x) return penguin.position.x - bear.position.x;
        else if (penguin.position.x > bear.position.x) return bear.position.x - penguin.position.x;
        return penguin.position.x - bear.position.x;
    }
    float Y_TargetDistance()
    {
        return penguin.position.y - bear.position.y; 
    }   

    Vector3 TargetPos()
    {
        return new Vector3(0, YDistance, Mathf.Clamp((X_TargetDistance() - ZDistanceAdjust) + Y_TargetDistance(), -20, -3));
    }

    void Update()
    {
        Vector3 middlePos = Vector3.Lerp(penguin.position, bear.position, middlePosAdjust);
        transform.position = Vector3.Lerp(transform.position, middlePos + TargetPos(), cameraMovementSpeed * Time.deltaTime);
    }
}
