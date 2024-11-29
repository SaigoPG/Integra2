using UnityEngine;
using UnityEngine.SceneManagement;  

public class CambiarEscenaTrigger : MonoBehaviour
{
    public int Escena;  
    private bool estaEnTrigger = false;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaEnTrigger = true;  // El jugador está dentro del trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del trigger, desactivar la condición de presionar "E"
        if (other.CompareTag("Player"))
        {
            estaEnTrigger = false;
        }
    }

    private void Update()
    {
        // Verificar si el jugador está dentro del trigger y presiona la tecla "E"
        if (estaEnTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Cambiar la escena al nombre especificado
            SceneManager.LoadScene(Escena);
        }
    }
}
