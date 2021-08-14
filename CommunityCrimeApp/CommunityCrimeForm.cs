using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CommunityCrimeApp
{
    public partial class CommunityCrimeForm : Form
    {
        public CommunityCrimeForm()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            dateTimePicker1.MinDate = _minDate;
            dateTimePicker1.MaxDate = _maxDate;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker2.MinDate = _minDate;
            dateTimePicker2.MaxDate = _maxDate;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM";
            dateTimePicker2.ShowUpDown = true;
        }

        private readonly DateTime _minDate = new DateTime(2017, 1, 1);
        private readonly DateTime _maxDate = new DateTime(2021, 6, 1);
        private readonly string _conString = "Data Source=TANSEM-PC;Initial Catalog=CommunityCrimeStat;Integrated Security=True";
        private List<CrimeRecord> _result = new List<CrimeRecord>();

        private void button1_Click(object sender, EventArgs e)
        {
            var con = new SqlConnection(_conString);
            con.Open();
            if (con.State != ConnectionState.Open)
            {
                MessageBox.Show("Cannot connect to database");
                return;
            }

            splitContainer1.Visible = true;
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.SelectedValueChanged += (o, i) => { UpdateSearch(con); };
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox2.SelectedValueChanged += (o, i) => { UpdateSearch(con); };
            dateTimePicker1.Value = _minDate;
            dateTimePicker1.ValueChanged += (o, i) => { UpdateSearch(con); };
            dateTimePicker2.Value = _maxDate;
            dateTimePicker2.ValueChanged += (o, i) => { UpdateSearch(con); };
            checkBox1.CheckedChanged += (o, i) => { UpdateSearch(con); };
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.DragButton = MouseButtons.Left;
            var latitude = 51.0447;
            var longitude = -114.0719;
            gMapControl1.Position = new GMap.NET.PointLatLng(latitude, longitude);
            gMapControl1.MinZoom = 9;
            gMapControl1.MaxZoom = 15;
            gMapControl1.Zoom = 10;

            UpdateSearch(con);
        }

        private void UpdateSearch(SqlConnection con)
        {
            _result.Clear();

            try
            {
                var sector = string.Empty;
                if (!comboBox1.Text.Equals("ALL"))
                {
                    sector = string.Concat("Sector = '", comboBox1.Text, "'AND ");
                }
                var category = string.Empty;
                if (!comboBox2.Text.Equals("ALL"))
                {
                    category = string.Concat("Category = '", comboBox2.Text, "'AND ");
                }

                var query = string.Concat("SELECT * FROM Community_Crime_Statistics WHERE ", sector, category,
                            "Date between '", dateTimePicker1.Value, "' and '", dateTimePicker2.Value, "';");
                var cmd = new SqlCommand(query, con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _result.Add(new CrimeRecord
                        {
                            Sector = reader.GetValue(0) == DBNull.Value ? string.Empty : reader.GetValue(0).ToString(),
                            CommunityName = reader.GetValue(1) == DBNull.Value ? string.Empty : reader.GetValue(1).ToString(),
                            Category = reader.GetValue(2) == DBNull.Value ? string.Empty : reader.GetValue(2).ToString(),
                            CrimeCount = reader.GetValue(3) == DBNull.Value ? 0 : Convert.ToInt32(reader.GetValue(3)),
                            ResidentCount = reader.GetValue(4) == DBNull.Value ? 0 : Convert.ToInt32(reader.GetValue(4)),
                            Date = reader.GetValue(5) == DBNull.Value ? DateTime.MinValue : (DateTime)reader.GetValue(5),
                            Year = reader.GetValue(6) == DBNull.Value ? string.Empty : reader.GetValue(6).ToString(),
                            Month = reader.GetValue(7) == DBNull.Value ? string.Empty : reader.GetValue(7).ToString(),
                            Long = reader.GetValue(8) == DBNull.Value ? 0 : Convert.ToDouble(reader.GetValue(8)),
                            Lat = reader.GetValue(9) == DBNull.Value ? 0 : Convert.ToDouble(reader.GetValue(9)),
                            ID = reader.GetValue(10) == DBNull.Value ? string.Empty : reader.GetValue(10).ToString(),
                            CommunityCenterPoint = reader.GetValue(11) == DBNull.Value ? string.Empty : reader.GetValue(11).ToString()
                        });
                    }
                }

                label6.Text = _result.Count().ToString();

                if (checkBox1.Checked)
                {
                    var markers = new GMapOverlay("markers");
                    gMapControl1.Overlays.Clear();
                    foreach (var record in _result)
                    {
                        var marker = new GMarkerGoogle(new GMap.NET.PointLatLng(record.Lat, record.Long), GMarkerGoogleType.blue_dot);
                        markers.Markers.Add(marker);
                    }
                    gMapControl1.Overlays.Add(markers);
                    RefreshMap();
                }
                else
                {
                    gMapControl1.Overlays.Clear();
                    RefreshMap();
                }
            }
            catch
            {
                MessageBox.Show("Database connection error");
                splitContainer1.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var f = new RecordTable(_result);
            f.ShowDialog();
        }

        private void RefreshMap()
        {
            gMapControl1.Zoom--;
            gMapControl1.Zoom++;
        }
    }
}
