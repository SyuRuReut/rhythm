using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadBar;
    [SerializeField] private Canvas canvas;
    public void toTitle()
    {
        Instantiate(loadBar, canvas.transform);
        StartCoroutine(toScene("Title"));
    }

    IEnumerator toScene(string scene)
    {
        yield return new WaitForSeconds(0.1f);
        AsyncOperation sceneOper =  SceneManager.LoadSceneAsync(scene);
        sceneOper.allowSceneActivation = true;
    }

    public void toUser()
    {
        Instantiate(loadBar, canvas.transform);
        StartCoroutine(toScene("UserLoad"));
    }
    public void toRobby()
    {
        Instantiate(loadBar, canvas.transform);
        StartCoroutine(toScene("Robby"));
    }
    public void toSelect()
    {
        Instantiate(loadBar, canvas.transform);
        StartCoroutine(toScene("SongSelect"));
    }
    public void toPlay()
    {
        Instantiate(loadBar, canvas.transform);
        StartCoroutine(toScene("Play")) ;
    }
    public void toGacha()
    {
        Instantiate(loadBar, canvas.transform);
        StartCoroutine(toScene("Gacha"));
    }
    public void toParty()
    {
        Instantiate(loadBar, canvas.transform);
        StartCoroutine(toScene("Party"));
    }

    public void toResult()
    {
        Instantiate(loadBar, canvas.transform);
        SceneManager.LoadScene("Result");
    }
}
