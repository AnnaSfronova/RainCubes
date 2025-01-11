using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb>
{
    public void CreateBomb(Vector3 position)
    {
        Pool.Get(out Bomb bomb);
        bomb.Init(position);
    }
}
