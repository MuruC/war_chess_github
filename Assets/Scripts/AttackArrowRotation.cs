using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArrowRotation : MonoBehaviour
{
    public float rotateSpeed = 20f;
    Vector2 temp;
    float size;
    // Start is called before the first frame update
    void Start()
    {
        size = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,rotateSpeed*Time.deltaTime);

        float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) - 2.5f;
        temp = transform.localScale;
        temp.x += distance;
        temp.x = Mathf.Clamp(temp.x, 1, 2);
        transform.localScale = temp;
    }
}
