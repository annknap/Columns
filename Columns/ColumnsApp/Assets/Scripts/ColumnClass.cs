using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UnityEditor;
using UnityEngine;

namespace Assets
{
    public class ColumnClass
    {

        
        public GameObject nextColumn;
        public GameObject currentColumn;
        public Vector3 rotAxis;
        public int direction;

        public ColumnClass(GameObject cCol, GameObject nCol, Vector3 axis, int dir)
        {

            currentColumn = cCol;
            nextColumn = nCol;
            rotAxis = axis;
            direction = dir;
        }
}
    
}
