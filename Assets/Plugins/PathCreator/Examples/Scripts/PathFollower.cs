using PathCreation;
using UnityEngine;

// Moves along a path at constant speed.
// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
public class PathFollower : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private float speed = 5;

    public PathCreator PathCreator
    {
        get => pathCreator;
        set
        {
            if (value != null)
            {
                pathCreator = value;
            }
        }
    }

    private float distanceTravelled;
    private bool moveAvailable;

    private void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    private void Update()
    {
        if (pathCreator != null && moveAvailable)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    private void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void SetMove()
    {
        moveAvailable = !moveAvailable;
    }
}
