using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeClockTest : MonoBehaviour
{
    public SOMont[] scriptableMonths;
    public SOMont month;

    // TimeClock Properties
    public int yy;
    public int date;
    public int actualMonth;
   // private int monthReference; 
    private int monthRef;
    private int daysPassed;
    public int hh;
    public int mm;
    private int[] daysInMonth = new int[] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
    
    int hoursPassed;
    private Coroutine repeatcoroutine;

    public float secondSpeed;

   private string[] daysOfWeek = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
  
    private int startingDay; // starting day of the year (0 = Sunday, 1 = Monday, etc.)

    void Start()
    {
        // Set starting day of the year (January 1st)
        DateTime startingDate = new DateTime(2022, 1, 1);
        startingDay = (int)startingDate.DayOfWeek;
        Debug.Log("starting day in array = " + startingDay);
       

        yy = 2022;
        actualMonth = month.monthPos - 1;
        // monthReference = actualMonth; 
        repeatcoroutine = StartCoroutine(RepeatMethod(secondSpeed));
    }

    private void StopRepeating()
    {
        if (repeatcoroutine != null)
        {
            StopCoroutine(repeatcoroutine);
            repeatcoroutine = null;
        }
    }

    private IEnumerator RepeatMethod(float secondSpeed)
    {
        while (true)
        {
            yield return new WaitForSeconds(secondSpeed);
            TimePasses();
        }
    }

    public void TimePasses() //Sets the IngameTime passing
    {
        mm++;
        if (mm > 59)
        {
            mm = 0;
            hh++;
            hoursPassed++;
            CheckClock();
        }
    }

    private void CheckClock()
    {
        if (hh > 23)
        {
            hh = 0;
            date++;

            if (date > month.dayAmmount)
            {
                //monthReference++;
                if (actualMonth < 11)
                {
                    date = 1;
                    actualMonth++;
                    month = scriptableMonths[actualMonth];
                }
                else
                {
                    date = 1;
                    actualMonth = 0;
                    month = scriptableMonths[actualMonth];
                    yy++;
                }
            }
        }

        // Calculate day of the week
        //Debug.Log("the month reference is: " + monthReference);
        daysPassed = DaysPassed(yy, actualMonth +1, date); 
        Debug.Log("days passed: " + daysPassed);
        int dayOfWeek = ((daysPassed) + (startingDay)) % 7;
        Debug.Log("Today is " + daysOfWeek[dayOfWeek]);
    }

public static int DaysPassed(int year, int month, int day)
{
    DateTime startDate = new DateTime(2022, 1, 1);
    DateTime endDate = new DateTime(year, month, day);
    TimeSpan daysPassed = endDate - startDate;
    return daysPassed.Days;
}
    private int DayOfYear(int year, int month, int day)
    {
       
        
        if(month > 12)
        {
            monthRef = month - 12; 
        }
        
       //  Debug.Log("is it a leap year?  " + IsLeapYear(year));
        int dayOfYear = daysInMonth[monthRef - 1] + day;
       // if (month > 2 && IsLeapYear(year))
          //  dayOfYear++;
      //  Debug.Log("day of the year is: " + dayOfYear);
        return dayOfYear;
        
    }

    private bool IsLeapYear(int year)
    {
        if (year % 4 != 0)
            return false;
        if (year % 100 != 0)
            return true;
        if (year % 400 == 0)
            return true;

        return false;
    }
}
