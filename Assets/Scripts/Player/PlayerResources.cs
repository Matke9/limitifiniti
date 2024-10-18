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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource"))
        {
            Meteor m = other.gameObject.GetComponent<Meteor>();
            PickUp(m.resourceType, m.size);
            Destroy(other.gameObject);
        }
    }
}
