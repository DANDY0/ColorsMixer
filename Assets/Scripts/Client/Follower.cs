using PathCreation;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private float speed = 5;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    private float distanceTravelled;
    private bool moveAvailable;

    private void OnEnable()
    {
        EventsManager.OnLevelCompleted += SetMove;
        EventsManager.OnLevelFailed += SetMove;
    }
    private void Start()
    {
        if (pathCreator != null)
            pathCreator.pathUpdated += OnPathChanged;
        SetMove();
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
    private void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
    public void SetMove()
    {
        moveAvailable = !moveAvailable;
    }
    private void OnDisable()
    {
        EventsManager.OnLevelCompleted -= SetMove;
        EventsManager.OnLevelFailed -= SetMove;
    }
}

