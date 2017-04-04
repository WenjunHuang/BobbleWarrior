using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public Rigidbody head;

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.SimpleMove(moveDirection * moveSpeed);
    }

    private void FixedUpdate()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {

        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }
    }
}