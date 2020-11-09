using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [Space]
    public UnityEvent atStop;
    [Space]
    public UnityEvent whenMovementResumes;

    // global variables
    private GridBehavior gridBehavior;
    private List<GameObject> path = new List<GameObject>();
    private CsGlobal csGlobal;
    private Rigidbody2D rigidBody;

    //local variables
    private Vector3 movePosition = new Vector3(0, 0, 0);
    private int index;
    private bool isStanding = true;

    private void Awake()
    {
        gridBehavior = FindObjectOfType<GridBehavior>();
        csGlobal = FindObjectOfType<CsGlobal>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

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
        if (path != null && index < path.Count)
        {
            isStanding = false;
            whenMovementResumes.Invoke();

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
            if (transform.position + new Vector3(0, 0, -10) != path[index].transform.position)
                rigidBody.MovePosition(Vector2.MoveTowards(transform.position, path[index].transform.position, Time.fixedDeltaTime * speed));
            else
                index++;
        }
    }

    // it calls by onTouch event in CsGlobal and sets all values
    public void InitialSetUp()
    {
        movePosition = csGlobal.g_mousePosition; // sets end position

        if (isStanding && gridBehavior.CanAchive(movePosition)) // executes when player is satnding
            setPath(transform.position);
        else if (gridBehavior.CanAchive(movePosition)) // executes when player is moving
            setPath(path[index].transform.position);

        // sets path and reset index
        void setPath(Vector3 startPosition)
        {
            gridBehavior.SetPosition(startPosition, true); // sets start position
            gridBehavior.SetPosition(movePosition, false); // sets end position
            gridBehavior.SetDistance();
            gridBehavior.SetPath();

            path = gridBehavior.g_path;
            index = 0;
        }
    }

}
