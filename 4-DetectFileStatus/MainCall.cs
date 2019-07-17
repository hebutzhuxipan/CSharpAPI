//调用文件是否被占用API
if (FileStatusHelper.IsFileOccupied(fileSave))
{
	MessageBox.Show(string.Format("{0}文件已被占用,请关闭", openFileDialog.FileName));
	return;
}