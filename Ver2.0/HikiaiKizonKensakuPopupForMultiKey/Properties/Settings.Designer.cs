﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HikiaiKizonKensakuPopupForMultiKey.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool HYOUJI_JOUKEN_TEKIYOU {
            get {
                return ((bool)(this["HYOUJI_JOUKEN_TEKIYOU"]));
            }
            set {
                this["HYOUJI_JOUKEN_TEKIYOU"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool HYOUJI_JOUKEN_DELETED {
            get {
                return ((bool)(this["HYOUJI_JOUKEN_DELETED"]));
            }
            set {
                this["HYOUJI_JOUKEN_DELETED"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool HYOUJI_JOUKEN_TEKIYOUGAI {
            get {
                return ((bool)(this["HYOUJI_JOUKEN_TEKIYOUGAI"]));
            }
            set {
                this["HYOUJI_JOUKEN_TEKIYOUGAI"] = value;
            }
        }
    }
}