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


namespace MoreBuilding
{
    public class Main : Mod
    {
        #region ItemCreation
        public static float DiagonalMagnitude = Vector2.one.magnitude;
        public static Vector3 DiagonalScale = new Vector3(DiagonalMagnitude, 1,1);
        public static float DiagonalBlockSize = DiagonalMagnitude * BlockSize;
        public static float DiagonalHalfBlockSize = DiagonalMagnitude * HalfBlockSize;
        public ItemCreation[] items = new[]
        {
            new ItemCreation() { baseIndex = 546, loadIcon = true, uniqueIndex = 160546, uniqueName = "Block_Upgrade_ScrapMetal", isUpgrade = true, localization = "Replaced with scrap metal@" },
            new ItemCreation() { baseIndex = 548, loadIcon = true, uniqueIndex = 160548, uniqueName = "Block_Upgrade_Metal", isUpgrade = true, localization = "Replaced with solid metal@" },
            new ItemCreation() { baseIndex = 548, loadIcon = true, uniqueIndex = 160550, uniqueName = "Block_Upgrade_Glass", isUpgrade = true, localization = "Replaced with glass@" },
            new BlockItemCreation() {
                baseIndex = 382, uniqueIndex = 160002, uniqueName = "Block_Foundation_ScrapMetal", localization = "Scrap Metal Foundation@Used to expand your raft on the bottom floor",
                mesh = new[] {new[] { GeneratedMeshes.Foundation } },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                additionEdits = x => x.MakeAlwaysReinforced()
            },
            new BlockItemCreation() {
                baseIndex = 383, uniqueIndex = 160383, uniqueName = "Block_Foundation_Triangular_ScrapMetal", localization = "Triangular Scrap Metal Foundation@Used to expand your raft on the bottom floor",
                mesh = new[]{new[] { GeneratedMeshes.FoundationTriangle } },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                additionEdits = x => x.MakeAlwaysReinforced()
            },
            new BlockItemCreation() {
                baseIndex = 387, uniqueIndex = 160387, uniqueName = "Block_Foundation_Triangular_ScrapMetal_Mirrored", localization = "Triangular Scrap Metal Foundation@Used to expand your raft on the bottom floor",
                mesh = new[]{new[] { GeneratedMeshes.FoundationTriangleMirrored } },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                mirroredItem = 160383,
                additionEdits = x => x.MakeAlwaysReinforced()
            },
            new BlockItemCreation() {
                baseIndex = 384, uniqueIndex = 160384, uniqueName = "Block_Floor_ScrapMetal", localization = "Scrap Metal Floor@Used to build additional floors and roof. Cannot be built in thin air",
                mesh = new[] {new[] { GeneratedMeshes.Floor } },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 385, uniqueIndex = 160385, uniqueName = "Block_Floor_Triangular_ScrapMetal", localization = "Triangular Scrap Metal Floor@Used to build additional floors and roof. Cannot be built in thin air",
                mesh = new[] {new[] { GeneratedMeshes.FloorTriangle } },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 388, uniqueIndex = 160388, uniqueName = "Block_Floor_Triangular_ScrapMetal_Mirrored", localization = "Triangular Scrap Metal Floor@Used to build additional floors and roof. Cannot be built in thin air",
                mesh = new[] {new[] { GeneratedMeshes.FloorTriangleMirrored } },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                mirroredItem = 160385
            },
            new BlockItemCreation() {
                baseIndex = 409, uniqueIndex = 160409, uniqueName = "Block_Wall_ScrapMetal", localization = "Scrap Metal Wall@Provides support for additional floors",
                mesh = new[] {new[] { GeneratedMeshes.Wall }, new[] { GeneratedMeshes.WallDiagonal } },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 410, uniqueIndex = 160410, uniqueName = "Block_HalfWall_ScrapMetal", localization = "Half Scrap Metal Wall@Provides support for additional floors",
                mesh = new[] {new[] { GeneratedMeshes.WallHalf }, new[] { GeneratedMeshes.WallHalfDiagonal } },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 423, uniqueIndex = 160421, uniqueName = "Block_Wall_VSlope_ScrapMetal", localization = "Pyramid Scrap Metal Wall@Fills pyramid holes",
                mesh = new[] {new[] { GeneratedMeshes.WallV } },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 408, uniqueIndex = 160408, uniqueName = "Block_Wall_Slope_ScrapMetal", localization = "Triangular Scrap Metal Wall@Fills triangular holes",
                mesh = new[]{new[] { GeneratedMeshes.WallSlope } },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 445, uniqueIndex = 160443, uniqueName = "Block_Wall_Slope_ScrapMetal_Inverted", localization = "Triangular Scrap Metal Wall@Fills triangular holes",
                mesh = new[]{new[] { GeneratedMeshes.WallSlopeInverted } },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                additionEdits = x => x.transform.Find("model").localPosition = new Vector3(HalfBlockSize,HalfFloorHeight,0)
            },
            new BlockItemCreation() {
                baseIndex = 386, uniqueIndex = 160085, uniqueName = "Block_Wall_Fence_ScrapMetal", localization = "Scrap Metal Fence@Keeps you from falling of your raft",
                mesh = new[] {new[] { GeneratedMeshes.Fence, GeneratedMeshes.FenceConnector, GeneratedMeshes.FenceConnector }, new[] { GeneratedMeshes.FenceDiagonal, GeneratedMeshes.FenceConnector, GeneratedMeshes.FenceConnector } },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
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
                baseIndex = 407, uniqueIndex = 160255, uniqueName = "Block_Wall_Gate_ScrapMetal", localization = "Scrap Metal Gate@Provides support for additional floors",
                mesh = new[] {
                    new[] { GeneratedMeshes.Gate, GeneratedMeshes.Empty, GeneratedMeshes.Empty },
                    new[] { GeneratedMeshes.GateDiagonal, GeneratedMeshes.Empty, GeneratedMeshes.Empty }
                },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                additionEdits = x => x.MakeDoorSkinRendered()
            },
            new BlockItemCreation() {
                baseIndex = 406, uniqueIndex = 160088, uniqueName = "Block_Wall_Door_ScrapMetal", localization = "Scrap Metal Door@Provides support for additional floors",
                mesh = new[] {
                    new[] { GeneratedMeshes.Door, GeneratedMeshes.Empty, GeneratedMeshes.Empty },
                    new[] { GeneratedMeshes.DoorDiagonal, GeneratedMeshes.Empty, GeneratedMeshes.Empty }
                },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                additionEdits = x => x.MakeDoorSkinRendered()
            },
            new BlockItemCreation() {
                baseIndex = 411, uniqueIndex = 160411, uniqueName = "Block_Wall_Window_ScrapMetal", localization = "Scrap Metal Window@Provides support for additional floors",
                mesh = new[] {new[] { GeneratedMeshes.Window }, new[] { GeneratedMeshes.WindowDiagonal } },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 493, uniqueIndex = 160493, uniqueName = "Block_Wall_Window_ScrapMetal_Half", localization = "Half Scrap Metal Window@Provides support for additional floors",
                mesh = new[] {new[] { GeneratedMeshes.WindowHalf }, new[] { GeneratedMeshes.WindowHalfDiagonal } },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 3, uniqueIndex = 160003, uniqueName = "Block_Stair_ScrapMetal", localization = "Scrap Metal Stair@Great to get from one floor to another",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfFloorHeight,0.15f-HalfBlockSize,-HalfBlockSize),
                    new Vector3(0,0.35f-HalfBlockSize,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90, DifferentEnds: true ), true, Quaternion.Euler(0,0,-90)),
                new MeshBox(
                    new Vector3(-HalfFloorHeight,HalfBlockSize-0.35f,-HalfBlockSize),
                    new Vector3(0,HalfBlockSize-0.15f,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90, DifferentEnds: true ), true, Quaternion.Euler(0,0,-90)),
                new MeshBox(
                    new Vector3(0.24f-HalfBlockSize,0,HalfBlockSize*3-0.24f),
                    new Vector3(0.36f-HalfBlockSize,FullFloorHeight-0.2f,HalfBlockSize*3-0.12f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(0.24f-HalfBlockSize,0,HalfBlockSize*3-0.24f),
                    new Vector3(0.36f-HalfBlockSize,FullFloorHeight-0.2f,HalfBlockSize*3-0.12f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0.9f, 0.1f, 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.36f,0,HalfBlockSize*3-0.24f),
                    new Vector3(HalfBlockSize-0.24f,FullFloorHeight-0.2f,HalfBlockSize*3-0.12f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.36f,0,HalfBlockSize*3-0.24f),
                    new Vector3(HalfBlockSize-0.24f,FullFloorHeight-0.2f,HalfBlockSize*3-0.12f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0.9f, 0.1f, 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.19f,0,-HalfBlockSize),
                    new Vector3(HalfBlockSize-0.14f,0.3f,HalfBlockSize*3),
                    new UVData(new Vector4(0.9f,0,1,1), new Vector2(0,0.1f), -0.025f, 0.05f, 1.95f, -90 ), x=> x.z > 0 ? x + (Vector3.up * (FullFloorHeight - 0.3f)) : x),
                new MeshBox(
                    new Vector3(0.14f-HalfBlockSize,0,-HalfBlockSize),
                    new Vector3(0.19f-HalfBlockSize,0.3f,HalfBlockSize*3),
                    new UVData(new Vector4(0.9f,0,1,1), new Vector2(0,0.1f), -0.025f, 0.05f, 1.95f, -90 ), x=> x.z > 0 ? x + (Vector3.up * (FullFloorHeight - 0.3f)) : x)}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 84, uniqueIndex = 160084, uniqueName = "Block_Pillar_ScrapMetal", localization = "Scrap Metal Pillar@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-0.075f, -0.05f, -0.075f),
                    new Vector3(0.075f,FullFloorHeight-0.05f,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(-0.075f, -0.05f, -0.075f),
                    new Vector3(0.075f,FullFloorHeight-0.05f,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0.9f, 0.1f, 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 143, uniqueIndex = 160143, uniqueName = "Block_HalfFloor_ScrapMetal", localization = "Scrap Metal Raised Floor@A floor half the height of a wall",
                mesh = new[] {new[] {
                    new MeshBox(
                        new Vector3(-HalfBlockSize,HalfFloorHeight-0.15f,-HalfBlockSize),
                        new Vector3(HalfBlockSize,HalfFloorHeight,HalfBlockSize),
                        new UVData(new Vector4(0,0,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90 )),
                    new MeshBox(
                        new Vector3(-HalfBlockSize*1.3f,0,-0.05f),
                        new Vector3(HalfBlockSize*1.3f,HalfFloorHeight-0.15f,0.05f),
                        new UVData(new Vector4(1,0,0.9f,1), new Vector2(0,0.4f), 0, 0.9f, 0.1f ), Rotation: Quaternion.Euler(0,45,0), Faces: new FaceChanges() { excludeU = true }),
                    new MeshBox(
                        new Vector3(-HalfBlockSize*1.3f,0,-0.05f),
                        new Vector3(HalfBlockSize*1.3f,HalfFloorHeight-0.15f,0.05f),
                        new UVData(new Vector4(1,0,0.9f,1), new Vector2(0,0.4f), 0, 0.9f, 0.1f ), Rotation: Quaternion.Euler(0,-45,0), Faces: new FaceChanges() { excludeU = true })} },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 146, uniqueIndex = 160146, uniqueName = "Block_HalfPillar_ScrapMetal", localization = "Half Scrap Metal Pillar@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-0.075f, -0.05f, -0.075f),
                    new Vector3(0.075f,HalfFloorHeight-0.05f,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(-0.075f, -0.05f, -0.075f),
                    new Vector3(0.075f,HalfFloorHeight-0.05f,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0.9f, 0.1f, 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 148, uniqueIndex = 160148, uniqueName = "Block_Roof_Straight_ScrapMetal", localization = "Scrap Metal Roof@Covers your head",
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
                upgradeItem = 160546, material = () => instance.ScrapMetal, resetModelRotations = new[]{null, new[] { true }, new[] { true } }
            },
            new BlockItemCreation() {
                baseIndex = 150, uniqueIndex = 160150, uniqueName = "Block_Roof_Corner_ScrapMetal", localization = "Scrap Metal Roof Corner@Covers your head",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (x.x < 0 && x.z > 0) ? x + Vector3.up * HalfFloorHeight : x)}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 160, uniqueIndex = 160160, uniqueName = "Block_Roof_InvCorner_ScrapMetal", localization = "Inverted Scrap Metal Roof Corner@Covers your head",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((x.x < 0 || x.z > 0) ? x + Vector3.up * HalfFloorHeight : x))}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 193, uniqueIndex = 160193, uniqueName = "Block_HalfFloor_Triangular_ScrapMetal", localization = "Triangular Scrap Metal Raised Floor@A floor half the height of a wall",
                mesh = new[] {new[] {
                    new MeshBox(
                        new Vector3(-HalfBlockSize,HalfFloorHeight-0.15f,-HalfBlockSize),
                        new Vector3(HalfBlockSize,HalfFloorHeight,HalfBlockSize),
                        new UVData(new Vector4(0.9f,1,0,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), true, Quaternion.Euler(0,180,0)),
                    new MeshBox(
                        new Vector3(-HalfBlockSize,0,-0.15f),
                        new Vector3(HalfBlockSize,HalfFloorHeight-0.15f,-0.05f),
                        new UVData(new Vector4(1,0,0.9f,1), new Vector2(0,0.9f), 0, 0.9f, 0.1f ), Rotation: Quaternion.Euler(0,45,0), Faces: new FaceChanges() { excludeU = true }),
                    new MeshBox(
                        new Vector3(-HalfBlockSize*1.3f,0,-0.05f),
                        new Vector3(-0.1f,HalfFloorHeight-0.15f,0.05f),
                        new UVData(new Vector4(1,0,0.9f,1), new Vector2(0,0.9f), 0.55f, 0.35f, 0.1f ), Rotation: Quaternion.Euler(0,-45,0), Faces: new FaceChanges() { excludeU = true,excludeE = true })} },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 424, uniqueIndex = 160424, uniqueName = "Block_Roof_ScrapMetal_Endcap", localization = "Scrap Metal Roof Endcap@Covers your head",
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
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 427, uniqueIndex = 160427, uniqueName = "Block_Roof_ScrapMetal_Pyramid", localization = "Scrap Metal Roof Pyramid@Covers your head",
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
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 430, uniqueIndex = 160430, uniqueName = "Block_Roof_ScrapMetal_StraightV", localization = "Scrap Metal Double Roof Corner@Covers your head",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,0,0.9f,0.5f), new Vector2(0,0.1f), 0, 1, 1, -90 ), x => Mathf.Abs(x.x) < 0.25f ? x + Vector3.up * HalfFloorHeight * 0.5f : x),
                new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(0,0,HalfBlockSize),
                    new UVData(new Vector4(0,0.5f,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ), x => Mathf.Abs(x.x) < 0.25f ? x + Vector3.up * HalfFloorHeight * 0.5f : x)}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 488, uniqueIndex = 160488, uniqueName = "Block_Roof_ScrapMetal_TJunction", localization = "Scrap Metal T-Junction@Covers your head",
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
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 497, uniqueIndex = 160497, uniqueName = "Block_Roof_ScrapMetal_XJunction", localization = "Scrap Metal X-Junction@Covers your head",
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
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 502, uniqueIndex = 160502, uniqueName = "Block_Roof_ScrapMetal_LJunction", localization = "Scrap Metal L-Junction@Covers your head",
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
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 541, uniqueIndex = 160541, uniqueName = "Block_HorizontalPillar_ScrapMetal", localization = "Horizontal Scrap Metal Pillar@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize*2,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0, 0.9f, 0.1f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize*2,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,2), 0.9f, 0.1f, 0.9f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 542, uniqueIndex = 160542, uniqueName = "Block_HorizontalPillar_ScrapMetal_Half", localization = "Horizontal Half Scrap Metal Pillar@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0, 0.9f, 0.1f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0.9f, 0.1f, 0.9f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            /* Glass Blocks */
            new BlockItemCreation() {
                baseIndex = 2, uniqueIndex = 6970, uniqueName = "Block_Foundation_Glass", localization = "Glass Foundation@Used to expand your raft on the bottom floor",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight / 2,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0.75f,1), -0.05f, 1, 1 ))} },
                upgradeItem = 160550, material = () => instance.Glass,
                additionEdits = x => x.MakeAlwaysReinforced()
            },
            new BlockItemCreation() {
                baseIndex = 189, uniqueIndex = 6971, uniqueName = "Block_Foundation_Triangular_Glass", localization = "Triangular Glass Foundation@Used to expand your raft on the bottom floor",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight / 2,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,1,0,0), new Vector2(0.75f,1), -0.05f, 1, 1 ), true, Quaternion.Euler(0,180,0))} },
                upgradeItem = 160550, material = () => instance.Glass,
                additionEdits = x => x.MakeAlwaysReinforced()
            },
            new BlockItemCreation() {
                baseIndex = 1, uniqueIndex = 6973, uniqueName = "Block_Floor_Glass", localization = "Glass Floor@Used to build additional floors and roof. Cannot be built in thin air",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ))} },
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 191, uniqueIndex = 6974, uniqueName = "Block_Floor_Triangular_Glass", localization = "Triangular Glass Floor@Used to build additional floors and roof. Cannot be built in thin air",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,1,0,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), true, Quaternion.Euler(0,180,0))} },
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 4, uniqueIndex = 6976, uniqueName = "Block_Wall_Glass", localization = "Glass Wall@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,-0.04f),
                    new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,2), 0, 0.9f, 0.1f ))} },
                upgradeItem = 160550, material = () => instance.Glass, modelScales = new[] { Vector3.one, DiagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 144, uniqueIndex = 6977, uniqueName = "Block_HalfWall_Glass", localization = "Half Glass Wall@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,0),
                    new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0.04f),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,1), 0, 0.9f, 0.1f ))} },
                upgradeItem = 160550, material = () => instance.Glass, modelScales = new[] { Vector3.one, DiagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 152, uniqueIndex = 6978, uniqueName = "Block_Wall_Slope_Glass", localization = "Triangular Glass Wall@Fills triangular holes",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.04f,0.05f-HalfFloorHeight),
                    new Vector3(HalfBlockSize,0,0.05f),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90,true, DifferentEnds: true), true, Quaternion.Euler(0,0,180) * Quaternion.Euler(-90,0,0))} },
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 421, uniqueIndex = 6979, uniqueName = "Block_Wall_VSlope_Glass", localization = "Pyramid Glass Wall@Fills pyramid holes",
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
                baseIndex = 443, uniqueIndex = 6980, uniqueName = "Block_Wall_Slope_Glass_Inverted", localization = "Triangular Glass Wall@Fills triangular holes",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0.006f-HalfBlockSize,0,-0.051f-HalfFloorHeight),
                    new Vector3(HalfBlockSize+0.006f,0.04f,-0.051f),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90, true, true), true, Quaternion.Euler(90,180,0))} },
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 148, uniqueIndex = 6983, uniqueName = "Block_Roof_Straight_Glass", localization = "Glass Roof@Covers your head",
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
                upgradeItem = 160550, material = () => instance.Glass, resetModelRotations = new[]{null, new[] { true }, new[] { true } }
            },
            new BlockItemCreation() {
                baseIndex = 150, uniqueIndex = 6984, uniqueName = "Block_Roof_Corner_Glass", localization = "Glass Roof Corner@Covers your head",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (x.x < 0 && x.z > 0) ? x + Vector3.up * HalfFloorHeight : x)}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 160, uniqueIndex = 6985, uniqueName = "Block_Roof_InvCorner_Glass", localization = "Inverted Glass Roof Corner@Covers your head",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((x.x < 0 || x.z > 0) ? x + Vector3.up * HalfFloorHeight : x))}},
                upgradeItem = 160550, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 430, uniqueIndex = 6986, uniqueName = "Block_Roof_Glass_StraightV", localization = "Glass Double Roof Corner@Covers your head",
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
                baseIndex = 502, uniqueIndex = 6987, uniqueName = "Block_Roof_Glass_LJunction", localization = "Glass L-Junction@Covers your head",
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
                baseIndex = 488, uniqueIndex = 6988, uniqueName = "Block_Roof_Glass_TJunction", localization = "Glass T-Junction@Covers your head",
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
                baseIndex = 497, uniqueIndex = 6989, uniqueName = "Block_Roof_Glass_XJunction", localization = "Glass X-Junction@Covers your head",
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
                baseIndex = 424, uniqueIndex = 6990, uniqueName = "Block_Roof_Glass_Endcap", localization = "Glass Roof Endcap@Covers your head",
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
                baseIndex = 427, uniqueIndex = 6991, uniqueName = "Block_Roof_Glass_Pyramid", localization = "Glass Roof Pyramid@Covers your head",
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
                baseIndex = 84, uniqueIndex = 6992, uniqueName = "Block_Pillar_Glass", localization = "Glass Pillar@Provides support for additional floors",
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
                baseIndex = 146, uniqueIndex = 6993, uniqueName = "Block_Pillar_Glass_Half", localization = "Half Glass Pillar@Provides support for additional floors",
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
                baseIndex = 541, uniqueIndex = 6994, uniqueName = "Block_HorizontalPillar_Glass", localization = "Horizontal Glass Pillar@Provides support for additional floors",
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
                baseIndex = 542, uniqueIndex = 6995, uniqueName = "Block_HorizontalPillar_Glass_Half", localization = "Horizontal Half Glass Pillar@Provides support for additional floors",
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
                baseIndex = 88, uniqueIndex = 6996, uniqueName = "Block_Wall_Door_Glass", localization = "Glass Door@Provides support for additional floors",
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

        Material Glass;
        Material ScrapMetal;
        Material Metal;
        public LanguageSourceData language;
        public static List<Object> createdObjects = new List<Object>();
        Harmony harmony;
        Transform prefabHolder;
        public static Main instance;
        bool loaded = false;
        public void Start()
        {
            AssetBundle.GetAllLoadedAssetBundles();
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
                mLanguages = new List<LanguageData> { new LanguageData() { Code = "en", Name = "English" } }
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
            var t = LoadImage("scrapMetal_Specular.png");
            var p = t.GetPixels();
            for (int i = 0; i < p.Length; i++)
                p[i] = new Color(0, 1 - Mathf.Abs(p[i].r * 2 - 1f), 0, 1 - Mathf.Abs(p[i].r * 2 - 1f));
            t.SetPixels(p);
            t.Apply();
            ScrapMetal.SetTexture("_MetallicRPaintMaskGSmoothnessA", t);
            Metal.SetTexture("_Diffuse", LoadImage("metal.png"));
            Metal.SetTexture("_Normal", LoadImage("metalNormal.png"));
            

            foreach (var item in items)
            {
                var baseItem = ItemManager.GetItemByIndex(item.baseIndex);
                if (baseItem)
                {
                    item.baseItem = baseItem;
                    CreateItem(item);
                }
            }
            (harmony = new Harmony("com.aidanamite.MetalBuilding")).PatchAll();
            ModUtils_ReloadBuildMenu();
            Traverse.Create(typeof(LocalizationManager)).Field("OnLocalizeEvent").GetValue<LocalizationManager.OnLocalizeCallback>().Invoke();
            Log("Mod has been loaded!");
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
                    Traverse.Create(q).Field("acceptableBlockTypes").GetValue<List<Item_Base>>().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
                foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockCollisionMask>())
                    Traverse.Create(q).Field("blockTypesToIgnore").GetValue<List<Item_Base>>().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
                ItemManager.GetAllItems().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
                foreach (var b in GetPlacedBlocks())
                    if (b && b.buildableItem && items.Any(y => y.item?.UniqueIndex == b.buildableItem.UniqueIndex))
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
            Log("Mod has been unloaded!");
        }

        public void CreateItem(ItemCreation item)
        {
            item.item = item.baseItem.Clone(item.uniqueIndex, item.uniqueName);
            if (item.loadIcon)
                Debug.Log("not loaded image");// item.item.settings_Inventory.Sprite = LoadImage(item.uniqueName + ".png").ToSprite();
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
            language.mDictionary[item.item.settings_Inventory.LocalizationTerm] = new TermData() { Languages = new[] { item.localization } };
            var blockCreation = item as BlockItemCreation;
            if (blockCreation != null)
            {
                var p = item.item.settings_buildable.GetBlockPrefabs().ToArray();
                for (int i = 0; i < p.Length; i++)
                {
                    var me = blockCreation.mesh[i];
                    p[i] = Instantiate(p[i], prefabHolder, false);
                    p[i].name = item.item.UniqueName + ((p.Length == 1 || p[i].dpsType == DPS.Default) ? "" : $"_{p[i].dpsType}");
                    var r = p[i].GetComponentsInChildren<Renderer>(true);
                    for (int j = 0; j < r.Length; j++)
                        if (me.Length > j && me[j] != null)
                        {
                            r[j].sharedMaterial = blockCreation.materials[i];
                            if (r[j] is SkinnedMeshRenderer)
                                (r[j] as SkinnedMeshRenderer).sharedMesh = me.GetSafe(j);
                            else
                                r[j].GetComponent<MeshFilter>().sharedMesh = me.GetSafe(j);
                            if (blockCreation.resetModelRotations?.Length > i && blockCreation.resetModelRotations[i]?.Length > j && blockCreation.resetModelRotations[i][j])
                                r[j].transform.localRotation = default;
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

            if (item.baseItem.settings_recipe.NewCost.Length > 0)
            {
                Item_Base craftingMaterial = ItemManager.GetItemByName("Glass");

                if (item.item.UniqueName.Contains("_ScrapMetal"))
                {
                    craftingMaterial = ItemManager.GetItemByName("Scrap");
                }
                else if (item.item.UniqueName.Contains("_Metal"))
                {
                    craftingMaterial = ItemManager.GetItemByName("MetalIngot");
                }
                
                item.item.SetRecipe(new CostMultiple[] { new CostMultiple(new Item_Base[] { craftingMaterial }, item.baseItem.settings_recipe.NewCost[0].amount) });
            }

            ItemManager.GetAllItems().Add(item.item);
        }


        public Texture2D LoadImage(string filename, bool leaveReadable = true)
        {
            var t = new Texture2D(0, 0);
            t.LoadImage(GetEmbeddedFileBytes(filename), !leaveReadable);
            if (leaveReadable)
                t.Apply();
            createdObjects.Add(t);
            return t;
        }
        public static Texture2D LoadAbsoluteImage(string filename, bool leaveReadable = true)
        {
            var t = new Texture2D(0, 0);
            t.LoadImage(System.IO.File.ReadAllBytes(filename), !leaveReadable);
            if (leaveReadable)
                t.Apply();
            createdObjects.Add(t);
            return t;
        }

        List<(Item_Base, Item_Base, bool)> ModUtils_BuildMenuItems()
        {
            if (!loaded) return null;
            var l = new List<(Item_Base, Item_Base, bool)>();
            foreach (var i in items)
                if (i.baseItem && i.item)
                    l.Add((i.baseItem, i.item, i.isUpgrade));
            return l;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        void ModUtils_ReloadBuildMenu() { }

        [ConsoleCommand("loadMetal")]
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
    }

    [HarmonyPatch(typeof(ItemInstance_Buildable.Upgrade), "GetNewItemFromUpgradeItem")]
    static class Patch_GetUpgradeItem
    {
        static void Postfix(ItemInstance_Buildable.Upgrade __instance, Item_Base buildableItem, ref Item_Base __result)
        {
            var i = buildableItem == null ? null : instance.items.FirstOrDefault(x => x.isUpgrade && x.item?.UniqueIndex == buildableItem.UniqueIndex)?.baseItem;
            if (i)
            {
                foreach (var p in instance.items)
                    if ((p as BlockItemCreation)?.upgradeItem == buildableItem.UniqueIndex && p.item && p.baseItem?.settings_buildable?.Upgrades == __instance)
                    {
                        __result = p.item;
                        return;
                    }
                foreach (var p in instance.items)
                    if ((p as BlockItemCreation)?.upgradeItem == buildableItem.UniqueIndex && p.item && (p.baseItem?.settings_buildable?.Upgrades?.HasFieldValueMatch<Item_Base>(x => x?.settings_buildable?.Upgrades == __instance) ?? false))
                    {
                        __result = p.item;
                        return;
                    }
                var b = __instance.GetNewItemFromUpgradeItem(i);
                foreach (var p in instance.items)
                    if ((p as BlockItemCreation)?.upgradeItem == buildableItem.UniqueIndex && p.item && (
                        p.baseItem?.settings_buildable?.Upgrades == b.settings_buildable.Upgrades
                        || (p.baseItem?.settings_buildable?.Upgrades?.HasFieldValueMatch<Item_Base>(x => x?.settings_buildable?.Upgrades == b.settings_buildable.Upgrades) ?? false)))
                    {
                        __result = p.item;
                        return;
                    }
            }
        }
    }

    [HarmonyPatch(typeof(BlockCreator), "IsBuildableItemUpgradeItem")]
    static class Patch_BlockCreator
    {
        static void Postfix(Item_Base buildableItem, ref bool __result)
        {
            if (!__result && buildableItem != null && instance.items.Any(x => x.isUpgrade && x.item?.UniqueIndex == buildableItem.UniqueIndex))
                __result = true;
        }
    }

    [HarmonyPatch(typeof(Block), "IsWalkable")]
    static class Patch_Block
    {
        static void Postfix(Block __instance, ref bool __result)
        {
            foreach (var p in instance.items)
                if (p.item && p.item.UniqueIndex == __instance.buildableItem?.UniqueIndex)
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

    [HarmonyPatch(typeof(BaseModHandler), "UnloadMod")]
    static class Patch_UnloadMod
    {
        static bool Prefix(ModData moddata)
        {
            if (moddata?.modinfo?.mainClass is Main && SceneManager.GetActiveScene().name == Raft_Network.GameSceneName && ComponentManager<Raft_Network>.Value.remoteUsers.Count > 1)
            {
                Debug.LogError($"[{moddata.jsonmodinfo.name}]: This cannot be unloaded while in a multiplayer");
                return false;
            }
            return true;
        }
    }
}