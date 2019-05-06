//双击变大变下
private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
{
    if (debugFrame.Parameter.u8RowNum == 0 || debugFrame.Parameter.u8RowNum > debugFrame.chip.GetChipPhysicalRowNum() ||
      debugFrame.Parameter.u8ColNum == 0 || debugFrame.Parameter.u8ColNum > debugFrame.chip.GetChipPhysicalColNum())
        return;

    if (zoomOutFlag == false)
    {
        oldItemWidth = dataGridView1.Columns[0].Width;
        oldItemHeight = dataGridView1.Rows[0].Height;

        int iBlank = ((dataGridView1.ClientRectangle.Width > 20) && (dataGridView1.ClientRectangle.Height > 20))  ? 20 : 0;

        int w = (dataGridView1.ClientRectangle.Width - iBlank) / (debugFrame.Parameter.u8ColNum + 1);
        int h = (dataGridView1.ClientRectangle.Height - iBlank) / (debugFrame.Parameter.u8RowNum + 1);

        if (w > oldItemWidth)
            w = oldItemWidth;

        if (h > oldItemHeight)
            h = oldItemHeight;

        for (int i = 0; i < dataGridView1.Columns.Count; i++)
        {
            dataGridView1.Columns[i].Width = w;
        }

        dataGridView1.RowHeadersWidth = w;

        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        {
            dataGridView1.Rows[i].Height = h;
        }

        zoomOutFlag = true;
    }
    else
    {
        for (int i = 0; i < dataGridView1.Columns.Count; i++)
        {
            dataGridView1.Columns[i].Width = oldItemWidth;
        }
        dataGridView1.RowHeadersWidth = oldItemWidth;

        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        {
            dataGridView1.Rows[i].Height = oldItemHeight;
        }

        zoomOutFlag = false;
    }
}

//调整窗口后，仍然调整单元格大小
private void dataGridView1_Resize(object sender, EventArgs e)
{
    if (zoomOutFlag == true)
    {
        int iBlank = ((dataGridView1.ClientRectangle.Width > 20) && (dataGridView1.ClientRectangle.Height > 20)) ? 20 : 0;

        int w = (dataGridView1.ClientRectangle.Width - iBlank) / (debugFrame.Parameter.u8ColNum + 1);
        int h = (dataGridView1.ClientRectangle.Height - iBlank) / (debugFrame.Parameter.u8RowNum + 1);

        if (w > oldItemWidth)
            w = oldItemWidth;

        if (h > oldItemHeight)
            h = oldItemHeight;

        for (int i = 0; i < dataGridView1.Columns.Count; i++)
        {
            dataGridView1.Columns[i].Width = w;
        }

        if (w != 0)
        {
            dataGridView1.RowHeadersWidth = w;
        }

        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        {
            dataGridView1.Rows[i].Height = h;
        }
    }
}