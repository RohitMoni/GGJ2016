using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public void StartGameButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGameButton()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }
}
