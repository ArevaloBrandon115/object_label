using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Camera cameraUsed;
    [SerializeField] private Camera UICamera;

    private ISelectionRepsonse _selectionResponse;

    private Transform _selection;

    [SerializeField] private LineRenderer lineRenderer;
    //[SerializeField] private LineRenderer lineRendereArrow;
    private Vector3 tempCurrent;
    [SerializeField] private GameObject arrowImage;


    private Transform[] points;
    public GameObject gameObjectLabel;
    private void Awake() {
        _selectionResponse = GetComponent<ISelectionRepsonse>();
        //Vector3 temp = cameraUsed.WorldToScreenPoint(gameObjectLabel.transform.position);
        //lineRenderer.SetPosition(1, gameObjectLabel.transform.position);
        //lr.SetPosition(1, new Vector3(8, 3, 45));
    }

    void Update() {
        //arrowImage.transform.LookAt(cameraUsed.transform.position);
        //deselecting
        if (_selection != null) {
            _selectionResponse.OnDeselect(_selection);
        }
        //selecting an object left click
        Ray ray = cameraUsed.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        _selection = null;
        if (Physics.Raycast(ray, out hit)) {
            Transform selection = hit.transform;
            if (selection.CompareTag(selectableTag)) {
                _selection = selection;
                if (Input.GetMouseButtonDown(0)) {
                    Debug.Log("clicked on: " + hit.transform.gameObject.transform.position + " vs "+ hit.transform.position);
                    lineRenderer.SetPosition(0, hit.transform.gameObject.transform.position);
                    lineRenderer.SetPosition(1, gameObjectLabel.transform.position);
                    /*lineRendereArrow.SetPosition(0, hit.transform.position);
                    lineRendereArrow.SetPosition(1, hit.transform.position + new Vector3(0.2f, 0, 0));*/

                    /*if (gameObjectLabel.transform.position.y > hit.transform.gameObject.transform.position.y) {
                        tempCurrent = new Vector3(gameObjectLabel.transform.position.x, hit.transform.gameObject.transform.position.y, gameObjectLabel.transform.position.z);
                    }
                    else {
                        tempCurrent = gameObjectLabel.transform.position;
                    }

                    Vector3 dir = (hit.transform.gameObject.transform.position - tempCurrent).normalized;
                    arrowImage.transform.rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), dir);
                    arrowImage.transform.position = hit.transform.gameObject.transform.position;*/
                    //reset x to 90
                    //arrowImage.transform.eulerAngles = new Vector3(90, arrowImage.transform.eulerAngles.y, arrowImage.transform.eulerAngles.z);
                    //arrowImage.transform.LookAt(new Vector3(arrowImage.transform.position.x, hit.transform.position.y, hit.transform.position.z));

                }
            }
        }
        //selecting
        if (_selection != null) {
            _selectionResponse.OnSelect(_selection);
        }
    }

    public void SetUpLine(Transform[] points) {
        lineRenderer.positionCount = points.Length;
        this.points = points;
    }
}