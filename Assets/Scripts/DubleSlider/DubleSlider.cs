using System;
using UnityEngine;
using UnityEngine.UI;

public class DubleSlider : MonoBehaviour
{

    public Image LeftImage;
    public Image RightImage;
    public Slider LeftSlider;
    public Slider RightSlider;

    public float LeftCount;
    public float RightCount;
    public float DeltaRange = 0.048f;

    public Action<float> Action;
    
    void Start()
    {
        Init();
        
    }

    void Update()
    {
        LeftCount = LeftSlider.value;
        RightCount = RightSlider.value;
    }

    public void LeftDelta()
    {
        if (RightSlider.value - LeftSlider.value < DeltaRange)
        {
            LeftSlider.value = RightSlider.value - DeltaRange;
        }

        LeftImage.fillAmount = 1f - LeftSlider.value;
        Action.Invoke(LeftImage.fillAmount);
    }

    public void RightDelta()
    {
        if (RightSlider.value - LeftSlider.value < DeltaRange)
        {
            RightSlider.value = LeftSlider.value + DeltaRange;
        }

        RightImage.fillAmount = RightSlider.value;
        Action.Invoke(RightImage.fillAmount);
    }

    public void Init()
    {
        LeftImage.fillAmount = 1f;
        RightImage.fillAmount = 1f;
        LeftSlider.value = 0f;
        RightSlider.value = 1f;
    }
}
