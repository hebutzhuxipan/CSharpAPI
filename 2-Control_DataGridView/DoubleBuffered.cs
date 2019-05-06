//用于快速加载，只可见区域加载
//利用反射设置DataGridView的双缓冲
Type dgvType = dataGridView1.GetType();
PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
    BindingFlags.Instance | BindingFlags.NonPublic);
pi.SetValue(dataGridView1, true, null);