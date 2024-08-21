using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    public GameObject shop;

    private SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                shop.SetActive(true);
                shop.GetComponent<ShopController>().pos = this.transform;
            }
        }

        if (transform.childCount > 0)
        {
            //Hide slot
            Color newColor = sp.color;
            newColor.a = 0;
            sp.color = newColor;
        } else
        {
            Color newColor = sp.color;
            newColor.a = 255;
            sp.color = newColor;
        }
    }
}
