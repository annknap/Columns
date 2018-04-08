using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class CollisionDetection : MonoBehaviour
    {

        Dictionary<string, bool> move = new Dictionary<string, bool>();
        Dictionary<string, Vector3> pos = new Dictionary<string, Vector3>();

        int direction;
        Vector3 rotationPoint;
        Vector3 rotAxis;
        //bool x = false;
      
        // Use this for initialization
        void Start()
        {
            //move.Add(this.name, false);
            
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("xx");

            /*transform.position = pos[this.name];
            if (move[this.name])
            {
                Debug.Log("lalalllllllllllll");
                
                float degree = 0.3f * direction;
                transform.RotateAround(rotationPoint, rotAxis, degree);
                pos[this.name] = transform.position;
            }*/
        }

        void OnCollisionEnter(Collision collision)
        {
            if (this.gameObject.transform.parent.name == "Columns")
                transform.parent.GetComponent<ColumnBehaviour2>().CollisionDetected(this.gameObject, collision.gameObject);
            else if(this.gameObject.transform.parent.name == "Columns1")
                transform.parent.GetComponent<ColumnBehaviour>().CollisionDetected(this.gameObject, collision.gameObject);
            else
                transform.parent.GetComponent<ColumnBehaviour3>().CollisionDetected(this.gameObject, collision.gameObject);
            //Debug.Log("Collisionnn!!");
            //move[this.name] = false;

            //x = 0.1f;
        }

        public void Move(int dir, Vector3 rotPoint, Vector3 rAxis)
        {
            Debug.Log("lalalla");
            direction = dir;
            rotationPoint = rotPoint;
            rotAxis = rAxis;
            move[this.name] = true;
            pos[this.name] = transform.position;
        }
    }
}
