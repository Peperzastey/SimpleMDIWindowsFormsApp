using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class ListViewMdiChild : Form, IMdiChild
    {
        public ListViewMdiChild()
        {
            InitializeComponent();
            this.Activated += new EventHandler(ListViewMdiChild_Activated);
        }

        void IMdiChild.Update(List<Shape> newShapes)
        {
            foreach (var shape in newShapes)
            {
                var newItem = new ListViewItem(new[] {
                    shape.Id.ToString(),
                    shape.Color.ToString(),
                    shape.Type.ToString(),
                    shape.CenterX.ToString(),
                    shape.CenterY.ToString(),
                    shape.SurfaceArea.ToString(),
                    shape.Label.ToString()
                });
                newItem.Tag = shape;
                listView1.Items.Add(newItem);
            }
        }
        void IMdiChild.Remove(List<Shape> removedShapes)
        {
            throw new NotImplementedException();
        }
        void IMdiChild.Clear()
        {
            listView1.Items.Clear();
        }

        private void ListViewMdiChild_Activated(object sender, EventArgs e)
        {
            var mdiParent = this.MdiParent as MdiParent;
            mdiParent.SetStatusLabels(elementCount: listView1.Items.Count);
        }

        private void ListViewMdiChild_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = (this.MdiParent.MdiChildren.Length == 1);
            }
        }
    }
}
