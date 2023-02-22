
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeClock : MonoBehaviour
{
	public SOMont[] scriptableMonths =  new SOMont[12];
	public SOMont month;

	//TimeClock Properties
	public int yy;
	public int days;
	public int actualMonth;
	public int hh;
	public int mm;


	public float secondSpeed;

		


	int hoursPassed;
   
	// Start is called before the first frame update
	void Start()
	{
		//month = scriptableMonths[8];
		InvokeRepeating("TimePasses", secondSpeed, secondSpeed);
		yy = 2022; 
		
	}
	// Update is called once per frame
	
	void FixedUpdate()
	{
		
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
	private void CheckClock() //Checks whether the clock has arrived to time 24 and resets it to00;
	{
		if (hh > 23)
		{
			hh = 0;
			days++;
			if (days > month.dayAmmount)
			{
				days = 1;
				actualMonth++;				
				month = scriptableMonths[actualMonth];				
				if (actualMonth >= 12)
				{
					actualMonth = 0;
					yy++;
				}
			}
		}
	}
	

}