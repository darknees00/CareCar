<template>
  <div>
    <div class="maxWidth">
      <div>
        <v-row class="ma-0" justify="center" style="min-height: 90vh">
          <v-col cols="8" align-self="center">
            <div class="">
              <v-row class="ma-0" justify="center">
                <v-col cols="auto">
                  <h2 class="text-uppercase">Đăng nhập trang quản trị</h2>
                </v-col>
              </v-row>
              <v-row class="ma-0 mt-3" justify="center">
                <v-col cols="12" md="10" lg="8">
                  <v-form ref="form">
                    <div>
                      <p class="mb-2 font-weight-bold">
                        Tên đăng nhập <span class="text-error">*</span>
                      </p>
                      <v-text-field
                        v-model="tenDangNhap"
                        density="compact"
                        hide-details="auto"
                        variant="outlined"
                        :rules="[(v) => !!v || 'Vui lòng nhập tên đăng nhập']"
                        placeholder="Tên đăng nhập"
                      ></v-text-field>
                    </div>
                    <div class="mt-3">
                      <p class="mb-2 font-weight-bold">
                        Mật khẩu <span class="text-error">*</span>
                      </p>
                      <v-text-field
                        v-model="matKhau"
                        density="compact"
                        hide-details="auto"
                        variant="outlined"
                        type="password"
                        :rules="[(v) => !!v || 'Vui lòng nhập mật khẩu']"
                        placeholder="Mật khẩu"
                      ></v-text-field>
                    </div>
                    <div class="mt-4">
                      <v-btn width="100%" color="#4a90e2" @click="checkAccount">
                        Đăng nhập</v-btn
                      >
                    </div>
                  </v-form>
                </v-col>
              </v-row>
              <v-row class="ma-0" justify="center">
                <v-col cols="12" md="10" lg="8" style="text-align: center">
                  <p class="">Tổng đài hỗ trợ : <b>0397 815 746</b></p>
                  <p>
                    Hãy đồng hành cùng CareCar và trải nghiệm dịch vụ chăm sóc
                    xe ô tô chất lượng hàng đầu!
                  </p>
                </v-col>
              </v-row>
            </div>
          </v-col>
        </v-row>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      tenDangNhap: "",
      matKhau: "",
    };
  },
  methods: {
    checkAccount() {
      axios
        .get(
          `https://localhost:7040/api/NhanVien/GetByName/${this.tenDangNhap}`
        )
        .then((response) => {
          if (response.data.matKhau === this.matKhau) {
            let user = {
              nhanVienId: response.data.nhanVienId,
              tenDangNhap: response.data.tenDangNhap,
              matKhau: response.data.matKhau,
              hoTen: response.data.hoTen,
              email: response.data.email,
              phone: response.data.phone,
              quyen: response.data.quyen
            };
            localStorage.setItem("user", JSON.stringify(user));
            // this.showErrorMessage("Đăng nhập thành công");
            this.$router.push("/admin");
          } else {
            this.showErrorMessage("Thông tin đăng nhập không chính xác !");
          }
        })
        .catch((err) => {
          console.log(err);
          this.showErrorMessage("Thông tin đăng nhập không chính xác");
        });
    },
    showErrorMessage(message) {
      alert(message);
    },
  },
};
</script>
