using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;


public class GradeScript : MonoBehaviour
{
     public CalendarData clockinfo; 
    //private SOMont currMonth; 
    
    public TextMeshProUGUI[] courseGrades; 
    [SerializeField] private BoolValue[] BossKills; 
    [SerializeField] private ExamBoolValue[] ExamCompletion; 
    [SerializeField] private CourseEvent[] courseinfo;
    [SerializeField] private Inventory playerinventory; 
    [SerializeField] private Grades[] courseGradesSO;


    

    public void Update()
    {
        WarningCheck();
    }

    public void WarningCheck()
    {
        for(int i = 0; i <courseinfo.Length; i++)
        {
            int warningDatePassed = WarningDateCheck(clockinfo.yy, clockinfo.actualMonth +1, clockinfo.date);
            int deadlineDatePassed = DeadlineDateCheck(courseinfo[i].year, courseinfo[i].month +1, courseinfo[i].date);

            if(warningDatePassed > (deadlineDatePassed + 7))
            {
                courseGrades[i].text = "Course: " + courseinfo[i].CourseType + courseinfo[i].CourseCode + "\nGrade :" + CourseGrade();
                //courseGrades[i].text = "the grade is"; 
               courseGradesSO[i].RuntimeValue = CourseGrade() ;
            }       
        }
    }

    public string CourseGrade()
    {
        string course_grade =""; 
        for(int i = 0; i < courseinfo.Length; i++)
        {

            
            
            if(ExamCompletion[i].RuntimeValue == true) 
            {
                if(BossKills[i].RuntimeValue == true)
                {
                    if((ExamCompletion[i].assignmentCode == courseinfo[i].CourseAssignmentCode))
                    {
                        if(playerinventory.CompSysCoins >35)
                        {
                            course_grade = "A"; 
                        }
                        else
                        {
                            course_grade = "B";
                        }
                    }  
                    else 
                    {
                        Debug.Log ("Codes do not match");
                    }
                }
                else 
                {
                    if((ExamCompletion[i].assignmentCode == courseinfo[i].CourseAssignmentCode))
                    {
                        if(playerinventory.CompSysCoins >35)
                        {
                            course_grade = "B"; 
                        }
                        else
                        {
                            course_grade = "C";
                        }
                    }
                    else 
                    {
                        Debug.Log ("Codes do not match");
                    } 
                }
            } 
            else 
            {
                course_grade = "Incomplete: " + courseinfo[i].CourseAssignmentCode;
            }
        }
        return course_grade; 
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


