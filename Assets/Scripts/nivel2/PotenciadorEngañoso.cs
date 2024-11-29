using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Text.RegularExpressions;  // Necesario para Expresiones Regulares

public class PotenciadorEngañoso : MonoBehaviour
{
    public GameObject uiPanelCorreo; 
    public TMP_InputField inputCorreo; // Para ingresar el correo
    public TMP_Text mensajeInfo; // Mensaje informativo sobre el correo

    private bool haInteractuado = false; // Controla si el formulario ya se ha mostrado

    public GameObject otroCanvas; // Canvas que debe desactivarse cuando se activa el formulario

    private void OnTriggerEnter(Collider other)
    {
        // Activar solo la primera vez que el jugador entra en el trigger
        if (other.CompareTag("Player") && !haInteractuado)
        {
            if (otroCanvas != null)
            {
                otroCanvas.SetActive(false); // Desactiva otro Canvas si es necesario
            }

            // Mostrar el formulario de correo
            mensajeInfo.text = "¡Para reclamar la tarjeta, ingresa tu correo electrónico!";
            uiPanelCorreo.SetActive(true);

            Time.timeScale = 0;  // Pausar el juego para permitir la interacción

            haInteractuado = true; // Evitar que se active más de una vez
        }
    }

    private bool ValidarCorreo(string correo)
    {
        // Expresión regular para validar correos de Gmail o Hotmail
        string patronCorreo = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com)$";
        return Regex.IsMatch(correo, patronCorreo);
    }

    // Función para cerrar el formulario (botón Cerrar)
    public void CerrarFormulario()
    {
        uiPanelCorreo.SetActive(false); // Cierra el formulario
        Time.timeScale = 1; // Reanuda el juego
    }

    public void Continuar()
    {
        string correo = inputCorreo.text; // Obtener el correo ingresado

        // Verificar si el correo es válido
        if (ValidarCorreo(correo))
        {
            // Registrar el correo en PlayFab
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> { { "CorreoRegistrado", correo } }
            }, OnCorreoRegistrado, OnError);

            // Mostrar mensaje de agradecimiento
            mensajeInfo.text = "¡Gracias por registrar tu correo!";
            uiPanelCorreo.SetActive(false); // Cerrar el formulario
            Time.timeScale = 1; // Reanudar el juego
        }
        else
        {
            // Mostrar un mensaje de error si el correo no es válido
            mensajeInfo.text = "Por favor, ingresa un correo válido de Gmail o Hotmail.";
        }
    }

    // Callback para cuando el correo se registre correctamente en PlayFab
    private void OnCorreoRegistrado(UpdateUserDataResult result)
    {
        Debug.Log("Correo registrado exitosamente en PlayFab.");
        // Puedes agregar lógica adicional aquí si es necesario
    }

    // Callback para manejar errores en el registro del correo en PlayFab
    private void OnError(PlayFabError error)
    {
        Debug.LogError("Error al registrar el correo: " + error.GenerateErrorReport());
    }
}
