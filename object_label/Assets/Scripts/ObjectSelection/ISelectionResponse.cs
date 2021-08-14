using UnityEngine;

namespace SelectionController {
    internal interface ISelectionRepsonse {
        void OnSelect(Transform selection);
        void OnDeselect(Transform selection);
    }
}