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
    [SerializeField] private Grades[] courseGradesSO;

    void Start()
    {
        InitEnding();
    }

    public void InitEnding()
    {
        int compCoins = playerInventory.CompSysCoins;
        int elecCoins = playerInventory.ElecPowerCoins;

        int aCount = 0;
        int bCount = 0;
        int cCount = 0;

        for (int i = 0; i < courseGradesSO.Length; i++)
        {
            if (courseGradesSO[i].RuntimeValue == "A")
            {
                aCount++;
            }
            else if (courseGradesSO[i].RuntimeValue == "B")
            {
                bCount++;
            }
            else if (courseGradesSO[i].RuntimeValue == "C")
            {
                cCount++;
            }
        }

        if (aCount == courseGradesSO.Length)
        {
            EndingRating[0].SetActive(true);
            EndText.text = "Congratulations!! You Have Achieved The Gold Ending, Excellent Work!!";
        }
        else if (aCount == 0 && bCount == 1)
        {
            EndingRating[2].SetActive(true);
            EndText.text = "You Have Achieved The Copper Ending, There Is Work To Be Done";
        }
        else if (aCount > 0 && bCount > 0 && cCount == 0)
        {
            EndingRating[1].SetActive(true);
            EndText.text = "Good Job, You Have Achieved The Silver Ending";
        }
        else if (aCount > cCount)
        {
            EndingRating[0].SetActive(true);
            EndText.text = "Congratulations!! You Have Achieved The Gold Ending, Excellent Work!!";
        }
        else
        {
            EndingRating[2].SetActive(true);
            EndText.text = "You Have Achieved The Copper Ending, There Is Work To Be Done";
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
