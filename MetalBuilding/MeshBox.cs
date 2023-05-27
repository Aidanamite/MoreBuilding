using System;
using UnityEngine;
using System.Collections.Generic;
using static BlockCreator;
using static MoreBuilding.Main;


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
        public static Mesh[] CreateMesh(MeshBox[][] data)
        {
            var a = new Mesh[data.Length];
            for (int i = 0; i < a.Length; i++)
                if (data[i] != null)
                    a[i] = CreateMesh(data[i]);
            return a;
        }

        public static Mesh CreateMesh(MeshBox[] data)
        {
            var m = new MeshBuilder();
            foreach (var d in data)
            {
                var min = d.min;
                var max = d.max;
                var uv = d.uv;
                var v = new List<Vertex>();
                foreach (var i in new[]
                {
                    (max, new Vector2(uv.xStart, uv.Vertical.y), true),
                    (new Vector3(min.x, max.y, max.z), new Vector2(uv.xStart + (d.wedge ? uv.Face2Width : uv.Face1Width), uv.Vertical.y), true),
                    (new Vector3(min.x, min.y, max.z), new Vector2(uv.xStart + (d.wedge ? uv.Face2Width : uv.Face1Width), uv.Vertical.x), true),
                    (new Vector3(max.x, min.y, max.z), new Vector2(uv.xStart, uv.Vertical.x), true),
                    (new Vector3(max.x, max.y, min.z), new Vector2(uv.xStart + (d.wedge ? uv.Face1Width : (uv.Face1Width * 2)) + uv.Face2Width, uv.Vertical.y), true),
                    (new Vector3(min.x, max.y, min.z), new Vector2(uv.xStart + uv.Face1Width + uv.Face2Width, uv.Vertical.y), true),
                    (min, new Vector2(uv.xStart + uv.Face1Width + uv.Face2Width, uv.Vertical.x), true),
                    (new Vector3(max.x, min.y, min.z), new Vector2(uv.xStart + (d.wedge ? uv.Face1Width : (uv.Face1Width * 2)) + uv.Face2Width, uv.Vertical.x), true),

                    (max, uv.EndMin, false),
                    (new Vector3(min.x, max.y, max.z), uv.EndMinMax, false),
                    (new Vector3(min.x, min.y, max.z), uv.AltEndUV ? uv.EndMin : uv.EndMinMax, false),
                    (new Vector3(max.x, min.y, max.z), uv.AltEndUV ? uv.EndMinMax : uv.EndMin, false),
                    (new Vector3(max.x, max.y, min.z), uv.EndMaxMin, false),
                    (new Vector3(min.x, max.y, min.z), uv.EndMax, false),
                    (min, uv.AltEndUV ? uv.EndMaxMin : uv.EndMax, false),
                    (new Vector3(max.x, min.y, min.z), uv.AltEndUV ? uv.EndMax : uv.EndMaxMin, false),

                    (max, new Vector2(uv.xStart + (d.wedge ? uv.Face1Width : (uv.Face1Width * 2)) + uv.Face2Width * 2, uv.Vertical.y), true),
                    (new Vector3(max.x, min.y, max.z), new Vector2(uv.xStart + (d.wedge ? uv.Face1Width : (uv.Face1Width * 2)) + uv.Face2Width * 2, uv.Vertical.x), true)
                })
                    v.Add((d.ModifyVerts(i.Item1), i.Item3 ? i.Item2.Rotate(uv.Rot) : i.Item2));
                if (d.wedge)
                {
                    if (!d.faces.excludeN)
                        m.AddSquare(v[0], v[1], v[2], v[3]);
                    if (!d.faces.excludeE)
                        m.AddSquare(v[16], v[17], v[7], v[4]);
                    if (!d.faces.excludeS && !d.faces.excludeW)
                        m.AddSquare(v[7], v[2], v[1], v[4]);
                    if (!d.faces.excludeU)
                        m.AddTriangle(v[9], v[8], v[12]);
                    if (!d.faces.excludeD)
                        m.AddTriangle(v[11], v[10], v[15]);
                }
                else
                {
                    if (!d.faces.excludeN)
                        m.AddSquare(v[0], v[1], v[2], v[3]);
                    if (!d.faces.excludeE)
                        m.AddSquare(v[16], v[17], v[7], v[4]);
                    if (!d.faces.excludeS)
                        m.AddSquare(v[7], v[6], v[5], v[4]);
                    if (!d.faces.excludeW)
                        m.AddSquare(v[1], v[5], v[6], v[2]);
                    if (!d.faces.excludeU)
                        m.AddSquare(v[9], v[8], v[12], v[13]);
                    if (!d.faces.excludeD)
                        m.AddSquare(v[11], v[10], v[14], v[15]);
                }
            }
            var mesh = m.ToMesh("");
            createdObjects.Add(mesh);
            return mesh;
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