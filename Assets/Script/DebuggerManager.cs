using UnityEngine;
using UnityEngine.SceneManagement;

public class DebuggerManager : Singleton<DebuggerManager>
{




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene("EnigmaXXXX");

        if (Input.GetKeyDown(KeyCode.J))
            SceneManager.LoadScene("Liquid");

        if (Input.GetKeyDown(KeyCode.O))
            SceneManager.LoadScene("Puzzle");
    }
}
