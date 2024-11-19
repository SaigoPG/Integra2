using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class TermsAndConditionsManager : MonoBehaviour
{
    public Toggle acceptToggle; // Asigna el Toggle de "Aceptar términos".
    public Button continueButton; // Botón para continuar.
    public TextMeshProUGUI warningMessage; // Mensaje de advertencia.
    private float timeSpentInScene;
    private bool isLoggedIn = false;
    public int scene;

    void Start()
    {
        warningMessage.gameObject.SetActive(false); // Ocultar el mensaje al inicio.
        timeSpentInScene = 0f;
        continueButton.onClick.AddListener(OnContinueClicked);
        LoginToPlayFab(); // Autenticar al jugador en PlayFab al iniciar.
    }

    void Update()
    {
        timeSpentInScene += Time.deltaTime; // Incrementa el tiempo en la escena.
    }

    void LoginToPlayFab()
    {
        // Genera un identificador único nuevo cada vez que inicie el juego
        string newCustomId = System.Guid.NewGuid().ToString();

        var request = new LoginWithCustomIDRequest
        {
            CustomId = newCustomId, // Nuevo ID único
            CreateAccount = true // Crear una nueva cuenta automáticamente
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    void OnLoginSuccess(LoginResult result)
    {
        isLoggedIn = true;
        Debug.Log("Login exitoso en PlayFab.");
    }

    void OnLoginFailure(PlayFabError error)
    {
        isLoggedIn = false;
        Debug.LogError("Error al iniciar sesión en PlayFab: " + error.GenerateErrorReport());
    }

    void OnContinueClicked()
    {
        if (!acceptToggle.isOn) // Verifica si no se ha aceptado.
        {
            warningMessage.text = "Debes aceptar los términos y condiciones.";
            warningMessage.gameObject.SetActive(true);
        }
        else
        {
            warningMessage.gameObject.SetActive(false);

            if (isLoggedIn)
            {
                SendTimeToPlayFab(); // Envía el tiempo en segundos a PlayFab.
            }
            else
            {
                Debug.LogError("El usuario no está autenticado. No se enviará información.");
            }

            // Cambia a la siguiente escena
            SceneManager.LoadScene(scene);
        }
    }

    void SendTimeToPlayFab()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new System.Collections.Generic.Dictionary<string, string>
            {
                { "TermsSceneTime", timeSpentInScene.ToString("F2") } // Guardar tiempo con dos decimales
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSendSuccess, OnDataSendError);
    }

    void OnDataSendSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Datos enviados exitosamente a PlayFab.");
    }

    void OnDataSendError(PlayFabError error)
    {
        Debug.LogError("Error al enviar datos a PlayFab: " + error.GenerateErrorReport());
    }
}
