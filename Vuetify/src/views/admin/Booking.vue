<template>
  <v-container style="margin: 30px">
    <v-subheader class="d-flex justify-space-between align-center">
      <h3>Lịch hẹn</h3>
      <!-- <v-col lg="6" cols="12">
        <v-text-field
          placeholder="Search here"
          append-inner-icon="mdi-magnify"
          v-model="searchTerm"
          @click:append-inner="searchBooking"
        />
      </v-col> -->
      <v-btn color="success" @click="addBooking">Thêm</v-btn>
    </v-subheader>
    <v-table style="margin-top: 50px">
      <thead>
        <tr>
          <th class="text-left">STT</th>
          <th class="text-left">Tên Khách Hàng</th>
          <th class="text-left">Tên Dịch vụ</th>
          <th class="text-left">Hãng xe</th>
          <th class="text-left">Ngày đặt dịch vụ</th>
          <th class="text-left">Ngày hẹn</th>
          <th class="text-left">Trạng thái</th>
          <th class="text-left">Tiền</th>
          <th class="text-left">Thanh toán</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, id) in listBooking" :key="item.lichHenId">
          <td>{{ id + 1 }}</td>
          <td>{{ getTenKhachHang(item.khachHangId) }}</td>
          <td>{{ getTenDichVu(item.dichVuId) }}</td>
          <td>{{ item.xe }}</td>
          <td>{{ formatDate(item.ngay) }}</td>
          <td>{{ formatDate(item.ngayHen) }}</td>
          <td>{{ getTrangThai(item.trangThaiId) }}</td>
          <td>{{ item.tongTien }}$</td>
          <td>{{ item.thanhToan }}</td>
          <td>
            <v-btn color="primary" @click="editBooking(item)">Sửa</v-btn>
            <v-btn color="error" @click="deleteBooking(item)">Xóa</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>
  </v-container>
  <v-dialog v-model="dialogVisible" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Lịch hẹn</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-autocomplete
            v-model="editingBooking.khachHangId"
            :items="listCustomer"
            label="Khách hàng"
            item-title="tenKhachHang"
            item-value="khachHangId"
          ></v-autocomplete>
          <v-text-field
            v-model="editingBooking.xe"
            label="Loại xe"
          ></v-text-field>
          <v-autocomplete
            v-model="editingBooking.dichVuId"
            :items="listDichVu"
            label="Dịch vụ"
            item-title="tenDichVu"
            item-value="dichVuId"
          ></v-autocomplete>
          <v-text-field
            v-model="editingBooking.ngayHen"
            label="Ngày hẹn"
            type="date"
            :value="formattedNgayHen"
          ></v-text-field>
          <v-autocomplete
            v-model="editingBooking.trangThaiId"
            label="Trạng Thái"
            :items="listStatus"
            item-value="trangThaiId"
            item-title="tenTrangThai"
          ></v-autocomplete>
          <v-text-field
            v-model="editingBooking.tongTien"
            label="Tổng tiền"
            readonly
          ></v-text-field>
          <v-select
            v-model="editingBooking.thanhToan"
            label="Thanh toán"
            :items="thanhToan"
            item-value="value"
            item-title="text"
          ></v-select>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="saveBooking">Lưu</v-btn>
        <v-btn color="error" @click="cancel">Hủy</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <v-dialog v-model="dialogCustomer" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline"> Khách hàng</span>
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
            type="number"
          ></v-text-field>
          <v-text-field
            v-model="customer.diaChi"
            label="Địa chỉ"
          ></v-text-field>
          <v-text-field v-model="customer.email" label="Email"></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="saveCustomer">Lưu</v-btn>
        <v-btn color="error" @click="cancelCustomer">Hủy</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <v-dialog v-model="dialogAddBooking" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Thêm lịch hẹn</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-autocomplete
            v-model="formData.khachHangId"
            :items="listCustomer"
            label="Khách hàng"
            item-title="tenKhachHang"
            item-value="khachHangId"
          ></v-autocomplete>
          <v-text-field v-model="formData.xe" label="Loại xe"></v-text-field>
          <v-autocomplete
            v-model="formData.dichVuId"
            :items="listDichVu"
            label="Dịch vụ"
            item-title="tenDichVu"
            item-value="dichVuId"
          ></v-autocomplete>
          <v-text-field
            v-model="formData.ngayHen"
            label="Ngày hẹn"
            type="date"
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="booking">Lưu</v-btn>
        <v-btn color="error" @click="cancelAddBooking">Hủy</v-btn>
        <v-btn color="purple" @click="addCustomer">Thêm khách hàng</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      listDichVu: [],
      listBooking: [],
      listCustomer: [],
      listStatus: [],
      formData: {
        lichHenId: "",
        khachHangId: "",
        xe: "",
        ngayHen: "",
        trangThaiId: "",
        dichVuId: "",
        tongTien: 0,
        thanhToan: false,
        ngay: "",
      },
      customer: {
        tenKhachHang: "",
        soDienThoai: "",
        diaChi: "",
        email: "",
      },
      searchTerm: "",
      editingBooking: null,
      dialogVisible: false,
      dialogCustomer: false,
      dialogAddBooking: false,
      thanhToan: [
        {
          text: "Đã thanh toán",
          value: true,
        },
        {
          text: "Chưa thanh toán",
          value: false,
        },
      ],
    };
  },
  created() {
    this.fetchData();
  },
  computed: {
    formattedNgayHen() {
      if (this.editingBooking.ngayHen) {
        const date = new Date(this.editingBooking.ngayHen);
        const year = date.getFullYear();
        const month = (date.getMonth() + 1).toString().padStart(2, "0");
        const day = date.getDate().toString().padStart(2, "0");
        return `${year}-${month}-${day}`;
      }
      return "";
    },
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
    formatDate(date) {
      const formattedDate = new Date(date);
      const options = { year: "numeric", month: "long", day: "numeric" };
      return formattedDate.toLocaleDateString("vi-VN", options);
    },
    fetchData() {
      axios
        .get("https://localhost:7040/api/DichVu")
        .then((response) => {
          this.listDichVu = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
      axios
        .get("https://localhost:7040/api/KhachHang")
        .then((response) => {
          this.listCustomer = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
      axios
        .get("https://localhost:7040/api/TrangThai")
        .then((response) => {
          this.listStatus = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
      axios
        .get("https://localhost:7040/api/LichHen")
        .then((response) => {
          this.listBooking = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    getTenDichVu(dichVuId) {
      const dichvu = this.listDichVu.find(
        (dichvu) => dichvu.dichVuId === dichVuId
      );
      return dichvu ? dichvu.tenDichVu : "";
    },
    getTenKhachHang(khachHangId) {
      const kh = this.listCustomer.find((kh) => kh.khachHangId === khachHangId);
      return kh ? kh.tenKhachHang : "";
    },
    getTrangThai(trangThaiId) {
      const kh = this.listStatus.find((kh) => kh.trangThaiId === trangThaiId);
      return kh ? kh.tenTrangThai : "";
    },
    searchBooking() {
      if (!this.searchTerm) {
        this.fetchData(); // Tải lại dữ liệu ban đầu
        return;
      }
      axios
        .get(
          `https://localhost:7040/api/LichHen/search?searchTerm=${this.searchTerm}`
        )
        .then((response) => {
          this.listBooking = response.data;
        })
        .catch((error) => {
          console.error(error);
          this.fetchData();
          this.showErrorMessage("Tìm kiếm không thành công");
        });
    },
    addCustomer() {
      this.dialogCustomer = true;
    },
    saveCustomer() {
      if (this.validateForm()) {
        axios
          .post(`https://localhost:7040/api/KhachHang`, this.customer)
          .then(() => {
            this.fetchData();
            this.clearFormCustomer();
            this.dialogCustomer = false;
            this.showErrorMessage("Khách hàng mới thêm thành công");
          })
          .catch((error) => {
            console.error(error);
            this.fetchData();
            this.dialogCustomer = false;
          });
      }
    },
    addBooking() {
      this.dialogAddBooking = true;
    },
    booking() {
      const selectedDichVu = this.listDichVu.find(
        (dichvu) => dichvu.dichVuId === this.formData.dichVuId
      );

      if (selectedDichVu) {
        this.formData.tongTien = selectedDichVu.giaTien;
      }

      const selectedKhachHang = this.listCustomer.find(
        (kh) => kh.khachHangId === this.formData.khachHangId
      );

      this.formData.dichVuId = selectedDichVu;
      this.formData.khachHangId = selectedKhachHang;
      this.formData.trangThaiId = 2;
      this.formData.thanhToan = "Chưa thanh toán";
      const currentDate = new Date();
      this.formData.ngay = `${currentDate.getFullYear()}-${
        currentDate.getMonth() + 1
      }-${currentDate.getDate()}`;
      axios
        .post("https://localhost:7040/api/LichHen", {
          khachHangId: selectedKhachHang.khachHangId,
          xe: this.formData.xe,
          ngay: this.formData.ngay,
          ngayHen: this.formData.ngayHen,
          trangThaiId: this.formData.trangThaiId,
          dichVuId: selectedDichVu.dichVuId,
          tongTien: this.formData.tongTien,
          thanhToan: this.formData.thanhToan,
        })
        .then(() => {
          this.fetchData();
          this.clearForm();
          this.dialogAddBooking = false
        })
        .catch((error) => {
          console.error(error);
          this.fetchData();
          this.clearForm();
          this.dialogAddBooking = false
        });
    },
    editBooking(Booking) {
      this.dialogVisible = true; // Show the dialog
      this.editingBooking = Object.assign({}, Booking); // Create a copy of the Booking object
    },
    saveBooking() {
      if (this.editingBooking) {
        if (this.editingBooking.thanhToan === true) {
          this.editingBooking.thanhToan = "Đã thanh toán";
        } else {
          this.editingBooking.thanhToan = "Chưa thanh toán";
        }
        axios
          .put(
            `https://localhost:7040/api/LichHen/${this.editingBooking.lichHenId}`,
            this.editingBooking
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
    deleteBooking(Booking) {
      const confirmDelete = confirm(
        "Bạn có chắc chắn muốn xóa lịch hẹn này không?"
      );
      axios;
      if (confirmDelete) {
        axios
          .delete(`https://localhost:7040/api/LichHen/${Booking.lichHenId}`)
          .then(() => {
            this.fetchData();
          })
          .catch((error) => {
            console.error(error);
          });
      }
    },
    clearForm() {
      this.formData = {
        khachHangId: "",
        xe: "",
        ngayHen: "",
        trangThaiId: "",
        dichVuId: "",
        tongTien: 0,
        thanhToan: false,
      };
      this.formData.khachHangId = "";
      this.formData.dichVuId = "";
    },
    clearFormCustomer() {
      this.customer = {
        khachHangId: "",
        soDienThoai: "",
        diaChi: "",
        email: "",
      };
      this.formData.khachHangId = "";
      this.formData.dichVuId = "";
    },
    cancel() {
      this.dialogVisible = false;
    },
    cancelCustomer() {
      this.dialogCustomer = false;
    },
    cancelAddBooking() {
      this.dialogAddBooking = false;
    },
    showErrorMessage(message) {
      alert(message);
    },
  },
  watch: {
    "editingBooking.dichVuId": function (newDichVuId) {
      const selectedDichVu = this.listDichVu.find(
        (dichvu) => dichvu.dichVuId === newDichVuId
      );
      if (selectedDichVu) {
        this.editingBooking.tongTien = selectedDichVu.giaTien;
      }
    },
  },
};
</script>
