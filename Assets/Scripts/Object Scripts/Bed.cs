using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Bed : Interactable
{
    public string sceneToLoad_bed; 
    public Vector2 playerPosition_bed; 
    public VectorValue playerStorage_bed; 
    public GameObject fadeInPanel;
    public GameObject fadeoutPanel; 
    public float fade_wait; 
    public Text dialogText; 
     public GameObject dialogBox; 
    public string dialog; 
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& playerInRange){
            playerStorage_bed.initialValue = playerPosition_bed; 
            StartCoroutine(FadeCo());
        } 
    }

   private  void OnTriggerEnter2D (Collider2D other)
    {

        if(other.CompareTag("Player_Passive") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;  
             dialogBox.SetActive(true);
             dialogText.text = dialog; 
                
            
        }
    }

    private void OnTriggerExit2D(Collider2D  other)
    {

        if (other.CompareTag("Player_Passive") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false; 
            dialogBox.SetActive(false);
        
        }
    }
    public IEnumerator FadeCo()
    {
        if (fadeoutPanel != null)
        {
        Instantiate(fadeoutPanel, Vector3.zero, Quaternion.identity); 
        }
        yield return new WaitForSeconds(fade_wait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad_bed);
        while(!asyncOperation.isDone)
        {
            yield return null; 
        }
    }
}

