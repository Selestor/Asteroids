using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuclideanTorusScript : MonoBehaviour
{
    private float widthMargin;
    private float heightMargin;

    private void Awake()
    {
        widthMargin = 0.02f;
        heightMargin = 0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x > (1 + widthMargin))
        {

            gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0 - widthMargin, pos.y, pos.z));

        }
        else if (pos.x < (0 - widthMargin))
        {
            gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1 + widthMargin, pos.y, pos.z));
        }

        else if (pos.y > (1 + heightMargin))
        {
            gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(pos.x, 0 - heightMargin, pos.z));
        }

        else if (pos.y < (0 - heightMargin))
        {
            gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(pos.x, 1 + heightMargin, pos.z));
        }
    }
}
