using UnityEngine;

public class Deathzone : MonoBehaviour
{
    public MainMenu menu;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Animal>()) menu.YouLose();
    }
}
