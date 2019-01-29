using UnityEngine.SceneManagement;

public class RestartGameButtonController : ButtonController
{
    protected override void OnButtonClicked()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
