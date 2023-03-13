
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory; 
    private Text EndGameText;
    public string mainMenu;
    public GameObject[] EndingRating; 
   [SerializeField] private TextMeshProUGUI EndText; 

    // Start is called before the first frame update
    void Start()
    {
        InitEnding();
    }

    public void InitEnding()
    {
        int compCoins = playerInventory.CompSysCoins; 
        int elecCoins = playerInventory.ElecPowerCoins; 
        
        if((compCoins > 50) && (elecCoins > 50))
        {
            EndingRating[0].SetActive(true); 
            EndText.text = "Congratulations!! You Have Achieved The Gold Ending, Excellent Work!!";
        }
        else if ((compCoins < 50 && compCoins > 25) || (elecCoins <50 && elecCoins >25))
        {
             EndingRating[1].SetActive(true); 
            EndText.text = "Good Job, You Have Achieved The Silver Ending";
        }
        else if ((compCoins < 25) || (elecCoins < 25))
        {
             EndingRating[2].SetActive(true);  
            EndText.text = " You Have Achieved The Copper Ending, There Is Work To Be Done";
        }
        

    }
    

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
     
    }

}
