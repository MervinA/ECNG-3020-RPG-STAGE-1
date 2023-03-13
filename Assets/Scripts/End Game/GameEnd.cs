using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnd : MonoBehaviour
{

    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject fadeoutPanel; 
    [SerializeField] private float fade_wait; 
    [SerializeField] private string sceneToLoad; 



    public void EndGameScene()
    {
        StartCoroutine(FadeCo());
        
    }

    private  IEnumerator FadeCo()
    {
        if (fadeoutPanel != null)
        {
        Instantiate(fadeoutPanel, Vector3.zero, Quaternion.identity); 
        }
        yield return new WaitForSeconds(fade_wait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {
            yield return null; 
        }
    }
}

