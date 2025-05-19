using UnityEngine;

public class FishController : EnemyBase
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(isDamage)
        {
            animator.SetTrigger("Hit");
            isDamage = false;
        }
    }
}
