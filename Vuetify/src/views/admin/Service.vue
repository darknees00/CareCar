<template>
  <v-container style="margin: 30px">
    <v-subheader class="d-flex justify-space-between align-center">
      <h3>Dịch vụ</h3>
      <v-col lg="6" cols="12">
        <v-text-field
          placeholder="Search here"
          append-inner-icon="mdi-magnify"
          v-model="searchTerm"
          @click:append-inner="searchService"
        />
      </v-col>
      <v-btn color="success" @click="dialogAddService">Thêm</v-btn>
    </v-subheader>
    <v-table>
      <thead>
        <tr>
          <th class="text-left">STT</th>
          <th class="text-left">Dịch vụ</th>
          <th class="text-left">Giá</th>
          <th class="text-left">Mô tả</th>
          <th class="text-left">Ảnh</th>
          <th class="text-left">Chức năng</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, id) in listService" :key="item.dichVuId">
          <td>{{ id + 1 }}</td>
          <td>{{ item.tenDichVu }}</td>
          <td>{{ item.giaTien }}$</td>
          <td>{{ item.moTa }}</td>
          <td>
            <img style="max-width: 100px; max-height: 100px;" :src="item.img" alt="#"/>
          </td>
          <td>
            <v-btn color="primary" @click="editService(item)">Sửa</v-btn>
            <v-btn color="error" @click="deleteService(item)">Xóa</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>
  </v-container>
  <v-dialog v-model="dialogVisible" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline"> Dịch vụ</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model="editingService.tenDichVu"
            label="Tên dịch vụ"
            :rules="[(v) => !!v || 'Tên dịch vụ không được để trống']"
          ></v-text-field>
          <v-text-field
            v-model="editingService.giaTien"
            label="Giá"
            :rules="[(v) => !!v || 'Giá không được bỏ trống']"
            type="number"
          ></v-text-field>
          <v-text-field
            v-model="editingService.moTa"
            label="Mô tả"
            :rules="[(v) => !!v || 'Không được bỏ trống']"
          ></v-text-field>
          <v-text-field
            label="Hình ảnh thay thế"
            type="file"
            @change="uploadFile"
            accept="image/jpg, image/jpeg, image/png"
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="saveService">Lưu</v-btn>
        <v-btn color="error" @click="closeDialog">Hủy</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <v-dialog v-model="dialogAdd" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Thêm dịch vụ</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model="service.tenDichVu"
            label="Tên dịch vụ"
            :rules="[(v) => !!v || 'Tên dịch vụ không được để trống']"
          ></v-text-field>
          <v-text-field
            v-model="service.giaTien"
            label="Giá"
            :rules="[(v) => !!v || 'Giá không được bỏ trống']"
            type="number"
          ></v-text-field>
          <v-text-field
            v-model="service.moTa"
            label="Mô tả"
            :rules="[(v) => !!v || 'Không được bỏ trống']"
          ></v-text-field>
          <v-text-field
            label="Ảnh"
            type="file"
            :rules="[(v) => !!v || 'Không được để trống']"
            @change="uploadFile"
            accept="image/jpg, image/jpeg, image/png"
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="addService">Lưu</v-btn>
        <v-btn color="error" @click="closeDialog">Hủy</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      listService: [],
      service: {
        tenDichVu: "",
        giaTien: "",
        moTa: "",
        img: "",
      },
      searchTerm: "",
      editingService: null,
      dialogVisible: false,
      dialogAdd: false,
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    validateForm() {
      if (!this.service.tenDichVu) {
        this.showErrorMessage("Tên dịch vụ không được để trống");
        return false;
      }
      if (!this.service.giaTien) {
        this.showErrorMessage("Giá không được bỏ trống");
        return false;
      }
      if (!this.service.moTa) {
        this.showErrorMessage("Mô tả không được bỏ trống");
        return false;
      }
      // if (!this.service.img) {
      //   this.showErrorMessage("Ảnh không được bỏ trống");
      //   return false;
      // }
      return true;
    },
    fetchData() {
      axios
        .get(`https://localhost:7040/api/DichVu`)
        .then((response) => {
          this.listService = response.data;
        })
        .catch((error) => {
          console.error(error);
          this.showErrorMessage("Tải dữ liệu thất bại");
        });
    },
    searchService() {
      if (!this.searchTerm) {
        this.fetchData(); // Tải lại dữ liệu ban đầu
        return;
      }
      axios
        .get(
          `https://localhost:7040/api/DichVu/search?searchTerm=${this.searchTerm}`
        )
        .then((response) => {
          this.listService = response.data;
        })
        .catch((error) => {
          console.error(error);
          this.fetchData();
        });
    },
    dialogAddService() {
      this.dialogAdd = true;
    },
    addService() {
      if (this.validateForm()) {
        axios
          .post(`https://localhost:7040/api/DichVu`, this.service)
          .then(() => {
            this.fetchData();
            this.clearForm();
            this.dialogAdd = false;
          })
          .catch((error) => {
            console.error(error);
            this.showErrorMessage(error);
          });
      }
    },
    editService(Service) {
      this.dialogVisible = true; // Show the dialog
      this.editingService = Object.assign({}, Service); // Create a copy of the Service object
    },
    uploadFile(event) {
      const file = event.target.files[0];

      const formData = new FormData();
      formData.append("formFile", file);

      axios
        .post("https://localhost:7040/api/DichVu/Upload", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        })
        .then((response) => {
          this.service.img = response.data.url;
          this.editingService.img = response.data.url
        })
        .catch((error) => {
          console.error(error);
          this.showErrorMessage("Tải lên hình ảnh không thành công");
        });
    },
    
    saveService() {
      if (this.editingService) {
        axios
          .put(
            `https://localhost:7040/api/DichVu/${this.editingService.dichVuId}`,
            this.editingService
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
    deleteService(Service) {
      const confirmDelete = confirm(
        "Bạn có chắc chắn muốn xóa dịch vụ này không?"
      );
      if (confirmDelete) {
        axios
          .delete(`https://localhost:7040/api/DichVu/${Service.dichVuId}`)
          .then(() => {
            this.fetchData();
          })
          .catch((error) => {
            console.error(error);
            this.showErrorMessage("Xóa không thành công");
          });
      }
    },
    clearForm() {
      this.service = {
        tenDichVu: "",
        giaTien: "",
        moTa: "",
        img: "",
      };
    },
    closeDialog() {
      this.dialogVisible = false;
      this.dialogAdd = false;
    },
    showErrorMessage(message) {
      alert(message);
    },
  },
};
</script>
