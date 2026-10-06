using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BaiTap52
{
    public partial class Form1 : Form
    {
        // Khai báo danh sách chứa dữ liệu các dịch vụ
        private Dictionary<string, List<Service>> serviceData = new Dictionary<string, List<Service>>();

        public Form1()
        {
            InitializeComponent();
            InitData();
        }

        // Tạo dữ liệu ban đầu
        private void InitData()
        {
            // Thêm các loại dịch vụ vào ComboBox
            cboCategory.Items.AddRange(new string[] { "Khám bệnh", "Xét nghiệm", "Chụp X-Quang", "Vắc-xin" });

            // Danh sách các món/dịch vụ theo từng loại
            serviceData["Khám bệnh"] = new List<Service>
            {
                new Service("Khám tổng quát", 150000),
                new Service("Khám chuyên khoa", 250000)
            };

            serviceData["Xét nghiệm"] = new List<Service>
            {
                new Service("Xét nghiệm máu", 120000),
                new Service("Xét nghiệm nước tiểu", 80000),
                new Service("Xét nghiệm đường huyết", 50000)
            };

            serviceData["Chụp X-Quang"] = new List<Service>
            {
                new Service("Chụp X-Quang Phổi", 200000),
                new Service("Chụp X-Quang Cột sống", 250000)
            };

            serviceData["Vắc-xin"] = new List<Service>
            {
                new Service("Vắc-xin Cúm", 300000),
                new Service("Vắc-xin Viêm gan B", 220000)
            };

            // Chọn mặc định loại đầu tiên
            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;

            txtDiscount.Text = "0";
        }

        // 1. Khi chọn loại dịch vụ trên ComboBox
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedItem == null) return;

            string selectedCategory = cboCategory.SelectedItem.ToString();
            lstAvailableServices.Items.Clear();

            if (serviceData.ContainsKey(selectedCategory))
            {
                foreach (var service in serviceData[selectedCategory])
                {
                    lstAvailableServices.Items.Add(service);
                }
            }
        }

        // Chuyển 1 món từ bảng trái sang bảng phải
        private void MoveSelectedService()
        {
            if (lstAvailableServices.SelectedItem != null)
            {
                Service selectedItem = (Service)lstAvailableServices.SelectedItem;
                lstSelectedServices.Items.Add(selectedItem);
                CalculateTotal();
            }
        }

        // 2. Nút chuyển dịch vụ (Nút >)
        private void btnSelect_Click(object sender, EventArgs e)
        {
            MoveSelectedService();
        }

        // 2. Double Click vào dịch vụ ở danh sách trái
        private void lstAvailableServices_DoubleClick(object sender, EventArgs e)
        {
            MoveSelectedService();
        }

        // Nút xóa dịch vụ được chọn khỏi bảng phải (Nút <)
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSelectedServices.SelectedItem != null)
            {
                lstSelectedServices.Items.Remove(lstSelectedServices.SelectedItem);
                CalculateTotal();
            }
        }

        // Nút xóa toàn bộ (Nút <<)
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            lstSelectedServices.Items.Clear();
            CalculateTotal();
        }

        // 3. Tự động tính lại tổng tiền
        private void CalculateTotal()
        {
            decimal subTotal = 0;

            foreach (Service item in lstSelectedServices.Items)
            {
                subTotal += item.Price;
            }

            decimal discountPercent = 0;
            decimal.TryParse(txtDiscount.Text, out discountPercent);

            if (discountPercent < 0) discountPercent = 0;
            if (discountPercent > 100) discountPercent = 100;

            decimal total = subTotal * (1 - (discountPercent / 100));

            txtSubTotal.Text = subTotal.ToString("N0") + " VNĐ";
            txtTotal.Text = total.ToString("N0") + " VNĐ";
        }

        // Tính lại tiền khi thay đổi chiết khấu
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }
    }
}
