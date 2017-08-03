using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Circle : MonoBehaviour
{
	public int segments;
	public int smooth;
	public int endSmooth;
	public float xradius;
	public float yradius;
	public float maxAngle;

	private LineRenderer line;
	private Ray2D ray;

	void Start()
	{
		line = gameObject.GetComponent<LineRenderer>();
		line.numPositions = segments + 1;
		line.numCornerVertices = smooth;
		line.numCapVertices = endSmooth;

		line.useWorldSpace = false;
		CreatePoints();
	}


	void CreatePoints()
	{
		float x;
		float y;
		float z = 0f;

		float angle = 20f;
		List<Vector2> path = new List<Vector2>();
		
		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
			y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

			line.SetPosition(i, new Vector3(x, y, z));
			angle += (maxAngle / segments);
		}

		for (int i = 0; i < segments; i++)
		{
			AddColliderToLine(line, line.GetPosition(i), line.GetPosition(i + 1));
			if (i == 0)
			{
				AddCircleColliderToLine(line, line.GetPosition(i));
			}
			else if (i == segments - 1)
			{
				AddCircleColliderToLine(line, line.GetPosition(i + 1));
			}
		}
	}

	private void AddColliderToLine(LineRenderer line, Vector3 startPoint, Vector3 endPoint)
	{
		//create the collider for the line
		BoxCollider2D lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider2D>();

		lineCollider.tag = "Challenge";
		//set the collider as a child of your line
		lineCollider.transform.parent = line.transform;
		// get width of collider from line 
		float lineWidth = line.endWidth;
		// get the length of the line using the Distance method
		float lineLength = Vector3.Distance(startPoint, endPoint);
		// size of collider is set where X is length of line, Y is width of line
		//z will be how far the collider reaches to the sky
		lineCollider.size = new Vector3(lineLength, lineWidth);
		// get the midPoint
		Vector3 midPoint = (startPoint + endPoint) / 2;
		// move the created collider to the midPoint
		lineCollider.transform.position = transform.position + midPoint;


		//heres the beef of the function, Mathf.Atan2 wants the slope, be careful however because it wants it in a weird form
		//it will divide for you so just plug in your (y2-y1),(x2,x1)
		float angle = Mathf.Atan2((endPoint.x - startPoint.x), (endPoint.y - startPoint.y));

		// angle now holds our answer but it's in radians, we want degrees
		// Mathf.Rad2Deg is just a constant equal to 57.2958 that we multiply by to change radians to degrees
		angle *= Mathf.Rad2Deg;

		//were interested in the inverse so multiply by -1
		angle *= -1;
		// now apply the rotation to the collider's transform, carful where you put the angle variable
		// in 3d space you don't wan't to rotate on your y axis
		lineCollider.transform.Rotate(0, 0, angle + 90);
	}

	private void AddCircleColliderToLine(LineRenderer line, Vector3 point)
	{
		CircleCollider2D lineCollider = new GameObject("LineCollider").AddComponent<CircleCollider2D>();
		lineCollider.tag = "Challenge";
		lineCollider.transform.parent = line.transform;
		float lineWidth = line.endWidth;
		lineCollider.radius = lineWidth / 2;
		lineCollider.transform.position = transform.position + point;
	}
}
