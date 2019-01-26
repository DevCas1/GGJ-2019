using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<CrashingObject>()) gameObject.SetActive(false);
    }
}