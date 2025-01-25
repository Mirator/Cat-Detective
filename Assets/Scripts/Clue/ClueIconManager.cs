using System.Collections.Generic;
using UnityEngine;

public static class ClueIconManager
{
    private static Dictionary<string, Sprite> timeIcons = new Dictionary<string, Sprite>();
    private static Dictionary<Location, Sprite> locationIcons = new Dictionary<Location, Sprite>();

    static ClueIconManager()
    {
        // Load time icons
        timeIcons["Morning"] = Resources.Load<Sprite>("Icons/Time/Morning");
        timeIcons["Noon"] = Resources.Load<Sprite>("Icons/Time/Noon");
        timeIcons["Evening"] = Resources.Load<Sprite>("Icons/Time/Evening");

        // Load location icons
        locationIcons[Location.Store] = Resources.Load<Sprite>("Icons/Location/Store");
        locationIcons[Location.Barn] = Resources.Load<Sprite>("Icons/Location/Barn");
        locationIcons[Location.Treehouse] = Resources.Load<Sprite>("Icons/Location/Treehouse");
        locationIcons[Location.Beach] = Resources.Load<Sprite>("Icons/Location/Beach");
        locationIcons[Location.Garden] = Resources.Load<Sprite>("Icons/Location/Garden");
        locationIcons[Location.Bakery] = Resources.Load<Sprite>("Icons/Location/Bakery");
    }

    public static Sprite GetIconForTime(string time)
    {
        return timeIcons.ContainsKey(time) ? timeIcons[time] : null;
    }

    public static Sprite GetIconForLocation(Location location)
    {
        if (locationIcons.ContainsKey(location))
        {
            Sprite icon = locationIcons[location];
            //Debug.Log($"Loaded icon for location: {location} -> {(icon != null ? icon.name : "None")}");
            return icon;
        }
        else
        {
            Debug.LogWarning($"No icon found for location: {location}");
            return null;
        }
    }



}
