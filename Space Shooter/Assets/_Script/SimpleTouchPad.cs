using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler {
	private Vector2 origin;
	private Vector2 direction;
	private bool touched;
	private int pointerID;
	private Vector2 smoothDirection;
	public float smoothing;
	void Awake () {
		direction = Vector2.zero;
		touched = false;
		//touched = false;
	}
	public void OnPointerDown (PointerEventData data){
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
		}

	}
	public void OnDrag (PointerEventData data){
		//compare the difference between start point and current point pos
		if (data.pointerId == pointerID) {
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;
		}
		//Debug.Log (direction);


	}
	public void OnPointerUp (PointerEventData data){
		//reset everything
		if (data.pointerId == pointerID) {
		direction = Vector2.zero;
		touched = false;
		}		

	}
	public Vector2 GetDirection ()  {
			smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		    return smoothDirection;
		//return direction;
	}


}
