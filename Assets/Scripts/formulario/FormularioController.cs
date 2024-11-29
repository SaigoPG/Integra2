using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement; // Importar para cambiar de escena
using System.Collections.Generic;

public class FormularioController : MonoBehaviour
{
    public TMP_Dropdown dropdown1;
    public TMP_Dropdown dropdown2;
    public TMP_Dropdown dropdown3;
    public TMP_Dropdown dropdown4;
    public TMP_Dropdown dropdown5;
    public TMP_Dropdown dropdown6;
    public TMP_InputField comentarioInput; // Cambia a TMP_InputField para TextMeshPro
    public Button enviarButton;
    public int Escena;  

    void Start()
    {
        enviarButton.onClick.AddListener(OnEnviarButtonClick);
    }

    void OnEnviarButtonClick()
    {
        // Obtener las selecciones de los dropdowns
        int seleccion1 = dropdown1.value + 1;
        int seleccion2 = dropdown2.value + 1;
        int seleccion3 = dropdown3.value + 1;
        int seleccion4 = dropdown4.value + 1;
        int seleccion5 = dropdown5.value + 1;
        int seleccion6 = dropdown6.value + 1;
        string comentario = comentarioInput.text;

        // Imprimir los datos en la consola para verificación
        Debug.Log("Selección 1: " + seleccion1);
        Debug.Log("Selección 2: " + seleccion2);
        Debug.Log("Selección 3: " + seleccion3);
        Debug.Log("Selección 4: " + seleccion4);
        Debug.Log("Selección 5: " + seleccion5);
        Debug.Log("Selección 6: " + seleccion6);
        Debug.Log("Comentario: " + comentario);

        // Enviar los datos a PlayFab
        EnviarDatosAPlayFab(seleccion1, seleccion2, seleccion3, seleccion4, seleccion5, seleccion6, comentario);

        // Cambiar de escena después de enviar los datos
        CambiarEscena();
    }

    void EnviarDatosAPlayFab(int seleccion1, int seleccion2, int seleccion3, int seleccion4, int seleccion5, int seleccion6, string comentario)
    {
        // Crear un diccionario con los datos a enviar
        var data = new Dictionary<string, string>
        {
            { "Selección1", seleccion1.ToString() },
            { "Selección2", seleccion2.ToString() },
            { "Selección3", seleccion3.ToString() },
            { "Selección4", seleccion4.ToString() },
            { "Selección5", seleccion5.ToString() },
            { "Selección6", seleccion6.ToString() },
            { "Comentario", comentario }
        };

        // Llamada a PlayFab para actualizar los datos del usuario
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = data
        }, OnDatosEnviados, OnError);
    }

    // Callback cuando los datos se envían correctamente
    void OnDatosEnviados(UpdateUserDataResult result)
    {
        Debug.Log("Datos enviados correctamente a PlayFab.");
    }

    // Callback en caso de error al enviar los datos
    void OnError(PlayFabError error)
    {
        Debug.LogError("Error al enviar los datos a PlayFab: " + error.GenerateErrorReport());
    }

    void CambiarEscena()
    {
        // Cambiar a la escena especificada
        SceneManager.LoadScene(Escena);
    }
}
