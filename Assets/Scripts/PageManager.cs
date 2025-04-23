using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{

    [Header("General")]
    public List<GameObject> pageList = new List<GameObject>();
    public Planner planner;

    [Header("SelectionPage")]
    public TMP_Text selectionCurrentMealDisplay;
    public MealLoader mealLoader;

    [Header("CartPage")]

    public Color hasMealColor;
    public Color noMealColor;


    public void Start()
    {
        ChangePage(0);
    }

    public void ChangePage(int x)
    {
        foreach (GameObject page in pageList)
        {
            page.SetActive(false);
            pageList[x].SetActive(true);
        }
    }

    public void GoToPlanningPage()
    {
        planner.ClearCartDisplay();

        ChangePage(0);
        planner.ResetDay();
        mealLoader.ResearchMeals("");
    }

    public void GoToCartPage()
    {
        ChangePage(2);
        planner.Calculator();
    }




    public void GoToMondaySelectionPage(int nb)
    {
        ChangePage(1);
        planner.SetDay(0, nb);
        SetMealDisplay();
    }
    public void GoToTuesdaySelectionPage(int nb)
    {
        ChangePage(1);
        planner.SetDay(1, nb);
        SetMealDisplay();
    }
    public void GoToWednesdaySelectionPage(int nb)
    {
        ChangePage(1);
        planner.SetDay(2, nb);
        SetMealDisplay();
    }
    public void GoToThursdaySelectionPage(int nb)
    {
        ChangePage(1);
        planner.SetDay(3, nb);
        SetMealDisplay();
    }
    public void GoToFridaySelectionPage(int nb)
    {
        ChangePage(1);
        planner.SetDay(4, nb);
        SetMealDisplay();
    }
    public void GoToSaturdaySelectionPage(int nb)
    {
        ChangePage(1);
        planner.SetDay(5, nb);
        SetMealDisplay();
    }
    public void GoToSundaySelectionPage(int nb)
    {
        ChangePage(1);
        planner.SetDay(6, nb);
        SetMealDisplay();
    }

    // If Days and nb already set
    public void SetMeal(Meal meal)
    {
        planner.SetMeal(meal);
        SetMealDisplay();
    }

    // If Days and nb already set
    public void RemoveMeal()
    {
        planner.RemoveMeal();
        SetMealDisplay();
    }

    // If Days and nb already set
    public void SetMealDisplay()
    {
        if (planner.GetCurrentMeal() != null)
        {
            selectionCurrentMealDisplay.text = planner.GetCurrentMeal().name;
            planner.daysButtons[(planner.currentDay * 3) + planner.currentNb].GetComponentInChildren<TMP_Text>().text = planner.GetCurrentMeal().name;
            planner.daysButtons[(planner.currentDay * 3) + planner.currentNb].GetComponent<Image>().color = hasMealColor;
        }
        else
        {
            selectionCurrentMealDisplay.text = "";
            planner.daysButtons[(planner.currentDay * 3) + planner.currentNb].GetComponentInChildren<TMP_Text>().text = "+";
            planner.daysButtons[(planner.currentDay * 3) + planner.currentNb].GetComponent<Image>().color = noMealColor;
            mealLoader.ResetScroller();
        }

    }

    public void ResetDayDisplay(int x)
    {
        for(int i = 0; i < 3; i++)
        {
            planner.SetDay(x, i);
            RemoveMeal();
        }

        planner.ResetDay();
    }

    public void ResetAllDaysDisplay()
    {
        for(int i = 0; i < 7; i++)
        {
            ResetDayDisplay(i);
        }
    }

}
