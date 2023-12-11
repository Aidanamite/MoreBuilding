using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using static MoreBuilding.Main;


namespace MoreBuilding
{
    static class ExtentionMethods
    {
        public static Item_Base Clone(this Item_Base source, int uniqueIndex, string uniqueName)
        {
            Item_Base item = ScriptableObject.CreateInstance<Item_Base>();
            item.Initialize(uniqueIndex, uniqueName, source.MaxUses);
            item.name = uniqueName;
            item.settings_buildable = source.settings_buildable.Clone();
            item.settings_consumeable = source.settings_consumeable.Clone();
            item.settings_cookable = source.settings_cookable.Clone();
            item.settings_equipment = source.settings_equipment.Clone();
            item.settings_Inventory = source.settings_Inventory.Clone();
            item.settings_recipe = source.settings_recipe.Clone();
            item.settings_usable = source.settings_usable.Clone();
            return item;
        }
        public static void SetRecipe(this Item_Base item, CostMultiple[] cost, CraftingCategory category = CraftingCategory.Resources, int amountToCraft = 1, bool learnedFromBeginning = false, string subCategory = null, int subCatergoryOrder = 0)
        {
            Traverse recipe = Traverse.Create(item.settings_recipe);
            recipe.Field("craftingCategory").SetValue(category);
            recipe.Field("amountToCraft").SetValue(amountToCraft);
            recipe.Field("learnedFromBeginning").SetValue(learnedFromBeginning);
            recipe.Field("subCategory").SetValue(subCategory);
            recipe.Field("subCatergoryOrder").SetValue(subCatergoryOrder);
            item.settings_recipe.NewCost = cost;
        }

        public static void CopyFieldsOf(this object value, object source)
        {
            var t1 = value.GetType();
            var t2 = source.GetType();
            while (!t1.IsAssignableFrom(t2))
                t1 = t1.BaseType;
            while (t1 != typeof(Object) && t1 != typeof(object))
            {
                foreach (var f in t1.GetFields(~BindingFlags.Default))
                    if (!f.IsStatic)
                        f.SetValue(value, f.GetValue(source));
                t1 = t1.BaseType;
            }
        }

        public static void CopyPropertiesOf(this object value, object source)
        {
            var t1 = value.GetType();
            var t2 = source.GetType();
            while (!t1.IsAssignableFrom(t2))
                t1 = t1.BaseType;
            while (t1 != typeof(Object) && t1 != typeof(object))
            {
                foreach (var p in t1.GetProperties(~BindingFlags.Default))
                    if (p.GetGetMethod() != null && p.GetSetMethod() != null && !p.GetGetMethod().IsStatic)
                        p.SetValue(value, p.GetValue(source));
                t1 = t1.BaseType;
            }
        }

        public static void ReplaceValues(this Component value, object original, object replacement)
        {
            foreach (var c in value.GetComponentsInChildren<Component>())
                (c as object).ReplaceValues(original, replacement);
        }
        public static void ReplaceValues(this GameObject value, object original, object replacement)
        {
            foreach (var c in value.GetComponentsInChildren<Component>())
                (c as object).ReplaceValues(original, replacement);
        }

        public static void ReplaceValues(this object value, object original, object replacement)
        {
            var t = value.GetType();
            while (t != typeof(Object) && t != typeof(object))
            {
                foreach (var f in t.GetFields(~BindingFlags.Default))
                    if (!f.IsStatic && f.GetValue(value) == original)
                        f.SetValue(value, replacement);
                t = t.BaseType;
            }
        }

        public static bool HasFieldWithValue(this object obj, object value)
        {
            var t = obj.GetType();
            while (t != typeof(Object) && t != typeof(object))
            {
                foreach (var f in t.GetFields(~BindingFlags.Default))
                    if (!f.IsStatic && Equals(f.GetValue(obj), value))
                        return true;
                t = t.BaseType;
            }
            return false;
        }
        public static bool HasFieldValueMatch<T>(this object obj, Predicate<T> predicate)
        {
            var t = obj.GetType();
            while (t != typeof(Object) && t != typeof(object))
            {
                foreach (var f in t.GetFields(~BindingFlags.Default))
                    if (!f.IsStatic && f.GetValue(obj) is T v && (predicate == null || predicate(v)))
                        return true;
                t = t.BaseType;
            }
            return false;
        }
        public static FieldInfo[] FindFieldsMatch<T>(this object obj, Predicate<T> predicate)
        {
            var fs = new List<FieldInfo>();
            var t = obj.GetType();
            while (t != typeof(Object) && t != typeof(object))
            {
                foreach (var f in t.GetFields(~BindingFlags.Default))
                    if (!f.IsStatic && typeof(T).IsAssignableFrom(f.FieldType) && (predicate == null || predicate((T)f.GetValue(obj))))
                        fs.Add(f);
                t = t.BaseType;
            }
            return fs.ToArray();
        }

