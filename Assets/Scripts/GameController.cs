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
        Application.Quit();
    }
}
