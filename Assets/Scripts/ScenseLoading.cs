using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScenseLoading : MonoBehaviour
{
    [SerializeField]
    private Image _progressBar;
    void Start()
    {
        //Start async operator
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        //Create async operation
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
        //while (gameLevel.progress < 1)
        {
            _progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }


    }
}
