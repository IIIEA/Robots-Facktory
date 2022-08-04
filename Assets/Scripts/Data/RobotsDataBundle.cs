using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Robots Data Bundle", menuName = "Data/Robots Data Bundle", order = 51)]
public class RobotsDataBundle : ScriptableObject
{
    [SerializeField] private List<RobotData> _robotsData;

    public bool TryGetRobotByLvl(int lvl, out RobotData robotData)
    {
        robotData = _robotsData.Find(robotData => robotData.Lvl == lvl);

        return robotData != null;
    }
}
