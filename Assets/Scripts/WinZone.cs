using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [SerializeField] private float winDelay = 1f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(WinLevel());
        }
    }

    private IEnumerator WinLevel()
    {
        PixelPerfectCamera pixelCam = Camera.main.GetComponent<PixelPerfectCamera>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("WinAnim");

        yield return new WaitForSeconds(.5f);

        int zoom = 1;
        int target = 6;

        float timer = 0f;

        while (timer < winDelay)
        {
            timer += Time.deltaTime;
            float t = timer / winDelay;

            pixelCam.assetsPPU = Mathf.RoundToInt(Mathf.Lerp(zoom, target, t));

            yield return null;
        }

        pixelCam.assetsPPU = target;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
