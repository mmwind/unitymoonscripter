using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace Assets
{
    public class Loader : IScriptLoader
    {

        public List<string> ModulePaths = new List<string>();
        private string lastPath;
        public Loader()
        {
            ModulePaths.Add("");
            lastPath = "./";
        }

        public object LoadFile(string file, Table globalContext)
        {
            //throw new System.NotImplementedException();
            // Debug.Log($"Load FILE {file}");
            return new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        private string saveLastPathAndReturn(string fname)
        {
            //Debug.Log($"saveAndReturn {lastPath}");
            lastPath = Path.GetDirectoryName(fname)+'/';
            return fname;
        }
        public string ResolveFileName(string filename, Table globalContext)
        {
            //Debug.Log($"ResolveFileName {filename}");
            List<string> mp = new List<string>(ModulePaths);
            mp.Add(lastPath);
            foreach (string path in mp)
            {
                 string fname = path + filename;
                 if (!fname.EndsWith(".lua"))
                 {
                        fname = fname.Replace('.', '/');
                        if (ScriptFileExists(fname + ".lua"))
                            return saveLastPathAndReturn(fname + ".lua");
                        if (ScriptFileExists(fname + "/init.lua"))
                            return saveLastPathAndReturn(fname + "/init.lua");
                 } else {
                        if (ScriptFileExists(fname))
                            return saveLastPathAndReturn(fname);
                 }
            }
            throw new FileNotFoundException(filename);
        }

        private bool ScriptFileExists(string name)
        {
            return File.Exists(name);
        }

        public string ResolveModuleName(string modname, Table globalContext)
        {
            //Debug.Log($"ResolveModuleName {modname}");
            return(modname);
        }
    }
}