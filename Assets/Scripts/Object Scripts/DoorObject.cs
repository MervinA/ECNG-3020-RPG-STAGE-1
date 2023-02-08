using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum DoorType
{
    key, 
    enemy, 
    doorbutton

}
public class DoorObject : Interactable
{
    

public string sceneToLoad; 
    public GameObject fadeInPanel;
    public GameObject fadeoutPanel; 
    public float fade_wait; 
    public Text dialogText; 
     public GameObject dialogBox; 
    public string dialog; 
    private  PlayerMovement Player; 
    public string exitspawnName;
    public DoorType thisDoorType;
    public bool open; 

    private Animator anim; 
    public Inventory playerInventory; 
   
    // Start is called before the first frame update
    void Start()
    {
          Player = FindObjectOfType<PlayerMovement>(); 
          anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& playerInRange)
        {
            if(open == false)
            {
                //&& (playerInventory.numberofkeys > 0)
               OpenDoor();
               
            }

         if(open == true)
         {
           StartCoroutine(FadeCo());
         } 
           
        } 
    }

public void OpenDoor() 
{
// set animation to door open 
//playerInventory.numberofkeys --; 
open = true; 
anim.SetBool("DoorOpened", true); 
//StartCoroutine(Waitingco());
}


public void DoorIsClose() 
{

}
public override void OnTriggerEnter2D (Collider2D other)
    {

        if(other.CompareTag("Player_Passive") && !other.isTrigger)
        {
            context.Raise();
            PlayerMovement.spawnPointName = exitspawnName; 
            playerInRange = true;  
             dialogBox.SetActive(true);
             dialogText.text = dialog; 
                
            
        }
    }

    public override void OnTriggerExit2D(Collider2D  other)
    {

        if (other.CompareTag("Player_Passive") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false; 
            dialogBox.SetActive(false);
        
        }
    }

    private IEnumerator FadeCo()
    {
        yield return new WaitForSeconds(1.3f);
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
