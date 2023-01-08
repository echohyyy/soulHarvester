using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField]
    Transform origin;
    [SerializeField]
    GameObject[] tile;
    [SerializeField]
    SpriteRenderer sampleSize;

    //struct ZList<T>
    //{
    //    public List<T> a;
    //    public List<T> b;
    //}
    //ZList<ZList<GameObject>> map = new();

    List<GameObject> tiles = new();
    void Start()
    {
        var size = sampleSize.size;
        for (int i = -20; i < 20; i++)
        {
            for (int j = -20; j < 20; j++)
            {
                int idx = Random.Range(0, tile.Length);
                GameObject t = tile[idx];
                tiles.Add(Instantiate(t, origin.position + new Vector3((size.x- 0.01f) * i, (size.y - 0.01f) * j, 0), Quaternion.identity, transform));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
