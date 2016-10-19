using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;
    // used to check for collisions
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        // inverse move time allows us to use multiplication instead of division
        inverseMoveTime = 1f / moveTime;
	}
	
    // out keyword causes params to be passed by reference
    public bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        // disable our collider so when we raycast, we don't hit our own collider
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        // open space to move into
        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        else
        {
            // can't move into that space
            return false;
        }
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
        {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();

        // moving is blocked, but there is a component we can interact with
        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }

    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        // sqrMagnitude is cheaper than magnitude which has to do a sqrt
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            // wait a frame before re-checking the while loop condition
            yield return null;
        }
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}
