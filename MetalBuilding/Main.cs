using HarmonyLib;
using HMLLibrary;
using RaftModLoader;
using Steamworks;
using System;
using System.Collections;
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
using UnityEngine.Experimental.Rendering;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using static BlockCreator;


namespace MoreBuilding
{
    public class Main : Mod
    {
        #region ItemCreation
        public static float diagonalMagnitude = Vector2.one.magnitude;
        public static Vector3 diagonalScale = new Vector3(Vector2.one.magnitude,1,1);
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
                baseIndex = 408, uniqueIndex = 160408, uniqueName = "Block_Wall_Slope_ScrapMetal", localization = "Triangular Scrap Metal Wall@Fills triangular holes",
                mesh = new[]{new[] { GeneratedMeshes.WallSlope } },
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
                baseIndex = 411, uniqueIndex = 160411, uniqueName = "Block_Wall_Window_ScrapMetal", localization = "Scrap Metal Window@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,-0.04f),
                    new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,1), 0, 0.9f, 0.1f )),
                new MeshBox(
                    new Vector3(-HalfBlockSize,FullFloorHeight-0.4f,-0.04f),
                    new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(1 - (0.35f/HalfFloorHeight),1), 0, 0.9f, 0.1f )),
                new MeshBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight-0.05f, -0.04f),
                    new Vector3(0.04f-HalfBlockSize,FullFloorHeight-0.4f,0),
                    new UVData(Vector4.zero, new Vector2(0,1 - (0.35f/HalfFloorHeight)), 0.9f - 0.04f / BlockSize * 0.9f, 0.04f / BlockSize * 0.9f, 0.1f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.04f, HalfFloorHeight-0.05f, -0.04f),
                    new Vector3(HalfBlockSize,FullFloorHeight-0.4f,0),
                    new UVData(Vector4.zero, new Vector2(0,1 - (0.35f/HalfFloorHeight)), 0, 0.04f / BlockSize * 0.9f, 0.9f - 0.08f / BlockSize * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(-HalfBlockSize, HalfFloorHeight-0.05f, -0.04f),
                    new Vector3(HalfBlockSize,FullFloorHeight-0.4f,0),
                    new UVData(Vector4.zero, new Vector2(0,1 - (0.35f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.04f, HalfFloorHeight-0.05f, -0.04f),
                    new Vector3(0.04f-HalfBlockSize,FullFloorHeight-0.4f,0),
                    new UVData(Vector4.zero, new Vector2(0,1 - (0.35f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160546, material = () => instance.ScrapMetal, modelScales = new[] { Vector3.one, diagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 493, uniqueIndex = 160493, uniqueName = "Block_Wall_Window_ScrapMetal_Half", localization = "Half Scrap Metal Window@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,-0.04f),
                    new Vector3(HalfBlockSize,0.3f,0),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,0.35f/HalfFloorHeight), 0, 0.9f, 0.1f )),
                new MeshBox(
                    new Vector3(-HalfBlockSize,HalfFloorHeight-0.35f,-0.04f),
                    new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(1 - (0.3f/HalfFloorHeight),1), 0, 0.9f, 0.1f )),
                new MeshBox(
                    new Vector3(-HalfBlockSize, 0.3f, -0.04f),
                    new Vector3(0.2f-HalfBlockSize,HalfFloorHeight-0.35f,0),
                    new UVData(Vector4.zero, new Vector2(0.35f/HalfFloorHeight,1 - (0.3f/HalfFloorHeight)), 0.9f - 0.2f / BlockSize * 0.9f, 0.2f / BlockSize * 0.9f, 0.1f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.2f, 0.3f, -0.04f),
                    new Vector3(HalfBlockSize,HalfFloorHeight-0.35f,0),
                    new UVData(Vector4.zero, new Vector2(0.35f/HalfFloorHeight,1 - (0.3f/HalfFloorHeight)), 0, 0.2f / BlockSize * 0.9f, 0.9f - 0.4f / BlockSize * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(-HalfBlockSize, 0.3f, -0.04f),
                    new Vector3(HalfBlockSize,HalfFloorHeight-0.35f,0),
                    new UVData(Vector4.zero, new Vector2(0.35f/HalfFloorHeight,1 - (0.3f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.2f, 0.3f, -0.04f),
                    new Vector3(0.2f-HalfBlockSize,HalfFloorHeight-0.35f,0),
                    new UVData(Vector4.zero, new Vector2(0.35f/HalfFloorHeight,1 - (0.3f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160546, material = () => instance.ScrapMetal, modelScales = new[] { Vector3.one, diagonalScale }
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
                baseIndex = 85, uniqueIndex = 160085, uniqueName = "Block_Wall_Fence_ScrapMetal", localization = "Scrap Metal Fence@Keeps you from falling of your raft",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize, -0.05f, -0.06f),
                    new Vector3(0.12f-HalfBlockSize,HalfFloorHeight*0.8f-0.05f,0.06f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(-HalfBlockSize, -0.05f, -0.06f),
                    new Vector3(0.12f-HalfBlockSize,HalfFloorHeight*0.8f-0.05f,0.06f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0.9f, 0.1f, 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.12f, -0.05f, -0.06f),
                    new Vector3(HalfBlockSize,HalfFloorHeight*0.8f-0.05f,0.06f),
                    new UVData(new Vector4(0.9f,0.9f,1,1), new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(HalfBlockSize-0.12f, -0.05f, -0.06f),
                    new Vector3(HalfBlockSize,HalfFloorHeight*0.8f-0.05f,0.06f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0.9f, 0.1f, 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true }),
                new MeshBox(
                    new Vector3(0.12f-HalfBlockSize, 0.05f, -0.05f),
                    new Vector3(HalfBlockSize-0.12f,0.15f,0.05f),
                    new UVData(new Vector4(0.9f,0,1,1), new Vector2(0,0.1f), 0, 1, 0, -90 ), Faces: new FaceChanges() { excludeE = true, excludeW = true }),
                new MeshBox(
                    new Vector3(0.12f-HalfBlockSize, 0.25f, -0.05f),
                    new Vector3(HalfBlockSize-0.12f,0.35f,0.05f),
                    new UVData(new Vector4(0.9f,0,1,1), new Vector2(0,0.1f), 0, 1, 0, -90 ), Faces: new FaceChanges() { excludeE = true, excludeW = true }),
                new MeshBox(
                    new Vector3(0.12f-HalfBlockSize, 0.45f, -0.05f),
                    new Vector3(HalfBlockSize-0.12f,0.55f,0.05f),
                    new UVData(new Vector4(0.9f,0,1,1), new Vector2(0,0.1f), 0, 1, 0, -90 ), Faces: new FaceChanges() { excludeE = true, excludeW = true }),
                new MeshBox(
                    new Vector3(0.12f-HalfBlockSize, 0.65f, -0.05f),
                    new Vector3(HalfBlockSize-0.12f,0.75f,0.05f),
                    new UVData(new Vector4(0.9f,0,1,1), new Vector2(0,0.1f), 0, 1, 0, -90 ), Faces: new FaceChanges() { excludeE = true, excludeW = true })}},
                upgradeItem = 160546, material = () => instance.ScrapMetal, modelScales = new[] { Vector3.one, diagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 88, uniqueIndex = 160088, uniqueName = "Block_Wall_Door_ScrapMetal", localization = "Scrap Metal Door@Provides support for additional floors",
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
                            new Vector3(-HalfBlockSize,FullFloorHeight-0.09f,-0.04f).Multiply(diagonalScale),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0).Multiply(diagonalScale),
                            new UVData(new Vector4(0.9f,1,1,0), new Vector2(2 - (0.04f/HalfFloorHeight),2), 0, 0.9f, 0.1f )),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.04f).Multiply(diagonalScale),
                            new Vector3(0.08f-HalfBlockSize * diagonalMagnitude,FullFloorHeight-0.09f,0),
                            new UVData(new Vector4(0.9f,1,1,1 - 0.04f / BlockSize), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.9f - 0.08f / BlockSize / diagonalMagnitude * 0.9f, 0.08f / BlockSize / diagonalMagnitude * 0.9f, 0.1f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize * diagonalMagnitude-0.08f, -0.05f, -0.04f),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.09f,0).Multiply(diagonalScale),
                            new UVData(new Vector4(0.9f,0.04f / BlockSize,1,0), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.08f / BlockSize / diagonalMagnitude * 0.9f, 0.9f - 0.16f / BlockSize / diagonalMagnitude * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.04f).Multiply(diagonalScale),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.09f,0).Multiply(diagonalScale),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize * diagonalMagnitude-0.08f, -0.05f, -0.04f),
                            new Vector3(0.08f-HalfBlockSize * diagonalMagnitude,FullFloorHeight-0.09f,0),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true })},
                    new[] {
                        new MeshBox(
                            new Vector3(-0.001f,-0.97f,0.01f),
                            new Vector3(HalfBlockSize * diagonalMagnitude-0.081f,FullFloorHeight-1.01f,0.03f),
                            new UVData(new Vector4(0.9f,0.5f,1,0), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.45f, 0.45f-0.08f/BlockSize / diagonalMagnitude * 0.9f, 0.1f + 0.16f/BlockSize / diagonalMagnitude * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(-0.001f,-0.97f,0.01f),
                            new Vector3(HalfBlockSize * diagonalMagnitude-0.081f,FullFloorHeight-1.01f,0.03f),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )},
                    new[] {
                        new MeshBox(
                            new Vector3(0.081f-HalfBlockSize * diagonalMagnitude,-0.975f,0.01f),
                            new Vector3(0.001f,FullFloorHeight-1.015f,0.03f),
                            new UVData(new Vector4(0.9f,1,1,0.5f), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.08f/BlockSize / diagonalMagnitude * 0.9f, 0.45f-0.08f/BlockSize / diagonalMagnitude * 0.9f, 1 ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(0.081f-HalfBlockSize * diagonalMagnitude,-0.975f,0.01f),
                            new Vector3(0.001f,FullFloorHeight-1.015f,0.03f),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )}}},
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
                baseIndex = 255, uniqueIndex = 160255, uniqueName = "Block_Wall_Gate_ScrapMetal", localization = "Scrap Metal Gate@Provides support for additional floors",
                mesh = new[] {new[] {
                    new[] {
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.05f),
                            new Vector3(0.08f-HalfBlockSize,HalfFloorHeight-0.05f,0.075f),
                            new UVData(new Vector4(0.9f,1,1,1 - 0.04f / BlockSize), new Vector2(0,1), 0.9f - 0.08f / BlockSize * 0.9f, 0.08f / BlockSize * 0.9f, 0.1f ), Faces: new FaceChanges() { excludeE = true, excludeW = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize-0.08f, -0.05f, -0.05f),
                            new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0.075f),
                            new UVData(new Vector4(0.9f,0.04f / BlockSize,1,0), new Vector2(0,1), 0, 0.08f / BlockSize * 0.9f, 0.9f - 0.16f / BlockSize * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true }),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.05f),
                            new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0.075f),
                            new UVData(Vector4.zero, new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize-0.08f, -0.05f, -0.05f),
                            new Vector3(0.08f-HalfBlockSize,HalfFloorHeight-0.05f,0.075f),
                            new UVData(Vector4.zero, new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true })},
                    new[] {
                        new MeshBox(
                            new Vector3(0.074f-HalfBlockSize,-HalfFloorHeight/2-0.011f,-0.05f),
                            new Vector3(-0.006f,HalfFloorHeight/2-0.011f,0.05f),
                            new UVData(new Vector4(0.9f,1,1,0.5f), new Vector2(0,1), 0.08f/BlockSize * 0.9f, 0.45f-0.08f/BlockSize * 0.9f, 1 ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(0.074f-HalfBlockSize,-HalfFloorHeight/2-0.011f,-0.05f),
                            new Vector3(-0.006f,HalfFloorHeight/2-0.011f,0.05f),
                            new UVData(Vector4.zero, new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )},
                    new[] {
                        new MeshBox(
                            new Vector3(-0.004f,-HalfFloorHeight/2-0.011f,-0.05f),
                            new Vector3(HalfBlockSize-0.084f,HalfFloorHeight/2-0.011f,0.05f),
                            new UVData(new Vector4(0.9f,0.5f,1,0), new Vector2(0,1), 0.45f, 0.45f-0.08f/BlockSize * 0.9f, 0.1f + 0.16f/BlockSize * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(-0.004f,-HalfFloorHeight/2-0.011f,-0.05f),
                            new Vector3(HalfBlockSize-0.084f,HalfFloorHeight/2-0.011f,0.05f),
                            new UVData(Vector4.zero, new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )}},
                new[] {
                    new[] {
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.05f).Multiply(diagonalScale),
                            new Vector3(0.08f-HalfBlockSize*diagonalMagnitude,HalfFloorHeight-0.05f,0.075f),
                            new UVData(new Vector4(0.9f,1,1,1 - 0.04f / BlockSize), new Vector2(0,1), 0.9f - 0.08f / BlockSize/diagonalMagnitude * 0.9f, 0.08f/BlockSize/diagonalMagnitude*0.9f, 0.1f ), Faces: new FaceChanges() { excludeE = true, excludeW = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize*diagonalMagnitude-0.08f, -0.05f, -0.05f),
                            new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0.075f).Multiply(diagonalScale),
                            new UVData(new Vector4(0.9f,0.04f / BlockSize,1,0), new Vector2(0,1), 0, 0.08f / BlockSize/diagonalMagnitude * 0.9f, 0.9f - 0.16f / BlockSize/diagonalMagnitude * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true }),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.05f).Multiply(diagonalScale),
                            new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0.075f).Multiply(diagonalScale),
                            new UVData(Vector4.zero, new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize*diagonalMagnitude-0.08f, -0.05f, -0.05f),
                            new Vector3(0.08f-HalfBlockSize*diagonalMagnitude,HalfFloorHeight-0.05f,0.075f),
                            new UVData(Vector4.zero, new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true })},
                    new[] {
                        new MeshBox(
                            new Vector3(-0.004f,-HalfFloorHeight/2-0.011f,-0.05f),
                            new Vector3(HalfBlockSize*diagonalMagnitude-0.084f,HalfFloorHeight/2-0.011f,0.05f),
                            new UVData(new Vector4(0.9f,0.5f,1,0), new Vector2(0,1), 0.45f, 0.45f-0.08f/BlockSize/diagonalMagnitude * 0.9f, 0.1f + 0.16f/BlockSize/diagonalMagnitude * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(-0.004f,-HalfFloorHeight/2-0.011f,-0.05f),
                            new Vector3(HalfBlockSize*diagonalMagnitude-0.084f,HalfFloorHeight/2-0.011f,0.05f),
                            new UVData(Vector4.zero, new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )},
                    new[] {
                        new MeshBox(
                            new Vector3(0.074f-HalfBlockSize*diagonalMagnitude,-HalfFloorHeight/2-0.011f,-0.05f),
                            new Vector3(-0.006f,HalfFloorHeight/2-0.011f,0.05f),
                            new UVData(new Vector4(0.9f,1,1,0.5f), new Vector2(0,1), 0.08f/BlockSize/diagonalMagnitude * 0.9f, 0.45f-0.08f/BlockSize/diagonalMagnitude * 0.9f, 1 ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(0.074f-HalfBlockSize*diagonalMagnitude,-HalfFloorHeight/2-0.011f,-0.05f),
                            new Vector3(-0.006f,HalfFloorHeight/2-0.011f,0.05f),
                            new UVData(Vector4.zero, new Vector2(0,1), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )}}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 421, uniqueIndex = 160421, uniqueName = "Block_Wall_VSlope_ScrapMetal", localization = "Pyramid Scrap Metal Wall@Fills pyramid holes",
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
                baseIndex = 443, uniqueIndex = 160443, uniqueName = "Block_Wall_Slope_ScrapMetal_Inverted", localization = "Triangular Scrap Metal Wall@Fills triangular holes",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(0.006f-HalfBlockSize,0,-0.051f-HalfFloorHeight),
                    new Vector3(HalfBlockSize+0.006f,0.04f,-0.051f),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90, true, true), true, Quaternion.Euler(90,180,0))} },
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
                upgradeItem = 160550, material = () => instance.Glass, modelScales = new[] { Vector3.one, diagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 144, uniqueIndex = 6977, uniqueName = "Block_HalfWall_Glass", localization = "Half Glass Wall@Provides support for additional floors",
                mesh = new[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,0),
                    new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0.04f),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,1), 0, 0.9f, 0.1f ))} },
                upgradeItem = 160550, material = () => instance.Glass, modelScales = new[] { Vector3.one, diagonalScale }
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
                            new Vector3(-HalfBlockSize,FullFloorHeight-0.09f,-0.04f).Multiply(diagonalScale),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0).Multiply(diagonalScale),
                            new UVData(new Vector4(0.9f,1,1,0), new Vector2(2 - (0.04f/HalfFloorHeight),2), 0, 0.9f, 0.1f )),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.04f).Multiply(diagonalScale),
                            new Vector3(0.08f-HalfBlockSize * diagonalMagnitude,FullFloorHeight-0.09f,0),
                            new UVData(new Vector4(0.9f,1,1,1 - 0.04f / BlockSize), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.9f - 0.08f / BlockSize / diagonalMagnitude * 0.9f, 0.08f / BlockSize / diagonalMagnitude * 0.9f, 0.1f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize * diagonalMagnitude-0.08f, -0.05f, -0.04f),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.09f,0).Multiply(diagonalScale),
                            new UVData(new Vector4(0.9f,0.04f / BlockSize,1,0), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.08f / BlockSize / diagonalMagnitude * 0.9f, 0.9f - 0.16f / BlockSize / diagonalMagnitude * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(-HalfBlockSize, -0.05f, -0.04f).Multiply(diagonalScale),
                            new Vector3(HalfBlockSize,FullFloorHeight-0.09f,0).Multiply(diagonalScale),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true }),
                        new MeshBox(
                            new Vector3(HalfBlockSize * diagonalMagnitude-0.08f, -0.05f, -0.04f),
                            new Vector3(0.08f-HalfBlockSize * diagonalMagnitude,FullFloorHeight-0.09f,0),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeD = true, excludeU = true })},
                    new[] {
                        new MeshBox(
                            new Vector3(-0.001f,-0.97f,0.01f),
                            new Vector3(HalfBlockSize * diagonalMagnitude-0.081f,FullFloorHeight-1.01f,0.03f),
                            new UVData(new Vector4(0.9f,0.5f,1,0), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.45f, 0.45f-0.08f/BlockSize / diagonalMagnitude * 0.9f, 0.1f + 0.16f/BlockSize / diagonalMagnitude * 0.9f ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(-0.001f,-0.97f,0.01f),
                            new Vector3(HalfBlockSize * diagonalMagnitude-0.081f,FullFloorHeight-1.01f,0.03f),
                            new UVData(Vector4.zero, new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0, 0.9f, 0.1f ), Faces: new FaceChanges() { excludeN = true, excludeS = true, excludeU = true, excludeD = true } )},
                    new[] {
                        new MeshBox(
                            new Vector3(0.081f-HalfBlockSize * diagonalMagnitude,-0.975f,0.01f),
                            new Vector3(0.001f,FullFloorHeight-1.015f,0.03f),
                            new UVData(new Vector4(0.9f,1,1,0.5f), new Vector2(0,2 - (0.04f/HalfFloorHeight)), 0.08f/BlockSize / diagonalMagnitude * 0.9f, 0.45f-0.08f/BlockSize / diagonalMagnitude * 0.9f, 1 ), Faces: new FaceChanges() { excludeE = true, excludeW = true } ),
                        new MeshBox(
                            new Vector3(0.081f-HalfBlockSize * diagonalMagnitude,-0.975f,0.01f),
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
            foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockQuadType>())
                    Traverse.Create(q).Field("acceptableBlockTypes").GetValue<List<Item_Base>>().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
            foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockCollisionMask>())
                    Traverse.Create(q).Field("blockTypesToIgnore").GetValue<List<Item_Base>>().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
            ItemManager.GetAllItems().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
            foreach (var b in GetPlacedBlocks())
                if (b && b.buildableItem && items.Any(y => y.item?.UniqueIndex == b.buildableItem.UniqueIndex))
                    RemoveBlock(b, null, false);
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
            foreach(var d in data)
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

    public static class GeneratedMeshes
    {
        public static Mesh Foundation;
        public static Mesh FoundationTriangle;
        public static Mesh FoundationTriangleMirrored;
        public static Mesh Floor;
        public static Mesh FloorTriangle;
        public static Mesh FloorTriangleMirrored;
        public static Mesh FloorHalf;
        public static Mesh FloorHalfTriangle;
        public static Mesh FloorHalfTriangleMirrored;
        public static Mesh Wall;
        public static Mesh WallDiagonal;
        public static Mesh WallHalf;
        public static Mesh WallHalfDiagonal;
        public static Mesh WallV;
        public static Mesh WallSlope;
        public static Mesh WallSlopeInverted;
        public static Mesh Fence;
        public static Mesh Gate;
        public static Mesh Door;
        public static Mesh DoorWide;
        public static Mesh Window;
        public static Mesh WindowHalf;
        public static Mesh Roof;
        public static Mesh RoofDiagonal;
        public static Mesh RoofDiagonalAlt;
        public static Mesh RoofCorner;
        public static Mesh RoofCornerInverted;
        public static Mesh RoofV0;
        public static Mesh RoofV1;
        public static Mesh RoofV2I;
        public static Mesh RoofV2L;
        public static Mesh RoofV3;
        public static Mesh RoofV4;
        public static Mesh Pillar;
        public static Mesh PillarHalf;
        public static Mesh PillarHorizontal;
        public static Mesh PillarHorizontalHalf;
        static GeneratedMeshes()
        {
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight / 2,-HalfBlockSize), new Vector3(HalfBlockSize,0,HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0.6666666f, 0.9f, 1), (0, 0.6666666f, 0.9f, 1),
                    (0, 0.6666666f, 0.9f, 1), (0, 0.6666666f, 0.9f, 1)
                    );
                Foundation = builder.ToMesh("Foundation");
            }
            {
                var builder = new MeshBuilder();
                builder.AddWedge(
                    new Vector3(-HalfBlockSize, -HalfFloorHeight / 2, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0.6666666f, 0.9f, 1), (0, 0.6666666f, 0.9f, 1), (0, 0.6666666f, 0.9f, 1)
                    );
                FoundationTriangle = builder.ToMesh("FoundationTriangle");
            }
            {
                var builder = new MeshBuilder();
                builder.AddWedge(
                    new Vector3(-HalfBlockSize, -HalfFloorHeight / 2, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 1, 0.9f), (0, 0, 1, 0.9f),
                    (0, 0.6666666f, 0.9f, 1), (0, 0.6666666f, 0.9f, 1), (0, 0.6666666f, 0.9f, 1),
                    x => x.Rotate(0,-90,0), (x, y) => y == Axis.Y || y == Axis.NY ? x.Rotate(90) : x
                    );
                FoundationTriangleMirrored = builder.ToMesh("FoundationTriangleMirrored");
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyUV: (x,y) => y == Axis.Y || y == Axis.NY ? x : x.Rotate(-90)
                    );
                Floor = builder.ToMesh("Floor");
            }
            {
                var builder = new MeshBuilder();
                builder.AddWedge(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0, 0, 1, .1f), (0, 0, 1, .1f), (0, 0, 1, .1f),
                    modifyUV: (x, y) => y == Axis.Y || y == Axis.NY ? x : x.Rotate(-90)
                    );
                FloorTriangle = builder.ToMesh("FloorTriangle");
            }
            {
                var builder = new MeshBuilder();
                builder.AddWedge(
                    new Vector3(-HalfBlockSize, -0.2f, -HalfBlockSize), new Vector3(HalfBlockSize, 0, HalfBlockSize),
                    (0, 0, 1, 0.9f), (0, 0, 1, 0.9f),
                    (0, 0, 1, .1f), (0, 0, 1, .1f), (0, 0, 1, .1f),
                    x => x.Rotate(0, -90, 0), (x, y) => y == Axis.Y || y == Axis.NY ? x.Rotate(90) : x.Rotate(-90)
                    );
                FloorTriangleMirrored = builder.ToMesh("FloorTriangleMirrored");
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2)
                    );
                Wall = builder.ToMesh("Wall");
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize * Main.diagonalMagnitude, -0.01f, -0.06f), new Vector3(0, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0, 0.9f, 2), null,
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2)
                    );
                builder.AddBox(
                    new Vector3(0, -0.01f, -0.06f), new Vector3(HalfBlockSize * Main.diagonalMagnitude, FullFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.9f, 2), (0.9f, 0, 1, 2),
                    (0, 0, 0.9f, 2), null
                    );
                WallDiagonal = builder.ToMesh("WallDiagonal");
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                WallHalf = builder.ToMesh("WallHalf");
            }
            {
                var builder = new MeshBuilder();
                builder.AddBox(
                    new Vector3(-HalfBlockSize * Main.diagonalMagnitude, -0.01f, -0.06f), new Vector3(0, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 0.5f), (0.9f, 0, 1, 0.5f),
                    (0, 0, 0.9f, 1), null,
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1)
                    );
                builder.AddBox(
                    new Vector3(0, -0.01f, -0.06f), new Vector3(HalfBlockSize * Main.diagonalMagnitude, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0.5f, 1, 1), (0.9f, 0.5f, 1, 1),
                    (0, 0, 0.9f, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), null
                    );
                WallHalfDiagonal = builder.ToMesh("WallHalfDiagonal");
            }
            {
                var builder = new MeshBuilder();
                builder.AddRamp(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0.9f, 0, 1, 1)
                    );
                WallSlope = builder.ToMesh("WallSlope");
            }
            {
                var builder = new MeshBuilder();
                builder.AddRamp(
                    new Vector3(-HalfBlockSize, -0.01f, -0.06f), new Vector3(HalfBlockSize, HalfFloorHeight - 0.01f, 0.06f),
                    (0.9f, 0, 1, 1), (0.9f, 0, 1, 1),
                    (0, 0, 0.9f, 1), (0, 0, 0.9f, 1),
                    (0.9f, 0, 1, 1)
                    );
                WallSlope = builder.ToMesh("WallSlope");
            }
        }

        public static void AddBox(this MeshBuilder builder, Vector3 min, Vector3 max, (float minX, float minY, float maxX, float maxY)? top = null, (float minX, float minY, float maxX, float maxY)? bottom = null, (float minX, float minY, float maxX, float maxY)? north = null, (float minX, float minY, float maxX, float maxY)? east = null, (float minX, float minY, float maxX, float maxY)? south = null, (float minX, float minY, float maxX, float maxY)? west = null, Func<Vector3,Vector3> modifyVert = null, Func<Vector2, Axis, Vector2> modifyUV = null, (float minX, float minY, float maxX, float maxY, Axis axis1, Axis axis2)? slice = null)
        {
            if (modifyVert == null)
                modifyVert = x => x;
            if (modifyUV == null)
                modifyUV = (x, y) => x;
            var slices = slice == null || slice.Value.axis1 == slice.Value.axis2 || slice.Value.axis1.Opposite() == slice.Value.axis2 ? new Axis[0] : new[] { slice.Value.axis1, slice.Value.axis2 };
            var keep = new HashSet<Axis>() { Axis.X, Axis.Y, Axis.Z, Axis.NX, Axis.NY, Axis.NZ };
            keep.RemoveWhere(slices.Contains);
            void AddFace(Axis direction, Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector2 uv4)
            {
                if (slices.Contains(direction))
                    return;
                var u = new List<Vector2>() { modifyUV(uv1, direction), modifyUV(uv2, direction), modifyUV(uv3, direction), modifyUV(uv4, direction) };
                var p = new List<(Axis[] a, Vertex v)>();
                foreach (var a in new[]
                    {
                        new[] { Axis.NX, Axis.Y, Axis.NZ },
                        new[] { Axis.NX, Axis.Y, Axis.Z },
                        new[] { Axis.X, Axis.Y, Axis.Z },
                        new[] { Axis.X, Axis.Y, Axis.NZ },
                        new[] { Axis.X, Axis.NY, Axis.NZ },
                        new[] { Axis.X, Axis.NY, Axis.Z },
                        new[] { Axis.NX, Axis.NY, Axis.Z },
                        new[] { Axis.NX, Axis.NY, Axis.NZ }
                    })
                    if (a.Contains(direction))
                        p.Add((a, (modifyVert(new Vector3((((int)a[0] / 3) == 1 ? min : max).x, (((int)a[1] / 3) == 1 ? min : max).y, (((int)a[2] / 3) == 1 ? min : max).z)), u.Take())));
                if (slice == null || slices.Contains(direction.Opposite()))
                    builder.AddSquare(p[0].v, p[1].v, p[2].v, p[3].v);
                else
                {
                    p.RemoveAll(x => slices.Contains(x.a[((int)direction + 1) % 3]) && slices.Contains(x.a[((int)direction + 2) % 3]));
                    builder.AddTriangle(p[0].v, p[1].v, p[2].v);
                }
            }
            if (top != null)
                AddFace(Axis.Y,
                    new Vector2(top.Value.maxX, top.Value.minY),
                    new Vector2(top.Value.minX, top.Value.minY),
                    new Vector2(top.Value.minX, top.Value.maxY),
                    new Vector2(top.Value.maxX, top.Value.maxY));
            if (bottom != null)
                AddFace(Axis.NY,
                    new Vector2(bottom.Value.minX, bottom.Value.maxY),
                    new Vector2(bottom.Value.maxX, bottom.Value.maxY),
                    new Vector2(bottom.Value.maxX, bottom.Value.minY),
                    new Vector2(bottom.Value.minX, bottom.Value.minY));
            if (north != null)
                AddFace(Axis.Z,
                    new Vector2(north.Value.minX, north.Value.maxY),
                    new Vector2(north.Value.maxX, north.Value.maxY),
                    new Vector2(north.Value.maxX, north.Value.minY),
                    new Vector2(north.Value.minX, north.Value.minY));
            if (east != null)
                AddFace(Axis.X,
                    new Vector2(east.Value.minX, east.Value.maxY),
                    new Vector2(east.Value.maxX, east.Value.maxY),
                    new Vector2(east.Value.maxX, east.Value.minY),
                    new Vector2(east.Value.minX, east.Value.minY));
            if (south != null)
                AddFace(Axis.NZ,
                    new Vector2(south.Value.maxX, south.Value.maxY),
                    new Vector2(south.Value.minX, south.Value.maxY),
                    new Vector2(south.Value.minX, south.Value.minY),
                    new Vector2(south.Value.maxX, south.Value.minY));
            if (west != null)
                AddFace(Axis.NX,
                    new Vector2(west.Value.minX, west.Value.maxY),
                    new Vector2(west.Value.maxX, west.Value.maxY),
                    new Vector2(west.Value.maxX, west.Value.minY),
                    new Vector2(west.Value.minX, west.Value.minY));
            // TODO: Add slice face

        }

        public static void AddWedge(this MeshBuilder builder, Vector3 min, Vector3 max, (float minX, float minY, float maxX, float maxY)? top = null, (float minX, float minY, float maxX, float maxY)? bottom = null, (float minX, float minY, float maxX, float maxY)? east = null, (float minX, float minY, float maxX, float maxY)? south = null, (float minX, float minY, float maxX, float maxY)? northwest = null, Func<Vector3, Vector3> modifyVert = null, Func<Vector2, Axis, Vector2> modifyUV = null)
        {
            if (modifyVert == null)
                modifyVert = x => x;
            if (modifyUV == null)
                modifyUV = (x, y) => x;
            if (top != null)
                builder.AddTriangle(
                    (modifyVert(new Vector3(min.x, max.y, min.z)), modifyUV(new Vector2(top.Value.minX, top.Value.minY), Axis.Y)),
                    (modifyVert(new Vector3(max.x, max.y, max.z)), modifyUV(new Vector2(top.Value.maxX, top.Value.maxY), Axis.Y)),
                    (modifyVert(new Vector3(max.x, max.y, min.z)), modifyUV(new Vector2(top.Value.maxX, top.Value.minY), Axis.Y))
                );
            if (bottom != null)
                builder.AddTriangle(
                    (modifyVert(new Vector3(min.x, min.y, min.z)), modifyUV(new Vector2(bottom.Value.maxX, bottom.Value.minY), Axis.NY)),
                    (modifyVert(new Vector3(max.x, min.y, min.z)), modifyUV(new Vector2(bottom.Value.minX, bottom.Value.minY), Axis.NY)),
                    (modifyVert(new Vector3(max.x, min.y, max.z)), modifyUV(new Vector2(bottom.Value.minX, bottom.Value.maxY), Axis.NY))
                );
            if (east != null)
                builder.AddSquare(
                    new Vertex(modifyVert(new Vector3(max.x, min.y, min.z)), modifyUV(new Vector2(east.Value.maxX, east.Value.minY), Axis.X), unique: 2),
                    new Vertex(modifyVert(new Vector3(max.x, max.y, min.z)), modifyUV(new Vector2(east.Value.maxX, east.Value.maxY), Axis.X), unique: 2),
                    new Vertex(modifyVert(new Vector3(max.x, max.y, max.z)), modifyUV(new Vector2(east.Value.minX, east.Value.maxY), Axis.X), unique: 2),
                    new Vertex(modifyVert(new Vector3(max.x, min.y, max.z)), modifyUV(new Vector2(east.Value.minX, east.Value.minY), Axis.X), unique: 2)
                );
            if (south != null)
                builder.AddSquare(
                    new Vertex(modifyVert(new Vector3(min.x, min.y, min.z)), modifyUV(new Vector2(south.Value.minX, south.Value.minY), Axis.NZ), unique: 1),
                    new Vertex(modifyVert(new Vector3(min.x, max.y, min.z)), modifyUV(new Vector2(south.Value.minX, south.Value.maxY), Axis.NZ), unique: 1),
                    new Vertex(modifyVert(new Vector3(max.x, max.y, min.z)), modifyUV(new Vector2(south.Value.maxX, south.Value.maxY), Axis.NZ), unique: 1),
                    new Vertex(modifyVert(new Vector3(max.x, min.y, min.z)), modifyUV(new Vector2(south.Value.maxX, south.Value.minY), Axis.NZ), unique: 1)
                );
            if (northwest != null)
                builder.AddSquare(
                    new Vertex(modifyVert(new Vector3(max.x, min.y, max.z)), modifyUV(new Vector2(northwest.Value.minX, northwest.Value.minY), Axis.NZ), unique: 3),
                    new Vertex(modifyVert(new Vector3(max.x, max.y, max.z)), modifyUV(new Vector2(northwest.Value.minX, northwest.Value.maxY), Axis.NZ), unique: 3),
                    new Vertex(modifyVert(new Vector3(min.x, max.y, min.z)), modifyUV(new Vector2(northwest.Value.maxX, northwest.Value.maxY), Axis.NZ), unique: 3),
                    new Vertex(modifyVert(new Vector3(min.x, min.y, min.z)), modifyUV(new Vector2(northwest.Value.maxX, northwest.Value.minY), Axis.NZ), unique: 3)
                );
        }

        public static void AddRamp(this MeshBuilder builder, Vector3 min, Vector3 max, (float minX, float minY, float maxX, float maxY)? top = null, (float minX, float minY, float maxX, float maxY)? bottom = null, (float minX, float minY, float maxX, float maxY)? north = null, (float minX, float minY, float maxX, float maxY)? south = null, (float minX, float minY, float maxX, float maxY)? west = null, Func<Vector3, Vector3> modifyVert = null, Func<Vector2, Axis, Vector2> modifyUV = null)
        {
            if (modifyVert == null)
                modifyVert = x => x;
            if (modifyUV == null)
                modifyUV = (x, y) => x;
            if (top != null)
                builder.AddSquare(
                    (modifyVert(new Vector3(min.x, max.y, min.z)), modifyUV(new Vector2(top.Value.maxX, top.Value.minY), Axis.Y)),
                    (modifyVert(new Vector3(min.x, max.y, max.z)), modifyUV(new Vector2(top.Value.minX, top.Value.minY), Axis.Y)),
                    (modifyVert(new Vector3(max.x, min.y, max.z)), modifyUV(new Vector2(top.Value.minX, top.Value.maxY), Axis.Y)),
                    (modifyVert(new Vector3(max.x, min.y, min.z)), modifyUV(new Vector2(top.Value.maxX, top.Value.maxY), Axis.Y))
                );
            if (bottom != null)
                builder.AddSquare(
                    (modifyVert(new Vector3(min.x, min.y, min.z)), modifyUV(new Vector2(bottom.Value.minX, bottom.Value.minY), Axis.NY)),
                    (modifyVert(new Vector3(max.x, min.y, min.z)), modifyUV(new Vector2(bottom.Value.minX, bottom.Value.maxY), Axis.NY)),
                    (modifyVert(new Vector3(max.x, min.y, max.z)), modifyUV(new Vector2(bottom.Value.maxX, bottom.Value.maxY), Axis.NY)),
                    (modifyVert(new Vector3(min.x, min.y, max.z)), modifyUV(new Vector2(bottom.Value.maxX, bottom.Value.minY), Axis.NY))
                );
            if (north != null)
                builder.AddTriangle(
                    new Vertex(modifyVert(new Vector3(min.x, min.y, max.z)), modifyUV(new Vector2(north.Value.minX, north.Value.minY), Axis.Z), unique: 1),
                    new Vertex(modifyVert(new Vector3(max.x, min.y, max.z)), modifyUV(new Vector2(north.Value.maxX, north.Value.minY), Axis.Z), unique: 1),
                    new Vertex(modifyVert(new Vector3(min.x, max.y, max.z)), modifyUV(new Vector2(north.Value.minX, north.Value.maxY), Axis.Z), unique: 1)
                );
            if (south != null)
                builder.AddTriangle(
                    new Vertex(modifyVert(new Vector3(min.x, min.y, min.z)), modifyUV(new Vector2(south.Value.maxX, south.Value.minY), Axis.NZ), unique: 1),
                    new Vertex(modifyVert(new Vector3(min.x, max.y, min.z)), modifyUV(new Vector2(south.Value.maxX, south.Value.maxY), Axis.NZ), unique: 1),
                    new Vertex(modifyVert(new Vector3(max.x, min.y, min.z)), modifyUV(new Vector2(south.Value.minX, south.Value.minY), Axis.NZ), unique: 1)
                );
            if (west != null)
                builder.AddSquare(
                    new Vertex(modifyVert(new Vector3(min.x, min.y, min.z)), modifyUV(new Vector2(west.Value.minX, west.Value.minY), Axis.NX), unique: 2),
                    new Vertex(modifyVert(new Vector3(min.x, min.y, max.z)), modifyUV(new Vector2(west.Value.maxX, west.Value.minY), Axis.NX), unique: 2),
                    new Vertex(modifyVert(new Vector3(min.x, max.y, max.z)), modifyUV(new Vector2(west.Value.maxX, west.Value.maxY), Axis.NX), unique: 2),
                    new Vertex(modifyVert(new Vector3(min.x, max.y, min.z)), modifyUV(new Vector2(west.Value.minX, west.Value.maxY), Axis.NX), unique: 2)
                );
        }
    }

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

    public class MeshData
    {
        Mesh[][] meshes;
        MeshBox[][][] meshboxes;
        public static implicit operator MeshData(Mesh m) => new MeshData() { meshes = new[] { new[] { m } } };
        public static implicit operator MeshData(Mesh[] m) => new MeshData() { meshes = new[] { m } };
        public static implicit operator MeshData(Mesh[][] m) => new MeshData() { meshes = m };
        public static implicit operator MeshData(MeshBox[] m) => new MeshData() { meshboxes = new[] { new[] { m } } };
        public static implicit operator MeshData(MeshBox[][] m) => new MeshData() { meshboxes = new[] { m } };
        public static implicit operator MeshData(MeshBox[][][] m) => new MeshData() { meshboxes = m };

        public Mesh[] this[int index]
        {
            get
            {
                if (meshes == null)
                {
                    if (meshboxes == null)
                        return null;
                    meshes = new Mesh[meshboxes.Length][];
                }
                if (meshes.Length <= index)
                    index = 0;
                if (meshes[index] == null)
                {
                    var b = meshboxes.GetSafe(index);
                    if (b != null)
                        meshes[index] = Main.CreateMesh(b);
                }
                return meshes[index] ?? meshes[0];
            }
        }
    }
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

    static class ExtentionMethods
    {
        public static Item_Base Clone(this Item_Base source, int uniqueIndex, string uniqueName)
        {
            Item_Base item = ScriptableObject.CreateInstance<Item_Base>();
            item.Initialize(uniqueIndex, uniqueName, source.MaxUses);
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
                    if (!f.IsStatic && f.GetValue(obj).Equals(value))
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
                    if (!f.IsStatic && f.GetValue(obj) is T && (predicate == null || predicate((T)f.GetValue(obj))))
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
            Main.createdObjects.Add(s);
            return s;
        }

        public static Texture2D GetReadable(this Texture2D source, GraphicsFormat targetFormat, bool mipChain = true, Rect? copyArea = null, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default) =>
            source.CopyTo(
                new Texture2D(
                    (int)(copyArea?.width ?? source.width),
                    (int)(copyArea?.height ?? source.height),
                    targetFormat,
                    mipChain ? TextureCreationFlags.MipChain : TextureCreationFlags.None),
                copyArea,
                format,
                readWrite);

        public static Texture2D GetReadable(this Texture2D source, TextureFormat? targetFormat = null, bool mipChain = true, Rect? copyArea = null, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default) =>
            source.CopyTo(
                new Texture2D(
                    (int)(copyArea?.width ?? source.width),
                    (int)(copyArea?.height ?? source.height),
                    targetFormat ?? TextureFormat.ARGB32,
                    mipChain),
                copyArea,
                format,
                readWrite);

        static Texture2D CopyTo(this Texture2D source, Texture2D texture, Rect? copyArea = null, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
        {
            var temp = RenderTexture.GetTemporary(source.width, source.height, 0, format, readWrite);
            Graphics.Blit(source, temp);
            temp.filterMode = FilterMode.Point;
            var prev = RenderTexture.active;
            RenderTexture.active = temp;
            var area = copyArea ?? new Rect(0, 0, temp.width, temp.height);
            texture.ReadPixels(area, 0, 0);
            texture.Apply();
            RenderTexture.active = prev;
            RenderTexture.ReleaseTemporary(temp);
            Main.createdObjects.Add(texture);
            return texture;
        }
        public static void Edit(this Texture2D baseImg)
        {
            var w = baseImg.width - 1;
            var h = baseImg.height - 1;
            for (var x = 0; x <= w; x++)
                for (var y = 0; y <= h; y++)
                    Debug.Log("ExecutionEngineException");//baseImg.SetPixel(x, y, baseImg.GetPixel(x, y).Overlay(Main.instance.overlay.GetPixelBilinear((float)x / w, (float)y / h)));
            baseImg.Apply();
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
            if (value.y < 0)
                a += Mathf.PI;
            return new Vector2(Mathf.Sin(a) * l, Mathf.Cos(a) * l);
        }
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

        public static T GetSafe<T>(this T[] c, int index)
        {
            if (c == null)
                return default;
            if (c.Length <= index)
                return default;
            return c[index];
        }

        public static Axis Opposite(this Axis axis) => (Axis)(((int)axis + 3) % 6);

        public static T Take<T>(this List<T> list)
        {
            var v = list[0];
            list.RemoveAt(0);
            return v;
        }

        public static bool Bit(this int value, byte offset) => (value & (1 << offset)) != 0;
    }

    [HarmonyPatch(typeof(ItemInstance_Buildable.Upgrade), "GetNewItemFromUpgradeItem")]
    static class Patch_GetUpgradeItem
    {
        static void Postfix(ItemInstance_Buildable.Upgrade __instance, Item_Base buildableItem, ref Item_Base __result)
        {
            var i = buildableItem == null ? null : Main.instance.items.FirstOrDefault(x => x.isUpgrade && x.item?.UniqueIndex == buildableItem.UniqueIndex)?.baseItem;
            if (i)
            {
                foreach (var p in Main.instance.items)
                    if ((p as BlockItemCreation)?.upgradeItem == buildableItem.UniqueIndex && p.item && p.baseItem?.settings_buildable?.Upgrades == __instance)
                    {
                        __result = p.item;
                        return;
                    }
                foreach (var p in Main.instance.items)
                    if ((p as BlockItemCreation)?.upgradeItem == buildableItem.UniqueIndex && p.item && (p.baseItem?.settings_buildable?.Upgrades?.HasFieldValueMatch<Item_Base>(x => x?.settings_buildable?.Upgrades == __instance) ?? false))
                    {
                        __result = p.item;
                        return;
                    }
                var b = __instance.GetNewItemFromUpgradeItem(i);
                foreach (var p in Main.instance.items)
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
            if (!__result && buildableItem != null && Main.instance.items.Any(x => x.isUpgrade && x.item?.UniqueIndex == buildableItem.UniqueIndex))
                __result = true;
        }
    }

    [HarmonyPatch(typeof(Block), "IsWalkable")]
    static class Patch_Block
    {
        static void Postfix(Block __instance, ref bool __result)
        {
            foreach (var p in Main.instance.items)
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
            if (__result == -1 && __instance == Main.instance.language)
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