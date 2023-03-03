using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class CourseDeadlines : MonoBehaviour
{
    public TimeClockTest clockinfo; 
    //private SOMont currMonth; 
    public CourseEvent[] courseinfo;
    public TextMeshProUGUI[] courseDeadlines; 
    private Coroutine repeatcoroutine;

    void Start()
    {
      // InvokeRepeating("Deadlinecheck", clockinfo.secondSpeed, clockinfo.secondSpeed);
     repeatcoroutine = StartCoroutine(RepeatMethod(clockinfo.secondSpeed));
    }

    private IEnumerator RepeatMethod(float secondSpeed)
    {
        while (true)
        {
           //StartCoroutine(Deadlinecheck());
           WarningCheck();
            yield return new WaitForSeconds(secondSpeed);
        }
    
    } 
    private void WarningCheck()
    {
        for(int i = 0; i <courseinfo.Length; i++)
        {
            int warningDatePassed = WarningDateCheck(clockinfo.calendarData.yy, clockinfo.calendarData.actualMonth +1, clockinfo.calendarData.date);
            int deadlineDatePassed = DeadlineDateCheck(courseinfo[i].year, courseinfo[i].month +1, courseinfo[i].date);

            if(warningDatePassed >= (deadlineDatePassed - 7) && warningDatePassed < deadlineDatePassed)
            {
                Debug.Log("Deadline Approaching, Course: " + courseinfo[i].CourseCode); 
                courseDeadlines[i].text = courseinfo[i].CourseType + courseinfo[i].CourseCode.ToString() + "\n" + courseinfo[i].date + "," + clockinfo.scriptableMonths[courseinfo[i].month].monthName.ToString();
            }       
        }
    }
    private int WarningDateCheck(int year, int month, int date)
    {
        int warningDaysPassed = DaysPassed(year, month, date);
        return warningDaysPassed; 
    }
    private int DeadlineDateCheck(int year, int month, int date)
    {
        int deadlineDaysPassed = DaysPassed(year, month, date);
        return deadlineDaysPassed; 
    }
    private static int DaysPassed(int year, int month, int day)
    {
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(year, month, day);
        TimeSpan daysPassed = endDate - startDate;
        return daysPassed.Days;
    }
    
}
