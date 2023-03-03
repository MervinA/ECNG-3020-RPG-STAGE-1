using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CourseEvent : ScriptableObject
{
   public int CourseCode; 
   public string CourseType; 
   public string CourseAssignment; 
   public int CourseAssignmentCode; 
   public int hours; 
   public int minutes; 
   public int month; 
   public int date; 
   public int year; 
   public int reminderDate; 

 
}
