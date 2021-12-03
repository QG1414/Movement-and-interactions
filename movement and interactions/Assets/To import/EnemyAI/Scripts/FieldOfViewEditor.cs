using UnityEditor;
using UnityEngine;
using Delore.AI;


[CustomEditor(typeof(AIDetection))] // use this file when this script is used
public class FieldOfViewEditor : Editor
{

    private void OnSceneGUI() // GUI we see in scene window
    {
        AIDetection ai = (AIDetection)target; // We Get our script

        Handles.color = Color.green; // change color of lines to green
        Handles.DrawWireArc(ai.transform.position, Vector3.up, Vector3.forward, 360, ai.radius); // draw circle line around player with given radius



        Vector3 viewAngle01 = DirectionFromAngle(ai.transform.eulerAngles.y, -ai.angle / 2); // calculate first angle
        Vector3 viewAngle02 = DirectionFromAngle(ai.transform.eulerAngles.y, ai.angle / 2); // calculate second angle


        Handles.color = Color.red; // chenge color of lines to red
        Handles.DrawLine(ai.transform.position,ai.transform.position + viewAngle01 * ai.radius); // draw first line representing first angle
        Handles.DrawLine(ai.transform.position,ai.transform.position + viewAngle02 * ai.radius); // draw second line representing second angle
        
    }
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));
    }
   
}

