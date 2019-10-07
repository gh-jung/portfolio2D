using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OnClcikUIButton : MonoBehaviour
{
    public string sceneName;
    private UIButton uIButton;
    private UI2DSprite uI2DSprite;
    private UILabel uILabel;
    public bool isDisabled;

    private void Start()
    {
        uI2DSprite = GetComponent<UI2DSprite>();
        uIButton = GetComponent<UIButton>();
        uILabel = GetComponentInChildren<UILabel>();
        if (isDisabled)
        {
            //uIButton.SetState(UIButtonColor.State.Disabled, isDisabled);
            uIButton.isEnabled = !isDisabled;
            uILabel.color = new Color(100.0f/ 255.0f, 100.0f / 255.0f, 100.0f / 255.0f);
        }
    }

    public void OnClick()
    {
        if (!isDisabled)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnHover(bool isOver)
    {
        uILabel.cachedTransform.localScale = isOver ? Vector3.one * 1.2f : Vector3.one;
    }
}
