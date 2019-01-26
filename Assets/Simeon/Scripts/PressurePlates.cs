using UnityEngine;

public class PressurePlates : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Penguin_Movement>()) col.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up, col.transform.position, ForceMode.Force);
    }
}
