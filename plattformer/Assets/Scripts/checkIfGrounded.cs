using UnityEngine;

public class checkIfGrounded : MonoBehaviour
{
    public PlayerController player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Foregorund"))
            player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Foregorund"))
            player.grounded = false;
    }
}