using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SelectionController {
    public class SelectionManager : MonoBehaviour {
        [SerializeField] private string selectableTag = "Selectable";
        [SerializeField] private Camera cameraUsed;
        [SerializeField] private Camera UICamera;

        private ISelectionRepsonse _selectionResponse;
        private Transform _selection;

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private GameObject partLabelText;
        [SerializeField] private GameObject cameraContainer;
        [SerializeField] private GameObject buttonContainer;
        [SerializeField] private GameObject carGameObject;
        [SerializeField] private GameObject ButtonPrefab;


        private void Awake() {
            _selectionResponse = GetComponent<ISelectionRepsonse>();
            SetUpButtons();
        }

        void Update() {
            //deselecting
            if (_selection != null) {
                _selectionResponse.OnDeselect(_selection);
            }
            if (partLabelText.activeSelf) {
                partLabelText.transform.rotation = Quaternion.LookRotation(partLabelText.transform.position - cameraUsed.transform.position);
            }
            //selecting an object left click
            Ray ray = cameraUsed.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            _selection = null;
            if (Physics.Raycast(ray, out hit, 1000f)) {
                Transform selection = hit.transform;
                if (selection.CompareTag(selectableTag)) {
                    _selection = selection;
                    if (Input.GetMouseButtonDown(0)) {
                        lineRenderer.SetPosition(0, hit.point);
                        lineRenderer.SetPosition(1, partLabelText.transform.position);

                        cameraContainer.transform.position = hit.transform.position;
                        cameraUsed.orthographicSize = 5;

                        partLabelText.GetComponent<TextMesh>().text = hit.transform.name;
                        partLabelText.SetActive(true);
                    }
                }
                else {
                    if (Input.GetMouseButtonDown(0)) {
                        partLabelText.SetActive(false);
                        lineRenderer.SetPosition(0, Vector3.zero);
                        lineRenderer.SetPosition(1, Vector3.zero);
                    }
                }
            }
            //selecting
            if (_selection != null) {
                _selectionResponse.OnSelect(_selection);
            }
        }

        public void GetCarInformation(GameObject gameObject) {

            lineRenderer.SetPosition(0, gameObject.GetComponent<MeshRenderer>().bounds.center);
            lineRenderer.SetPosition(1, partLabelText.transform.position);

            cameraContainer.transform.position = gameObject.transform.position;
            cameraUsed.orthographicSize = 5;

            partLabelText.GetComponent<TextMesh>().text = gameObject.transform.name;
            partLabelText.SetActive(true);
        }

        private void SetUpButtons() {
            //get children objects
            Transform[] allChildren = carGameObject.GetComponentsInChildren<Transform>();
            int index = 0;
            foreach (Transform child in allChildren) {
                if (index > 0) {
                    GameObject newButton = (GameObject)Instantiate(ButtonPrefab);
                    //make a button for each make them clickable
                    newButton.GetComponent<Button>().onClick.AddListener(() => {
                        GetCarInformation(child.transform.gameObject);
                    });

                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerEnter;
                    entry.callback.AddListener((eventData) => { _selectionResponse.OnSelect(child.transform); });
                    newButton.GetComponent<EventTrigger>().triggers.Add(entry);

                    entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerExit;
                    entry.callback.AddListener((eventData) => { _selectionResponse.OnDeselect(child.transform); });
                    newButton.GetComponent<EventTrigger>().triggers.Add(entry);

                    newButton.GetComponentInChildren<Text>().text = child.transform.gameObject.name.ToString();
                    newButton.transform.SetParent(buttonContainer.transform);
                }
                index++;
            }

        }

    }
}