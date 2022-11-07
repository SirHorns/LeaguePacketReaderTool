using System.Windows.Forms;

namespace LPRT;

public static class AssetLoader
{

    public static string OpenFileDialog()
    {
        using OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = @"JSON (*.json)|*.json*";
        openFileDialog.FilterIndex = 1;
        openFileDialog.RestoreDirectory = true;
        openFileDialog.Multiselect = false;

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            return openFileDialog.FileName;
            
        }
        return null;
    }
}