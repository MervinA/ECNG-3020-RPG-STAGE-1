using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SchoolTransfer : MonoBehaviour
{
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject fadeoutPanel; 
    [SerializeField] private CalendarData currTime; 
    [SerializeField] private float fade_wait; 
    [SerializeField] private CourseEvent[] listOfEvents;  
    private  PlayerMovement Player;
    private string exitspawnName;
    

    public void OnTriggerEnter2D()
    {
            choosingEvent();
    }

    public void choosingEvent()
    {
        int date = currTime.date; 
        int hour = currTime.hh; 
        int month = currTime.actualMonth; 
        int year = currTime.yy; 
        PlayerMovement.spawnPointName = exitspawnName;

        for (int i = 0; i < listOfEvents.Length; i++)
        {
            if(date == listOfEvents[i].date && month == listOfEvents[i].month && year == listOfEvents[i].year)
            {
                if(listOfEvents[i].EventType == 1)
                {
                    exitspawnName = "FinalDoor";
                    StartCoroutine(FadeCo("ExamRoom"));

                }
                else if(listOfEvents[i].EventType == 3)
                {
                    exitspawnName = "OrientationDoor";
                    StartCoroutine(FadeCo("Orientation"));
                }
                else if(listOfEvents[i].EventType == 4)
                {
                    exitspawnName = "ProgressDoor";
                    StartCoroutine(FadeCo("Class Presentation"));
                }
            }
            else
                {
                    exitspawnName = "ClassDoor";
                    StartCoroutine(FadeCo("Class"));
                }
        }
    }

    private  IEnumerator FadeCo( string sceneName)
    {
        if (fadeoutPanel != null)
        {
        Instantiate(fadeoutPanel, Vector3.zero, Quaternion.identity); 
        }
        yield return new WaitForSeconds(fade_wait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while(!asyncOperation.isDone)
        {
            yield return null; 
        }
    }
}
