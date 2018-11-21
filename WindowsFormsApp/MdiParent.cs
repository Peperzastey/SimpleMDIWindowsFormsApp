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
    public partial class MdiParent : Form
    {
        const string MDI_CHILD_BASE_TITLE = "View ";
        const string ACTIVE_MDI_CHILD_VIEW_LABEL = "Active MDI child view: ";
        const string ELEMENT_COUNT_LABEL = "Element count in active window: ";

        const string SAVED_DATA_FILEPATH = "savedData.xml";

        Model model = new Model();
        private int nextViewIndex = 0;

        public MdiParent()
        {
            InitializeComponent();
            CreateAndShowView();
        }

        public void SetStatusLabels(long elementCount)
        {
            //TODO
            this.toolStripStatusLabel1.Text = ACTIVE_MDI_CHILD_VIEW_LABEL + ActiveMdiChild.Text;
            this.toolStripStatusLabel.Text = ELEMENT_COUNT_LABEL + elementCount;
        }


        private void AddTestData()
        {
            var testShapes = new[] 
            {
                new Shape(1)
                {
                    Color = "Black",
                    Type = Shape.ShapeType.TRIANGLE,
                    CenterX = -2.43,
                    CenterY = 5.02,
                    SurfaceArea = 11.1,
                    Label = "testShape Black Triangle"
                },
                new Shape(2)
                {
                    Color = "Pink",
                    Type = Shape.ShapeType.CIRCLE,
                    CenterX = 0,
                    CenterY = 0,
                    SurfaceArea = 963,
                    Label = "testShape Pink Circle"
                },
                new Shape(3)
                {
                    Color = "Blue",
                    Type = Shape.ShapeType.SQUARE,
                    CenterX = 443,
                    CenterY = -443,
                    SurfaceArea = 1,
                    Label = "testShape Blue Square"
                }
            };
            model.Shapes.AddRange(testShapes);

            //TODO as Model method. Model should have a list of views (observers)
            var testShapesList = new List<Shape>(testShapes);
            foreach (IMdiChild view in MdiChildren)
            {
                view.Update(testShapesList);
            }
        }

        private void CreateAndShowView()
        {
            var newMDIChild = new ListViewMdiChild();
            newMDIChild.MdiParent = this;
            newMDIChild.Text = MDI_CHILD_BASE_TITLE + nextViewIndex++;
            newMDIChild.Show();
            this.InitializeViewData(newMDIChild);
        }

        private void InitializeViewData(IMdiChild view)
        {
            view.Update(model.Shapes);
        }

        private void newViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateAndShowView();
        }

        private void NewRichTextMDIChild_Click(object sender, System.EventArgs e)
        {
            RichTextMdiChild newMDIChild = new RichTextMdiChild();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void NewEmptyMDIChild_Click(object sender, EventArgs e)
        {
            var newMDIChild = new EmptyMdiChild();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        //TODO one handler method; distinguish using sender parameter; switch-case for choosing the correct MdiLayout
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void addTestDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTestData();
        }

        private void saveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.SaveDataToFile(SAVED_DATA_FILEPATH);
        }

        private void loadSavedDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!model.LoadDataFromFile(SAVED_DATA_FILEPATH))
                MessageBox.Show("No saved data.");
            foreach (IMdiChild view in MdiChildren)
            {
                view.Clear();
                view.Update(model.Shapes);
            }
        }

        private void clearDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Shapes.Clear();
            foreach (IMdiChild view in MdiChildren)
            {
                view.Clear();
            }
        }
    }
}
