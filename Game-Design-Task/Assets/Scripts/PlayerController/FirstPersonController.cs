using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float GroundDistance = 0.4f;
    public LayerMask groundMask;
    private Camera _camera;
    private CharacterController _controller;
    private Vector3 _velocity;
    private Transform _groundCheckPosition;

    private bool _isGrounded;

    public float Speed = 12;
    private static float _gravity = -9.81f;

    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _controller = GetComponent<CharacterController>();
        _groundCheckPosition = GetComponentInChildren<FirstPersonGroundCheck>().transform;
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, GroundDistance, groundMask);

        if(_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * Speed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}
