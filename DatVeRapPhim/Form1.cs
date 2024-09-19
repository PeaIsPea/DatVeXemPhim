using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatVeRapPhim
{
    public partial class Form1 : Form
    {
        List<Customer> dsKH = new List<Customer>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xuLyVe();
        }

        private void xuLyVe()
        {
            pnGhe.Controls.Clear();
            int ghe = 1;
            for(int i = 0;i< pnGhe.RowCount;i++)
            {
                for(int j = 0; j < pnGhe.ColumnCount; j++)
                {
                    Label lblGhe = new Label();
                    lblGhe.Text = ghe + "";
                    lblGhe.AutoSize = false;
                    lblGhe.Dock = DockStyle.Fill;
                    lblGhe.TextAlign =ContentAlignment.MiddleCenter;
                    lblGhe.Width = lblGhe.Height = 50;
                    lblGhe.BackColor = Color.White;
                    pnGhe.Controls.Add(lblGhe, i, j);
                    ghe++;
                    lblGhe.Click += LblGhe_Click;
                }
            }
        }

        private void LblGhe_Click(object sender, EventArgs e)
        {
            Label lblGhe = sender as Label;
            if(lblGhe.BackColor == Color.White)
            {
                lblGhe.BackColor = Color.Green;
            }
            else if(lblGhe.BackColor == Color.Green){
                lblGhe.BackColor = Color.White;

            }
            else if( lblGhe.BackColor == Color.Yellow) 
            {
                MessageBox.Show("Ghế số [" + lblGhe.Text + "] đã có người đặt");
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {

            
            FrmThongTinDatVe frm = new FrmThongTinDatVe();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Customer cus = new Customer();
                cus.Name = frm.txtHoTen.Text;
                cus.Phone = frm.txtSdt.Text;
                cus.GioDatGhe = DateTime.Now;
                
                

                for(int i = 0; i < pnGhe.Controls.Count; i++)
                {
                    Label lblGhe = pnGhe.Controls[i] as Label;
                    if(lblGhe.BackColor == Color.Green)
                    {
                        lblGhe.BackColor = Color.Yellow;
                        int ghe = int.Parse(lblGhe.Text);
                        cus.Ghes.Add(ghe);
                    }
                }
                txtThanhTien.Text = cus.TinhTien + "VND";
                dsKH.Add(cus);
                HienThiTongTien();
                HienThiKhachHang();
                

            }
        }

        private void HienThiKhachHang()
        {
            lstDanhSach.Items.Clear();
            foreach(Customer cus in dsKH)
            {
                lstDanhSach.Items.Add(cus);
            }
        }

        private void HienThiTongTien()
        {
            int sum = 0;
            foreach(Customer cus in dsKH)
            {
                sum += cus.TinhTien;
                
            }
            txtTongTien.Text = sum + "VND";
        }

        private void lstDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lstDanhSach.SelectedIndex!= -1)
            {
                Customer cus = lstDanhSach.SelectedItem as Customer;
                txtThanhTien.Text = cus.TinhTien.ToString();
                
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //coming soon
            if(lstDanhSach.SelectedIndex!= -1)
            {
                Customer cus = lstDanhSach.SelectedItem as Customer;
                //kiểm tra DateTime.now với cus.GioDatGhe
                // Tính toán khoảng thời gian đã trôi qua (đơn vị: phút)
                TimeSpan elapsedTime = DateTime.Now - cus.GioDatGhe;
                double minutesElapsed = elapsedTime.TotalMinutes;

                // Kiểm tra điều kiện hủy
                if (minutesElapsed <= 30)
                {
                    var gheDaDat = new HashSet<int>(cus.Ghes);

                    for (int i = 0; i < pnGhe.Controls.Count; i++)
                    {
                        Label lblGhe = pnGhe.Controls[i] as Label;
                        int maGhe = int.Parse(lblGhe.Text);

                        if (gheDaDat.Contains(maGhe))
                        {
                            lblGhe.BackColor = Color.White;
                        }
                    }

                    dsKH.Remove(cus);
                    HienThiKhachHang();
                    HienThiTongTien();
                    // Cho phép hủy đặt vé
                    // ... các xử lý khi hủy đặt vé, ví dụ: cập nhật trạng thái đặt vé, thông báo cho người dùng...
                    MessageBox.Show("Đặt vé đã được hủy.");
                }
                else
                {
                    // Không cho phép hủy
                    MessageBox.Show("Quá thời gian cho phép hủy đặt vé.");
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn khách hàng trước mới hủy được");
            }

        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn muốn thoát?" ,"Hỏi", 
                MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)

            {
                this.Close(); // Đóng form hiện tại
            }
        }
    }
}
