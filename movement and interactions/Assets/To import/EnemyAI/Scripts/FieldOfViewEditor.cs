using UnityEditor;
using UnityEngine;
using Delore.AI;


[CustomEditor(typeof(AIMovment))]
public class FieldOfViewEditor : Editor
{
    // Start is called before the first frame update
    private void OnSceneGUI()
    {
        AIMovment ai = (AIMovment)target;

        Handles.color = Color.green;
        Handles.DrawWireArc(ai.transform.position, Vector3.up, Vector3.forward, 360, ai.radius);



        Vector3 viewAngle01 = DirectionFromAngle(ai.transform.eulerAngles.y, -ai.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(ai.transform.eulerAngles.y, ai.angle / 2);


        Handles.color = Color.red;
        Handles.DrawLine(ai.transform.position,ai.transform.position + viewAngle01 * ai.radius);
        Handles.DrawLine(ai.transform.position,ai.transform.position + viewAngle02 * ai.radius);
        
    }
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));
    }
   
}
