using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    // mask make enable for click just object that is set up as ground,
    // other objects are not visible for raycast
    public LayerMask maskForWalk;

    public Camera cam;

    private PlayerMotor playerMotor;

    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, maskForWalk))
            {
                Debug.Log("You hit: "+ hit.collider.name+" pos: "+hit.point);

                // move player to point we hit
                playerMotor.MoveToPoint(hit.point);
                // disable focus, for future
            }
        }
    }




}
