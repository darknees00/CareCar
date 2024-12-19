<template>
    <v-app-bar 
          style="padding: 0px 90px;"
      app 
      color="while" 
      light 
      elevation="6" 
      elevate-on-scroll
    >
      <v-toolbar-title>CareCar</v-toolbar-title>

      <v-list class="d-flex align-center">
        <v-list-item link v-for="(menu,index) in menus" :key="index" :to="menu.route">
          <v-list-item-title>{{menu.title}}</v-list-item-title>
        </v-list-item>
      </v-list>
      <v-btn text>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>
      <v-btn tonal color="success" to="/booking-appointment">Đặt lịch hẹn</v-btn>
    </v-app-bar>
</template>
<script>
export default{
    data(){
    return{
      menus:[
        {title: 'Trang chủ',  route:'/'},
        {title: 'Dịch vụ',    route:'/service'},
        {title: 'Giới thiệu', route:'/about-us'},
        {title: 'Liên hệ',    route:'/contact'},
      ]
    }
  }
}
</script>