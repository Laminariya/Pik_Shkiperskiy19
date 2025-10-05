using UnityEngine;
using UnityEngine.UI;

public class SpashalImageClass : MonoBehaviour
{

    private Button b_Click;
    private Image imageDown;
    private Image imageUp;
    
    public void Init()
    {
        b_Click = GetComponentInChildren<Button>(true);
        b_Click.onClick.AddListener(OnClick);
        imageDown = GetComponent<Image>();
        imageUp = GetComponentsInChildren<Image>(true)[1];
    }

    public void Show()
    {
        imageUp.enabled = false;
    }

    private void OnClick()
    {
        imageUp.enabled = !imageUp.enabled;
    }

}
