using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1; // Notice not 1f. Setting timescale to 1 resets time to the default value
    }
}
