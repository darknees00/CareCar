<template>
  <v-container style="margin: 30px">
    <v-subheader class="d-flex justify-space-between align-center">
      <h3>Khách hàng</h3>
      <v-col lg="6" cols="12">
        <v-text-field
          placeholder="Search here"
          append-inner-icon="mdi-magnify"
          v-model="searchTerm"
          @click:append-inner="searchCustomer"
        />
      </v-col>
      <v-btn color="success" @click="dialogAdd">Thêm</v-btn>
    </v-subheader>
    <v-table>
      <thead>
        <tr>
          <th class="text-left">STT</th>
          <th class="text-left">Họ tên</th>
          <th class="text-left">Số điện thoại</th>
          <th class="text-left">Địa chỉ</th>
          <th class="text-left">Email</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, id) in listCustomer" :key="item.khachHangId">
          <td>{{ id + 1 }}</td>
          <td>{{ item.tenKhachHang }}</td>
          <td>{{ item.soDienThoai }}</td>
          <td>{{ item.diaChi }}</td>
          <td>{{ item.email }}</td>
          <td>
            <v-btn color="primary" @click="editCustomer(item)">Sửa</v-btn>
            <v-btn color="error" @click="deleteCustomer(item)">Xóa</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>
  </v-container>
  <v-dialog v-model="dialogVisible" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Sửa thông tin khách hàng</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model="editingCustomer.tenKhachHang"
            label="Họ tên"
          ></v-text-field>
          <v-text-field
            v-model="editingCustomer.soDienThoai"
            label="Số điện thoại"
          ></v-text-field>
          <v-text-field
            v-model="editingCustomer.diaChi"
            label="Địa chỉ"
          ></v-text-field>
          <v-text-field
            v-model="editingCustomer.email"
            label="Email"
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="saveCustomer">Lưu</v-btn>
        <v-btn color="error" @click="cancel">Hủy</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <v-dialog v-model="dialogAddCustomer" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Thêm Khách hàng</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model="customer.tenKhachHang"
            label="Họ tên"
          ></v-text-field>
          <v-text-field
            v-model="customer.soDienThoai"
            label="Số điện thoại"
          ></v-text-field>
          <v-text-field
            v-model="customer.diaChi"
            label="Địa chỉ"
          ></v-text-field>
          <v-text-field v-model="customer.email" label="Email"></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="addCustomer">Lưu</v-btn>
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
      listCustomer: [],
      customer: {
        tenKhachHang: "",
        soDienThoai: "",
        diaChi: "",
        email: "",
      },
      searchTerm: "",
      dialogVisible: false,
      dialogAddCustomer: false,
      editingCustomer: null,
    };
  },
  mounted() {
    this.fetchData();
  },
  methods: {
    validateForm() {
      if (!this.customer.tenKhachHang) {
        this.showErrorMessage("Vui lòng nhập họ tên");
        return false;
      }
      if (!/^[^\d]+$/.test(this.customer.tenKhachHang)) {
        this.showErrorMessage("Họ tên không được chứa số");
        return false;
      }
      if (!this.customer.soDienThoai) {
        this.showErrorMessage("Vui lòng nhập số điện thoại");
        return false;
      }
      if (!/^[0-9]{10}$/.test(this.customer.soDienThoai)) {
        this.showErrorMessage("Số điện thoại không hợp lệ");
        return false;
      }
      if (!this.customer.diaChi) {
        this.showErrorMessage("Vui lòng nhập địa chỉ");
        return false;
      }
      if (!this.customer.email) {
        this.showErrorMessage("Vui lòng nhập email");
        return false;
      }
      if (!/.+@.+\..+/.test(this.customer.email)) {
        this.showErrorMessage("Email không hợp lệ");
        return false;
      }
      return true;
    },
    validateForm2() {
      if (!this.editingCustomer.tenKhachHang) {
        this.showErrorMessage("Vui lòng nhập họ tên");
        return false;
      }
      if (!/^[^\d]+$/.test(this.editingCustomer.tenKhachHang)) {
        this.showErrorMessage("Họ tên không được chứa số");
        return false;
      }
      if (!this.editingCustomer.soDienThoai) {
        this.showErrorMessage("Vui lòng nhập số điện thoại");
        return false;
      }
      if (!/^[0-9]{10}$/.test(this.editingCustomer.soDienThoai)) {
        this.showErrorMessage("Số điện thoại không hợp lệ");
        return false;
      }
      if (!this.editingCustomer.diaChi) {
        this.showErrorMessage("Vui lòng nhập địa chỉ");
        return false;
      }
      if (!this.editingCustomer.email) {
        this.showErrorMessage("Vui lòng nhập email");
        return false;
      }
      if (!/.+@.+\..+/.test(this.editingCustomer.email)) {
        this.showErrorMessage("Email không hợp lệ");
        return false;
      }
      return true;
    },
    fetchData() {
      axios
        .get(`https://localhost:7040/api/KhachHang`)
        .then((response) => {
          this.listCustomer = response.data;
        })
        .catch((error) => {
          console.error(error);
          this.showErrorMessage("Tải dữ liệu thất bại");
        });
    },
    dialogAdd() {
      this.dialogAddCustomer = true;
      this.customer = {
        tenKhachHang: "",
        soDienThoai: "",
        diaChi: "",
        email: "",
      };
    },
    addCustomer() {
      if (this.validateForm()) {
        axios
          .post(`https://localhost:7040/api/KhachHang`, this.customer)
          .then(() => {
            this.fetchData();
            this.dialogAddCustomer = false;
          })
          .catch((error) => {
            console.error(error);
            this.showErrorMessage(error);
          });
      }
    },
    editCustomer(customer) {
      this.editingCustomer = { ...customer };
      this.dialogVisible = true;
    },
    saveCustomer() {
      if (this.editingCustomer && this.validateForm2()) {
        axios
          .put(
            `https://localhost:7040/api/KhachHang/${this.editingCustomer.khachHangId}`,
            this.editingCustomer
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
    deleteCustomer(customer) {
      const confirmDelete = confirm(
        "Bạn có chắc chắn muốn xóa khách hàng này không?"
      );
      if (confirmDelete) {
        axios
          .delete(
            `https://localhost:7040/api/KhachHang/${customer.khachHangId}`
          )
          .then(() => {
            this.fetchData();
          })
          .catch((error) => {
            console.error(error);
            this.showErrorMessage("Xóa không thành công");
          });
      }
    },
    cancel() {
      this.dialogVisible = false;
      this.dialogAddCustomer = false;
    },
    searchCustomer() {
      if (!this.searchTerm) {
        this.fetchData(); // Tải lại dữ liệu ban đầu
        return;
      }
      axios
        .get(
          `https://localhost:7040/api/KhachHang/search?searchTerm=${this.searchTerm}`
        )
        .then((response) => {
          this.listCustomer = response.data;
        })
        .catch((error) => {
          console.error(error);
          this.fetchData();
        });
    },
    showErrorMessage(message) {
      alert(message);
    },
  },
};
</script>
