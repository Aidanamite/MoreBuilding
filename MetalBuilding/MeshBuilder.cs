using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;

namespace UnityEngine
{
    public class MeshBuilder : ICloneable
    {
        public int VertexCount => verts.Count;
        public int SubmeshCount => tri.Count;
        public IEnumerable<int> SubmeshIndecies => tri.Keys;
        Dictionary<Vertex, int> verts = new Dictionary<Vertex, int>();
        Dictionary<int, List<int>> tri = new Dictionary<int, List<int>>();
        int totalTri = 0;
        object ICloneable.Clone() => Clone();
        public MeshBuilder Clone()
        {
            var m = new MeshBuilder();
            foreach (var p in verts)
                m.verts[p.Key] = p.Value;
            foreach (var p in tri)
                m.tri[p.Key] = new List<int>(p.Value);
            m.totalTri = totalTri;
            return m;
        }
        int GetVertIndex(Vertex vert)
        {
            if (HasVertIndex(vert, out var index))
                return index;
            return verts[vert] = verts.Count;
        }
        bool HasVertIndex(Vertex vert, out int index) => verts.TryGetValue(vert, out index);
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
        public void AddMesh(Mesh mesh, bool mergeWithExisting = true, bool mergeWithSelf = true, Func<int,Vertex,Vertex> modify = null, Func<int,int> modifySubmeshIndex = null)
        {
            if (!mesh)
            {
                Debug.LogError("Cannot add mesh to meshbuilder. Mesh is null");
                return;
            }
            if (!mesh.isReadable)
            {
                Debug.LogError("Cannot add mesh to meshbuilder. Mesh is not readable");
                return;
            }
            AddMesh(
                mesh.vertices,
                mesh.uv,
                mesh.GetAllBoneWeights(),
                mesh.GetBonesPerVertex(),
                mesh.normals.GetNullable(),
                mesh.tangents.GetNullable(),
                mesh.colors,
                mesh.subMeshCount == 0 ? new List<int[]> { mesh.triangles } : mesh.Gather(0, x => x < mesh.subMeshCount, (x, y) => y + 1, (x, y) => x.GetTriangles(y)),
                mergeWithExisting,
                mergeWithSelf,
                modify,
                modifySubmeshIndex);
        }

        void AddMesh(IList<Vector3> vertices, IList<Vector2> uv, NativeArray<BoneWeight1> weights, IEnumerable<byte> bonesPerVertex, IList<Vector3?> normals, IList<Vector4?> tangents, IList<Color> colors, IList<int[]> submeshes, bool mergeWithExisting = true, bool mergeWithSelf = true, Func<int, Vertex, Vertex> modify = null, Func<int, int> modifySubmeshIndex = null)
        {
            if (modify == null)
                modify = (y, x) => x;
            if (modifySubmeshIndex == null)
                modifySubmeshIndex = x => x;
            var bones = new List<(int start, int end)>();
            var c = 0;
            if (bonesPerVertex != null)
                foreach (var b in bonesPerVertex)
                    bones.Add((c, c += b));
            var vert = new List<Vertex>();
            var hashVert = new HashSet<Vertex>();
            for (int i = 0; i < vertices.Count; i++)
            {
                var w = bones.GetSafe(i);
                var v = modify(vert.Count, new Vertex(vertices[i], uv.GetSafe(i), weights.Gather(w.start, x => x < w.end, (x, y) => y + 1, (x, y) => (Vertex.Weight)x[y]), colors.GetSafe(i, Color.white), normals.GetSafe(i), tangents.GetSafe(i)));
                if (v != null)
                    if (mergeWithSelf || mergeWithExisting)
                        while ((!mergeWithExisting && HasVertIndex(v, out _)) || (!mergeWithSelf && hashVert.Contains(v)))
                        {
                            v = new Vertex(v, unique: v.Unique + 1);
                            if (v.Unique == 0)
                                throw new IndexOutOfRangeException("Could not enforce a unique vertex. Too many of the same vertex exist. There should not be this many");
                        }
                vert.Add(v);
                if (v != null)
                    hashVert.Add(v);
            }
            var j = 0;
            foreach (var s in submeshes)
            {
                for (int i = 0; i < s.Length; i += 3)
                    if (vert[s[i]] != null && vert[s[i + 1]] != null && vert[s[i + 2]] != null)
                        AddTriangle(vert[s[i]], vert[s[i + 1]], vert[s[i + 2]], modifySubmeshIndex(j));
                j++;
            }
        }

