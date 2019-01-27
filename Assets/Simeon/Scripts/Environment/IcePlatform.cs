using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    public GameObject soundObj;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<CrashingObject>())
        {
            Instantiate(soundObj);
            gameObject.SetActive(false);
        }
    }
}