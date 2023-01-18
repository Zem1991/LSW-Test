using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Character, Interactable
{
    public string GetInteractionText()
    {
        return "Talk to Shopkeeper";
    }

    public Vector3 GetInteractionPosition()
    {
        return transform.position;
    }

    public bool Interact(Player player)
    {
        throw new System.NotImplementedException();
    }
}
