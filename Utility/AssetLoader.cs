using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LPRT;

public static class AssetLoader
{
    public static string OpenFileDialog()
    {
        Debug.WriteLine("Loading Json File.");
        using OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = @"JSON (*.json)|*.json*";
        openFileDialog.FilterIndex = 1;
        openFileDialog.RestoreDirectory = true;
        openFileDialog.Multiselect = false;

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            Debug.WriteLine("Returning file path: " + openFileDialog.FileName);
            return openFileDialog.FileName;
        }
        Debug.WriteLine("File path not selected, returning null.");
        return null;
    }
}