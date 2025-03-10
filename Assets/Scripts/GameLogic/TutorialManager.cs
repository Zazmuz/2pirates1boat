using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameInformation gameInformation;
    public GameObject tutorialText;
    bool started = false;

    private void Start()
    {
        gameInformation.ResetGame();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!started)
        {
            started = true;
            gameInformation.gameStarted = true;
            gameInformation.numberOfHullBreaches = 3;
            tutorialText.SetActive(true);
        }
    }

    public void RemoveHullBreach()
    {
        Debug.Log("REDUCED");
        Debug.Log("BEFORE: " + gameInformation.numberOfHullBreaches);

        gameInformation.numberOfHullBreaches--;
        Debug.Log("AFTER: " + gameInformation.numberOfHullBreaches);

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Enter))
        //{
        //    RestartGame();
        //}
    }
}
