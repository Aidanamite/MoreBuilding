using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngine
{
    class MeshBuilder
    {
        Dictionary<Vertex, int> verts = new Dictionary<Vertex, int>();
        Dictionary<int, List<int>> tri = new Dictionary<int, List<int>>();
        int totalTri = 0;
        int GetVertIndex(Vertex vert)
        {
            if (verts.TryGetValue(vert, out var index))
                return index;
            return verts[vert] = verts.Count;
        }
        public void AddTriangle(Vertex a, Vertex b, Vertex c, int submesh = 0)
        {
            var aI = GetVertIndex(a);
            var bI = GetVertIndex(b);
            var cI = GetVertIndex(c);
            if (!tri.TryGetValue(submesh, out var l))
                tri[submesh] = l = new List<int>();
            l.AddRange(new[] { aI, bI, cI });
            totalTri += 3;
        }
        public void AddSquare(Vertex a, Vertex b, Vertex c, Vertex d, int submesh = 0)
        {
            AddTriangle(a, b, c, submesh);
            AddTriangle(a, c, d, submesh);
        }

        public Mesh ToMesh(string name = "")
        {
            var v = new Vector3[verts.Count];
            var u = new Vector2[verts.Count];
            var b = new BoneWeight[verts.Count];
            var s = new SubMeshDescriptor[tri.Count];
            var t = new int[totalTri];
            var n = new Vector3[verts.Count];
            foreach (var p in verts)
            {
                v[p.Value] = p.Key.Location;
                u[p.Value] = p.Key.UV;
                b[p.Value] = p.Key.GetWeight();
                if (p.Key.Normal != null)
                    n[p.Value] = p.Key.Normal.Value.normalized;
            }
            foreach (var p in verts)
                if (p.Key.Normal == null)
                {
                    var sum = Vector3.zero;
                    var edges = new HashSet<Vector3>();
                    foreach (var sub in tri)
                        for (int i = 0; i < sub.Value.Count; i += 3)
                        {
                            var inds = new[] { sub.Value[i], sub.Value[i + 1], sub.Value[i + 2] };
                            var ind = Array.IndexOf(inds, p.Value);
                            if (ind == -1)
                                continue;
                            var v1 = inds[(ind + 1) % 3];
                            var v2 = inds[(ind + 2) % 3];
                            var norm = Quaternion.LookRotation(v[p.Value] - ((v[v1] + v[v2]) / 2), v[v1] - ((v[v1] + v[v2]) / 2)) * Vector3.left;
                            norm.x = Mathf.Round(norm.x * 1000) / 1000;
                            norm.y = Mathf.Round(norm.y * 1000) / 1000;
                            norm.z = Mathf.Round(norm.z * 1000) / 1000;
                            if (edges.Add(norm))
                                sum += norm;
                        }
                    n[p.Value] = sum.normalized;
                }
            var submeshIds = new List<int>();
            foreach (var key in tri.Keys)
                submeshIds.SortedAdd(key);
            var current = 0;
            for (int i = 0; i < submeshIds.Count; i++)
            {
                var sub = tri[submeshIds[i]];
                s[i] = new SubMeshDescriptor(current, sub.Count);
                for (int j = 0; j < sub.Count; j++)
                    t[j + current] = sub[j];
                current += sub.Count;
            }
            var mesh = new Mesh();
            mesh.name = name;
            mesh.vertices = v;
            mesh.uv = u;
            mesh.triangles = t;
            mesh.normals = n;
            mesh.boneWeights = b;
            mesh.subMeshCount = s.Length;
            for (int i = 0; i < s.Length; i++)
                mesh.SetSubMesh(i, s[i]);
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();
            return mesh;
        }
    }
    public class Vertex
    {
        public readonly Vector3 Location;
        public readonly Vector2 UV;
        readonly HashSet<Weight> BoneWeights;
        public readonly Weight Weight0;
        public readonly Weight Weight1;
        public readonly Weight Weight2;
        public readonly Weight Weight3;
        public readonly Vector3? Normal;
        public readonly int Unique;

        public Vertex(Vector3 location, Vector2 uv = default, Weight weight0 = null, Weight weight1 = null, Weight weight2 = null, Weight weight3 = null, Vector3? normal = null, int unique = 0)
        {
            Location = location;
            UV = uv;
            BoneWeights = new HashSet<Weight>();
            if (weight0 != null)
                BoneWeights.Add(weight0);
            if (weight1 != null)
                BoneWeights.Add(weight1);
            if (weight2 != null)
                BoneWeights.Add(weight2);
            if (weight3 != null)
                BoneWeights.Add(weight3);
            var a = BoneWeights.ToArray();
            if (a.Length >= 1)
                Weight0 = a[0];
            if (a.Length >= 2)
                Weight1 = a[1];
            if (a.Length >= 3)
                Weight2 = a[2];
            if (a.Length >= 4)
                Weight3 = a[3];
            Normal = normal;
            Unique = unique;
        }

        public BoneWeight GetWeight() => new BoneWeight()
        {
            boneIndex0 = Weight0?.BoneIndex ?? -1,
            weight0 = Weight0?.Strength ?? 0,
            boneIndex1 = Weight1?.BoneIndex ?? -1,
            weight1 = Weight1?.Strength ?? 0,
            boneIndex2 = Weight2?.BoneIndex ?? -1,
            weight2 = Weight2?.Strength ?? 0,
            boneIndex3 = Weight3?.BoneIndex ?? -1,
            weight3 = Weight3?.Strength ?? 0
        };

        public static implicit operator Vertex(Vector3 value) => new Vertex(value);
        public static implicit operator Vertex((Vector3, Vector2) value) => new Vertex(value.Item1, value.Item2);
        public static implicit operator Vertex((Vector3, Vector2, Weight) value) => new Vertex(value.Item1, value.Item2, value.Item3);
        public static implicit operator Vertex((Vector3, Vector2, Weight, Weight) value) => new Vertex(value.Item1, value.Item2, value.Item3, value.Item4);
        public static implicit operator Vertex((Vector3, Vector2, Weight, Weight, Weight) value) => new Vertex(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
        public static implicit operator Vertex((Vector3, Vector2, Weight, Weight, Weight, Weight) value) => new Vertex(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);

        public override int GetHashCode() => Location.GetHashCode() ^ ~UV.GetHashCode() ^ BoneWeights.Join(x => x.GetHashCode(), (a, b) => a ^ b) ^ ~(Normal?.GetHashCode() ?? -1) ^ Unique.GetHashCode();
        public override bool Equals(object obj)
        {
            if (obj is Vertex other)
                return Location.Equals(other.Location) && UV.Equals(other.UV) && Unique.Equals(other.Unique) && ((Normal != null && other.Normal != null) ? Normal.Value.Equals(other.Normal.Value) : (Normal.HasValue == other.Normal.HasValue)) && BoneWeights.SetEquals(other.BoneWeights);
            return base.Equals(obj);
        }
        public static bool operator ==(Vertex a, Vertex b) => (object)a == null ? (object)b == null ? true : b.Equals(a) : a.Equals(b);
        public static bool operator !=(Vertex a, Vertex b) => !(a == b);

        public class Weight
        {
            public readonly int BoneIndex;
            public readonly float Strength;
            public Weight(int boneIndex, float strength)
            {
                BoneIndex = boneIndex;
                Strength = strength;
            }
            public static implicit operator Weight((int, float) value) => new Weight(value.Item1, value.Item2);
            public override int GetHashCode() => BoneIndex.GetHashCode() ^ ~Strength.GetHashCode();
            public override bool Equals(object obj)
            {
                if (obj is Weight other)
                    return BoneIndex.Equals(other.BoneIndex) && Strength.Equals(other.Strength);
                return base.Equals(obj);
            }
            public static bool operator ==(Weight a, Weight b) => (object)a == null ? (object)b == null ? true : b.Equals(a) : a.Equals(b);
            public static bool operator !=(Weight a, Weight b) => !(a == b);
        }
    }

    static class MeshBuilderExtentionMethods
    {
        public static void SortedAdd<T>(this List<T> list, T value, IComparer<T> compare = null)
        {
            if (compare == null)
                compare = Comparer<T>.Default;
            if (list.Count == 0)
            {
                list.Add(value);
                return;
            }
            var p = list.BinarySearch(value, compare);
            list.Insert(p < 0 ? ~p : p, value);
        }

        public static Y Join<X, Y>(this IEnumerable<X> collection, Func<X, Y> getter, Func<Y, Y, Y> joiner, Predicate<X> condition = null)
        {
            if (condition == null)
                condition = x => true;
            var first = true;
            var current = default(Y);
            foreach (var item in collection)
                if (condition(item))
                {
                    if (first)
                    {
                        first = false;
                        current = getter(item);
                    }
                    else
                        current = joiner(current, getter(item));
                }
            return current;
        }
    }
}
