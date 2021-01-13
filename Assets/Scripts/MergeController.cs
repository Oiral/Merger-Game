using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeController : MonoBehaviour
{
    bool isMouseDragging;
    bool isDraggingCam;

    private GameObject target;

    public LayerMask draggingLayers;

    Vector3 originalPosition;

    Vector3 initialMousePos;

    public float mouseMinToDrag;

    public Transform cameraRig;
    public EventSystem eventSystem;
    Vector3 previousMousePos;

    void Update()
    {
        Vector3 screenPosition = Vector3.zero;
        Vector3 offset = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);
            if (target != null)
            {
                initialMousePos = Input.mousePosition;

                originalPosition = target.transform.position;
                isMouseDragging = true;
                Debug.Log("our target position :" + target.transform.position);
                //Here we Convert world position to screen position.
                screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }
            else
            {
                //Lets drag the camera
                isDraggingCam = true;
                previousMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (target != null)
            {

                if (Vector3.Distance(Input.mousePosition, initialMousePos) > mouseMinToDrag)
                {
                    PlaceTarget();
                }
                else
                {
                    //Lets rotate the target
                    target.transform.Rotate(Vector3.up, 90);
                    target.transform.position = originalPosition;
                }
            }
            target = null;
            isMouseDragging = false;
            isDraggingCam = false;
        }

        if (isMouseDragging)
        {
            float planeY = 0;

            Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float distance; // the distance from the ray origin to the ray intersection of the plane
            if (plane.Raycast(ray, out distance))
            {
                target.transform.position = ray.GetPoint(distance); // distance along the ray
            }
        }
        else if (isDraggingCam)
        {



            Vector3 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - previousMousePos;

            dif.y = 0;

            cameraRig.position -= dif;

            previousMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        }

    }

    void PlaceTarget()
    {
        //Lets set our target to the nearest position
        Vector3 targetPos = new Vector3(
            Mathf.Round(target.transform.position.x),
            Mathf.Round(0),
            Mathf.Round(target.transform.position.z)
            );

        int tempLayer = target.transform.gameObject.layer;

        target.gameObject.layer = 0;

        RaycastHit hit;
        //If it is a valid position
        //Lets do a raycast to see if we can drop here
        if (Physics.Raycast(targetPos + Vector3.up * 2, Vector3.down, out hit, 5, draggingLayers))
        {
            //If we hit something
            //Lets check if we can merge with it

            if (target.GetComponent<Tile>().currentData == hit.transform.gameObject.GetComponent<Tile>().currentData)
            {
                //We can merge
                //Level up the thing we were going to drop onto
                hit.transform.GetComponent<Tile>().LevelUpData();
                Debug.Log("Merging");

                Destroy(target);
                isMouseDragging = false;
                return;
            }

            target.transform.position = originalPosition;
            Debug.Log(hit.transform.name, hit.transform.gameObject);
        }
        else
        {
            target.transform.position = targetPos;
        }
        target.gameObject.layer = tempLayer;
        target.transform.GetComponent<Tile>().SetSaveData();
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, Mathf.Infinity ,draggingLayers))
        {
            targetObject = hit.collider.gameObject;
        }
        return targetObject;
    }
}
