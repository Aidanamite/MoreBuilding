using HarmonyLib;
using HMLLibrary;
using RaftModLoader;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using I2.Loc;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using static BlockCreator;
using static MoreBuilding.Main;
using static MoreBuilding.GeneratedMeshes;


namespace MoreBuilding
{
    public class Main : Mod
    {
        class ReInit
        {
            static bool loadedOnce = false;
            public ReInit()
            {
                if (loadedOnce)
                    foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
                        if (!t.ContainsGenericParameters)
                            t.TypeInitializer?.Invoke(null, null);
                loadedOnce = true;
            }
        }
        ReInit ______________ = new ReInit();
        static System.Text.StringBuilder log = null;//new System.Text.StringBuilder();
        public static void Logg(string message)
        {
            log?.AppendLine(message);
        }
        #region ItemCreation
        public static float DiagonalMagnitude = Vector2.one.magnitude;
        public static Vector3 DiagonalScale = new Vector3(DiagonalMagnitude, 1,1);
        public static float DiagonalBlockSize = DiagonalMagnitude * BlockSize;
        public static float DiagonalHalfBlockSize = DiagonalMagnitude * HalfBlockSize;
        static Func<T> ItemByIndex<T>(int index) where T : ItemCreation => () => instance.LookupCreation(index) as T;
        static Func<T> ItemByIndex<T>(Index index) where T : ItemCreation => ItemByIndex<T>((int)index);
        public ItemCreation[] items = new[]
        {
            new ItemCreation() { baseIndex = 548, standardIndexSetup = Index.ScrapMetal_Upgrade },
            new ItemCreation() { baseIndex = 548, standardIndexSetup = Index.SolidMetal_Upgrade },
            new ItemCreation() { baseIndex = 548, standardIndexSetup = Index.Glass_Upgrade },
            new BlockItemCreation() { baseIndex = 382, standardIndexSetup = Index.ScrapMetal_Foundation, mesh = Foundation, additionEdits = x => x.MakeAlwaysReinforced() },
            new BlockItemCreation() { baseIndex = 383, standardIndexSetup = Index.ScrapMetal_TriangleFoundation, mesh = FoundationTriangle, additionEdits = x => x.MakeAlwaysReinforced() },
            new BlockItemCreation() { baseIndex = 387, standardIndexSetup = Index.ScrapMetal_TriangleFoundationMirrored, mesh = FoundationTriangleMirrored, additionEdits = x => x.MakeAlwaysReinforced() },
            new BlockItemCreation() { baseIndex = 384, standardIndexSetup = Index.ScrapMetal_Floor, mesh = Floor },
            new BlockItemCreation() { baseIndex = 385, standardIndexSetup = Index.ScrapMetal_TriangleFloor, mesh = FloorTriangle },
            new BlockItemCreation() { baseIndex = 388, standardIndexSetup = Index.ScrapMetal_TriangleFloorMirrored, mesh = FloorTriangleMirrored },
            new BlockItemCreation() { baseIndex = 409, standardIndexSetup = Index.ScrapMetal_Wall, mesh = new[] { new[] { Wall }, new[] { WallDiagonal } } },
            new BlockItemCreation() { baseIndex = 410, standardIndexSetup = Index.ScrapMetal_WallHalf, mesh = new[] { new[] { WallHalf }, new[] { WallHalfDiagonal } } },
            new BlockItemCreation() { baseIndex = 423, standardIndexSetup = Index.ScrapMetal_WallV, mesh = WallV },
            new BlockItemCreation() { baseIndex = 408, standardIndexSetup = Index.ScrapMetal_WallSlope, mesh = WallSlope },
            new BlockItemCreation() {
                baseIndex = 445, standardIndexSetup = Index.ScrapMetal_WallSlopeInverted, mesh = WallSlopeInverted,
                additionEdits = x => x.transform.Find("model").localPosition = new Vector3(HalfBlockSize, HalfFloorHeight, 0)
            },
            new BlockItemCreation() {
                baseIndex = 386, standardIndexSetup = Index.ScrapMetal_Fence, mesh = new[] { new[] { Fence, FenceConnector, FenceConnector }, new[] { FenceDiagonal, FenceConnector, FenceConnector } },
                additionEdits = x =>
                {
                    var t = x.transform.Find("knobRight");
                    t.localPosition += new Vector3(0, -t.localPosition.y, 0);
                    t = x.transform.Find("knobLeft");
                    t.localPosition += new Vector3(0, -t.localPosition.y, 0);
                    Traverse.Create(x).Field<int>("knobPriority").Value += 5;
                }
            },
            new BlockItemCreation() {
                baseIndex = 407, standardIndexSetup = Index.ScrapMetal_Gate, mesh = new[] { new[] { Gate, Empty, Empty }, new[] { GateDiagonal, Empty, Empty } },
                additionEdits = x => x.MakeDoorSkinRendered()
            },
            new BlockItemCreation() {
                baseIndex = 406, standardIndexSetup = Index.ScrapMetal_Door, mesh = new[] { new[] { GeneratedMeshes.Door, Empty, Empty }, new[] { DoorDiagonal, Empty, Empty } },
                additionEdits = x => x.MakeDoorSkinRendered()
            },
            new BlockItemCreation() { baseIndex = 411, standardIndexSetup = Index.ScrapMetal_Window, mesh = new[] { new[] { Window }, new[] { WindowDiagonal } } },
            new BlockItemCreation() { baseIndex = 493, standardIndexSetup = Index.ScrapMetal_WindowHalf, mesh = new[] { new[] { WindowHalf }, new[] { WindowHalfDiagonal } } },
            new BlockItemCreation() {
                baseIndex = 403, standardIndexSetup = Index.ScrapMetal_RoofStraight,
                mesh = new[] { new[] { Roof }, new[] { RoofDiagonal }, new[] { RoofDiagonalAlt } }, modelRotations = new[]{ null, new[] { Quaternion.identity }, new[] { Quaternion.identity } }
            },
            new BlockItemCreation() { baseIndex = 401, standardIndexSetup = Index.ScrapMetal_RoofCorner, mesh = RoofCorner },
            new BlockItemCreation() { baseIndex = 402, standardIndexSetup = Index.ScrapMetal_RoofCornerInverted, mesh = RoofCornerInverted },
            new BlockItemCreation() { baseIndex = 429, standardIndexSetup = Index.ScrapMetal_RoofV0, mesh = RoofV0 },
            new BlockItemCreation() { baseIndex = 426, standardIndexSetup = Index.ScrapMetal_RoofV1, mesh = RoofV1 },
            new BlockItemCreation() { baseIndex = 432, standardIndexSetup = Index.ScrapMetal_RoofV2I, mesh = RoofV2I },
            new BlockItemCreation() { baseIndex = 500, standardIndexSetup = Index.ScrapMetal_RoofV2L, mesh = RoofV2L },
            new BlockItemCreation() { baseIndex = 490, standardIndexSetup = Index.ScrapMetal_RoofV3, mesh = RoofV3 },
            new BlockItemCreation() { baseIndex = 499, standardIndexSetup = Index.ScrapMetal_RoofV4, mesh = RoofV4 },
            new BlockItemCreation() { baseIndex = 399, standardIndexSetup = Index.ScrapMetal_Pillar, mesh = Pillar },
            new BlockItemCreation() { baseIndex = 400, standardIndexSetup = Index.ScrapMetal_PillarHalf, mesh = PillarHalf },
            new BlockItemCreation() { baseIndex = 543, standardIndexSetup = Index.ScrapMetal_PillarHorizontal, mesh = PillarHorizontal },
            new BlockItemCreation() { baseIndex = 544, standardIndexSetup = Index.ScrapMetal_PillarHorizontalHalf, mesh = PillarHorizontalHalf },
            new BlockItemCreation() { baseIndex = 398, standardIndexSetup = Index.ScrapMetal_Ladder, mesh = Ladder },
            new BlockItemCreation() { baseIndex = 495, standardIndexSetup = Index.ScrapMetal_LadderHalf, mesh = LadderHalf },
            new BlockItemCreation() {
                baseIndex = 143, standardIndexSetup = Index.ScrapMetal_FloorHalf,
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, HalfFloorHeight-FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight, HalfBlockSize),
                        (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                        (0, 0, 1, .1f), (0, 0, 1, .1f),
                        (0, 0, 1, .1f), (0, 0, 1, .1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90)
                    );
                    for (int i = 0; i < 4; i++)
                    {
                        var x = (HalfBlockSize - HalfWallThickness * 1.5f) * ((i % 2 == 0) ? -1 : 1);
                        var z = x * ((i / 2 == 0) ? -1 : 1);
                        builder.AddBox(
                            new Vector3(x - HalfWallThickness, 0, z - HalfWallThickness), new Vector3(x + HalfWallThickness, HalfFloorHeight-FloorThickness, z + HalfWallThickness),
                            null, (0.9f, 0.9f, 1, 1),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                            );
                        for (int j = 0; j < 2; j++)
                        {
                            if (x > 0)
                                builder.AddBox(
                                    new Vector3(-x + HalfWallThickness, (HalfFloorHeight-FloorThickness) * (2 + 5 * j) / 12, z - HalfWallThickness), new Vector3(x - HalfWallThickness, (HalfFloorHeight-FloorThickness) * (6 + 5 * j) / 12, z + HalfWallThickness),
                                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                                    (0, 0.3333333f, 0.9f, 0.6666666f), null,
                                    (0, 0.3333333f, 0.9f, 0.6666666f), null
                                    );
                            if (z > 0)
                                builder.AddBox(
                                    new Vector3(x - HalfWallThickness, (HalfFloorHeight-FloorThickness) * (2 + 5 * j) / 12, -z + HalfWallThickness), new Vector3(x + HalfWallThickness, (HalfFloorHeight-FloorThickness) * (6 + 5 * j) / 12, z - HalfWallThickness),
                                    (0, 0.9f, 1, 1), (0, 0.9f, 1, 1),
                                    null, (0, 0.3333333f, 0.9f, 0.6666666f),
                                    null, (0, 0.3333333f, 0.9f, 0.6666666f),
                                    modifyUV: (a,b) => b.ToPositive() == Axis.Y ? a.Rotate(90) : a
                                    );
                        }
                    }
                    return builder.ToMesh("ScrapMetal_HalfFloor");
                })
            },
            new BlockItemCreation() {
                baseIndex = 397, standardIndexSetup = Index.ScrapMetal_TriangleFloorHalf, modelRotations = new[] { new[] { Quaternion.Euler(0, 180, 0) } },
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, HalfFloorHeight-FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight, HalfBlockSize),
                        (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                        (0, 0, 1, .1f), (0, 0, 1, .1f),
                        (0, 0, 1, .1f), (0, 0, 1, .1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                        generation: ((0, 0, 1, .1f), Axis.NZ, Axis.NX)
                    );
                    for (int i = 0; i < 4; i++)
                    {
                        var x = (HalfBlockSize - HalfWallThickness * 1.5f) * ((i % 2 == 0) ? -1 : 1);
                        var z = x * ((i / 2 == 0) ? -1 : 1);
                        if (z < 0)
                        {
                            if (x < 0)
                                continue;
                            z += HalfWallThickness * 2;
                        }
                        else if (x < 0)
                            x += HalfWallThickness * 2;
                        builder.AddBox(
                            new Vector3(x - HalfWallThickness, 0, z - HalfWallThickness), new Vector3(x + HalfWallThickness, HalfFloorHeight-FloorThickness, z + HalfWallThickness),
                            null, (0.9f, 0.9f, 1, 1),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                            );
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        builder.AddBox(
                            new Vector3(HalfWallThickness * 4.5f - HalfBlockSize, (HalfFloorHeight-FloorThickness) * (2 + 5 * j) / 12, HalfBlockSize - HalfWallThickness * 2.5f), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, (HalfFloorHeight-FloorThickness) * (6 + 5 * j) / 12, HalfBlockSize - HalfWallThickness * 0.5f),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                            (0, 0.3333333f, 0.9f, 0.6666666f), null,
                            (0, 0.3333333f, 0.9f, 0.6666666f), null
                            );
                        builder.AddBox(
                            new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, (HalfFloorHeight-FloorThickness) * (2 + 5 * j) / 12, HalfWallThickness * 4.5f - HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 0.5f, (HalfFloorHeight-FloorThickness) * (6 + 5 * j) / 12, HalfBlockSize - HalfWallThickness * 2.5f),
                            (0, 0.9f, 1, 1), (0, 0.9f, 1, 1),
                            null, (0, 0.3333333f, 0.9f, 0.6666666f),
                            null, (0, 0.3333333f, 0.9f, 0.6666666f),
                            modifyUV: (a,b) => b.ToPositive() == Axis.Y ? a.Rotate(90) : a
                            );
                        builder.AddBox(
                            new Vector3((HalfWallThickness * 2.5f - HalfBlockSize) * DiagonalMagnitude, (HalfFloorHeight-FloorThickness) * (2 + 5 * j) / 12 + 0.001f, HalfWallThickness * 0.5f), new Vector3((HalfBlockSize - HalfWallThickness * 2.5f) * DiagonalMagnitude, (HalfFloorHeight-FloorThickness) * (6 + 5 * j) / 12 - 0.001f, HalfWallThickness * 2.5f),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                            (0, 0.3333333f, 0.9f, 0.6666666f, 2, 1), null,
                            (0, 0.3333333f, 0.9f, 0.6666666f, 2, 1), null,
                            modifyVert: x => Quaternion.Euler(0,45,0) * x
                            );
                    }
                    return builder.ToMesh("ScrapMetal_TriangularHalfFloor");
                })
            },
            new BlockItemCreation() {
                baseIndex = 465, standardIndexSetup = Index.ScrapMetal_TriangleFloorHalfMirrored, modelRotations = new[] { new[] { Quaternion.Euler(0, 180, 0) } },
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, HalfFloorHeight-FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight, HalfBlockSize),
                        (0, 0, 1, 0.9f), (0, 0, 1, 0.9f),
                        (0, 0, 1, .1f), (0, 0, 1, .1f),
                        (0, 0, 1, .1f), (0, 0, 1, .1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                        generation: ((0, 0, 1, .1f), Axis.NZ, Axis.NX)
                    );
                    for (int i = 0; i < 4; i++)
                    {
                        var x = (HalfBlockSize - HalfWallThickness * 1.5f) * ((i % 2 == 0) ? -1 : 1);
                        var z = x * ((i / 2 == 0) ? -1 : 1);
                        if (z < 0)
                        {
                            if (x < 0)
                                continue;
                            z += HalfWallThickness * 2;
                        }
                        else if (x < 0)
                            x += HalfWallThickness * 2;
                        builder.AddBox(
                            new Vector3(x - HalfWallThickness, 0, z - HalfWallThickness), new Vector3(x + HalfWallThickness, HalfFloorHeight-FloorThickness, z + HalfWallThickness),
                            null, (0.9f, 0.9f, 1, 1),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1)
                            );
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        builder.AddBox(
                            new Vector3(HalfWallThickness * 4.5f - HalfBlockSize, (HalfFloorHeight-FloorThickness) * (2 + 5 * j) / 12, HalfBlockSize - HalfWallThickness * 2.5f), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, (HalfFloorHeight-FloorThickness) * (6 + 5 * j) / 12, HalfBlockSize - HalfWallThickness * 0.5f),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                            (0, 0.3333333f, 0.9f, 0.6666666f), null,
                            (0, 0.3333333f, 0.9f, 0.6666666f), null
                            );
                        builder.AddBox(
                            new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, (HalfFloorHeight-FloorThickness) * (2 + 5 * j) / 12, HalfWallThickness * 4.5f - HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 0.5f, (HalfFloorHeight-FloorThickness) * (6 + 5 * j) / 12, HalfBlockSize - HalfWallThickness * 2.5f),
                            (0, 0.9f, 1, 1), (0, 0.9f, 1, 1),
                            null, (0, 0.3333333f, 0.9f, 0.6666666f),
                            null, (0, 0.3333333f, 0.9f, 0.6666666f),
                            modifyUV: (a,b) => b.ToPositive() == Axis.Y ? a.Rotate(90) : a
                            );
                        builder.AddBox(
                            new Vector3((HalfWallThickness * 2.5f - HalfBlockSize) * DiagonalMagnitude, (HalfFloorHeight-FloorThickness) * (2 + 5 * j) / 12 + 0.001f, HalfWallThickness * 0.5f), new Vector3((HalfBlockSize - HalfWallThickness * 2.5f) * DiagonalMagnitude, (HalfFloorHeight-FloorThickness) * (6 + 5 * j) / 12 - 0.001f, HalfWallThickness * 2.5f),
                            (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                            (0, 0.3333333f, 0.9f, 0.6666666f, 2, 1), null,
                            (0, 0.3333333f, 0.9f, 0.6666666f, 2, 1), null,
                            modifyVert: x => Quaternion.Euler(0,45,0) * x
                            );
                    }
                    return builder.ToMesh("ScrapMetal_TriangularHalfFloorMirrored");
                })
            },
            new BlockItemCreation() {
                baseIndex = 404, standardIndexSetup = Index.ScrapMetal_Stair,
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, WallOffset, -HalfBlockSize), new Vector3(HalfWallThickness * 5 - HalfBlockSize, FullFloorHeight + WallOffset, HalfBlockSize + BlockSize),
                        (0, 1.9f, 2, 2), (0, 1.9f, 2, 2),
                        (1.9f, 0, 2, 2), (1, 0, 1.9f, 1, 2, 2),
                        (1.9f, 0, 2, 2), (1, 0, 1.9f, 1, 2, 2),
                        modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                        generation: ((1.9f, 0, 2, 2),Axis.NZ,Axis.Y)
                        );
                    builder.AddBox(
                        new Vector3(HalfBlockSize - HalfWallThickness * 5, WallOffset, -HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, FullFloorHeight + WallOffset, HalfBlockSize + BlockSize),
                        (0, 1.9f, 2, 2), (0, 1.9f, 2, 2),
                        (1.9f, 0, 2, 2), (1, 0, 1.9f, 1, 2, 2),
                        (1.9f, 0, 2, 2), (1, 0, 1.9f, 1, 2, 2),
                        modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                        generation: ((1.9f, 0, 2, 2),Axis.NZ,Axis.Y)
                        );
                    builder.AddBox(
                        new Vector3(HalfWallThickness * 2.4f - HalfBlockSize, WallOffset, -HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 2.4f, 0, HalfBlockSize + BlockSize),
                        (0, 1, 4, 1.9f), (0, 1, 4, 1.9f),
                        (0, 0, 1, .1f), (0, 0, 4, .1f),
                        (0, 0, 1, .1f), (0, 0, 4, .1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                        modifyVert: x => new Vector3(x.x, x.y + (x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z)
                        );
                    for (int i = 0; i < 12; i++)
                        builder.AddBox(
                            new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, FullFloorHeight * (i + 0.8f) / 12, -HalfBlockSize + BlockSize * i / 6), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, FullFloorHeight * (i + 1) / 12, -HalfBlockSize + BlockSize * (i + 1) / 6 - 0.0001f),
                            (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2),
                            (0, 1, 1, 1.1f), (0, 1, 1, 1.1f),
                            (0, 1, 1, 1.1f), (0, 1, 1, 1.1f),
                            modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90)
                            );
                    return builder.ToMesh("ScrapMetal_Stairs");
                })
            },
            new BlockItemCreation() {
                baseIndex = 405, standardIndexSetup = Index.ScrapMetal_StairHalf,
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, WallOffset, -HalfBlockSize), new Vector3(HalfWallThickness * 5 - HalfBlockSize, HalfFloorHeight + WallOffset, HalfBlockSize),
                        (0, 1.9f, 1, 2), (0, 1.9f, 1, 2),
                        (1.9f, 0, 2, 1), (1, 0, 1.9f, 1),
                        (1.9f, 0, 2, 1), (1, 0, 1.9f, 1),
                        modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                        generation: ((1.9f, 0, 2, 2),Axis.NZ,Axis.Y)
                        );
                    builder.AddBox(
                        new Vector3(HalfBlockSize - HalfWallThickness * 5, WallOffset, -HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, HalfFloorHeight + WallOffset, HalfBlockSize),
                        (0, 1.9f, 1, 2), (0, 1.9f, 1, 2),
                        (1.9f, 0, 2, 1), (1, 0, 1.9f, 1),
                        (1.9f, 0, 2, 1), (1, 0, 1.9f, 1),
                        modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                        generation: ((1.9f, 0, 2, 2),Axis.NZ,Axis.Y)
                        );
                    builder.AddBox(
                        new Vector3(HalfWallThickness * 2.4f - HalfBlockSize, WallOffset, -HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 2.4f, 0, HalfBlockSize),
                        (0, 1, 2, 1.9f), (0, 1, 2, 1.9f),
                        (0, 0, 1, .1f), (0, 0, 2, .1f),
                        (0, 0, 1, .1f), (0, 0, 2, .1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                        modifyVert: x => new Vector3(x.x, x.y + (x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z)
                        );
                    for (int i = 0; i < 6; i++)
                        builder.AddBox(
                            new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, FullFloorHeight * (i + 0.8f) / 12, -HalfBlockSize + BlockSize * i / 6), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, FullFloorHeight * (i + 1) / 12, -HalfBlockSize + BlockSize * (i + 1) / 6 - 0.0001f),
                            (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2), (0.3333333f, 0, 0.6666666f, 0.9f, 1, 2),
                            (0, 1, 1, 1.1f), (0, 1, 1, 1.1f),
                            (0, 1, 1, 1.1f), (0, 1, 1, 1.1f),
                            modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90)
                            );
                    return builder.ToMesh("ScrapMetal_HalfStairs");
                })
            },
            /* Solid Metal Blocks */
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Foundation), x => { x.standardIndexSetup = Index.SolidMetal_Foundation; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_TriangleFoundation), x => { x.standardIndexSetup = Index.SolidMetal_TriangleFoundation; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_TriangleFoundationMirrored), x => { x.standardIndexSetup = Index.SolidMetal_TriangleFoundationMirrored; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Floor), x => { x.standardIndexSetup = Index.SolidMetal_Floor; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_TriangleFloor), x => { x.standardIndexSetup = Index.SolidMetal_TriangleFloor; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_TriangleFloorMirrored), x => { x.standardIndexSetup = Index.SolidMetal_TriangleFloorMirrored; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Wall), x => { x.standardIndexSetup = Index.SolidMetal_Wall; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_WallHalf), x => { x.standardIndexSetup = Index.SolidMetal_WallHalf; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_WallV), x => { x.standardIndexSetup = Index.SolidMetal_WallV; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_WallSlope), x => { x.standardIndexSetup = Index.SolidMetal_WallSlope; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_WallSlopeInverted), x => { x.standardIndexSetup = Index.SolidMetal_WallSlopeInverted; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Fence), x => { x.standardIndexSetup = Index.SolidMetal_Fence; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Gate), x => { x.standardIndexSetup = Index.SolidMetal_Gate; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Door), x => { x.standardIndexSetup = Index.SolidMetal_Door; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Window), x => { x.standardIndexSetup = Index.SolidMetal_Window; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_WindowHalf), x => { x.standardIndexSetup = Index.SolidMetal_WindowHalf; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofStraight), x => { x.standardIndexSetup = Index.SolidMetal_RoofStraight; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofCorner), x => { x.standardIndexSetup = Index.SolidMetal_RoofCorner; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofCornerInverted), x => { x.standardIndexSetup = Index.SolidMetal_RoofCornerInverted; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofV0), x => { x.standardIndexSetup = Index.SolidMetal_RoofV0; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofV1), x => { x.standardIndexSetup = Index.SolidMetal_RoofV1; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofV2I), x => { x.standardIndexSetup = Index.SolidMetal_RoofV2I; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofV2L), x => { x.standardIndexSetup = Index.SolidMetal_RoofV2L; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofV3), x => { x.standardIndexSetup = Index.SolidMetal_RoofV3; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_RoofV4), x => { x.standardIndexSetup = Index.SolidMetal_RoofV4; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Pillar), x => { x.standardIndexSetup = Index.SolidMetal_Pillar; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_PillarHalf), x => { x.standardIndexSetup = Index.SolidMetal_PillarHalf; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_PillarHorizontal), x => { x.standardIndexSetup = Index.SolidMetal_PillarHorizontal; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_PillarHorizontalHalf), x => { x.standardIndexSetup = Index.SolidMetal_PillarHorizontalHalf; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_Ladder), x => { x.standardIndexSetup = Index.SolidMetal_Ladder; }),
            new MimicItemCreation<BlockItemCreation>(ItemByIndex<BlockItemCreation>(Index.ScrapMetal_LadderHalf), x => { x.standardIndexSetup = Index.SolidMetal_LadderHalf; }),
            new BlockItemCreation() {
                baseIndex = 396, standardIndexSetup = Index.SolidMetal_FloorHalf,
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, HalfFloorHeight - FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight, HalfBlockSize),
                        (0, 0, 0.9f, 1), null,
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90)
                        );
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight - FloorThickness, HalfBlockSize),
                        null, null,
                        (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                        (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1)
                        );
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, 0, -HalfBlockSize), new Vector3(HalfBlockSize, FloorThickness, HalfBlockSize),
                        null, (0, 0, 0.9f, 1),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90)
                        );
                    return builder.ToMesh("SolidMetal_HalfFloor");
                })
            },
            new BlockItemCreation() {
                baseIndex = 397, standardIndexSetup = Index.SolidMetal_TriangleFloorHalf, modelRotations = new[] { new[] { Quaternion.Euler(0, 90, 0) } },
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, HalfFloorHeight - FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight, HalfBlockSize),
                        (0, 0, 0.9f, 1), null,
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                        generation: ((0, 0, 1, 0.1f), Axis.Z, Axis.NX)
                        );
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight - FloorThickness, HalfBlockSize),
                        null, null,
                        (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                        (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                        generation: ((0, 0.3333333f, 0.9f, 1, 2, 1), Axis.Z, Axis.NX)
                        );
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, 0, -HalfBlockSize), new Vector3(HalfBlockSize, FloorThickness, HalfBlockSize),
                        null, (0, 0, 0.9f, 1),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                        generation: ((0, 0, 1, 0.1f), Axis.Z, Axis.NX)
                        );
                    return builder.ToMesh("SolidMetal_TriangularHalfFloor");
                })
            },
            new BlockItemCreation() {
                baseIndex = 465, standardIndexSetup = Index.SolidMetal_TriangleFloorHalfMirrored, modelRotations = new[] { new[] { Quaternion.identity } },
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, HalfFloorHeight - FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight, HalfBlockSize),
                        (0, 0, 0.9f, 1), null,
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                        generation: ((0, 0, 1, 0.1f), Axis.Z, Axis.X)
                        );
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, FloorThickness, -HalfBlockSize), new Vector3(HalfBlockSize, HalfFloorHeight - FloorThickness, HalfBlockSize),
                        null, null,
                        (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                        (0, 0.3333333f, 0.9f, 1), (0, 0.3333333f, 0.9f, 1),
                        generation: ((0, 0.3333333f, 0.9f, 1, 2, 1), Axis.Z, Axis.X)
                        );
                    builder.AddBox(
                        new Vector3(-HalfBlockSize, 0, -HalfBlockSize), new Vector3(HalfBlockSize, FloorThickness, HalfBlockSize),
                        null, (0, 0, 0.9f, 1),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        (0, 0, 1, 0.1f), (0, 0, 1, 0.1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x : x.Rotate(-90),
                        generation: ((0, 0, 1, 0.1f), Axis.Z, Axis.X)
                        );
                    return builder.ToMesh("SolidMetal_TriangularHalfFloorMirrored");
                })
            },
            new BlockItemCreation() {
                baseIndex = 404, standardIndexSetup = Index.SolidMetal_Stair,
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, WallOffset, -HalfBlockSize), new Vector3(HalfWallThickness * 5 - HalfBlockSize, FullFloorHeight + WallOffset, HalfBlockSize + BlockSize),
                        (0, 1.9f, 2, 2), (0, 1.9f, 2, 2),
                        (1.9f, 0, 2, 2), (1, 0, 1.9f, 1, 2, 2),
                        (1.9f, 0, 2, 2), (1, 0, 1.9f, 1, 2, 2),
                        modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                        generation: ((1.9f, 0, 2, 2),Axis.NZ,Axis.Y)
                        );
                    builder.AddBox(
                        new Vector3(HalfBlockSize - HalfWallThickness * 5, WallOffset, -HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, FullFloorHeight + WallOffset, HalfBlockSize + BlockSize),
                        (0, 1.9f, 2, 2), (0, 1.9f, 2, 2),
                        (1.9f, 0, 2, 2), (1, 0, 1.9f, 1, 2, 2),
                        (1.9f, 0, 2, 2), (1, 0, 1.9f, 1, 2, 2),
                        modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                        generation: ((1.9f, 0, 2, 2),Axis.NZ,Axis.Y)
                        );
                    builder.AddBox(
                        new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, WallOffset, -HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, 0, HalfBlockSize + BlockSize),
                        null, (0, 1, 4, 1.9f),
                        (0, 0, 1, .1f), (0, 0, 4, .1f),
                        (0, 0, 1, .1f), (0, 0, 4, .1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                        modifyVert: x => new Vector3(x.x, x.y + (x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z)
                        );
                    for (int i = 0; i < 12; i++)
                        builder.AddBox(
                            new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, FullFloorHeight * i / 12, -HalfBlockSize + BlockSize * i / 6), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, FullFloorHeight * (i + 1) / 12, -HalfBlockSize + BlockSize * (i + 1) / 6 - 0.0001f),
                            (0, 0, 0.3333333f, 0.9f, 1, 2), (0, 0, 0.3333333f, 0.9f, 1, 2),
                            (0, 0, 0.9f, 0.3333333f, 2, 1), (0, 0.3333333f, 0.3f, 0.6666666f),
                            (0, 0, 0.9f, 0.3333333f, 2, 1), (0.6f, 0.3333333f, 0.9f, 0.6666666f),
                            modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                            generation: (null,Axis.NY,Axis.Z)
                            );
                    return builder.ToMesh("SolidMetal_Stairs");
                })
            },
            new BlockItemCreation() {
                baseIndex = 405, standardIndexSetup = Index.SolidMetal_StairHalf,
                mesh = new MeshSource(() => {
                    var builder = new MeshBuilder();
                    builder.AddBox(
                        new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, WallOffset, -HalfBlockSize), new Vector3(HalfWallThickness * 5 - HalfBlockSize, HalfFloorHeight + WallOffset, HalfBlockSize),
                        (0, 1.9f, 1, 2), (0, 1.9f, 1, 2),
                        (1.9f, 0, 2, 1), (1, 0, 1.9f, 1),
                        (1.9f, 0, 2, 1), (1, 0, 1.9f, 1),
                        modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                        generation: ((1.9f, 0, 2, 2),Axis.NZ,Axis.Y)
                        );
                    builder.AddBox(
                        new Vector3(HalfBlockSize - HalfWallThickness * 5, WallOffset, -HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, HalfFloorHeight + WallOffset, HalfBlockSize),
                        (0, 1.9f, 1, 2), (0, 1.9f, 1, 2),
                        (1.9f, 0, 2, 1), (1, 0, 1.9f, 1),
                        (1.9f, 0, 2, 1), (1, 0, 1.9f, 1),
                        modifyUV: (x, y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                        generation: ((1.9f, 0, 2, 2),Axis.NZ,Axis.Y)
                        );
                    builder.AddBox(
                        new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, WallOffset, -HalfBlockSize), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, 0, HalfBlockSize),
                        null, (0, 1, 2, 1.9f),
                        (0, 0, 1, .1f), (0, 0, 2, .1f),
                        (0, 0, 1, .1f), (0, 0, 2, .1f),
                        modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x.Rotate(-90),
                        modifyVert: x => new Vector3(x.x, x.y + (x.z / BlockSize + 0.5f) * HalfFloorHeight, x.z)
                        );
                    for (int i = 0; i < 6; i++)
                        builder.AddBox(
                            new Vector3(HalfWallThickness * 2.5f - HalfBlockSize, FullFloorHeight * i / 12, -HalfBlockSize + BlockSize * i / 6), new Vector3(HalfBlockSize - HalfWallThickness * 2.5f, FullFloorHeight * (i + 1) / 12, -HalfBlockSize + BlockSize * (i + 1) / 6 - 0.0001f),
                            (0, 0, 0.3333333f, 0.9f, 1, 2), (0, 0, 0.3333333f, 0.9f, 1, 2),
                            (0, 0, 0.9f, 0.3333333f, 2, 1), (0, 0.3333333f, 0.3f, 0.6666666f),
                            (0, 0, 0.9f, 0.3333333f, 2, 1), (0.6f, 0.3333333f, 0.9f, 0.6666666f),
                            modifyUV: (x,y) => y.ToPositive() == Axis.Y ? x.Rotate(90) : x,
                            generation: (null,Axis.NY,Axis.Z)
                            );
                    return builder.ToMesh("SolidMetal_HalfStairs");
                })
            },
            /* Glass Blocks */
            new BlockItemCreation() {
                baseIndex = 2, uniqueIndex = 6970, uniqueName = UniqueName.Foundation.ToText(UniqueName.Glass), localization = () => Localization.Foundation.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight / 2,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0.75f,1), -0.05f, 1, 1 ))} },
                upgradeItem = 160550, material = () => instance.Glass,
                additionEdits = x => x.MakeAlwaysReinforced()
            },
            new BlockItemCreation() {
                baseIndex = 189, uniqueIndex = 6971, uniqueName =  UniqueName.TriangleFoundation.ToText(UniqueName.Glass), localization = () => Localization.TriangleFoundation.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight / 2,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,1,0,0), new Vector2(0.75f,1), -0.05f, 1, 1 ), true, Quaternion.Euler(0,180,0))} },
                upgradeItem = 160550, material = () => instance.Glass,
                additionEdits = x => x.MakeAlwaysReinforced()
            },
            new BlockItemCreation() {
                baseIndex = 1, uniqueIndex = 6973, uniqueName =  UniqueName.Floor.ToText(UniqueName.Glass), localization = () => Localization.Floor.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ))} },
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 191, uniqueIndex = 6974, uniqueName = UniqueName.TriangleFloor.ToText(UniqueName.Glass), localization = () => Localization.TriangleFloor.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,1,0,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), true, Quaternion.Euler(0,180,0))} },
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 4, uniqueIndex = 6976, uniqueName = UniqueName.Wall.ToText(UniqueName.Glass), localization = () => Localization.Wall.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,-0.04f),
                    new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,2), 0, 0.9f, 0.1f ))} },
                upgradeItem = 160550, material = () => instance.Glass, modelScales = new[] { Vector3.one, DiagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 144, uniqueIndex = 6977, uniqueName = UniqueName.WallHalf.ToText(UniqueName.Glass), localization = () => Localization.WallHalf.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,0),
                    new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0.04f),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,1), 0, 0.9f, 0.1f ))} },
                upgradeItem = 160550, material = () => instance.Glass, modelScales = new[] { Vector3.one, DiagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 152, uniqueIndex = 6978, uniqueName = UniqueName.WallSlope.ToText(UniqueName.Glass), localization = () => Localization.WallSlope.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.04f,0.05f-HalfFloorHeight),
                    new Vector3(HalfBlockSize,0,0.05f),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90,true, DifferentEnds: true), true, Quaternion.Euler(0,0,180) * Quaternion.Euler(-90,0,0))} },
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 421, uniqueIndex = 6979, uniqueName = UniqueName.WallV.ToText(UniqueName.Glass), localization = () => Localization.WallV.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.02f,0.05f-HalfFloorHeight / 2),
                    new Vector3(0,0.02f,0.05f),
                    new UVData(new Vector4(0.45f,0,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90,true), true, Quaternion.Euler(0,0,180) * Quaternion.Euler(-90,0,0), new FaceChanges() { excludeU = true }),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.02f,0.05f-HalfFloorHeight / 2),
                    new Vector3(0,0.02f,0.05f),
                    new UVData(new Vector4(0.45f,0,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90,true), true, Quaternion.Euler(90,0,0), new FaceChanges() { excludeD = true }),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.02f,0.05f-HalfFloorHeight / 2),
                    new Vector3(0,0.02f,0.05f),
                    new UVData(new Vector4(0.45f,0,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90,true), true, Quaternion.Euler(0,0,180) * Quaternion.Euler(-90,0,0), new FaceChanges() { excludeN = true, excludeE = true, excludeS = true, excludeW = true, excludeD = true }),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.02f,0.05f-HalfFloorHeight / 2),
                    new Vector3(0,0.02f,0.05f),
                    new UVData(new Vector4(0.45f,0,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90,true), true, Quaternion.Euler(90,0,0), new FaceChanges() { excludeN = true, excludeE = true, excludeS = true, excludeW = true, excludeU = true })}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 443, uniqueIndex = 6980, uniqueName = UniqueName.WallSlopeInverted.ToText(UniqueName.Glass), localization = () => Localization.WallSlope.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0.006f-HalfBlockSize,0,-0.051f-HalfFloorHeight),
                    new Vector3(HalfBlockSize+0.006f,0.04f,-0.051f),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90, true, true), true, Quaternion.Euler(90,180,0))} },
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 148, uniqueIndex = 6983, uniqueName = UniqueName.RoofStraight.ToText(UniqueName.Glass), localization = () => Localization.RoofStraight.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(HalfBlockSize,0,BlockSize),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ), x => x.z > 0 ? x + Vector3.up * HalfFloorHeight : x)},
                new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,0,0,1), new Vector2(0,0.1f), 0, 1, 1, -90 ), x => Quaternion.Euler(0,135,0) * (x.x == x.z ? x + Vector3.up * HalfFloorHeight : x), true)},
                new[] {new MeshBox(
                    new Vector3(-BlockSize,-0.15f,-BlockSize),
                    new Vector3(0,0,0),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x => Quaternion.Euler(0,-45,0) * (x.x != x.z ? x + Vector3.up * HalfFloorHeight : x), true)}},
                upgradeItem = 160550, material = () => instance.Glass, modelRotations = new[] {null, new[] { Quaternion.identity }, new[] { Quaternion.identity } }
            },
            new BlockItemCreation() {
                baseIndex = 150, uniqueIndex = 6984, uniqueName = UniqueName.RoofCorner.ToText(UniqueName.Glass), localization = () => Localization.RoofCorner.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (x.x < 0 && x.z > 0) ? x + Vector3.up * HalfFloorHeight : x)}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 160, uniqueIndex = 6985, uniqueName = UniqueName.RoofCornerInverted.ToText(UniqueName.Glass), localization = () => Localization.RoofCornerInverted.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((x.x < 0 || x.z > 0) ? x + Vector3.up * HalfFloorHeight : x))}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 430, uniqueIndex = 6986, uniqueName = UniqueName.RoofV2I.ToText(UniqueName.Glass), localization = () => Localization.RoofV2I.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,0,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90 ), x => Mathf.Abs(x.x) < 0.25f ? x + Vector3.up * HalfFloorHeight * 0.5f : x),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0,0.5f,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ), x => Mathf.Abs(x.x) < 0.25f ? x + Vector3.up * HalfFloorHeight * 0.5f : x)}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 502, uniqueIndex = 6987, uniqueName = UniqueName.RoofV2L.ToText(UniqueName.Glass), localization = () => Localization.RoofV2L.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,0.5f,0.45f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.45f,0,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, -90, 0) * ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0,0.5f,0.45f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, -90, 0) * ((x.x == 0 && x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.45f,1,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((x.x == 0 && x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.45f,0,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> (x.x == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.9f,0.5f,0.45f,1), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> (x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x)}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 488, uniqueIndex = 6988, uniqueName = UniqueName.RoofV3.ToText(UniqueName.Glass), localization = () => Localization.RoofV3.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,0.5f,0.45f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((Mathf.Abs(x.x) < 0.25f || Mathf.Abs(x.z) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x)),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.9f,0.5f,0.45f,1), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> (Mathf.Abs(x.x) < 0.25f || Mathf.Abs(x.z) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.45f,0,0,1), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> (x.x == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x)}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 497, uniqueIndex = 6989, uniqueName = UniqueName.RoofV4.ToText(UniqueName.Glass), localization = () => Localization.RoofV4.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,0.5f,0.45f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.45f,0,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, -90, 0) * ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.45f,1,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> Quaternion.Euler(0, 180, 0) * ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.9f,0.5f,0.45f,1), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0,0.5f,0.45f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, -90, 0) * ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.45f,1,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.45f,0,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0,0.5f,0.45f,0), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> Quaternion.Euler(0, 180, 0) * ((x.x == 0 || x.z == 0) ? x + Vector3.up * HalfFloorHeight * 0.5f : x), true)}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 424, uniqueIndex = 6990, uniqueName = UniqueName.RoofV1.ToText(UniqueName.Glass), localization = () => Localization.RoofV1.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.45f,0,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> Quaternion.Euler(0, 90, 0) * ((Mathf.Abs(x.x) < 0.25f && Mathf.Abs(x.z) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x)),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0,0.5f,0.45f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (Mathf.Abs(x.x) < 0.25f && Mathf.Abs(x.z) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.45f,0,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (Mathf.Abs(x.x) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(0,0,0),
                    new UVData(new Vector4(0.45f,0.5f,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (Mathf.Abs(x.x) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x)}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 427, uniqueIndex = 6991, uniqueName = UniqueName.RoofV0.ToText(UniqueName.Glass), localization = () => Localization.RoofV0.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.45f,1,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> Quaternion.Euler(0, 90, 0) * ((Mathf.Abs(x.x) < 0.25f && Mathf.Abs(x.z) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x)),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,0.5f,0.45f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (Mathf.Abs(x.x) < 0.25f && Mathf.Abs(x.z) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,0),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0.45f,0,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90, true ), x=> Quaternion.Euler(0, -90, 0) * ((Mathf.Abs(x.x) < 0.25f && Mathf.Abs(x.z) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x)),
                new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,0),
                    new UVData(new Vector4(0.45f,1,0,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (Mathf.Abs(x.x) < 0.25f && Mathf.Abs(x.z) < 0.25f) ? x + Vector3.up * HalfFloorHeight * 0.5f : x)}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 84, uniqueIndex = 6992, uniqueName = UniqueName.Pillar.ToText(UniqueName.Glass), localization = () => Localization.Pillar.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-0.075f, -0.05f, -0.075f),
                    new Vector3(0.075f,FullFloorHeight-0.05f,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(-0.075f, -0.05f, -0.075f),
                    new Vector3(0.075f,FullFloorHeight-0.05f,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0.9f, 0.1f, 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 146, uniqueIndex = 6993, uniqueName = UniqueName.PillarHalf.ToText(UniqueName.Glass), localization = () => Localization.PillarHalf.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-0.075f, -0.05f, -0.075f),
                    new Vector3(0.075f,HalfFloorHeight-0.05f,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(-0.075f, -0.05f, -0.075f),
                    new Vector3(0.075f,HalfFloorHeight-0.05f,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0.9f, 0.1f, 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 541, uniqueIndex = 6994, uniqueName = UniqueName.PillarHorizontal.ToText(UniqueName.Glass), localization = () => Localization.PillarHorizontal.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize*2,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0, 0.9f, 0.1f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize*2,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0.9f, 0.1f, 0.9f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160550, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 542, uniqueIndex = 6995, uniqueName = UniqueName.PillarHorizontalHalf.ToText(UniqueName.Glass), localization = () => Localization.PillarHorizontalHalf.ToText(Localization.Glass),
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0, 0.9f, 0.1f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0.9f, 0.1f, 0.9f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 88, uniqueIndex = 6996, uniqueName = UniqueName.Door.ToText(UniqueName.Glass), localization = () => Localization.Door.ToText(Localization.Glass),
                mesh = new[] {new[] {
                    new[] {
                        new MeshBox(
                            new Vector3(-HalfBlockSize,FullFloorHeight-0.09f,-0.04f),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0),
                            new UVData(new Vector4(0.9f,1,1,0), new Vector2(2 - (0.04f/HalfFloorHeight),2), 0, 0.9f, 0.1f )),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.04f),
                            new Vector3(0.08f-HalfBlockSize,FullFloorHeight-0.09f,0),
                            new UVData(new Vector4(0.9f,1,1,1 - 0.04f / BlockSize), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.9f - 0.08f / BlockSize * 0.9f, 0.08f / BlockSize * 0.9f, 0.1f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize-0.08f, -0.05f, -0.04f),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.09f,0),
                            new UVData(new Vector4(0.9f,0.04f / BlockSize,1,0), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.08f / BlockSize * 0.9f, 0.9f - 0.16f / BlockSize * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.04f),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.09f,0),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize-0.08f, -0.05f, -0.04f),
                            new Vector3(0.08f-HalfBlockSize,FullFloorHeight-0.09f,0),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true })},
                    new[] {
                        new MeshBox(
                            new Vector3(-0.001f,-0.97f,-0.01f),
                            new Vector3(HalfBlockSize-0.081f,FullFloorHeight-1.01f,0.01f),
                            new UVData(new Vector4(0.9f,0.5f,1,0), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.45f, 0.45f-0.08f/BlockSize * 0.9f, 0.1f + 0.16f/BlockSize * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(-0.001f,-0.97f,-0.01f),
                            new Vector3(HalfBlockSize-0.081f,FullFloorHeight-1.01f,0.01f),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )},
                    new[] {
                        new MeshBox(
                            new Vector3(0.081f-HalfBlockSize,-0.975f,-0.01f),
                            new Vector3(0.001f,FullFloorHeight-1.015f,0.01f),
                            new UVData(new Vector4(0.9f,1,1,0.5f), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.08f/BlockSize * 0.9f, 0.45f-0.08f/BlockSize * 0.9f, 1 ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(0.081f-HalfBlockSize,-0.975f,-0.01f),
                            new Vector3(0.001f,FullFloorHeight-1.015f,0.01f),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )}},
                new[] {
                    new[] {
                        new MeshBox(
                            new Vector3(-HalfBlockSize,FullFloorHeight-0.09f,-0.04f).Multiply(DiagonalScale),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0).Multiply(DiagonalScale),
                            new UVData(new Vector4(0.9f,1,1,0), new Vector2(2 - (0.04f/HalfFloorHeight),2), 0, 0.9f, 0.1f )),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.04f).Multiply(DiagonalScale),
                            new Vector3(0.08f-DiagonalHalfBlockSize,FullFloorHeight-0.09f,0),
                            new UVData(new Vector4(0.9f,1,1,1 - 0.04f / BlockSize), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.9f - 0.08f / BlockSize / DiagonalMagnitude * 0.9f, 0.08f / BlockSize / DiagonalMagnitude * 0.9f, 0.1f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(DiagonalHalfBlockSize-0.08f, -0.05f, -0.04f),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.09f,0).Multiply(DiagonalScale),
                            new UVData(new Vector4(0.9f,0.04f / BlockSize,1,0), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.08f / BlockSize / DiagonalMagnitude * 0.9f, 0.9f - 0.16f / BlockSize / DiagonalMagnitude * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.04f).Multiply(DiagonalScale),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.09f,0).Multiply(DiagonalScale),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(DiagonalHalfBlockSize-0.08f, -0.05f, -0.04f),
                            new Vector3(0.08f-DiagonalHalfBlockSize,FullFloorHeight-0.09f,0),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true })},
                    new[] {
                        new MeshBox(
                            new Vector3(-0.001f,-0.97f,0.01f),
                            new Vector3(DiagonalHalfBlockSize-0.081f,FullFloorHeight-1.01f,0.03f),
                            new UVData(new Vector4(0.9f,0.5f,1,0), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.45f, 0.45f-0.08f/BlockSize / DiagonalMagnitude * 0.9f, 0.1f + 0.16f/BlockSize / DiagonalMagnitude * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(-0.001f,-0.97f,0.01f),
                            new Vector3(DiagonalHalfBlockSize-0.081f,FullFloorHeight-1.01f,0.03f),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )},
                    new[] {
                        new MeshBox(
                            new Vector3(0.081f-DiagonalHalfBlockSize,-0.975f,0.01f),
                            new Vector3(0.001f,FullFloorHeight-1.015f,0.03f),
                            new UVData(new Vector4(0.9f,1,1,0.5f), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.08f/BlockSize / DiagonalMagnitude * 0.9f, 0.45f-0.08f/BlockSize / DiagonalMagnitude * 0.9f, 1 ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(0.081f-DiagonalHalfBlockSize,-0.975f,0.01f),
                            new Vector3(0.001f,FullFloorHeight-1.015f,0.03f),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )}}},
                upgradeItem = 160550, material = () => instance.Glass
            },
        };
        #endregion

        public Material Glass;
        public Material ScrapMetal;
        public Material Metal;
        public LanguageSourceData language;
        public static List<Object> createdObjects = new List<Object>();
        Harmony harmony;
        Transform prefabHolder;
        public static Main instance;
        bool loaded = false;
        public void Awake()
        {
            if (SceneManager.GetActiveScene().name == Raft_Network.GameSceneName && ComponentManager<Raft_Network>.Value.remoteUsers.Count > 1)
            {
                Debug.LogError($"[{name}]: This cannot be loaded while in a multiplayer");
                modlistEntry.modinfo.unloadBtn.GetComponent<Button>().onClick.Invoke();
                return;
            }
            loaded = true;
            instance = this;
            prefabHolder = new GameObject("prefabHolder").transform;
            prefabHolder.gameObject.SetActive(false);
            createdObjects.Add(prefabHolder.gameObject);
            DontDestroyOnLoad(prefabHolder.gameObject);
            language = new LanguageSourceData()
            {
                mDictionary = new Dictionary<string, TermData>(),
                mLanguages = //new List<LanguageData> { new LanguageData() { Code = "en", Name = "English" } }
                Enum.GetValues(typeof(Language)).Cast(x => {
                    var r = ((Language)x).GetText();
                    return new LanguageData() { Code = LocalizationManager.Sources[0].mLanguages.Find(y => y.Name == r).Code, Name = r };
                }).ToList()
            };
            LocalizationManager.Sources.Add(language);

            var assetBundle = AssetBundle.LoadFromMemory(GetEmbeddedFileBytes("glasswalls.assets"));
            createdObjects.Add(assetBundle);
            Glass = Instantiate(assetBundle.LoadAsset<Material>("Glass_Mat"));
            createdObjects.Add(Glass);
            Metal = Instantiate(ItemManager.GetItemByIndex(1).settings_buildable.GetBlockPrefab(0).GetComponentInChildren<MeshRenderer>().material);
            createdObjects.Add(Metal);
            ScrapMetal = Instantiate(Metal);
            createdObjects.Add(ScrapMetal);
            ScrapMetal.SetTexture("_Diffuse", LoadImage("scrapMetal_Diffuse.png"));
            ScrapMetal.SetTexture("_Normal", LoadImage("scrapMetal_Normal.png"));
            {
                var t = LoadImage("scrapMetal_Specular.png");
                var p = t.GetPixels();
                var t2 = new Texture2D(t.width * 2, t.height);
                createdObjects.Add(t2);
                for (int i = 0; i < p.Length; i++)
                    p[i] = new Color(0, 1 - Mathf.Pow(Mathf.Abs(p[i].r * 2 - 1f), 2), 0, 1 - Mathf.Abs(p[i].r * 2 - 1f));
                t2.SetPixels(0,0,t.width,t.height,p);
                for (int i = 0; i < p.Length; i++)
                    p[i] = new Color(p[i].r, p[i].g, 1, p[i].a);
                t2.SetPixels(t.width, 0, t.width, t.height, p);
                t2.Apply();
                ScrapMetal.SetTexture("_MetallicRPaintMaskGSmoothnessA", t2);
                ScrapMetal.SetTextureScale("_MetallicRPaintMaskGSmoothnessA", new Vector2(0.5f,1));
            }
            Metal.SetTexture("_Diffuse", LoadImage("metal.png"));
            Metal.SetTexture("_Normal", LoadImage("metal_Normal.png"));
            {
                var t = LoadImage("metal_Specular.png");
                var p = t.GetPixels();
                var t2 = new Texture2D(t.width * 2, t.height);
                createdObjects.Add(t2);
                for (int i = 0; i < p.Length; i++)
                    p[i] = new Color(0, p[i].r * 0.85f, 0, p[i].r);
                t2.SetPixels(0, 0, t.width, t.height, p);
                for (int i = 0; i < p.Length; i++)
                    p[i] = new Color(p[i].r, p[i].g, 1, p[i].a);
                t2.SetPixels(t.width, 0, t.width, t.height, p);
                t2.Apply();
                Metal.SetTexture("_MetallicRPaintMaskGSmoothnessA", t2);
                Metal.SetTextureScale("_MetallicRPaintMaskGSmoothnessA", new Vector2(0.5f, 1));
            }


            foreach (var item in items)
            {
                var i = item.GetRealInstance();
                var baseItem = ItemManager.GetItemByIndex(i.baseIndex);
                if (baseItem)
                {
                    i.baseItem = baseItem;
                    CreateItem(i);
                }
            }
            (harmony = new Harmony("com.aidanamite.MetalBuilding")).PatchAll();
            ModUtils_ReloadBuildMenu();
            Traverse.Create(typeof(LocalizationManager)).Field("OnLocalizeEvent").GetValue<LocalizationManager.OnLocalizeCallback>().Invoke();
            Log("Mod has been loaded!");
            if (log != null)
                Log(log.ToString());
        }
        public void OnModUnload()
        {
            loaded = false;
            ModUtils_ReloadBuildMenu();
            harmony?.UnpatchAll(harmony.Id);
            LocalizationManager.Sources.Remove(language);
            if (items != null)
            {
                foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockQuadType>())
                    Traverse.Create(q).Field("acceptableBlockTypes").GetValue<List<Item_Base>>().RemoveAll(x => items.Any(y => y.GetRealInstance().uniqueIndex == x.UniqueIndex));
                foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockCollisionMask>())
                    Traverse.Create(q).Field("blockTypesToIgnore").GetValue<List<Item_Base>>().RemoveAll(x => items.Any(y => y.GetRealInstance().uniqueIndex == x.UniqueIndex));
                ItemManager.GetAllItems().RemoveAll(x => items.Any(y => y.GetRealInstance().uniqueIndex == x.UniqueIndex || y.GetRealInstance().uniqueName == x.UniqueName));
                foreach (var b in GetPlacedBlocks())
                    if (b && b.buildableItem && items.Any(y => y.GetRealInstance().uniqueIndex == b.buildableItem.UniqueIndex))
                        RemoveBlock(b, null, false);
            }
            foreach (var o in createdObjects)
                if (o)
                {
                    if (o is AssetBundle)
                        (o as AssetBundle).Unload(true);
                    else
                        Destroy(o);
                }
            createdObjects.Clear();
            lookup.Clear();
            upgradeCheck.Clear();
            Log("Mod has been unloaded!");
        }

        public void CreateItem(ItemCreation item)
        {
            Logg($"[CreateItem] Creating {item.uniqueName}#{item.uniqueIndex}");
            item.item = item.baseItem.Clone(item.uniqueIndex, item.uniqueName);
            if (item.loadIcon)
            {
                var t = LoadImage("icons/" + item.uniqueName + ".png", false);
                if (t)
                    item.item.settings_Inventory.Sprite = t.ToSprite();
                else
                    Logg("Failed to load item icon");
            }
            else
            {
                var t = item.item.settings_Inventory.Sprite.texture.GetReadable(copyArea: item.item.settings_Inventory.Sprite.rect, mipChain: false);
                var p = t.GetPixels();
                for (int i = 0; i < p.Length; i++)
                    p[i] = new Color(p[i].grayscale, p[i].grayscale, p[i].grayscale, p[i].a);
                t.SetPixels(p);
                t.Apply();
                item.item.settings_Inventory.Sprite = t.ToSprite();
            }
            var up = new ItemInstance_Buildable.Upgrade();
            up.CopyFieldsOf(item.baseItem.settings_buildable.Upgrades);
            var fl = up.FindFieldsMatch<Item_Base>(x => !x);
            var fl2 = up.FindFieldsMatch<Item_Base>(x => x);
            foreach (var f in fl)
                foreach (var f2 in fl2)
                    foreach( var f3 in (f2.GetValue(up) as Item_Base).settings_buildable.Upgrades?.FindFieldsMatch<Item_Base>(x => x && x.UniqueIndex == item.baseItem.UniqueIndex) ?? new FieldInfo[0])
                    {
                        f3.SetValue(up, item.baseItem);
                        goto exitLoop;
                    }
            exitLoop: Traverse.Create(item.item.settings_buildable).Field("upgrades").SetValue(up);
            item.item.settings_Inventory.LocalizationTerm = "Item/"+item.item.UniqueName;
            if (item.cost != null)
                item.item.settings_recipe.NewCost = item.cost;
            language.mDictionary[item.item.settings_Inventory.LocalizationTerm] = new TermData() { Languages = Enum.GetValues(typeof(Language)).Cast(x => {
                var l = (Language)x;
                TextAttribute.forcedContext = l;
                var r = item.localization();
                TextAttribute.forcedContext = null;
                return r;
            }) };
            var blockCreation = item as BlockItemCreation;
            if (blockCreation != null)
            {
                Logg($"Block Prefab Creation. Found {item.item.settings_buildable.GetBlockPrefabs().Length} prefabs to modify");
                var p = item.item.settings_buildable.GetBlockPrefabs().ToArray();
                for (int i = 0; i < p.Length; i++)
                {
                    var me = blockCreation.mesh[i];
                    p[i] = Instantiate(p[i], prefabHolder, false);
                    p[i].name = item.item.UniqueName + ((p.Length == 1 || p[i].dpsType == DPS.Default) ? "" : $"_{p[i].dpsType}");
                    var r = p[i].GetComponentsInChildren<Renderer>(true);
                    Logg($"Found {r.Length} renderers on prefab {i}");
                    for (int j = 0; j < r.Length; j++)
                        if (me.Length > j && me[j] != null)
                        {
                            r[j].sharedMaterials = blockCreation.materials[i];
                            if (r[j] is SkinnedMeshRenderer)
                                (r[j] as SkinnedMeshRenderer).sharedMesh = me.GetSafe(j);
                            else
                                r[j].GetComponent<MeshFilter>().sharedMesh = me.GetSafe(j);
                            if (blockCreation.modelRotations?.Length > i && blockCreation.modelRotations[i]?.Length > j)
                                r[j].transform.localRotation = blockCreation.modelRotations[i][j] == default ? Quaternion.identity : blockCreation.modelRotations[i][j];
                        }
                    if (blockCreation.modelScales?.Length > i)
                        r[0].transform.localScale = blockCreation.modelScales[i];
                    p[i].ReplaceValues(item.baseItem, item.item);
                    blockCreation.additionEdits?.Invoke(p[i]);
                    var mirror = ItemManager.GetItemByIndex(blockCreation.mirroredItem);
                    if (mirror)
                    {
                        Traverse.Create(item.item.settings_buildable).Field("mirroredVersion").SetValue(mirror);
                        Traverse.Create(mirror.settings_buildable).Field("mirroredVersion").SetValue(item.item);
                    }
                }
                Traverse.Create(item.item.settings_buildable).Field("blockPrefabs").SetValue(p);
            }
            foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockQuadType>())
                if (q.AcceptsBlock(item.baseItem))
                    Traverse.Create(q).Field("acceptableBlockTypes").GetValue<List<Item_Base>>().Add(item.item);
            foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockCollisionMask>())
                if (q.IgnoresBlock(item.baseItem))
                    Traverse.Create(q).Field("blockTypesToIgnore").GetValue<List<Item_Base>>().Add(item.item);

            if (blockCreation != null && item.cost == null && item.baseItem.settings_recipe.NewCost.Length > 0)
            {
                Item_Base craftingMaterial = StandardItemSetup.GetCraftingMaterial(blockCreation.upgradeItem);
                item.item.SetRecipe(new CostMultiple[] { new CostMultiple(new Item_Base[] { craftingMaterial }, (int)Math.Round(item.baseItem.settings_recipe.NewCost.Sum(x => x.amount) / 2d + item.baseItem.settings_recipe.NewCost[0].amount / 2d)) });
                Logg($"Set Recipe: {item.item.settings_recipe.NewCost.Join(x => $"\n - {x.amount}x[{x.items.Join(y => y.settings_Inventory.DisplayName)}]", "")}");
            }

            ItemManager.GetAllItems().Add(item.item);
            if (item.isUpgrade)
                upgradeCheck[item.uniqueIndex] = (item.item, CreateParticles(StandardItemSetup.GetPrimaryMaterial(StandardItemSetup.GetValues((Index)item.uniqueIndex).material)(), Particle), upgradeCheck.TryGetValue(item.uniqueIndex, out var t) ? t.list : new List<BlockItemCreation>());
            if (blockCreation?.upgradeItem > 0)
                (upgradeCheck.TryGetValue(blockCreation.upgradeItem, out var t2) ? t2 : (upgradeCheck[blockCreation.upgradeItem] = (null, null, new List<BlockItemCreation>()))).list.Add(blockCreation);
            lookup[item.uniqueIndex] = item;
            Logg("[CreateItem] Done");
        }

        public override byte[] GetEmbeddedFileBytes(string path)
        {
            if (modlistEntry.modinfo.modFiles.TryGetValue(path, out var r))
                return r;
            return null;
        }

        Dictionary<(string, bool, bool), Texture2D> loadedImages = new Dictionary<(string, bool, bool), Texture2D>();
        public Texture2D LoadImage(string filename, bool mipChain = true, bool leaveReadable = true)
        {
            if (loadedImages.TryGetValue((filename, mipChain, leaveReadable), out var i) && i)
                return i;
            var b = GetEmbeddedFileBytes(filename);
            if (b == null)
                return null;
            var t = new Texture2D(0, 0, TextureFormat.RGBA32, mipChain);
            t.LoadImage(b, !leaveReadable);
            loadedImages[(filename, mipChain, leaveReadable)] = t;
            createdObjects.Add(t);
            return t;
        }
        public static Texture2D LoadAbsoluteImage(string filename, bool mipChain = true, bool leaveReadable = true)
        {
            var t = new Texture2D(0, 0, TextureFormat.RGBA32, mipChain);
            t.LoadImage(System.IO.File.ReadAllBytes(filename), !leaveReadable);
            if (leaveReadable)
                t.Apply();
            createdObjects.Add(t);
            return t;
        }

        IEnumerable<(Item_Base, Item_Base, bool)> ModUtils_BuildMenuItems()
        {
            if (!loaded) yield break;
            foreach (var i in items)
            {
                var r = i.GetRealInstance();
                if (r.baseItem && r.item)
                    yield return (r.baseItem, r.item, r.isUpgrade);
            }
            yield break;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        void ModUtils_ReloadBuildMenu() { }

        //[ConsoleCommand("loadMetal")]
        public static string Comm(string[] args)
        {
            instance.ScrapMetal.SetTexture("_Normal", LoadAbsoluteImage(args[0]));
            return "Done";
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static Dictionary<string, WeakReference<Item_Base>> lookupIM = new Dictionary<string, WeakReference<Item_Base>>();
        public static Item_Base LookupItemByName(string uniqueName)
        {
            if (lookupIM.TryGetValue(uniqueName, out var r) && r.TryGetTarget(out var i) && i)
                return i;
            i = ItemManager.GetItemByName(uniqueName);
            if (i)
                lookupIM[uniqueName] = new WeakReference<Item_Base>(i);
            return i;
        }

        public Dictionary<int,(Item_Base item, Func<ParticleSystem> breakParticle, List<BlockItemCreation> list)> upgradeCheck = new Dictionary<int, (Item_Base, Func<ParticleSystem>, List<BlockItemCreation>)>();
        Dictionary<int, ItemCreation> lookup = new Dictionary<int, ItemCreation>();
        public ItemCreation LookupCreation(int index)
        {
            if (lookup.TryGetValue(index, out var i))
                return i;
            lookup[index] = i = items.FirstOrDefault(x => x.uniqueIndex == index);
            return i;
        }

        public override bool CanUnload(ref string message)
        {
            if (loaded && SceneManager.GetActiveScene().name == Raft_Network.GameSceneName && ComponentManager<Raft_Network>.Value.remoteUsers.Count > 1)
            {
                message = "Cannot unload while in a multiplayer";
                return false;
            }
            return base.CanUnload(ref message);
        }

        public static Func<ParticleSystem> CreateParticles(Material mat, Mesh mesh)
        {
            ParticleSystem v = null;
            return () => {
                if (v)
                    return v;
                var p = GetBreakParticles();
                if (!p)
                    return null;
                var n = Instantiate(p, p.transform.parent, false);
                var r = n.GetComponent<ParticleSystemRenderer>();
                var a = r.sharedMaterials.ToArray();
                for (int i = 0; i < a.Length; i++)
                    if (a[i])
                        a[i] = mat;
                r.sharedMaterials = a;
                var ma = new Mesh[r.meshCount];
                for (int i = 0; i < ma.Length; i++)
                    ma[i] = mesh;
                r.SetMeshes(ma);
                n.gameObject.SetActive(false); // Deactivation then reactivation is required for mesh to update
                n.gameObject.SetActive(true);
                return v = n;
            };
        }

        static FieldInfo _bp = typeof(BlockCreator).GetField("blockBreakParticlesStatic", ~BindingFlags.Default);
        public static ParticleSystem GetBreakParticles() => (ParticleSystem)_bp.GetValue(null);
    }

    [HarmonyPatch(typeof(ItemInstance_Buildable.Upgrade), "GetNewItemFromUpgradeItem")]
    static class Patch_GetUpgradeItem
    {
        static ConditionalWeakTable<ItemInstance_Buildable.Upgrade, Dictionary<int, WeakReference<Item_Base>>> resultCache = new ConditionalWeakTable<ItemInstance_Buildable.Upgrade, Dictionary<int, WeakReference<Item_Base>>>();
        static void Postfix(ItemInstance_Buildable.Upgrade __instance, Item_Base buildableItem, ref Item_Base __result)
        {
            if (!buildableItem)
                return;
            if (resultCache.TryGetValue(__instance,out var d) && d.TryGetValue(buildableItem.UniqueIndex,out var r))
            {
                if (r.TryGetTarget(out var v))
                {
                    __result = v;
                    return;
                }
                else
                    d.Remove(buildableItem.UniqueIndex);
            }
            if (instance.upgradeCheck.TryGetValue(buildableItem.UniqueIndex, out var l))
            {
                foreach (var p in l.list)
                    if (p.item && p.baseItem?.settings_buildable?.Upgrades == __instance)
                    {
                        __result = p.item;
                        goto result;
                    }
                foreach (var p in l.list)
                    if (p.item && (p.baseItem?.settings_buildable?.Upgrades?.HasFieldValueMatch<Item_Base>(x => x?.settings_buildable?.Upgrades == __instance) ?? false))
                    {
                        __result = p.item;
                        goto result;
                    }
                var b = __instance.GetNewItemFromUpgradeItem(l.item);
                foreach (var p in l.list)
                    if (p.item && (
                        p.baseItem?.settings_buildable?.Upgrades == b.settings_buildable.Upgrades
                        || (p.baseItem?.settings_buildable?.Upgrades?.HasFieldValueMatch<Item_Base>(x => x?.settings_buildable?.Upgrades == b.settings_buildable.Upgrades) ?? false)))
                    {
                        __result = p.item;
                        goto result;
                    }
            }
            return;
        result:
            if (d == null)
                resultCache.Add(__instance, d = new Dictionary<int, WeakReference<Item_Base>>());
            d[buildableItem.UniqueIndex] = new WeakReference<Item_Base>(__result);
            return;
        }
    }

    [HarmonyPatch(typeof(BlockCreator), "IsBuildableItemUpgradeItem")]
    static class Patch_BlockCreator
    {
        static void Postfix(Item_Base buildableItem, ref bool __result)
        {
            if (!__result && buildableItem && instance.upgradeCheck.ContainsKey(buildableItem.UniqueIndex))
                __result = true;
        }
    }

    [HarmonyPatch(typeof(Block), "IsWalkable")]
    static class Patch_Block
    {
        static void Postfix(Block __instance, ref bool __result)
        {
            if (!__instance.buildableItem)
                return;
            var p = instance.LookupCreation(__instance.buildableItem.UniqueIndex);
            if (p?.item)
            {
                __result = p.baseItem.settings_buildable.GetBlockPrefab(0).IsWalkable();
                return;
            }
        }
    }

    [HarmonyPatch(typeof(LanguageSourceData), "GetLanguageIndex")]
    static class Patch_GetLanguageIndex
    {
        static void Postfix(LanguageSourceData __instance, ref int __result)
        {
            if (__result == -1 && __instance == instance.language)
                __result = 0;
        }
    }

    [HarmonyPatch]
    static class Patch_ReplaceBreakParticles
    {
        static MethodBase TargetMethod() => typeof(BlockCreator).GetNestedTypes(~BindingFlags.Default).First(x => x.Name.Contains("<DestroyBlock>")).GetMethod("MoveNext", ~BindingFlags.Default);
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase method)
        {
            var code = instructions.ToList();
            for (int i = code.Count - 1; i >= 0; i--)
                if (code[i].opcode == OpCodes.Ldsfld && code[i].operand is FieldInfo f && f.Name == "blockBreakParticlesStatic")
                    code.InsertRange(i + 1, new[]
                    {
                        new CodeInstruction(OpCodes.Ldarg_0),
                        new CodeInstruction(OpCodes.Ldfld, method.DeclaringType.GetField("block",~BindingFlags.Default)),
                        new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patch_ReplaceBreakParticles),nameof(ReplaceSystem)))
                    });
            return code;
        }
        static ParticleSystem ReplaceSystem(ParticleSystem original, Block block)
        {
            if (block.buildableItem)
            {
                var c = instance.LookupCreation(block.buildableItem.UniqueIndex);
                if (c is BlockItemCreation b && Main.instance.upgradeCheck.TryGetValue(b.upgradeItem, out var t) && t.breakParticle?.Invoke())
                {
                    Debug.Log("Replacing particle for " + block);
                    return t.breakParticle();
                }
            }
            return original;
        }
    }
}