using UnityEngine;

public class FishController : EnemyBase
{
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(isDamage)
        {
            //animator.SetTrigger("")
        }
    }
}
