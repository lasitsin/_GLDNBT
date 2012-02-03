using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Management;
using System.Globalization;


namespace exam_FManager
{
   public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            FillDrives();
        }


        private void FillDrives()
        {
            const int removable = 2;
            const int localDisk = 3;
            const int network = 4;
            const int CD = 5;


            Cursor = Cursors.WaitCursor;

            treeView1.Nodes.Clear();
            var nodeTreeNode = new TreeNode("Computer", 0, 0);
            treeView1.Nodes.Add(nodeTreeNode);

            //set node collection
            TreeNodeCollection nodeCollection = nodeTreeNode.Nodes;

            //Get Drive list
            ManagementObjectCollection queryCollection = getDrives();
            foreach (ManagementObject mo in queryCollection)
            {
                int selectIndex;
                int imageIndex;
                switch (int.Parse(mo["DriveType"].ToString()))
                {
                    case removable:
                        imageIndex = 5;
                        selectIndex = 5;
                        break;
                    case localDisk:			
                        imageIndex = 6;
                        selectIndex = 6;
                        break;
                    case CD:
                        imageIndex = 7;
                        selectIndex = 7;
                        break;
                    case network:
                        imageIndex = 8;
                        selectIndex = 8;
                        break;
                    default:
                        imageIndex = 2;
                        selectIndex = 3;
                        break;
                }
                //create new drive node
                nodeTreeNode = new TreeNode(string.Format("{0}\\", mo["Name"]), imageIndex, selectIndex);
                //add new node
                nodeCollection.Add(nodeTreeNode);
            }

            InitListView();
            Cursor = Cursors.Default;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            TreeNode nodeCurrent = e.Node;
            nodeCurrent.Nodes.Clear();

            if (nodeCurrent.SelectedImageIndex == 0)
            {
                FillDrives();
            }
            else
            {
               FillDirs(nodeCurrent, nodeCurrent.Nodes);
            }
            nodeCurrent.Expand();
            Cursor = Cursors.Default;
        }

        private void FillDirs(TreeNode nodeCurrent, TreeNodeCollection nodeCurrentCollection)
        {
            const int imageIndex = 2;
            const int selectIndex = 3;

            if (nodeCurrent.SelectedImageIndex != 0)
            {
                try
                {
                    //check path
                    if (Directory.Exists(getFullPath(nodeCurrent.FullPath)) == false)
                        MessageBox.Show("Directory or path " + nodeCurrent + " does not exist.");
                    else
                    {
                        FillFiles(nodeCurrent.FullPath);

                        string[] stringDirectories = Directory.GetDirectories(getFullPath(nodeCurrent.FullPath));

                        foreach (string stringDir in stringDirectories)
                        {
                            string stringFullPath = stringDir;
                            string stringPathName = GetPathName(stringFullPath);

                            //create node for directories
                            var nodeDir = new TreeNode(stringPathName, imageIndex, selectIndex);
                            nodeCurrentCollection.Add(nodeDir);
                        }
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Error: Drive not ready or directory does not exist.");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Error: Drive or directory access denided.");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e);
                }
            }
        }

        private void FillFiles(string path)
        {
            var lvData = new string[6];
            InitListView();

                if (Directory.Exists(getFullPath(path)) == false)
                {
                    MessageBox.Show("Directory or path " + path + " does not exist.");
                }
                else
                {
                    try
                    {
                        string[] stringDirectories = Directory.GetDirectories(getFullPath(path));
                        DateTime dirCreateDate, dirModifyDate;

                        foreach (string stringDir in stringDirectories)
                        {
                            string stringDirName = GetPathName(stringDir);
                            var objDir = new DirectoryInfo(stringDirName);
                            dirCreateDate = objDir.CreationTime; 
                            dirModifyDate = objDir.LastWriteTime;
                            lvData[0] = stringDirName;
                            lvData[2] = objDir.Extension;

                            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(dirCreateDate) == false)
                            {
                                //not in day light saving time adjust time
                                lvData[3] = formatDate(dirCreateDate.AddHours(1));
                            }
                            else
                            {
                                //is in day light saving time adjust time
                                lvData[3] = formatDate(dirCreateDate);
                            }

                            //check if file is in local current day light saving time
                            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(dirModifyDate) == false)
                            {
                                //not in day light saving time adjust time
                                lvData[4] = formatDate(dirModifyDate.AddHours(1));
                            }
                            else
                            {
                                //not in day light saving time adjust time
                                lvData[4] = formatDate(dirModifyDate);
                            }
                            lvData[5] = stringDir;

                            //Create actual list item
                            var lvItem = new ListViewItem(lvData, 2);
                            listView1.Items.Add(lvItem); 
                        }



                        string[] stringFiles = Directory.GetFiles(getFullPath(path));
                        DateTime dtCreateDate, dtModifyDate;

                        foreach (string stringFile in stringFiles)
                        {
                            string stringFileName = stringFile;
                            var objFileSize = new FileInfo(stringFileName);
                            Int64 lFileSize = objFileSize.Length;
                            dtCreateDate = objFileSize.CreationTime; //GetCreationTime(stringFileName);
                            dtModifyDate = objFileSize.LastWriteTime; //GetLastWriteTime(stringFileName);

                            //create listview data
                            lvData[0] = GetPathName(stringFileName);
                            lvData[1] = formatSize(lFileSize);
                            lvData[2] = objFileSize.Extension;

                            //check if file is in local current day light saving time
                            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(dtCreateDate) == false)
                            {
                                //not in day light saving time adjust time
                                lvData[3] = formatDate(dtCreateDate.AddHours(1));
                            }
                            else
                            {
                                //is in day light saving time adjust time
                                lvData[3] = formatDate(dtCreateDate);
                            }

                            //check if file is in local current day light saving time
                            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(dtModifyDate) == false)
                            {
                                //not in day light saving time adjust time
                                lvData[4] = formatDate(dtModifyDate.AddHours(1));
                            }
                            else
                            {
                                //not in day light saving time adjust time
                                lvData[4] = formatDate(dtModifyDate);
                            }
                            lvData[5] = stringFileName;

                            //Create actual list item
                            Icon ic = Icon.ExtractAssociatedIcon(objFileSize.FullName);
                            imageList2.Images.Add(ic);
                            imageList3.Images.Add(ic);
                            var lvItem = new ListViewItem(lvData,4);
                            listView1.Items.Add(lvItem);
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error: Drive not ready or directory does not exist.");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Error: Drive or directory access denided.");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: " + e);
                    }
                }
                //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        protected void InitListView()
        {
            listView1.Clear();
            listView1.Columns.Add("Имя", 150, HorizontalAlignment.Left);        //0
            listView1.Columns.Add("Размер", 75, HorizontalAlignment.Right);     //1
            listView1.Columns.Add("Тип", 150, HorizontalAlignment.Center);      //2
            listView1.Columns.Add("Создан", 140, HorizontalAlignment.Left);     //3 2
            listView1.Columns.Add("Изменен", 140, HorizontalAlignment.Left);    //4 3
            listView1.Columns.Add("Путь", 200, HorizontalAlignment.Left);       //5 4
        }




        
        protected static ManagementObjectCollection getDrives()
        {
            //get drive collection
            ManagementObjectCollection queryCollection;
            using (var query = new ManagementObjectSearcher("SELECT * From Win32_LogicalDisk "))
            {
                queryCollection = query.Get();
            }
            return queryCollection;
        }

        protected static string getFullPath(string stringPath)
        {
            string stringParse = stringPath.Replace("Computer\\", "");
            return stringParse;
        }

        protected static string GetPathName(string stringPath)
        {
            string[] stringSplit = stringPath.Split('\\');
            int maxIndex = stringSplit.Length;
            return stringSplit[maxIndex - 1];
        }

        protected string formatDate(DateTime dtDate)
        {
            string stringDate;
            stringDate = dtDate.ToShortDateString() + " " + dtDate.ToShortTimeString();
            return stringDate;
        }

        protected static string formatSize(Int64 lSize)
        {
            string stringSize;
            var myNfi = new NumberFormatInfo();
            Int64 lKBSize;
            if (lSize < 1024)
            {
                stringSize = lSize == 0 ? "0" : "1";
            }
            else
            {
                lKBSize = lSize / 1024;
                stringSize = lKBSize.ToString("n", myNfi);
                stringSize = stringSize.Replace(".00", "");
            }
            return string.Format("{0} KB", stringSize);
        }



        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode nodeCurrent = e.Node;
            if (nodeCurrent.SelectedImageIndex == 3)
            {
                nodeCurrent.SelectedImageIndex = 2;
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode nodeCurrent = e.Node;
            if (nodeCurrent.SelectedImageIndex == 2)
            {
                nodeCurrent.SelectedImageIndex = 3;
            }
        }




        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }




        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Orientation==Orientation.Vertical)
            {
                splitContainer1.Orientation = Orientation.Horizontal;
            }
            else
            {
                splitContainer1.Orientation = Orientation.Vertical;
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (statusStrip1.Visible)
            {
                statusStrip1.Visible = false;
            }
            else
            {
                statusStrip1.Visible = true;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(Directory.GetParent(this.listView1.TopItem.SubItems[5].Text).FullName))
                {
                    FillFiles(Directory.GetParent(Directory.GetParent(this.listView1.TopItem.SubItems[5].Text).FullName).FullName);
                }
            }
            catch (Exception er)
            {
                //MessageBox.Show("Error: " + er);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = (ListView)sender;
            var objFile = new FileInfo(lv.SelectedItems[0].SubItems[5].Text);
            var objDir = new DirectoryInfo(lv.SelectedItems[0].SubItems[5].Text);
            if (objFile.Exists)
            {
                try
                {
                    System.Diagnostics.Process.Start(lv.SelectedItems[0].SubItems[5].Text);

                }
                catch (Exception er)
                {
                    //MessageBox.Show("Error: " + er);
                }
            }
            else
            {
                if (objDir.Exists)
                {
                    FillFiles(objDir.FullName);
                }
            }
        }




        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
            else
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));
            // Retrieve the node at the drop location.
            TreeNode targetNode = treeView1.GetNodeAt(targetPoint);
            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                // If it is a move operation, remove the node from its current 
                // location and add it to the node at the drop location.
                if (e.Effect == DragDropEffects.Move)
                {
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);
                }

                // If it is a copy operation, clone the dragged node 
                // and add it to the node at the drop location.
                else if (e.Effect == DragDropEffects.Copy)
                {
                    targetNode.Nodes.Add((TreeNode)draggedNode.Clone());

                    string fileName;// = "test.txt";
                    string destFile;
                    string sourcePath = getFullPath(draggedNode.FullPath);
                    string targetPath = getFullPath(targetNode.FullPath) + sourcePath.Substring(sourcePath.LastIndexOf("\\"));
                    if (!System.IO.Directory.Exists(targetPath))
                    {
                        System.IO.Directory.CreateDirectory(targetPath);
                    }
                    if (System.IO.Directory.Exists(sourcePath))
                    {
                        string[] files = System.IO.Directory.GetFiles(sourcePath);

                        // Copy the files and overwrite destination files if they already exist.
                        foreach (string s in files)
                        {
                            // Use static Path methods to extract only the file name from the path.
                            fileName = System.IO.Path.GetFileName(s);
                            destFile = System.IO.Path.Combine(targetPath, fileName);
                            System.IO.File.Copy(s, destFile, true);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Source path does not exist!");
                    }

                }

                // Expand the node at the location 
                // to show the dropped node.
                targetNode.Expand();
            }
        }

        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));
            treeView1.SelectedNode = treeView1.GetNodeAt(targetPoint);
        }

        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;
            return ContainsNode(node1, node2.Parent);
        }




        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //listView1.DoDragDrop(listView1.SelectedItems[0], DragDropEffects.Copy);
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
            else
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = listView1.PointToClient(new Point(e.X, e.Y));
            //var objDir = new DirectoryInfo(listView1.GetItemAt(targetPoint.X, targetPoint.Y).SubItems[5].Text);
            //if (objDir.Exists)
            //{
            //    FillFiles(objDir.FullName);
            //}
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            //this.listBox1.Items.Add(e.Data.GetData(DataFormats.StringFormat.ToString()));
        }

        private void Collapse_Click(object sender, EventArgs e)
        {
            this.treeView1.CollapseAll();
            treeView1.Nodes[0].Expand();
        }

        private void Expand_Click(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private List<TreeNode> FindAllNodes(TreeNodeCollection tr)
        {
            List<TreeNode> tmptrnds = new List<TreeNode>();
            for (int i = 0; i < tr.Count; i++)
            {
                tmptrnds.Add(tr[i]);
                if (tr[i].Nodes.Count > 0)
                {
                    List<TreeNode> TempRec = FindAllNodes(tr[i].Nodes);
                    tmptrnds.AddRange(TempRec);
                }
            }
            return tmptrnds;
        }




    }
}
