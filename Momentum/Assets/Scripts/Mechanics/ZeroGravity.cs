using UnityEngine;
using System.Collections;

public class ZeroGravity : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject[] allObjs = GameObject.FindGameObjectsWithTag("Circle");
        foreach (GameObject obj in allObjs)
        {
            Vector3 pos = obj.transform.position;
            if (PointInOABB(pos, gameObject.GetComponent<BoxCollider2D>()))
            {
                obj.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        coll.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        coll.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    bool PointInOABB(Vector3 point, BoxCollider2D box)
    {
        Vector3 inverse = box.transform.InverseTransformPoint(point);
        point = new Vector2(inverse.x, inverse.y) - box.offset;

        float halfX = (box.size.x * 0.5f);
        float halfY = (box.size.y * 0.5f);
        if (point.x < halfX && point.x > -halfX &&
           point.y < halfY && point.y > -halfY)
            return true;
        else
            return false;
    }
}

