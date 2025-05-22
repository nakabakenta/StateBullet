using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    public Slider sliderHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //プレイヤーの体力値を取得して体力スライダーに入れる
        sliderHp.minValue = 0;//最小値
        sliderHp.maxValue = 1;//最大値
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
