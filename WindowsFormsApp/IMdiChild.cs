using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    interface IMdiChild
    {
        void Update(List<Shape> newShapes);
        void Remove(List<Shape> removedShapes);
        void Clear();
    }
}
