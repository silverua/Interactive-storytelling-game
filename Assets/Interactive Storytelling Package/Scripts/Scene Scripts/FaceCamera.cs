//	CameraFacing.cs 
//	original by Neil Carter (NCarter)
//	modified by Hayden Scott-Baron (Dock) - http://starfruitgames.com
//  allows specified orientation axis
using UnityEngine;
 
public class FaceCamera : MonoBehaviour
{
    private Camera referenceCamera;
 
    public enum Axis {up, down, left, right, forward, back};
    public bool reverseFace = false; 
    public Axis axis = Axis.up; 
 
    // return a direction based upon chosen axis
    public static Vector3 GetAxis (Axis refAxis)
    {
        switch (refAxis)
        {
            case Axis.down:
                return Vector3.down; 
            case Axis.forward:
                return Vector3.forward; 
            case Axis.back:
                return Vector3.back; 
            case Axis.left:
                return Vector3.left; 
            case Axis.right:
                return Vector3.right; 
        }
 
        // default is Vector3.up
        return Vector3.up; 		
    }

    private void  Awake ()
    {
        // if no camera referenced, grab the main camera
        if (!referenceCamera)
            referenceCamera = Camera.main; 
    }
    
    //Orient the camera after all movement is completed this frame to avoid jittering
    private void LateUpdate ()
    {
        // rotates the object relative to the camera
        var targetPos = transform.position +
                        referenceCamera.transform.rotation * (reverseFace ? Vector3.forward : Vector3.back);
        var targetOrientation = referenceCamera.transform.rotation * GetAxis(axis);
        transform.LookAt (targetPos, targetOrientation);
    }
}