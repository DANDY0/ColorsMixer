using System.Collections;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private Transform target;
    private Coroutine lookCoroutine;

    private void OnEnable()
    {
        EventsManager.OnLevelCompleted += StopRotating;
        EventsManager.OnLevelFailed += StopRotating;
    }

    private void OnDisable()
    {
        EventsManager.OnLevelCompleted -= StopRotating;
        EventsManager.OnLevelFailed -= StopRotating;
    }

    public void StartRotating(Transform lookAtTarget)
    {
        target = lookAtTarget;

        if(lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }

        lookCoroutine = StartCoroutine(LookAt());
    }

    public void StopRotating()
    {
        StopCoroutine(lookCoroutine);
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * speed;
            yield return null;
        }
    }
}
