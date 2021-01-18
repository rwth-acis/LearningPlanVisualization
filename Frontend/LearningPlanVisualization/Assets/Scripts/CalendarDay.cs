using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Cell or slot in the calendar. All the information each day should now about itself
/// </summary>
public class CalendarDay : MonoBehaviour
{
    private int dayNum;
    private Material dayColor;
    private string plannedEventName;
    public MeshRenderer meshRenderer;
    public TextMeshPro textDayNum;
    public TextMeshPro textPlannedEvent;
    public Material invalidDayColor;
    
    public void UpdateMaterial(Material newColor)
    {
        if (newColor) dayColor = newColor;
        else dayColor = invalidDayColor;
        
        meshRenderer.material = dayColor;
    }

    public void UpdateDay(int newDayNum)
    {
        dayNum = newDayNum;
        if (dayColor != invalidDayColor)
        {
            textDayNum.text = (dayNum + 1).ToString();
        }
        else
        {
            textDayNum.text = (dayNum + 1).ToString();
        }
        UpdatePlannedEvent("");
    }
    
    public void UpdatePlannedEvent(string newPlannedEventName)
    {
        plannedEventName = newPlannedEventName;
        textPlannedEvent.text = plannedEventName;
    }
}
