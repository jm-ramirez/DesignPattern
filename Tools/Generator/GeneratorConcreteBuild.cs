using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Generator
{
    public class GeneratorConcreteBuild : IBuilderGenerator
    {
        private Generator _generator;

        public GeneratorConcreteBuild()
        {
            Reset();
        }

        public void Reset() => _generator = new Generator();

        public void SetContent(List<string> content) => _generator.Content = content;
        public void SetPath(string path) => _generator.Path = path;
        public void SetFormat(TypeFormat format) => _generator.Format = format;
        public void SetCharacter(TypeCharacter character) => _generator.Character = character;

        public Generator GetGenerator() => _generator;
    }
}
