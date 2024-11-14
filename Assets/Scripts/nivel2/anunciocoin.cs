using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anunciocoin : MonoBehaviour
{
    public GameObject adPopup;
    public Image adImage;
    public Button adButton;
    public Button closeButton;
    public Text coinsText; // Asigna aquí el componente Text en el Inspector
    public List<Sprite> adPopupImages;
    public List<string> adPopupURLs;
    private int currentImageIndex = 0;
    public float timeBeforeCloseButtonAppears = 3f;
    private int coins = 0; // Contador de monedas

    void Start()
    {
        adPopup.SetActive(false);
        closeButton.gameObject.SetActive(false);

        adButton.onClick.AddListener(ShowAd);
        adImage.GetComponent<Button>().onClick.AddListener(OpenAdURL);
        closeButton.onClick.AddListener(CloseAd);
        
        UpdateCoinsText(); // Asegura que el texto esté actualizado al inicio
    }

    void ShowAd()
    {
        adPopup.SetActive(true);
        Time.timeScale = 0;

        if (adPopupImages.Count > 0)
        {
            adImage.sprite = adPopupImages[currentImageIndex];
        }

        closeButton.gameObject.SetActive(false);
        StartCoroutine(ShowCloseButtonAfterDelay());
    }

    IEnumerator ShowCloseButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(timeBeforeCloseButtonAppears);
        closeButton.gameObject.SetActive(true);
    }

    void CloseAd()
    {
        adPopup.SetActive(false);
        Time.timeScale = 1;

        currentImageIndex = (currentImageIndex + 1) % adPopupImages.Count;

        // Sumar monedas y actualizar el texto
        coins += 10;
        UpdateCoinsText();
    }

    void OpenAdURL()
    {
        if (adPopupURLs.Count > currentImageIndex)
        {
            string url = adPopupURLs[currentImageIndex];
            Application.OpenURL(url);
        }
    }

    void UpdateCoinsText()
    {
        coinsText.text = "Monedas: " + coins.ToString();
    }
}
