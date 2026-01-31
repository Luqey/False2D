using UnityEngine;
using UnityEngine.UI;

public class RotateUI : MonoBehaviour
{
    [SerializeField] private Image qObj;
    [SerializeField] private Image eObj;

    [SerializeField] private Sprite qUnpressed, qPressed, eUnpressed, ePressed;

    [SerializeField] private Animator rotateAnim;

    private bool qSetOnce = true, eSetOnce = true;

    private void Awake()
    {
        rotateAnim.speed = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && qSetOnce)
        {
            qObj.sprite = qPressed;
            qSetOnce = false;
        }
        else if (Input.GetKeyUp(KeyCode.Q) && !qSetOnce)
        {
            qObj.sprite = qUnpressed;
            qSetOnce = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && eSetOnce)
        {
            eObj.sprite = ePressed;
            eSetOnce = false;
        }
        else if (Input.GetKeyUp(KeyCode.E) && !eSetOnce)
        {
            eObj.sprite = eUnpressed;
            eSetOnce = true;
        }
    }

    public void AnimRotateUI(string dir)
    {
        rotateAnim.speed = 1;
        rotateAnim.Play(dir);
    }
    public void AnimRotateUIStop()
    {
        rotateAnim.speed = 0;
    }
}
