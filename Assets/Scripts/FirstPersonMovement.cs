using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    private CharacterController _controller;

    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Motion();
    }

    private void Motion()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;
        movementDirection = transform.TransformDirection(movementDirection);

        float magnitude = Mathf.Clamp01(new Vector2(horizontal, vertical).magnitude);

        float curSpeed = speed * magnitude;
        _controller.SimpleMove(movementDirection * curSpeed);
    }

}
