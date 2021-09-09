using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(HunterAI))]
public class DrawWireArc : Editor
{
    void OnSceneGUI()
    {
        HunterAI myObj = (HunterAI)target;
        
        Vector3 viewAngle01 = DirectionFromAngle(myObj.transform.eulerAngles.y, -myObj.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(myObj.transform.eulerAngles.y, myObj.angle / 2);

        Handles.color = Color.cyan;
        Handles.DrawLine(myObj.transform.position, myObj.transform.position + viewAngle01 * myObj.sightRange);
        Handles.DrawLine(myObj.transform.position, myObj.transform.position + viewAngle02 * myObj.sightRange);



        if(myObj.playerInSight)
		{
            Handles.color = Color.blue;
            Handles.DrawLine(myObj.transform.position, myObj.namelessPlayer.position);
		}
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
