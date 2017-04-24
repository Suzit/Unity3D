using UnityEngine;
using System.Collections;
[System.Serializable]
public class Boundary{
	public float xMin,xMax,zMin,zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;
	public GameObject shot;
	public float fireRate;
	
	private float nextFire;
	public Transform shotSpawn;//shotSpawn.transform.position(GameObject)
	public Boundary boundary;
	private Quaternion calibrationQuaternion;
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;
	void Start(){
		CalibrateAccelerometer ();
	}
	void Update(){
		//Instantiate(object,postion,rotation);
		//Instantiate (shot, shotSpawn, shotSpawn.rotation);
		if (areaButton.CanFire () && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}
	}
	void CalibrateAccelerometer (){
		//Used to calibrate the Iput.acceleration input
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);

	}
	Vector3 FixAcceleration(Vector3 acceleration){
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	void FixedUpdate(){
		//float moveHorizontal = Input.GetAxis ("Horizontal");//GetAxis returns 0 and 1 only
		//float moveVertical = Input.GetAxis ("Vertical");
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		//Vector3 accelerationRaw = Input.acceleration;
		//Vector3 movement = new Vector3 (acceleration.x,0.0f,acceleration.y);
		//Vector3 acceleration = FixAcceleration (accelerationRaw);
		Vector2 direction = touchPad.GetDirection ();
		Vector3 movement = new Vector3 (direction.x,0.0f,direction.y);
		GetComponent<Rigidbody>().velocity = movement*speed;
		GetComponent<Rigidbody> ().position = new Vector3 (

			Mathf.Clamp(GetComponent<Rigidbody>().position.x,boundary.xMin,boundary.xMax), 
			0.0f, 
			Mathf.Clamp(GetComponent<Rigidbody>().position.z,boundary.zMin,boundary.zMax)
			);
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);//velocity left to right
		//Quaternion Euler rotate x,y,z axix

             }


}
