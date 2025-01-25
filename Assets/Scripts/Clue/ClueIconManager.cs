using System.Collections.Generic;
using UnityEngine;

public static class ClueIconManager
{
    private static Dictionary<TimeOfDay, Sprite> timeIcons = new Dictionary<TimeOfDay, Sprite>();
    private static Dictionary<Location, Sprite> locationIcons = new Dictionary<Location, Sprite>();

    static ClueIconManager()
    {
        // Load time icons
        timeIcons[TimeOfDay.Morning] = Resources.Load<Sprite>("Icons/Time/Morning");
        timeIcons[TimeOfDay.Noon] = Resources.Load<Sprite>("Icons/Time/Noon");
        timeIcons[TimeOfDay.Evening] = Resources.Load<Sprite>("Icons/Time/Evening");

        // Load location icons
        locationIcons[Location.Store] = Resources.Load<Sprite>("Icons/Location/Store");
        locationIcons[Location.Barn] = Resources.Load<Sprite>("Icons/Location/Barn");
        locationIcons[Location.Treehouse] = Resources.Load<Sprite>("Icons/Location/Treehouse");
        locationIcons[Location.Beach] = Resources.Load<Sprite>("Icons/Location/Beach");
        locationIcons[Location.Garden] = Resources.Load<Sprite>("Icons/Location/Garden");
        locationIcons[Location.Bakery] = Resources.Load<Sprite>("Icons/Location/Bakery");
    }

    public static Sprite GetIconForTime(TimeOfDay time)
    {
        if (timeIcons.ContainsKey(time))
        {
            return timeIcons[time];
        }
        else
        {
            Debug.LogWarning($"No icon found for time of day: {time}");
            return null;
        }
    }

    public static Sprite GetIconForLocation(Location location)
    {
        if (locationIcons.ContainsKey(location))
        {
            Sprite icon = locationIcons[location];
            return icon;
        }
        else
        {
            Debug.LogWarning($"No icon found for location: {location}");
            return null;
        }
    }
}
