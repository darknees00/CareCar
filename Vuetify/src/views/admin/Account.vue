<template>
  <v-container style="margin: 30px">
    <v-subheader class="d-flex justify-space-between align-center">
      <h3>Nhân viên</h3>
      <v-col lg="6" cols="12">
        <v-text-field
          placeholder="Search here"
          append-inner-icon="mdi-magnify"
          v-model="searchTerm"
          @click:append-inner="searchNhanVien"
        />
      </v-col>
      <v-btn color="success" @click="dialogAddNhanVien">Thêm</v-btn>
    </v-subheader>
    <v-table>
      <thead>
        <tr>
          <th class="text-left">ID</th>
          <th class="text-left">Họ tên</th>
          <th class="text-left">Tên đăng nhập</th>
          <th class="text-left">Mật khẩu</th>
          <th class="text-left">Email</th>
          <th class="text-left">Số điện thoại</th>
          <th class="text-left">Quyền</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, id) in listNhanVien" :key="item.nhanVienId">
          <td>{{ id + 1 }}</td>
          <td>{{ item.hoTen }}</td>
          <td>{{ item.tenDangNhap }}</td>
          <td>{{ item.matKhau }}</td>
          <td>{{ item.email }}</td>
          <td>{{ item.phone }}</td>
          <td>{{ item.quyen }}</td>
          <td>
            <v-btn color="primary" @click="editNhanVien(item)">Sửa</v-btn>
            <v-btn color="error" @click="deleteNhanVien(item)">Xóa</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>
  </v-container>
  <v-dialog v-model="dialogVisible" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Thông tin tài khoản</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model="editingNhanVien.hoTen"
            label="Họ tên"
            required
          ></v-text-field>
          <v-text-field
            v-model="editingNhanVien.tenDangNhap"
            label="Tên đăng nhập"
            required
          ></v-text-field>
          <v-text-field
            v-model="editingNhanVien.matKhau"
            label="Mật khẩu"
            required
            type="password"
          ></v-text-field>
          <v-text-field
            v-model="editingNhanVien.email"
            label="Email"
            required
          ></v-text-field>
          <v-text-field
            v-model="editingNhanVien.phone"
            label="Số điện thoại"
            required
          ></v-text-field>
          <v-text-field
            v-model="editingNhanVien.quyen"
            label="Quyền"
            required
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="saveNhanVien">Lưu</v-btn>
        <v-btn color="error" @click="cancel">Hủy</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <v-dialog v-model="dialogAdd" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Thêm nhân viên</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model="nhanvien.hoTen"
            label="Họ tên"
            type="text"
            required
          ></v-text-field>
          <v-text-field
            v-model="nhanvien.tenDangNhap"
            label="Tên đăng nhập"
            type="text"
            required
          ></v-text-field>
          <v-text-field
            v-model="nhanvien.matKhau"
            label="Mật khẩu"
            required
            type="password"
          ></v-text-field>
          <v-text-field
            v-model="nhanvien.email"
            label="Email"
            type="email"
            required
          ></v-text-field>
          <v-text-field
            v-model="nhanvien.phone"
            label="Số điện thoại"
            type="number"
            required
          ></v-text-field>
          <v-text-field
            v-model="nhanvien.quyen"
            label="Quyền"
            type="text"
            required
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="addNhanVien">Lưu</v-btn>
        <v-btn color="error" @click="cancel">Hủy</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      listNhanVien: [],
      nhanvien: {
        hoTen: "",
        tenDangNhap: "",
        matKhau: "",
        email: "",
        phone: "",
        quyen: "",
      },
      searchTerm: "",
      editingNhanVien: null,
      dialogVisible: false,
      dialogAdd :false,
      quyen: [
        {
          text: "admin",
          value: true,
        },
        {
          text: "nhanvien",
          value: false,
        },
      ],
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    validateForm() {
      if (!this.nhanvien.hoTen) {
        this.showErrorMessage("Vui lòng nhập họ tên");
        return false;
      }
      if (!/^[^\d]+$/.test(this.nhanvien.hoTen)) {
        this.showErrorMessage("Họ tên không được chứa số");
        return false;
      }
      if (!this.nhanvien.tenDangNhap) {
        this.showErrorMessage("Vui lòng nhập tên đăng nhập");
        return false;
      }
      if (!this.nhanvien.matKhau) {
        this.showErrorMessage("Vui lòng nhập mật khẩu");
        return false;
      }
      if (!this.nhanvien.email) {
        this.showErrorMessage("Vui lòng nhập email");
        return false;
      }
      if (!/.+@.+\..+/.test(this.nhanvien.email)) {
        this.showErrorMessage("Email không hợp lệ");
        return false;
      }
      if (!this.nhanvien.phone) {
        this.showErrorMessage("Vui lòng nhập số điện thoại");
        return false;
      }
      if (!/^[0-9]{10}$/.test(this.nhanvien.phone)) {
        this.showErrorMessage("Số điện thoại không hợp lệ");
        return false;
      }
      if (!this.nhanvien.quyen) {
        this.showErrorMessage("Vui lòng nhập quyền");
        return false;
      }
      return true;
    },
    validateForm2() {
      if (!this.editingNhanVien.hoTen) {
        this.showErrorMessage("Vui lòng nhập họ tên");
        return false;
      }
      if (!this.editingNhanVien.tenDangNhap) {
        this.showErrorMessage("Vui lòng nhập tên đăng nhập");
        return false;
      }
      if (!this.editingNhanVien.matKhau) {
        this.showErrorMessage("Vui lòng nhập mật khẩu");
        return false;
      }
      if (!this.editingNhanVien.email) {
        this.showErrorMessage("Vui lòng nhập email");
        return false;
      }
      if (!this.editingNhanVien.phone) {
        this.showErrorMessage("Vui lòng nhập số điện thoại");
        return false;
      }
      if (!this.editingNhanVien.quyen) {
        this.showErrorMessage("Vui lòng nhập quyền");
        return false;
      }
      return true;
    },
    fetchData() {
      axios
        .get(`https://localhost:7040/api/NhanVien`)
        .then((response) => {
          this.listNhanVien = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    searchNhanVien() {
      if (!this.searchTerm) {
        this.fetchData(); // Tải lại dữ liệu ban đầu
        return;
      }
      axios
        .get(
          `https://localhost:7040/api/NhanVien/search?searchTerm=${this.searchTerm}`
        )
        .then((response) => {
          this.listNhanVien = response.data;
        })
        .catch((error) => {
          console.error(error);
          this.fetchData();
        });
    },
    dialogAddNhanVien(){
      this.dialogAdd = true;
    },
    addNhanVien() {
      if (this.validateForm()) {
        axios
          .post(`https://localhost:7040/api/NhanVien`, this.nhanvien)
          .then(() => {
            this.fetchData();
            this.clearForm();
            this.dialogAdd = false;
          })
          .catch((error) => {
            console.error(error);
            this.showErrorMessage(error)
          });
      }
    },
    editNhanVien(nhanvien) {
      this.dialogVisible = true; // Show the dialog
      this.editingNhanVien = {...nhanvien}; // Create a copy of the Car object
    },
    saveNhanVien() {
      if (this.editingNhanVien && this.validateForm2()) {
        axios
          .put(
            `https://localhost:7040/api/NhanVien/${this.editingNhanVien.nhanVienId}`,
            this.editingNhanVien
          )
          .then(() => {
            this.fetchData();
            this.dialogVisible = false;
          })
          .catch((error) => {
            console.error(error);
            this.showErrorMessage("Lưu không thành công");
          });
      }
    },
    deleteNhanVien(nhanVien) {
      const confirmDelete = confirm(
        "Bạn có chắc chắn muốn xóa nhân viên này không?"
      );
      if (confirmDelete) {
        axios
          .delete(`https://localhost:7040/api/NhanVien/${nhanVien.nhanVienId}`)
          .then(() => {
            this.fetchData();
          })
          .catch((error) => {
            console.error(error);
          });
      }
    },
    clearForm() {
      this.nhanvien = {
        hoTen: "",
        tenDangNhap: "",
        matKhau: "",
        email: "",
        phone: "",
        quyen: "",
      };
    },
    cancel() {
      this.dialogVisible = false;
      this.dialogAdd = false;
    },
    showErrorMessage(message) {
      alert(message);
    },
  },
};
</script>
