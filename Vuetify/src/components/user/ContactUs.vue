<template>
  <v-container>
    <v-row justify="center">
      <v-col lg="12" class="text-center">
        <v-subheader class="text-h4 justify-center">Liên hệ</v-subheader>
        <p>Mọi thắc mắc hay yêu cầu hãy liên hệ ngay với chúng tôi !</p>
      </v-col>
      <v-col lg="6">
        <iframe
          src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d7865144.833266968!2d100.61311550318408!3d15.729819263356687!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31157a4d736a1e5f%3A0xb03bb0c9e2fe62be!2sVietnam!5e0!3m2!1sen!2s!4v1699347958577!5m2!1sen!2s"
          width="100%"
          height="450"
          style="border: 0"
          loading="lazy"
          referrerpolicy="no-referrer-when-downgrade"
        ></iframe>
      </v-col>
      <v-col lg="6">
        <v-form ref="form">
          <v-text-field
            v-model="contactInfo.name"
            :counter="10"
            label="Họ tên"
            :rules="[
              (v) => !!v || 'Không được để trống',
              (v) => /^[^\d]+$/.test(v) || 'Họ tên không được chứa số',
            ]"
            repuired
          ></v-text-field>
          <v-text-field
            v-model="contactInfo.email"
            :counter="10"
            label="E-mail"
            :rules="[
              (v) => !!v || 'Không được để trống',
              (v) => /.+@.+\..+/.test(v) || 'Email không hợp lệ',
            ]"
            type="email"
            repuired
          ></v-text-field>
          <v-text-field
            v-model="contactInfo.phone"
            :counter="10"
            label="Số điện thoại"
            :rules="[
              (v) => !!v || 'Không được để trống',
              (v) => /^[0-9]{10}$/.test(v) || 'Số điện thoại không hợp lệ',
            ]"
            type="number"
            repuired
          ></v-text-field>
          <v-textarea
            label="..."
            v-model="contactInfo.message"
            :rules="[(v) => !!v || 'Không được để trống']"
            required
          ></v-textarea>
          <v-btn color="success" class="mr-4" @click="sendContact"> Gửi</v-btn>
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import axios from "axios";

export default {
  name: "ContactUs",
  data() {
    return {
      contactInfo: {
        name: "",
        email: "",
        phone: "",
        message: "",
      },
    };
  },
  methods: {
    sendContact() {
      this.$refs.form.validate().then((valid) => {
        if (valid) {
          if (
            this.contactInfo.name &&
            this.contactInfo.email &&
            this.contactInfo.phone &&
            this.contactInfo.message
          ) {
            axios
              .post("https://localhost:7040/api/LienHe", this.contactInfo)
              .then(() => {
                this.showErrorMessage("Thông tin liên hệ của bạn đã được gửi");
                this.clearForm();
              })
              .catch((error) => {
                console.log(error);
                this.showErrorMessage(error.message);
              });
          } else {
            this.showErrorMessage("Vui lòng điền đầy đủ thông tin liên hệ");
          }
        } else {
          this.showErrorMessage("Vui lòng kiểm tra lại các trường dữ liệu");
        }
      });
    },
    clearForm() {
      this.contactInfo.name = "";
      this.contactInfo.email = "";
      this.contactInfo.phone = "";
      this.contactInfo.message = "";
    },
    showErrorMessage(message) {
      alert(message);
    },
  },
};
</script>
