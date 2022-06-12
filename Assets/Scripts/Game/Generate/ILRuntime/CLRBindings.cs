using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            System_String_Binding.Register(app);
            UnityGameFramework_Runtime_Log_Binding.Register(app);
            System_Char_Binding.Register(app);
            Game_LoadHotfixDataTableUserData_Binding.Register(app);
            GameFramework_DataTable_DataTableBase_Binding.Register(app);
            Game_AwaitableExtension_Binding.Register(app);
            System_Type_Binding.Register(app);
            UnityGameFramework_Runtime_DataTableComponent_Binding.Register(app);
            Game_UIExtension_Binding.Register(app);
            UnityEngine_LayerMask_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            UnityGameFramework_Runtime_EntityComponent_Binding.Register(app);
            UnityGameFramework_Runtime_EntityLogic_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            UnityGameFramework_Runtime_Entity_Binding.Register(app);
            Game_GameEntry_Binding.Register(app);
            System_Int32_Binding.Register(app);
            Game_DREntity_Binding.Register(app);
            Game_AssetUtility_Binding.Register(app);
            System_Exception_Binding.Register(app);
            System_Threading_Tasks_Task_Binding.Register(app);
            System_EventHandler_1_ILTypeInstance_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            GameFramework_GameFrameworkException_Binding.Register(app);
            GameFramework_Utility_Binding_Text_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_ILTypeInstance_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Variable_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding.Register(app);
            Game_HotfixComponent_Binding.Register(app);
            System_Collections_Generic_List_1_Type_Binding.Register(app);
            System_Collections_Generic_List_1_Type_Binding_Enumerator_Binding.Register(app);
            System_Activator_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneSuccessEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_EventComponent_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneFailureEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneUpdateEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneDependencyAssetEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_SoundComponent_Binding.Register(app);
            UnityGameFramework_Runtime_SceneComponent_Binding.Register(app);
            UnityGameFramework_Runtime_BaseComponent_Binding.Register(app);
            UnityGameFramework_Runtime_VarInt32_Binding.Register(app);
            Game_DRScene_Binding.Register(app);
            Game_SoundExtension_Binding.Register(app);
            System_Single_Binding.Register(app);
            UnityEngine_Application_Binding.Register(app);
            UnityGameFramework_Runtime_VarObject_Binding.Register(app);
            GameFramework_Variable_1_Object_Binding.Register(app);
            UnityGameFramework_Runtime_LoadConfigSuccessEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadConfigFailureEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadDictionarySuccessEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadDictionaryFailureEventArgs_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Boolean_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Boolean_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_Boolean_Binding.Register(app);
            UnityGameFramework_Runtime_ConfigComponent_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Type_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Type_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_Type_Binding.Register(app);
            UnityGameFramework_Runtime_LocalizationComponent_Binding.Register(app);
            GameFramework_Resource_LoadAssetCallbacks_Binding.Register(app);
            UnityGameFramework_Runtime_ResourceComponent_Binding.Register(app);
            Game_UGuiForm_Binding.Register(app);
            UnityEngine_Input_Binding.Register(app);
            System_Collections_Generic_ICollection_1_KeyValuePair_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_IEnumerable_1_KeyValuePair_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_IEnumerator_1_KeyValuePair_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_IEnumerator_Binding.Register(app);
            System_Collections_Generic_IDictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Queue_1_ILTypeInstance_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_LinkedList_1_EventHandler_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_LinkedList_1_EventHandler_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_LinkedListNode_1_EventHandler_1_ILTypeInstance_Binding.Register(app);
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
