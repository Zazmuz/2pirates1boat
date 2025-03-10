using System.Collections;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public Grid grid;
    public GameInformation gameInformation;

    public delegate void ShipMovementComplete();
    public event ShipMovementComplete OnShipMovementComplete;

    private IEnumerator MoveGridOut(Vector2 startGridPos, Vector2 targetGridPos, float duration)
    {
        grid.transform.position = startGridPos;
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector2[] startPlayerPositions = new Vector2[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            startPlayerPositions[i] = (Vector2)players[i].transform.position - startGridPos;
        }

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = Mathf.SmoothStep(0, 1, t);
            Vector2 newGridPos = Vector2.Lerp(startGridPos, targetGridPos, t);
            grid.transform.position = newGridPos;

            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = startPlayerPositions[i] + newGridPos;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        grid.transform.position = targetGridPos;
        OnShipMovementComplete?.Invoke();
    }

    public void Move(Vector2 from, Vector2 to, float duration)
    {
        FindObjectOfType<HullBreachParent>().RemoveAllBreaches();
        StartCoroutine(MoveGridOut(from, to, duration));
    }
}
