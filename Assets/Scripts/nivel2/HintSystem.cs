using UnityEngine;
using TMPro;
using System.Collections;

public class HintSystem : MonoBehaviour
{
    public string[] hints; // Array de pistas
    public TextMeshProUGUI hintText; // Texto en el panel de diálogo
    public Canvas hintCanvas; // Canvas del panel de diálogo
    public float timemj;
    private int currentHintIndex = 0;

    void Start()
    {
        if (hints.Length > 0)
        {
            hintCanvas.gameObject.SetActive(false); // Inicialmente oculto
            StartCoroutine(ShowHints());
        }
    }

    void Update()
    {
        // Oculte el canvas si se presiona Enter
        if (Input.GetKeyDown(KeyCode.Return) && hintCanvas.gameObject.activeSelf)
        {
            hintCanvas.gameObject.SetActive(false);
            StopAllCoroutines(); // Detener la visualización actual de la pista
            StartCoroutine(WaitForNextHint()); // Iniciar espera de 60 segundos para la siguiente pista
        }
    }

    IEnumerator ShowHints()
    {
        while (currentHintIndex < hints.Length)
        {
            hintCanvas.gameObject.SetActive(true); // Mostrar el canvas
            hintText.text = hints[currentHintIndex];
            currentHintIndex++;
            
            yield return new WaitForSeconds(15); // Mostrar pista por 15 segundos

            hintCanvas.gameObject.SetActive(false); // Ocultar el canvas
            yield return WaitForNextHint(); // Espera 60 segundos para la siguiente pista
        }
    }

    IEnumerator WaitForNextHint()
    {
        yield return new WaitForSeconds(timemj); // Espera 60 segundos antes de la siguiente pista
        StartCoroutine(ShowHints());
    }
}
