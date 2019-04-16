using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopText : MonoBehaviour
{
    public float aliveTime;
    public float offsetX;
    public float offsetY;
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);

        Destroy(gameObject, aliveTime);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
