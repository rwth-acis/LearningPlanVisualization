using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class LTCalendar : MonoBehaviour
{
    public class PlannedEvent
    {
        LTAction action;
        DateTime startDate;

        public PlannedEvent(LTAction newAction,DateTime newDate)
        {
            action = newAction;
            startDate = newDate;
        }

        public LTAction GetAction()
        {
            return action;
        }

        public DateTime GetStartDate()
        {
            return startDate;
        }

        public DateTime GetEndDate()
        {
            return startDate + action.time;
        }
    }

    public List<PlannedEvent> plannedEvents = new List<PlannedEvent>();


    /// <summary>
    /// All the days in the month. After we make our first calendar we store these days in this list so we do not have to recreate them every time.
    /// </summary>
    List<CalendarDay> days = new List<CalendarDay>();

    /// <summary>
    /// Setup in editor since there will always be six weeks. 
    /// Try to figure out why it must be six weeks even though at most there are only 31 days in a month
    /// </summary>
    public GameObject[] weeks;

    /// <summary>
    /// This is the text object that displays the current month and year
    /// </summary>
    public TextMeshPro MonthAndYear;

    /// <summary>
    /// this currDate is the date our Calendar is currently on. The year and month are based on the calendar, 
    /// while the day itself is almost always just 1
    /// If you have some option to select a day in the calendar, you would want the change this objects day value to the last selected day
    /// </summary>
    public DateTime currDate = DateTime.Now;

    public Material dayStd;
    public Material dayNow;

    private DateTime lastClickedDay;




    /// <summary>
    /// In start we set the Calendar to the current date
    /// </summary>
    void Start()
    {
        for (int w = 0; w < 6; w++)
        {
            CalendarDay[] week = weeks[w].GetComponentsInChildren<CalendarDay>();
            for (int i = 0; i < 7; i++)
            {
                days.Add(week[i]);
                int counter = w * 7 + i;
                week[i].GetComponent<Interactable>().OnClick.AddListener(delegate { DayCLicked(counter); });
            }
        }
        currDate = DateTime.Now;
        RefreshCalendar();
    }


    void UpdateCalendarStatusOfNodes()
    {
        foreach(var spawner in LTMainMenu.instance.nodeSpawner)
            foreach(var instance in spawner.SpawnedInstances)
            {
                instance.GetComponentInChildren<LTNode>().UpdateCalendarStatus();
            }
    }

    void ResetCalendarStatusOfNodes()
    {
        foreach (var spawner in LTMainMenu.instance.nodeSpawner)
            foreach (var instance in spawner.SpawnedInstances)
            {
                instance.GetComponentInChildren<LTNode>().calendarStatus = LTStatus.NotAvailable;
            }
    }

    void CalenderTillDay(DateTime date)
    {
        ResetCalendarStatusOfNodes();
        foreach(PlannedEvent plannedEvent in plannedEvents)
        {
            if (plannedEvent.GetEndDate() <= date) plannedEvent.GetAction().calendarStatus = LTStatus.Done;
        }
        UpdateCalendarStatusOfNodes();
    }

    /// <summary>
    /// Anytime the Calendar is changed we call this to make sure we have the right days for the right month/year
    /// </summary>
    void UpdateCalendar(int year, int month)
    {
        DateTime firstOfMonth = new DateTime(year, month, 1);
        currDate = firstOfMonth;
        MonthAndYear.text = firstOfMonth.ToString("MMMM") + " " + firstOfMonth.Year.ToString();
        int startDay = GetMonthStartDay(year, month);
        int endDay = GetTotalNumberOfDays(year, month);
        int endDayLastMonth = firstOfMonth.AddDays(-1).Day;


        ///loop through days

        for (int i = 0; i < 42; i++)
        {
            if (i < startDay)
            {
                days[i].UpdateMaterial(null);
                days[i].UpdateDay(endDayLastMonth + i - startDay);
            }
            else if (i - startDay >= endDay)
            {
                days[i].UpdateMaterial(null);
                days[i].UpdateDay(i - startDay-endDay);
            }
            else
            {
                days[i].UpdateMaterial(dayStd);
                days[i].UpdateDay(i - startDay);
            }

        }

        foreach(PlannedEvent plannedEvent in plannedEvents)
        {
            var tempDate = plannedEvent.GetStartDate();
            while (tempDate.Date < plannedEvent.GetEndDate().Date)
            {
                if (tempDate.Year == firstOfMonth.Year && tempDate.Month == firstOfMonth.Month)
                {
                    days[tempDate.Day - 1 + startDay].UpdatePlannedEvent(plannedEvent.GetAction().name);
                }
                tempDate = tempDate.AddDays(1);
            }


        }

        ///This just checks if today is on our calendar. If so, we highlight it in green
        if (DateTime.Now.Year == year && DateTime.Now.Month == month)
        {
            days[(DateTime.Now.Day - 1) + startDay].UpdateMaterial(dayNow);
        }

    }

    public void RefreshCalendar()
    {
        UpdateCalendar(currDate.Year, currDate.Month);
    }

    /// <summary>
    /// This returns which day of the week the month is starting on
    /// </summary>
    int GetMonthStartDay(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);

        //DayOfWeek Sunday == 0, Saturday == 6 etc.
        return (int)temp.DayOfWeek;
    }

    /// <summary>
    /// Gets the number of days in the given month.
    /// </summary>
    int GetTotalNumberOfDays(int year, int month)
    {
        return DateTime.DaysInMonth(year, month);
    }

    /// <summary>
    /// This either adds or subtracts one month from our currDate.
    /// The arrows will use this function to switch to past or future months
    /// </summary>
    public void SwitchMonth(int direction)
    {
        if (direction < 0)
        {
            currDate = currDate.AddMonths(-1);
        }
        else
        {
            currDate = currDate.AddMonths(1);
        }
        RefreshCalendar();
    }

    public void AddEvent(LTAction action, DateTime startDate)
    {
        PlannedEvent temp = new PlannedEvent(action, startDate);
        RemoveEvent(action);
        plannedEvents.Add(temp);
        action.calendarStatus = LTStatus.Done;
        RefreshCalendar();
    }

    public void RemoveEvent(LTAction action)
    {
        plannedEvents.RemoveAll(x => x.GetAction().name == action.name);
        RefreshCalendar();
    }

    public void ClearEvents()
    {
        plannedEvents.Clear();
        RefreshCalendar();
    }

    public void DayCLicked(int id)
    {
        lastClickedDay = currDate.AddDays(id - GetMonthStartDay(currDate.Year, currDate.Month));
        CalenderTillDay(lastClickedDay);
        LTMainMenu.instance.SwitchMode(LTModes.AddToCalendar);

        //AddDummyEvent(currDate.AddDays(id - GetMonthStartDay(currDate.Year, currDate.Month)));
    }

    public void AddToCalendarButtonClicked(LTAction action)
    {
        AddEvent(action, lastClickedDay);
        LTMainMenu.instance.SwitchMode(LTModes.Normal);
    }
}
