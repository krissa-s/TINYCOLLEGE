using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tiny_College
{

    public class MySQLConnector
    {
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=tinycollege;";
        internal class mSQLCollege
        {
        }
        private void ExecuteNonQuery(string query, params (string, object)[] parameters)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Item1, param.Item2);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        // Generalized method to execute select queries and return a DataTable
        private DataTable ExecuteQuery(string query)
        {
            DataTable table = new DataTable();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            return table;
        }
        /*SCHOOL*/
        public void DeleteSchool(int SCHOOL_CODE)
        {
            string query = "DELETE FROM SCHOOL WHERE SCHOOL_CODE = @SCHOOL_CODE";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SCHOOL_CODE", SCHOOL_CODE);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void InsertSchool(int schoolCode, string schoolName, int PROF_NUM)
        {
            string checkQuery = "SELECT COUNT(*) FROM SCHOOL WHERE SCHOOL_CODE = @schoolCode";
            string insertQuery = "INSERT INTO SCHOOL (SCHOOL_CODE, SCHOOL_NAME, PROF_NUM) VALUES (@schoolCode, @schoolName, @profNum)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the record exists
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@schoolCode", schoolCode);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Error: School Code already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert if it does not exist
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@schoolCode", schoolCode);
                        insertCmd.Parameters.AddWithValue("@schoolName", schoolName);
                        insertCmd.Parameters.AddWithValue("@profNum", PROF_NUM);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        public void UpdateSchool(int schoolcode, string schoolName, int profNum)
        {
            string query = "UPDATE SCHOOL SET SCHOOL_NAME = @schoolName, PROF_NUM = @profNum WHERE SCHOOL_CODE = @schoolCode";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@schoolCode", schoolcode);
                        command.Parameters.AddWithValue("@schoolName", schoolName);
                        command.Parameters.AddWithValue("@profNum", profNum);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No book found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable SearchSchool(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM SCHOOL WHERE SCHOOL_CODE LIKE @keyword OR SCHOOL_NAME LIKE @keyword OR PROF_NUM LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public DataTable FetchSchool()
        {
            string query = "SELECT * FROM SCHOOL";
            return ExecuteQuery(query);
        }
     


        /*PROFESSOR*/
        public void DeleteProfessor(int PROF_NUM)
        {
            string query = "DELETE FROM PROFESSOR WHERE PROF_NUM = @PROF_NUM";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PROF_NUM", PROF_NUM);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsertProfessor(int profNum, string profSpecialty, int profRank, string profLname, string profFname, string profInitial, string profEmail, int DEPT_CODE)
        {
            string checkQuery = "SELECT COUNT(*) FROM PROFESSOR WHERE PROF_NUM = @profNum";
            string insertQuery = "INSERT INTO PROFESSOR (PROF_NUM, PROF_SPECIALTY, PROF_RANK, PROF_LNAME, PROF_FNAME, PROF_INITIAL, PROF_EMAIL, DEPT_CODE) VALUES (@profNum, @profSpecialty, @profRank, @profLname, @profFname, @profInitial, @profEmail, @DEPT_CODE)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the record exists
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@profNum", profNum);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Error: Professor Number already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert if it does not exist
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@profNum", profNum);
                        insertCmd.Parameters.AddWithValue("@profSpecialty", profSpecialty);
                        insertCmd.Parameters.AddWithValue("@profRank", profRank);
                        insertCmd.Parameters.AddWithValue("@profLname", profLname);
                        insertCmd.Parameters.AddWithValue("@profFname", profFname);
                        insertCmd.Parameters.AddWithValue("@profInitial", profInitial);
                        insertCmd.Parameters.AddWithValue("@profEmail", profEmail);
                        insertCmd.Parameters.AddWithValue("@DEPT_CODE", DEPT_CODE);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void UpdateProfessor(int profNum, string profSpecialty, int profRank, string profLname, string profFname, string profInitial, string profEmail,int DEPT_CODE)
        {
            string query = "UPDATE PROFESSOR SET PROF_SPECIALTY = @profSpecialty, PROF_RANK = @profRank, PROF_LNAME = @profLname, PROF_FNAME = @profFname, PROF_INITIAL = @profInitial, PROF_EMAIL = @profEmail WHERE PROF_NUM = @profNum, DEPT_CODE = @DEPT_CODE";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@profNum", profNum);
                        command.Parameters.AddWithValue("@profSpecialty", profSpecialty);
                        command.Parameters.AddWithValue("@profRank", profRank);
                        command.Parameters.AddWithValue("@profLname", profLname);
                        command.Parameters.AddWithValue("@profFname", profFname);
                        command.Parameters.AddWithValue("@profInitial", profInitial);
                        command.Parameters.AddWithValue("@profEmail", profEmail);
                        command.Parameters.AddWithValue("@DEPT_CODE", DEPT_CODE);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Professor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No professor found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating professor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable SearchProfessor(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM PROFESSOR WHERE PROF_NUM LIKE @keyword OR PROF_SPECIALTY LIKE @keyword OR PROF_RANK LIKE @keyword OR PROF_LNAME LIKE @keyword OR PROF_FNAME LIKE @keyword OR PROF_INITIAL LIKE @keyword OR PROF_EMAIL LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching professors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public DataTable FetchProfessor()
        {
            string query = "SELECT * FROM PROFESSOR";
            return ExecuteQuery(query);
        }


        /*Department*/
        public void DeleteDepartment(int DEPT_CODE)
        {
            string query = "DELETE FROM DEPARTMENT WHERE DEPT_CODE = @DEPT_CODE";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DEPT_CODE", DEPT_CODE);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsertDepartment(int deptCode, string deptName, int schoolCode, int PROF_NUM)
        {
            string checkQuery = "SELECT COUNT(*) FROM DEPARTMENT WHERE DEPT_CODE = @deptCode";
            string insertQuery = "INSERT INTO DEPARTMENT (DEPT_CODE, DEPT_NAME, SCHOOL_CODE, PROF_NUM) VALUES (@deptCode, @deptName, @schoolCode,@PROF_NUM)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the department already exists
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@deptCode", deptCode);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Error: Department Code already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert if it does not exist
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@deptCode", deptCode);
                        insertCmd.Parameters.AddWithValue("@deptName", deptName);
                        insertCmd.Parameters.AddWithValue("@schoolCode", schoolCode);
                        insertCmd.Parameters.AddWithValue("@PROF_NUM", PROF_NUM);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

      public void UpdateDepartment(int deptCode, string deptName, int schoolCode, int PROF_NUM)
        {
            string query = "UPDATE DEPARTMENT SET DEPT_NAME = @deptName, SCHOOL_CODE = @schoolCode WHERE DEPT_CODE = @deptCode PROF_NUM = @PROF_NUM";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@deptCode", deptCode);
                        command.Parameters.AddWithValue("@deptName", deptName);
                        command.Parameters.AddWithValue("@schoolCode", schoolCode);
                        command.Parameters.AddWithValue("@PROF_NUM", PROF_NUM);


                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Department updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No department found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating department: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable SearchDepartment(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM DEPARTMENT WHERE DEPT_CODE LIKE @keyword OR DEPT_NAME LIKE @keyword OR SCHOOL_CODE LIKE @keyword OR PROF_NUM LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching departments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public DataTable FetchDepartment()
        {
            string query = "SELECT * FROM DEPARTMENT";
            return ExecuteQuery(query);
        }


        /*COURSE*/
        public void DeleteCourse(int CRS_CODE)
        {
            string query = "DELETE FROM COURSE WHERE CRS_CODE = @CRS_CODE";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CRS_CODE", CRS_CODE);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        public void InsertCourse(int crsCode, string crsTitle, string crsDescription, int crsCredit, int deptCode)
        {
            string checkQuery = "SELECT COUNT(*) FROM COURSE WHERE CRS_CODE = @crsCode";
            string insertQuery = "INSERT INTO COURSE (CRS_CODE, CRS_TITLE, CRS_DESCRIPTION, CRS_CREDIT, DEPT_CODE) VALUES (@crsCode, @crsTitle, @crsDescription, @crsCredit, @deptCode)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                   
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@crsCode", crsCode);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Error: Course Code already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert if it does not exist
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@crsCode", crsCode);
                        insertCmd.Parameters.AddWithValue("@crsTitle", crsTitle);
                        insertCmd.Parameters.AddWithValue("@crsDescription", crsDescription);
                        insertCmd.Parameters.AddWithValue("@crsCredit", crsCredit);
                        insertCmd.Parameters.AddWithValue("@deptCode", deptCode);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

      
        public void UpdateCourse(int crsCode, string crsTitle, string crsDescription, int crsCredit, int deptCode)
        {
            string query = "UPDATE COURSE SET CRS_TITLE = @crsTitle, CRS_DESCRIPTION = @crsDescription, CRS_CREDIT = @crsCredit, DEPT_CODE = @deptCode WHERE CRS_CODE = @crsCode";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@crsCode", crsCode);
                        command.Parameters.AddWithValue("@crsTitle", crsTitle);
                        command.Parameters.AddWithValue("@crsDescription", crsDescription);
                        command.Parameters.AddWithValue("@crsCredit", crsCredit);
                        command.Parameters.AddWithValue("@deptCode", deptCode);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Course updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No course found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating course: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     
        public DataTable SearchCourse(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM COURSE WHERE CRS_CODE LIKE @keyword OR CRS_TITLE LIKE @keyword OR CRS_DESCRIPTION LIKE @keyword OR CRS_CREDIT LIKE @keyword OR DEPT_CODE LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public DataTable FetchCourse()
        {
            string query = "SELECT * FROM Course";
            return ExecuteQuery(query);
        }


        /*BUILDING*/
        public void DeleteBuilding(int BLDG_CODE)
        {
            string query = "DELETE FROM BUILDING WHERE BLDG_CODE = @BLDG_CODE";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BLDG_CODE", BLDG_CODE);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Insert Building */
        public void InsertBuilding(int bldgCode, string bldgName, string bldgLocation)
        {
            string checkQuery = "SELECT COUNT(*) FROM BUILDING WHERE BLDG_CODE = @bldgCode";
            string insertQuery = "INSERT INTO BUILDING (BLDG_CODE, BLDG_NAME, BLDG_LOCATION) VALUES (@bldgCode, @bldgName, @bldgLocation)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                 
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@bldgCode", bldgCode);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Error: Building Code already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@bldgCode", bldgCode);
                        insertCmd.Parameters.AddWithValue("@bldgName", bldgName);
                        insertCmd.Parameters.AddWithValue("@bldgLocation", bldgLocation);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

     
        public void UpdateBuilding(int bldgCode, string bldgName, string bldgLocation)
        {
            string query = "UPDATE BUILDING SET BLDG_NAME = @bldgName, BLDG_LOCATION = @bldgLocation WHERE BLDG_CODE = @bldgCode";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@bldgCode", bldgCode);
                        command.Parameters.AddWithValue("@bldgName", bldgName);
                        command.Parameters.AddWithValue("@bldgLocation", bldgLocation);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Building updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No building found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating building: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        public DataTable SearchBuilding(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM BUILDING WHERE BLDG_CODE LIKE @keyword OR BLDG_NAME LIKE @keyword OR BLDG_LOCATION LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching buildings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

      
        public DataTable FetchBuilding()
        {
            string query = "SELECT * FROM BUILDING";
            return ExecuteQuery(query);
        }

        /*CLASS*/

        public void DeleteClass(int CLASS_CODE)
        {
            string query = "DELETE FROM CLASS WHERE CLASS_CODE = @CLASS_CODE";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CLASS_CODE", CLASS_CODE);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsertClass(int classCode, string classSection, string classTime,  int crsCode, int profNum, int roomCode, int semesterCode)
        {
            string checkQuery = "SELECT COUNT(*) FROM CLASS WHERE CLASS_CODE = @classCode";
            string insertQuery = "INSERT INTO CLASS (CLASS_CODE, CLASS_SECTION, CLASS_TIME, CRS_CODE, PROF_NUM, ROOM_CODE, SEMESTER_CODE) VALUES (@classCode, @classSection, @classTime, @crsCode, @profNum, @roomCode, @semesterCode)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@classCode", classCode);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Error: Class Code already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@classCode", classCode);
                        insertCmd.Parameters.AddWithValue("@classSection", classSection);
                        insertCmd.Parameters.AddWithValue("@classTime", classTime);
                        insertCmd.Parameters.AddWithValue("@crsCode", crsCode);
                        insertCmd.Parameters.AddWithValue("@profNum", profNum);
                        insertCmd.Parameters.AddWithValue("@roomCode", roomCode);
                        insertCmd.Parameters.AddWithValue("@semesterCode", semesterCode);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        public void UpdateClass(int classCode, string classSection, string classTime, int crsCode, int profNum, int roomCode, int semesterCode)
        {
            string query = "UPDATE CLASS SET CLASS_SECTION = @classSection, CLASS_TIME = @classTime, CRS_CODE = @crsCode, PROF_NUM = @profNum, ROOM_CODE = @roomCode, SEMESTER_CODE = @semesterCode WHERE CLASS_CODE = @classCode";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@classCode", classCode);
                        command.Parameters.AddWithValue("@classSection", classSection);
                        command.Parameters.AddWithValue("@classTime", classTime);
                        command.Parameters.AddWithValue("@crsCode", crsCode);
                        command.Parameters.AddWithValue("@profNum", profNum);
                        command.Parameters.AddWithValue("@roomCode", roomCode);
                        command.Parameters.AddWithValue("@semesterCode", semesterCode);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Class updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No class found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating class: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable SearchClass(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM CLASS WHERE CLASS_CODE LIKE @keyword OR CLASS_SECTION LIKE @keyword OR CLASS_TIME LIKE @keyword OR CRS_CODE LIKE @keyword OR PROF_NUM LIKE @keyword OR ROOM_CODE LIKE @keyword OR SEMESTER_CODE LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching classes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public DataTable FetchClass()
        {
            string query = "SELECT * FROM CLASS";
            return ExecuteQuery(query);
        }


        /*SEMESTER*/
        public void DeleteSemester(int semesterCode)
        {
            string query = "DELETE FROM SEMESTER WHERE SEMESTER_CODE = @semesterCode";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@semesterCode", semesterCode);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting semester: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void InsertSemester(int semesterCode, int semesterYear, int semesterTerm, string semesterStartDate, string semesterEndDate)
        {
            string query = "INSERT INTO SEMESTER (SEMESTER_CODE, SEMESTER_YEAR, SEMESTER_TERM, SEMESTER_START_DATE, SEMESTER_END_DATE) VALUES (@semesterCode, @semesterYear, @semesterTerm, @semesterStartDate,@semesterEndDate)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@semesterCode", semesterCode);
                        command.Parameters.AddWithValue("@semesterYear", semesterYear);
                        command.Parameters.AddWithValue("@semesterTerm", semesterTerm);
                        command.Parameters.AddWithValue("@semesterStartDate", semesterStartDate);
                        command.Parameters.AddWithValue("@semesterEndDate", semesterEndDate);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting semester: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      


        public void UpdateSemester(int semesterCode, int semesterYear, int semesterTerm, string semesterStartDate, string semesterEndDate)
        {
            string query = "UPDATE SEMESTER SET SEMESTER_YEAR = @semesterYear, SEMESTER_TERM = @semesterTerm, SEMESTER_START_DATE = @semesterStartDate, SEMESTER_END_DATE = @semesterEndDate WHERE SEMESTER_CODE = @semesterCode";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@semesterCode", semesterCode);
                        command.Parameters.AddWithValue("@semesterYear", semesterYear);
                        command.Parameters.AddWithValue("@semesterTerm", semesterTerm);
                        command.Parameters.AddWithValue("@semesterStartDate", semesterStartDate);
                        command.Parameters.AddWithValue("@semesterEndDate", semesterEndDate);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating semester: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable FetchSemester()
        {
            string query = "SELECT * FROM SEMESTER";
            return ExecuteQuery(query);
        }
        public DataTable SearchSemester(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM SEMESTER WHERE SEMESTER_CODE LIKE @keyword OR SEMESTER_YEAR LIKE @keyword OR SEMESTER_TERM LIKE @keyword OR SEMESTER_START_DATE LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching semester: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public void DeleteEnroll(int enrollCode)
        {
            string query = "DELETE FROM ENROLL WHERE ENROLL_CODE = @enrollCode";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enrollCode", enrollCode);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting enrollment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void InsertEnroll(int enrollCode, DateTime enrollDate, int enrollGrade, int classCode, int stuNum)
        {
            string query = "INSERT INTO ENROLL (ENROLL_CODE, ENROLL_DATE, ENROLL_GRADE, CLASS_CODE, STU_NUM) VALUES (@enrollCode, @enrollDate, @enrollGrade, @classCode, @stuNum)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enrollCode", enrollCode);
                        command.Parameters.AddWithValue("@enrollDate", enrollDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@enrollGrade", enrollGrade);
                        command.Parameters.AddWithValue("@classCode", classCode);
                        command.Parameters.AddWithValue("@stuNum", stuNum);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting enrollment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void UpdateEnroll(int enrollCode, DateTime enrollDate, int enrollGrade, int classCode, int stuNum)
        {
            string query = "UPDATE ENROLL SET ENROLL_DATE = @enrollDate, ENROLL_GRADE = @enrollGrade, " +
                           "CLASS_CODE = @classCode, STU_NUM = @stuNum WHERE ENROLL_CODE = @enrollCode";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@enrollCode", enrollCode);
                        command.Parameters.AddWithValue("@enrollDate", enrollDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@enrollGrade", enrollGrade);
                        command.Parameters.AddWithValue("@classCode", classCode);
                        command.Parameters.AddWithValue("@stuNum", stuNum);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Enrollment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No enrollment found with the given code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating enrollment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable SearchEnroll(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM ENROLL WHERE ENROLL_CODE LIKE @keyword OR STU_NUM LIKE @keyword OR CLASS_CODE LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching enrollments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public DataTable FetchEnroll()
        {
            string query = "SELECT * FROM ENROLL";
            return ExecuteQuery(query);
        }

        /*STUDENT*/
        public void InsertStudent(int stuNum, string stuLname, string stuFname, string stuInitial, string stuEmail, int deptCode, int profNum)
        {
            string query = "INSERT INTO STUDENT (STU_NUM, STU_LNAME, STU_FNAME, STU_INITIAL, STU_EMAIL, DEPT_CODE, PROF_NUM) " +
                           "VALUES (@stuNum, @stuLname, @stuFname, @stuInitial, @stuEmail, @deptCode, @profNum)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@stuNum", stuNum);
                        command.Parameters.AddWithValue("@stuLname", stuLname);
                        command.Parameters.AddWithValue("@stuFname", stuFname);
                        command.Parameters.AddWithValue("@stuInitial", stuInitial);
                        command.Parameters.AddWithValue("@stuEmail", stuEmail);
                        command.Parameters.AddWithValue("@deptCode", deptCode);
                        command.Parameters.AddWithValue("@profNum", profNum);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting student: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DeleteStudent(int stuNum)
        {
            string query = "DELETE FROM STUDENT WHERE STU_NUM = @stuNum";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@stuNum", stuNum);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable SearchStudent(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM STUDENT WHERE STU_NUM LIKE @keyword OR STU_LNAME LIKE @keyword OR STU_FNAME LIKE @keyword OR STU_EMAIL LIKE @keyword OR DEPT_CODE LIKE @keyword OR PROF_NUM LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching students: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public void UpdateStudent(int stuNum, string stuLname, string stuFname, string stuInitial, string stuEmail, int deptCode, int profNum)
        {
            string query = "UPDATE STUDENT SET STU_LNAME = @stuLname, STU_FNAME = @stuFname, STU_INITIAL = @stuInitial, STU_EMAIL = @stuEmail, DEPT_CODE = @deptCode, PROF_NUM = @profNum WHERE STU_NUM = @stuNum";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@stuNum", stuNum);
                        command.Parameters.AddWithValue("@stuLname", stuLname);
                        command.Parameters.AddWithValue("@stuFname", stuFname);
                        command.Parameters.AddWithValue("@stuInitial", stuInitial);
                        command.Parameters.AddWithValue("@stuEmail", stuEmail);
                        command.Parameters.AddWithValue("@deptCode", deptCode);
                        command.Parameters.AddWithValue("@profNum", profNum);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No student found with the given Student Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable FetchStudent()
        {
            string query = "SELECT * FROM STUDENT";
            return ExecuteQuery(query);
        }

        /*ROOM*/
        public void DeleteRoom(int roomCode)
        {
            string query = "DELETE FROM ROOM WHERE ROOM_CODE = @roomCode";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomCode", roomCode);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Room deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No room found with the given code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting room: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void UpdateRoom(int roomCode, string roomType, int bldgCode)
        {
            string query = "UPDATE ROOM SET ROOM_TYPE = @roomType, BLDG_CODE = @bldgCode WHERE ROOM_CODE = @roomCode";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomCode", roomCode);
                        command.Parameters.AddWithValue("@roomType", roomType);
                        command.Parameters.AddWithValue("@bldgCode", bldgCode);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Room updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No room found with the given code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating room: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable SearchRoom(string keyword)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM ROOM WHERE ROOM_CODE LIKE @keyword OR ROOM_TYPE LIKE @keyword OR BLDG_CODE LIKE @keyword";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching rooms: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public void InsertRoom(int roomCode, string roomType, int bldgCode)
        {
            string query = "INSERT INTO ROOM (ROOM_CODE, ROOM_TYPE, BLDG_CODE) VALUES (@roomCode, @roomType, @bldgCode)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomCode", roomCode);
                        command.Parameters.AddWithValue("@roomType", roomType);
                        command.Parameters.AddWithValue("@bldgCode", bldgCode);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting room: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public DataTable FetchRoom()
        {
            string query = "SELECT * FROM ROOM";
            return ExecuteQuery(query);
        }



    }
}
