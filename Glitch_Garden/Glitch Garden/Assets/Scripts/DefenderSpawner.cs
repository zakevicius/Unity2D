using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    GameObject defenderParent;
    Defender defender;
    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(SnapToGrid(GetSquareClicked()));
    }

    public void SetSelectedDefender(Defender defenderToSelected)
    {
        defender = defenderToSelected;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        var StarDisplay = FindObjectOfType<StarDisplay>();

        if (defender)
        {
            int defenderCost = defender.GetStarCost();


            if (StarDisplay.HaveEnoughStars(defenderCost))
            {
                spawnDefender(gridPos);
                StarDisplay.SpendStars(defenderCost);
            }
        }
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return worldPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        return new Vector2(newX, newY);
    }

    private void spawnDefender(Vector2 worldPos)
    {
        Defender newDefender = Instantiate(defender, worldPos, Quaternion.identity) as Defender;
        newDefender.transform.parent = defenderParent.transform;
    }
}
