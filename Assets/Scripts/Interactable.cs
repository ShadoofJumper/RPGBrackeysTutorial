using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float rangeAcces;

    // for interact with some point of object
    // fro example for chest we interact with front of chest, and it transfrom on front
    public Transform interacteTransform;
    private Transform playerTransform;
    // is object in progress interaction
    private bool isInteracte = false;
    // is object focused
    private bool isFocused = false;

    // rewritable methos for Interaction
    public virtual void Interact()
    {
    }


    // methos for understande is player near intretacteble range
    private void Update()
    {
        // if we focused and not already in interaction progress
        if (isFocused && !isInteracte) { 
            // calculate distance form player to object
            float distance = Vector3.Distance(playerTransform.position, interacteTransform.position);
            // if player near object
            if (distance < rangeAcces)
            {
                isInteracte = true;
                Interact();
            }
        }
    }

    public void OnFocus(Transform player)
    {
        isFocused = true;
        // on focus interacteble object, we save player transform info
        playerTransform = player;
        // on new focus clear old flag
        isInteracte = false;
    }

    public void OnDefocus()
    {
        isFocused = false;
        // clear player transform info
        playerTransform = null;
        // on defocus clear old flag
        isInteracte = false;
    }


    // create gizmo for interacteble object
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        if (interacteTransform == null)
        {
            interacteTransform = transform;
        }
        Gizmos.DrawWireSphere(interacteTransform.position, rangeAcces);
    }
}
