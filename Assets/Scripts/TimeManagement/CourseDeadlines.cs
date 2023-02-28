using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseDeadlines : MonoBehaviour
{
    public TimeClock clockinfo; 
    public int hour; 
    public int month; 
    public int date;
    public CourseEvent[] courseinfo; 
    public bool deadline; 
    private Coroutine repeatcoroutine;

    void Start()
    {
      // InvokeRepeating("Deadlinecheck", clockinfo.secondSpeed, clockinfo.secondSpeed);
     repeatcoroutine = StartCoroutine(RepeatMethod(clockinfo.secondSpeed));
    }

    // Update is called once per frame

    void update()
    {
        
    }
    private IEnumerator RepeatMethod(float secondSpeed)
    {
        while (true)
        {
            yield return new WaitForSeconds(secondSpeed);
            Deadlinecheck();
            //Debug.Log("" + clockinfo.mm);
        }
    }
    public void Deadlinecheck()
    {
       for(int i = 0; i <courseinfo.Length; i++){
      //  Debug.Log("i is: " + i); 
            if((clockinfo.date==courseinfo[i].date) && ((clockinfo.actualMonth+1) == courseinfo[i].month) && (clockinfo.hh==courseinfo[i].hours) && (clockinfo.mm == courseinfo[i].minutes))
            {
                deadline = true ; 
               
               
                
               
            }
            else
            {
                deadline = false; 
            }
            Debug.Log("" + deadline);
        }
    }
}
