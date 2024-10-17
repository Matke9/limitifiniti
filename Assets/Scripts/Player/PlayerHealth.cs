using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    public void AddHealth(int healthToAdd)
    {
        health += healthToAdd;
    }

    public void RemoveHealth(int healthToRemove)
    {
        health -= healthToRemove;
    }
}
