using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lightP03
{
    public partial class Form1 : Form
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\82104\source\repos\lightP03\lightP03\Database1.mdf;Integrated Security=True;Connect Timeout=30";

        int[] idN = new int[5] { 1001, 1002, 1003, 1004, 1005 };
        //int comboN = 2000;//콤보박스 아이디
        string[] orienN = new string[5];//기기 작동방향

        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); //종료
        }

        private void button1_Click(object sender, EventArgs e)//설정 화면으로 넘어가기
        {
            //작동 화면으로 이동
            this.Visible = false;
            Form2 showForm2 = new Form2();
            showForm2.Show();

            //발전소 이름 가져오기
            string comboS = comboBox1.Text.ToString();
            string plantName = comboS.Substring(0, 6);

            //Form2에 값 넘겨주기
            //Form2.plantN = plantName;
            /*Form2.idN[0] = label3.Text.ToString();
            Form2.idN[1] = label8.Text.ToString();
            Form2.idN[2] = label12.Text.ToString();
            Form2.idN[3] = label16.Text.ToString();
            Form2.idN[4] = label20.Text.ToString();*/
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit(); //종료
        }

        private void button8_Click(object sender, EventArgs e)//설정 완료 버튼 눌렀을 때
        {
            //발전소 이름&ID 가져오기
            string comboS = comboBox1.Text.ToString();
            string plantId = comboS.Substring(9);//PlantA : P001
            string plantName = comboS.Substring(0, 6);

            //Form2에 값 넘겨주기
            //Form2.plantN = plantName;//textBox16.Text.ToString();
            /*Form2.idN[0] = label3.Text.ToString();
            Form2.idN[1] = label8.Text.ToString();
            Form2.idN[2] = label12.Text.ToString();
            Form2.idN[3] = label16.Text.ToString();
            Form2.idN[4] = label20.Text.ToString();*/

            MessageBox.Show("설정 완료되었습니다.");

            //결과 창에 보여주기
            resultL.Text = "설정 완료되었습니다.";

            //방향 값 저장하기
            int[] dirN = new int[5];
            if (radioButton1.Checked)
            {
                dirN[0] = 1;
            }
            else 
            {
                dirN[0] = 2;
            }
            if (radioButton4.Checked)
            {
                dirN[1] = 1;
            }
            else 
            {
                dirN[1] = 2;
            }
            if (radioButton6.Checked)
            {
                dirN[2] = 1;
            }
            else 
            {
                dirN[2] = 2;
            }
            if (radioButton8.Checked)
            {
                dirN[3] = 1;
            }
            else
            {
                dirN[3] = 2;
            }
            if (radioButton10.Checked)
            {
                dirN[4] = 1;
            }
            else
            {
                dirN[4] = 2;
            }
            //방향
            string[] dirNS = new string[5] {dirS(dirN[0]), dirS(dirN[1]), dirS(dirN[2]), dirS(dirN[3]), dirS(dirN[4])};

            //데이터베이스 값 추가
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            //발전소명 관리(PlantList)
            string tableName1 = "PlantList";
            SqlCommand[] cmdP = new SqlCommand[5];

            for(int i = 0; i < 5; i++)
            {
                cmdP[i] = new SqlCommand("insert into " + tableName1 + " values (@PlantId, @PlantName)", con);
            }

            cmdP[0].Parameters.AddWithValue("@PlantId", plantId);
            cmdP[0].Parameters.AddWithValue("@PlantName", plantName);

            cmdP[1].Parameters.AddWithValue("@PlantId", plantId);
            cmdP[1].Parameters.AddWithValue("@PlantName", plantName);

            cmdP[2].Parameters.AddWithValue("@PlantId", plantId);
            cmdP[2].Parameters.AddWithValue("@PlantName", plantName);

            cmdP[3].Parameters.AddWithValue("@PlantId", plantId);
            cmdP[3].Parameters.AddWithValue("@PlantName", plantName);

            cmdP[4].Parameters.AddWithValue("@PlantId", plantId);
            cmdP[4].Parameters.AddWithValue("@PlantName", plantName);

            for(int i = 0; i < 5; i++)
            {
                cmdP[i].ExecuteNonQuery();
            }

            //데이터베이스 값 추가(WorkLog)
            string tableName2 = "WorkLog";

            SqlCommand[] cmdW = new SqlCommand[5];

            for(int i = 0; i < 5; i++)
            {
                cmdW[i]= new SqlCommand("insert into " + tableName2 + " values (@Rid, @TimeStamp, @State, @Counter, @Etc)", con);
            }
            cmdW[0].Parameters.AddWithValue("@Rid", label3.Text.ToString());
            cmdW[0].Parameters.AddWithValue("@TimeStamp", "10");
            cmdW[0].Parameters.AddWithValue("@State", "error");
            cmdW[0].Parameters.AddWithValue("@Counter", 0);
            cmdW[0].Parameters.AddWithValue("@Etc", "");

            cmdW[1].Parameters.AddWithValue("@Rid", label8.Text.ToString());
            cmdW[1].Parameters.AddWithValue("@TimeStamp", "20");
            cmdW[1].Parameters.AddWithValue("@State", "ready");
            cmdW[1].Parameters.AddWithValue("@Counter", 0);
            cmdW[1].Parameters.AddWithValue("@Etc", "");

            cmdW[2].Parameters.AddWithValue("@Rid", label12.Text.ToString());
            cmdW[2].Parameters.AddWithValue("@TimeStamp", "30");
            cmdW[2].Parameters.AddWithValue("@State", "no signal");
            cmdW[2].Parameters.AddWithValue("@Counter", 0);
            cmdW[2].Parameters.AddWithValue("@Etc", "");

            cmdW[3].Parameters.AddWithValue("@Rid", label16.Text.ToString());
            cmdW[3].Parameters.AddWithValue("@TimeStamp", "40");
            cmdW[3].Parameters.AddWithValue("@State", "end");
            cmdW[3].Parameters.AddWithValue("@Counter", 0);
            cmdW[3].Parameters.AddWithValue("@Etc", "");

            cmdW[4].Parameters.AddWithValue("@Rid", label20.Text.ToString());
            cmdW[4].Parameters.AddWithValue("@TimeStamp", "50");
            cmdW[4].Parameters.AddWithValue("@State", "run");
            cmdW[4].Parameters.AddWithValue("@Counter", 0);
            cmdW[4].Parameters.AddWithValue("@Etc", "");

            for (int i = 0; i < 5; i++)
            {
                cmdW[i].ExecuteNonQuery();
            }

            //데이터베이스 값 수정(settings)
            string[] Query = new string[5];
            Query[0] = "update Settings set LSize='" + textBox2.Text + "',RSize='" + textBox1.Text + "'," +
                    "Orientation='" + dirNS[0] + "' where RId ='" + label3.Text + "'";
            Query[1] = "update Settings set LSize='" + textBox5.Text + "',RSize='" + textBox4.Text + "'," +
                    "Orientation='" + dirNS[1] + "' where RId ='" + label8.Text + "'";
            Query[2] = "update Settings set LSize='" + textBox8.Text + "',RSize='" + textBox7.Text + "'," +
                    "Orientation='" + dirNS[2] + "' where RId ='" + label12.Text + "'";
            Query[3] = "update Settings set LSize='" + textBox11.Text + "',RSize='" + textBox10.Text + "'," +
                    "Orientation='" + dirNS[3] + "' where RId ='" + label16.Text + "'";
            Query[4] = "update Settings set LSize='" + textBox14.Text + "',RSize='" + textBox13.Text + "'," +
                    "Orientation='" + dirNS[4] + "' where RId ='" + label20.Text + "'";

            SqlCommand[] cmdD = new SqlCommand[5];
            cmdD[0] = new SqlCommand(Query[0], con);
            cmdD[1] = new SqlCommand(Query[1], con);
            cmdD[2] = new SqlCommand(Query[2], con);
            cmdD[3] = new SqlCommand(Query[3], con);
            cmdD[4] = new SqlCommand(Query[4], con);

            SqlDataReader[] sdR = new SqlDataReader[5];
            sdR[0] = cmdD[0].ExecuteReader();
            sdR[0].Close();
            sdR[1] = cmdD[1].ExecuteReader();
            sdR[1].Close();
            sdR[2] = cmdD[2].ExecuteReader();
            sdR[2].Close();
            sdR[3] = cmdD[3].ExecuteReader();
            sdR[3].Close();
            sdR[4] = cmdD[4].ExecuteReader();
            sdR[4].Close();

            //발전소명 관리 삽입 끝

            con.Close();
        }

        //라디오버튼 클릭했을 때
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1001 기기의 수평 버튼을 클릭했습니다.";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1001 기기의 수직 버튼을 클릭했습니다.";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1002 기기의 수평 버튼을 클릭했습니다.";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1002 기기의 수직 버튼을 클릭했습니다.";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1003 기기의 수평 버튼을 클릭했습니다.";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1003 기기의 수직 버튼을 클릭했습니다.";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1004 기기의 수평 버튼을 클릭했습니다.";
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1004 기기의 수직 버튼을 클릭했습니다.";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1005 기기의 수평 버튼을 클릭했습니다.";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            resultL.Text = "1005 기기의 수직 버튼을 클릭했습니다.";
        }
        private void displayDB(string idN, int num)//데이터베이스 결과 보여주는 함수
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"select Lsize, Rsize, Orientation
            from Settings 
            where Rid =" + @idN;/*수정*/

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read() == true)
            {
                if (num == 1)
                {
                    textBox2.Text = sdr["Lsize"].ToString();
                    textBox1.Text = sdr["Rsize"].ToString();
                    orienN[0] = sdr["Orientation"].ToString();
                }
                else if (num == 2)
                {
                    textBox5.Text = sdr["Lsize"].ToString();
                    textBox4.Text = sdr["Rsize"].ToString();
                    orienN[1] = sdr["Orientation"].ToString();
                }
                else if (num == 3)
                {
                    textBox8.Text = sdr["Lsize"].ToString();
                    textBox7.Text = sdr["Rsize"].ToString();
                    orienN[2] = sdr["Orientation"].ToString();
                }
                else if (num == 4)
                {
                    textBox11.Text = sdr["Lsize"].ToString();
                    textBox10.Text = sdr["Rsize"].ToString();
                    orienN[3] = sdr["Orientation"].ToString();
                }
                else
                {
                    textBox14.Text = sdr["Lsize"].ToString();
                    textBox13.Text = sdr["Rsize"].ToString();
                    orienN[4] = sdr["Orientation"].ToString();
                }
                

            }
            con.Close();
            /*수정*/
            sdr.Close();
            sdr.Dispose();
        }
        public int displayO(string orienS)//방향 알려주는 함수
        {
            int oriN = 1;

            if (orienS.Equals("vertical"))
            {
                oriN = 2;
            }

            return oriN;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) // plantA 
            {
                resultL.Text = "PlantA를 선택했습니다.";
                label3.Text = idN[0].ToString();
                label8.Text = idN[1].ToString();
                label12.Text = idN[2].ToString();
                label16.Text = idN[3].ToString();
                label20.Text = idN[4].ToString();

                //저장된 데이터 불러오기
                displayDB(idN[0].ToString(),1);/*수정*/
                displayDB(idN[1].ToString(), 2);
                displayDB(idN[2].ToString(), 3);
                displayDB(idN[3].ToString(), 4);
                displayDB(idN[4].ToString(), 5);

                //라디오버튼 선택
                int[] plantA = new int[5] { displayO(orienN[0]), displayO(orienN[1]), displayO(orienN[2]), displayO(orienN[3]), displayO(orienN[4]) };
                if (plantA[0] == 1)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                if (plantA[1] == 1)
                {
                    radioButton4.Checked = true;
                }
                else
                {
                    radioButton3.Checked = true;
                }
                if (plantA[2] == 1)
                {
                    radioButton6.Checked = true;
                }
                else
                {
                    radioButton5.Checked = true;
                }
                if (plantA[3] == 1)
                {
                    radioButton8.Checked = true;
                }
                else
                {
                    radioButton7.Checked = true;
                }
                if (plantA[4] == 1)
                {
                    radioButton10.Checked = true;
                }
                else
                {
                    radioButton9.Checked = true;
                }
            }
            else if (comboBox1.SelectedIndex == 1) // plantB
            {
                resultL.Text = "PlantB를 선택했습니다.";
                label3.Text = (idN[0] + 1000).ToString();
                label8.Text = (idN[1] + 1000).ToString();
                label12.Text = (idN[2] + 1000).ToString();
                label16.Text = (idN[3] + 1000).ToString();
                label20.Text = (idN[4] + 1000).ToString();

                //저장된 데이터 불러오기
                displayDB((idN[0] + 1000).ToString(),1);/*수정*/
                displayDB((idN[1] + 1000).ToString(), 2);
                displayDB((idN[2] + 1000).ToString(), 3);
                displayDB((idN[3] + 1000).ToString(), 4);
                displayDB((idN[4] + 1000).ToString(), 5);

                //라디오버튼 선택
                int[] plantB = new int[5] { displayO(orienN[0]), displayO(orienN[1]), displayO(orienN[2]), displayO(orienN[3]), displayO(orienN[4]) };
                if (plantB[0] == 1)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                if (plantB[1] == 1)
                {
                    radioButton4.Checked = true;
                }
                else
                {
                    radioButton3.Checked = true;
                }
                if (plantB[2] == 1)
                {
                    radioButton6.Checked = true;
                }
                else
                {
                    radioButton5.Checked = true;
                }
                if (plantB[3] == 1)
                {
                    radioButton8.Checked = true;
                }
                else
                {
                    radioButton7.Checked = true;
                }
                if (plantB[4] == 1)
                {
                    radioButton10.Checked = true;
                }
                else
                {
                    radioButton9.Checked = true;
                }
            }
            else if (comboBox1.SelectedIndex == 2) // plantC
            {
                resultL.Text = "PlantC를 선택했습니다.";
                label3.Text = (idN[0] + 2000).ToString();
                label8.Text = (idN[1] + 2000).ToString();
                label12.Text = (idN[2] + 2000).ToString();
                label16.Text = (idN[3] + 2000).ToString();
                label20.Text = (idN[4] + 2000).ToString();

                //저장된 데이터 불러오기
                displayDB((idN[0] + 2000).ToString(), 1);/*수정*/
                displayDB((idN[1] + 2000).ToString(), 2);
                displayDB((idN[2] + 2000).ToString(), 3);
                displayDB((idN[3] + 2000).ToString(), 4);
                displayDB((idN[4] + 2000).ToString(), 5);

                //라디오버튼 선택
                int[] plantC = new int[5] { displayO(orienN[0]), displayO(orienN[1]), displayO(orienN[2]), displayO(orienN[3]), displayO(orienN[4]) };
                if (plantC[0] == 1)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                if (plantC[1] == 1)
                {
                    radioButton4.Checked = true;
                }
                else
                {
                    radioButton3.Checked = true;
                }
                if (plantC[2] == 1)
                {
                    radioButton6.Checked = true;
                }
                else
                {
                    radioButton5.Checked = true;
                }
                if (plantC[3] == 1)
                {
                    radioButton8.Checked = true;
                }
                else
                {
                    radioButton7.Checked = true;
                }
                if (plantC[4] == 1)
                {
                    radioButton10.Checked = true;
                }
                else
                {
                    radioButton9.Checked = true;
                }
            }
            else if (comboBox1.SelectedIndex == 3)
            {

            }else if (comboBox1.SelectedIndex == 4)
            {

            }//5개까지만 추가되도록
        }
        //방향 알려주는 함수
        public string dirS(int num)
        {
            string dirS = "";
            if (num == 1)
            {
                dirS = "horizontal";
            }
            else if(num==2)
            {
                dirS = "vertical";
            }
            return dirS;
        }
        //값 수정 안되도록 바꾸기
        //데이터베이스에 값 따로 저장해주기
        private void button6_Click(object sender, EventArgs e)//기기 1 저장
        {
            /*textBox3.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox1.ReadOnly = true;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;*/

            //발전소 이름&ID 가져오기
            string comboS = comboBox1.Text.ToString();
            string plantId = comboS.Substring(9);//PlantA : P001
            string plantName = comboS.Substring(0, 6);

            //데이터베이스에 값 저장해주기

            //결과 창에 보여주기
            resultL.Text = label3.Text.ToString() + "설정 완료되었습니다.";

            //방향 값 저장하기
            int dirN;
            if (radioButton1.Checked)
            {
                dirN = 1;
            }
            else
            {
                dirN = 2;
            }
            //방향
            string dirNS = dirS(dirN);

            //데이터베이스 값 추가
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            //데이터베이스 값 추가(PlantList)
            string tableName1 = "PlantList";
            SqlCommand cmdP = new SqlCommand("insert into " + tableName1 + " values (@PlantId, @PlantName)", con);

            cmdP.Parameters.AddWithValue("@PlantId", plantId);
            cmdP.Parameters.AddWithValue("@PlantName", plantName);

            cmdP.ExecuteNonQuery();

            //데이터베이스 값 추가(WorkLog)
            string tableName2 = "WorkLog";

            SqlCommand cmdW = new SqlCommand("insert into " + tableName2 + " values (@Rid, @TimeStamp, @State, @Counter, @Etc)", con);

            cmdW.Parameters.AddWithValue("@Rid", label3.Text.ToString());
            cmdW.Parameters.AddWithValue("@TimeStamp", "10");
            cmdW.Parameters.AddWithValue("@State", "error");
            cmdW.Parameters.AddWithValue("@Counter", 0);
            cmdW.Parameters.AddWithValue("@Etc", "");

            cmdW.ExecuteNonQuery();

            //데이터베이스 값 수정(settings)
            string Query = "update Settings set LSize='" + textBox2.Text + "',RSize='" + textBox1.Text + "'," +
                    "Orientation='" + dirNS + "' where RId ='" + label3.Text + "'";

            SqlCommand cmdD = new SqlCommand(Query, con);

            SqlDataReader sdR = cmdD.ExecuteReader();
            sdR.Close();

            //발전소명 관리 삽입 끝
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)//기기 2 저장
        {
            /*textBox6.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox4.ReadOnly = true;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;*/

            //발전소 이름&ID 가져오기
            string comboS = comboBox1.Text.ToString();
            string plantId = comboS.Substring(9);//PlantA : P001
            string plantName = comboS.Substring(0, 6);

            //데이터베이스에 값 저장해주기

            //결과 창에 보여주기
            resultL.Text = label8.Text.ToString() + "설정 완료되었습니다.";

            //방향 값 저장하기
            int dirN;
            if (radioButton4.Checked)
            {
                dirN = 1;
            }
            else
            {
                dirN = 2;
            }
            //방향
            string dirNS = dirS(dirN);

            //데이터베이스 값 추가
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            //데이터베이스 값 추가(PlantList)
            string tableName1 = "PlantList";
            SqlCommand cmdP = new SqlCommand("insert into " + tableName1 + " values (@PlantId, @PlantName)", con);

            cmdP.Parameters.AddWithValue("@PlantId", plantId);
            cmdP.Parameters.AddWithValue("@PlantName", plantName);

            cmdP.ExecuteNonQuery();

            //데이터베이스 값 추가(WorkLog)
            string tableName2 = "WorkLog";

            SqlCommand cmdW = new SqlCommand("insert into " + tableName2 + " values (@Rid, @TimeStamp, @State, @Counter, @Etc)", con);

            cmdW.Parameters.AddWithValue("@Rid", label8.Text.ToString());
            cmdW.Parameters.AddWithValue("@TimeStamp", "10");
            cmdW.Parameters.AddWithValue("@State", "error");
            cmdW.Parameters.AddWithValue("@Counter", 0);
            cmdW.Parameters.AddWithValue("@Etc", "");

            cmdW.ExecuteNonQuery();

            //데이터베이스 값 수정(settings)
            string Query = "update Settings set LSize='" + textBox5.Text + "',RSize='" + textBox4.Text + "'," +
                    "Orientation='" + dirNS + "' where RId ='" + label8.Text + "'";

            SqlCommand cmdD = new SqlCommand(Query, con);

            SqlDataReader sdR = cmdD.ExecuteReader();
            sdR.Close();

            //발전소명 관리 삽입 끝
            con.Close();
        }

        private void button7_Click(object sender, EventArgs e)//기기 3 저장
        {
            /*textBox9.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox7.ReadOnly = true;
            radioButton5.Enabled = false;
            radioButton6.Enabled = false;*/

            //발전소 이름&ID 가져오기
            string comboS = comboBox1.Text.ToString();
            string plantId = comboS.Substring(9);//PlantA : P001
            string plantName = comboS.Substring(0, 6);

            //데이터베이스에 값 저장해주기

            //결과 창에 보여주기
            resultL.Text = label12.Text.ToString() + "설정 완료되었습니다.";

            //방향 값 저장하기
            int dirN;
            if (radioButton6.Checked)
            {
                dirN = 1;
            }
            else
            {
                dirN = 2;
            }
            //방향
            string dirNS = dirS(dirN);

            //데이터베이스 값 추가
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            //데이터베이스 값 추가(PlantList)
            string tableName1 = "PlantList";
            SqlCommand cmdP = new SqlCommand("insert into " + tableName1 + " values (@PlantId, @PlantName)", con);

            cmdP.Parameters.AddWithValue("@PlantId", plantId);
            cmdP.Parameters.AddWithValue("@PlantName", plantName);

            cmdP.ExecuteNonQuery();

            //데이터베이스 값 추가(WorkLog)
            string tableName2 = "WorkLog";

            SqlCommand cmdW = new SqlCommand("insert into " + tableName2 + " values (@Rid, @TimeStamp, @State, @Counter, @Etc)", con);

            cmdW.Parameters.AddWithValue("@Rid", label12.Text.ToString());
            cmdW.Parameters.AddWithValue("@TimeStamp", "10");
            cmdW.Parameters.AddWithValue("@State", "error");
            cmdW.Parameters.AddWithValue("@Counter", 0);
            cmdW.Parameters.AddWithValue("@Etc", "");

            cmdW.ExecuteNonQuery();

            //데이터베이스 값 수정(settings)
            string Query = "update Settings set LSize='" + textBox8.Text + "',RSize='" + textBox7.Text + "'," +
                    "Orientation='" + dirNS + "' where RId ='" + label12.Text + "'";

            SqlCommand cmdD = new SqlCommand(Query, con);

            SqlDataReader sdR = cmdD.ExecuteReader();
            sdR.Close();

            //발전소명 관리 삽입 끝
            con.Close();
        }

        private void button10_Click(object sender, EventArgs e)//기기 4 저장
        {
            /*textBox12.ReadOnly = true;
            textBox11.ReadOnly = true;
            textBox10.ReadOnly = true;
            radioButton7.Enabled = false;
            radioButton8.Enabled = false;*/

            //발전소 이름&ID 가져오기
            string comboS = comboBox1.Text.ToString();
            string plantId = comboS.Substring(9);//PlantA : P001
            string plantName = comboS.Substring(0, 6);

            //데이터베이스에 값 저장해주기

            //결과 창에 보여주기
            resultL.Text = label16.Text.ToString() + "설정 완료되었습니다.";

            //방향 값 저장하기
            int dirN;
            if (radioButton8.Checked)
            {
                dirN = 1;
            }
            else
            {
                dirN = 2;
            }
            //방향
            string dirNS = dirS(dirN);

            //데이터베이스 값 추가
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            //데이터베이스 값 추가(PlantList)
            string tableName1 = "PlantList";
            SqlCommand cmdP = new SqlCommand("insert into " + tableName1 + " values (@PlantId, @PlantName)", con);

            cmdP.Parameters.AddWithValue("@PlantId", plantId);
            cmdP.Parameters.AddWithValue("@PlantName", plantName);

            cmdP.ExecuteNonQuery();

            //데이터베이스 값 추가(WorkLog)
            string tableName2 = "WorkLog";

            SqlCommand cmdW = new SqlCommand("insert into " + tableName2 + " values (@Rid, @TimeStamp, @State, @Counter, @Etc)", con);

            cmdW.Parameters.AddWithValue("@Rid", label16.Text.ToString());
            cmdW.Parameters.AddWithValue("@TimeStamp", "10");
            cmdW.Parameters.AddWithValue("@State", "error");
            cmdW.Parameters.AddWithValue("@Counter", 0);
            cmdW.Parameters.AddWithValue("@Etc", "");

            cmdW.ExecuteNonQuery();

            //데이터베이스 값 수정(settings)
            string Query = "update Settings set LSize='" + textBox11.Text + "',RSize='" + textBox10.Text + "'," +
                    "Orientation='" + dirNS + "' where RId ='" + label16.Text + "'";

            SqlCommand cmdD = new SqlCommand(Query, con);

            SqlDataReader sdR = cmdD.ExecuteReader();
            sdR.Close();

            //발전소명 관리 삽입 끝
            con.Close();
        }

        private void button11_Click(object sender, EventArgs e)//기기 5 저장
        {
            /*textBox15.ReadOnly = true;
            textBox14.ReadOnly = true;
            textBox13.ReadOnly = true;
            radioButton9.Enabled = false;
            radioButton10.Enabled = false;*/

            //발전소 이름&ID 가져오기
            string comboS = comboBox1.Text.ToString();
            string plantId = comboS.Substring(9);//PlantA : P001
            string plantName = comboS.Substring(0, 6);

            //데이터베이스에 값 저장해주기

            //결과 창에 보여주기
            resultL.Text = label20.Text.ToString()+"설정 완료되었습니다.";

            //방향 값 저장하기
            int dirN;
            if (radioButton10.Checked)
            {
                dirN = 1;
            }
            else
            {
                dirN = 2;
            }
            //방향
            string dirNS = dirS(dirN);

            //데이터베이스 값 추가
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            //데이터베이스 값 추가(PlantList)
            string tableName1 = "PlantList";
            SqlCommand cmdP = new SqlCommand("insert into " + tableName1 + " values (@PlantId, @PlantName)", con);

            cmdP.Parameters.AddWithValue("@PlantId", plantId);
            cmdP.Parameters.AddWithValue("@PlantName", plantName);

            cmdP.ExecuteNonQuery();

            //데이터베이스 값 추가(WorkLog)
            string tableName2 = "WorkLog";

            SqlCommand cmdW = new SqlCommand("insert into " + tableName2 + " values (@Rid, @TimeStamp, @State, @Counter, @Etc)", con);

            cmdW.Parameters.AddWithValue("@Rid", label20.Text.ToString());
            cmdW.Parameters.AddWithValue("@TimeStamp", "10");
            cmdW.Parameters.AddWithValue("@State", "error");
            cmdW.Parameters.AddWithValue("@Counter", 0);
            cmdW.Parameters.AddWithValue("@Etc", "");

            cmdW.ExecuteNonQuery();

            //데이터베이스 값 수정(settings)
            string Query = "update Settings set LSize='" + textBox14.Text + "',RSize='" + textBox13.Text + "'," +
                    "Orientation='" + dirNS + "' where RId ='" + label20.Text + "'";

            SqlCommand cmdD = new SqlCommand(Query, con);

            SqlDataReader sdR = cmdD.ExecuteReader();
            sdR.Close();

            //발전소명 관리 삽입 끝
            con.Close();
        }
    }
}
