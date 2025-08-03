using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class SceneChangeScript : MonoBehaviour
{
    public void LoadMainSceneByName()
    {
        SceneManager.LoadScene("MainScene"); // Replace "MainScene" with the actual name of your main scene
    }

    // Method to load the main scene by index
    public void LoadMainSceneByIndex()
    {
        SceneManager.LoadScene(1); // Replace 1 with the actual build index of your main scene
    }
    public void LoadInstructionsSceneByIndex()
    {
        SceneManager.LoadScene(2); // Replace 1 with the actual build index of your main scene
    }
    public void LoadMenusSceneByIndex()
    {
        SceneManager.LoadScene(0); // Replace 1 with the actual build index of your main scene
    }
}
