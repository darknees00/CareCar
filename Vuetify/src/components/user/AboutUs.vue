<template>
    <v-container class="pt-15">
        <v-row>
            <v-col lg="6">
                <v-subheader class="text-h4">Thông tin về chúng tôi</v-subheader>
                <p class="description">
                    Trang web của chúng tôi cung cấp một môi trường thân thiện và thuận tiện để bạn khám
                     phá các dịch vụ chăm sóc xe ô tô đa dạng. Từ việc thay nhớt và bảo dưỡng định kỳ cho 
                     đến sửa chữa và nâng cấp, chúng tôi có đội ngũ kỹ thuật viên chuyên nghiệp và giàu kinh
                    nghiệm sẵn sàng đáp ứng mọi nhu cầu của bạn.
                </p>
                <p class="description">
                    Trang web của chúng tôi cung cấp một miêu tả chi tiết về các dịch vụ mà chúng tôi 
                    cung cấp, giúp bạn hiểu rõ hơn về quy trình và lợi ích của từng dịch vụ. Bên cạnh đó
                    , chúng tôi cũng cung cấp thông tin về đội ngũ kỹ thuật viên của chúng tôi, đảm bảo
                     rằng bạn có thể tin tưởng vào sự chuyên môn và sự chăm sóc của chúng tôi.
                </p>
                <p class="description">
                    Hãy đồng hành cùng CareCar và trải nghiệm dịch vụ chăm sóc xe ô tô chất lượng hàng đầu!
                </p>
                <v-btn color="indigo" class="white--text" rounded>
                    Đọc Tiếp
                </v-btn>
            </v-col>
            <v-col lg="6">
                <img max-width="100%" src="@/assets/logo2.png" alt="#" />
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
    p.description{
        font-size: 17px;
        margin: 32px 0px 10px 0px;
        
    }
</style>