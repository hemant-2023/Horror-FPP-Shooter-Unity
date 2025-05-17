using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;

[CustomEditor(typeof(Interactable),true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable._promptMassage = EditorGUILayout.TextField("Prompt Message", interactable._promptMassage);
            EditorGUILayout.HelpBox("Don't forget to assing the GameObject to Interactable Layer, ",MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable._useEvent = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            //using event , adding it
            if (interactable._useEvent)
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                    interactable.gameObject.AddComponent<InteractionEvent>();
            }
            //not using event, removing it 
            else
            {
                if (interactable.GetComponent<InteractionEvent>() != null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
    }
}