        public Mesh ToMesh(string name = "", bool enforceNormalsIncludeTouchingVerts = false)
        {
            var v = new Vector3[verts.Count];
            var u = new Vector2[verts.Count];
            var w = new byte[verts.Count];
            var b = new List<BoneWeight1>[verts.Count];
            var c = new Color[verts.Count];
            var s = new SubMeshDescriptor[tri.Count];
            var t = new int[totalTri];
            var n = new Vector3[verts.Count];
            var tg = new Vector4[verts.Count];
            foreach (var p in verts)
            {
                v[p.Value] = p.Key.Location;
                u[p.Value] = p.Key.UV;
                var bones = p.Key.GetWeight();
                b[p.Value] = bones;
                w[p.Value] = (byte)bones.Count;
                if (p.Key.Normal != null)
                    n[p.Value] = p.Key.Normal.Value.normalized;
                if (p.Key.Tangent != null)
                    tg[p.Value] = p.Key.Tangent.Value;
            }
            foreach (var p in verts)
                if (p.Key.Normal == null || p.Key.Tangent == null)
                {
                    var sum = Vector3.zero;
                    var sum2 = Vector3.zero;
                    var edges = new HashSet<Vector3>();
                    foreach (var sub in tri)
                        for (int i = 0; i < sub.Value.Count; i += 3)
                        {
                            var inds = new[] { sub.Value[i], sub.Value[i + 1], sub.Value[i + 2] };
                            var ind = Array.IndexOf(inds, p.Value);
                            if (p.Key.Tangent == null && ind != -1)
                            {
                                var v1 = inds[(ind + 1) % 3];
                                var v2 = inds[(ind + 2) % 3];
                                if (u[p.Value] != u[v1] && u[p.Value] != u[v2] && u[v2] != u[v1] && v[p.Value] != v[v1] && v[p.Value] != v[v2] && v[v2] != v[v1])
                                    sum2 += Quaternion.LerpUnclamped(Quaternion.LookRotation(v[v1] - v[p.Value]), Quaternion.LookRotation(v[v2] - v[p.Value]), Vector2.SignedAngle(u[v1] - u[p.Value], Vector2.right) / Vector2.SignedAngle(u[v1] - u[p.Value], u[v2] - u[p.Value])) * Vector3.forward;
                            }
                            if (p.Key.Normal == null)
                            {
                                if (ind == -1)
                                {
                                    if (enforceNormalsIncludeTouchingVerts)
                                        ind = Array.FindIndex(inds, x => v[x] == v[p.Value]);
                                    if (ind == -1)
                                        continue;
                                }
                                var v1 = inds[(ind + 1) % 3];
                                var v2 = inds[(ind + 2) % 3];
                                var norm = Quaternion.LookRotation(v[p.Value] - ((v[v1] + v[v2]) / 2), v[v1] - ((v[v1] + v[v2]) / 2)) * Vector3.left;
                                norm.x = Mathf.Round(norm.x * 1000) / 1000;
                                norm.y = Mathf.Round(norm.y * 1000) / 1000;
                                norm.z = Mathf.Round(norm.z * 1000) / 1000;
                                if (edges.Add(norm))
                                    sum += norm;
                            }
                        }
                    if (p.Key.Normal == null)
                        n[p.Value] = sum.normalized;
                    if (p.Key.Tangent == null)
                    {
                        sum2 = sum2.normalized;
                        tg[p.Value] = new Vector4(sum2.x, sum2.y, sum2.z, -1);
                    }
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
            mesh.tangents = tg;
            mesh.colors = c;
            mesh.SetBoneWeights(new NativeArray<byte>(w,Allocator.Temp), new NativeArray<BoneWeight1>(b.Squash().ToArray(), Allocator.Temp));
            mesh.subMeshCount = s.Length;
            for (int i = 0; i < s.Length; i++)
                mesh.SetSubMesh(i, s[i]);
            mesh.RecalculateBounds();
            return mesh;
        }
    }
    public class Vertex
    {
        public readonly Vector3 Location;
        public readonly Vector2 UV;
        readonly HashSet<Weight> Bones;
        public IEnumerable<Weight> BoneWeights => Bones;
        public readonly Color Color;
        public readonly Vector3? Normal;
        public readonly Vector4? Tangent;
        public readonly int Unique;

