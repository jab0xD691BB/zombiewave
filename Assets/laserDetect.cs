using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserDetect : MonoBehaviour
{

    LineRenderer lineRenderer;
    MeshCollider meshCollider;

    float interval = 0.03f;
    float timer = 0.0f;
    bool hasHit = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        timer = interval + 1;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var usedInterval = interval;
        if (Time.deltaTime > usedInterval) usedInterval = Time.deltaTime;

        if (timer >= usedInterval)
        {
            timer = 0;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.up * 55 + transform.position);


            //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
            Debug.DrawRay(transform.position, transform.up * 55 + transform.position, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 9)
                    {
                        Vector3 distance = (hit.collider.transform.position - transform.position).normalized;
                        float d = distance.magnitude;
                        float dis = Vector3.Distance(hit.collider.transform.position, transform.position);
                        lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, 0));


                    }

                }
            }

        }

        timer += Time.deltaTime;

    }
}
