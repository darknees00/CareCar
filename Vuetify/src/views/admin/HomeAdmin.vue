<template>
  <div class="dashboard" style="margin: 30px">
    <v-subheader class="d-flex justify-space-between align-center">
      <h3>Dashboard</h3>
      <!-- <v-col lg="6" cols="12">
        <v-text-field
          placeholder="Search here"
          append-inner-icon="mdi-magnify"
          v-model="searchTerm"
          @click:append-inner="searchBooking"
        />
      </v-col> -->
    </v-subheader>
    <v-row style="margin: 30px">
      <v-col lg="3" cols="12" v-for="(i, index) in activities" :key="index">
        <v-card elevation-2 class="rounded-lg">
          <v-card-text class="d-flex justify-space-between align-center">
            <div class="">
              <strong>{{ i.title }}</strong>
            </div>
            <v-avatar :color="i.color" size="60">
              <span class="white--text">{{ i.amouts }}</span>
            </v-avatar>
          </v-card-text>
        </v-card>
      </v-col>
      <v-select
        v-model="filterTrangThai"
        label="Trạng Thái"
        :items="listStatus"
        item-value="trangThaiId"
        item-title="tenTrangThai"
        @input="filterData"
      ></v-select>
      <v-col lg="12">
        <v-subheader class="text-h5">Danh sách lịch hẹn chưa thanh toán </v-subheader>
      </v-col>
      <v-col lg="12" cols="12">
        <v-table>
          <thead>
            <tr>
              <th class="text-left">STT</th>
              <th class="text-left">Tên Khách Hàng</th>
              <th class="text-left">Tên Dịch vụ</th>
              <th class="text-left">Hãng xe</th>
              <th class="text-left">Ngày đặt lịch</th>
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
                <v-btn color="primary" @click="editBooking(item)"
                  >Cập nhật</v-btn
                >
              </td>
            </tr>
          </tbody>
        </v-table>
      </v-col>
      <v-col lg="12">
        <v-subheader class="text-h5 ">Danh sách thông tin yêu cầu liên hệ </v-subheader>
      </v-col>
      <v-col lg="12" cols="12">
        <v-table>
          <thead>
            <tr>
              <th class="text-left">STT</th>
              <th class="text-left">Họ tên</th>
              <th class="text-left">Email</th>
              <th class="text-left">Số điện thoại</th>
              <th class="text-left">Message</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, id) in listContact" :key="item.contactId">
              <td>{{ id + 1 }}</td>
              <td>{{ item.name }}</td>
              <td>{{ item.email }}</td>
              <td>{{ item.phone }}</td>
              <td>{{ item.message }}</td>
              <td>
                <v-btn color="primary" @click="detailContact(item)"
                  >Chi tiết</v-btn
                >
                <v-btn color="error" @click="deleteContact(item)">Xóa</v-btn>
              </td>
            </tr>
          </tbody>
        </v-table>
      </v-col>
    </v-row>
  </div>
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
            readonly
          ></v-autocomplete>
          <v-text-field
            v-model="editingBooking.xe"
            label="Loại xe"
            readonly
          ></v-text-field>
          <v-autocomplete
            v-model="editingBooking.dichVuId"
            :items="listService"
            label="Dịch vụ"
            item-title="tenDichVu"
            item-value="dichVuId"
            readonly
          ></v-autocomplete>
          <v-text-field
            v-model="editingBooking.ngayHen"
            label="Ngày hẹn"
            type="date"
            :value="formattedNgayHen"
            readonly
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
            readonly
          ></v-select>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="saveBooking">Đã thanh toán</v-btn>
        <v-btn color="error" @click="cancel">Hủy</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <v-dialog v-model="dialogDetail" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Chi tiết liên hệ</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model="detail.name"
            label="Họ tên"
            readonly
          ></v-text-field>
          <v-text-field
            v-model="detail.email"
            label="Email"
            readonly
          ></v-text-field>
          <v-text-field
            v-model="detail.phone"
            label="Số điện thoại"
            readonly
          ></v-text-field>
          <v-textarea
            v-model="detail.message"
            label="Message"
            readonly
          ></v-textarea>
        </v-form>
      </v-card-text>
      <v-card-actions>
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
      activities: [
        { title: "Số lượng dịch vụ", color: "red", amouts: 0 },
        {
          title: "Tổng tiền lịch hẹn đã thanh toán",
          color: "purple",
          amouts: 0,
        },
        { title: "Lịch đặt chưa thanh toán", color: "indigo", amouts: 0 },
        { title: "Số lượng khách hàng", color: "light-blue", amouts: 0 },
      ],
      listCustomer: [],
      listService: [],
      listBooking: [],
      listStatus: [],
      listContact: [],
      filteredData: [],
      filterTrangThai: null,
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
      searchTerm: "",
      editingBooking: null,
      dialogVisible: false,
      dialogDetail: false,
      savingBooking: false,
      detail: null,
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
    this.filteredData = [...this.listBooking];
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
    formatDate(date) {
      const formattedDate = new Date(date);
      const options = { year: "numeric", month: "long", day: "numeric" };
      return formattedDate.toLocaleDateString("vi-VN", options);
    },
    fetchData() {
      axios
        .get("https://localhost:7040/api/DichVu")
        .then((response) => {
          this.listService = response.data;
          this.activities[0].amouts = this.listService.length;
        })
        .catch((error) => {
          console.error(error);
        });
      axios
        .get("https://localhost:7040/api/KhachHang")
        .then((response) => {
          this.listCustomer = response.data;
          this.activities[3].amouts = this.listCustomer.length;
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
        .get("https://localhost:7040/api/LichHen/ChuaThanhToan")
        .then((response) => {
          console.log(response.data);
          this.listBooking = response.data;
          this.activities[2].amouts = this.listBooking.length;
        })
        .catch((error) => {
          console.error(error);
          this.showErrorMessage("Tải dữ liệu k thành công");
        });
      axios
        .get(`https://localhost:7040/api/LienHe`)
        .then((response) => {
          this.listContact = response.data;
        })
        .catch((error) => {
          console.log(error);
          this.showErrorMessage(error);
        });
      axios
        .get("https://localhost:7040/api/LichHen/totalPaidAmount")
        .then((response) => {
          this.activities[1].amouts = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    getTenDichVu(dichVuId) {
      const dichVu = this.listService.find(
        (dichVu) => dichVu.dichVuId === dichVuId
      );
      return dichVu ? dichVu.tenDichVu : "";
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
    editBooking(Booking) {
      this.dialogVisible = true; // Show the dialog
      this.editingBooking = Object.assign({}, Booking); // Create a copy of the Booking object
    },
    saveBooking() {
      this.savingBooking = true;
      if (this.editingBooking) {
        this.editingBooking.thanhToan = "Đã thanh toán";
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
            this.savingBooking = false;
            this.showErrorMessage("Lưu không thành công");
          });
      }
    },
    showErrorMessage(message) {
      alert(message);
    },
    filterData() {
      let filteredData = this.listBooking; // Tạo một bản sao của listBooking ban đầu

      if (this.filterTrangThai !== null) {
        filteredData = filteredData.filter(
          (item) => item.trangThaiId === this.filterTrangThai
        );
      }

      this.listBooking = filteredData; // Cập nhật mảng filteredData với dữ liệu đã được lọc
    },
    cancel() {
      this.dialogVisible = false;
      this.dialogDetail = false;
    },

    deleteContact(listContact) {
      const confirmDelete = confirm(
        "Bạn có chắc chắn muốn xóa thông tin liên hệ tư vấn này không ?"
      );
      if (confirmDelete) {
        axios
          .delete(`https://localhost:7040/api/LienHe/${listContact.contactId}`)
          .then(() => {
            this.fetchData();
          })
          .catch((error) => {
            console.log(error);
            this.showErrorMessage(error);
          });
      }
    },

    detailContact(listContact) {
      this.detail = { ...listContact };
      this.dialogDetail = true;
    },
  },
  watch: {
    filterTrangThai: function (newVal, oldVal) {
      if (newVal !== oldVal) {
        this.filterData();
      }
    },
    "editingBooking.dichVuId": function (newDichVuId) {
      const selectedDichVu = this.listService.find(
        (dichvu) => dichvu.dichVuId === newDichVuId
      );
      if (selectedDichVu) {
        this.editingBooking.tongTien = selectedDichVu.giaTien;
      }
    },
  },
};
</script>
