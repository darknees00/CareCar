<template>
  <v-container>
    <v-row justify="center">
      <v-col lg="12" class="text-center">
        <v-subheader class="text-h4 justify-center">Đặt dịch vụ</v-subheader>
      </v-col>
      <v-col lg="6">
        <v-form ref="form">
          <v-text-field
            v-model="formData.tenKhachHang"
            label="Họ tên"
            :rules="[
              (v) => !!v || 'Không được để trống',
              (v) => /^[^\d]+$/.test(v) || 'Họ tên không được chứa số',
            ]"
            type="text"
            required
          ></v-text-field>
          <v-text-field
            v-model="formData.soDienThoai"
            label="Số điện thoại"
            :rules="[
              (v) => !!v || 'Không được để trống',
              (v) => /^[0-9]{10}$/.test(v) || 'Số điện thoại không hợp lệ',
            ]"
            type="number"
            required
          ></v-text-field>
          <v-text-field
            v-model="formData.diaChi"
            label="Địa chỉ"
            required
            :rules="[(v) => !!v || 'Không được để trống']"
            type="text"
          ></v-text-field>
          <v-text-field
            v-model="formData.email"
            label="Email"
            required
            :rules="[
              (v) => !!v || 'Không được để trống',
              (v) => /.+@.+\..+/.test(v) || 'Email không hợp lệ',
            ]"
            type="email"
          ></v-text-field>
          <v-text-field
            v-model="formData.xe"
            :rules="[(v) => !!v || 'Không được để trống']"
            label="Loại xe"
          ></v-text-field>
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
          <v-text-field
            v-model="formData.tongTien"
            label="Tiền dự tính"
            readonly
          ></v-text-field>
          <v-btn color="success" class="mr-4" @click="addBooking"
            >Đặt lịch</v-btn
          >
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      listDichVu: [],
      formData: {
        khachHangId: "",
        tenKhachHang: "",
        email: "",
        soDienThoai: "",
        diaChi: "",
        lichHenId: "",
        xe: "",
        ngayHen: "",
        trangThaiId: "",
        dichVuId: "",
        ngay: "",
        tongTien: 0,
        thanhToan: "",
      },
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    fetchData() {
      axios
        .get("https://localhost:7040/api/DichVu")
        .then((response) => {
          this.listDichVu = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    addBooking() {
      this.$refs.form.validate().then((valid) => {
        if (valid !== null) {
          axios
            .post(`https://localhost:7040/api/KhachHang`, {
              tenKhachHang: this.formData.tenKhachHang,
              soDienThoai: this.formData.soDienThoai,
              diaChi: this.formData.diaChi,
              email: this.formData.email,
            })
            .then((response) => {
              const khachHangId = response.data.khachHangId;
              const selectedDichVu = this.listDichVu.find(
                (dichvu) => dichvu.dichVuId === this.formData.dichVuId
              );
              this.formData.trangThaiId = 2;
              this.formData.thanhToan = "Chưa thanh toán";
              const currentDate = new Date();
              this.formData.ngay = `${currentDate.getFullYear()}-${
                currentDate.getMonth() + 1
              }-${currentDate.getDate()}`;
              axios
                .post(`https://localhost:704/api/LichHen`, {
                  khachHangId: khachHangId,
                  xe: this.formData.xe,
                  ngay: this.formData.ngay,
                  ngayHen: this.formData.ngayHen,
                  trangThaiId: this.formData.trangThaiId,
                  dichVuId: selectedDichVu.dichVuId,
                  tongTien: this.formData.tongTien,
                  thanhToan: this.formData.thanhToan,
                })
                .then(() => {
                  this.clearForm();
                  this.showErrorMessage("Bạn đã đặt lịch thành công");
                })
                .catch((error) => {
                  console.error(error);
                  this.showErrorMessage("Gửi không thành công1");
                });
            })
            .catch((error) => {
              console.error(error);
              this.showErrorMessage("Gửi không thành công2");
            });
        } else {
          this.showErrorMessage("Vui lòng kiểm tra lại các trường dữ liệu");
        }
      });
    },
    clearForm() {
      this.formData.tenKhachHang = "";
      this.formData.email = "";
      this.formData.soDienThoai = "";
      this.formData.diaChi = "";
      this.formData.xe = "";
      this.formData.dichVuId = "";
      this.formData.ngayHen = "";
    },
    showErrorMessage(message) {
      alert(message);
    },
  },
  watch: {
    "formData.dichVuId": function (newDichVuId) {
      const selectedDichVu = this.listDichVu.find(
        (dichvu) => dichvu.dichVuId === newDichVuId
      );
      if (selectedDichVu) {
        this.formData.tongTien = selectedDichVu.giaTien;
      }
    },
  },
};
</script>
