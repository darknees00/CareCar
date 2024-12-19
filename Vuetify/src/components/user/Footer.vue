<template>
    <v-footer padless class="bg-indigo-lighten-1 text-center d-flex flex-column">
        <v-col lg="12">
            <v-row justify="center">
                <v-col lg="4">
                    <v-toolbar-title1 class="h1">CareCar</v-toolbar-title1>
                    <v-card-text>
                        <v-btn
                            v-for="icon in icons"
                            :key="icon"
                            class="mx-4 while--text"
                            icon
                        >
                            <v-icon size="24px">
                                {{ icon }}
                            </v-icon>
                        </v-btn>
                    </v-card-text>
                    <v-subheader> Hãy đồng hành cùng CareCar và trải nghiệm dịch vụ chăm sóc xe ô tô chất lượng hàng đầu!</v-subheader>
                </v-col>
                <v-col lg="12">
                    {{ new Date().getFullYear() }} — <strong>Darkness</strong>
                </v-col>
            </v-row>
        </v-col>
  </v-footer>
</template>
<script>
export default{
    data() {
        return {
            icons: [
                'mdi-facebook',
                'mdi-twitter',
                'mdi-linkedin',
                'mdi-instagram',
            ],
        }
    },
}
</script>
<style>
  .h1{
    font-size: 32px;
    color: #fff;
    font-weight: bold;
    font-family: cursive;
  }
</style>