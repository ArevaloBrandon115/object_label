using UnityEngine;

internal class HighlighingObject : MonoBehaviour, ISelectionRepsonse {
    [SerializeField] public Material highlightMaterial;
    public Material previousMaterial;

    public void OnDeselect(Transform selection) {
        Renderer selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null) {
            selectionRenderer.material = this.previousMaterial;
        }
    }

    public void OnSelect(Transform selection) {
        Renderer selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null) {
            this.previousMaterial = selectionRenderer.material;
            selectionRenderer.material = this.highlightMaterial;
        }
    }
}
