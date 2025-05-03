using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MealPlannedData
{
    public List<MealWrapper> meals = new List<MealWrapper>();
}

[System.Serializable]
public class MealWrapper
{
    public int day;
    public int slot;
    public Meal meal;
}