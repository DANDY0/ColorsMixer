using UnityEngine;

public class ClientAnimationController : MonoBehaviour
{
    private Animator animator;

    private readonly string idleKeyAnimator = "isIdle";
    private readonly string walkKeyAnimator = "isWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        WalkAnimation(true);
    }
    private void OnEnable()
    {
        EventsManager.OnLevelCompleted += FinalAnimation;
        EventsManager.OnLevelFailed += FinalAnimation;
    }
    public void IdleAnimation(bool isIdle)
    {
        animator.SetBool(idleKeyAnimator, isIdle);
        animator.SetBool(walkKeyAnimator, false);
    }
    public void WalkAnimation(bool isWalking)
    {
        animator.SetBool(walkKeyAnimator , isWalking);
        animator.SetBool(idleKeyAnimator , false);
    }
    private void FinalAnimation()
    {
        WalkAnimation(true);
    }
    private void OnDisable()
    {
        EventsManager.OnLevelCompleted -= FinalAnimation;
        EventsManager.OnLevelFailed -= FinalAnimation;
    }
}
