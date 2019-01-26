using UnityEngine;

public class Swimarea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Penguin_Movement>()) other.GetComponent<Penguin_Movement>().EnterSwimZone();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Penguin_Movement>()) other.GetComponent<Penguin_Movement>().ExitSwimZone();
    }
}
