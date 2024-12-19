<template>
    <v-container>
        <v-row>
            <v-col>
                <img src="@/assets/contact.png" alt="#"/>
            </v-col>
            <v-col>
                <v-subheader class="text-h4">Dịch vụ</v-subheader>
                <p class="description">Dịch vụ xe ô tô của chúng tôi cung cấp một loạt các dịch vụ chất lượng 
                    để đảm bảo xe ô tô của bạn được duy trì và hoạt động tốt.</p>
                <v-list>
                    <v-list-item>
                        <v-icon color="success" class="mr-3">mdi-check-all</v-icon>
                        <v-list-item-subtitle>Thay nhớt và bảo dưỡng định kỳ</v-list-item-subtitle>
                    </v-list-item>
                    <v-list-item>
                        <v-icon color="success" class="mr-3">mdi-check-all</v-icon>
                        <v-list-item-subtitle>Sửa chữa và bảo dưỡng hệ thống phanh</v-list-item-subtitle>
                    </v-list-item>
                    <v-list-item>
                        <v-icon color="success" class="mr-3">mdi-check-all</v-icon>
                        <v-list-item-subtitle>Sửa chữa và bảo dưỡng hệ thống treo và lái</v-list-item-subtitle>
                    </v-list-item>
                    <v-list-item>
                        <v-icon color="success">mdi-check-all</v-icon>
                        <v-list-item-subtitle>Kiểm tra và sửa chữa hệ thống điện</v-list-item-subtitle>
                    </v-list-item>
                    <v-list-item>
                        <v-icon color="success">mdi-check-all</v-icon>
                        <v-list-item-subtitle>Nâng cấp và độ xe</v-list-item-subtitle>
                    </v-list-item>
                    <v-list-item>
                        <v-icon color="success">mdi-check-all</v-icon>
                        <v-list-item-subtitle> ...</v-list-item-subtitle>
                    </v-list-item>
                </v-list>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
// import Headerr from "@/components/user/Header"
// import Footerr from "@/components/user/Footer"
export default {
    name: "AboutUs",
};
</script>
<style scoped>
p.description{
    font-size: 17px;
    margin: 23px 0px 0px 0px;
    
}
</style>