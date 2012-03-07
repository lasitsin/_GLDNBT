using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;


namespace exam_FManager
{
    class Explorer
    {
        //public static void FillDrives()
        //{
        //    const int removable = 2;
        //    const int localDisk = 3;
        //    const int network = 4;
        //    const int CD = 5;

        //    Cursor = Cursors.WaitCursor;

        //    treeView1.Nodes.Clear();
        //    var nodeTreeNode = new TreeNode("Computer", 0, 0);
        //    treeView1.Nodes.Add(nodeTreeNode);

        //    //set node collection
        //    TreeNodeCollection nodeCollection = nodeTreeNode.Nodes;

        //    //Get Drive list
        //    ManagementObjectCollection queryCollection = getDrives();
        //    foreach (ManagementObject mo in queryCollection)
        //    {
        //        int selectIndex;
        //        int imageIndex;
        //        switch (int.Parse(mo["DriveType"].ToString()))
        //        {
        //            case removable:
        //                imageIndex = 5;
        //                selectIndex = 5;
        //                break;
        //            case localDisk:
        //                imageIndex = 6;
        //                selectIndex = 6;
        //                break;
        //            case CD:
        //                imageIndex = 7;
        //                selectIndex = 7;
        //                break;
        //            case network:
        //                imageIndex = 8;
        //                selectIndex = 8;
        //                break;
        //            default:
        //                imageIndex = 2;
        //                selectIndex = 3;
        //                break;
        //        }
        //        //create new drive node
        //        nodeTreeNode = new TreeNode(string.Format("{0}\\", mo["Name"]), imageIndex, selectIndex);
        //        //add new node
        //        nodeCollection.Add(nodeTreeNode);
        //    }

        //    InitListView();
        //    Cursor = Cursors.Default;
        //}


    }
}
