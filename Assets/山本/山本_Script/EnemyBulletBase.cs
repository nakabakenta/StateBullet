using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public float firstAttack;    //‰ŠúUŒ‚—Í
    public float currentAttack;  //Œ»İ‚ÌUŒ‚—Í

    public float firstSpeed;    //‰Šú’e‘¬
    public float currentSpeed;  //Œ»İ‚Ì’e‘¬

    Vector3 bulletVec;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //‰Šú’l‚ğİ’è
        currentAttack = firstAttack;  //UŒ‚—Í
        currentSpeed = firstSpeed;    //’e‘¬
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
        
    }
}
