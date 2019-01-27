using UnityEngine;

public class EndZone : MonoBehaviour
{
    public MainMenu menu;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Animal>()) menu.YouWin();
    }
}
