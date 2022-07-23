using UnityEngine;
public class StopTrigger : MonoBehaviour
{
    [SerializeField] private Transform lookAtTarget;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Follower>(out Follower follower))
        {
            follower.SetMove();
            follower.GetComponent<LookAtTarget>().StartRotating(lookAtTarget);
            follower.GetComponent<ClientAnimationController>().IdleAnimation(true);

            EventsManager.OnClientOrder?.Invoke();
        }
    }
}
