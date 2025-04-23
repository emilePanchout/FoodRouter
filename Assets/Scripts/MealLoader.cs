using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MealLoader : MonoBehaviour
{
    public MealList JSONMealList;
    public PageManager pageManager;

    public Transform selectionGrid;
    public GameObject mealPanelPrefab;

    public List<Color> CategoryColorList;
    private Dictionary<string, Color> categoryColors = new Dictionary<string, Color>();

    void Start()
    {

        TextAsset jsonFile = Resources.Load<TextAsset>("recipes");
        JSONMealList = JsonUtility.FromJson<MealList>(jsonFile.text);

        CreateMeals();
        ResetScroller();
    }

    public void CreateMeals()
    {
        var sortedMeals = JSONMealList.meals.OrderBy(meal => meal.name).ToList();
        int colorIndex = 0;

        foreach (var meal in sortedMeals)
        {
            GameObject go = Instantiate(mealPanelPrefab, selectionGrid);
            go.GetComponent<Button>().onClick.AddListener(() => pageManager.SetMeal(meal));
            go.GetComponentInChildren<TMP_Text>().text = meal.name;
            go.name = meal.name;

            if (!categoryColors.ContainsKey(meal.category))
            {
                categoryColors[meal.category] = CategoryColorList[colorIndex];
                colorIndex = (colorIndex + 1) % CategoryColorList.Count;
            }

            go.GetComponent<Image>().color = categoryColors[meal.category];
        }
    }

    public void ResearchMeals(string searchValue)
    {

        foreach (Transform child in selectionGrid)
        {
            child.gameObject.SetActive(false);
            bool searchedMeals = child.name.ToLower().Contains(searchValue.ToLower());
            child.gameObject.SetActive(searchedMeals);
        }
        Debug.Log(searchValue);
    }

    public void ResetScroller()
    {
        selectionGrid.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
    }
}

[System.Serializable]
public class Ingredient
{
    public string name;
    public float quantity;
    public string unit;
}

[System.Serializable]
public class Meal
{
    public string name;
    public string category;
    public List<Ingredient> ingredients;
}

[System.Serializable]
public class MealList
{
    public List<Meal> meals;
}