using System;
using UnityEngine;
using static BlockCreator;


namespace MoreBuilding
{
    public class MeshBox
    {
        public Vector3 min;
        public Vector3 max;
        public UVData uv;
        public Func<Vector3, Vector3> ModifyVerts;
        public FaceChanges faces;
        public bool wedge;
        public MeshBox(Vector3 Min, Vector3 Max, UVData UV, bool Wedge = false, Quaternion Rotation = default, FaceChanges Faces = default) : this(Min, Max, UV, x => Rotation * x, Wedge, Faces) { }
        public MeshBox(Vector3 Min, Vector3 Max, UVData UV, Func<Vector3,Vector3> Modify, bool Wedge = false, FaceChanges Faces = default)
        {
            min = Min;
            max = Max;
            uv = UV;
            wedge = Wedge;
            ModifyVerts = Modify ?? (x => x);
            faces = Faces;
        }
        public static class Presets
        {
            public static MeshBox ThinPlatform(float thickness, float startHeight = 0) => new MeshBox(
                    new Vector3(-HalfBlockSize, startHeight - thickness, -HalfBlockSize),
                    new Vector3(HalfBlockSize, startHeight, HalfBlockSize),
                    new UVData(new Vector4(0, 0, 0.9f, 1), new Vector2(0, 0.1f), 0, 1, 1, -90));
            public static MeshBox ThinPlatformWedge(float thickness, float startHeight = 0) => new MeshBox(
                    new Vector3(-HalfBlockSize, startHeight - thickness, -HalfBlockSize),
                    new Vector3(HalfBlockSize, startHeight, HalfBlockSize),
                    new UVData(new Vector4(0.9f, 1, 0, 0), new Vector2(0, 0.1f), 0, 1, 1, -90), true, Quaternion.Euler(0, 180, 0));
            public static MeshBox ThickPlatform(float thickness, float uvHeight, float startHeight = 0) => new MeshBox(
                    new Vector3(-HalfBlockSize, startHeight - thickness, -HalfBlockSize),
                    new Vector3(HalfBlockSize, startHeight, HalfBlockSize),
                    new UVData(new Vector4(0, 0, 0.9f, 1), new Vector2(1 - uvHeight, 1), -0.05f, 1, 1));
            public static MeshBox ThickPlatformWedge(float thickness, float uvHeight, float startHeight = 0) => new MeshBox(
                    new Vector3(-HalfBlockSize, startHeight - thickness, -HalfBlockSize),
                    new Vector3(HalfBlockSize, startHeight, HalfBlockSize),
                    new UVData(new Vector4(0.9f, 1, 0, 0), new Vector2(1 - uvHeight, 1), -0.05f, 1, 1), true, Quaternion.Euler(0, 180, 0));
        }
    }

    public struct FaceChanges
    {
        public bool excludeN;
        public bool excludeE;
        public bool excludeS;
        public bool excludeW;
        public bool excludeU;
        public bool excludeD;
        public Vector3 extrudeN;
        public Vector3 extrudeE;
        public Vector3 extrudeS;
        public Vector3 extrudeW;
        public Vector3 extrudeU;
        public Vector3 extrudeD;
    }

    public class UVData
    {
        public float xStart;
        public Vector2 Vertical;
        public float Face1Width;
        public float Face2Width;
        public Vector4 Ends;
        public float Rot;
        public bool EndFlipped;
        public bool AltEndUV;
        public UVData(Vector4 EndFacesUV, Vector2 SideUVY, float XStart, float NSFaceWidth, float EWFaceWidth, float Rotation = 0, bool FlipEndUV = false, bool DifferentEnds = false)
        {
            Ends = EndFacesUV;
            Vertical = SideUVY;
            xStart = XStart;
            Face1Width = NSFaceWidth;
            Face2Width = EWFaceWidth;
            Rot = Rotation;
            EndFlipped = FlipEndUV;
            AltEndUV = DifferentEnds;
        }
        public Vector2 EndMin => new Vector2(Ends.x, Ends.y);
        public Vector2 EndMax => new Vector2(Ends.z, Ends.w);
        public Vector2 EndMinMax => EndFlipped ? new Vector2(Ends.z, Ends.y) : new Vector2(Ends.x, Ends.w);
        public Vector2 EndMaxMin => EndFlipped ? new Vector2(Ends.x, Ends.w) : new Vector2(Ends.z, Ends.y);
    }
}