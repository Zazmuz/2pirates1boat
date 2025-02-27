using System;
using UnityEngine;

public class RouteSystem : MonoBehaviour
{
    public GameInformation gameInformation;
    public string[] routes = { "short", "medium", "long" };

    void Start(){

        string randomRoute = routes[UnityEngine.Random.Range(0, routes.Length)];
        SetTimeTilDestination(randomRoute);
    }

    void SetTimeTilDestination(string route)
    {
        switch (route.ToLower()) 
        {
            case "long":
                gameInformation.timeTilDestination = 120f;
                break;
            case "medium":
                gameInformation.timeTilDestination = 60f;
                break;
            case "short":
                gameInformation.timeTilDestination = 30f;
                break;
            default:
                Debug.LogWarning("Invalid route type provided.");
                break;
        }
    }
}
