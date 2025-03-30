using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Tiny_College
{
    public partial class College_System : Form
    {
        MySQLConnector mySQLConnector = new MySQLConnector();

        public College_System()
        {
            InitializeComponent();
        }

        private void College_System_Load(object sender, EventArgs e)
        {
            LoadSchool();
            LoadProfessor();
            LoadDepartment();
            LoadCourse();
            LoadBuilding();
            LoadClass();
            LoadSemester();
            LoadEnroll();
            LoadStudent();
            LoadRoom();
        }
        private void LoadRoom()
        {
            dataGridViewR.DataSource = mySQLConnector.FetchRoom();
        }

        private void LoadStudent()
        {
            dataGridViewSc.DataSource = mySQLConnector.FetchStudent();
        }
        private void LoadEnroll()
        {
            dataGridViewEL.DataSource = mySQLConnector.FetchEnroll();
        }
        private void LoadSemester()
        {
            dataGridViewSEM.DataSource = mySQLConnector.FetchSemester();
        }
        private void LoadClass()
        {
            dataGridViewCL.DataSource = mySQLConnector.FetchClass();
        }
        private void LoadBuilding()
        {
            dataGridViewbd.DataSource = mySQLConnector.FetchBuilding();
        }
        private void LoadCourse()
        {
            dataGridViewcrs.DataSource = mySQLConnector.FetchCourse();
        }

        private void LoadDepartment()
        {
            dataGridViewD.DataSource = mySQLConnector.FetchDepartment();
        }


        private void LoadSchool()
        {
            dataGridView2.DataSource = mySQLConnector.FetchSchool();
        }

        private void LoadProfessor()
        {
            dataGridView1.DataSource = mySQLConnector.FetchProfessor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxSchool.Text, out int schoolCode))
            {
                MessageBox.Show("Please enter a valid School Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox6.Text, out int profNum))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.InsertSchool(schoolCode, textBox2.Text, profNum);
            LoadSchool();
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView2.SelectedRows[0].Index;

                // Convert BookID to an integer
                if (int.TryParse(dataGridView2.Rows[rowIndex].Cells["SCHOOL_CODE"].Value.ToString(), out int SCHOOL_CODE))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteSchool(SCHOOL_CODE); // Now passing an int

                        // Refresh DataGridView
                        dataGridView2.DataSource = mySQLConnector.FetchSchool();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid BookID format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxSchool.Text, out int schoolCode))
            {
                MessageBox.Show("Invalid School Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox6.Text, out int profNum))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.UpdateSchool(schoolCode, textBox2.Text, profNum);
            LoadSchool();
            MessageBox.Show("School updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxSchool.Clear();
            textBox2.Clear();
            textBox6.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string keyword = textBox5.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridView2.DataSource = mySQLConnector.SearchSchool(keyword);
        }

        private void SAVE_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxProfessor.Text, out int PROF_NUM))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxrank.Text, out int PROF_RANK))
            {
                MessageBox.Show("Please enter a valid Professor Rank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxde.Text, out int DEPT_CODE))
            {
                MessageBox.Show("Please enter a valid Professor Rank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.InsertProfessor(PROF_NUM, textBoxProfessor.Text, PROF_RANK, textBoxL.Text, textBoxF.Text, textBoxI.Text, textBoxEm.Text, DEPT_CODE);
            LoadProfessor();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxProfessor.Text, out int PROF_NUM))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxrank.Text, out int PROF_RANK))
            {
                MessageBox.Show("Invalid Professor Rank!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxde.Text, out int DEPT_CODE))
            {
                MessageBox.Show("Invalid Professor Rank!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.UpdateProfessor(PROF_NUM, textBoxS.Text, PROF_RANK, textBoxL.Text, textBoxF.Text, textBoxI.Text, textBoxEm.Text, DEPT_CODE);
            LoadProfessor(); // FIXED: Previously called LoadSchool()
            MessageBox.Show("Professor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void button9_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                // Convert PROF_NUM to an integer
                if (int.TryParse(dataGridView1.Rows[rowIndex].Cells["PROF_NUM"].Value.ToString(), out int PROF_NUM))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteProfessor(PROF_NUM); // Now passing an int

                        // Refresh DataGridView
                        dataGridView1.DataSource = mySQLConnector.FetchProfessor();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Professor Number format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;

                // Assign values using column indexes
                textBoxProfessor.Text = row.Cells[0].Value?.ToString() ?? "";
                textBoxS.Text = row.Cells[1].Value?.ToString() ?? "";
                textBoxrank.Text = row.Cells[2].Value?.ToString() ?? "";
                textBoxL.Text = row.Cells[3].Value?.ToString() ?? "";
                textBoxF.Text = row.Cells[4].Value?.ToString() ?? "";
                textBoxI.Text = row.Cells[5].Value?.ToString() ?? "";
                textBoxEm.Text = row.Cells[6].Value?.ToString() ?? "";
                textBoxde.Text = row.Cells[7].Value?.ToString() ?? "";

            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            textBoxProfessor.Clear();
            textBoxS.Clear();
            textBoxrank.Clear();
            textBoxL.Clear();
            textBoxF.Clear();
            textBoxI.Clear();
            textBoxEm.Clear();
            textBoxde.Clear();

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                DataGridViewRow row = dataGridView2.CurrentRow;

                // Assign values using column indexes
                textBoxSchool.Text = row.Cells[0].Value?.ToString() ?? "";
                textBox2.Text = row.Cells[1].Value?.ToString() ?? "";
                textBox6.Text = row.Cells[2].Value?.ToString() ?? "";

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxPN.Text, out int PROF_NUM))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxSC.Text, out int School_Code))
            {
                MessageBox.Show("Please enter a valid Professor Rank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxDepartment.Text, out int Dept_Code))
            {
                MessageBox.Show("Please enter a valid Professor Rank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            mySQLConnector.InsertDepartment(Dept_Code, textBoxN.Text, School_Code, PROF_NUM);
            LoadDepartment();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxDepartment.Text, out int Dept_Code))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxPN.Text, out int PROF_NUM))
            {
                MessageBox.Show("Invalid Professor Rank!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxSC.Text, out int School_Code))
            {
                MessageBox.Show("Invalid Professor Rank!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            mySQLConnector.UpdateDepartment(Dept_Code, textBoxN.Text, School_Code, PROF_NUM);
            LoadDepartment(); // FIXED: Previously called LoadSchool()
            MessageBox.Show("Professor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button13_Click(object sender, EventArgs e)
        {

            if (dataGridViewD.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewD.SelectedRows[0].Index;

                // Convert PROF_NUM to an integer
                if (int.TryParse(dataGridViewD.Rows[rowIndex].Cells["Dept_Code"].Value.ToString(), out int Dept_Code))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteDepartment(Dept_Code); // Now passing an int

                        // Refresh DataGridView
                        dataGridViewD.DataSource = mySQLConnector.FetchDepartment();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Professor Number format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBoxDepartment.Clear();
            textBoxN.Clear();
            textBoxSC.Clear();
            textBoxPN.Clear();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            string keyword = textBoxDS.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewD.DataSource = mySQLConnector.SearchDepartment(keyword);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string keyword = textBoxS.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridView1.DataSource = mySQLConnector.SearchProfessor(keyword);
        }

        private void dataGridViewD_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridViewD.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewD.CurrentRow;

                // Assign values using column indexes
                textBoxDepartment.Text = row.Cells[0].Value?.ToString() ?? "";
                textBoxN.Text = row.Cells[1].Value?.ToString() ?? "";
                textBoxSC.Text = row.Cells[2].Value?.ToString() ?? "";
                textBoxPN.Text = row.Cells[3].Value?.ToString() ?? "";

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxcrs.Text, out int CRS_CODE))
            {
                MessageBox.Show("Please enter a valid School Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxcr.Text, out int CRS_CREDIT))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxdc.Text, out int DEPT_CODE))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            mySQLConnector.InsertCourse(CRS_CODE, textBoxT.Text, textBoxdes.Text, CRS_CREDIT, DEPT_CODE);
            LoadCourse();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxcrs.Text, out int CRS_CODE))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxcr.Text, out int CRS_CREDIT))
            {
                MessageBox.Show("Invalid Professor Rank!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxdc.Text, out int DEPT_CODE))
            {
                MessageBox.Show("Invalid Professor Rank!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.UpdateCourse(CRS_CODE, textBoxT.Text, textBoxdes.Text, CRS_CREDIT, DEPT_CODE);
            LoadCourse(); // FIXED: Previously called LoadSchool()
            MessageBox.Show("Professor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (dataGridViewcrs.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewcrs.SelectedRows[0].Index;

                // Convert PROF_NUM to an integer
                if (int.TryParse(dataGridViewcrs.Rows[rowIndex].Cells["CRS_CODE"].Value.ToString(), out int CRS_CODE))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteCourse(CRS_CODE); // Now passing an int

                        // Refresh DataGridView
                        dataGridViewcrs.DataSource = mySQLConnector.FetchCourse();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Professor Number format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBoxcrs.Clear();
            textBoxT.Clear();
            textBoxdes.Clear();
            textBoxcr.Clear();
            textBoxdc.Clear();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string keyword = textBoxscc.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewcrs.DataSource = mySQLConnector.SearchCourse(keyword);
        }

        private void dataGridViewcrs_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewcrs.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewcrs.CurrentRow;

                // Assign values using column indexes
                textBoxcrs.Text = row.Cells[0].Value?.ToString() ?? "";
                textBoxT.Text = row.Cells[1].Value?.ToString() ?? "";
                textBoxdes.Text = row.Cells[2].Value?.ToString() ?? "";
                textBoxcr.Text = row.Cells[3].Value?.ToString() ?? "";
                textBoxdc.Text = row.Cells[4].Value?.ToString() ?? "";

            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxbld.Text, out int BLDG_CODE))
            {
                MessageBox.Show("Please enter a valid School Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.InsertBuilding(BLDG_CODE, textBoxbn.Text, textBoxbl.Text);
            LoadBuilding();
        }

        private void button22_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(textBoxbld.Text, out int BLDG_CODE))
            {
                MessageBox.Show("Invalid School Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            mySQLConnector.UpdateBuilding(BLDG_CODE, textBoxbn.Text, textBoxbl.Text);
            LoadBuilding();
            MessageBox.Show("School updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button23_Click(object sender, EventArgs e)
        {

            if (dataGridViewbd.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewbd.SelectedRows[0].Index;

                // Convert BookID to an integer
                if (int.TryParse(dataGridViewbd.Rows[rowIndex].Cells["BLDG_CODE"].Value.ToString(), out int BLDG_CODE))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteBuilding(BLDG_CODE); // Now passing an int

                        // Refresh DataGridView
                        dataGridViewbd.DataSource = mySQLConnector.FetchBuilding();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid BookID format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBoxbld.Clear();
            textBoxbn.Clear();
            textBoxbl.Clear();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string keyword = textBoxbs.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewbd.DataSource = mySQLConnector.SearchBuilding(keyword);
        }

        private void dataGridViewbd_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewbd.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewbd.CurrentRow;

                // Assign values using column indexes
                textBoxbld.Text = row.Cells[0].Value?.ToString() ?? "";
                textBoxbn.Text = row.Cells[1].Value?.ToString() ?? "";
                textBoxbl.Text = row.Cells[2].Value?.ToString() ?? "";

            }
        }

        private void button29_Click(object sender, EventArgs e)


        {
            if (!int.TryParse(textBoxCL.Text, out int classCode))
            {
                MessageBox.Show("Invalid Class Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string classSelection = textBoxCSEL.Text.Trim();
            string classTime = textBoxCT.Text.Trim();

            if (!int.TryParse(textBoxcd.Text, out int courseCode))
            {
                MessageBox.Show("Invalid Course Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxpnm.Text, out int professorNumber))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxrc.Text, out int roomCode))
            {
                MessageBox.Show("Invalid Room Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxsem.Text, out int semesterCode))
            {
                MessageBox.Show("Invalid Semester Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.InsertClass(classCode, classSelection, classTime, courseCode, professorNumber, roomCode, semesterCode);
            LoadClass(); // Refresh DataGridView
        }



        private void button27_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxCL.Text, out int CLASS_CODE))
            {
                MessageBox.Show("Invalid School Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (!int.TryParse(textBoxpnm.Text, out int PROF_NUM))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxcd.Text, out int CRS_CODE))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxsem.Text, out int SEMESTER_CODE))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxrc.Text, out int ROOM_CODE))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.UpdateClass(CLASS_CODE, textBoxCSEL.Text, textBoxCT.Text, CRS_CODE, PROF_NUM, ROOM_CODE, SEMESTER_CODE);
            LoadClass();
            MessageBox.Show("School updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (dataGridViewCL.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewCL.SelectedRows[0].Index;

                // Convert BookID to an integer
                if (int.TryParse(dataGridViewCL.Rows[rowIndex].Cells["CLASS_CODE"].Value.ToString(), out int CLASS_CODE))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteClass(CLASS_CODE); // Now passing an int

                        // Refresh DataGridView
                        dataGridViewCL.DataSource = mySQLConnector.FetchClass();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid BookID format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button26_Click(object sender, EventArgs e)
        {
            textBoxCL.Clear();
            textBoxCT.Clear();
            textBoxCSEL.Clear();
            textBoxcd.Clear();
            textBoxrc.Clear();
            textBoxpnm.Clear();
            textBoxsem.Clear();

        }

        private void button25_Click(object sender, EventArgs e)
        {
            string keyword = textBoxCs.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewCL.DataSource = mySQLConnector.SearchClass(keyword);

        }

        private void dataGridViewCL_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCL.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewCL.CurrentRow;

                // Assign values using column indexes
                textBoxCL.Text = row.Cells[0].Value?.ToString() ?? ""; // AuthorID
                textBoxCT.Text = row.Cells[2].Value?.ToString() ?? ""; // Last Name
                textBoxCSEL.Text = row.Cells[1].Value?.ToString() ?? "";
                textBoxpnm.Text = row.Cells[4].Value?.ToString() ?? "";
                textBoxrc.Text = row.Cells[5].Value?.ToString() ?? "";
                textBoxsem.Text = row.Cells[6].Value?.ToString() ?? "";
                textBoxcd.Text = row.Cells[3].Value?.ToString() ?? "";

            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxSM.Text, out int SEMESTER_CODE))
            {
                MessageBox.Show("Please enter a valid School Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxSY.Text, out int SEMESTER_YEAR))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxSTE.Text, out int SEMESTER_TERM))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.InsertSemester(SEMESTER_CODE, SEMESTER_YEAR, SEMESTER_TERM, textBoxSD.Text, textBoxSEN.Text);
            LoadSemester();

        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxSM.Text, out int SEMESTER_CODE))
            {
                MessageBox.Show("Invalid School Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxSY.Text, out int SEMESTER_YEAR))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxSTE.Text, out int SEMESTER_TERM))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            mySQLConnector.UpdateSemester(SEMESTER_CODE, SEMESTER_YEAR, SEMESTER_TERM, textBoxSD.Text, textBoxSEN.Text);
            LoadSemester();
            MessageBox.Show("School updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button33_Click(object sender, EventArgs e)
        {

            if (dataGridViewSEM.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewSEM.SelectedRows[0].Index;

                // Convert BookID to an integer
                if (int.TryParse(dataGridViewSEM.Rows[rowIndex].Cells["SEMESTER_CODE"].Value.ToString(), out int SEMESTER_CODE))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteSemester(SEMESTER_CODE); // Now passing an int

                        // Refresh DataGridView
                        dataGridViewSEM.DataSource = mySQLConnector.FetchSemester();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid BookID format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button31_Click(object sender, EventArgs e)
        {
            textBoxSM.Clear();
            textBoxSY.Clear();
            textBoxSTE.Clear();
            textBoxSD.Clear();
            textBoxSEN.Clear();

        }

        private void button30_Click(object sender, EventArgs e)
        {
            string keyword = textBoxsems.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewSEM.DataSource = mySQLConnector.SearchSemester(keyword);

        }

        private void dataGridViewSEM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewSEM_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSEM.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewSEM.CurrentRow;

                // Assign values using column indexes
                textBoxSM.Text = row.Cells[0].Value?.ToString() ?? ""; // AuthorID
                textBoxSY.Text = row.Cells[1].Value?.ToString() ?? ""; // Last Name
                textBoxSTE.Text = row.Cells[2].Value?.ToString() ?? "";
                textBoxSD.Text = row.Cells[3].Value?.ToString() ?? "";
                textBoxSEN.Text = row.Cells[4].Value?.ToString() ?? "";

            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxCC.Text, out int CLASS_CODE))
            {
                MessageBox.Show("Please enter a valid School Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxSTU.Text, out int STU_NUM))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime ENROLL_DATE;
            if (!DateTime.TryParse(textBoxed.Text, out ENROLL_DATE))
            {
                MessageBox.Show("Please enter a valid End Date (YYYY-MM-DD).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!int.TryParse(textBoxeg.Text, out int ENROLL_GRADE))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxEC.Text, out int ENROLL_CODE))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            mySQLConnector.InsertEnroll(ENROLL_CODE, ENROLL_DATE, ENROLL_GRADE, CLASS_CODE, STU_NUM);
            LoadEnroll();

        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxCC.Text, out int CLASS_CODE))
            {
                MessageBox.Show("Invalid School Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxSTU.Text, out int STU_NUM))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime ENROLL_DATE;
            if (!DateTime.TryParse(textBoxed.Text, out ENROLL_DATE))
            {
                MessageBox.Show("Please enter a valid End Date (YYYY-MM-DD).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (!int.TryParse(textBoxeg.Text, out int ENROLL_GRADE))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxEC.Text, out int ENROLL_CODE))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.UpdateEnroll(ENROLL_CODE, ENROLL_DATE, ENROLL_GRADE, CLASS_CODE, STU_NUM);
            LoadEnroll();
            MessageBox.Show("School updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void button38_Click(object sender, EventArgs e)
        {

            if (dataGridViewEL.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewEL.SelectedRows[0].Index;

                // Convert BookID to an integer
                if (int.TryParse(dataGridViewEL.Rows[rowIndex].Cells["ENROLL_CODE"].Value.ToString(), out int ENROLL_CODE))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteEnroll(ENROLL_CODE); // Now passing an int

                        // Refresh DataGridView
                        dataGridViewEL.DataSource = mySQLConnector.FetchEnroll();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid BookID format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button36_Click(object sender, EventArgs e)
        {
            textBoxEC.Clear();
            textBoxCC.Clear();
            textBoxSTU.Clear();
            textBoxed.Clear();
            textBoxeg.Clear();

        }

        private void button35_Click(object sender, EventArgs e)
        {
            string keyword = textBoxEn.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewEL.DataSource = mySQLConnector.SearchEnroll(keyword);

        }

        private void dataGridViewEL_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEL.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewEL.CurrentRow;

                // Assign values using column indexes
                textBoxCC.Text = row.Cells[3].Value?.ToString() ?? ""; // AuthorID
                textBoxEC.Text = row.Cells[0].Value?.ToString() ?? ""; // Last Name
                textBoxSTU.Text = row.Cells[4].Value?.ToString() ?? "";
                textBoxed.Text = row.Cells[1].Value?.ToString() ?? "";
                textBoxeg.Text = row.Cells[2].Value?.ToString() ?? "";
            }
        }



        private void button44_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxST.Text, out int STU_NUM))
            {
                MessageBox.Show("Please enter a valid School Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxpr.Text, out int PROF_NUM))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxdcc.Text, out int DEPT_CODE))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            mySQLConnector.InsertStudent(STU_NUM, textBoxSL.Text, textBoxSF.Text, textBoxSI.Text, textBoxSE.Text, DEPT_CODE, PROF_NUM);
            LoadStudent();

        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxST.Text, out int STU_NUM))
            {
                MessageBox.Show("Invalid School Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxpr.Text, out int PROF_NUM))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxdcc.Text, out int DEPT_CODE))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            mySQLConnector.UpdateStudent(STU_NUM, textBoxSL.Text, textBoxSF.Text, textBoxSI.Text, textBoxSE.Text, DEPT_CODE, PROF_NUM);
            LoadStudent();
            MessageBox.Show("School updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (dataGridViewSc.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewSc.SelectedRows[0].Index;

                // Convert BookID to an integer
                if (int.TryParse(dataGridViewSc.Rows[rowIndex].Cells["STU_NUM"].Value.ToString(), out int STU_NUM))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteStudent(STU_NUM); // Now passing an int

                        // Refresh DataGridView
                        dataGridViewSc.DataSource = mySQLConnector.FetchStudent();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid BookID format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button41_Click(object sender, EventArgs e)
        {
            textBoxST.Clear();
            textBoxSL.Clear();
            textBoxSF.Clear();
            textBoxSI.Clear();
            textBoxSE.Clear();
            textBoxdcc.Clear();
            textBoxpr.Clear();

        }

        private void button40_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSTH.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewSc.DataSource = mySQLConnector.SearchStudent(keyword);

        }

        private void dataGridViewSc_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSc.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewSc.CurrentRow;

                // Assign values using column indexes
                textBoxST.Text = row.Cells[0].Value?.ToString() ?? ""; // AuthorID
                textBoxSL.Text = row.Cells[1].Value?.ToString() ?? ""; // Last Name
                textBoxSF.Text = row.Cells[2].Value?.ToString() ?? "";
                textBoxSI.Text = row.Cells[3].Value?.ToString() ?? "";
                textBoxSE.Text = row.Cells[4].Value?.ToString() ?? "";
                textBoxdcc.Text = row.Cells[5].Value?.ToString() ?? "";
                textBoxpr.Text = row.Cells[6].Value?.ToString() ?? "";


            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxRoom.Text, out int ROOM_CODE))
            {
                MessageBox.Show("Please enter a valid School Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxbco.Text, out int BLDG_CODE))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.InsertRoom(ROOM_CODE, textBoxty.Text, BLDG_CODE);
            LoadRoom();

        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxRoom.Text, out int ROOM_CODE))
            {
                MessageBox.Show("Invalid School Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxty.Text, out int BLDG_CODE))
            {
                MessageBox.Show("Invalid Professor Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mySQLConnector.UpdateRoom(ROOM_CODE, textBoxty.Text, BLDG_CODE);
            LoadSchool();
            MessageBox.Show("School updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button48_Click(object sender, EventArgs e)
        {

            if (dataGridViewR.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridViewR.SelectedRows[0].Index;

                // Convert BookID to an integer
                if (int.TryParse(dataGridViewR.Rows[rowIndex].Cells["ROOM_CODE"].Value.ToString(), out int ROOM_CODE))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteRoom(ROOM_CODE); // Now passing an int

                        // Refresh DataGridView
                        dataGridViewR.DataSource = mySQLConnector.FetchRoom();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid BookID format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void button45_Click(object sender, EventArgs e)
        {
            textBoxRoom.Clear();
            textBoxbco.Clear();
            textBoxty.Clear();

        }

        private void button46_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSh.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewR.DataSource = mySQLConnector.SearchRoom(keyword);

        }

        private void dataGridViewR_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewR.CurrentRow != null)
            {
                DataGridViewRow row = dataGridViewR.CurrentRow;

                textBoxRoom.Text = row.Cells[0].Value?.ToString() ?? "";
                textBoxbco.Text = row.Cells[2].Value?.ToString() ?? "";
                textBoxty.Text = row.Cells[1].Value?.ToString() ?? "";

            }

        }

        private void button50_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(textBoxProfessor.Text, out int PROF_NUM))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxS.Text, out int PROF_SPECIALTY))
            {
                MessageBox.Show("Please enter a valid Professor Specialty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxrank.Text, out int PROF_RANK))
            {
                MessageBox.Show("Please enter a valid Professor Rank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxL.Text, out int PROF_LNAME))
            {
                MessageBox.Show("Please enter a valid Last Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxF.Text, out int PROF_FNAME))
            {
                MessageBox.Show("Please enter a valid First Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxI.Text, out int PROF_INITIAL))
            {
                MessageBox.Show("Please enter a valid Initial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxEm.Text, out int PROF_EMAIL))
            {
                MessageBox.Show("Please enter a valid Email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxde.Text, out int DEPT_CODE))
            {
                MessageBox.Show("Please enter a valid Department Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


                mySQLConnector.InsertProfessor(PROF_NUM, textBoxS.Text, PROF_RANK, textBoxL.Text,textBoxF.Text, textBoxI.Text, textBoxEm.Text, DEPT_CODE);
                LoadProfessor();

            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;

                // Assign values using column indexes
                textBoxProfessor.Text = row.Cells[0].Value?.ToString() ?? ""; // AuthorID
                textBoxrank.Text = row.Cells[1].Value?.ToString() ?? ""; // Last Name
                textBoxL.Text = row.Cells[2].Value?.ToString() ?? "";
                textBoxF.Text = row.Cells[3].Value?.ToString() ?? "";
                textBoxI.Text = row.Cells[4].Value?.ToString() ?? "";
                textBoxEm.Text = row.Cells[5].Value?.ToString() ?? "";
                textBoxde.Text = row.Cells[6].Value?.ToString() ?? "";

            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxProfessor.Text, out int PROF_NUM))
            {
                MessageBox.Show("Please enter a valid Professor Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxS.Text, out int PROF_SPECIALTY))
            {
                MessageBox.Show("Please enter a valid Professor Specialty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxrank.Text, out int PROF_RANK))
            {
                MessageBox.Show("Please enter a valid Professor Rank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxL.Text, out int PROF_LNAME))
            {
                MessageBox.Show("Please enter a valid Last Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxF.Text, out int PROF_FNAME))
            {
                MessageBox.Show("Please enter a valid First Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxI.Text, out int PROF_INITIAL))
            {
                MessageBox.Show("Please enter a valid Initial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxEm.Text, out int PROF_EMAIL))
            {
                MessageBox.Show("Please enter a valid Email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxde.Text, out int DEPT_CODE))
            {
                MessageBox.Show("Please enter a valid Department Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

                mySQLConnector.UpdateProfessor(PROF_NUM, textBoxS.Text, PROF_RANK, textBoxL.Text, textBoxF.Text, textBoxI.Text, textBoxEm.Text, DEPT_CODE);
                LoadProfessor();
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                // Convert PROF_NUM to an integer
                if (int.TryParse(dataGridView1.Rows[rowIndex].Cells["PROF_NUM"].Value.ToString(), out int PROF_NUM))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        mySQLConnector.DeleteProfessor(PROF_NUM); // Now passing an int

                        // Refresh DataGridView
                        dataGridView1.DataSource = mySQLConnector.FetchProfessor();

                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Professor Number format. Please select a valid record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            textBoxProfessor.Clear();
            textBoxS.Clear();
            textBoxrank.Clear();
            textBoxL.Clear();
            textBoxF.Clear();
            textBoxI.Clear();
            textBoxEm.Clear();
            textBoxde.Clear();
        }

        private void textBoxProfessor_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxcrs_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string keyword = textBox7.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Please enter a keyword to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridView1.DataSource = mySQLConnector.SearchProfessor(keyword);
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label64_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSM_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCL_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEC_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxed_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxeg_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSTU_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxST_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSL_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSF_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSE_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewSc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxdc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