        public static Sprite ToSprite(this Texture2D texture, Rect? rect = null, Vector2? pivot = null)
        {
            var s = Sprite.Create(texture, rect ?? new Rect(0, 0, texture.width, texture.height), pivot ?? new Vector2(0.5f, 0.5f));
            createdObjects.Add(s);
            return s;
        }

        public static Texture2D GetReadable(this Texture2D source, GraphicsFormat targetFormat, bool mipChain = true, Rect? copyArea = null, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
        {
            var t = new Texture2D(
                        (int)(copyArea?.width ?? source.width),
                        (int)(copyArea?.height ?? source.height),
                        targetFormat,
                        mipChain ? TextureCreationFlags.MipChain : TextureCreationFlags.None);
            createdObjects.Add(t);
            source.CopyTo(
                t,
                copyArea,
                format,
                readWrite,
                new Rect(0, 0, t.width, t.height));
            return t;
        }

        public static Texture2D GetReadable(this Texture2D source, TextureFormat? targetFormat = null, bool mipChain = true, Rect? copyArea = null, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
        {
            var t = new Texture2D(
                        (int)(copyArea?.width ?? source.width),
                        (int)(copyArea?.height ?? source.height),
                        targetFormat ?? TextureFormat.ARGB32,
                        mipChain);
            createdObjects.Add(t);
            source.CopyTo(
                t,
                copyArea,
                format,
                readWrite,
                new Rect(0,0,t.width,t.height));
            return t;
        }

        public static Texture2D CopyTo(this Texture2D source, Texture2D texture, Rect? copyArea = null, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, Rect? targetArea = null, FilterMode filterMode = FilterMode.Point, bool apply = true)
        {
            var prev = RenderTexture.active;
            var area = copyArea ?? new Rect(0, 0, source.width, source.height);
            var target = targetArea ?? area;
            var temp = RenderTexture.GetTemporary((int)target.width, (int)target.height, 0, format, readWrite);
            RenderTexture.active = temp;
            Graphics.Blit(source, temp, new Vector2(area.width / source.width, area.height / source.height), new Vector2(area.x / source.width, area.y / source.width));
            temp.filterMode = filterMode;
            texture.ReadPixels(new Rect(0, 0, target.width, target.height), (int)target.x, (int)target.y, false);
            if (apply)
                texture.Apply(true);
            RenderTexture.active = prev;
            RenderTexture.ReleaseTemporary(temp);
            return texture;
        }

        public static Texture2D GetAdjustedReadable(this Texture2D source, TextureFormat? targetFormat = null, bool mipChain = true, Rect? copyArea = null, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
        {
            var area = copyArea ?? new Rect(0, 0, source.width, source.height);
            var nt = new Texture2D(
                    (int)area.width * 10 / 9,
                    (int)area.height,
                    targetFormat ?? TextureFormat.ARGB32,
                    mipChain);
            createdObjects.Add(nt);
            source.CopyTo(nt, area, targetArea: new Rect(0, 0, area.width, area.height), apply: false);
            source.CopyTo(nt, new Rect(area.x, area.y, area.width / 10, area.height), targetArea: new Rect(area.width, 0, nt.width / 10, area.height), filterMode: FilterMode.Bilinear);
            return nt;
        }

        public static Color Overlay(this Color a, Color b)
        {
            if (a.a <= 0)
                return b;
            if (b.a <= 0)
                return a;
            var r = b.a / (b.a + a.a * (1 - b.a));
            float Ratio(float aV, float bV) => bV * r + aV * (1 - r);
            return new Color(Ratio(a.r, b.r), Ratio(a.g, b.g), Ratio(a.b, b.b), b.a + a.a * (1 - b.a));
        }
        public static Vector2 Rotate(this Vector2 value, float angle)
        {
            if (angle % 360 == 0)
                return value;
            var l = value.magnitude;
            var a = Mathf.Atan2(value.x, value.y) + angle / 180 * Mathf.PI;
            return new Vector2(Mathf.Sin(a) * l, Mathf.Cos(a) * l);
        }
        public static float Magnitude(this Vector2 vector, Vector2 relativeDirection) => vector.Rotate(-Vector2.SignedAngle(relativeDirection, Vector2.up)).y;
        public static Vector2 Scale(this Vector2 vector, float scale, Vector2 center = default) => ((vector - center) * scale) + center;
        public static Vector2 Scale(this Vector2 vector, Vector2 scale, Vector2 center = default) => ((vector - center) * scale) + center;
        public static Vector2 ModifyAround(this Vector2 vector, Vector2 center, Func<Vector2, Vector2> function) => function(vector - center) + center;
        public static Vector3 Rotate(this Vector3 vector, Quaternion rotation) => rotation * vector;
        public static Vector3 Rotate(this Vector3 vector, Vector3 euler) => vector.Rotate(Quaternion.Euler(euler));
        public static Vector3 Rotate(this Vector3 vector, float x, float y, float z) => vector.Rotate(Quaternion.Euler(x, y, z));

        public static Vector3 Multiply(this Vector3 value, Vector3 scale) => new Vector3(value.x * scale.x, value.y * scale.y, value.z * scale.z);

        public static void MakeAlwaysReinforced(this Block block)
        {
            var foundation = block as Block_Foundation;
            foundation.meshRenderer = foundation.GetComponentInChildren<MeshRenderer>();
            var meshFilter = foundation.GetComponentInChildren<MeshFilter>();
            Traverse.Create(foundation).Field("meshFilter").SetValue(meshFilter);
            foundation.armoredMesh = meshFilter.sharedMesh;
            Traverse.Create(foundation).Field("defaultMesh").SetValue(meshFilter.sharedMesh);
            Traverse.Create(foundation).Field("defaultMaterial").SetValue(foundation.meshRenderer.sharedMaterial);
            Traverse.Create(foundation).Field("armoredMaterial").SetValue(foundation.meshRenderer.sharedMaterial);
            Traverse.Create(foundation).Field("reinforced").SetValue(true);
        }

        public static void MakeDoorSkinRendered (this Block block)
        {
            var t = block.transform.Find("model");
            var r = t.GetComponent<MeshRenderer>().ReplaceComponent<SkinnedMeshRenderer>();
            var f = t.GetComponent<MeshFilter>();
            var m = Object.Instantiate(f.sharedMesh);
            createdObjects.Add(m);
            r.rootBone = t;
            r.bones = new[] { t, t.Find("door right"), t.Find("door left") };
            var binds = new Matrix4x4[r.bones.Length];
            for (int i = 0; i < binds.Length; i++)
                binds[i] = r.bones[i].worldToLocalMatrix * r.rootBone.localToWorldMatrix;
            m.bindposes = binds;
            r.sharedMesh = m;
            Object.DestroyImmediate(f);
        }

        public static void ShowData(this Material material)
        {
            var found = $" - \"{material.name}\" (shader name: \"{material.shader.name}\")";
            for (int i = 0; i < material.shader.GetPropertyCount(); i++)
            {
                string t = material.shader.GetPropertyType(i).ToString();
                var n = material.shader.GetPropertyName(i);
                string value = null;
                switch (material.shader.GetPropertyType(i))
                {
                    case UnityEngine.Rendering.ShaderPropertyType.Texture:
                        var b = material.GetTexture(n);
                        if (b == null)
                            t = "Unknown Texture";
                        else
                        {
                            t = b.GetType().FullName;
                            value = b.name;
                        }
                        break;
                    case UnityEngine.Rendering.ShaderPropertyType.Range:
                        Array a = material.GetColorArray(n);
                        if (a == null)
                            a = material.GetFloatArray(n);
                        if (a == null)
                            a = material.GetMatrixArray(n);
                        if (a == null)
                            a = material.GetVectorArray(n);
                        if (a == null)
                            t = "Unknown Range";
                        else
                        {
                            t = a.GetType().FullName;
                            value = a.GetValue(0).ToString();
                            for (int j = 1; j < a.Length; j++)
                                value += ", " + a.GetValue(j);
                        }
                        break;
                    case UnityEngine.Rendering.ShaderPropertyType.Vector:
                        var c = material.GetVector(n);
                        if (c == null)
                            t = "Unknown Vector";
                        else
                        {
                            t = c.GetType().FullName;
                            value = $"({c.x}, {c.y}, {c.z}, {c.w})";
                        }
                        break;
                    case UnityEngine.Rendering.ShaderPropertyType.Float:
                        value = material.GetFloat(n).ToString();
                        break;
                    case UnityEngine.Rendering.ShaderPropertyType.Color:
                        var v = material.GetColor(n);
                        value = $"({v.r}, {v.g}, {v.b}, {v.a})";
                        break;
                }
                found += $"\nProperty: {n} ({t})\nDescription: {material.shader.GetPropertyDescription(i)}" + (value == null ? "" : $"\nValue: {value}");
            }
            Debug.Log(found);
        }

        public static void ShowValues(this object obj)
        {
            if (obj == null)
            {
                Debug.Log("Value: null");
                return;
            }
            var t = obj.GetType();
            var m = new System.Text.StringBuilder();
            while (t != typeof(object))
            {
                foreach (var f in t.GetFields(~BindingFlags.Default))
                    if (f != null && !f.IsStatic)
                    {
                        m.Append($"\n - Field: {f.FieldType.FullName} {f.DeclaringType.FullName}.{f.Name}");
                        try
                        {
                            var v = f.GetValue(obj);
                            if (v == null)
                                m.Append("\n   Value: null");
                            else
                                m.Append($"\n   Value: {v}\n   Type: {v.GetType()}");
                        } catch (Exception e)
                        {
                            m.Append($"\n   Exception: {e.GetType().FullName}: {e.Message}");
                        }
                    }
                foreach (var p in t.GetProperties(~BindingFlags.Default))
                    if (p != null && p.GetGetMethod() != null && !p.GetGetMethod().IsStatic && p.GetIndexParameters().Length == 0)
                    {
                        m.Append($"\n - Property: {p.GetGetMethod().ReturnType.FullName} {p.DeclaringType.FullName}.{p.Name}");
                        try
                        {
                            var v = p.GetValue(obj);
                            if (v == null)
                                m.Append("\n   Value: null");
                            else
                                m.Append($"\n   Value: {v}\n   Type: {v.GetType()}");
                        } catch (Exception e)
                        {
                            m.Append($"\n   Exception: {e.GetType().FullName}: {e.Message}");
                        }
                    }
                t = t.BaseType;
            }
            Debug.Log(m.ToString());
        }

        public static Axis Opposite(this Axis axis) => axis < Axis.All ? (Axis)(((int)axis + 3) % 6) : axis;
        public static bool IsNegative(this Axis axis) => axis < Axis.All ? axis > Axis.Z : false;
        public static Axis ToPositive(this Axis axis) => axis < Axis.All ? (Axis)((int)axis % 3) : axis;
        public static Axis ToNegative(this Axis axis) => axis < Axis.All ? (Axis)((int)axis % 3 + 3) : axis;
        public static Axis Next(this Axis axis, int steps = 1) => axis < Axis.All ? (Axis)((int)axis / 3 * 3 + (((int)axis + steps) % 3)) : axis;

        public static T Take<T>(this List<T> list)
        {
            var v = list[0];
            list.RemoveAt(0);
            return v;
        }

        public static bool Bit(this int value, int offset) => (value & (1 << offset)) != 0;

        public static Y[] Cast<X,Y>(this X[] array,Func<X,Y> cast)
        {
            var r = new Y[array.Length];
            for (int i = 0; i < array.Length; i++)
                r[i] = cast(array[i]);
            return r;
        }

        public static T[] Cast<T>(this Array array, Func<object, T> cast)
        {
            var r = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                r[i] = cast(array.GetValue(i));
            return r;
        }

        public static T ReplaceComponent<T>(this Component original, int serializationLayers = 0) where T : Component
        {
            var g = original.gameObject;
            var n = g.AddComponent<T>();
            n.CopyFieldsOf(original);
            n.CopyPropertiesOf(original);
            g.ReplaceValues(original, n, serializationLayers);
            Object.DestroyImmediate(original);
            return n;
        }
        public static void ReplaceValues(this Component value, object original, object replacement, int serializableLayers = 0)
        {
            foreach (var c in value.GetComponentsInChildren<Component>(true))
                (c as object).ReplaceValues(original, replacement, serializableLayers);
        }
        public static void ReplaceValues(this GameObject value, object original, object replacement, int serializableLayers = 0)
        {
            foreach (var c in value.GetComponentsInChildren<Component>(true))
                (c as object).ReplaceValues(original, replacement, serializableLayers);
        }

        public static void ReplaceValues(this object value, object original, object replacement, int serializableLayers = 0)
        {
            if (value == null)
                return;
            var t = value.GetType();
            while (t != typeof(Object) && t != typeof(object))
            {
                foreach (var f in t.GetFields(~BindingFlags.Default))
                    if (!f.IsStatic)
                    {
                        if (f.GetValue(value) == original || (f.GetValue(value)?.Equals(original) ?? false))
                            try
                            {
                                f.SetValue(value, replacement);
                            }
                            catch { }
                        else if (f.GetValue(value) is IList)
                        {
                            var l = f.GetValue(value) as IList;
                            for (int i = 0; i < l.Count; i++)
                                if (l[i] == original || (l[i]?.Equals(original) ?? false))
                                    try
                                    {
                                        l[i] = replacement;
                                    }
                                    catch { }

                        }
                        else if (serializableLayers > 0 && (f.GetValue(value)?.GetType()?.IsSerializable ?? false))
                            f.GetValue(value).ReplaceValues(original, replacement, serializableLayers - 1);
                    }
                t = t.BaseType;
            }
        }

        public static T GetWrapped<T>(this IList<T> list, int index) => list[(index % list.Count) + (index < 0 ? list.Count : 0)];

        static FieldInfo _primaryPaintAxis = typeof(ItemInstance_Buildable).GetField("primaryPaintAxis", ~BindingFlags.Default);
        public static void SetPrimaryPaintAxis(this ItemInstance_Buildable iib, Axis value) => _primaryPaintAxis.SetValue(iib, value);
    }
}