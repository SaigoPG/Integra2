using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform puntoPanel1;
    public Transform puntoPanel2;
    public Transform puntoPanel3;

    private Transform objetivoActual;
    public float velocidadMovimiento = 5f;

    void Start()
    {
        // Inicia con el primer punto como objetivo
        objetivoActual = puntoPanel1;
    }

    void Update()
    {
        // Mueve la cámara suavemente hacia el objetivo actual
        if (objetivoActual != null)
        {
            transform.position = Vector3.Lerp(transform.position, objetivoActual.position, velocidadMovimiento * Time.deltaTime);
        }
    }

    public void IrAPanel2()
    {
        // Cambia el objetivo al punto del panel 2
        objetivoActual = puntoPanel2;
    }

    public void IrAPanel3()
    {
        // Cambia el objetivo al punto del panel 3
        objetivoActual = puntoPanel3;
    }
}
