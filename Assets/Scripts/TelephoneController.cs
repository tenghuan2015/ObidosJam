using UnityEngine;

public class TelephoneController : MonoBehaviour
{
    public float moveSpeed = 3;
    public GameObject Player;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Left/Right arrow keys
        float moveY = Input.GetAxis("Vertical"); // Up/Down arrow keys

        // Calculate new position
        Vector3 newPosition = Player.transform.position + new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        //// Apply constraints
        //newPosition.x = Mathf.Clamp(newPosition.x, xConstraints.x, xConstraints.y);
        //newPosition.y = Mathf.Clamp(newPosition.y, yConstraints.x, yConstraints.y);

        // Update the object's position
        Player.transform.position = newPosition;
    }
}
