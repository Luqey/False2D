using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class SurfaceTile : TileBase
{
    public Sprite sprite;

    public enum SurfaceType
    {
        None,
        Wood,
        Grass,
        Snow,
        Metal,
        Dirt,
    }

    public SurfaceType surfaceType;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
        tileData.color = Color.white;
        tileData.transform = Matrix4x4.identity;
        tileData.flags = TileFlags.LockTransform;
        tileData.colliderType = Tile.ColliderType.None;
    }
}
