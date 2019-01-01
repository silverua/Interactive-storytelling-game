using UnityEngine;

public class CamMouseLook: MonoBehaviour
{
    public float Sensitivity = 5f;
    public float Smoothing = 2f;
    public float ClampUpDownLook = 75f;

    private Vector2 mouseLook;
    private Vector2 smoothV;
    private GameObject character;

    private void Start()
    {
        character = transform.parent.gameObject;
    }

    private void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(Sensitivity * Smoothing, Sensitivity * Smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / Smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / Smoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -ClampUpDownLook, ClampUpDownLook);
        
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}