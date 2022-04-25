using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Router : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadLastLevel());
    }

    private IEnumerator LoadLastLevel()
    {
        int lastScene = PlayerPrefs.GetInt("LEVEL", 1);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(lastScene);
    }
}