        public Vertex(Vector3 location, Vector2 uv = default, IEnumerable<Weight> weights = null, Color color = default, Vector3? normal = null, Vector4? tangent = null, int unique = 0)
        {
            Location = location;
            UV = uv;
            Bones = new HashSet<Weight>(weights != null && weights.Any(x => x != null) ? weights.Cast(x => x,x => x != null) as IEnumerable<Weight> : Weight.Defaults);
            Color = color;
            Normal = normal;
            Tangent = tangent;
            Unique = unique;
        }
        public Vertex(Vertex original, Optional<Vector3> location = null, Optional<Vector2> uv = null, Optional<IEnumerable<Weight>> weights = null, Optional<Color> color = null, Optional<Vector3?> normal = null, Optional<Vector4?> tangent = null, Optional<int> unique = null, Func<Vertex, Vector3> locationModify = null, Func<Vertex, Vector2> uvModify = null, Func<Vertex, IEnumerable<Weight>> weightsModify = null, Func<Vertex, Color> colorModify = null, Func<Vertex, Vector3?> normalModify = null, Func<Vertex, Vector4?> tangentModify = null, Func<Vertex, int> uniqueModify = null)
            : this(
                  locationModify == null ? location ^ original.Location : locationModify(original),
                  uvModify == null ? uv ^ original.UV : uvModify(original),
                  weightsModify == null ? weights ^ original.BoneWeights : weightsModify(original),
                  colorModify == null ? color ^ original.Color : colorModify(original),
                  normalModify == null ? normal ^ original.Normal : normalModify(original),
                  tangentModify == null ? tangent ^ original.Tangent : tangentModify(original),
                  uniqueModify == null ? unique ^ original.Unique : uniqueModify(original)
                  ) { }
        

        public List<BoneWeight1> GetWeight() => new SortedSet<Weight>(Bones,new FuncComparer<Weight>((x,y) => y.Strength == x.Strength ? -1 : y.Strength.CompareTo(x.Strength))).Cast(x => (BoneWeight1)x);

        public static implicit operator Vertex(Vector3 value) => new Vertex(value);
        public static implicit operator Vertex((Vector3, Vector2) value) => new Vertex(value.Item1, value.Item2);
        public static implicit operator Vertex((Vector3, Vector2, Weight) value) => new Vertex(value.Item1, value.Item2, new[] { value.Item3 });
        public static implicit operator Vertex((Vector3, Vector2, Weight, Weight) value) => new Vertex(value.Item1, value.Item2, new[] { value.Item3, value.Item4 });
        public static implicit operator Vertex((Vector3, Vector2, Weight, Weight, Weight) value) => new Vertex(value.Item1, value.Item2, new[] { value.Item3, value.Item4, value.Item5 });
        public static implicit operator Vertex((Vector3, Vector2, Weight, Weight, Weight, Weight) value) => new Vertex(value.Item1, value.Item2, new[] { value.Item3, value.Item4, value.Item5, value.Item6 });

