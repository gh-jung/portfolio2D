using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OnClcikUIButton : MonoBehaviour
{
    public string sceneName;
    public AudioClip[] clickSound;
    public bool isDisabled;

    private UIButton uIButton;
    private UI2DSprite uI2DSprite;
    private UILabel uILabel;
    private AudioSource audioSource;

    private void Start()
    {
        uI2DSprite = GetComponent<UI2DSprite>();
        uIButton = GetComponent<UIButton>();
        uILabel = GetComponentInChildren<UILabel>();
        audioSource = GetComponent<AudioSource>();
        if (isDisabled)
        {
            //uIButton.SetState(UIButtonColor.State.Disabled, isDisabled);
            uIButton.isEnabled = !isDisabled;
            uILabel.color = new Color(100.0f/ 255.0f, 100.0f / 255.0f, 100.0f / 255.0f);
        }
    }

    public void OnClickButton()
    {
        if (!isDisabled)
        {
            StopAllCoroutines();
            StartCoroutine(OnSoundPlaySceneChnage());
        }
    }

    public void OnHover(bool isOver)
    {
        uILabel.cachedTransform.localScale = isOver ? Vector3.one * 1.2f : Vector3.one;
    }

    public IEnumerator OnSoundPlaySceneChnage()
    {
        audioSource.clip = clickSound[0];
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        audioSource.clip = clickSound[1];
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
