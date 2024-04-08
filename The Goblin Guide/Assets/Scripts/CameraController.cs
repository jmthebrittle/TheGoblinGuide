using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	
	public Transform target;
	
	public Vector3 offset;
	
	public bool useOffsetValues;
	
	public float rotateSpeed;
	
	public Transform pivot;
	
	public float maxViewAngle;
	public float minViewAngle;
	public bool invertY;

	bool gamestarted = false;
	
    // Start is called before the first frame update
    void Start()
    {
		if(!useOffsetValues){
			offset = target.position - transform.position;
		}
		
		pivot.transform.position = target.position;
		//pivot.transform.parent = target.transform;
		pivot.transform.parent = null;
        
		
		Cursor.lockState = CursorLockMode.Locked;
    }

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Return))
		{
			gamestarted = true;
		}
    }

    // Update is called once per frame
    void LateUpdate()
    {
		if (gamestarted)
		{

			pivot.transform.position = target.transform.position;

			//get the X position of the mouse & rotate the target
			//float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
			//pivot.Rotate(0, horizontal, 0);

			//Get the Y position of the mouse and rotate the pivot
			float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
			//pivot.Rotate(-vertical, 0, 0);
			if (invertY)
			{
				pivot.Rotate(vertical, 0, 0);
			}
			else
			{
				pivot.Rotate(-vertical, 0, 0);
			}

			//Limit the up/down of the camera rotation
			if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 91f)
			{
				pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
			}

			/*if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle){
				pivot.rotation = Quaternion.Euler(360f + minViewAngle,0,0);
			}*/
			if (pivot.rotation.eulerAngles.x < minViewAngle)
			{
				pivot.rotation = Quaternion.Euler(minViewAngle, 0, 0);
			}

			//Move the camera based on the current rotation of the target & the original offset
			float desiredYAngle = pivot.eulerAngles.y;
			float desiredXAngle = pivot.eulerAngles.x;

			Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
			transform.position = target.position - (rotation * offset);

			//transform.position = target.position - offset;

			if (transform.position.y < target.position.y)
			{
				transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
			}

			transform.LookAt(target);

			//Camera Scroll
			float scrollvalue = Input.GetAxis("Mouse ScrollWheel") * 10;

			if (offset.z > 19 && offset.z < 61)
			{

				offset.z -= scrollvalue;

				if (offset.z <= 19)
				{
					offset.z = 20;
				}
				else if (offset.z >= 61)
				{
					offset.z = 60;
				}
			}
		}

    }

}
