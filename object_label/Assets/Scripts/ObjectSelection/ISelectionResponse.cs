using UnityEngine;
internal interface ISelectionRepsonse {
    void OnSelect(Transform selection);
    void OnDeselect(Transform selection);
}
