using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Map
{
    [DataMember]
    public int LevelSizeX { get; set; }

    [DataMember]
    public int LevelSizeY { get; set; }


    [DataMember]
    public int LevelSizeZ { get; set; }

    [DataMember]
    public int Coords { get; set; }

    public struct obstacles
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
    }

    [DataMember]
    public List<obstacles> lstObstacles { get; set; }

}
