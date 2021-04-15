using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        MySqlConnection conn;
        string connstr;
        int buffer = -1;
        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 fa = (Form1)this.Owner;
            try
            {
                connstr = fa.connstr;
                conn = new MySqlConnection(connstr);
                conn.Open();
                //MessageBox.Show("서버 상태", "Information");
            }
            catch (Exception)
            {
                MessageBox.Show("서버 상태가 양호하지 않습니다", "Information");
                this.Close();
            }
            U_View_P.Visible = false;
            U_Update_P.Visible = false;
            U_Add_P.Visible = false;
            B_Update_P.Visible = false;
            B_Add_P.Visible = false;

            //MessageBox.Show(Form1.form2_index.ToString(), "Information");

            switch (Form1.form2_index)
            {
                case 1:
                    this.ClientSize = new System.Drawing.Size(446, 488);

                    U_Add_P.Visible = true;
                    B_Update_P.Visible = false;
                    B_Add_P.Visible = false;
                    U_Update_P.Visible = false;
                    U_View_P.Visible = false;
                    B_View_P.Visible = false;
                    U_Add_P.Location = new System.Drawing.Point(1, 1);

                    break;
                case 2:
                    this.ClientSize = new System.Drawing.Size(446, 488);
                    U_Update_P.Visible = true;
                    U_Add_P.Visible = false;
                    B_Update_P.Visible = false;
                    B_Add_P.Visible = false;
                    U_View_P.Visible = false;
                    B_View_P.Visible = false;
                    U_Update_P.Location = new System.Drawing.Point(1, 1);
                    try
                    {
                        U_Update_Name.Text = fa.U_N_Name_TB.Text;
                        U_Update_Address1.Text = fa.U_N_Address_TB1.Text;
                        U_Update_Address2.Text = fa.U_N_Address_TB2.Text;
                        U_Update_Phone2.Text = fa.U_N_Phone_TB.Text.Substring(4, 4);
                        U_Update_Phone3.Text = fa.U_N_Phone_TB.Text.Substring(9, 4);
                        U_Update_Birthday1.Text = fa.U_N_Birthday_TB.Text.Substring(0, 4);
                        U_Update_Birthday2.Text = fa.U_N_Birthday_TB.Text.Substring(5, 2);
                        U_Update_Birthday3.Text = fa.U_N_Birthday_TB.Text.Substring(8, 2);
                    }
                    catch (Exception ex){ }
                    break;
                case 3:
                    this.ClientSize = new System.Drawing.Size(446, 418);
                    B_Add_P.Visible = true;
                    U_Update_P.Visible = false;
                    U_Add_P.Visible = false;
                    B_Update_P.Visible = false;
                    U_View_P.Visible = false;
                    B_View_P.Visible = false;
                    B_Add_P.Location = new System.Drawing.Point(1, 1);

                    break;
                case 4:
                    this.ClientSize = new System.Drawing.Size(446, 418);
                    B_Update_P.Visible = true;
                    U_Update_P.Visible = false;
                    U_Add_P.Visible = false;
                    B_Add_P.Visible = false;
                    U_View_P.Visible = false;
                    B_View_P.Visible = false;
                    B_Update_P.Location = new System.Drawing.Point(1, 1);
                    try
                    {
                        B_Update_Name.Text = fa.B_B_Name_TB.Text;
                        B_Update_Writer.Text = fa.B_B_Writer_TB.Text;

                        B_Update_Realse1.Text = fa.B_B_Realse_TB.Text.Substring(0, 4);
                        B_Update_Realse2.Text = fa.B_B_Realse_TB.Text.Substring(5, 2);
                        B_Update_Realse3.Text = fa.B_B_Realse_TB.Text.Substring(8, 2);
                    }
                    catch (Exception ex) { }
                    break;
                case 5:
                    this.ClientSize = new System.Drawing.Size(1029, 272);
                    U_View_P.Visible = true;
                    B_View_P.Visible = false;
                    B_Update_P.Visible = false;
                    U_Update_P.Visible = false;
                    U_Add_P.Visible = false;
                    B_Add_P.Visible = false;
                    U_View_P.Location = new System.Drawing.Point(1, 1);
                    DataTable dt = new DataTable();
                    fa.send_data.Fill(dt);
                    U_View.View = View.Details;
                    U_View.CheckBoxes = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ListViewItem listitem = new ListViewItem();
                        listitem.SubItems.Add(dr["uid"].ToString());
                        listitem.SubItems.Add(dr["name"].ToString());
                        listitem.SubItems.Add(dr["address1"].ToString() +"  "+ dr["address2"].ToString());
                        listitem.SubItems.Add(dr["phone"].ToString());
                        listitem.SubItems.Add(dr["birthday"].ToString().Substring(0, 10));

                        //listitem.SubItems.Add(dr["birthday"].ToString().Substring(0, 10));
                        U_View.Items.Add(listitem);
                    }

                    break;
                case 6:
                    this.ClientSize = new System.Drawing.Size(1029, 272);
                    B_View_P.Visible = true;
                    U_View_P.Visible = false;
                    B_Update_P.Visible = false;
                    U_Update_P.Visible = false;
                    U_Add_P.Visible = false;
                    B_Add_P.Visible = false;
                    B_View_P.Location = new System.Drawing.Point(1, 1);
                    dt = new DataTable();
                    fa.send_data.Fill(dt);
                    B_View.View = View.Details;
                    B_View.CheckBoxes = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ListViewItem listitem = new ListViewItem();
                        listitem.SubItems.Add(dr["bid"].ToString());
                        listitem.SubItems.Add(dr["name"].ToString());
                        listitem.SubItems.Add(dr["writer"].ToString());
                        listitem.SubItems.Add(dr["realse"].ToString().Substring(0, 10));
                        if ((dr["state"].ToString()).Equals("1"))
                            listitem.SubItems.Add("대출 가능");
                        else
                            listitem.SubItems.Add("대출 불가");
                        //listitem.SubItems.Add(dr["birthday"].ToString().Substring(0, 10));
                        B_View.Items.Add(listitem);
                    }

                    break;





            }
        }
        private void U_View_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 fa = (Form1)this.Owner;
            //fa.form1_number = 0;
            //U_View.Items[2].Checked;
            int num = 0;
            buffer = -1;
            int i = 0;
            for (i = U_View.Items.Count - 1; i >= 0; i--)
            {
                if (U_View.Items[i].Checked == true)
                {
                    buffer = i;
                    num++;
                    
                }
            }
            if(num == 2){
                U_View.Items[fa.form1_number].Checked = false;
            }else if(num == 1){
                fa.form1_number = buffer;
            }

        }

        private void B_View_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 fa = (Form1)this.Owner;
            //fa.form1_number = 0;
            //U_View.Items[2].Checked;
            int num = 0;
            buffer = -1;
            int i = 0;
            for (i = B_View.Items.Count - 1; i >= 0; i--)
            {
                if (B_View.Items[i].Checked == true)
                {
                    buffer = i;
                    num++;

                }
            }
            if (num == 2)
            {
                B_View.Items[fa.form1_number].Checked = false;
            }
            else if (num == 1)
            {
                fa.form1_number = buffer;
            }

        }
        private void U_Add_DB_Click(object sender, EventArgs e)
        {
            int error = 0;
            try
            {
                if (U_Add_Name.Text.Equals(""))
                {
                    MessageBox.Show("이름을 입력하여 주십시오", "Information");
                    error++;
                }
                else if (!(U_Add_Phone2.Text.Length == 4) || !(U_Add_Phone3.Text.Length == 4))
                {
                    MessageBox.Show("전화번호를 확인하여 주십시오", "Information");
                    U_Add_Phone2.Text = "";
                    U_Add_Phone3.Text = "";
                    error++;
                }
                else if (U_Add_Address1.Text.Equals("") || U_Add_Address2.Text.Equals(""))
                {
                    MessageBox.Show("주소를 확인하여 주십시오", "Information");
                    U_Add_Address1.Text = "";
                    U_Add_Address2.Text = "";
                    error++;
                }
                else if (!(U_Add_Birthday1.Text.Length == 4) || !(U_Add_Birthday2.Text.Length == 2) || !(U_Add_Birthday3.Text.Length == 2))
                {
                    MessageBox.Show("생년월일를 확인하여 주십시오", "Information");
                    U_Add_Birthday1.Text = "";
                    U_Add_Birthday2.Text = "";
                    U_Add_Birthday3.Text = "";
                    error++;
                }
                else if (!(Convert.ToInt32(U_Add_Birthday1.Text) > 1000) || !(Convert.ToInt32(U_Add_Birthday2.Text) > 0) || !(Convert.ToInt32(U_Add_Birthday2.Text) < 13) || !(Convert.ToInt32(U_Add_Birthday3.Text) > 0) || !(Convert.ToInt32(U_Add_Birthday3.Text) < 32))
                {
                    MessageBox.Show("생년월일를 확인하여 주십시오", "Information");
                    U_Add_Birthday1.Text = "";
                    U_Add_Birthday2.Text = "";
                    U_Add_Birthday3.Text = "";
                    error++;
                }
                if (error == 0)
                {
                    string Phone = "010-" + U_Add_Phone2.Text + "-" + U_Add_Phone3.Text;
                    string Birthday = U_Add_Birthday1.Text + "-" + U_Add_Birthday2.Text + "-" + U_Add_Birthday3.Text;
                    string str = "INSERT INTO users (NAME,address1,address2,phone,birthday)VALUES('" + U_Add_Name.Text + "','" + U_Add_Address1.Text + "','" + U_Add_Address2.Text + "','" + Phone + "','" + Birthday + "');";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("추가 완료", "Information");


                    U_Add_Name.Text = "";
                    U_Add_Address1.Text = "";
                    U_Add_Address2.Text = "";
                    U_Add_Phone2.Text = "";
                    U_Add_Phone3.Text = "";
                    U_Add_Birthday1.Text = ""; 
                    U_Add_Birthday2.Text = "";
                    U_Add_Birthday3.Text = "";
                }
            } catch (Exception ex) {
                MessageBox.Show("생년월일를 확인하여 주십시오", "Information");
                U_Add_Birthday1.Text = "";
                U_Add_Birthday2.Text = "";
                U_Add_Birthday3.Text = "";
                error++;
            }
        }

        private void B_Add_DB_Click(object sender, EventArgs e)
        {
            int error = 0;
            try
            {
                if (B_Add_Name.Text.Equals(""))
                {
                    MessageBox.Show("책이름을 확인하여 주십시오", "Information");
                    error++;
                }
                else if (B_Add_Writer.Text.Equals(""))
                {
                    MessageBox.Show("작가명을 확인하여 주십시오", "Information");
                    error++;
                }
                else if (!(B_Add_Realse1.Text.Length == 4) || !(B_Add_Realse2.Text.Length == 2) || !(B_Add_Realse3.Text.Length == 2))
                {
                    MessageBox.Show("출판년도를 확인하여 주십시오", "Information");
                    B_Add_Realse1.Text = "";
                    B_Add_Realse2.Text = "";
                    B_Add_Realse3.Text = "";
                    error++;
                }
                else if (!(Convert.ToInt32(B_Add_Realse1.Text) > 1000) || !(Convert.ToInt32(B_Add_Realse2.Text) > 0) || !(Convert.ToInt32(B_Add_Realse2.Text) < 13) || !(Convert.ToInt32(B_Add_Realse3.Text) > 0) || !(Convert.ToInt32(B_Add_Realse3.Text) < 32))
                {
                    MessageBox.Show("출판년도를 확인하여 주십시오", "Information");
                    B_Add_Realse1.Text = "";
                    B_Add_Realse2.Text = "";
                    B_Add_Realse3.Text = "";
                    error++;
                }
                if (error == 0)
                {
                    string Realse = B_Add_Realse1.Text + "-" + B_Add_Realse2.Text + "-" + B_Add_Realse3.Text;

                    string str = "INSERT INTO book (name,writer,realse) VALUES('" + B_Add_Name.Text + "','" + B_Add_Writer.Text + "','" + Realse + "');";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("추가 완료", "Information");

                    Form1 fa = (Form1)this.Owner;
                    fa.Book_DB_List();

                    B_Add_Name.Text = "";
                    B_Add_Writer.Text = "";
                    B_Add_Realse1.Text = "";
                    B_Add_Realse2.Text = "";
                    B_Add_Realse3.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("출판년도를 확인하여 주십시오", "Information");
                B_Add_Realse1.Text = "";
                B_Add_Realse2.Text = "";
                B_Add_Realse3.Text = "";
                error++;
            }
        }
        private void Update_DB_Click(object sender, EventArgs e)
        {
            int error = 0;
            try
            {
                if (U_Update_Name.Text.Equals(""))
                {
                    MessageBox.Show("이름을 입력하여 주십시오", "Information");
                    error++;
                }else if (!(U_Update_Phone2.Text.Length == 4) || !(U_Update_Phone3.Text.Length == 4))
                {
                    MessageBox.Show("전화번호를 확인하여 주십시오", "Information");
                    U_Update_Phone2.Text = "";
                    U_Update_Phone3.Text = "";
                    error++;
                }
                else if (U_Update_Address1.Text.Equals("") || U_Update_Address2.Text.Equals(""))
                {
                    MessageBox.Show("주소를 확인하여 주십시오", "Information");
                    U_Update_Address1.Text = "";
                    U_Update_Address2.Text = "";
                    error++;
                }
                else if (!(U_Update_Birthday1.Text.Length == 4) || !(U_Update_Birthday2.Text.Length == 2) || !(U_Update_Birthday3.Text.Length == 2))
                {
                    MessageBox.Show("생년월일를 확인하여 주십시오", "Information");
                    U_Update_Birthday1.Text = "";
                    U_Update_Birthday2.Text = "";
                    U_Update_Birthday3.Text = "";
                    error++;
                }
                else if (!(Convert.ToInt32(U_Update_Birthday1.Text) > 1000) || !(Convert.ToInt32(U_Update_Birthday2.Text) > 0) || !(Convert.ToInt32(U_Update_Birthday2.Text) < 13) || !(Convert.ToInt32(U_Update_Birthday3.Text) > 0) || !(Convert.ToInt32(U_Update_Birthday3.Text) < 32))
                {
                    MessageBox.Show("생년월일를 확인하여 주십시오", "Information");
                    U_Update_Birthday1.Text = "";
                    U_Update_Birthday2.Text = "";
                    U_Update_Birthday3.Text = "";
                    error++;
                }

                if (error == 0)
                {
                    string Phone = "010-" + U_Update_Phone2.Text + "-" + U_Update_Phone3.Text;

                    string Birthday = U_Update_Birthday1.Text + "-" + U_Update_Birthday2.Text + "-" + U_Update_Birthday3.Text;

                    Form1 fa = (Form1)this.Owner;
                    string str = "UPDATE users SET name = '" + U_Update_Name.Text + "', address1 = '"
                        + U_Update_Address1.Text + "', address2 = '" + U_Update_Address2.Text + "', phone = '" + Phone + "', birthday = '" + Birthday + "' WHERE uid = '" + fa.Uid + "';";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();

                    str = "UPDATE loan SET uname = '" + U_Update_Name.Text + "' WHERE uid = '" + fa.Uid + "';";
                    cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();

                    str = "UPDATE history SET uname = '" + U_Update_Name.Text + "' WHERE uid = '" + fa.Uid + "';";
                    cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("수정 완료", "Information");

                }
            } catch (Exception ex) {
                MessageBox.Show("생년월일를 확인하여 주십시오", "Information");
                U_Add_Birthday1.Text = "";
                U_Add_Birthday2.Text = "";
                U_Add_Birthday3.Text = "";
                error++;
            }


}
        private void B_Update_DB_Click(object sender, EventArgs e)
        {
            int error = 0;
            try
            {
                if (B_Update_Name.Text.Equals(""))
                {
                    MessageBox.Show("책이름을 확인하여 주십시오", "Information");
                    error++;
                }
                else if (B_Update_Writer.Text.Equals(""))
                {
                    MessageBox.Show("작가명을 확인하여 주십시오", "Information");
                    error++;
                }
                else if (!(B_Update_Realse1.Text.Length == 4) || !(B_Update_Realse2.Text.Length == 2) || !(B_Update_Realse3.Text.Length == 2))
                {
                    MessageBox.Show("출판년도를 확인하여 주십시오", "Information");
                    B_Add_Realse1.Text = "";
                    B_Add_Realse2.Text = "";
                    B_Add_Realse3.Text = "";
                    error++;
                }
                else if (!(Convert.ToInt32(B_Update_Realse1.Text) > 1000) || !(Convert.ToInt32(B_Update_Realse2.Text) > 0) || !(Convert.ToInt32(B_Update_Realse2.Text) < 13) || !(Convert.ToInt32(B_Update_Realse3.Text) > 0) || !(Convert.ToInt32(B_Update_Realse3.Text) < 32))
                {
                    MessageBox.Show("출판년도를 확인하여 주십시오", "Information");
                    B_Add_Realse1.Text = "";
                    B_Add_Realse2.Text = "";
                    B_Add_Realse3.Text = "";
                    error++;
                }
                if (error == 0)
                {
                    string Realse = B_Update_Realse1.Text + "-" + B_Update_Realse2.Text + "-" + B_Update_Realse3.Text;
                    Form1 fa = (Form1)this.Owner;
                    string str = "UPDATE book SET name = '" + B_Update_Name.Text + "', writer = '" + B_Update_Writer.Text + "', realse = '" + Realse + "' WHERE bid = '" + fa.Bid + "';";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();

                    str = "UPDATE loan SET bname = '" + B_Update_Name.Text + "' WHERE bid = '" + fa.Bid + "';";
                    cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();

                    str = "UPDATE history SET bname = '" + B_Update_Name.Text + "' WHERE bid = '" + fa.Bid + "';";
                    cmd = new MySqlCommand(str, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("수정 완료", "Information");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("출판년도를 확인하여 주십시오", "Information");
                B_Add_Realse1.Text = "";
                B_Add_Realse2.Text = "";
                B_Add_Realse3.Text = "";
                error++;
            }
        }

        private void U_View_Insert_Click(object sender, EventArgs e)
        {
            if (!(buffer == -1))
                Close();
            else
                MessageBox.Show("사용자를 선택해주시기 바랍니다", "Information");
        }

        private void B_View_Insert_Click(object sender, EventArgs e)
        {
            if (!(buffer == -1))
                Close();
            else
                MessageBox.Show("책을 선택해주시기 바랍니다", "Information");
        }
    }
}
