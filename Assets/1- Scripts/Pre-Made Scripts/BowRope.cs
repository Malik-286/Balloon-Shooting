using UnityEngine;
using System.Collections;


public class BowRope : MonoBehaviour
{
		
		public LineRenderer rope1, rope2;

	
		public Transform bowTopPoint;

	
		public Transform bowBottomPoint;

	
		public Transform arrowCatchPoint;

		public Color color;

		
		public Vector2 width = new Vector2 (0.03f, 0.03f);

	
		public Material ropeMaterial;

		public static BowRope instance;

      void Awake()
    {
        instance = this;
    }

    void Start ()
		{
				//Setting up the references
				if (ropeMaterial == null) {
					ropeMaterial = new Material (Shader.Find ("Sprites/Default"));
				}

				if (rope1 == null) {
						rope1 = transform.Find ("Rope1").GetComponent<LineRenderer> ();
				}

				if (rope2 == null) {
						rope2 = transform.Find ("Rope2").GetComponent<LineRenderer> ();
				}

				if (bowTopPoint == null) {
						bowTopPoint = transform.Find ("TopPoint");
				}

				if (bowBottomPoint == null) {
						bowBottomPoint = transform.Find ("BottomPoint");
				}

				//Setting up ropes width
				rope1.startWidth = width.x;
				rope1.endWidth = width.y;

				rope2.startWidth = width.x;
				rope2.endWidth = width.y;

		        
				//Setting up material color
				ropeMaterial.color = color;

				//Setting up ropes material
				rope1.material = ropeMaterial;
				rope2.material = ropeMaterial;
		}
	
		void LateUpdate ()
		{
				//Draw the rope(top,bottom) of the bow
				if (arrowCatchPoint == null) {
						rope1.SetPosition (0, bowTopPoint.position);
						rope1.SetPosition (1, bowBottomPoint.position);
			
						rope2.SetPosition (0, Vector3.zero);
						rope2.SetPosition (1, Vector3.zero);
						return;
				}

				rope1.SetPosition (0,bowTopPoint.position);
				rope1.SetPosition (1, arrowCatchPoint.position);

				rope2.SetPosition (0, arrowCatchPoint.position);
				rope2.SetPosition (1,bowBottomPoint.position);
		}
}