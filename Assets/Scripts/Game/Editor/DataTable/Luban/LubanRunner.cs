using System.Diagnostics;
using GameFramework;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Game.Editor.DataTableTools
{
    public sealed class LubanRunner
    {
        public static void GenerateBin()
        {
#if UNITY_EDITOR_WIN
            RunGenerateCommand("gen_code_bin.bat");
#endif

#if UNITY_EDITOR_OSX
            RunGenerateCommand("gen_code_bin.sh");
#endif
        }
        
        public static void GenerateJson()
        {
#if UNITY_EDITOR_WIN
            RunGenerateCommand("gen_code_json.bat");
#endif
            
#if UNITY_EDITOR_OSX
            RunGenerateCommand("gen_code_json.sh");
#endif
        }

        private static void RunGenerateCommand(string fileName)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.WorkingDirectory = Utility.Path.GetRegularPath(Application.dataPath + "/../Tools/Luban/");
            psi.FileName = Utility.Path.GetRegularPath(psi.WorkingDirectory + fileName);
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            Process process = new Process();
            process.StartInfo = psi;
            process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs args)
            {
                if (args != null && !string.IsNullOrEmpty(args.Data))
                {
                    Debug.Log(args.Data);
                }
            };
            process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs args)
            {
                if (args != null && !string.IsNullOrEmpty(args.Data))
                {
                    Debug.LogError(args.Data);
                }
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
    }
}
