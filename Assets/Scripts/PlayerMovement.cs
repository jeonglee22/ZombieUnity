using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private static readonly int hashMove = Animator.StringToHash("Move");

	private PlayerInput input;
	private Rigidbody rb;
	private Animator animator;

	public float moveSpeed = 5f;
	public float rotateSpeed = 5f;

	private void Awake()
	{
		input = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		var rotation = Quaternion.Euler(0f, input.Rotate * rotateSpeed * Time.deltaTime, 0f);
		rb.MoveRotation(rb.rotation * rotation);

		var delta =  input.Move * moveSpeed * Time.deltaTime;
		rb.MovePosition(transform.position + transform.forward * delta);

		animator.SetFloat(hashMove, input.Move);
	}
}
