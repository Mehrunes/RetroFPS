using UnityEngine;
using System.Collections;

public class camMouseLook : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 1.0f;
    private float xRotate, yRotate;

    GameObject character;

	// Use this for initialization
	void Start ()
    {
        character = this.transform.parent.gameObject;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        var mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseDirection = Vector2.Scale(mouseDirection, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDirection.y, 1f / smoothing);
        smoothV.x = Mathf.Lerp(smoothV.x, mouseDirection.x, 1f / smoothing);
        mouseLook += smoothV;

        if (mouseLook.y > -60 && mouseLook.y < 80)
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        if (mouseLook.y <= -60)
            mouseLook.y = -60;
        if (mouseLook.y >= 80)
            mouseLook.y = 80;

        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
