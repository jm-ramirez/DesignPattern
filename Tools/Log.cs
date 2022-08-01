using System;
using System.IO;

namespace Tools
{
    public sealed class Log //sealed siginifica que esta clase no se puede heredar
    {
        private static Log _instance = null;
        private string _path;
        private static object _protect = new object();

        public static Log GetInstance(string path)
        {
            //Mientras esta un hilo trabajando con _protect, lo bloquea, para evitar que se instancie dos veces _intance.
            lock (_protect)
            {
                if (_instance == null)
                {
                    _instance = new Log(path);
                }
            } 

            return _instance;
        }

        private Log(string path) //Constructor
        {
            _path = path;
        }

        public void Save(string message)
        {
            File.AppendAllText(_path, message + Environment.NewLine);
        }
    }
}
