using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockUpdater : MonoBehaviour
{
	//Referenced Scripts
	public TimeClockTest clock;

	//Text labels
	public TextMeshProUGUI timeText;

	public TextMeshProUGUI curryearText;
	public TextMeshProUGUI currmonthText;
	public TextMeshProUGUI currdateText;
	public TextMeshProUGUI currtdayText; 

	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating("UpdateClock", 0, clock.secondSpeed);
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	private void UpdateClock()
	{
		curryearText.text = clock.yy.ToString();
		currmonthText.text = clock.month.monthName;
		currdateText.text = clock.date.ToString();		
		currtdayText.text = clock.daysOfWeek[clock.dayOfWeek];

		string hours = clock.hh.ToString();
		string minutes = clock.mm.ToString();
		if (hours.Length <1 )
		{
			hours = "0"+ clock.hh.ToString();
		}
		if (minutes.Length <= 1)
		{
			minutes = "0" + clock.mm.ToString();
		}
		timeText.text = hours + ":" + minutes;

	}
}
