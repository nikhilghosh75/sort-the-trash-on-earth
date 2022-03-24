using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

/*
 * Adapted from Arbor Interactive
 * https://f002.backblazeb2.com/file/sharex-hN8T5vpN8wZGmmwU/2021/October/07/02/16/12/535/fd64f5f0-9586-4ad5-b2c6-4a1afa834250/ArborScriptExecutionOrderSync.cs
 */

[InitializeOnLoad]
public class ScriptExecutionOrder
{
    [UnityEditor.Callbacks.DidReloadScripts]
    static void OnScriptsReloaded()
    {
        Refresh();
    }

    static void Refresh()
    {
        if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            SyncExecutionOrderBetweenEditorAndBuild();
    }

    [MenuItem("Dev Tools/Force Execution Order Sync")]
    public static void SyncExecutionOrderBetweenEditorAndBuild()
    {
        // MonoImporter.SetExecutionOrder can cause script reload and cause infinite loops
        if (EditorPrefs.HasKey("syncing_execution_order_operations_remaining") 
            && EditorPrefs.GetInt("syncing_execution_order_operations_remaining") > 0)
        {
            int remainingCount = EditorPrefs.GetInt("syncing_execution_order_operations_remaining");
            EditorPrefs.SetInt("syncing_execution_order_operations_remaining", remainingCount - 1);
            return;
        }

        EditorApplication.LockReloadAssemblies();

        try
        {
            Debug.Log("Beginning Syncronization");

            List<MonoScript> monoScripts = new List<MonoScript>(MonoImporter.GetAllRuntimeMonoScripts());

            int compare(MonoScript a, MonoScript b)
            {
                return a.name.CompareTo(b.name);
            }
            monoScripts.Sort(compare);

            Debug.Log("Identified " + monoScripts.Count + " total MonoBehaviors");

            monoScripts = SanitizeMonoScriptList(monoScripts);

            Debug.Log("Identifying developer-configured (thus unavailable) indeces...");

            ScriptExecutionTakenIndexResult takenIndecesResult = GetUnavailableScriptExecutionIndeces(monoScripts);

            foreach(MonoScript monoScript in takenIndecesResult.customized_monoscripts)
            {
                if (monoScripts.Contains(monoScript))
                    monoScripts.Remove(monoScript);
            }

            EditorPrefs.SetInt("syncing_execution_order_operations_remaining", monoScripts.Count);
            Debug.Log("Adjusting script execution order for " + monoScripts.Count + " scripts");
            
            int orderIndex = 1;
            foreach (MonoScript monoScript in monoScripts)
            {
                while (takenIndecesResult.taken_indeces.Contains(orderIndex))
                    orderIndex++;

                MonoImporter.SetExecutionOrder(monoScript, orderIndex);
                takenIndecesResult.taken_indeces.Add(orderIndex);
            }

            Debug.Log("Finished script execution order changes!");
        }
        catch (Exception e)
        {
            Debug.LogError("[ArborExtensionsEditorSetup.SyncronizeExecutionOrderBetweenEditorAndBuilds] Something went wrong. Contact ayarger@umich.edu. Here's the message [" + e.Message + "] and stacktrace [" + e.StackTrace + "]");
            EditorPrefs.SetInt("syncing_execution_order_operations_remaining", 0);
        }

        EditorApplication.UnlockReloadAssemblies();
    }

    // Remove scripts that don't have a valid type
    // This seems like something that shouldn't be possible
    static List<MonoScript> SanitizeMonoScriptList(List<MonoScript> monoScripts)
    {
        for (int i = monoScripts.Count - 1; i >= 0; i--)
        {
            MonoScript monoScript = monoScripts[i];

            System.Type monobehaviorType = monoScript.GetClass();
            if(monobehaviorType == null)
            {
                monoScripts.RemoveAt(i);
                continue;
            }

            Assembly assem = Assembly.GetAssembly(monobehaviorType);
            if(!assem.FullName.Contains("Assembly-CSharp"))
            {
                monoScripts.RemoveAt(i);
                continue;
            }
        }
        return monoScripts;
    }

    // Obtain all "currently-taken' script execution order indeces
    // This includes scripts that have been given an explicit index via the Unity Editor or the DefaultExecutionOrder attribute
    static ScriptExecutionTakenIndexResult GetUnavailableScriptExecutionIndeces(List<MonoScript> monoScripts)
    {
        ScriptExecutionTakenIndexResult result = new ScriptExecutionTakenIndexResult();

        foreach (MonoScript monoScript in monoScripts)
        {
            System.Type monobehaviorType = monoScript.GetClass();
            Attribute[] attrs = Attribute.GetCustomAttributes(monobehaviorType);
            foreach (Attribute attr in attrs)
            {
                DefaultExecutionOrder deo = attr as DefaultExecutionOrder;

                if(deo != null)
                {
                    if(deo.order != 0)
                    {
                        result.taken_indeces.Add(deo.order);
                        result.customized_monoscripts.Add(monoScript);
                    }
                    break;
                }
            }

            int currentOrder = MonoImporter.GetExecutionOrder(monoScript);
            if(currentOrder != 0)
            {
                result.taken_indeces.Add(currentOrder);
                result.customized_monoscripts.Add(monoScript);
            }
        }

        if (result.taken_indeces.Contains(0))
            result.taken_indeces.Remove(0);

        return result;
    }
}

public class ScriptExecutionTakenIndexResult
{
    public HashSet<int> taken_indeces = new HashSet<int>();
    public HashSet<MonoScript> customized_monoscripts = new HashSet<MonoScript>();
}