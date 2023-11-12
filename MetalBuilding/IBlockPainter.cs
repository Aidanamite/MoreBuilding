using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreBuilding
{
    public interface IBlockPainter
    {
        void OnPaintRenderer(Renderer renderer, Block block, MaterialPropertyBlock propBlock, SO_ColorValue primary, SO_ColorValue secondary, int paintSide, uint patternIndex);
    }

    public class GlassBlockPainter : MonoBehaviour, IBlockPainter
    {
        void IBlockPainter.OnPaintRenderer(Renderer renderer, Block block, MaterialPropertyBlock propBlock, SO_ColorValue primary, SO_ColorValue secondary, int paintSide, uint patternIndex)
        {
            propBlock.SetColor("_Color", primary == null ? default : new Color(primary.paintColor.r, primary.paintColor.g, primary.paintColor.b, 0.5f));
        }
    }
}
