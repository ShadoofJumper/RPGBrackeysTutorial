using UnityEngine.EventSystems;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    // mask make enable for click just object that is set up as ground,
    // other objects are not visible for raycast
    public LayerMask maskForWalk;
    public Camera cam;
    public Interactable focus;

    private PlayerMotor playerMotor;

    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        // get info, is mouse over UI object
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // for left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, maskForWalk))
            {
                // move player to point we hit
                playerMotor.MoveToPoint(hit.point);
                // disable focus
                DisableFocus();
            }
        }

        //for right mouse button
        if (Input.GetMouseButtonDown(1))
        {

            var ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // set focus interactele object
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

        }
    }

    public void SetFocus(Interactable newFocus)
    {
        //if our focuse changed
        if (newFocus != focus)
        {
            // if have focus 100%
            if (focus != null)
                focus.OnDefocus();
            focus = newFocus;
            playerMotor.FollowTarget(newFocus);

        }
        newFocus.OnFocus(transform);
    }

    public void DisableFocus()
    {
        if (focus != null)
        {
            focus.OnDefocus();
        }
        focus = null;
        playerMotor.UnfollowTarget();

    }




}
