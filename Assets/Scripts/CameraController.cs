using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    //угол поднятия камеры по вертикали
    public float pitch = 2f;

    public float yawSpeed = 100f;

    private float currentZoom = 10f;
    private float currentYaw = 0f;


    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        // move camera with target
        transform.position = target.position - offset * currentZoom;
        // look at targeet
        // добавляем высоты к объекту за которым следим, тем самим смотри не на объект а чуть више чем он находится
        transform.LookAt(target.position + Vector3.up * pitch);

        // чтобы крутить камеру вокруг заданого объекта
        // параметры: таргет вокруг которого крутим , ось по которой крутим, и угол прокрутки
        Debug.Log($"angel: {currentYaw}");
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

}
