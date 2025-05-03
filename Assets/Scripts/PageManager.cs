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

    private void Start()
    {
        // Exécute le changement de page après le prochain cycle pour éviter une récursion du PlayerLoop
        StartCoroutine(InitNextFrame());
    }

    private IEnumerator InitNextFrame()
    {
        yield return null; // attend la fin de la frame actuelle
        ChangePage(0);
        LoadAllMeal();
    }

    /// <summary>
    /// Active uniquement la page d'index x et désactive les autres
    /// </summary>
    public void ChangePage(int x)
    {
        // Ne pas inclure le GameObject porteur du PageManager dans pageList
        for (int i = 0; i < pageList.Count; i++)
        {
            pageList[i].SetActive(i == x);
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

    // Pages de sélection par jour
    public void GoToMondaySelectionPage(int nb) => OpenSelectionPage(0, nb);
    public void GoToTuesdaySelectionPage(int nb) => OpenSelectionPage(1, nb);
    public void GoToWednesdaySelectionPage(int nb) => OpenSelectionPage(2, nb);
    public void GoToThursdaySelectionPage(int nb) => OpenSelectionPage(3, nb);
    public void GoToFridaySelectionPage(int nb) => OpenSelectionPage(4, nb);
    public void GoToSaturdaySelectionPage(int nb) => OpenSelectionPage(5, nb);
    public void GoToSundaySelectionPage(int nb) => OpenSelectionPage(6, nb);

    private void OpenSelectionPage(int day, int nb)
    {
        ChangePage(1);
        planner.SetDay(day, nb);
        SetMealDisplay();
    }

    public void SetMeal(Meal meal)
    {
        planner.SetMeal(meal);
        SetMealDisplay();
    }

    public void RemoveMeal()
    {
        planner.RemoveMeal();
        SetMealDisplay();
    }

    public void SetMealDisplay()
    {
        var current = planner.GetCurrentMeal();
        int index = planner.currentDay * 3 + planner.currentNb;

        if (current != null)
        {
            selectionCurrentMealDisplay.text = current.name;
            UpdateButtonDisplay(index, current.name, hasMealColor);
        }
        else
        {
            selectionCurrentMealDisplay.text = string.Empty;
            UpdateButtonDisplay(index, "+", noMealColor);
            mealLoader.ResetScroller();
        }
    }

    private void UpdateButtonDisplay(int index, string text, Color color)
    {
        var button = planner.daysButtons[index];
        button.GetComponentInChildren<TMP_Text>().text = text;
        button.GetComponent<Image>().color = color;
    }

    public void ResetDayDisplay(int day)
    {
        for (int nb = 0; nb < 3; nb++)
        {
            planner.SetDay(day, nb);
            RemoveMeal();
        }
        planner.ResetDay();
    }

    public void ResetAllDaysDisplay()
    {
        for (int day = 0; day < 7; day++)
            ResetDayDisplay(day);
    }

    public void LoadAllMeal()
    {
        planner.LoadPlan();

        for (int day = 0; day < 7; day++)
        {
            for (int nb = 0; nb < 3; nb++)
            {
                planner.SetDay(day, nb);
                SetMealDisplay();
            }
        }
        planner.ResetDay();
    }
}
