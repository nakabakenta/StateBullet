using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public int currentAttack;  //現在の攻撃力
    public string BulletMaster;//弾の持ち主のタグ保存

    public CharacterBase character;
    public Environment environment;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        environment = GameObject.Find("VirtualEnvironment").GetComponent<Environment>();
    }
}
