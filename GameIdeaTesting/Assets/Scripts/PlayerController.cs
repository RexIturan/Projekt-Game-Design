using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;

    [Header("Statische Daten")]
    public PlayerData playerData;
    
    public LayerMask playerMask;

    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");


    // Start is called before the first frame update
    void Start()
    {
        //GameEvents.current.onPlayerClicked += onPlayerClicked;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GameEvents.current.PlayerMoved(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        onPlayerClicked();
    }

    private void PlayerMove()
    {
        //movement
        Vector3 inputMovement = transform.forward * (playerData.getMovementSpeed() * Input.GetAxisRaw("Vertical"));
        characterController.Move(inputMovement * Time.deltaTime);

        transform.Rotate(Vector3.up * (Input.GetAxisRaw("Horizontal") * playerData.getRotationSpeed()));
        animator.SetBool(IsWalking, Input.GetAxisRaw("Vertical") != 0);
        GameEvents.current.PlayerMoved(gameObject.transform);
    }

    private void onPlayerClicked()
    {
        // Did we hit the surface?
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, playerMask, QueryTriggerInteraction.Collide) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Der Spieler wurde getroffen");
            GameEvents.current.PlayerClicked(this.gameObject);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            GameEvents.current.GridClicked();   
        }
    }
    
    private void OnMouseDown()
    {
        //Debug.Log(playerData.getNameID() + "wurde angeklickt.");
        GameEvents.current.PlayerClicked(this.gameObject);
    }
}