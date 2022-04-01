using UnityEngine.UI;

namespace Game
{
    public class RaycastGraphic : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}