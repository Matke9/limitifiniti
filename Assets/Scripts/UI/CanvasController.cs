using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Image ship;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 1f; // Speed of movement
    [SerializeField] private float radius = 50f; // Radius of the circular path

    private float time;

    private void Update()
    {
        time += Time.deltaTime * speed;

        // Calculate the midpoint between pointA and pointB
        Vector2 midPoint = (pointA.position + pointB.position) / 2f;

        // Calculate the direction perpendicular to the line between pointA and pointB
        Vector2 direction = (pointB.position - pointA.position).normalized;
        Vector2 perpendicularDir = new Vector2(-direction.y, direction.x);

        // Calculate the circular position
        float angle = Mathf.PingPong(time, 1) * Mathf.PI; // Moves from 0 to π
        Vector2 circularOffset = Mathf.Sin(angle) * radius * perpendicularDir;

        // Interpolate between pointA and pointB for the center of the arc
        Vector2 centerPosition = Vector2.Lerp(pointA.position, pointB.position, Mathf.PingPong(time, 1));

        // Apply the offset to the center position to create the curved movement
        Vector2 finalPosition = centerPosition + circularOffset;

        // Set the ship's position
        ship.rectTransform.position = finalPosition;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
