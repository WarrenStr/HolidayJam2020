using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSphere : MonoBehaviour
{
    public float radiusMovementScale = .1f;
    public float angleMovementScale = 5f;
    public GameObject cone;
    Vector2 distFromCenter;
    float xbound;
    float ybound;

    // Start is called before the first frame update
    void Start()
    {
        Renderer coneRend = cone.GetComponent<Renderer>();
        xbound = coneRend.bounds.size.x / 2;
        ybound = coneRend.bounds.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Find distance from center of cone, normalize and multiply by transform.position.z plugged into the equation of the line representing the cone
        distFromCenter.Set(this.transform.position.x, this.transform.position.z);
        distFromCenter = distFromCenter.normalized * (xbound * (1 - (this.transform.position.y / ybound)));

        float radius = Mathf.Clamp(distFromCenter.magnitude, 0, xbound);
        float theta = Vector2.Angle(Vector2.right, distFromCenter);
        print(theta);

        // Movement of the angle, analogous to movement in (rotation on) x on the tangent plane that includes the point on the surface of the cone
        if (Input.GetKey("d"))
        {
            theta += angleMovementScale;
        } 
        else if (Input.GetKey("a"))
        {
            theta -= angleMovementScale;
        }

        // Movement of the radius, analogous to movement in y on the tangent plane that includes the point on the surface of the cone
        if (Input.GetKey("w"))
        {
            radius -= radiusMovementScale;
        }
        else if (Input.GetKey("s"))
        {
            radius += radiusMovementScale;
        }

        // Apply movement (go back to cartesian)
        float x = radius * Mathf.Cos(Mathf.Deg2Rad * theta);
        float y = -(ybound / xbound) * (radius - xbound);
        float z = radius * Mathf.Sin(Mathf.Deg2Rad * theta);

        this.transform.position = new Vector3(x, y, z);
    }
}
