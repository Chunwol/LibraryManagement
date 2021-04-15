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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection conn;
        MySqlCommand cmd;
        public string connstr;
        public static int form2_index;
        public int form1_number;
        public string Uid;
        public string Bid;
        public MySqlDataAdapter send_data;
        private void Db_login_Click(object sender, EventArgs e)
        {
            string loginName = db_id.Text;
            string password = db_pw.Text;

            if (loginName == "" && password == "")
            {
                MessageBox.Show("아이디와 비밀번호를 확인하십시오");
            }
            else
            {
                try
                {
                    connstr = "Server="+ db_ip.Text + ";Port="+ db_port.Text +";Database=library;Uid=" + loginName + ";Pwd=" + password;
                    conn = new MySqlConnection(connstr);
                    conn.Open();
                    MessageBox.Show("로그인에 성공하였습니다.", "Information");
                    Login_P.Visible = false;
                    this.ClientSize = new System.Drawing.Size(897, 561);
                    Lobby_P.Location = new Point(1, 1);
                    Main_P.Location = new Point(1, 82);
                    Lobby_P.Visible = true;
                    Main_P.Visible = true;
                    Member_P.Visible = false;
                    Book_P.Visible = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("  아이디 비밀번호가 다르거나\n 서버 상태가 양호하지 않습니다", "Information");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Login_P.Location = new Point(1, 1);
            this.ClientSize = new System.Drawing.Size(330, 396);
            Login_P.Visible = true;
            Lobby_P.Visible = false;
            Main_P.Visible = false;
            Member_P.Visible = false;
            Book_P.Visible = false;
        }

        public void Book_DB_List()
        {
            B_ListView1.View = View.Details;
            B_ListView1.Items.Clear();
            //
            string str = "select * from book;";
            //MessageBox.Show(str);
            MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                ListViewItem listitem = new ListViewItem(dr["bid"].ToString());
                listitem.SubItems.Add(dr["name"].ToString());
                listitem.SubItems.Add(dr["writer"].ToString());
                listitem.SubItems.Add(dr["realse"].ToString().Substring(0, 10));
                if ((dr["state"].ToString()).Equals("1"))
                    listitem.SubItems.Add("대출 가능");
                else
                    listitem.SubItems.Add("대출 불가");

                B_ListView1.Items.Add(listitem);
            }
        }

        public void Member_DB_List()
        {
            U_ListView1.View = View.Details;
            U_ListView1.Items.Clear();
            //
            string str = "select * from users;";
            string address;
            //MessageBox.Show(str);
            MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                ListViewItem listitem = new ListViewItem(dr["name"].ToString());
                address = dr["address1"].ToString();
                address += "   ";
                address += dr["address2"].ToString();
                listitem.SubItems.Add(address);
                listitem.SubItems.Add(dr["phone"].ToString());
                listitem.SubItems.Add(dr["birthday"].ToString().Substring(0, 10));

                U_ListView1.Items.Add(listitem);
            }
        }

        private void Member_B_Click(object sender, EventArgs e)
        {
            Member_P.Location = new Point(1, 82);
            Member_P.Visible = true;
            Main_P.Visible = false;
            Book_P.Visible = false;
            U_Reset();

        }

        private void Main_B_Click(object sender, EventArgs e)
        {
            Main_P.Location = new Point(1, 82);
            Main_P.Visible = true;
            Member_P.Visible = false;
            Book_P.Visible = false;
            M_Reset();
        }

        private void Book_B_Click(object sender, EventArgs e)
        {
            Book_P.Location = new Point(1, 82);
            Book_P.Visible = true;
            Member_P.Visible = false;
            Main_P.Visible = false;
            B_Reset();

        }

        private void Main_DB_List()
        {
            try
            {
                M_ListView.View = View.Details;
                M_ListView.Items.Clear();

                string str = "select * from loan where uname like '%" + M_N_Name_TB.Text + "%';";
                //MessageBox.Show(str);
                MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    string str2 = "select * from book where name like '%" + dr["bname"].ToString() + "%';";
                    //MessageBox.Show(str2);
                    MySqlDataAdapter adapt2 = new MySqlDataAdapter(str2, conn);
                    DataTable dt2 = new DataTable();
                    adapt2.Fill(dt2);
                    DataRow dr2 = dt2.Rows[0];

                    ListViewItem listitem = new ListViewItem(dr2["bid"].ToString());
                    listitem.SubItems.Add(dr["bname"].ToString());
                    listitem.SubItems.Add(dr2["writer"].ToString());
                    listitem.SubItems.Add(dr2["realse"].ToString().Substring(0, 10));
                    listitem.SubItems.Add(dr["sdate"].ToString().Substring(0, 10));

                    M_ListView.Items.Add(listitem);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("사용자가 존재하지 않습니다.");
            }

        }
        private void Main_BookName_Search()
        {
            try
            {
                if (!M_BookName.Text.Equals(""))
                {
                    M_B_Name_TB.Text = M_BookName.Text;
                }
                string str = "select * from book where name like '%" + M_B_Name_TB.Text + "%';";
                //MessageBox.Show(str);
                MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                form1_number = 0;
                if (dt.Rows.Count > 1)
                {
                    send_data = adapt;
                    form2_index = 6;
                    Form2 fb = new Form2();
                    fb.Owner = this;
                    fb.ShowDialog();
                }
                DataRow dr = dt.Rows[form1_number];
                M_B_Name_TB.Text = dr["name"].ToString();
                M_B_Num_TB.Text = dr["bid"].ToString();
                M_B_Writer_TB.Text = dr["writer"].ToString();
                M_B_Realse_TB.Text = dr["realse"].ToString().Substring(0, 10);
                Bid = dr["Bid"].ToString();
                if ((dr["state"].ToString()).Equals("1"))
                    M_B_State_TB.Text = "대출 가능";
                else
                    M_B_State_TB.Text = "대출 불가";
                M_BookName.Text = "";
            }
            catch (Exception ex)
            {
                M_B_Name_TB.Text = "";
                M_B_Num_TB.Text = "";
                M_B_Writer_TB.Text = "";
                M_B_Realse_TB.Text = "";
                M_B_State_TB.Text = "";
                MessageBox.Show("검색하신 도서가 존재하지 않습니다.");
            }
        }

        public void Member_Name_Search()
        {
            try
            {
                U_ListView2.View = View.Details;
                U_ListView2.Items.Clear();

                if (U_MemberName.Text.Equals(""))
                {
                    MessageBox.Show("사용자를 입력해주십시오.");
                }
                else
                {
                    string str = "select * from users where name like '%" + U_MemberName.Text + "%';";
                    //MessageBox.Show(str);
                    MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
                    DataTable dt = new DataTable();
                    adapt.Fill(dt);
                    form1_number = 0;
                    if (dt.Rows.Count > 1)
                    {
                        send_data = adapt;
                        form2_index = 5;
                        Form2 fb = new Form2();
                        fb.Owner = this;
                        fb.ShowDialog();
                    }

                    DataRow dr = dt.Rows[form1_number];
                    U_N_Name_TB.Text = dr["name"].ToString();
                    U_N_Address_TB1.Text = dr["address1"].ToString();
                    U_N_Address_TB2.Text = dr["address2"].ToString();
                    U_N_Phone_TB.Text = dr["phone"].ToString();
                    U_N_Birthday_TB.Text = dr["birthday"].ToString().Substring(0, 10);
                    Uid = dr["Uid"].ToString();
                    try
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            str = "select * from loan where uname like '%" + U_N_Name_TB.Text + "%';";
                            //MessageBox.Show(str);
                            adapt = new MySqlDataAdapter(str, conn);
                            dt = new DataTable();
                            adapt.Fill(dt);
                            dr = dt.Rows[i];
                            ListViewItem listitem = new ListViewItem(dr["bname"].ToString());
                            listitem.SubItems.Add(dr["sdate"].ToString().Substring(0, 10));
                            U_ListView2.Items.Add(listitem);
                        }
                    }
                    catch (Exception ex) { };

                    str = "select * from history where uname like '%" + U_N_Name_TB.Text + "%';";
                    //MessageBox.Show(str);
                    adapt = new MySqlDataAdapter(str, conn);
                    dt = new DataTable();
                    adapt.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        ListViewItem listitem = new ListViewItem(dr["bname"].ToString());
                        listitem.SubItems.Add(dr["sdate"].ToString().Substring(0, 10));
                        listitem.SubItems.Add(dr["rdate"].ToString().Substring(0, 10));
                        U_ListView2.Items.Add(listitem);
                    }

                    U_MemberName.Text = "";
                }

            }
            catch (Exception ex)
            {
                U_MemberName.Text = "";
                MessageBox.Show("사용자가 존재하지 않습니다.");
            }
        }
        public void Book_BookName_Search()
        {
            int num = 1;
            try
            {
                B_ListView2.View = View.Details;
                B_ListView2.Items.Clear();

                if (!B_BookName.Text.Equals(""))
                {
                    B_B_Name_TB.Text = B_BookName.Text;
                }
                string str = "select * from book where name like '%" + B_B_Name_TB.Text + "%';";
                //MessageBox.Show(str);
                MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
                DataTable dt = new DataTable();
                form1_number = 0;
                adapt.Fill(dt);
                if (dt.Rows.Count > 1)
                {
                    send_data = adapt;
                    form2_index = 6;
                    Form2 fb = new Form2();
                    fb.Owner = this;
                    fb.ShowDialog();
                }
                DataRow dr = dt.Rows[form1_number];
                B_B_Name_TB.Text = dr["name"].ToString();
                B_B_Num_TB.Text = dr["bid"].ToString();
                B_B_Writer_TB.Text = dr["writer"].ToString();
                B_B_Realse_TB.Text = dr["realse"].ToString().Substring(0, 10);
                Bid = dr["bid"].ToString();
                if ((dr["state"].ToString()).Equals("1"))
                    B_B_State_TB.Text = "대출 가능";
                else
                {
                    B_B_State_TB.Text = "대출 불가";
                    str = "select * from loan where bid like '%" + dr["bid"].ToString() + "%';";
                    //MessageBox.Show(str);
                    adapt = new MySqlDataAdapter(str, conn);
                    dt = new DataTable();
                    adapt.Fill(dt);
                    dr = dt.Rows[0];
                    ListViewItem listitem = new ListViewItem(num.ToString());
                    num++;
                    listitem.SubItems.Add(dr["uname"].ToString());
                    listitem.SubItems.Add(dr["sdate"].ToString().Substring(0, 10));
                    B_ListView2.Items.Add(listitem);
                }
                str = "select * from history where bname like '%" + B_B_Name_TB.Text + "%';";
                //MessageBox.Show(str);
                adapt = new MySqlDataAdapter(str, conn);
                dt = new DataTable();
                adapt.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    ListViewItem listitem = new ListViewItem(num.ToString());
                    num++;
                    listitem.SubItems.Add(dr["uname"].ToString());
                    listitem.SubItems.Add(dr["sdate"].ToString().Substring(0, 10));
                    listitem.SubItems.Add(dr["rdate"].ToString().Substring(0, 10));
                    B_ListView2.Items.Add(listitem);
                }

                B_BookName.Text = "";
            }
            catch (Exception ex)
            {
                B_BookName.Text = "";
                B_B_Name_TB.Text = "";
                B_B_Num_TB.Text = "";
                B_B_Writer_TB.Text = "";
                B_B_Realse_TB.Text = "";
                B_B_State_TB.Text = "";
                MessageBox.Show("검색하신 도서가 존재하지 않습니다.");
            }
        }
        private void M_MemberName_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (M_MemberName.Text.Equals(""))
                {
                    MessageBox.Show("사용자를 입력해주십시오.");
                }
                else
                {
                    string str = "select * from users where name like '%" + M_MemberName.Text + "%';";
                    //MessageBox.Show(str);
                    MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
                    DataTable dt = new DataTable();
                    adapt.Fill(dt);

                    if(dt.Rows.Count > 1)
                    {
                        form1_number = 0;
                        send_data = adapt;
                        form2_index = 5;
                        Form2 fb = new Form2();
                        fb.Owner = this;
                        fb.ShowDialog();
                    }

                    DataRow dr = dt.Rows[form1_number];
                    M_N_Name_TB.Text = dr["name"].ToString();
                    M_N_Address_TB1.Text = dr["address1"].ToString();
                    M_N_Address_TB2.Text = dr["address2"].ToString();
                    M_N_Phone_TB.Text = dr["phone"].ToString();
                    M_N_Birthday_TB.Text = dr["birthday"].ToString().Substring(0, 10);
                    Uid = dr["uid"].ToString();

                    Main_DB_List();

                    M_MemberName.Text = "";
                }
            }
            catch (Exception ex)
            {
                M_Reset();
                MessageBox.Show("사용자가 존재하지 않습니다.");
            }

        }

        private void M_BookName_Search_Click(object sender, EventArgs e)
        {
            Main_BookName_Search();
        }

        private void M_Insert_Click(object sender, EventArgs e)
        {
            if (!M_N_Name_TB.Text.Equals("") && !M_B_Name_TB.Text.Equals(""))
            {
                try
                {
                    if ((M_B_State_TB.Text).Equals("대출 가능"))
                    {
                        string str = "INSERT INTO loan (uid,bid,uname,bname,sdate)VALUES('" + Uid + "','" + Bid + "','" + M_N_Name_TB.Text + "','" + M_B_Name_TB.Text + "',NOW());";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        str = "UPDATE book SET state=0 WHERE bid='" + Bid + "';";
                        cmd = new MySqlCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        //Main_BookName_Search();
                        M_B_State_TB.Text = "대출 불가";
                        Main_DB_List();

                    }
                    else if ((M_B_State_TB.Text).Equals("대출 불가"))
                    {
                        string str = "select * from loan where bid like '%" + Bid + "%';";
                        //MessageBox.Show(str);
                        MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        DataRow dr = dt.Rows[0];

                        if ((dr["uid"].ToString()).Equals(Uid))
                        {
                            string str_date = dr["sdate"].ToString().Substring(0, 4);
                            str_date += dr["sdate"].ToString().Substring(5, 2);
                            str_date += dr["sdate"].ToString().Substring(8, 2);
                            string str2 = "INSERT INTO history (uid,bid,uname,bname,sdate,rdate)VALUES('" + Uid + "','" + Bid + "','" + M_N_Name_TB.Text + "','" + M_B_Name_TB.Text + "'," + str_date + ",now());";
                            MySqlCommand cmd = new MySqlCommand(str2, conn);
                            cmd.ExecuteNonQuery();
                            str2 = "UPDATE book SET state=1 WHERE bid='" + Bid + "';";
                            cmd = new MySqlCommand(str2, conn);
                            cmd.ExecuteNonQuery();
                            str2 = "DELETE FROM loan WHERE bid = '" + Bid + "';";
                            cmd = new MySqlCommand(str2, conn);
                            cmd.ExecuteNonQuery();

                            Main_BookName_Search();
                            Main_DB_List();
                        }
                        else
                        {
                            string mstr = "이미 " + dr["uname"].ToString() + "님이 대출중인 도서입니다";
                            MessageBox.Show(mstr);
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error");
                }
                Main_DB_List();
            }
        }

        private void B_BookName_Search_Click(object sender, EventArgs e)
        {
            if (B_BookName.Text.Equals(""))
                MessageBox.Show("이름을 입력하여 주십시오");
            else
            {
                Book_BookName_Search();
            }
        }

        private void U_MemberName_Search_Click(object sender, EventArgs e)
        {
            if (U_MemberName.Text.Equals(""))
                MessageBox.Show("이름을 입력하여 주십시오");
            else
            {
                Member_Name_Search();
            }
        }



        private void U_Add_Click(object sender, EventArgs e)
        {
            form2_index = 1;
            Form2 fb = new Form2();
            fb.Owner = this;
            fb.ShowDialog();
            Member_DB_List();
        }

        private void U_Update_Click(object sender, EventArgs e)
        {
            if (!U_N_Name_TB.Text.Equals(""))
            {
                form2_index = 2;
                Form2 fb = new Form2();
                fb.Owner = this;
                fb.ShowDialog();
                U_Reset();
            }
            else
            {
                MessageBox.Show("사용자 검색을 먼저 해주시기 바랍니다.", "추가");
            }
        }

        private void U_Delete_Click(object sender, EventArgs e)
        {
            if (!U_N_Name_TB.Text.Equals(""))
            {
                string str = "select * from loan where uid like '%" + Uid + "%';";
                //MessageBox.Show(str);
                MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                if (dt.Rows.Count == 0) {

                    if (MessageBox.Show(U_N_Name_TB.Text + "님의 정보를 정말로 삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        str = "DELETE FROM users WHERE uid = '" + Uid + "';";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("삭제가 정상적으로 완료되었습니다", "삭제");
                        U_Reset();
                    }
                    else { }
                }
                else
                {
                    MessageBox.Show("대출중인 도서가 존재합니다.", "제거");
                }
            }
            else
            {
                MessageBox.Show("사용자 검색을 먼저 해주시기 바랍니다.", "제거");
            }
        }

        private void B_Add_Click(object sender, EventArgs e)
        {
            form2_index = 3;
            Form2 fb = new Form2();
            fb.Owner = this;
            fb.ShowDialog();
            Book_DB_List();
        }

        private void B_Update_Click(object sender, EventArgs e)
        {
            if (!B_B_Name_TB.Text.Equals(""))
            {
                form2_index = 4;
                Form2 fb = new Form2();
                fb.Owner = this;
                fb.ShowDialog();
                B_Reset();
            }
            else
            {
                MessageBox.Show("도서 검색을 먼저 해주시기 바랍니다.", "수정");
            }


        }

        private void B_Delete_Click(object sender, EventArgs e)
        {
            string str = "select * from loan where bid like '%" + Bid + "%';";
            //MessageBox.Show(str);
            MySqlDataAdapter adapt = new MySqlDataAdapter(str, conn);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            if (!B_B_Name_TB.Text.Equals(""))
            {
                if (dt.Rows.Count == 0)
                {
                    if (MessageBox.Show(B_B_Name_TB.Text + "을 정말로 삭제하시겠습니까?", "도서 데이터 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        str = "DELETE FROM book WHERE Bid = '" + Bid + "';";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("삭제가 정상적으로 완료되었습니다", "삭제");
                        B_Reset();
                    }
                    else { }
                }
                else
                {
                    MessageBox.Show("대출중인 사용자가 존재합니다.", "제거");
                }
            }
            else
            {
                MessageBox.Show("도서 검색을 먼저 해주시기 바랍니다.", "제거");
            }
        }
        private void M_Reset()
        {
            Main_DB_List();
            B_ListView2.View = View.Details;
            B_ListView2.Items.Clear();
            M_B_Name_TB.Text = "";
            M_B_Num_TB.Text = "";
            M_B_Writer_TB.Text = "";
            M_B_Realse_TB.Text = "";
            M_B_State_TB.Text = "";
            M_N_Name_TB.Text = "";
            M_N_Address_TB1.Text = "";
            M_N_Address_TB2.Text = "";
            M_N_Phone_TB.Text = "";
            M_N_Birthday_TB.Text = "";
            M_MemberName.Text = "";
            M_BookName.Text = "";
        }
        private void B_Reset()
        {
            Book_DB_List();
            B_ListView2.View = View.Details;
            B_ListView2.Items.Clear();
            B_BookName.Text = "";
            B_B_Name_TB.Text = "";
            B_B_Num_TB.Text = "";
            B_B_Writer_TB.Text = "";
            B_B_Realse_TB.Text = "";
            B_B_State_TB.Text = "";
        }
        private void U_Reset()
        {
            Member_DB_List();
            U_ListView2.View = View.Details;
            U_ListView2.Items.Clear();
            U_N_Name_TB.Text = "";
            U_N_Address_TB1.Text = "";
            U_N_Address_TB2.Text = "";
            U_N_Phone_TB.Text = "";
            U_N_Birthday_TB.Text = "";
        }


    }
}