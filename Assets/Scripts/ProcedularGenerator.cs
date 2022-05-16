using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedularGenerator : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] GameObject dirt, grass;
    void Start()
    {
        Generation();
    }

    private void Generation()
    {
        for (int xx = 0; xx < width; xx++)
        {
            for (int yy = 0; yy < height; yy++)
            {
                Instantiate(dirt, new Vector2(xx,yy), Quaternion.identity);
            }
            Instantiate(grass, new Vector2(xx, height), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
