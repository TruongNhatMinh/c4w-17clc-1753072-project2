﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;

namespace Student_Management.DAL
{
    class DataAccess
    {
        string conectionString = "Provider=SQLNCLI11;Server=DESKTOP-9MGEK4O\\SQLEXPRESS;Database=QLSV;Trusted_Connection=yes;";
        OleDbConnection cnn;
        public DataAccess()
        {
            cnn = new OleDbConnection();
            cnn.ConnectionString = conectionString;
        }

        public bool isAccountExist(string username)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.CommandText = $"Select Acc.Password FROM Account Acc WHERE Acc.Username= ?";
            cmd.Parameters.AddWithValue("@Username", username);
            var rd = cmd.ExecuteReader();
            bool res = rd.Read();
            cnn.Close();
            return res;
        }

        public int comparePassword(string username, string password)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.CommandText = $"Select Acc.Password, Acc.Type FROM Account Acc WHERE Acc.Username = ?";
            cmd.Parameters.AddWithValue("@Username", username);
            var rd = cmd.ExecuteReader();
            int state = 0;
            if (rd.Read())
            {
                if (password.Equals(rd.GetString(0)))
                {
                    if (rd.GetString(1).Contains("gv"))
                        state = 3;
                    else
                        state = 2;
                }
            }
            cnn.Close();
            return state;
        }

        public bool isClassExist(string path)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.CommandText = $"Select Count(*) FROM Class C WHERE C.MALOP = ?";
            cmd.Parameters.AddWithValue("@Username", $"{Path.GetFileNameWithoutExtension(path)}");
            var rd = cmd.ExecuteScalar();
            bool res = Convert.ToBoolean(rd);
            cnn.Close();
            return res;
        }

        public List<string[]> addClass(string filePath)
        {
            List<string[]> saveClass = new List<string[]>();
            string[] parameters;
            char[] spliter = new char[] { ',' };
            StreamReader sourceFile = new StreamReader(filePath);
            string nameClass = "";

            parameters = sourceFile.ReadLine().Split(spliter, 7);
            if(parameters.Length == 1)
            {
                nameClass = parameters[0];
                parameters = sourceFile.ReadLine().Split(spliter, 7);
                if (parameters.Length != 7)
                    return saveClass;
            }

            int siso = 0;

            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            OleDbCommand cmd2 = new OleDbCommand();
            cmd.Connection = cnn;
            cmd2.Connection = cnn;
            cmd.CommandText = $"INSERT INTO Class(MALOP) VALUES(?)";
            cmd.Parameters.AddWithValue("@MALOP", $"{Path.GetFileNameWithoutExtension(filePath)}");
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                try
                {
                    OleDbCommand deleteCmd = new OleDbCommand();
                    deleteCmd.Connection = cnn;
                    deleteCmd.CommandText = "DELETE FROM Student WHERE MALOP = ?";
                    deleteCmd.Parameters.AddWithValue("@MALOP", Path.GetFileNameWithoutExtension(filePath));
                    deleteCmd.ExecuteNonQuery();

                    deleteCmd.CommandText = "DELETE FROM Account WHERE Username IN (SELECT SV.MSSV FROM Student SV WHERE SV.MALOP = ?)";

                    deleteCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            while (!sourceFile.EndOfStream)
            {
                siso++;
                cmd.CommandText = $"INSERT INTO Student(STT, MSSV, HOTEN, GIOITINH, CMND, NGAYSINH, DIACHI, MALOP) VALUES(?, ?, ?, ?, ?, ?, ?, ?)";
                cmd2.CommandText = $"INSERT INTO Account VALUES(?, ?, ?)";
                parameters = sourceFile.ReadLine().Split(spliter, 7);

                parameters = new string[] { parameters[0].Replace(" ", ""), parameters[1].Replace(" ", ""),
                                            parameters[2], parameters[3].Replace(" ", ""), parameters[4].Replace(" ", ""),
                                            parameters[5].Replace(" ", ""),parameters[6], nameClass};

                saveClass.Add(parameters);

                try
                {
                    cmd2.Parameters.AddWithValue("@Username", parameters[1].Replace(" ", ""));
                    cmd2.Parameters.AddWithValue("@Password", parameters[5].Replace("/", ""));
                    cmd2.Parameters.AddWithValue("@Type", "sv");
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    cmd2.Parameters.Clear();
                }
                try
                {
                    cmd.Parameters.AddWithValue("@STT", Int32.Parse(parameters[0]));
                    cmd.Parameters.AddWithValue("@MSSV", parameters[1]);
                    cmd.Parameters.AddWithValue("@HOTEN", parameters[2]);
                    cmd.Parameters.AddWithValue("@GIOITINH", parameters[3]);
                    cmd.Parameters.AddWithValue("@CMND", parameters[4]);
                    cmd.Parameters.AddWithValue("@NGAYSINH", parameters[5]);
                    cmd.Parameters.AddWithValue("@DIACHI", parameters[6]);
                    cmd.Parameters.AddWithValue("@MALOP", nameClass);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }

            cmd.CommandText = $"UPDATE Class set SISO = ? Where MALOP = ?";
            try
            {
                cmd.Parameters.AddWithValue("@SISO", siso);
                cmd.Parameters.AddWithValue("@MALOP", nameClass);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
            }

            cnn.Close();
            return saveClass;
        }

        public List<string[]> addSchedule(string path)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            OleDbCommand cmd2 = new OleDbCommand();
            cmd.Connection = cnn;
            cmd2.Connection = cnn;

            List<string[]> saveSchedule = new List<string[]>();
            string[] parameters;
            char[] spliter = new char[] { ',' };
            StreamReader sourceFile = new StreamReader(path);

            parameters = sourceFile.ReadLine().Split(spliter, 4);
            if (parameters.Length != 1)
                return saveSchedule;
            string nameClass = parameters[0];
            parameters = sourceFile.ReadLine().Split(spliter, 4);


            OleDbCommand deleteCmd = new OleDbCommand();
            deleteCmd.Connection = cnn;
            deleteCmd.CommandText = $"DELETE FROM StudentOfCourse WHERE MALOP = '{nameClass}' AND IDDELETE is null";
            deleteCmd.ExecuteNonQuery();

            while (!sourceFile.EndOfStream)
            {
                cmd.CommandText = $"INSERT INTO Schedule(STT, MAMON, TENMON, PHONGHOC, MALOP) VALUES(?, ?, ?, ?, ?)";

                cmd2.CommandText = $"INSERT INTO StudentOfCourse(MALOP, MAMON) VALUES(?, ?)";
                parameters = sourceFile.ReadLine().Split(spliter, 4);

                parameters = new string[] { parameters[0].Replace(" ", ""), parameters[1].Replace(" ", ""),
                                            parameters[2], parameters[3].Replace(" ", ""), nameClass};

                saveSchedule.Add(parameters);

                try
                {
                    cmd.Parameters.AddWithValue("@STT", Int32.Parse(parameters[0]));
                    cmd.Parameters.AddWithValue("@MAMON", parameters[1]);
                    cmd.Parameters.AddWithValue("@TENMON", parameters[2]);
                    cmd.Parameters.AddWithValue("@PHONGHOC", parameters[3]);
                    cmd.Parameters.AddWithValue("@MALOP", nameClass);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    cmd.Parameters.Clear();
                }

                try
                {
                    cmd2.Parameters.AddWithValue("@MALOP", nameClass);
                    cmd2.Parameters.AddWithValue("@MAMON", parameters[1]);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    cmd2.Parameters.Clear();
                }
            }
            cnn.Close();
            return saveSchedule;
        }

        public List<string[]> addScoreboard(string path)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            List<string[]> saveScoreboard = new List<string[]>();
            string[] parameters;
            char[] spliter = new char[] { ',' };
            StreamReader sourceFile = new StreamReader(path);

            parameters = sourceFile.ReadLine().Split(spliter, 2);
            if (parameters.Length != 2)
                return saveScoreboard;
            string nameClass = parameters[0];
            string codeCourses = parameters[1];

            try
            {
                OleDbCommand deleteCmd = new OleDbCommand();
                deleteCmd.Connection = cnn;
                deleteCmd.CommandText = "DELETE FROM Scoreboard WHERE MALOP = ? AND MAMON = ?";
                deleteCmd.Parameters.AddWithValue("@MALOP", nameClass);
                deleteCmd.Parameters.AddWithValue("@MAMON", codeCourses);
                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            parameters = sourceFile.ReadLine().Split(spliter, 7);

            while (!sourceFile.EndOfStream)
            {
                cmd.CommandText = $"INSERT INTO Scoreboard(STT, MSSV, HOTEN, MAMON, DIEMGK, DIEMCK, DIEMKHAC, DIEMTB, MALOP) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?)";
                parameters = sourceFile.ReadLine().Split(spliter, 7);

                parameters = new string[] { parameters[0].Replace(" ", ""), parameters[1].Replace(" ", ""),
                                            parameters[2], codeCourses, parameters[3].Replace(" ", ""), parameters[4].Replace(" ", ""),
                                            parameters[5].Replace(" ", ""), parameters[6].Replace(" ", ""), nameClass};

                saveScoreboard.Add(parameters);


                try
                {
                    cmd.Parameters.AddWithValue("@STT", Int32.Parse(parameters[0]));
                    cmd.Parameters.AddWithValue("@MSSV", parameters[1]);
                    cmd.Parameters.AddWithValue("@HOTEN", parameters[2]);
                    cmd.Parameters.AddWithValue("@MAMON", codeCourses);
                    cmd.Parameters.AddWithValue("@DIEMGK", parameters[4]);
                    cmd.Parameters.AddWithValue("@DIEMCK", parameters[5]);
                    cmd.Parameters.AddWithValue("@DIEMKHAC", parameters[6]);
                    cmd.Parameters.AddWithValue("@DIEMTB", parameters[7]);
                    cmd.Parameters.AddWithValue("@MALOP", nameClass);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception) { }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
            cnn.Close();
            return saveScoreboard;
        }

        public List<string> nameClass(string user)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            List<string> saveNameClass = new List<string>();

            if(user == null)
            {
                cmd.CommandText = $"Select MALOP From Class";
            }

            else
            {
                cmd.CommandText = $"Select MALOP From Student Where MSSV = ?";
                cmd.Parameters.AddWithValue("@MSSV", user);
            }

            try
            {

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    saveNameClass.Add(rd.GetString(0));
                }
                
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
                cnn.Close();
            }
            return saveNameClass;
        }

        public List<string[]> viewClass(string nClass)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            List<string[]> saveClass = new List<string[]>();

            cmd.CommandText = $"SELECT STT, MSSV, HOTEN, GIOITINH, CMND, NGAYSINH, DIACHI, MALOP FROM Student WHERE MALOP = ?";
            cmd.Parameters.AddWithValue("@MALOP", nClass);
            var rd = cmd.ExecuteReader();        

            while (rd.Read())
            {
                string[] parameters = new string[] {rd.GetInt32(0).ToString(), rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetString(4),
                rd.GetDateTime(5).ToShortDateString().ToString(), rd.GetString(6), rd.GetString(7)};
                saveClass.Add(parameters);
            }
 
            cnn.Close();
            return saveClass;
        }

        public List<string[]> viewClassOfCourses(string nClass, string nCourses)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            List<string[]> saveClass = new List<string[]>();

            cmd.CommandText = $"SELECT STT, MSSV, HOTEN, GIOITINH, CMND, NGAYSINH, DIACHI, MALOP FROM Student Where (MALOP = ? " +
                $"AND MSSV NOT IN(SELECT IDDELETE FROM StudentOfCourse WHERE MALOP = ? AND MAMON = ? AND IDDELETE is not null)) " +
                $"OR MSSV IN(SELECT IDSIGN FROM StudentOfCourse WHERE MALOP = ? AND MAMON = ? AND IDSIGN is not null)";
            cmd.Parameters.AddWithValue("@MALOP", nClass);
            cmd.Parameters.AddWithValue("@MALOP", nClass);
            cmd.Parameters.AddWithValue("@MAMON", nCourses);
            cmd.Parameters.AddWithValue("@MALOP", nClass);
            cmd.Parameters.AddWithValue("@MAMON", nCourses);
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                string[] parameters = new string[] {rd.GetInt32(0).ToString(), rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetString(4),
                rd.GetDateTime(5).ToShortDateString().ToString(), rd.GetString(6), rd.GetString(7)};
                saveClass.Add(parameters);
            }

            cnn.Close();
            return saveClass;
        }

        public void addStudent(List<string> information)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.Connection = cnn;
            cmd.CommandText = $"Select MAX(STT) From Student Where MALOP = ?";
            cmd.Parameters.AddWithValue("@MALOP", information[6]);
            int stt = (int)cmd.ExecuteScalar();
            stt++;
            cmd.Parameters.Clear();

            cmd.CommandText = $"INSERT INTO Student(STT, MSSV, HOTEN, GIOITINH, CMND, NGAYSINH, DIACHI, MALOP) VALUES(?, ?, ?, ?, ?, ?, ?, ?)";
            cmd2.CommandText = $"INSERT INTO Account VALUES(?, ?, ?)";



            try
            {
                OleDbCommand deleteCmd = new OleDbCommand();
                deleteCmd.Connection = cnn;
                deleteCmd.CommandText = "DELETE FROM ACCOUNT WHERE USERNAME = ?";
                deleteCmd.Parameters.AddWithValue("@USERNAME", information[0]);

                deleteCmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd2.Parameters.AddWithValue("@Username", information[0].Replace(" ", ""));
                cmd2.Parameters.AddWithValue("@Password", information[4].Replace("/", ""));
                cmd2.Parameters.AddWithValue("@Type", "sv");
                cmd2.ExecuteNonQuery();
                cmd2.Parameters.Clear();
            }

            try
            {
                cmd.Parameters.AddWithValue("@STT", stt);
                cmd.Parameters.AddWithValue("@MSSV", information[0]);
                cmd.Parameters.AddWithValue("@HOTEN", information[1]);
                cmd.Parameters.AddWithValue("@GIOITINH", information[2]);
                cmd.Parameters.AddWithValue("@CMND", information[3]);
                cmd.Parameters.AddWithValue("@NGAYSINH", information[4]);
                cmd.Parameters.AddWithValue("@DIACHI", information[5]);
                cmd.Parameters.AddWithValue("@MALOP", information[6]);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
            }

            cmd.CommandText = $"UPDATE Class set SISO = ? Where MALOP = ?";
            try
            {
                cmd.Parameters.AddWithValue("@SISO", stt);
                cmd.Parameters.AddWithValue("@MALOP", information[6]);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
            }

            cnn.Close();
        }

        public List<string> nameCourses(string nClass)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            List<string> saveNameCourses = new List<string>();

            try
            {
                cmd.CommandText = $"SELECT MAMON FROM Schedule WHERE MALOP =?";
                cmd.Parameters.AddWithValue("@MALOP", nClass);
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    saveNameCourses.Add(rd.GetString(0));
                }

            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
                cnn.Close();
            }
            return saveNameCourses;
        }

        public List<string[]> viewSchedule(string nClass)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            List<string[]> saveSchedule = new List<string[]>();

            cmd.CommandText = $"SELECT STT, MAMON, TENMON, PHONGHOC, MALOP FROM Schedule WHERE MALOP = ?";
            cmd.Parameters.AddWithValue("@MALOP", nClass);
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                string[] parameters = new string[] {rd.GetInt32(0).ToString(), rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetString(4)};
                saveSchedule.Add(parameters);
            }

            cnn.Close();
            return saveSchedule;
        }

        public List<string[]> viewScoreboard(string nClass, string mssv, string nCourses)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            List<string[]> saveScoreboard = new List<string[]>();

            if (mssv == null)
            {
                cmd.CommandText = $"Select STT, MSSV, HOTEN, MAMON, DIEMGK, DIEMCK, DIEMKHAC, DIEMTB, MALOP FROM Scoreboard Where MALOP = ? and MAMON = ?";
                cmd.Parameters.AddWithValue("@MALOP", nClass);
                cmd.Parameters.AddWithValue("@MAMON", nCourses);
            }
            else
            {
                cmd.CommandText = $"Select STT, MSSV, HOTEN, MAMON, DIEMGK, DIEMCK, DIEMKHAC, DIEMTB, MALOP FROM Scoreboard Where MALOP = ? and MSSV = ? and MAMON = ?";
                cmd.Parameters.AddWithValue("@MALOP", nClass);
                cmd.Parameters.AddWithValue("@MSSV", mssv);
                cmd.Parameters.AddWithValue("@MAMON", nCourses);
            }

            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                string[] parameters = new string[] { rd.GetInt32(0).ToString(), rd.GetString(1), rd.GetString(2), rd.GetString(3), ((float)rd.GetDouble(4)).ToString(),
                ((float)rd.GetDouble(5)).ToString(), ((float)rd.GetDouble(6)).ToString(), ((float)rd.GetDouble(7)).ToString(), rd.GetString(8)};
                saveScoreboard.Add(parameters);
            }

            cnn.Close();
            return saveScoreboard;
        }

        public List<int> getPaF(string nClass, string nCourses)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            List<int> PaF = new List<int>();
            float checkMark = 5;
            cmd.Connection = cnn;
            try
            {
                cmd.CommandText = $"Select Count(DIEMTB) From Scoreboard Where MALOP = ? and MAMON = ? and DIEMTB < ?";
                cmd.Parameters.AddWithValue("@MALOP", nClass);
                cmd.Parameters.AddWithValue("@MAMON", nCourses);
                cmd.Parameters.AddWithValue("@DIEMTB", checkMark);
                int stt = (int)cmd.ExecuteScalar();
                PaF.Add(stt);
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
                
            }


            try
            {
                cmd.CommandText = $"Select Count(DIEMTB) From Scoreboard Where MALOP = ? and MAMON = ? and DIEMTB >= ?";
                cmd.Parameters.AddWithValue("@MALOP", nClass);
                cmd.Parameters.AddWithValue("@MAMON", nCourses);
                cmd.Parameters.AddWithValue("@DIEMTB", checkMark);
                int stt = (int)cmd.ExecuteScalar();
                PaF.Add(stt);
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
            }

            cnn.Close();
            return PaF;
        }

        public void editMark(List<string> mark)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            cmd.CommandText = $"UPDATE Scoreboard set DIEMGK = ?, DIEMCK = ?, DIEMKHAC = ?, DIEMTB = ? Where MSSV = ?";
            try
            {
                cmd.Parameters.AddWithValue("@DIEMGK", float.Parse(mark[1]));
                cmd.Parameters.AddWithValue("@DIEMCK", float.Parse(mark[2]));
                cmd.Parameters.AddWithValue("@DIEMKHAC", float.Parse(mark[3]));
                cmd.Parameters.AddWithValue("@DIEMTB", float.Parse(mark[4]));
                cmd.Parameters.AddWithValue("@MSSV", mark[0]);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
                cnn.Close();
            }
        }

        public bool isStudentExist(string nClass, string mssv, string nCourses)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;
            List<string> saveNameCourses = new List<string>();
            int _check = 0;
            try
            {
                cmd.CommandText = $"SELECT Count(MSSV) FROM Scoreboard WHERE MALOP = ? AND MSSV = ? AND MAMON = ?";
                cmd.Parameters.AddWithValue("@MALOP", nClass);
                cmd.Parameters.AddWithValue("@MSSV", mssv);
                cmd.Parameters.AddWithValue("@MAMON", nCourses);
                var rd = cmd.ExecuteReader();
                _check = (int)cmd.ExecuteScalar();

            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
                cnn.Close();
            }

            if (_check == 1)
                return true;

            return false;
        }

        public void deleteStudent(string mssv, string nClass, string nCourses)
        {
            cnn.Open();

            OleDbCommand check = new OleDbCommand();
            check.Connection = cnn;
            check.CommandText = $"SELECT Count(MSSV) FROM Student WHERE MSSV = ?";
            check.Parameters.AddWithValue("@MSSV", mssv);
            if ((int)check.ExecuteScalar() == 0)
            {
                cnn.Close();
                return;
            }

            OleDbCommand deleteCmd = new OleDbCommand();
            deleteCmd.Connection = cnn;
            deleteCmd.CommandText = $"DELETE FROM StudentOfCourse WHERE MALOP = ? AND MAMON = ? AND (IDSIGN = ? OR IDDELETE = ?)";
            deleteCmd.Parameters.AddWithValue("@MALOP", nClass);
            deleteCmd.Parameters.AddWithValue("@MAMON", nCourses);
            deleteCmd.Parameters.AddWithValue("@IDSIGN", mssv);
            deleteCmd.Parameters.AddWithValue("@IDDELETE", mssv);
            deleteCmd.ExecuteNonQuery();


            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            cmd.CommandText = $"INSERT INTO StudentOfCourse(MALOP, MAMON, IDDELETE) VALUES(?, ?, ?)";

            try
            {
                cmd.Parameters.AddWithValue("@MALOP", nClass);
                cmd.Parameters.AddWithValue("@MAMON", nCourses);
                cmd.Parameters.AddWithValue("@IDDELETE", mssv);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
            }
            cnn.Close();
        }

        public void signCourses(string mssv, string nClass, string nCourses)
        {
            cnn.Open();

            OleDbCommand check = new OleDbCommand();
            check.Connection = cnn;
            check.CommandText = $"SELECT Count(MSSV) FROM Student WHERE MSSV = ?";
            check.Parameters.AddWithValue("@MSSV", mssv);
            if ((int)check.ExecuteScalar() == 0)
            {
                cnn.Close();
                return;
            }       

            OleDbCommand deleteCmd = new OleDbCommand();
            deleteCmd.Connection = cnn;
            deleteCmd.CommandText = $"DELETE FROM StudentOfCourse WHERE MALOP = ? AND MAMON = ? AND (IDSIGN = ? OR IDDELETE = ?)";
            deleteCmd.Parameters.AddWithValue("@MALOP", nClass);
            deleteCmd.Parameters.AddWithValue("@MAMON", nCourses);
            deleteCmd.Parameters.AddWithValue("@IDSIGN", mssv);
            deleteCmd.Parameters.AddWithValue("@IDDELETE", mssv);
            deleteCmd.ExecuteNonQuery();


            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            cmd.CommandText = $"INSERT INTO StudentOfCourse(MALOP, MAMON, IDSIGN) VALUES(?, ?, ?)";

            try
            {
                cmd.Parameters.AddWithValue("@MALOP", nClass);
                cmd.Parameters.AddWithValue("@MAMON", nCourses);
                cmd.Parameters.AddWithValue("@IDSIGN", mssv);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                cmd.Parameters.Clear();
            }
            cnn.Close();
        }

        public bool modifyPassword(string account, string oldPassword, string newPassword)
        {
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            cmd.CommandText = $"UPDATE Account SET Password=? WHERE Username=?";
            cmd.Parameters.AddWithValue("@Password", newPassword);
            cmd.Parameters.AddWithValue("@Username", account);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
