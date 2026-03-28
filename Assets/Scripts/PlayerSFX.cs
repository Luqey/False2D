using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private Tilemap surfaceTilemap;

    private enum SurfaceType
    {
        None,
        Wood,
        Grass,
        Snow,
        Metal,
        Dirt,
    }

    [SerializeField] private SurfaceType surfaceType;

    [SerializeField] private AudioClip[] woodSurfaceSounds;
    [SerializeField] private AudioClip[] grassSurfaceSounds;
    [SerializeField] private AudioClip[] snowSurfaceSounds;
    [SerializeField] private AudioClip[] metalSurfaceSounds;
    [SerializeField] private AudioClip[] dirtSurfaceSounds;

    [SerializeField] private AudioSource sfxSource;

    private bool played;


    public void Update()
    {
        Vector3Int cellPos = surfaceTilemap.WorldToCell(transform.position);
        SurfaceTile tile = surfaceTilemap.GetTile<SurfaceTile>(cellPos);
        if(tile != null)
        {
            surfaceType = (SurfaceType)tile.surfaceType;
        }
    }

    public void DecideFootStep()
    {
        if (!played)
        {
            played = true;
            switch (surfaceType)
            {
                case SurfaceType.None:
                    break;
                case SurfaceType.Wood:
                    StartCoroutine(PlayFootstep(woodSurfaceSounds));
                    break;
                case SurfaceType.Grass:
                    StartCoroutine(PlayFootstep(dirtSurfaceSounds));
                    break;
                case SurfaceType.Snow:
                    StartCoroutine(PlayFootstep(snowSurfaceSounds));
                    break;
                case SurfaceType.Metal:
                    StartCoroutine(PlayFootstep(metalSurfaceSounds));
                    break;
                case SurfaceType.Dirt:
                    StartCoroutine(PlayFootstep(dirtSurfaceSounds));
                    break;
            }
        }
    }

    private IEnumerator PlayFootstep(AudioClip[] sounds)
    {
        sfxSource.pitch = Random.Range(0.7f, 1.3f);
        sfxSource.clip = sounds[Random.Range(0, sounds.Length)];
        sfxSource.Play();

        yield return new WaitForSeconds(0.1f);
        played = false;
    }
}
