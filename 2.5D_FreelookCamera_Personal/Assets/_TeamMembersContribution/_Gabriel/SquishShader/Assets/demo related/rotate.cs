using UnityEngine;

namespace _Gabriel.SquishShader.Assets.demo_related
{
    public class rotate : MonoBehaviour {
        void Update () {
            transform.Rotate(Vector3.up, Time.deltaTime * 60);
        }
    }
}
