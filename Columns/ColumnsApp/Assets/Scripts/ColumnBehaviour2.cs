using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

//using UnityEditor;
namespace Assets
{
    public class ColumnBehaviour2 : MonoBehaviour
    {


        //bool turn = false;
        bool startTurn = false;
        Vector3 prevMousePos = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 mousePos = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 rotationPoint = new Vector3(0.0f, 0.0f, 0.0f);
        List<GameObject> allColumns = new List<GameObject>();
        List<GameObject> columns = new List<GameObject>();
        List<GameObject> turnedColumns = new List<GameObject>();
        string startColumn = "Cylinder2";
        GameObject column;
        GameObject startColObj;
        public GameObject nextColumn;
        public Vector3 rotAxis;
        ColumnClass columnInfo = null;
        int xxx = 90;
        int yyy = 90;
        int direction = 0;
        GameObject selectedColumn = null;
        
        bool game = true;
        bool turned = false;
        bool collision = false;
        Vector3 pos = Vector3.zero;
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        bool chooseNew = false;
        Dictionary<GameObject, bool> moveDict = new Dictionary<GameObject, bool>();
        Dictionary<GameObject, Vector3> positionDict = new Dictionary<GameObject, Vector3>();
        Dictionary<GameObject, Quaternion> rotationDict = new Dictionary<GameObject, Quaternion>();
        Dictionary<GameObject, Vector3> rotationPointDict = new Dictionary<GameObject, Vector3>();
        Dictionary<GameObject, Vector3> rotationAxisDict = new Dictionary<GameObject, Vector3>();
        bool stopTurning = false;
        string endColumn = "";
        int green = 0;
        int red = 0;
        int winPoints = 0;
        int loosePoints = 0;
        bool countPoints = true;
        Material m_Material;
        float score = 0;
        bool wait = true;


        bool begin = true;

