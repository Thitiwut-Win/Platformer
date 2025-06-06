using UnityEngine;

public class HiddenSpike : Trap
{
    [SerializeField]
    private Animator animator;
    public override bool OnTriggerEnter2D(Collider2D collider2D)
    {
        if (base.OnTriggerEnter2D(collider2D))
        {
            animator.Play("Base Layer.HiddenSpike", 0, 0);
            return true;
        }
        return false;
    }
}