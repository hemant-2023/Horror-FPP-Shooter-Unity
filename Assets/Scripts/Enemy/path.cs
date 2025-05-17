using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class path : MonoBehaviour
{

    public List<Transform> _waypoint;
    [SerializeField]
    private bool _alwaysDrawPath;
    [SerializeField]
    private bool _drawAsLoop;
    [SerializeField]
    private bool _drawNumbers;
    public Color _debugColour = Color.white;

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (_alwaysDrawPath)
        {
            DrawPath();
        }
    }
    public void DrawPath()
    {
        for (int i = 0; i < _waypoint.Count; i++)
        {
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 30;
            labelStyle.normal.textColor = _debugColour;
            if (_drawNumbers)
                Handles.Label(_waypoint[i].position, i.ToString(), labelStyle);
            //Draw Lines Between Points.
            if (i >= 1)
            {
                Gizmos.color = _debugColour;
                Gizmos.DrawLine(_waypoint[i - 1].position, _waypoint[i].position);

                if (_drawAsLoop)
                    Gizmos.DrawLine(_waypoint[_waypoint.Count - 1].position, _waypoint[0].position);

            }
        }
    }
    public void OnDrawGizmosSelected()
    {
        if (_alwaysDrawPath)
            return;
        else
            DrawPath();
    }
#endif
}