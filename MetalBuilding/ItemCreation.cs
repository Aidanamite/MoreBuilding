using UnityEngine;
using System;

namespace MoreBuilding
{
    public class ItemCreation
    {
        public int baseIndex;
        public Item_Base baseItem;
        public string uniqueName;
        public int uniqueIndex;
        public Item_Base item;
        public bool loadIcon;
        public bool isUpgrade;
        public string localization;
        public CostMultiple[] cost;
    }

    public class BlockItemCreation : ItemCreation
    {
        public MeshData mesh;
        public int upgradeItem;
        public Func<Material> material
        {
            set
            {
                materials = value;
            }
        }
        public Materials materials;
        public Vector3[] modelScales;
        public bool[][] resetModelRotations;
        public Action<Block> additionEdits;
        public int mirroredItem = -1;
    }

    public class Materials
    {
        Material[] mats;
        Func<Material>[] matGets;
        public static implicit operator Materials(Material[] m) => new Materials() { mats = m };
        public static implicit operator Materials(Material m) => new Materials() { mats = new[] { m } };
        public static implicit operator Materials(Func<Material>[] m) => new Materials() { matGets = m };
        public static implicit operator Materials(Func<Material> m) => new Materials() { matGets = new[] { m } };

        public Material this[int index]
        {
            get
            {
                if (mats == null)
                {
                    if (matGets == null)
                        return null;
                    mats = new Material[matGets.Length];
                }
                if (mats.Length <= index)
                    index = 0;
                if (mats[index] == null)
                {
                    var m = matGets.GetSafe(index);
                    if (m != null)
                        mats[index] = m();
                }
                return mats[index] ?? mats[0];
            }
        }
    }

    public class MeshSource
    {
        Mesh mesh;
        MeshBox[] meshboxes;
        Func<Mesh> getMesh;
        MeshSource() { }
        MeshSource(Func<Mesh> getter) : this() => getMesh = getter;
        public static implicit operator MeshSource(Mesh m) => new MeshSource() { mesh = m };
        public static implicit operator MeshSource(Func<Mesh> m) => new MeshSource() { getMesh = m };
        public static implicit operator MeshSource(MeshBox m) => new MeshSource() { meshboxes = new[] { m } };
        public static implicit operator MeshSource(MeshBox[] m) => new MeshSource() { meshboxes = m };
        public Mesh Mesh
        {
            get
            {
                if (mesh == null)
                {
                    if (getMesh != null)
                        mesh = getMesh();
                    if (mesh == null && meshboxes != null)
                        mesh = MeshBox.CreateMesh(meshboxes);
                }
                return mesh;
            }
        }
    }

    public class MeshData
    {
        MeshSource[][] meshes;
        public static implicit operator MeshData(MeshSource m) => new MeshData() { meshes = new[] { new[] { m } } };
        public static implicit operator MeshData(MeshSource[] m) => new MeshData() { meshes = new[] { m } };
        public static implicit operator MeshData(MeshSource[][] m) => new MeshData() { meshes = m };
        public static implicit operator MeshData(Mesh m) => new MeshData() { meshes = new[] { new[] { (MeshSource)m } } };
        public static implicit operator MeshData(Mesh[] m) => new MeshData() { meshes = new[] { m.Cast(x => (MeshSource)x) } };
        public static implicit operator MeshData(Mesh[][] m) => new MeshData() { meshes = m.Cast(y => y.Cast(x => (MeshSource)x)) };
        public static implicit operator MeshData(MeshBox m) => new MeshData() { meshes = new[] { new[] { (MeshSource)m } } };
        public static implicit operator MeshData(MeshBox[] m) => new MeshData() { meshes = new[] { new[] { (MeshSource)m } } };
        public static implicit operator MeshData(MeshBox[][] m) => new MeshData() { meshes = new[] { m.Cast(x => (MeshSource)x) } };
        public static implicit operator MeshData(MeshBox[][][] m) => new MeshData() { meshes = m.Cast(y => y.Cast(x => (MeshSource)x)) };

        public Mesh[] this[int index] => (meshes.GetSafe(index) ?? meshes[0]).Cast(x => x.Mesh);
    }
}