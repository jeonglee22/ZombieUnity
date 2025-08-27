using UnityEngine;
using UnityEngine.UIElements;

public class ItemMoving : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float moveSpeed = 1f;

    private Vector3 moveDir = Vector3.up;
    public float moveTop = 0.5f;
    public float moveBottom = 0f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0), Space.World);
        if(transform.position.y >= moveTop)
        {
            moveDir = Vector3.down;
        }
        else if(transform.position.y <= moveBottom)
        {
			moveDir = Vector3.up;
		}
		transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
	}
}
