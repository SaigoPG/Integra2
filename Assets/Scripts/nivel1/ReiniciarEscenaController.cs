using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarEscenaController : MonoBehaviour
{
    public void ReiniciarEscena()
    {
        // Carga la escena actual desde el principio
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CambiarAScena(int escenaIndex)
    {
        SceneManager.LoadScene(escenaIndex);
    }
}
