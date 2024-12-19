<template>
  <v-navigation-drawer app>
    <v-img src="@/assets/logo.png" />

    <v-list>
      <v-list-item
        v-for="[icon, text] in links"
        :key="icon"
        :prepend-icon="icon"
        :title="text"
        link
        @click="navigateToPage(text)"
      ></v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
export default {
  data: () => ({
    links: [
      ["mdi-microsoft-windows", "Trang chủ"],
      ["mdi-clipboard-list", "Lịch đặt"],
      ["mdi-face-agent", "Khách hàng"],
      ["mdi-clipboard-list-outline", "Dịch vụ"],
      ["mdi-account", "Nhân Viên"],
      ["mdi-cog", "Thông tin tài khoản"],
      ["mdi-logout", "Đăng xuất"],
    ],
    userRole: "",
  }),
  methods: {
    navigateToPage(text) {
      const userData = localStorage.getItem("user");
      if (userData) {
        const user = JSON.parse(userData);
        this.userRole = user.quyen;
      }
      let confirmDeleteResult;
      switch (text) {
        case "Trang chủ":
          this.$router.push("/admin");
          break;
        case "Lịch đặt":
          this.$router.push("/admin/booking1");
          break;
        case "Khách hàng":
          this.$router.push("/admin/customer");
          break;
        case "Dịch vụ":
          if (this.userRole === "nhanvien") {
            return;
          }
          this.$router.push("/admin/service");
          break;
        case "Nhân Viên":
          if (this.userRole === "nhanvien") {
            return;
          }
          this.$router.push("/admin/employee");
          break;
        case "Thông tin tài khoản":
          this.$router.push("/admin/profile-account");
          break;
        case "Đăng xuất":
          confirmDeleteResult = confirm(
            "Bạn có chắc chắn muốn đăng xuất không?"
          );
          if (confirmDeleteResult) {
            localStorage.removeItem("user");
            this.$router.push("/admin/login");
          }
          break;
        default:
          break;
      }
    },
  },
};
</script>
