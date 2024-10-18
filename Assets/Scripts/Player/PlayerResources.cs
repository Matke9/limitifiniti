using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public int cobalt = 0;
    public int silicates = 0;
    public int carbon = 0;

    public void PickUp(ResourceTypes resource, int amount)
    {
        switch (resource)
        {
            case ResourceTypes.Cobalt:
                cobalt += amount;break;

            case ResourceTypes.Silicates:
                silicates += amount; break;

            case ResourceTypes.Carbon:
                carbon += amount; break;
        }
    }
}
