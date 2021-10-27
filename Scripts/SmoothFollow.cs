using UnityEngine;
using System.Collections;
using Prime31;
using EZCameraShake;

public class SmoothFollow : MonoBehaviour
{
	
	public float smoothDampTime = 0.2f;
	[HideInInspector]
	public new Transform transform;
	public Vector3 cameraOffset;
	public Vector3 RealOffset;
	public bool useFixedUpdate = false;
    private GenericInput playerInput;
	private Vector3 _smoothDampVelocity;
    public Transform centro;

	public Transform target;
	public Transform Target2;

	public float Aceleration;
	public float mouseY,mouseX;
    
    void Awake()
	{
		transform = gameObject.transform;
        
        Application.targetFrameRate = -1;

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
    }
	
	
	void LateUpdate()
	{

		CamControl();
		if( !useFixedUpdate)
			updateCameraPosition();
      
		
	}
	public void CamControl(){
		mouseX -= Input.GetAxis("Mouse X") * Aceleration;
		mouseY -= Input.GetAxis("Mouse Y") * Aceleration;
		//mouseX = Mathf.Clamp(mouseY,35,-60);
		transform.LookAt(target);
		target.rotation = Quaternion.Euler(0,0,mouseX);
	}


	void FixedUpdate()
	{
		if( useFixedUpdate )
			updateCameraPosition();
	}


	void updateCameraPosition()
	{
			RealOffset = cameraOffset + target.GetComponentInParent<GenericInput>().GenericMov._rb.velocity.normalized * (target.GetComponentInParent<GenericInput>().GenericMov._rb.velocity.magnitude);
			Vector3 TrueOffset;
			TrueOffset.x = RealOffset.x;
			TrueOffset.y = RealOffset.y;
			TrueOffset.z = cameraOffset.z;
			//transform.position = Vector3.SmoothDamp( transform.position, target.position + TrueOffset, ref _smoothDampVelocity, smoothDampTime );
			return;
	
	}
	
}
