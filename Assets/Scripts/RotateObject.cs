using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public enum RotateAxis { X, Y }

public class RotateObject : MonoBehaviour
{
    [Header("Perspective")]
    [SerializeField] private int perspective;
    [SerializeField] public bool locked;


    [Header("Rotation")]
    public RotateAxis rotateAxis;
    public float rotateDuration = 0.35f;
    public float hiddenAngle = 90f;

    private Quaternion visibleRotation;
    private Quaternion hiddenRotation;

    private bool isVisible;
    private Coroutine rotateCoroutine;

    private void Awake()
    {
        visibleRotation = transform.rotation;

        Vector3 hiddenEuler = transform.eulerAngles;

        if (rotateAxis == RotateAxis.X)
        {
            hiddenEuler.x += hiddenAngle;
        }
        else
        {
            hiddenEuler.x -= hiddenAngle;
        }

        hiddenRotation = Quaternion.Euler(hiddenEuler);

        transform.localRotation = hiddenRotation;
    }

    private int lastRotation = -1;
    void Update()
    {
        int curr = PlayerController.Instance.currRotation;

        if(curr == lastRotation)
        {
            return;
        }
        lastRotation = curr;

        bool shouldBeVisible = curr == perspective;

        if (shouldBeVisible && !isVisible)
        {
            RotateIn();
        }
        else if (!locked && !shouldBeVisible && isVisible)
        {
            RotateOut();
        }
    }

    void RotateIn()
    {
        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateCoroutine = StartCoroutine(Rotate(hiddenRotation, visibleRotation, true));
    }

    void RotateOut()
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }

        rotateCoroutine = StartCoroutine(Rotate(visibleRotation, hiddenRotation, false));
    }

    IEnumerator Rotate(Quaternion from, Quaternion to, bool becomingVisible)
    {
        isVisible = becomingVisible;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / rotateDuration;
            transform.localRotation = Quaternion.Slerp(from, to, t);
            yield return null;
        }

        transform.localRotation = to;
    }

    [SerializeField] GameObject lockObj;
    private GameObject lockObjHolder;
    public void Lock(bool isLocked)
    {
        locked = isLocked;
        if(isLocked == true)
        {
            lockObjHolder = Instantiate(lockObj, transform.position, PlayerController.Instance.transform.rotation, transform);
        }
        else
        {
            Destroy(lockObjHolder);
        }
    }

}
