using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

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

    void Start()
    {
        enviarButton.onClick.AddListener(OnEnviarButtonClick);
    }

    void OnEnviarButtonClick()
    {
        int seleccion1 = dropdown1.value + 1;
        int seleccion2 = dropdown2.value + 1;
        int seleccion3 = dropdown3.value + 1;
        int seleccion4 = dropdown4.value + 1;
        int seleccion5 = dropdown5.value + 1;
        int seleccion6 = dropdown6.value + 1;
        string comentario = comentarioInput.text;

        Debug.Log("Selección 1: " + seleccion1);
        Debug.Log("Selección 2: " + seleccion2);
        Debug.Log("Selección 3: " + seleccion3);
        Debug.Log("Selección 4: " + seleccion4);
        Debug.Log("Selección 5: " + seleccion5);
        Debug.Log("Selección 6: " + seleccion6);
        Debug.Log("Comentario: " + comentario);

        // Aquí puedes agregar la lógica para almacenar o enviar los datos
    }
}
