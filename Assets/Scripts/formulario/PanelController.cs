using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelController : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public TMP_Dropdown dropdownPregunta;
    public Button botonSiguiente;
    public Button botonSiguientePanel3; // Nuevo botón para pasar del Panel 2 al Panel 3
    public CameraController cameraController; // Referencia al controlador de la cámara

    void Start()
    {
        // Inicializa los paneles
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);

        // Asigna las funciones a los botones
        botonSiguiente.onClick.AddListener(VerificarRespuestaYAvanzar);
        botonSiguientePanel3.onClick.AddListener(IrAPanel3); // Asigna la función para pasar al Panel 3
    }

    void VerificarRespuestaYAvanzar()
    {
        string respuestaSeleccionada = dropdownPregunta.options[dropdownPregunta.value].text;

        if (respuestaSeleccionada == "Si")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(true);
            cameraController.IrAPanel3();
        }
        else
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel3.SetActive(false);
            cameraController.IrAPanel2();
        }
    }

    public void IrAPanel3()
    {
        // Cambia al tercer panel y mueve la cámara al punto correspondiente
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
        cameraController.IrAPanel3();
    }
}
