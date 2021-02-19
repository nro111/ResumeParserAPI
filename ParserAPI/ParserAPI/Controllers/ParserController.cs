using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ParserAPI.Core.Infrastructure;

namespace ParserAPI.Controllers
{
    public class ParserController : Controller
    {
        IExtractorFacade _extractorFacade;
        ITypeConverterTool _typeConverterTool;

        public ParserController(IExtractorFacade extractorFacade, ITypeConverterTool typeConverterTool)
        {
            _extractorFacade = extractorFacade;
            _typeConverterTool = typeConverterTool;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET api/values
        [HttpGet]
        public void Parse()
        {
            _extractorFacade.GenerateCandidateObject(@"[some resume path]");
        }

        [HttpGet]
        public Dictionary<int, string> GetSections()
        {            
            var resumeTextList = _typeConverterTool.ConvertWordDocumentToList(@"[some resume path]");
            return _extractorFacade.GenerateResumeSections(resumeTextList);
        }

        [HttpGet]
        public string RunPythonParser()
        {
            var resumeTextList = _typeConverterTool.ConvertWordDocumentToList(@"[some resume path]");
            Microsoft.Scripting.Hosting.ScriptEngine pythonEngine = IronPython.Hosting.Python.CreateEngine();
            Microsoft.Scripting.Hosting.ScriptSource pythonScript = pythonEngine.CreateScriptSourceFromFile("../SecondPassNLP/SecondPassNLP.py ");
            ICollection<string> searchPaths = pythonEngine.GetSearchPaths();
            searchPaths.Add(@"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\DLLs");
            searchPaths.Add(@"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\libs");
            searchPaths.Add(@"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\Lib");
            searchPaths.Add(@"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\Lib\site-packages");
            pythonEngine.SetSearchPaths(searchPaths);
            Microsoft.Scripting.Hosting.ScriptScope scope = pythonEngine.CreateScope();
            scope.SetVariable("resume", string.Join(',', resumeTextList));
            pythonScript.Execute();
            var test = scope.GetVariable("parsedResume");
            return test;
        }
    }
}
