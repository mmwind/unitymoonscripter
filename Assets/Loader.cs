using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

namespace Assets
{
    public class Loader : IScriptLoader
    {
        public object LoadFile(string file, Table globalContext)
        {
            throw new System.NotImplementedException();
        }

        public string ResolveFileName(string filename, Table globalContext)
        {
            throw new System.NotImplementedException();
        }

        public string ResolveModuleName(string modname, Table globalContext)
        {
            throw new System.NotImplementedException();
        }
    }
}