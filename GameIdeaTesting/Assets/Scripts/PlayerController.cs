using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;

    [Header("Statische Daten")]
    public PlayerData playerData;

    private Vector3 movementDirection = Vector3.zero;
    private bool playerGrounded;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerGrounded = characterController.isGrounded;

        //movement
       /* Vector3 inputMovement = transform.forward * (playerData.getMovementSpeed() * Input.GetAxisRaw("Vertical"));
        characterController.Move(inputMovement * Time.deltaTime);

        transform.Rotate(Vector3.up * (Input.GetAxisRaw("Horizontal") * playerData.getRotationSpeed()));

*/
        //jumping
        if(Input.GetButton("Jump") && playerGrounded)
        {
            movementDirection.y = playerData.getJumpSpeed();
        }
        movementDirection.y -= playerData.getGravity() * Time.deltaTime;

        characterController.Move(movementDirection * Time.deltaTime);


        //animations
        animator.SetBool(IsWalking, Input.GetAxisRaw("Vertical") != 0);
       // animator.SetBool(IsAttacking, !characterController.isGrounded);

    }

    private void OnMouseDown()
    {
        Debug.Log(playerData.getNameID() + "wurde angeklickt.");
    }
}