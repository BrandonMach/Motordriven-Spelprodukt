using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FloatingEnemyHB : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    private void Start()
    {
        _camera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = _target.position + _offset;
    }
}
