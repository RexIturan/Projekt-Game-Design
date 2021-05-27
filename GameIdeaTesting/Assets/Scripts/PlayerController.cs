using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private float movementSpeed, rotationSpeed, jumpSpeed, gravity;

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
        // Check for mouse input
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("This hit at " + hit.point);
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                onMouseClicked(hit);
            }
        }
        
        playerGrounded = characterController.isGrounded;

        //movement
        Vector3 inputMovement = transform.forward * (movementSpeed * Input.GetAxisRaw("Vertical"));
        characterController.Move(inputMovement * Time.deltaTime);

        transform.Rotate(Vector3.up * (Input.GetAxisRaw("Horizontal") * rotationSpeed));


        //jumping
        if(Input.GetButton("Jump") && playerGrounded)
        {
            movementDirection.y = jumpSpeed;
        }
        movementDirection.y -= gravity * Time.deltaTime;

        characterController.Move(movementDirection * Time.deltaTime);


        //animations
        animator.SetBool(IsWalking, Input.GetAxisRaw("Vertical") != 0);
       // animator.SetBool(IsAttacking, !characterController.isGrounded);

    }

    void onMouseClicked(RaycastHit hit)
    {
        //List<GameObject> list = hit.transform.gameObject.GetComponent<GridBehaviour>().path;
        

    }
}