using TMPro;
using UnityEngine;

namespace GDP01.Util {
    public class Text {
        public static TextMeshPro CreateWorldText(
            string text, 
            Transform parent = null,
            Vector2 dimensions = default(Vector2),
            Vector3 localPosition = default(Vector3), 
            Vector3 localRotationEuler = default(Vector3), 
            int fontSize = 40, 
            Color color = default(Color), 
            TextAlignmentOptions textAlignmentOptions = TextAlignmentOptions.Center, 
            int sortingOrder = 0) {
            if(color == null) color = Color.white;
            return CreateWorldText(parent, text, dimensions, localPosition, localRotationEuler, fontSize, (Color) color, 
                textAlignmentOptions, sortingOrder);
        }

        public static TextMeshPro CreateWorldText(
            Transform parent, 
            string text, 
            Vector2 dimensions,
            Vector3 localPosition, 
            Vector3 localRotationEuler, 
            int fontsize,
            Color color, 
            TextAlignmentOptions textAlignmentOptions, 
            int sortingOrder) {

            GameObject gameObject = new GameObject("World_Text", typeof(TextMeshPro));
            Transform transform = gameObject.transform;
            transform.SetParent(parent);
            transform.localPosition = localPosition;
            transform.rotation = Quaternion.Euler(localRotationEuler);

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = dimensions;
            
            TextMeshPro textMeshPro = gameObject.GetComponent<TextMeshPro>();
            // todo anchor
            // textMeshPro.anchor
            textMeshPro.alignment = textAlignmentOptions;
            textMeshPro.sortingOrder = sortingOrder;
            textMeshPro.text = text;
            textMeshPro.fontSize = fontsize;
            textMeshPro.color = color;
            
            // todo make parameter
            textMeshPro.enableAutoSizing = true;
            textMeshPro.fontSizeMin = 0.1f;
            textMeshPro.fontSizeMax = fontsize;
            return textMeshPro;
        }
        
    }
}