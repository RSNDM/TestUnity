using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RayInteract
{
    public class InteractRay : MonoBehaviour {
        RaycastHit raycastHit;
        RaycastHit MouseRaycastHit;
        private new Camera camera;
        Ray MouseRay;
        Ray EyeRay;
        int layer;
        float waitTime;
        float curTime;
        //InteractController Eyecon;
        //InteractController Mousecon;
        public GameObject origin;
        public GameObject cube;
        //鼠标坐标
        Vector3 screenPosition;
        Vector3 mousePositionOnScreen;
        Vector3 mousePositionInworld;
        // Use this for initialization
        void Start() {
            camera = GetComponent<Camera>();
            layer = LayerMask.GetMask("Raycast");
        }

        // Update is called once per frame
        void Update() {
            waitTime = Time.deltaTime;
            //MousePos();
            EyeRay = new Ray(origin.transform.position, origin.transform.TransformDirection(Vector3.forward));
            Debug.DrawRay(origin.transform.position, origin.transform.TransformDirection(Vector3.forward) * 100, Color.red);
            if (Physics.Raycast(EyeRay, out raycastHit, Mathf.Infinity, layer))
            {
                //Debug.DrawRay(EyeRay.origin, raycastHit.point, Color.blue);
                Debug.Log(raycastHit.transform.name);
                //if (raycastHit.transform.GetComponent<InteractController>()!=null&& Eyecon == null)
                //{
                //    Eyecon = raycastHit.transform.GetComponent<InteractController>();
                //    Eyecon.State = TouchState.EyeHovering;
                //    //传递交互物体
                //    Eyecon.SetInteractObj(raycastHit.transform);
                //    Eyecon.UseInteract();
                //}
            }
            else
            {
                //if (Eyecon != null)
                //{
                //    Eyecon.StopInteract();
                //    Eyecon = null;

                //}
            }
            Vector3 vec = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));//Input.mousePosition
            //Debug.LogError("camera position:"+camera.transform.position);
            //Debug.LogError("camera localposition:" + camera.transform.localPosition);
            vec = camera.ScreenToWorldPoint(Input.mousePosition);
            //Debug.LogError("vec:" + vec);
            MouseRay = camera.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(camera.transform.position, (vec - camera.transform.position).normalized * 1000, Color.blue);
            Debug.DrawRay(MouseRay.origin, MouseRay.direction * 1000, Color.green);
            if (Physics.Raycast(MouseRay, out MouseRaycastHit, 1000f))
            {
                //Debug.DrawRay(MouseRay.origin, MouseRaycastHit.point, Color.red);
                //    if (MouseRaycastHit.transform.GetComponent<InteractController>() != null)
                //    {
                //        Mousecon = MouseRaycastHit.transform.GetComponent<InteractController>();
                //        Mousecon.State = TouchState.MouseHovering;
                //        Mousecon.UseInteract();
                //        if (Input.GetMouseButtonDown(0))
                //        {
                //            Mousecon.State = TouchState.Touching;
                //            Mousecon.UseInteract();
                //        }
                //    }
                //}
                //else
                //{
                //    if (Mousecon != null)
                //    {
                //        Mousecon.StopInteract();
                //        Mousecon = null;
                //    }
                //}
            }
            //void MousePos()
            //{
            //    screenPosition = camera.WorldToScreenPoint(transform.position);
            //    mousePositionOnScreen = Input.mousePosition;
            //    mousePositionOnScreen.z = screenPosition.z;
            //    mousePositionInworld = camera.ScreenToWorldPoint(mousePositionOnScreen);
            //    Debug.Log(mousePositionInworld);
            //    cube.transform.position = new Vector3(mousePositionInworld.x * 10, cube.transform.position.y, cube.transform.position.z);
            //}
        }

    }
}
