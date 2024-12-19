<template>
  <v-container>
    <v-row>
        <v-col lg="12" cols="12" style="text-align: center;">
            <v-subheader class="text-h4 ">Dịch vụ</v-subheader>
            <v-row justify="center">
              <v-col lg="6" cols="12">
                <v-text-field
                  placeholder="Search here"
                  append-inner-icon="mdi-magnify"
                  v-model="searchTerm"
                  @click:append-inner="searchService"
                />
              </v-col>
            </v-row>
          </v-col>
      <v-col cols="12">
        <v-card
          v-for="item in listService"
          :key="item.dichVuId"
          class="service-card"
        >
          <div class="card-content">
            <img
                style="max-height: 150px; max-width: 150px;"
                :src="item.img"
                class="service-image"
              />
            <div class="text-content">
              <v-card-title>{{ item.tenDichVu }}</v-card-title>
              <v-card-text>{{ item.moTa }}</v-card-text>
            </div>
            <div class="price">Giá dự kiến: {{ item.giaTien }}$</div>
          </div>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import axios from "axios";

export default {
  name: "AboutUs",
  data() {
    return {
      listService: [],
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    fetchData() {
      axios
        .get(`https://localhost:7040/api/DichVu`)
        .then((response) => {
          this.listService = response.data;
        })
        .catch((error) => {
          console.error(error);
          this.showErrorMessage(error);
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
  },
};
</script>
<style scoped>
p.description {
  font-size: 17px;
  margin: 23px 0px 0px 0px;
}
.service-card {
  margin-bottom: 20px;
}

.card-content {
  display: flex;
  align-items: left;
}

.service-image {
  margin-right: 16px;
}

.text-content {
  flex-grow: 1;
}

.price {
  padding: 30px;
  text-align: right;
}
</style>