        public override int GetHashCode() => Location.GetHashCode() ^ ~UV.GetHashCode() ^ Bones.Join(x => x.GetHashCode(), (a, b) => a ^ b) ^ ~Color.GetHashCode() ^ (Normal?.GetHashCode() ?? -1) ^ ~(Tangent?.GetHashCode() ?? -1) ^ Unique.GetHashCode();
        public override bool Equals(object obj)
        {
            if (obj is Vertex other)
                return Location.Equals(other.Location) && UV.Equals(other.UV) && Unique.Equals(other.Unique) && Color.Equals(other.Color) && Normal.SafeEquals(other.Normal) && Tangent.SafeEquals(other.Tangent) && Bones.SetEquals(other.Bones);
            return base.Equals(obj);
        }
        public static bool operator ==(Vertex a, Vertex b) => a.SafeEquals(b);
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
            public static Weight Default => new Weight(0, 1);
            public static Weight[] Defaults => new[] { Default };
            public static implicit operator Weight((int, float) value) => new Weight(value.Item1, value.Item2);
            public static implicit operator Weight(BoneWeight1 value) => new Weight(value.boneIndex, value.weight);
            public static implicit operator BoneWeight1(Weight value) => new BoneWeight1() { boneIndex = value.BoneIndex, weight = value.Strength };
            public override int GetHashCode() => BoneIndex.GetHashCode();
            public override bool Equals(object obj)
            {
                if (obj is Weight other)
                    return BoneIndex.Equals(other.BoneIndex);
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

        public static List<Y> Gather<X,Y,Z>(this X obj, Z start, Predicate<Z> condition, Func<X, Z, Z> step, Func<X, Z, Y> get, Func<X, Z, Y, bool> itemCondition = null)
        {
            var list = new List<Y>();
            for (var current = start; condition(current); current = step(obj,current))
            {
                var item = get(obj, current);
                if (itemCondition == null || itemCondition(obj, current, item))
                    list.Add(item);
            }
            return list;
        }

        public static bool SafeEquals<T>(this T? obj, T? other) where T : struct => (obj != null && other != null) ? obj.Value.Equals(other.Value) : (obj != null ? obj.Value.Equals(null) : (other != null ? other.Value.Equals(null) : true));
        public static bool SafeEquals<T>(this T obj, T other) where T : class => obj != null ? obj.Equals(other) : (other != null ? other.Equals(obj) : true);

        public static T GetSafe<T>(this IList<T> c, int index, T fallback = default)
        {
            if (c == null)
                return fallback;
            if (c.Count <= index)
                return fallback;
            return c[index];
        }
        public static T GetSafe<T>(this NativeArray<T> c, int index, T fallback = default) where T : struct
        {
            if (c == null)
                return fallback;
            if (c.Length <= index)
                return fallback;
            return c[index];
        }

        public static List<T> Squash<T>(this IEnumerable<IEnumerable<T>> collection)
        {
            var l = new List<T>();
            foreach (var x in collection)
                foreach (var y in x)
                    l.Add(y);
            return l;
        }

        public static List<Y> Cast<X,Y>(this IEnumerable<X> collection, Func<X,Y> cast, Predicate<X> precondition = null, Func<X,Y,bool> condition = null)
        {
            var l = new List<Y>();
            foreach (var i in collection)
                if (precondition?.Invoke(i) ?? true)
                {
                    var c = cast(i);
                    if (condition?.Invoke(i,c) ?? true)
                        l.Add(c);
                }
            return l;
        }

        public static IList<T?> GetNullable<T>(this IList<T> source) where T : struct => source == null ? null : new NullableIList<T>(source);
    }
}
public class Optional<T>
{
    T value;
    public Optional(T value) => this.value = value;
    public static implicit operator Optional<T>(T v) => new Optional<T>(v);
    public static implicit operator T(Optional<T> v) => v.value;
    public static T operator ^(Optional<T> a, T b) => a == null ? b : a.value;
}
public class FuncComparer<T> : IComparer<T>
{
    Comparison<T> comparison;
    public FuncComparer(Comparison<T> comparison) => this.comparison = comparison;
    public int Compare(T a, T b) => comparison(a, b);
    public static implicit operator FuncComparer<T>(Comparison<T> v) => new FuncComparer<T>(v);
    public static implicit operator Comparison<T>(FuncComparer<T> v) => v.comparison;
}
public class NullableIList<T> : IList<T?> where T : struct
{
    IList<T> source;
    public NullableIList(IList<T> source) => this.source = source;
    public int IndexOf(T? item) => item == null ? -1 : source.IndexOf(item.Value);
    public void Insert(int index, T? item) => source.Insert(index, item.Value);
    public void RemoveAt(int index) => source.RemoveAt(index);
    public T? this[int index]
    {
        get => source[index];
        set => source[index] = value.Value;
    }
    public void Add(T? item) => source.Add(item.Value);
    public void Clear() => source.Clear();
    public bool Contains(T? item) => item == null ? false : source.Contains(item.Value);
    void ICollection<T?>.CopyTo(T?[] array, int arrayIndex) => throw new NotSupportedException();
    public bool Remove(T? item) => item == null ? false : source.Remove(item.Value);
    public int Count => source.Count;
    public bool IsReadOnly => source.IsReadOnly;
    public IEnumerator<T?> GetEnumerator()
    {
        foreach (var i in source)
            yield return i;
        yield break;
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
