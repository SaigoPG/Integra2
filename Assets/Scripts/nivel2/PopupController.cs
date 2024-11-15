using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public GameObject canvasPopup; // Canvas de los pop-ups
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public Button continueButtonPanel3;
    public Button continueButtonPanel4;
    public float initialDelay = 60f; // Tiempo de espera inicial para mostrar el canvas
    public float buttonDelay = 3f; // Tiempo antes de mostrar el botón "Continuar"
    public float popupHideDelay = 5f; // Tiempo de espera para volver a mostrar el pop-up

    void Start()
    {
        canvasPopup.SetActive(false); // Inicialmente, el canvas está oculto
        StartCoroutine(ShowPopupAfterInitialDelay());
    }

    IEnumerator ShowPopupAfterInitialDelay()
    {
        yield return new WaitForSeconds(initialDelay); // Espera 60 segundos antes de mostrar el canvas
        ShowPanel1(); // Muestra el primer panel
    }

    public void ShowPanel1()
    {
        SetActivePanel(panel1);
        canvasPopup.SetActive(true); // Activa el canvas al mostrar el primer panel
    }

    public void ShowPanel2()
    {
        SetActivePanel(panel2);
    }

    public void ShowPanel3()
    {
        SetActivePanel(panel3);
        StartCoroutine(ShowButtonAfterDelay(continueButtonPanel3));
        PauseGame();
    }

    public void ShowPanel4()
    {
        SetActivePanel(panel4);
        StartCoroutine(ShowButtonAfterDelay(continueButtonPanel4));
        PauseGame();
    }

    private void SetActivePanel(GameObject activePanel)
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        activePanel.SetActive(true);
    }

    IEnumerator ShowButtonAfterDelay(Button button)
    {
        button.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(buttonDelay);
        button.gameObject.SetActive(true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pausa el tiempo del juego
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo del juego
    }

    public void OnContinueButtonPanel3()
    {
        ResumeGame();
        StartCoroutine(HidePopupAndShowAgain());
    }

    public void OnContinueButtonPanel4()
    {
        ResumeGame();
        StartCoroutine(HidePopupAndShowAgain());
    }

    private IEnumerator HidePopupAndShowAgain()
    {
        canvasPopup.SetActive(false); // Oculta el canvas de los pop-ups
        yield return new WaitForSeconds(popupHideDelay); // Espera antes de volver a mostrar el pop-up
        ShowPanel1(); // Muestra el primer panel de nuevo o ajusta el flujo según lo que necesites
    }
}
