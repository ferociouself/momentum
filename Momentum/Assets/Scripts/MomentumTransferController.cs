using UnityEngine;
using System.Collections;

public class MomentumTransferController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonUp(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero, 0f);
            if (hit != null && hit.collider != null)
            {
                SelectObject(hit.collider.gameObject);
            }
        }
	}

    /// <summary>
    /// Select the given object
    /// </summary>
    void SelectObject(GameObject obj) {
        Debug.Log("Select: " + obj);
    }
}
