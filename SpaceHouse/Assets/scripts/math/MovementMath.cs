using UnityEngine;
using System.Collections;

public class MovementMath : MonoBehaviour {

	public Vector2 getToWardsHeading(Vector2 heading) {
		/* Normalized vector that points from transforms position to given heading */
		Vector2 foward = new Vector2 (heading.x - transform.position.x, heading.y - transform.position.y);
		return foward.normalized;
	}
	
	public Vector2 getHeadingVector(Camera mainCamera) {
		Vector3 mousePosition3D = mainCamera.ScreenToWorldPoint (Input.mousePosition);
		return new Vector2 (mousePosition3D.x, mousePosition3D.y);
	}
	
	public Vector2 getFoward() {
		if (this.transform.rotation.eulerAngles.z <= 0.0f && this.transform.rotation.eulerAngles.z >= 90) {
			return new Vector2 (Mathf.Sin (Mathf.Deg2Rad * this.transform.rotation.eulerAngles.z), Mathf.Cos (Mathf.Deg2Rad * this.transform.rotation.eulerAngles.z));
		} 
		
		else if (this.transform.rotation.eulerAngles.z < 90 && this.transform.rotation.eulerAngles.z > 180) {
			return new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 90)), Mathf.Sin (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 90)));
		} 
		
		else if (this.transform.rotation.eulerAngles.z < 180 && this.transform.rotation.eulerAngles.z > 270) {
			return new Vector2 (Mathf.Sin (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 180)), Mathf.Cos (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 180)));
		} 
		
		else {
			return new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 270)), Mathf.Sin (Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z - 270)));
		}
	}
	
	public void turnTowardsHeading(Vector2 heading, Vector2 foward, float rotationSpeed) {
		float angleHeading = getVectorsUnityAngle (heading);
		float angleFoward = getVectorsUnityAngle (foward);
		
		if (Vector2.Angle (foward, heading) - Mathf.Abs (angleHeading - angleFoward) < 0.1) {
			this.transform.rotation = Quaternion.Euler (0, 0, Mathf.Lerp (this.transform.rotation.z, Mathf.Rad2Deg * angleHeading, Time.deltaTime * rotationSpeed));
		} 
		
		else {
			this.transform.rotation = Quaternion.Euler (0, 0, Mathf.Lerp (this.transform.rotation.z, Mathf.Rad2Deg * angleHeading, Time.deltaTime * rotationSpeed));
		}
	}
	
	public float getVectorsUnityAngle(Vector2 vector) {
		Vector2 j = new Vector2 (0, 1);
		Vector2 v = vector.normalized;
		
		if (v.x < 0) {
			return Mathf.Acos (Vector2.Dot (j, v));
		} 
		
		else {
			return 2 * Mathf.PI - Mathf.Acos (Vector2.Dot (j, v));
		}
	}
}
