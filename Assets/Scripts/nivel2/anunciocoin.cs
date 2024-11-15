using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class anunciocoin : MonoBehaviour
{
    public GameObject adPopup;
    public Image adImage;
    public Button adButton;
    public Button closeButton;
    public TextMeshProUGUI timeText; // Cambiado a TextMeshProUGUI para mostrar el tiempo
    public List<Sprite> adPopupImages;
    public List<string> adPopupURLs;
    private int currentImageIndex = 0;
    public float timeBeforeCloseButtonAppears = 3f;
    private float tiempoPausado; // Almacena el tiempo al mostrar el anuncio
    private float tiempoTotal; // Tiempo total acumulado incluyendo extras

    void Start()
    {
        if (adPopup != null) adPopup.SetActive(false);
        closeButton.gameObject.SetActive(false);

        adButton.onClick.AddListener(ShowAd);
        adImage.GetComponent<Button>().onClick.AddListener(OpenAdURL);
        closeButton.onClick.AddListener(CloseAd);

        tiempoTotal = Time.time; // Inicializa el tiempo total con el tiempo de juego actual
        UpdateTimeText();
    }

    void ShowAd()
    {
        adPopup.SetActive(true);
        tiempoPausado = tiempoTotal; // Captura el tiempo total actual antes de pausar
        Time.timeScale = 0; // Pausa el tiempo del juego

        if (adPopupImages.Count > 0)
        {
            adImage.sprite = adPopupImages[currentImageIndex];
        }

        closeButton.gameObject.SetActive(false);
        StartCoroutine(ShowCloseButtonAfterDelay());
    }

    IEnumerator ShowCloseButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(timeBeforeCloseButtonAppears); // Usa tiempo real para que el botón aparezca incluso con el juego en pausa
        closeButton.gameObject.SetActive(true);
    }

    void CloseAd()
    {
        adPopup.SetActive(false);
        Time.timeScale = 1; // Reanuda el tiempo del juego

        currentImageIndex = (currentImageIndex + 1) % adPopupImages.Count;

        // Sumar 10 segundos al tiempo pausado
        tiempoTotal = tiempoPausado + 10f; // Añade 10 segundos al tiempo capturado antes de la pausa
        UpdateTimeText(); // Actualizar el texto
    }

    void OpenAdURL()
    {
        if (adPopupURLs.Count > currentImageIndex)
        {
            string url = adPopupURLs[currentImageIndex];
            Application.OpenURL(url);
        }
    }

    void UpdateTimeText()
    {
        timeText.text = "Tiempo Total: " + tiempoTotal.ToString("F1") + " segundos";
        Debug.Log("Texto de tiempo actualizado a: " + timeText.text); // Verifica que el texto se actualiza
    }
}
