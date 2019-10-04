using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OnClcikUIButton : MonoBehaviour
{
    public string sceneName;
    private UIButton uIButton;
    private UI2DSprite uI2DSprite;
    public bool isDisabled;

    private void Start()
    {
        uI2DSprite = GetComponent<UI2DSprite>();
        uIButton = GetComponent<UIButton>();
        if (isDisabled)
        {
            //uIButton.SetState(UIButtonColor.State.Disabled, isDisabled);
            uIButton.isEnabled = !isDisabled;
        }
    }

    public void OnClick()
    {
        if (!isDisabled)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