        Vector3 startPos = new Vector3(0, 0, 0);
        Vector3 endPos = new Vector3(0, 0, 0);

       


        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {



            if (begin)
                prepareColumns();


            if (!transform.parent.GetComponent<ControllerScript>().countDB)
            {
                if (!startTurn)
                {

                    startTurn = true;

                    columnInfo = new ColumnClass(startColObj, null, new Vector3(0, 0, 0), 0);
                    turnedColumns.Add(columnInfo.currentColumn);
                    chooseNew = true;

                }



                if (Input.GetMouseButtonDown(0) && startTurn)
                {
                    prevMousePos = mousePos;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;



                    if (Physics.Raycast(ray, out hit))
                    {
                        if (!turnedColumns.Contains(hit.transform.gameObject) && allColumns.Contains(hit.transform.gameObject))
                        {
                            selectedColumn = hit.transform.gameObject;
                            moveDict[selectedColumn] = true;
                            //Debug.Log("2 " + selectedColumn.name);
                        }
                    }
                }

                if (selectedColumn != null && yyy > 0)
                {

                    Vector3 rotPoint = new Vector3(0, 0, 0);
                    rotPoint.x = selectedColumn.transform.position.x;
                    rotPoint.y = selectedColumn.transform.position.y - selectedColumn.GetComponent<Collider>().bounds.size.y / 2;
                    rotPoint.z = selectedColumn.transform.position.z;

                    int index = 0;

                    for (int i = 0; i < allColumns.Count; i++)
                    {

                        if (allColumns[i] == selectedColumn)
                            index = i;

                    }

                    Vector3 rotAxisTemp = new Vector3(allColumns[index + 1].transform.position.z - allColumns[index - 1].transform.position.z, 0, allColumns[index - 1].transform.position.x - allColumns[index - 1].transform.position.x);
                    selectedColumn.transform.RotateAround(rotPoint, rotAxisTemp, 1.0f);

                    yyy -= 1;
                    pos = selectedColumn.transform.position;
                    rot = selectedColumn.transform.rotation;
                    positionDict[selectedColumn] = selectedColumn.transform.position;
                    rotationDict[selectedColumn] = selectedColumn.transform.rotation;
                }

                if (yyy == 0)
                {
                    moveDict[selectedColumn] = false;
                    if (!turnedColumns.Contains(selectedColumn))
                        turnedColumns.Add(selectedColumn);
                    selectedColumn.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    selectedColumn.transform.position = positionDict[selectedColumn];
                    selectedColumn.transform.rotation = rotationDict[selectedColumn];

                    if (!wait)
                        stopTurning = true;
                }


                if (xxx == 0)
                {
                    foreach (GameObject elem in moveDict.Keys)
                    {
                        if (elem.name == endColumn)
                        {
                            game = false;

                            moveDict[elem] = false;

                            stopTurning = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (GameObject elem in moveDict.Keys)
                    {
                        if (elem.name == endColumn)
                        {
                            if (moveDict[elem] == true)
                                xxx -= 1;

                        }
                    }
                }

                if (game)
                {
                    if (startTurn && columnInfo.currentColumn != null && chooseNew)// && !turn)
                    {

                        columnInfo = turnColumn(columnInfo);

                        moveDict[columnInfo.currentColumn] = true;
                    }

                }



                if (!stopTurning)
                {
                    foreach (GameObject elem in moveDict.Keys)
                    {
                        if (moveDict[elem])
                        {
                            if (!turnedColumns.Contains(elem))
                                turnedColumns.Add(elem);



                            if (selectedColumn != null)
                            {
                                if (elem.name == "Cylinder (2)" && selectedColumn.name == "CylinderX")
                                    direction = -direction;

                            }
                            if (selectedColumn != null)
                            {
                                if (elem.name == "CylinderX" && selectedColumn.name == "CylinderX (1)")
                                    direction = -direction;

                            }
                            if (selectedColumn != null)
                            {
                                if (elem.name == "Cylinder3" && selectedColumn.name == "Cylinder")
                                    direction = -direction;

                            }

                            float degree = 1.0f * direction;

                            elem.transform.RotateAround(rotationPointDict[elem], rotationAxisDict[elem], degree);

                            if (selectedColumn != null)
                            {
                                if (elem.name == "Cylinder (2)" && selectedColumn.name == "CylinderX")
                                    direction = -direction;

                            }
                            if (selectedColumn != null)
                            {
                                if (elem.name == "CylinderX" && selectedColumn.name == "CylinderX (1)")
                                    direction = -direction;

                            }
                            if (selectedColumn != null)
                            {
                                if (elem.name == "Cylinder3" && selectedColumn.name == "Cylinder")
                                    direction = -direction;

                            }


                            positionDict[elem] = elem.transform.position;
                            rotationDict[elem] = elem.transform.rotation;

                        }
                        else
                        {
                            elem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            elem.transform.position = positionDict[elem];
                            elem.transform.rotation = rotationDict[elem];
                        }
                    }
                }
                else
                {
                    foreach (GameObject elem in moveDict.Keys)
                    {
                        elem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        elem.transform.position = positionDict[elem];
                        elem.transform.rotation = rotationDict[elem];
                    }

                    if (countPoints)
                    {
                        for (int i = 0; i < turnedColumns.Count; i++)
                        {
                            Material mat = turnedColumns[i].GetComponent<Renderer>().material;

                            if (mat.color == Color.red)
                                winPoints += 1;
                            if (mat.color == Color.green)
                                loosePoints += 1;

                        }

                        countPoints = false;
                        score = winPoints - 0.5f * loosePoints;

                        transform.parent.GetComponent<ControllerScript>().FirstRow(score);
                        //UI.SetActive(true);

                    }
                }


                if (chooseNew)
                {

                    if (game)
                    {
                        columnInfo = new ColumnClass(columnInfo.nextColumn, null, rotationAxisDict[columnInfo.currentColumn], 0);
                        chooseNew = false;
                    }

                }


            

        }
            else
            {
                foreach (GameObject elem in allColumns)
                {
                    elem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    elem.transform.position = positionDict[elem];
                    elem.transform.rotation = rotationDict[elem];
                }

            }
        }
            


        public void CollisionDetected(GameObject obj, GameObject obj2)
        {

            if (!turnedColumns.Contains(obj2))
            {
                chooseNew = true;
                moveDict[obj] = false;
                columnInfo = new ColumnClass(obj2, null, rotationAxisDict[obj], 0);
            }
            else
            {
                if (obj2 == selectedColumn)
                {
                    Debug.Log(obj2.name);
                        wait = false;
                }

                moveDict[obj] = false;
            }

        }

        ColumnClass turnColumn(ColumnClass colInfo)
        {


            GameObject column = colInfo.currentColumn;
            GameObject nextColumn = colInfo.nextColumn;
            Vector3 prevRotAxis = colInfo.rotAxis;

            if (column != selectedColumn)
            {
                float minD = 10000.0f;
                float d = 0.0f;

                float x = column.transform.position.x;
                float y = column.transform.position.y;
                float z = column.transform.position.z;

                float xAxis = 0.0f;
                float zAxis = 0.0f;

                rotationPoint.x = x;
                rotationPoint.y = y - column.GetComponent<Collider>().bounds.size.y / 2;
                rotationPoint.z = z;

                if (columns.Count > 0)
                {
                    foreach (GameObject elem in columns)
                    {
                        d = Mathf.Sqrt(Mathf.Pow(elem.transform.position.x - x, 2) + Mathf.Pow(elem.transform.position.z - z, 2));

                        if (d < minD)
                        {
                            minD = d;
                            nextColumn = elem;
                            xAxis = elem.transform.position.x - x;
                            zAxis = elem.transform.position.z - z;
                        }
                    }

                    if (Mathf.Abs(zAxis) > Mathf.Abs(xAxis))
                    {
                        xAxis = xAxis / zAxis;
                        zAxis = zAxis / zAxis;
                    }
                    else
                    {
                        zAxis = zAxis / xAxis;
                        xAxis = xAxis / xAxis;

                    }

                    columns.Remove(nextColumn);



                    Vector3 rotAxis = new Vector3(0, 0, 0);


                    rotAxis = new Vector3(-zAxis, 0, xAxis);

                    rotationAxisDict[column] = rotAxis;
                    return new ColumnClass(column, nextColumn, rotAxis, direction);
                }
                else
                {
                    moveDict[column] = true;
                    rotationAxisDict[column] = prevRotAxis;
                    xxx = 90;
                    return new ColumnClass(column, null, prevRotAxis, direction);
                }

            }

            else
            {
                game = false;
                return new ColumnClass(column, null, prevRotAxis, direction);
            }
        }



        public void onBackButton()
        {
            SceneManager.LoadScene("MenuScene");
        }

        public void onRerunButton()
        {
            //UI.SetActive(false);
            begin = true;
            startTurn = false;
            float addZ = -2.04f + 5.3f;
            float addY = -100f;
            foreach (GameObject column in allColumns)
            {
                

                if (column.name == "Cylinder2")
                {
                    column.transform.position = new Vector3(-5.26f, -99.0f, -4.02f);

                }
                if (column.name == "Cylinder1")
                    column.transform.position = new Vector3(-3.24f, -99.0f, -2.82f);
                if (column.name == "Cylinder")
                    column.transform.position = new Vector3(-0.95f, -99.0f, -1.94f);
                if (column.name == "Cylinder3")
                    column.transform.position = new Vector3(1.08f, -99.0f, -2.7f);
                if (column.name == "Cylinder z")
                    column.transform.position = new Vector3(2.76f, -99.0f, -1.34f);
                if (column.name == "Cylinder (2)")
                    column.transform.position = new Vector3(5.1f, -99.0f, -0.9f);
                if (column.name == "CylinderX")
                    column.transform.position = new Vector3(7.01f, -99.0f, -2.3f);
                if (column.name == "CylinderX (1)")
                    column.transform.position = new Vector3(8.76f, -99.0f, -3.67f);
                if (column.name == "CylinderX (2)")
                    column.transform.position = new Vector3(10.76f, -99.0f, -2.76f);

                if (column.name == "CylinderX (3)")
                {
                    column.transform.position = new Vector3(12.63f, -99.0f, -3.61f);
                }
                column.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            foreach (GameObject column in allColumns)
            {
                Vector3 posAdd = column.transform.position;
                posAdd.y += addY;
                posAdd.z += addZ;

                column.transform.position = posAdd;

            }

            allColumns.Clear();
            turnedColumns.Clear();
            columns.Clear();
            game = true;

            startTurn = false;
            //Vector3 prevMousePos = new Vector3(0.0f, 0.0f, 0.0f);
            //Vector3 mousePos = new Vector3(0.0f, 0.0f, 0.0f);
            //Vector3 rotationPoint = new Vector3(0.0f, 0.0f, 0.0f);
            //List<GameObject> allColumns = new List<GameObject>();
            //List<GameObject> columns = new List<GameObject>();
            //List<GameObject> turnedColumns = new List<GameObject>();
            //string startColumn = "Cylinder2";
            //GameObject column;
            //GameObject startColObj;
            //public GameObject nextColumn;
            //public Vector3 rotAxis;
            //ColumnClass columnInfo = null;
            xxx = 90;
            yyy = 90;
            direction = 0;
            //GameObject selectedColumn = null;
            //public GameObject col1;
            //public GameObject col2;
            //bool game = true;
            turned = false;
            collision = false;
            //Vector3 pos = Vector3.zero;
            //Quaternion rot = new Quaternion(0, 0, 0, 0);
            chooseNew = false;
            moveDict = new Dictionary<GameObject, bool>();
            positionDict = new Dictionary<GameObject, Vector3>();
            rotationDict = new Dictionary<GameObject, Quaternion>();
            rotationPointDict = new Dictionary<GameObject, Vector3>();
            rotationAxisDict = new Dictionary<GameObject, Vector3>();
            stopTurning = false;
            endColumn = "";
            green = 0;
            red = 0;
            winPoints = 0;
            loosePoints = 0;
            countPoints = true;
            selectedColumn = null;
            score = 0;
	        wait = true;





        }

        void prepareColumns()
        {
            foreach (Transform child in this.transform)
                allColumns.Add(child.gameObject);


            int rand;
            rand = transform.parent.GetComponent<ControllerScript>().getRandom();

            if (rand == 1)
            {
                startColumn = allColumns[0].name;
                endColumn = allColumns[allColumns.Count - 1].name;
                direction = -1;
            }
            else
            {
                startColumn = allColumns[allColumns.Count - 1].name;
                endColumn = allColumns[0].name;
                direction = 1;
            }

            int colors = allColumns.Count / 2;

            for (int i = 0; i < allColumns.Count; i++)
            {
                GameObject col = allColumns[i];

                if (col.name == startColumn)
                {
                    m_Material = col.GetComponent<Renderer>().material;
                    m_Material.color = Color.yellow;
                }
                else
                {
                    rand = transform.parent.GetComponent<ControllerScript>().getRandom();
                    m_Material = col.GetComponent<Renderer>().material;
                    if (rand == 1)
                    {
                        if (green < colors)
                        {
                            m_Material.color = Color.green;
                            green += 1;
                        }
                        else
                        {
                            m_Material.color = Color.red;
                            red += 1;
                        }
                    }

                    else
                    {
                        if (red < colors)
                        {
                            m_Material.color = Color.red;
                            red += 1;
                        }
                        else
                        {
                            m_Material.color = Color.green;
                            green += 1;
                        }
                    }
                }
            }


            foreach (Transform child in this.transform)
            {
                if (child.gameObject.name != startColumn)
                    columns.Add(child.gameObject);
                else
                    startColObj = child.gameObject;

                moveDict[child.gameObject] = false;
                positionDict[child.gameObject] = child.gameObject.transform.position;
                rotationDict[child.gameObject] = child.gameObject.transform.rotation;
                rotationPointDict[child.gameObject] = new Vector3(child.gameObject.transform.position.x, child.gameObject.transform.position.y - child.gameObject.GetComponent<Collider>().bounds.size.y / 2, child.gameObject.transform.position.z);
                rotationAxisDict[child.gameObject] = Vector3.zero;
            }

            begin = false;
        }
    }
    }





