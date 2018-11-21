using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsApp
{
    [Serializable]
    public class Model
    {
        private List<Shape> _shapes;
        public List<Shape> Shapes { get { return _shapes; } }

        public Model()
        {
            _shapes = new List<Shape>();
        }

        public void SaveDataToFile(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Model));
            //TextWriter writer = new StreamWriter(filename);
            var stream = new FileStream(filename, FileMode.Truncate, FileAccess.Write);
            serializer.Serialize(stream, this);
            stream.Close();
        }

        public bool LoadDataFromFile(string filename)
        {
            FileStream stream;
            try
            {
                 stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (FileNotFoundException e)
            {
                return false;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Model));
            Model loadedModel = (Model) serializer.Deserialize(stream);
            stream.Close();
            this._shapes = loadedModel.Shapes;
            return true;
        }
    }
}
