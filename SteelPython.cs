
using System;
using Microsoft;
using IronPython.Hosting;

namespace SteelPython
{
    public class CustomPythonEngine
    {
        public static void RunPython()
        {
            var pythonEngine = Python.CreateEngine();
            pythonEngine.CreateScriptSourceFromString("print 'hello world'").Execute();
        }

    } 
}