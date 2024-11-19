using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : FadeMenu
{
    public int escene;
    public void StartBtn()
    {
        //AudioManager.Instance.EmitEffect("BtnSound");
        SceneManager.LoadScene(escene);
    }
}
