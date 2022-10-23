using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    [SerializeField]
    protected string levelName;

    public void LoadLevel() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(levelName);
    }
}
