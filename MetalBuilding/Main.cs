using HarmonyLib;
using HMLLibrary;
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
using UnityEngine.SceneManagement;
using I2.Loc;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using static BlockCreator;


namespace MoreBuilding
{
    public class Main : Mod
    {
        public static float diagonalMagnitude = Vector2.one.magnitude;
        public static Vector3 diagonalScale = new Vector3(Vector2.one.magnitude,1,1);
        public ItemCreation[] items = new[]
        {
            new ItemCreation() { baseIndex = 546, loadIcon = true, uniqueIndex = 160546, uniqueName = "Block_Upgrade_ScrapMetal", isUpgrade = true, localization = "Replaced with scrap metal@" },
            new ItemCreation() { baseIndex = 548, loadIcon = true, uniqueIndex = 160548, uniqueName = "Block_Upgrade_Metal", isUpgrade = true, localization = "Replaced with solid metal@" },
            new ItemCreation() { baseIndex = 548, loadIcon = true, uniqueIndex = 6969, uniqueName = "Block_Upgrade_Metal", isUpgrade = true, localization = "Replaced with solid metal@" },
            new BlockItemCreation() {
                baseIndex = 1, uniqueIndex = 160001, uniqueName = "Block_Floor_ScrapMetal", localization = "Scrap Metal Floor@Used to build additional floors and roof. Cannot be built in thin air",
                meshData = new MeshData[] { new[] {MeshBox.Presets.ThinPlatform(0.15f,0.1f)} },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 2, uniqueIndex = 160002, uniqueName = "Block_Foundation_ScrapMetal", localization = "Scrap Metal Foundation@Used to expand your raft on the bottom floor",
                meshData = new MeshData[] {new[] {MeshBox.Presets.ThickPlatform(HalfFloorHeight / 2,0.25f)} },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                additionEdits = x => {
                    var f = x as Block_Foundation;
                    f.meshRenderer = f.GetComponentInChildren<MeshRenderer>();
                    var meshFilter = f.GetComponentInChildren<MeshFilter>();
                    Traverse.Create(f).Field("meshFilter").SetValue(meshFilter);
                    f.armoredMesh = meshFilter.sharedMesh;
                    Traverse.Create(f).Field("defaultMesh").SetValue(meshFilter.sharedMesh);
                    Traverse.Create(f).Field("defaultMaterial").SetValue(f.meshRenderer.sharedMaterial);
                    Traverse.Create(f).Field("armoredMaterial").SetValue(f.meshRenderer.sharedMaterial);
                    Traverse.Create(f).Field("reinforced").SetValue(true);
                }
            },
            new BlockItemCreation() {
                baseIndex = 3, uniqueIndex = 160003, uniqueName = "Block_Stair_ScrapMetal", localization = "Scrap Metal Stair@Great to get from one floor to another",
                meshData = new MeshData[] {new[] {new MeshBox(
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
                baseIndex = 4, uniqueIndex = 160004, uniqueName = "Block_Wall_ScrapMetal", localization = "Scrap Metal Wall@Provides support for additional floors",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,-0.04f),
                    new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,2), 0, 0.9f, 0.1f ))} },
                upgradeItem = 160546, material = () => instance.ScrapMetal, modelScales = new[] { Vector3.one, diagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 82, uniqueIndex = 160082, uniqueName = "Block_Wall_Window_ScrapMetal", localization = "Scrap Metal Window@Provides support for additional floors",
                meshData = new MeshData[] {new[] {new MeshBox(
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
                baseIndex = 84, uniqueIndex = 160084, uniqueName = "Block_Pillar_ScrapMetal", localization = "Scrap Metal Pillar@Provides support for additional floors",
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {
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
                meshData = new MeshData[] {new[] {
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
                baseIndex = 144, uniqueIndex = 160144, uniqueName = "Block_HalfWall_ScrapMetal", localization = "Half Scrap Metal Wall@Provides support for additional floors",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,0),
                    new Vector3(HalfBlockSize,HalfFloorHeight-0.05f,0.04f),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,1), 0, 0.9f, 0.1f ))} },
                upgradeItem = 160546, material = () => instance.ScrapMetal, modelScales = new[] { Vector3.one, diagonalScale }
            },
            new BlockItemCreation() {
                baseIndex = 146, uniqueIndex = 160146, uniqueName = "Block_HalfPillar_ScrapMetal", localization = "Half Scrap Metal Pillar@Provides support for additional floors",
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> (x.x < 0 && x.z > 0) ? x + Vector3.up * HalfFloorHeight : x)}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 152, uniqueIndex = 160152, uniqueName = "Block_Wall_Slope_ScrapMetal", localization = "Triangular Scrap Metal Wall@Fills triangular holes",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.04f,0.05f-HalfFloorHeight),
                    new Vector3(HalfBlockSize,0,0.05f),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90,true, DifferentEnds: true), true, Quaternion.Euler(0,0,180) * Quaternion.Euler(-90,0,0))} },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 160, uniqueIndex = 160160, uniqueName = "Block_Roof_InvCorner_ScrapMetal", localization = "Inverted Scrap Metal Roof Corner@Covers your head",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), x=> Quaternion.Euler(0, 90, 0) * ((x.x < 0 || x.z > 0) ? x + Vector3.up * HalfFloorHeight : x))}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 189, uniqueIndex = 160189, uniqueName = "Block_Foundation_Triangular_ScrapMetal", localization = "Triangular Scrap Metal Foundation@Used to expand your raft on the bottom floor",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight / 2,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,1,0,0), new Vector2(0.75f,1), -0.05f, 1, 1 ), true, Quaternion.Euler(0,180,0))} },
                upgradeItem = 160546, material = () => instance.ScrapMetal,
                additionEdits = x => {
                    var f = x as Block_Foundation;
                    f.meshRenderer = f.GetComponentInChildren<MeshRenderer>();
                    var meshFilter = f.GetComponentInChildren<MeshFilter>();
                    Traverse.Create(f).Field("meshFilter").SetValue(meshFilter);
                    f.armoredMesh = meshFilter.sharedMesh;
                    Traverse.Create(f).Field("defaultMesh").SetValue(meshFilter.sharedMesh);
                    Traverse.Create(f).Field("defaultMaterial").SetValue(f.meshRenderer.sharedMaterial);
                    Traverse.Create(f).Field("armoredMaterial").SetValue(f.meshRenderer.sharedMaterial);
                    Traverse.Create(f).Field("reinforced").SetValue(true);
                }
            },
            new BlockItemCreation() {
                baseIndex = 191, uniqueIndex = 160191, uniqueName = "Block_Floor_Triangular_ScrapMetal", localization = "Triangular Scrap Metal Floor@Used to build additional floors and roof. Cannot be built in thin air",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,1,0,0), new Vector2(0,0.1f), 0, 1, 1, -90 ), true, Quaternion.Euler(0,180,0))} },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 193, uniqueIndex = 160193, uniqueName = "Block_HalfFloor_Triangular_ScrapMetal", localization = "Triangular Scrap Metal Raised Floor@A floor half the height of a wall",
                meshData = new MeshData[] {new[] {
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
                meshData = new MeshData[] {new[] {
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
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(0.006f-HalfBlockSize,0,-0.051f-HalfFloorHeight),
                    new Vector3(HalfBlockSize+0.006f,0.04f,-0.051f),
                    new UVData(new Vector4(0,1,0.9f,0), new Vector2(0,0.1f), 0, 1, 1, -90, true, true), true, Quaternion.Euler(90,180,0))} },
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 488, uniqueIndex = 160488, uniqueName = "Block_Roof_ScrapMetal_TJunction", localization = "Scrap Metal T-Junction@Covers your head",
                meshData = new MeshData[] {new[] {new MeshBox(
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
                baseIndex = 491, uniqueIndex = 160491, uniqueName = "Block_Wall_Window_ScrapMetal_Half", localization = "Half Scrap Metal Window@Provides support for additional floors",
                meshData = new MeshData[] {new[] {new MeshBox(
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
                baseIndex = 497, uniqueIndex = 160497, uniqueName = "Block_Roof_ScrapMetal_XJunction", localization = "Scrap Metal X-Junction@Covers your head",
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
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
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0, 0.9f, 0.1f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeN = true, excludeS = true }),
                new MeshBox(
                    new Vector3(0.1f, 0, -0.075f),
                    new Vector3(0.25f,BlockSize,0.075f),
                    new UVData(new Vector4(0.9f,0,1,0.1f), new Vector2(0,1), 0.9f, 0.1f, 0.9f ), Rotation: Quaternion.Euler(0,0,-90), Faces: new FaceChanges() { excludeE = true, excludeW = true, excludeD = true, excludeU = true })}},
                upgradeItem = 160546, material = () => instance.ScrapMetal
            },
            new BlockItemCreation() {
                baseIndex = 2, uniqueIndex = 6971, uniqueName = "Block_Foundation_Glass", localization = "Glass Foundation@Used to expand your raft on the bottom floor",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight / 2,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0.75f,1), -0.05f, 1, 1 ))} },
                upgradeItem = 160546, material = () => instance.Glass,
                additionEdits = x => {
                    var f = x as Block_Foundation;
                    f.meshRenderer = f.GetComponentInChildren<MeshRenderer>();
                    var meshFilter = f.GetComponentInChildren<MeshFilter>();
                    Traverse.Create(f).Field("meshFilter").SetValue(meshFilter);
                    f.armoredMesh = meshFilter.sharedMesh;
                    Traverse.Create(f).Field("defaultMesh").SetValue(meshFilter.sharedMesh);
                    Traverse.Create(f).Field("defaultMaterial").SetValue(f.meshRenderer.sharedMaterial);
                    Traverse.Create(f).Field("armoredMaterial").SetValue(f.meshRenderer.sharedMaterial);
                    Traverse.Create(f).Field("reinforced").SetValue(true);
                }
            },
            new BlockItemCreation() {
                baseIndex = 189, uniqueIndex = 6972, uniqueName = "Block_Foundation_Triangular_Glass", localization = "Triangular Glass Foundation@Used to expand your raft on the bottom floor",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-HalfFloorHeight / 2,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0.9f,1,0,0), new Vector2(0.75f,1), -0.05f, 1, 1 ), true, Quaternion.Euler(0,180,0))} },
                upgradeItem = 160546, material = () => instance.Glass,
                additionEdits = x => {
                    var f = x as Block_Foundation;
                    f.meshRenderer = f.GetComponentInChildren<MeshRenderer>();
                    var meshFilter = f.GetComponentInChildren<MeshFilter>();
                    Traverse.Create(f).Field("meshFilter").SetValue(meshFilter);
                    f.armoredMesh = meshFilter.sharedMesh;
                    Traverse.Create(f).Field("defaultMesh").SetValue(meshFilter.sharedMesh);
                    Traverse.Create(f).Field("defaultMaterial").SetValue(f.meshRenderer.sharedMaterial);
                    Traverse.Create(f).Field("armoredMaterial").SetValue(f.meshRenderer.sharedMaterial);
                    Traverse.Create(f).Field("reinforced").SetValue(true);
                }
            },
            new BlockItemCreation() {
                baseIndex = 1, uniqueIndex = 6974, uniqueName = "Block_Floor_Glass", localization = "Glass Floor@Used to build additional floors and roof. Cannot be built in thin air",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.15f,-HalfBlockSize),
                    new Vector3(HalfBlockSize,0,HalfBlockSize),
                    new UVData(new Vector4(0,0,0.9f,1), new Vector2(0,0.1f), 0, 1, 1, -90 ))} },
                upgradeItem = 160546, material = () => instance.Glass
            },
            new BlockItemCreation() {
                baseIndex = 4, uniqueIndex = 6976, uniqueName = "Block_Wall_Glass", localization = "Glass Wall@Provides support for additional floors",
                meshData = new MeshData[] {new[] {new MeshBox(
                    new Vector3(-HalfBlockSize,-0.05f,-0.04f),
                    new Vector3(HalfBlockSize,FullFloorHeight-0.05f,0),
                    new UVData(new Vector4(0.9f,1,1,0), new Vector2(0,2), 0, 0.9f, 0.1f ))} },
                upgradeItem = 160546, material = () => instance.Glass, modelScales = new[] { Vector3.one, diagonalScale }
            }
        };
        Material Glass;
        Material ScrapMetal;
        Material Metal;
        public LanguageSourceData language;
        public List<Object> createdObjects = new List<Object>();
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
                p[i] = new Color(p[i].r / 2, p[i].g, 0, 0.5f);
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
            harmony.UnpatchAll(harmony.Id);
            LocalizationManager.Sources.Remove(language);
            foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockQuadType>())
                    Traverse.Create(q).Field("acceptableBlockTypes").GetValue<List<Item_Base>>().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
            foreach (var q in Resources.FindObjectsOfTypeAll<SO_BlockCollisionMask>())
                    Traverse.Create(q).Field("blockTypesToIgnore").GetValue<List<Item_Base>>().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
            ItemManager.GetAllItems().RemoveAll(x => items.Any(y => y.item?.UniqueIndex == x.UniqueIndex));
            foreach (var b in GetPlacedBlocks())
                if (b.buildableItem != null && items.Any(y => y.item?.UniqueIndex == b.buildableItem.UniqueIndex))
                    RemoveBlock(b, null, false);
            foreach (var o in createdObjects)
                if (o is AssetBundle)
                    (o as AssetBundle).Unload(true);
                else
                    Destroy(o);
            createdObjects.Clear();
            Log("Mod has been unloaded!");
        }

        public void CreateItem(ItemCreation item)
        {
            item.item = item.baseItem.Clone(item.uniqueIndex, item.uniqueName);
            //var l = QualitySettings.masterTextureLimit;
            //QualitySettings.masterTextureLimit = 0;
            if (item.loadIcon)
                Debug.Log("not loaded image");// item.item.settings_Inventory.Sprite = LoadImage(item.uniqueName + ".png").ToSprite();
            else
            {
                var t = item.item.settings_Inventory.Sprite.texture.GetReadable(item.item.settings_Inventory.Sprite.rect);
                var p = t.GetPixels(0);
                for (int i = 0; i < p.Length; i++)
                    p[i] = new Color(p[i].grayscale, p[i].grayscale, p[i].grayscale, p[i].a);
                var t2 = new Texture2D(t.width, t.height, t.format, false);
                t2.SetPixels(p);
                t2.Apply(true, true);
                item.item.settings_Inventory.Sprite = t2.ToSprite();
                Destroy(t);
            }
            //QualitySettings.masterTextureLimit = l;
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
                var meshes = CreateMesh(blockCreation.meshData[0].meshes);
                var material = blockCreation.material();
                for (int i = 0; i < p.Length; i++)
                {
                    var me = blockCreation.meshData.Length <= i || blockCreation.meshData[i] == null ? meshes : CreateMesh(blockCreation.meshData[i].meshes);
                    p[i] = Instantiate(p[i], prefabHolder, false);
                    var r = p[i].GetComponentsInChildren<Renderer>(true);
                    for (int j = 0; j < r.Length; j++)
                        if (me.Length > j && me[j] != null)
                        {
                            r[j].sharedMaterial = material;
                            if (r[j] is SkinnedMeshRenderer)
                                (r[j] as SkinnedMeshRenderer).sharedMesh = me[j];
                            else
                                r[j].GetComponent<MeshFilter>().sharedMesh = me[j];
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
            var m = new Mesh();
            var v = new List<Vector3>();
            var u = new List<Vector2>();
            var t = new List<int>();
            foreach(var d in data)
            {
                var min = d.min;
                var max = d.max;
                var uv = d.uv;
                var c = v.Count;
                foreach (var i in new[]
                    {
                        max,new Vector3(min.x,max.y,max.z),new Vector3(min.x,min.y,max.z),new Vector3(max.x,min.y,max.z),
                        new Vector3(max.x,max.y,min.z),new Vector3(min.x,max.y,min.z),min,new Vector3(max.x,min.y,min.z),

                        max,new Vector3(min.x,max.y,max.z),new Vector3(min.x,min.y,max.z),new Vector3(max.x,min.y,max.z),
                        new Vector3(max.x,max.y,min.z),new Vector3(min.x,max.y,min.z),min,new Vector3(max.x,min.y,min.z),

                        max,new Vector3(max.x,min.y,max.z)
                    })
                    v.Add(d.ModifyVerts(i));
                if (d.wedge)
                {
                    u.AddRange(new[]
                    {
                        new Vector2(uv.xStart,uv.Vertical.y).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face2Width,uv.Vertical.y).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face2Width,uv.Vertical.x).Rotate(uv.Rot),new Vector2(uv.xStart,uv.Vertical.x).Rotate(uv.Rot),
                        new Vector2(uv.xStart+uv.Face1Width+uv.Face2Width,uv.Vertical.y).Rotate(uv.Rot),Vector2.zero,Vector2.zero,new Vector2(uv.xStart+uv.Face1Width+uv.Face2Width,uv.Vertical.x).Rotate(uv.Rot),

                        uv.EndMin,uv.EndMinMax,uv.AltEndUV ? uv.EndMin : uv.EndMinMax,uv.AltEndUV ? uv.EndMinMax : uv.EndMin,
                        uv.EndMaxMin,Vector2.zero,Vector2.zero,uv.AltEndUV ? uv.EndMax : uv.EndMaxMin,

                        new Vector2(uv.xStart+uv.Face1Width+uv.Face2Width*2,uv.Vertical.y).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face1Width+uv.Face2Width*2,uv.Vertical.x).Rotate(uv.Rot)
                    });
                    if (!d.faces.excludeN)
                        foreach (var i in new[] { 0, 1, 2, 0, 2, 3 })
                            t.Add(c + i);
                    if (!d.faces.excludeE)
                        foreach (var i in new[] { 16, 17, 7, 16, 7, 4 })
                            t.Add(c + i);
                    if (!d.faces.excludeS && !d.faces.excludeW)
                        foreach (var i in new[] { 7, 2, 1, 7, 1, 4 })
                            t.Add(c + i);
                    if (!d.faces.excludeU)
                        foreach (var i in new[] { 9, 8, 12 })
                            t.Add(c + i);
                    if (!d.faces.excludeD)
                        foreach (var i in new[] { 11, 10, 15 })
                            t.Add(c + i);
                }
                else
                {
                    u.AddRange(new[]
                    {
                        new Vector2(uv.xStart,uv.Vertical.y).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face1Width,uv.Vertical.y).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face1Width,uv.Vertical.x).Rotate(uv.Rot),new Vector2(uv.xStart,uv.Vertical.x).Rotate(uv.Rot),
                        new Vector2(uv.xStart+uv.Face1Width*2+uv.Face2Width,uv.Vertical.y).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face1Width+uv.Face2Width,uv.Vertical.y).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face1Width+uv.Face2Width,uv.Vertical.x).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face1Width*2+uv.Face2Width,uv.Vertical.x).Rotate(uv.Rot),

                        uv.EndMin,uv.EndMinMax,uv.AltEndUV ? uv.EndMin : uv.EndMinMax,uv.AltEndUV ? uv.EndMinMax : uv.EndMin,
                        uv.EndMaxMin,uv.EndMax,uv.AltEndUV ? uv.EndMaxMin : uv.EndMax,uv.AltEndUV ? uv.EndMax : uv.EndMaxMin,

                        new Vector2(uv.xStart+uv.Face1Width*2+uv.Face2Width*2,uv.Vertical.y).Rotate(uv.Rot),new Vector2(uv.xStart+uv.Face1Width*2+uv.Face2Width*2,uv.Vertical.x).Rotate(uv.Rot)
                    });
                    if (!d.faces.excludeN)
                        foreach (var i in new[] { 0, 1, 2, 0, 2, 3 })
                            t.Add(c + i);
                    if (!d.faces.excludeE)
                        foreach (var i in new[] { 16, 17, 7, 16, 7, 4 })
                            t.Add(c + i);
                    if (!d.faces.excludeS)
                        foreach (var i in new[] { 7, 6, 5, 7, 5, 4 })
                            t.Add(c + i);
                    if (!d.faces.excludeW)
                        foreach (var i in new[] { 1, 5, 6, 1, 6, 2 })
                            t.Add(c + i);
                    if (!d.faces.excludeU)
                        foreach (var i in new[] { 9, 8, 12, 9, 12, 13 })
                            t.Add(c + i);
                    if (!d.faces.excludeD)
                        foreach (var i in new[] { 11, 10, 15, 10, 14, 15 })
                            t.Add(c + i);
                }
            }
            var e = new List<int>();
            for (int j = 0; j < v.Count; j++)
            {
                if (e.Contains(j)) continue;
                for (int i = j + 1; i < v.Count; i++)
                    if (v[i] == v[j] && u[i] == u[j])
                    {
                        e.Add(i);
                        for (int k = 0; k < t.Count; k++)
                            if (t[k] == i)
                                t[k] = j;
                    }
            }
            for (int j = v.Count - 1; j >= 0; j--)
                if (!t.Any(x => x == j))
                {
                    v.RemoveAt(j);
                    u.RemoveAt(j);
                    for (int i = 0; i < t.Count; i++)
                        if (t[i] >= j)
                            t[i] -= 1;
                }
            m.vertices = v.ToArray();
            m.uv = u.ToArray();
            m.triangles = t.ToArray();
            m.RecalculateBounds();
            m.RecalculateNormals();
            m.RecalculateTangents();
            instance.createdObjects.Add(m);
            return m;
        }

        List<(Item_Base, Item_Base,bool)> ModUtils_BuildMenuItems()
        {
            if (!loaded) return null;
            var l = new List<(Item_Base, Item_Base, bool)>();
            foreach (var i in items)
                if (i.baseItem && i.item)
                    l.Add((i.baseItem, i.item, i.isUpgrade));
            return l;
        }

        void ModUtils_ReloadBuildMenu() { }
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
        public MeshData[] meshData;
        public int upgradeItem;
        public Func<Material> material;
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

    public class MeshData
    {
        public MeshBox[][] meshes;
        public static implicit operator MeshData(MeshBox[] m) => new MeshData() { meshes = new[] { m } };
        public static implicit operator MeshData(MeshBox[][] m) => new MeshData() { meshes = m };

        public static class Presets
        {
            
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
            Main.instance.createdObjects.Add(s);
            return s;
        }


        public static Texture2D GetReadable(this Texture2D source, Rect? copyArea = null, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, TextureFormat? targetFormat = null, bool mipChain = true)
        {
            var temp = RenderTexture.GetTemporary(source.width, source.height, 0, format, readWrite);
            Graphics.Blit(source, temp);
            temp.filterMode = FilterMode.Point;
            var prev = RenderTexture.active;
            RenderTexture.active = temp;
            var area = copyArea ?? new Rect(0, 0, temp.width, temp.height);
            var texture = new Texture2D((int)area.width, (int)area.height, targetFormat ?? TextureFormat.RGBA32, mipChain);
            texture.ReadPixels(area, 0, 0);
            texture.Apply();
            RenderTexture.active = prev;
            RenderTexture.ReleaseTemporary(temp);
            Main.instance.createdObjects.Add(texture);
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

        public static Vector3 Multiply(this Vector3 value, Vector3 scale) => new Vector3(value.x * scale.x, value.y * scale.y, value.z * scale.z);
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
                if (p.item.UniqueIndex == __instance.buildableItem?.UniqueIndex)
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