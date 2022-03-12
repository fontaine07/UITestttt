using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using UITest.Forms;

namespace UITest
{
    public partial class FormMainMenu : Form
    {
        //Khai báo
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        //Khởi tạo
        public FormMainMenu()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            //loại bỏ title bar mặc định
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;//giới hạn kích thước cửa sổ để không chèn vào task bar của desktop
        }
        //Màu
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color colorHome = Color.FromArgb(89, 131, 252);
            public static Color colorBase = Color.FromArgb(41, 53, 86);
        }

        //Phương thức
        private void ActiveButton(object senderBtn, Color color) //Button khi nhấp chuột
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button hiện tại
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Button bên trái
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //Icon của Child Form
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }
        private void HoverButton(object senderBtn, Color color) //Button khi di chuyển chuột vào vùng button
        {
            if (senderBtn != null)
            {
                //Button hiện tại
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(41, 53, 86);
                currentBtn.ForeColor = color;
                currentBtn.IconColor = color;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;
            }
        }
        private void DisableButton() //Bỏ highlight button khi thoát khỏi vùng button
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(46, 69, 131);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void FormMainMenu_Load(object sender, EventArgs e)
        {

        }
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //chỉ mở form
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
        }
        private void iconGiohang_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color1);
            OpenChildForm(new FormGioHang());
        }

        private void iconKigui_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color2);
            OpenChildForm(new FormKiGui());
        }

        private void iconSanpham_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color3);
            OpenChildForm(new FormSanPham());
        }

        private void iconTaikhoan_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color4);
            OpenChildForm(new FormTaiKhoan());
        }

        private void iconCaidat_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGBColors.color5);
            OpenChildForm(new FormCaiDat());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = RGBColors.colorHome;
            lblTitleChildForm.Text = "Trang chủ";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btnThuNhoKhoiPhuc_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnTatCuaSo_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnTatCuaSo_MouseHover(object sender, EventArgs e)
        {
            HoverButton(sender, RGBColors.colorHome);
        }

        private void btnTatCuaSo_MouseLeave(object sender, EventArgs e)
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(41, 53, 86);
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void btnThuNhoKhoiPhuc_MouseHover(object sender, EventArgs e)
        {
            HoverButton(sender, RGBColors.colorHome);
        }

        private void btnThuNhoKhoiPhuc_MouseLeave(object sender, EventArgs e)
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(41, 53, 86);
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void btnThoat_MouseHover(object sender, EventArgs e)
        {
            HoverButton(sender, RGBColors.colorHome);
        }
        private void btnThoat_MouseLeave(object sender, EventArgs e)
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(41, 53, 86);
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;
            }
        }
        //Xóa viền khi phóng toàn cửa sổ
        private void FormMainMenu_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                FormBorderStyle = FormBorderStyle.None;
            else
                FormBorderStyle = FormBorderStyle.Sizable;
        }

    }
}
