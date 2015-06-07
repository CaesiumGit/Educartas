using UnityEngine;
using System.Collections;

public class MainMenuControl : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        Application.LoadLevel(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
