using UnityEngine;
using System;
using System.Collections.Generic;

namespace MoreBuilding
{
    public class ItemCreation
    {
        public int baseIndex;
        public Item_Base baseItem;
        public string uniqueName;
        public int uniqueIndex;
        public Index UniqueIndex { set => uniqueIndex = (int)value; }
        public Item_Base item;
        public bool loadIcon = true;
        public bool isUpgrade;
        public Func<string> localization;
        public CostMultiple[] cost;
        public virtual Index standardIndexSetup
        {
            set
            {
                UniqueIndex = value;
                standardSetup = StandardItemSetup.GetValues(value);
            }
        }
        public virtual (UniqueName material, UniqueName item) standardSetup
        {
            set
            {
                uniqueName = value.item.ToText(value.material);
                var ml = StandardItemSetup.GetLocalization(value.material);
                var il = StandardItemSetup.GetLocalization(value.item);
                localization = () => il.ToText(ml);
                isUpgrade = value.item == UniqueName.Upgrade;
            }
        }

        public virtual ItemCreation Clone()
        {
            var i = (ItemCreation)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(GetType());
            i.baseIndex = baseIndex;
            i.uniqueName = uniqueName;
            i.uniqueIndex = uniqueIndex;
            i.loadIcon = loadIcon;
            i.isUpgrade = isUpgrade;
            i.localization = localization;
            i.cost = cost;
            return i;
        }

        public virtual ItemCreation GetRealInstance() => this;
    }

    public class BlockItemCreation : ItemCreation
    {
        public MeshData mesh;
        public int upgradeItem;
        public Index UpgradeItem { set => upgradeItem = (int)value; }
        public Materials materials;
        public Func<Material> material { set => materials = value; }
        public Vector3[] modelScales;
        public Quaternion[][] modelRotations;
        public Action<Block> additionEdits;
        public int mirroredItem = -1;
        public Index MirroredItem { set => mirroredItem = (int)value; }
        public override (UniqueName material, UniqueName item) standardSetup
        {
            set
            {
                base.standardSetup = value;
                materials = StandardItemSetup.GetMaterials(value.material);
                UpgradeItem = StandardItemSetup.GetUpgradeIndex(value.material);
                MirroredItem = StandardItemSetup.GetMirrored(value.material, value.item);
            }
        }
        public override ItemCreation Clone()
        {
            var i = (BlockItemCreation)base.Clone();
            i.mesh = mesh;
            i.upgradeItem = upgradeItem;
            i.materials = materials;
            i.modelScales = modelScales;
            i.modelRotations = modelRotations;
            i.additionEdits = additionEdits;
            return i;
        }
    }

    public class MimicItemCreation<T> : ItemCreation where T : ItemCreation
    {
        Func<T> _g;
        Action<T> _m;
        T _i;
        public MimicItemCreation(Func<T> getBase, Action<T> modify) => (_g, _m) = (getBase,modify);
        public override ItemCreation GetRealInstance()
        {
            if (_i == null && _g != null)
            {
                try
                {
                    var i = (T)_g().Clone();
                    _m?.Invoke(i);
                    _i = i;
                }
                catch { }
            }
            return _i;
        }
        public override ItemCreation Clone() => GetRealInstance().Clone();
    }

    public class Materials
    {
        Material[][] mats;
        Func<Material[][]> matGets;
        public static implicit operator Materials(Material[][] m) => new Materials() { mats =  m };
        public static implicit operator Materials(Material[] m) => new Materials() { mats = new[] { m } };
        public static implicit operator Materials(Material m) => new Materials() { mats = new[] { new[] { m } } };
        public static implicit operator Materials(Func<Material>[][] m) => new Materials() { matGets = () => m.Cast(x => x.Cast(y => y?.Invoke())) };
        public static implicit operator Materials(Func<Material>[] m) => new Materials() { matGets = () => new[] { m.Cast(x => x?.Invoke()) } };
        public static implicit operator Materials(Func<Material> m) => new Materials() { matGets = () => new[] { new[] { m?.Invoke() } } };
        public static implicit operator Materials(Func<Material[]> m) => new Materials() { matGets = () => new[] { m?.Invoke() } };
        public static implicit operator Materials(Func<Material[][]> m) => new Materials() { matGets = m };

        public Material[] this[int index]
        {
            get
            {
                if (mats == null)
                {
                    if (matGets == null)
                        return null;
                    mats = matGets();
                }
                if (mats.Length <= index)
                    index = 0;
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
        public MeshSource(Func<Mesh> getter) : this() => getMesh = getter;
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
                    if (mesh != null)
                        Main.createdObjects.Add(mesh);
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

    public static class StandardItemSetup
    {
        static Dictionary<UniqueName, Localization> convertOverrides = new Dictionary<UniqueName, Localization>()
        {
            { UniqueName.WallSlopeInverted, Localization.WallSlope }
        };
        public static Localization GetLocalization(UniqueName uniqueName)
        {
            if (convertOverrides.TryGetValue(uniqueName, out var l))
                return l;
            var s = uniqueName.ToString();
            if (s.EndsWith("Mirrored"))
                s = s.Remove(s.Length - 8);
            return (Localization)Enum.Parse(typeof(Localization), s);
        }
        public static Materials GetMaterials(UniqueName material)
        {
            if (material == UniqueName.ScrapMetal)
                return (Materials)(() => Main.instance.ScrapMetal);
            if (material == UniqueName.SolidMetal)
                return (Materials)(() => Main.instance.Metal);
            if (material == UniqueName.ScrapMetal)
                return (Materials)(() => Main.instance.Glass);
            throw new NullReferenceException();
        }
        public static Index GetUpgradeIndex(UniqueName material)
        {
            return (Index)Enum.Parse(typeof(Index), material + "_Upgrade");
        }
        public static (UniqueName material, UniqueName item) GetValues(Index index)
        {
            var a = index.ToString().Split('_');
            return ((UniqueName)Enum.Parse(typeof(UniqueName), a[0]), (UniqueName)Enum.Parse(typeof(UniqueName), a[1]));
        }
        public static Index GetMirrored(UniqueName material, UniqueName item)
        {
            var s = item.ToString();
            if (s.EndsWith("Mirrored"))
                return (Index)Enum.Parse(typeof(Index), material + "_" + s.Remove(s.Length - 8));
            return (Index)(-1);
        }
    }
}