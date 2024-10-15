using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

namespace ArcheryToolkit
{
    public class Path : MonoBehaviour
    {
        /// <summary>
        /// Whether the path is enabled or not.
        /// </summary>
        public bool isEnabled = true;

        /// <summary>
        /// Number of points in the path.
        /// </summary>
        [Range(5, 100)]
        public int size = 20;

        /// <summary>
        /// The points list of the path.
        /// </summary>
        private List<Point> points = new List<Point>();

        /// <summary>
        /// The point prefab.
        /// </summary>
        public GameObject pointPrefab;

        // Use this for initialization
        void Start()
        {
            //Create new points
            CreatePoints();
        }

        /// <summary>
        /// Draw the path based on given pivot , velocity.
        /// </summary>
        public void Draw(Vector3 pivot, Vector3 pv)
        {
            if (!isEnabled)
            {
                return;
            }

            float velocity = Mathf.Sqrt((pv.x * pv.x) + (pv.y * pv.y));
            float angle = Mathf.Rad2Deg * (Mathf.Atan2(pv.y, pv.x));
            float time = 0;

            bool skipNext = false;

            for (int i = 0; i < size; i++)
            {

                if (points[i].targetTriggered)
                {
                    skipNext = true;
                }

                if (skipNext)
                {
                    points[i].HideResources();
                }
                else
                {
                    points[i].ShowResources();
                }

                time += 0.05f;

                float dx = velocity * time * Mathf.Cos(angle * Mathf.Deg2Rad);
                float dy = velocity * time * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * time * time / 2.0f);

                Vector3 pos = new Vector3(pivot.x + dx, pivot.y + dy, 0);

                points[i].transform.position = pos;
                points[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pv.y - (Physics2D.gravity.magnitude) * time, pv.x) * Mathf.Rad2Deg);
            }
        }


        /// <summary>
        /// Creates the points.
        /// </summary>
        private void CreatePoints()
        {
            for (int i = 0; i < size; i++)
            {
                GameObject point = (GameObject)Instantiate(pointPrefab);
                point.name = "P" + (i + 1);
                point.transform.SetParent(transform);
                point.transform.position = Vector3.zero;
                point.transform.localScale = pointPrefab.transform.localScale;
                points.Insert(i, point.GetComponent<Point>());
            }
        }


        /// <summary>
        /// Reset the path.
        /// </summary>
        public void Reset()
        {
            if (points == null)
                return;

            for (int i = 0; i < points.Count; i++)
            {
                points[i].Reset();
            }
        }
    }

}
