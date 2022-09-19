using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace lightP03
{
    public partial class Form2 : Form
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\82104\source\repos\lightP03\lightP03\Database1.mdf;Integrated Security=True;Connect Timeout=30";

        //Form1 리스트뷰 값 받아오기
        //public static string plantN;

        //public static string[] idN = new string[5];
        int[] idN = new int[5] { 1001, 1002, 1003, 1004, 1005 };
        string[] orienN = new string[5];//기기 작동방향

        //기기 상태 출력
        public string state1L, state2L, state3L, state4L, state5L;

        //프로그레스바 숫자
        int pN1 = 0, pN2 = 0, pN3 = 0, pN4 = 0, pN5 = 0;

        public Form2()
        {
            InitializeComponent();

            //받아온 값으로 상태값 변경
            //tbl_PlantN.Text = plantN;
            //STATE1
            tbl_id1.Text = idN[0].ToString();
            //STATE2
            tbl_id2.Text = idN[1].ToString();
            //STATE3
            tbl_id3.Text = idN[2].ToString();
            //STATE4
            tbl_id4.Text = idN[3].ToString();
            //STATE5
            tbl_id5.Text = idN[4].ToString();

            //설정 패널 안보이게
            panel_set.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //설정 화면으로 이동
            this.Visible = false;
            Form1 showForm1 = new Form1();
            showForm1.Show();
        }
        //작동 버튼 클릭했을 때
        private void btnRun_Click(object sender, EventArgs e)
        {
            resultL.Text=tbl_id1.Text+" 기기의 작동 버튼을 클릭했습니다.";
            this.pgb_1.Value = this.pgb_1.Value + 50;
            pN1 += 50;
            tbl_per1.Text = pN1.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            resultL.Text=tbl_id2.Text+" 기기의 작동 버튼을 클릭했습니다.";
            this.pgb_2.Value = this.pgb_2.Value + 50;
            pN2 += 50;
            tbl_per2.Text = pN2.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            resultL.Text=tbl_id3.Text+" 기기의 작동 버튼을 클릭했습니다.";
            this.pgb_3.Value = this.pgb_3.Value + 50;
            pN3 += 50;
            tbl_per3.Text = pN3.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            resultL.Text = tbl_id4.Text + " 기기의 작동 버튼을 클릭했습니다.";
            this.pgb_4.Value = this.pgb_4.Value + 50;
            pN4 += 50;
            tbl_per4.Text = pN4.ToString();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            resultL.Text=tbl_id5.Text + " 기기의 작동 버튼을 클릭했습니다.";
            this.pgb_5.Value = this.pgb_5.Value + 50;
            pN5 += 50;
            tbl_per5.Text = pN5.ToString();
        }
        //멈춤 버튼 클릭했을 때
        private void btnStop_Click(object sender, EventArgs e)
        {
            resultL.Text= "1001 기기의 멈춤 버튼을 클릭했습니다.";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            resultL.Text= "1002 기기의 멈춤 버튼을 클릭했습니다.";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            resultL.Text= "1003 기기의 멈춤 버튼을 클릭했습니다.";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //설정 패널 안보이게
            panel_set.Visible = false;

            //설정 사항 저장하기
            //발전소 이름&ID 가져오기
            string comboS = combo_plant.Text.ToString();
            string plantId = comboS.Substring(9);//PlantA : P001
            string plantName = comboS.Substring(0, 6);

            //데이터베이스에 값 저장(시작)
            //방향 값 저장하기
            int dirN;
            if (rbtn_horizontal.Checked)
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

            cmdW.Parameters.AddWithValue("@Rid", tbl_setID.Text.ToString());
            cmdW.Parameters.AddWithValue("@TimeStamp", "10");
            cmdW.Parameters.AddWithValue("@State", "error");
            cmdW.Parameters.AddWithValue("@Counter", 0);
            cmdW.Parameters.AddWithValue("@Etc", "");

            cmdW.ExecuteNonQuery();

            //데이터베이스 값 수정(settings)
            string Query = "update Settings set LSize='" + tb_width.Text + "',RSize='" + tb_length.Text + "'," +
                    "Orientation='" + dirNS + "' where RId ='" + tbl_setID.Text + "'";

            SqlCommand cmdD = new SqlCommand(Query, con);

            SqlDataReader sdR = cmdD.ExecuteReader();
            sdR.Close();

            //발전소명 관리 삽입 끝
            con.Close();
            //데이터베이스에 값 저장(끝)

            //텍스트 바로 바뀔 수 있도록
            int rid = Convert.ToInt32(tbl_setID.Text.ToString());
            if (rid % 1000 == 1)
            {
                tbl_width1.Text = tb_width.Text;
                tbl_length1.Text = tb_length.Text;
                if (rbtn_horizontal.Checked == true)
                {
                    tbl_ori1.Text = "수평";
                }
                else
                {
                    tbl_ori1.Text = "수직";
                }
            }
            else if (rid % 1000 == 2)
            {
                tbl_width2.Text = tb_width.Text;
                tbl_length2.Text = tb_length.Text;
                if (rbtn_horizontal.Checked == true)
                {
                    tbl_ori2.Text = "수평";
                }
                else
                {
                    tbl_ori2.Text = "수직";
                }
            }
            else if (rid % 1000 == 3)
            {
                tbl_width3.Text = tb_width.Text;
                tbl_length3.Text = tb_length.Text;
                if (rbtn_horizontal.Checked == true)
                {
                    tbl_ori3.Text = "수평";
                }
                else
                {
                    tbl_ori3.Text = "수직";
                }
            }
            else if (rid % 1000 == 4)
            {
                tbl_width4.Text = tb_width.Text;
                tbl_length4.Text = tb_length.Text;
                if (rbtn_horizontal.Checked == true)
                {
                    tbl_ori4.Text = "수평";
                }
                else
                {
                    tbl_ori4.Text = "수직";
                }
            }
            else
            {
                tbl_width5.Text = tb_width.Text;
                tbl_length5.Text = tb_length.Text;
                if (rbtn_horizontal.Checked == true)
                {
                    tbl_ori5.Text = "수평";
                }
                else
                {
                    tbl_ori5.Text = "수직";
                }
            }
            tb_width.Text = "";
            tb_length.Text = "";
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //설정 패널 안보이게
            panel_set.Visible = false;
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
        private void combo_plant_SelectedIndexChanged(object sender, EventArgs e)
        {
            //발전소 이름 가져오기
            string comboS = combo_plant.Text.ToString();
            string plantName = comboS.Substring(0, 6);

            if (combo_plant.SelectedIndex == 0) // plantA 
            {
                resultL.Text = "PlantA를 선택했습니다.";
                tbl_id1.Text = idN[0].ToString();
                tbl_id2.Text = idN[1].ToString();
                tbl_id3.Text = idN[2].ToString();
                tbl_id4.Text = idN[3].ToString();
                tbl_id5.Text = idN[4].ToString();

                //저장된 데이터 불러오기
                displayDB(idN[0].ToString(), 1);/*수정*/
                displayDB(idN[1].ToString(), 2);
                displayDB(idN[2].ToString(), 3);
                displayDB(idN[3].ToString(), 4);
                displayDB(idN[4].ToString(), 5);

                //방향 값 받아오기
                int[] plantA = new int[5] { displayO(orienN[0]), displayO(orienN[1]), displayO(orienN[2]), displayO(orienN[3]), displayO(orienN[4]) };
                if (plantA[0] == 1)
                {
                    tbl_ori1.Text = "수평";
                }
                else
                {
                    tbl_ori1.Text = "수직";
                }
                if (plantA[1] == 1)
                {
                    tbl_ori2.Text = "수평";
                }
                else
                {
                    tbl_ori2.Text = "수직";
                }
                if (plantA[2] == 1)
                {
                    tbl_ori3.Text = "수평";
                }
                else
                {
                    tbl_ori3.Text = "수직";
                }
                if (plantA[3] == 1)
                {
                    tbl_ori4.Text = "수평";
                }
                else
                {
                    tbl_ori4.Text = "수직";
                }
                if (plantA[4] == 1)
                {
                    tbl_ori5.Text = "수평";
                }
                else
                {
                    tbl_ori5.Text = "수직";
                }
            }
            else if (combo_plant.SelectedIndex == 1) // plantB
            {
                resultL.Text = "PlantB를 선택했습니다.";
                tbl_id1.Text = (idN[0] + 1000).ToString();
                tbl_id2.Text = (idN[1] + 1000).ToString();
                tbl_id3.Text = (idN[2] + 1000).ToString();
                tbl_id4.Text = (idN[3] + 1000).ToString();
                tbl_id5.Text = (idN[4] + 1000).ToString();

                //저장된 데이터 불러오기
                displayDB((idN[0] + 1000).ToString(), 1);/*수정*/
                displayDB((idN[1] + 1000).ToString(), 2);
                displayDB((idN[2] + 1000).ToString(), 3);
                displayDB((idN[3] + 1000).ToString(), 4);
                displayDB((idN[4] + 1000).ToString(), 5);

                //방향 값 받아오기
                int[] plantB = new int[5] { displayO(orienN[0]), displayO(orienN[1]), displayO(orienN[2]), displayO(orienN[3]), displayO(orienN[4]) };
                if (plantB[0] == 1)
                {
                    tbl_ori1.Text = "수평";
                }
                else
                {
                    tbl_ori1.Text = "수직";
                }
                if (plantB[1] == 1)
                {
                    tbl_ori2.Text = "수평";
                }
                else
                {
                    tbl_ori2.Text = "수직";
                }
                if (plantB[2] == 1)
                {
                    tbl_ori3.Text = "수평";
                }
                else
                {
                    tbl_ori3.Text = "수직";
                }
                if (plantB[3] == 1)
                {
                    tbl_ori4.Text = "수평";
                }
                else
                {
                    tbl_ori4.Text = "수직";
                }
                if (plantB[4] == 1)
                {
                    tbl_ori5.Text = "수평";
                }
                else
                {
                    tbl_ori5.Text = "수직";
                }
            }
            else if (combo_plant.SelectedIndex == 2) // plantC
            {
                resultL.Text = "PlantC를 선택했습니다.";
                tbl_id1.Text = (idN[0] + 2000).ToString();
                tbl_id2.Text = (idN[1] + 2000).ToString();
                tbl_id3.Text = (idN[2] + 2000).ToString();
                tbl_id4.Text = (idN[3] + 2000).ToString();
                tbl_id5.Text = (idN[4] + 2000).ToString();

                //저장된 데이터 불러오기
                displayDB((idN[0] + 2000).ToString(), 1);/*수정*/
                displayDB((idN[1] + 2000).ToString(), 2);
                displayDB((idN[2] + 2000).ToString(), 3);
                displayDB((idN[3] + 2000).ToString(), 4);
                displayDB((idN[4] + 2000).ToString(), 5);

                //방향 값 받아오기
                int[] plantC = new int[5] { displayO(orienN[0]), displayO(orienN[1]), displayO(orienN[2]), displayO(orienN[3]), displayO(orienN[4]) };
                if (plantC[0] == 1)
                {
                    tbl_ori1.Text = "수평";
                }
                else
                {
                    tbl_ori1.Text = "수직";
                }
                if (plantC[1] == 1)
                {
                    tbl_ori2.Text = "수평";
                }
                else
                {
                    tbl_ori2.Text = "수직";
                }
                if (plantC[2] == 1)
                {
                    tbl_ori3.Text = "수평";
                }
                else
                {
                    tbl_ori3.Text = "수직";
                }
                if (plantC[3] == 1)
                {
                    tbl_ori4.Text = "수평";
                }
                else
                {
                    tbl_ori4.Text = "수직";
                }
                if (plantC[4] == 1)
                {
                    tbl_ori5.Text = "수평";
                }
                else
                {
                    tbl_ori5.Text = "수직";
                }
            }
        }
        //방향 알려주는 함수
        public string dirS(int num)
        {
            string dirS = "";
            if (num == 1)
            {
                dirS = "horizontal";
            }
            else if (num == 2)
            {
                dirS = "vertical";
            }
            return dirS;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            resultL.Text= "1004 기기의 멈춤 버튼을 클릭했습니다.";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            resultL.Text= "1005 기기의 멈춤 버튼을 클릭했습니다.";
        }
        //원래 자리로 돌아가는 버튼 클릭했을 때
        private void btnHome_Click(object sender, EventArgs e)
        {
            resultL.Text="1001 기기의 제자리 버튼을 클릭했습니다.";
            this.pgb_1.Value = 0;
            pN1 = 0;
            tbl_per1.Text = pN1.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            resultL.Text="1002 기기의 제자리 버튼을 클릭했습니다.";
            this.pgb_2.Value = 0;
            pN2 = 0;
            tbl_per2.Text = pN2.ToString();
        }

        private void label5_Click(object sender, EventArgs e)//확인용//수정
        {
            resultL.Text= "id="+idN[0];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            resultL.Text="1003 기기의 제자리 버튼을 클릭했습니다.";
            this.pgb_3.Value = 0;
            pN3 = 0;
            tbl_per3.Text = pN3.ToString();
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            //크리스탈 리포트 보고서 출력
            CrystalReport1 report = new CrystalReport1();

            //이름 상관없이 순서데로임
            //DataTable table = new DataSetMain.DataTableTestListReortDataTable();
            DataTable table = new DataSet1.DataTableReportDataTable();

            //크리스탈 레포트에 해당 정보 저장
            DataRow row1 = table.NewRow();
            DataRow row2 = table.NewRow();
            DataRow row3 = table.NewRow();
            DataRow row4 = table.NewRow();
            DataRow row5 = table.NewRow();

            //발전소 이름 가져오기
            string comboS = combo_plant.Text.ToString();
            string plantName = comboS.Substring(0, 6);

            int totalArea = Int32.Parse(tbl_width1.Text) * Int32.Parse(tbl_length1.Text) + Int32.Parse(tbl_width2.Text) * Int32.Parse(tbl_length2.Text) + Int32.Parse(tbl_width3.Text) * Int32.Parse(tbl_length3.Text) + Int32.Parse(tbl_width4.Text) * Int32.Parse(tbl_length4.Text) + Int32.Parse(tbl_width5.Text) * Int32.Parse(tbl_length5.Text);
            //1번 로봇
            row1["PlantName"] = plantName;
            row1["CleanPeriod"] = "10";
            row1["CleanTimeR"] = "10";
            row1["CleanArea"] = Int32.Parse(tbl_width1.Text) * Int32.Parse(tbl_length1.Text);
            row1["TotalCleanTime"] = "50";
            row1["TotalCleanArea"] = totalArea;
            table.Rows.Add(row1);

            //2번 로봇
            row2["PlantName"] = plantName;
            row2["CleanPeriod"] = "10";
            row2["CleanTimeR"] = "10";
            row2["CleanArea"] = Int32.Parse(tbl_width2.Text) * Int32.Parse(tbl_length2.Text);
            row2["TotalCleanTime"] = "50";
            row2["TotalCleanArea"] = totalArea;
            table.Rows.Add(row2);

            //3번 로봇
            row3["PlantName"] = plantName;
            row3["CleanPeriod"] = "10";
            row3["CleanTimeR"] = "10";
            row3["CleanArea"] = Int32.Parse(tbl_width3.Text) * Int32.Parse(tbl_length3.Text);
            row3["TotalCleanTime"] = "50";
            row3["TotalCleanArea"] = totalArea;
            table.Rows.Add(row3);

            //4번 로봇
            row4["PlantName"] = plantName;
            row4["CleanPeriod"] = "10";
            row4["CleanTimeR"] = "10";
            row4["CleanArea"] = Int32.Parse(tbl_width4.Text) * Int32.Parse(tbl_length4.Text);
            row4["TotalCleanTime"] = "50";
            row4["TotalCleanArea"] = totalArea;
            table.Rows.Add(row4);

            //5번 로봇
            row5["PlantName"] = plantName;
            row5["CleanPeriod"] = "10";
            row5["CleanTimeR"] = "10";
            row5["CleanArea"] = Int32.Parse(tbl_width5.Text) * Int32.Parse(tbl_length5.Text);
            row5["TotalCleanTime"] = "50";
            row5["TotalCleanArea"] = totalArea;

            table.Rows.Add(row5);
            //레포트에 정보 추가 끝

            report.SetDataSource(table);

            //CrystalReport 데이터베이스에 값 추가하기
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string tableName = "CrystalReport";
            SqlCommand[] cmdC = new SqlCommand[5];
            for (int i = 0; i < 5; i++)
            {
                cmdC[i] = new SqlCommand("insert into " + tableName + " values (@PlantName, @CleanPeriod, @CleanTimeR, @CleanArea, @TotalCleanTime, @TotalCleanArea)", con);

                cmdC[i].Parameters.AddWithValue("@PlantName", plantName);
                cmdC[i].Parameters.AddWithValue("@CleanPeriod", 10);
                cmdC[i].Parameters.AddWithValue("@CleanTimeR", 10);
                cmdC[i].Parameters.AddWithValue("@TotalCleanTime", 50);
                cmdC[i].Parameters.AddWithValue("@TotalCleanArea", 50);
            }

            /*cmdC[0].Parameters.AddWithValue("@PlantName", plantName);
            cmdC[1].Parameters.AddWithValue("@CleanPeriod", 10);
            cmdC[2].Parameters.AddWithValue("@CleanTimeR", 10);
            cmdC.Parameters.AddWithValue("@CleanArea", Int32.Parse(tbl_width1.Text) * Int32.Parse(tbl_length1.Text));
            cmdC.Parameters.AddWithValue("@TotalCleanTime", 50);
            cmdC.Parameters.AddWithValue("@TotalCleanArea", 50);*/

            cmdC[0].Parameters.AddWithValue("@CleanArea", Int32.Parse(tbl_width1.Text) * Int32.Parse(tbl_length1.Text));
            cmdC[1].Parameters.AddWithValue("@CleanArea", Int32.Parse(tbl_width2.Text) * Int32.Parse(tbl_length2.Text));
            cmdC[2].Parameters.AddWithValue("@CleanArea", Int32.Parse(tbl_width3.Text) * Int32.Parse(tbl_length3.Text));
            cmdC[3].Parameters.AddWithValue("@CleanArea", Int32.Parse(tbl_width4.Text) * Int32.Parse(tbl_length4.Text));
            cmdC[4].Parameters.AddWithValue("@CleanArea", Int32.Parse(tbl_width5.Text) * Int32.Parse(tbl_length5.Text));

            //cmdC.ExecuteNonQuery();

            for(int i = 0; i < 5; i++)
            {
                cmdC[i].ExecuteNonQuery();
            }
            //데이터베이스 삽입 끝

            FormPrint form = new FormPrint();
            form.setReportSource(report);
            form.ShowDialog();
            form.Dispose();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            resultL.Text="1004 기기의 제자리 버튼을 클릭했습니다.";
            this.pgb_4.Value = 0;
            pN4 = 0;
            tbl_per4.Text = pN4.ToString();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            resultL.Text="1005 기기의 제자리 버튼을 클릭했습니다.";
            this.pgb_5.Value = 0;
            pN5 = 0;
            tbl_per5.Text = pN5.ToString();
        }
        //데이터베이스 값 받아오기
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
                    tbl_width1.Text = sdr["Lsize"].ToString();
                    tbl_length1.Text = sdr["Rsize"].ToString();
                    orienN[0] = sdr["Orientation"].ToString();
                }
                else if (num == 2)
                {
                    tbl_width2.Text = sdr["Lsize"].ToString();
                    tbl_length2.Text = sdr["Rsize"].ToString();
                    orienN[1] = sdr["Orientation"].ToString();
                }
                else if (num == 3)
                {
                    tbl_width3.Text = sdr["Lsize"].ToString();
                    tbl_length3.Text = sdr["Rsize"].ToString();
                    orienN[2] = sdr["Orientation"].ToString();
                }
                else if (num == 4)
                {
                    tbl_width4.Text = sdr["Lsize"].ToString();
                    tbl_length4.Text = sdr["Rsize"].ToString();
                    orienN[3] = sdr["Orientation"].ToString();
                }
                else
                {
                    tbl_width5.Text = sdr["Lsize"].ToString();
                    tbl_length5.Text = sdr["Rsize"].ToString();
                    orienN[4] = sdr["Orientation"].ToString();
                }
            }
            con.Close();
            /*수정*/
            sdr.Close();
            sdr.Dispose();
        }
        private void btn_setting1_Click(object sender, EventArgs e)//기기1 설정
        {
            //설정 패널 보이게
            panel_set.Visible = true;
            tbl_setID.Text = tbl_id1.Text.ToString();//기기 ID 설정
            panel_set.Location = new Point(645, 192);
        }
        private void btn_setting2_Click(object sender, EventArgs e)//기기2 설정
        {
            //설정 패널 보이게
            panel_set.Visible = true;
            tbl_setID.Text = tbl_id2.Text.ToString();//기기 ID 설정
            panel_set.Location = new Point(645, 192);
        }
        private void btn_setting3_Click(object sender, EventArgs e)//기기3 설정
        {
            //설정 패널 보이게
            panel_set.Visible = true;
            tbl_setID.Text = tbl_id3.Text.ToString();//기기 ID 설정
            panel_set.Location = new Point(645, 192);
        }
        private void btn_setting4_Click(object sender, EventArgs e)//기기4 설정
        {
            //설정 패널 보이게
            panel_set.Visible = true;
            tbl_setID.Text = tbl_id4.Text.ToString();//기기 ID 설정
            panel_set.Location = new Point(645, 319);
        }
        private void btn_setting5_Click(object sender, EventArgs e)//기기 5 설정
        {
            //설정 패널 보이게
            panel_set.Visible = true;
            tbl_setID.Text = tbl_id5.Text.ToString();//기기 ID 설정
            panel_set.Location = new Point(645, 319);
        }
    }
}
