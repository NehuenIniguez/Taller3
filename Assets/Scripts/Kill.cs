using UnityEngine;

public class Kill : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            collision.gameObject.GetComponent<VidaPj>().Tomar_Daño(1f);
        }
    }
}
