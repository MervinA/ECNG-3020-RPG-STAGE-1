using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CalendarData", menuName = "ScriptableObjects/CalendarData")]
[System.Serializable]
public class CalendarData : ScriptableObject
{
    public int mm;
    public int hh;
    public int date;
    public int actualMonth;
    public int yy;
    public int daysPassed;
    public int dayOfWeek;
    public int maxMonthDays; 
    
    // Add any other necessary calendar properties here
}

