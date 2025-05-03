using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planner : MonoBehaviour
{
    public int currentDay;
    public int currentNb;

    public Meal[,] mealPlanned;
    public List<Button> daysButtons = new List<Button>();

    [Header("Cart")]
    public Cart cart = new Cart();
    public Transform cartGrid;
    public GameObject itemPrefab;
    public GameObject primeurPrefab;
    public GameObject boucheriePrefab;
    public GameObject poissoneriePrefab;
    public GameObject cremeriePrefab;
    public GameObject epiceriePrefab;


    public void Start()
    {
        mealPlanned = new Meal[7,3];


    }

    public void SetMeal(Meal meal)
    {
        mealPlanned[currentDay,currentNb] = meal;

        SavePlanInBrowser();
    }

    public void RemoveMeal()
    {
        mealPlanned[currentDay, currentNb] = null;
    }


    public void SetDay(int day, int nb)
    {
        currentDay = day;
        currentNb = nb;
    }


    public void ResetDay()
    {
        currentDay = -1;
        currentNb = -1;
    }

    public Meal GetCurrentMeal()
    {
        return mealPlanned[currentDay,currentNb];
    }


    public void Calculator()
    {
        // Reset cart
        ResetCartQuantity(cart.primeurList);
        ResetCartQuantity(cart.boucherieList);
        ResetCartQuantity(cart.poissonerieList);
        ResetCartQuantity(cart.cremerieList);
        ResetCartQuantity(cart.epicerieList);


        foreach (Meal meal in mealPlanned)
        {
            if(meal != null)
            {
                foreach (Ingredient ingredient in meal.ingredients)
                {

                    foreach (Ingredient baseIngr in cart.primeurList)
                    {
                        if(baseIngr.name == ingredient.name)
                        {
                            baseIngr.quantity = ingredient.quantity + baseIngr.quantity;
                        }
                    }

                    foreach (Ingredient baseIngr in cart.boucherieList)
                    {
                        if (baseIngr.name == ingredient.name)
                        {
                            baseIngr.quantity = ingredient.quantity + baseIngr.quantity;
                        }
                    }

                    foreach (Ingredient baseIngr in cart.cremerieList)
                    {
                        if (baseIngr.name == ingredient.name)
                        {
                            baseIngr.quantity = ingredient.quantity + baseIngr.quantity;
                        }
                    }
                    foreach (Ingredient baseIngr in cart.poissonerieList)
                    {
                        if (baseIngr.name == ingredient.name)
                        {
                            baseIngr.quantity = ingredient.quantity + baseIngr.quantity;
                        }
                    }
                    foreach (Ingredient baseIngr in cart.epicerieList)
                    {
                        if (baseIngr.name == ingredient.name)
                        {
                            baseIngr.quantity = ingredient.quantity + baseIngr.quantity;
                        }
                    }

                }
            }
            
        }

        CreateCartDisplays();
    }

    public void ResetCartQuantity(List<Ingredient> baseIngrList)
    {
        foreach (Ingredient baseIngr in baseIngrList)
        {
            baseIngr.quantity = 0;
        }
    }

    public void CreateCartDisplays()
    {
        CreateCartDisplay(cart.primeurList, primeurPrefab);
        CreateCartDisplay(cart.boucherieList, boucheriePrefab);
        CreateCartDisplay(cart.poissonerieList, poissoneriePrefab);
        CreateCartDisplay(cart.cremerieList, cremeriePrefab);
        CreateCartDisplay(cart.epicerieList, epiceriePrefab);

        cartGrid.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
    }

    public void CreateCartDisplay(List<Ingredient> cartList, GameObject categoryPrefab)
    {
        if (cartList.Count != 0)
        {
            bool addTitle = true;

            foreach (Ingredient ingredient in cartList)
            {

                if (ingredient.quantity > 0)
                {
                    if (addTitle)
                    {
                        Instantiate(categoryPrefab, cartGrid);
                        addTitle = false;
                    }

                    GameObject go = Instantiate(itemPrefab, cartGrid);
                    go.GetComponent<CartItem>().nameText.text = ingredient.name;
                    go.GetComponent<CartItem>().quantity.text = ingredient.quantity.ToString() + ingredient.unit;
                }
                else if(ingredient.quantity < 0)
                {
                    if (addTitle)
                    {
                        Instantiate(categoryPrefab, cartGrid);
                        addTitle = false;
                    }

                    GameObject go = Instantiate(itemPrefab, cartGrid);
                    go.GetComponent<CartItem>().nameText.text = ingredient.name;
                    go.GetComponent<CartItem>().quantity.text = "";
                }
            }

        }
    }

    public void ClearCartDisplay()
    {
        foreach(Transform item in cartGrid)
        {
            Destroy(item.gameObject);
        }
    }


    public void SavePlanInBrowser()
    {
        MealPlannedData data = new MealPlannedData();

        for (int day = 0; day < 7; day++)
        {
            for (int slot = 0; slot < 3; slot++)
            {
                Meal meal = mealPlanned[day, slot];
                if (meal != null)
                {
                    MealWrapper wrapper = new MealWrapper();
                    wrapper.day = day;
                    wrapper.slot = slot;
                    wrapper.meal = meal;
                    data.meals.Add(wrapper);

                    Debug.Log(data.meals[0].meal.name);
                }
            }
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("MealPlanned", json);
        PlayerPrefs.Save();

        Debug.Log("Saving In Browser");
    }

    public void LoadPlan()
    {
        if (PlayerPrefs.HasKey("MealPlanned"))
        {
            string json = PlayerPrefs.GetString("MealPlanned");
            MealPlannedData data = JsonUtility.FromJson<MealPlannedData>(json);

            mealPlanned = new Meal[7, 3];

            foreach (MealWrapper wrapper in data.meals)
            {
                mealPlanned[wrapper.day, wrapper.slot] = wrapper.meal;
            }
        }
    }

}
