using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public Rigidbody head;
    public LayerMask layerMask;
    public Animator BodyAnimator;

    private Vector3 currentLookTarget = Vector3.zero;


    private CharacterController _characterController;


    public void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.SimpleMove(moveDirection * moveSpeed);
    }

    public void FixedUpdate()
    {
        AddForceToHead();
        AdjustOrientation();
    }

    private void AdjustOrientation()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {
                currentLookTarget = hit.point;
            }
        }

        var targetPosition = new Vector3
        {
            x = hit.point.x,
            y = transform.position.y,
            z = hit.point.z
        };
        var rotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
    }

    private void AddForceToHead()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {
            BodyAnimator.SetBool("IsMoving", false);
        } else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
            BodyAnimator.SetBool("IsMoving", true);
        }
    }
}