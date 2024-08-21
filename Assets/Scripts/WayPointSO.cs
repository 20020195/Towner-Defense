using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WayPoints/WayPointSO")]
public class WayPointSO : ScriptableObject
{
    public List<Vector3> points;
}
