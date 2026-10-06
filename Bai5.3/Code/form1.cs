using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BaiTap53
{
    public partial class Form1 : Form
    {
        private List<Product> productList = new List<Product>();
        private BindingSource bindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            cboCategory.Items.AddRange(new string[] { "Điện thoại", "Laptop", "Phụ kiện", "Gia dụng" });
            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;

            productList.Add(new Product("SP01", "iPhone 15", 20000000, 10, "Điện thoại"));
            productList.Add(new Product("SP02", "MacBook Air M2", 25000000, 5, "Laptop"));
            productList.Add(new Product("SP03", "Chuột Logitech", 500000, 20, "Phụ kiện"));

            bindingSource.DataSource = productList;
            dgvProducts.DataSource = bindingSource;

            FormatDataGridView();
        }

        private void FormatDataGridView()
        {
            if (dgvProducts.Columns["ProductId"] != null)
                dgvProducts.Columns["ProductId"].HeaderText = "Mã SP";
            if (dgvProducts.Columns["ProductName"] != null)
                dgvProducts.Columns["ProductName"].HeaderText = "Tên SP";
            if (dgvProducts.Columns["UnitPrice"] != null)
            {
                dgvProducts.Columns["UnitPrice"].HeaderText = "Đơn Giá";
                dgvProducts.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            }
            if (dgvProducts.Columns["Quantity"] != null)
                dgvProducts.Columns["Quantity"].HeaderText = "Số Lượng";
            if (dgvProducts.Columns["Category"] != null)
                dgvProducts.Columns["Category"].HeaderText = "Danh Mục";
        }

        private void RefreshGrid()
        {
            bindingSource.ResetBindings(false);
        }

        private void ClearInputs()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;
            txtProductId.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã SP và Tên SP!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (productList.Any(p => p.ProductId.Equals(txtProductId.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal unitPrice = 0;
            int quantity = 0;
            decimal.TryParse(txtUnitPrice.Text, out unitPrice);
            int.TryParse(txtQuantity.Text, out quantity);

            Product newProduct = new Product
            {
                ProductId = txtProductId.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                UnitPrice = unitPrice,
                Quantity = quantity,
                Category = cboCategory.SelectedItem != null ? cboCategory.SelectedItem.ToString() : ""
            };

            productList.Add(newProduct);
            RefreshGrid();
            ClearInputs();
            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

                txtProductId.Text = row.Cells["ProductId"].Value?.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString();
                txtUnitPrice.Text = row.Cells["UnitPrice"].Value?.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value?.ToString();
                cboCategory.SelectedItem = row.Cells["Category"].Value?.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string id = txtProductId.Text.Trim();
            Product targetProduct = productList.FirstOrDefault(x => x.ProductId.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (targetProduct == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm có mã này để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal unitPrice = 0;
            int quantity = 0;
            decimal.TryParse(txtUnitPrice.Text, out unitPrice);
            int.TryParse(txtQuantity.Text, out quantity);

            targetProduct.ProductName = txtProductName.Text.Trim();
            targetProduct.UnitPrice = unitPrice;
            targetProduct.Quantity = quantity;
            targetProduct.Category = cboCategory.SelectedItem != null ? cboCategory.SelectedItem.ToString() : "";

            RefreshGrid();
            ClearInputs();
            MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null || dgvProducts.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product selectedProduct = (Product)dgvProducts.CurrentRow.DataBoundItem;

            if (selectedProduct != null)
            {
                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa sản phẩm [{selectedProduct.ProductName}] không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    productList.Remove(selectedProduct);
                    RefreshGrid();
                    ClearInputs();
                    MessageBox.Show("Đã xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                bindingSource.DataSource = productList;
            }
            else
            {
                var filteredList = productList.Where(p => p.ProductName.ToLower().Contains(keyword)).ToList();
                bindingSource.DataSource = filteredList;
            }

            RefreshGrid();
        }

        // Các hàm phụ hỗ trợ sửa lỗi Design bị rác event
        private void label3_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
    }
}
