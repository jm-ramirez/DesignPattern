using DesignPatterns.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Generator;

namespace DesignPatternAsp.Controllers
{
    public class GeneratorFileController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private GeneratorConcreteBuild _generatorConcreteBuild;
        public GeneratorFileController(IUnitOfWork unitOfWork, GeneratorConcreteBuild generatorConcreteBuild)
        {
            _unitOfWork = unitOfWork;
            _generatorConcreteBuild = generatorConcreteBuild;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateFile(int optionFile)
        {
            try
            {
                var beers = _unitOfWork.Beers.Get();
                List<string> content = beers.Select(d => d.Name).ToList();
                string path = "file" + DateTime.Now.Ticks + new Random().Next(1000) + ".txt";
                var generatorDirector = new GeneratorDirector(_generatorConcreteBuild);

                if (optionFile == 1)
                    generatorDirector.CreateSimpleJsonGenerator(content, path);
                else
                    generatorDirector.CreateSimplePipeGenerator(content, path);

                var generator = _generatorConcreteBuild.GetGenerator();
                generator.Save();

                return Json("Archivo generado");
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
    }
}
