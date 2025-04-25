using UnityEngine;

public class DisparoJefe : MonoBehaviour
{
    [SerializeField] private Transform ControlDisparo;
    [SerializeField] private GameObject bala;
    [SerializeField] private float Velocidadbala = 5f;
    [SerializeField] private float tiempoEntreDisparos = 1f;

    private float tiempoUltimoDisparo;

    private void Update()
    {
        Disparo();
    }

    public void Disparo()
    {
        if (Time.time - tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            float[] angulos = { -30f, 0f, 30f }; // abanico de 3 balas

            foreach (float angulo in angulos)
            {
                Quaternion rotacion = Quaternion.Euler(0, 0, angulo);
                GameObject proyectil = Instantiate(bala, ControlDisparo.position, ControlDisparo.rotation * rotacion);

                Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direccion = (ControlDisparo.rotation * rotacion) * Vector2.down;
                    rb.linearVelocity = direccion.normalized * Velocidadbala;
                }
            }

            tiempoUltimoDisparo = Time.time;
        }
    }
}
