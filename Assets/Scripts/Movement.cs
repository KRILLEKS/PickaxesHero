using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [Space] public UnityEvent atStop;
    [Space] public UnityEvent whenMovementResumes;

    // global variables
    private GridBehavior _gridBehavior;
    private List<GameObject> path = new List<GameObject>();
    private CsGlobal csGlobal;
    private Rigidbody2D rigidBody;
    private CharacterAnimatorController animatorController;

    //local variables
    private Vector3 movePosition = new Vector3(0, 0, 0);
    private int index;
    private bool isStanding = true;

    private void Awake()
    {
        _gridBehavior = FindObjectOfType<GridBehavior>();
        csGlobal = FindObjectOfType<CsGlobal>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        animatorController = FindObjectOfType<CharacterAnimatorController>();

        if (atStop == null)
            new UnityEvent();

        if (whenMovementResumes == null)
            new UnityEvent();
    }

    private void FixedUpdate()
    {
        MovementController();
    }

    // Move our character and Invokes atStop and whenMovementResumes Events
    private void MovementController()
    {
        if (path != null &&
            index < path.Count)
        {
            if (isStanding)
                whenMovementResumes.Invoke();

            isStanding = false;

            Move();
        }
        else if (!isStanding)
        {
            isStanding = true;
            atStop.Invoke();
        }

        // Move our character
        void Move()
        {
            if (transform.position + new Vector3(0, 0, -10) !=
                path[index].transform.position)
            {
                rigidBody.MovePosition(Vector2.MoveTowards(transform.position,
                    path[index].transform.position,
                    Time.fixedDeltaTime * speed));
            }
            else
            {
                index++;
                if (index < path.Count)
                    animatorController.SetValues(path[index].transform
                        .position);
            }
        }
    }

    // it calls by onTouch event in CsGlobal and sets all values
    public void InitialSetUp()
    {
        movePosition = csGlobal.g_mousePosition; // sets end position
        CheckIsTouchingShop();

        if (isStanding && _gridBehavior.CanAchive(movePosition)
        ) // executes when player is standing
            setPath(transform.position);
        else if (_gridBehavior.CanAchive(movePosition)
        ) // executes when player is moving
            setPath(path[index].transform.position);

        // sets path and reset index
        void setPath(Vector3 startPosition)
        {
            _gridBehavior.SetPosition(startPosition,
                true); // sets start position
            _gridBehavior.SetPosition(movePosition, false); // sets end position
            _gridBehavior.SetDistance();
            _gridBehavior.SetPath();

            path = _gridBehavior.g_path;
            index = 0;
        }

        void CheckIsTouchingShop()
        {
            if (Physics2D.OverlapPoint(movePosition))
                if (Physics2D.OverlapPoint(movePosition).gameObject.layer ==
                    7) //touching shop
                {
                    movePosition = Physics2D
                                   .OverlapPoint(movePosition).gameObject
                                   .transform
                                   .GetChild(0).position;
                }
        }
    }
}