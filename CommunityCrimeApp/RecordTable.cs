using System.Collections.Generic;
using System.Windows.Forms;

namespace CommunityCrimeApp
{
    public partial class RecordTable : Form
    {
        public RecordTable(List<CrimeRecord> result)
        {
            InitializeComponent();
            dataGridView1.DataSource = result;
        }
    }
}
