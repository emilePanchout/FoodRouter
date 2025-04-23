using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public List<Ingredient> primeurList = new List<Ingredient>();
    public List<Ingredient> boucherieList = new List<Ingredient>();
    public List <Ingredient> poissonerieList = new List<Ingredient>();
    public List<Ingredient> cremerieList = new List<Ingredient>();
    public List<Ingredient> epicerieList = new List<Ingredient>();

    public MealLoader mealLoader;

    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("ingredients");
        IngredientCategoryList categories = JsonUtility.FromJson<IngredientCategoryList>(jsonFile.text);

        primeurList = categories.primeur;
        boucherieList = categories.boucherie;
        poissonerieList = categories.poissonerie;
        cremerieList = categories.cremerie;
        epicerieList = categories.epicerie;

    }
}

[System.Serializable]
public class BaseIngredient
{
    public string name;
}

[System.Serializable]
public class IngredientCategoryList
{
    public List<Ingredient> primeur;
    public List<Ingredient> boucherie;
    public List<Ingredient> poissonerie;
    public List<Ingredient> cremerie;
    public List<Ingredient> epicerie;
}