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
        locationIcons[Location.Store] = Resources.Load<Sprite>("Icons/Time/Store");
        locationIcons[Location.Barn] = Resources.Load<Sprite>("Icons/Time/Barn");
        locationIcons[Location.Treehouse] = Resources.Load<Sprite>("Icons/Time/Treehouse");
        locationIcons[Location.Beach] = Resources.Load<Sprite>("Icons/Time/Beach");
        locationIcons[Location.Garden] = Resources.Load<Sprite>("Icons/Time/Garden");
        locationIcons[Location.Bakery] = Resources.Load<Sprite>("Icons/Time/Bakery");
    }

    public static Sprite GetIconForTime(string time)
    {
        return timeIcons.ContainsKey(time) ? timeIcons[time] : null;
    }

    public static Sprite GetIconForLocation(Location location)
    {
        if (locationIcons.ContainsKey(location))
        {
            Debug.Log($"Icon found for location: {location}");
            return locationIcons[location];
        }
        else
        {
            Debug.LogWarning($"No icon found for location: {location}");
            return null;
        }
    }
}
