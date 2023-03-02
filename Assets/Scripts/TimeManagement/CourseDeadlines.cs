using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CourseDeadlines : MonoBehaviour
{
    public TimeClockTest clockinfo; 
    //private SOMont currMonth; 
    public int hour; 
    public int month; 
    public int date;
    private int deadline; 
    private int deadlinemonth; 
    public CourseEvent[] courseinfo;
    public TextMeshProUGUI[] courseDeadlines; 
    
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
            Deadlinecheck();
            yield return new WaitForSeconds(secondSpeed);
            
        }
    }
    private void Deadlinecheck()
    {
        for(int i = 0; i <courseinfo.Length; i++)
        {
         //  Debug.Log("the course is: " + courseinfo[i].CourseCode) ; 
           // if((clockinfo.actualMonth == DeadlineMonth(DeadlinedateBool(courseinfo[i].date), clockinfo.actualMonth ,courseinfo[i].month)) && 
           // (clockinfo.date == Deadliedate(courseinfo[i].date, clockinfo.scriptableMonths[clockinfo.actualMonth].dayAmmount)))
            // (clockinfo.date == Deadliedate(courseinfo[i].date, 30)))
             int deadline_month = DeadlineMonth(DeadlinedateBool(courseinfo[i].date), clockinfo.actualMonth ,courseinfo[i].month);
             int deadline_Date= Deadliedate(courseinfo[i].date, clockinfo.scriptableMonths[clockinfo.actualMonth].dayAmmount);
             if(clockinfo.actualMonth == deadline_month &&
                clockinfo.date == deadline_Date){
               // while(clockinfo.date != courseinfo[i].date){
                    Debug.Log("The Course that is due" + courseinfo[i].CourseCode);
                    // Debug.Log("the deadline month is: "+ deadline_month); 
               //     // Debug.Log("the deadline date is: "+ deadline_Date);
               // }
             }
        }
    }
    private int DeadlineMonth(bool deadlineBool,int currentmonth, int deadlineMonth)
    {
        if(deadlineBool == false)
        {
            deadlinemonth = currentmonth;
        }
        else if (deadlineBool == true)
        {
            if(deadlineMonth == 0)
            {
                deadlinemonth = 11;
            }
            else
            {
                deadlinemonth = deadlineMonth - 1; 
            }
        }
        int deadlineMonthPos = deadlinemonth; 
        return deadlineMonthPos; 
    }
    private int Deadliedate(int deadline_Date, int monthDayAmount)
    {
      

        if( deadline_Date > 7)
        {
            deadline  = deadline_Date - 7; 
        }
        else if (deadline_Date <= 7)
        {
            deadline =monthDayAmount-(7-deadline_Date);
        }
        int deadlinedate = deadline; 
       return deadlinedate;
    }
    private bool DeadlinedateBool(int deadlinedate)
    {
        
        if (deadlinedate <= 7)
        {
            return true; 
        }
        return false; 
    }
    
    
}
