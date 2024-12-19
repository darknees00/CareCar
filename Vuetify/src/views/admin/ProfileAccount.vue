<template>
  <div class="dashboard" style="margin: 100px">
    <v-subheader class="d-flex justify-space-between align-center">
      <h3>Thông tin tài khoản</h3>
      <v-btn color="primary" class="mr-4" @click="openPasswordDialog">
        Đổi mật khẩu
      </v-btn>
    </v-subheader>
    <v-col lg="12" style="margin-top: 50px">
      <v-form ref="form">
        <v-text-field
          v-model="profile.hoTen"
          :label="editingEnabled ? 'Họ tên (Chỉnh sửa đã bật)' : 'Họ tên'"
          :readonly="!editingEnabled"
        ></v-text-field>
        <v-text-field
          v-model="profile.tenDangNhap"
          :label="
            editingEnabled
              ? 'Tên đăng nhập (Chỉnh sửa đã bật)'
              : 'Tên đăng nhập'
          "
          :readonly="!editingEnabled"
        ></v-text-field>
        <v-text-field
          v-model="profile.matKhau"
          label="Mật khẩu"
          readonly
          type="password"
        ></v-text-field>
        <v-text-field
          v-model="profile.email"
          :label="editingEnabled ? 'Email (Chỉnh sửa đã bật)' : 'Email'"
          :readonly="!editingEnabled"
          type="email"
        ></v-text-field>
        <v-text-field
          v-model="profile.phone"
          :label="
            editingEnabled
              ? 'Số điện thoại (Chỉnh sửa đã bật)'
              : 'Số điện thoại'
          "
          :readonly="!editingEnabled"
          type="number"
        ></v-text-field>
        <v-text-field
          v-model="profile.quyen"
          label="Quyền"
          readonly
        ></v-text-field>
        <v-btn
          color="success"
          class="mr-4"
          @click="saveChanges"
          v-if="editingEnabled"
        >
          Lưu thay đổi
        </v-btn>
        <v-btn
          color="success"
          class="rounded-lg"
          @click="enableEditing"
          v-show="!editingEnabled"
        >
          Chỉnh sửa
        </v-btn>
      </v-form>
    </v-col>
  </div>
  <v-dialog v-model="passwordDialogVisible" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Đổi mật khẩu</span>
      </v-card-title>
      <v-card-text>
        <v-form ref="passwordForm">
          <v-text-field
            v-model="currentPassword"
            label="Mật khẩu hiện tại"
          ></v-text-field>
          <v-text-field
            v-model="newPassword"
            label="Mật khẩu mới"
            type="password"
          ></v-text-field>
          <v-text-field
            v-model="confirmNewPassword"
            label="Xác nhận mật khẩu mới"
            type="password"
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn
          color="primary"
          @click="changePassword"
          :disabled="!validatePasswordForm"
        >
          Lưu thay đổi
        </v-btn>
        <v-btn color="error" text @click="closePasswordDialog"> Hủy </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      profile: {
        hoTen: "",
        tenDangNhap: "",
        matKhau: "",
        email: "",
        phone: "",
        quyen: "",
      },
      editingEnabled: false,
      passwordDialogVisible: false,
      currentPassword: "",
      newPassword: "",
      confirmNewPassword: "",
    };
  },
  methods: {
    loadProfileFromLocalStorage() {
      const profileData = localStorage.getItem("user");
      if (profileData) {
        this.profile = JSON.parse(profileData);
      }
    },
    enableEditing() {
      this.editingEnabled = true;
    },
    saveChanges() {
      if (this.validateFormData()) {
        localStorage.setItem("user", JSON.stringify(this.profile));
        axios
          .put(
            `https://localhost:7040/api/NhanVien/${this.profile.nhanVienId}`,
            this.profile
          )
          .then(() => {
            this.editingEnabled = false;
          })
          .catch((error) => {
            console.error(error);
            this.showErrorMessage("Lưu không thành công");
          });
      } else {
        this.showErrorMessage("Dữ liệu không hợp lệ");
      }
    },
    validateFormData() {
      if (this.profile.hoTen.trim() === "") {
        return false;
      }
      if (this.profile.tenDangNhap.trim() === "") {
        return false;
      }
      if (this.profile.matKhau.trim() === "") {
        return false;
      }
      if (this.profile.email.trim() === "") {
        return false;
      }
      if (this.profile.phone.trim() === "") {
        return false;
      }
      return true;
    },
    showErrorMessage(message) {
      alert(message);
    },
    openPasswordDialog() {
      this.passwordDialogVisible = true;
    },

    closePasswordDialog() {
      this.passwordDialogVisible = false;
    },

    changePassword() {
      if (this.validatePasswordForm()) {
        if (this.currentPassword !== this.profile.matKhau) {
          this.showErrorMessage("Mật khẩu hiện tại không đúng");
          return;
        }
        if (this.newPassword !== this.confirmNewPassword) {
          this.showErrorMessage("Xác nhận mật khẩu mới không khớp");
          return;
        }
        this.profile.matKhau = this.newPassword; // Cập nhật mật khẩu mới cho profile
        localStorage.setItem("user", JSON.stringify(this.profile));
        axios
          .put(
            `https://localhost:7040/api/NhanVien/${this.profile.nhanVienId}`,
            this.profile
          )
          .then(() => {
            this.showErrorMessage("Đổi mật khẩu thành công");
          })
          .catch((error) => {
            console.error(error);
            this.showErrorMessage("Lưu không thành công");
          });
        this.closePasswordDialog();
        this.currentPassword = "";
        this.newPassword = "";
        this.confirmNewPassword = "";
      } else {
        this.showErrorMessage("Dữ liệu không hợp lệ");
      }
    },

    validatePasswordForm() {
      if (this.currentPassword.trim() === "") {
        return false;
      }
      if (this.newPassword.trim() === "") {
        return false;
      }
      if (this.confirmNewPassword.trim() === "") {
        return false;
      }
      return true;
    },
  },
  mounted() {
    this.loadProfileFromLocalStorage();
  },
};
</script>
