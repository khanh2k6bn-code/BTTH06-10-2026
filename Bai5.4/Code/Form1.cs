using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BaiTap54
{
    public partial class Form1 : Form
    {
        private List<Employee> employeeList = new List<Employee>();

        public Form1()
        {
            InitializeComponent();
            InitListViewColumns();
            InitViewModeComboBox();
            InitTreeViewData();
            InitEmployeeData();

            // Gán sự kiện cho TreeView và ComboBox
            tvDepartments.AfterSelect += tvDepartments_AfterSelect;
            cboViewMode.SelectedIndexChanged += cboViewMode_SelectedIndexChanged;
        }

        // Hàm bổ sung để khắc phục lỗi CS1061 Form1_Load trong Designer
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // 1. Khởi tạo cột cho ListView
        private void InitListViewColumns()
        {
            lsvEmployees.Columns.Clear();
            lsvEmployees.Columns.Add("Mã NV", 90);
            lsvEmployees.Columns.Add("Họ Tên", 160);
            lsvEmployees.Columns.Add("Chức vụ", 130);
            lsvEmployees.Columns.Add("Ngày vào làm", 110);
        }

        // 2. Nạp chế độ xem vào ComboBox
        private void InitViewModeComboBox()
        {
            cboViewMode.Items.Clear();
            cboViewMode.Items.AddRange(new string[] { "Details", "LargeIcon", "SmallIcon", "List", "Tile" });
            cboViewMode.SelectedIndex = 0;
        }

        // 3. Xây dựng cây dữ liệu TreeView
        private void InitTreeViewData()
        {
            tvDepartments.Nodes.Clear();

            TreeNode root = new TreeNode("Công Ty Công Nghệ ABC", 0, 0) { Tag = "ROOT" };

            TreeNode nodeCNTT = new TreeNode("Phòng CNTT", 0, 0) { Tag = "CNTT" };
            TreeNode nodeDev = new TreeNode("Nhóm Lập Trình", 0, 0) { Tag = "DEV" };
            TreeNode nodeQA = new TreeNode("Nhóm Kiểm Thử (QA)", 0, 0) { Tag = "QA" };
            nodeCNTT.Nodes.Add(nodeDev);
            nodeCNTT.Nodes.Add(nodeQA);

            TreeNode nodeNS = new TreeNode("Phòng Nhân Sự", 0, 0) { Tag = "NS" };
            TreeNode nodeTuyenDung = new TreeNode("Nhóm Tuyển Dụng", 0, 0) { Tag = "TD" };
            TreeNode nodeDaoTao = new TreeNode("Nhóm Đào Tạo", 0, 0) { Tag = "DT" };
            nodeNS.Nodes.Add(nodeTuyenDung);
            nodeNS.Nodes.Add(nodeDaoTao);

            root.Nodes.Add(nodeCNTT);
            root.Nodes.Add(nodeNS);

            tvDepartments.Nodes.Add(root);
            tvDepartments.ExpandAll();
        }

        // 4. Khởi tạo danh sách nhân viên mẫu
        private void InitEmployeeData()
        {
            employeeList = new List<Employee>
            {
                new Employee("NV01", "Nguyễn Văn An", "Trưởng Phòng", new DateTime(2020, 3, 15), "CNTT"),
                new Employee("NV02", "Lê Thị Bích", "Lập trình viên C#", new DateTime(2021, 6, 1), "DEV"),
                new Employee("NV03", "Trần Văn Cường", "Lập trình viên Web", new DateTime(2022, 1, 10), "DEV"),
                new Employee("NV04", "Phạm Minh Dung", "Tester", new DateTime(2023, 5, 20), "QA"),
                new Employee("NV05", "Hoàng Văn Em", "Trưởng Phòng NS", new DateTime(2019, 8, 12), "NS"),
                new Employee("NV06", "Vũ Thị Hương", "Chuyên viên Tuyển dụng", new DateTime(2022, 11, 5), "TD"),
                new Employee("NV07", "Đặng Văn Giang", "Chuyên viên Đào tạo", new DateTime(2023, 2, 18), "DT")
            };
        }

        // 5. Sự kiện khi bấm chọn một node trên TreeView
        private void tvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;

            string selectedTag = e.Node.Tag.ToString();
            List<Employee> filteredEmployees;

            if (selectedTag == "ROOT")
            {
                filteredEmployees = employeeList;
            }
            else
            {
                List<string> validCodes = GetSubNodeTags(e.Node);
                validCodes.Add(selectedTag);

                filteredEmployees = employeeList
                    .Where(emp => validCodes.Contains(emp.DepartmentCode))
                    .ToList();
            }

            LoadEmployeesToListView(filteredEmployees);
        }

        private List<string> GetSubNodeTags(TreeNode parentNode)
        {
            List<string> tags = new List<string>();
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag != null) tags.Add(child.Tag.ToString());
                tags.AddRange(GetSubNodeTags(child));
            }
            return tags;
        }

        // 6. Hiển thị danh sách nhân viên lên ListView
        private void LoadEmployeesToListView(List<Employee> list)
        {
            lsvEmployees.Items.Clear();

            foreach (var emp in list)
            {
                ListViewItem item = new ListViewItem(emp.EmployeeId);
                item.ImageIndex = 0;
                item.SubItems.Add(emp.FullName);
                item.SubItems.Add(emp.Position);
                item.SubItems.Add(emp.StartDate.ToString("dd/MM/yyyy"));

                lsvEmployees.Items.Add(item);
            }
        }

        // 7. Thay đổi chế độ xem trên ComboBox
        private void cboViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboViewMode.SelectedItem?.ToString())
            {
                case "Details":
                    lsvEmployees.View = View.Details;
                    break;
                case "LargeIcon":
                    lsvEmployees.View = View.LargeIcon;
                    break;
                case "SmallIcon":
                    lsvEmployees.View = View.SmallIcon;
                    break;
                case "List":
                    lsvEmployees.View = View.List;
                    break;
                case "Tile":
                    lsvEmployees.View = View.Tile;
                    break;
            }
        }
    }
}
