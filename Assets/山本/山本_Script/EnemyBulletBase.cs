using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public float firstAttack;    //‰ŠúUŒ‚—Í
    public float currentAttack;  //Œ»İ‚ÌUŒ‚—Í

    public float firstSpeed;    //‰Šú’e‘¬
    public float currentSpeed;  //Œ»İ‚Ì’e‘¬

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //‰Šú’l‚ğİ’è
        currentAttack = firstAttack;  //UŒ‚—Í
        currentSpeed = firstSpeed;    //’e‘¬
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
