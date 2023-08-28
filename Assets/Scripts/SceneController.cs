using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
    }
}
