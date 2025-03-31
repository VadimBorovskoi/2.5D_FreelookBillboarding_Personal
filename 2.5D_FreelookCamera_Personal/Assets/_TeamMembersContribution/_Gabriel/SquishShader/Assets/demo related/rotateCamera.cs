using UnityEngine;

namespace _Gabriel.SquishShader.Assets.demo_related
{
	public class rotateCamera : MonoBehaviour {
		public Transform focusTarget;
		void Update () {
			transform.Translate(Vector3.right * Time.deltaTime*10);
			transform.LookAt(focusTarget);
		}
	}
}
