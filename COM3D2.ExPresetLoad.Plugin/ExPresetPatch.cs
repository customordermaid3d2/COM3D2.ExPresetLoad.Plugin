using BepInEx;
using CM3D2.ExternalPreset.Managed;
using CM3D2.ExternalSaveData.Managed;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;

namespace COM3D2.ExPresetLoad.Plugin
{
    [BepInPlugin("COM3D2.ExPresetLoad.Plugin", "COM3D2.ExPresetLoad.Plugin", "1.0")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    [BepInProcess("COM3D2x64.exe")]
    public class ExPresetPatch : BaseUnityPlugin
    {

        public void Awake()
        {
            Debug.Log("ExPresetPatch.Awake " );
            try
            {
                Harmony.CreateAndPatchAll(typeof(ExPresetPatch));
            }
            catch (Exception e)
            {
                Debug.LogError("ExPresetPatch.Awake "+e.ToString());
            }
        }

        [HarmonyPatch(typeof(CharacterMgr), "PresetSet", new Type[] { typeof(Maid), typeof(CharacterMgr.Preset) })]
        [HarmonyPostfix]
        public static void PresetSet(Maid f_maid, CharacterMgr.Preset f_prest)
        {
            ExPreset.Load(f_maid, f_prest);
        }
    }
}
