using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public int LevelIndex;

    public void StartGame ()
    {
        SceneManager.LoadScene(LevelIndex);
    }
}
