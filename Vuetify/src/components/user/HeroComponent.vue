<template>
    <v-container class="pt-15">
        <v-row>
            <v-col lg="6">
                <h2 class="intro-text">CHÀO MỪNG ĐẾN VỚI CARECAR</h2>
                <p class="description">
                    Tại CareCar, chúng tôi hiểu rằng việc duy trì và bảo dưỡng xe ô tô là yếu tố quan trọng 
                    để đảm bảo sự an toàn và hiệu suất tối đa của phương tiện của bạn. Chính vì vậy, chúng 
                    tôi cung cấp các dịch vụ chất lượng cao nhằm đáp ứng mọi nhu cầu chăm sóc xe ô tô của bạn.
                </p>   
                <v-btn color="success" class="white--text mr-10" rounded>Liên hệ</v-btn>
                <v-btn color="indigo" class="white--text" rounded>
                    <v-icon class="mr-2">mdi-play-circle</v-icon>  Đọc Thêm
                </v-btn>
            </v-col>
            <v-col lg="6">
                <img max-width="100%" src="@/assets/imf.png" alt="#" />
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
export default {
    name: "HeroComponent"
};
</script>
<style scoped>
    h2.intro-text{
        font-size: 60px;
        line-height: 100px;
        color: #444;
        font-weight: bold;
        padding-bottom: 10px;        
        margin-top: 30px;
    }
    p.description{
        font-size: 17px;
        margin: 32px 0px 50px 0px;
        
    }
</style>